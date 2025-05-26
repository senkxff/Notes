using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Microsoft.Win32;
using Newtonsoft.Json;
using TasksTracker.Commands;
using TasksTracker.Model;
using TasksTracker.View.Windows.WarningWindows;

namespace TasksTracker.ViewModel
{
    class TasksViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string inputTitle = "Новая задача";
        public string InputTitle
        {
            get { return inputTitle; }
            set
            {
                inputTitle = value;
                OnPropertyChanged();
            }
        }

        private string content = "";
        public string Content
        {
            get => content;
            set
            {
                content = value;
                OnPropertyChanged();

            }
        }

        private TaskModel selectedTask = new TaskModel();
        public TaskModel SelectedTask
        {
            get { return selectedTask; }
            set
            {
                selectedTask = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<TaskModel> tasks = new ObservableCollection<TaskModel>()
        {
            new TaskModel { Title = "Новая задача", Content = "" }
        };
        public ObservableCollection<TaskModel> Tasks
        {
            get { return tasks; }
            set
            {
                tasks = value;
                OnPropertyChanged();
            }
        }

        public TasksViewModel()
        {
            AddImageCommand = new CommonCommand(AddImage);
            AddTaskCommand = new CommonCommand(AddNote);
            DeleteTaskCommand = new CommonCommand(DeleteNote);
            SaveTaskCommand = new CommonCommand(async () => await Task.Run(() => SaveNoteAsync()));

            Parallel.Invoke(() => LoadTasksAsync(), () => LoadImagesAsync());

            SelectedTask = tasks[0];
        }

        public ICommand AddTaskCommand { get; }
        private void AddNote()
        {
            var newNote = new TaskModel { Title = "Новая задача", Content = "" };

            Tasks.Add(newNote);
            SelectedTask = newNote;
            InputTitle = string.Empty;
        }

        public ICommand DeleteTaskCommand { get; }
        private void DeleteNote()
        {
            if (SelectedTask != null && Tasks.Contains(SelectedTask))
            {
                if (Tasks.Count > 1)
                {
                    Tasks.Remove(SelectedTask);
                    SelectedTask = Tasks[Tasks.Count - 1];
                }
                else
                {
                    DeleteAllTasksWarningWindow deleteAllNotesWarningWindow = new DeleteAllTasksWarningWindow();
                    Window ownerWindow = Window.GetWindow(Application.Current.MainWindow);
                    deleteAllNotesWarningWindow.Owner = ownerWindow;
                    deleteAllNotesWarningWindow.ShowDialog();
                }
            }
            else
            {
                DeleteAllTasksWarningWindow deleteAllNotesWarningWindow = new DeleteAllTasksWarningWindow();
                Window ownerWindow = Window.GetWindow(Application.Current.MainWindow);
                deleteAllNotesWarningWindow.Owner = ownerWindow;
                deleteAllNotesWarningWindow.ShowDialog();
            }
        }

        public ICommand AddImageCommand { get; }
        private ObservableCollection<BitmapImage> noteImages = new ObservableCollection<BitmapImage>();
        public ObservableCollection<BitmapImage> NoteImages
        {
            get => noteImages;
            set
            {
                noteImages = value;
                OnPropertyChanged();
            }
        }

        private void AddImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "Image files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png|All files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    byte[] imageBytes = File.ReadAllBytes(openFileDialog.FileName);
                    string bmpBase64 = Convert.ToBase64String(imageBytes);
                    if (SelectedTask != null)
                    {
                        SelectedTask.ImagesBase64.Add(bmpBase64);
                        BitmapImage bitmap = new BitmapImage();
                        using (MemoryStream ms = new MemoryStream(imageBytes))
                        {
                            bitmap.BeginInit();
                            bitmap.CacheOption = BitmapCacheOption.OnLoad;
                            bitmap.StreamSource = ms;
                            bitmap.EndInit();
                            bitmap.Freeze();
                        }
                        SelectedTask.Images.Add(bitmap);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки изображения: {ex.Message}");
                }
            }
        }


        public ICommand SaveTaskCommand { get; }
        private bool isSyncing = false;
        private async void SaveNoteAsync()
        {
            if (isSyncing)
            {
                return;
            }
            else
            {
                isSyncing = true;
            }

            try
            {
                string localFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NotesCollection.json");

                string jsonNotes = JsonConvert.SerializeObject(tasks, Formatting.Indented);
                await File.WriteAllTextAsync(localFilePath, jsonNotes);

                UserCredential credential;
                using (var stream = new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
                {
                    string credPath = "token.json";
                    credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.FromStream(stream).Secrets,
                        new[] { DriveService.Scope.Drive },
                        "user3",
                        CancellationToken.None,
                        new FileDataStore(credPath, true));
                }

                var service = new DriveService(new BaseClientService.Initializer
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "Notes"
                });

                var listRequest = service.Files.List();
                listRequest.Q = "name = 'NotesCollection.json' and trashed = false";
                listRequest.Spaces = "drive";
                var fileList = await listRequest.ExecuteAsync();

                string? fileId = fileList.Files != null && fileList.Files.Count > 0 ? fileList.Files.First().Id : null;

                var fileMetadata = new Google.Apis.Drive.v3.Data.File()
                {
                    Name = "NotesCollection.json"
                };

                using (var fileStream = new FileStream(localFilePath, FileMode.Open))
                {
                    if (fileId == null)
                    {
                        var createRequest = service.Files.Create(fileMetadata, fileStream, "application/json");
                        await createRequest.UploadAsync();
                        fileId = createRequest.ResponseBody.Id;
                    }
                    else
                    {
                        var updateRequest = service.Files.Update(fileMetadata, fileId, fileStream, "application/json");
                        await updateRequest.UploadAsync();
                    }
                }

                if (fileId != null)
                {
                    var downloadRequest = service.Files.Get(fileId);
                    var memoryStream = new MemoryStream();
                    await downloadRequest.DownloadAsync(memoryStream);
                    memoryStream.Position = 0;
                    using (var reader = new StreamReader(memoryStream))
                    {
                        string downloadedJson = await reader.ReadToEndAsync();
                        var updatedNotes = JsonConvert.DeserializeObject<ObservableCollection<TaskModel>>(downloadedJson);

                        if (updatedNotes != null)
                        {
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                tasks.Clear();
                                foreach (var note in updatedNotes)
                                {
                                    tasks.Add(note);
                                }
                            });
                        }
                    }
                }
                await Task.Run(() => LoadImagesAsync());
            }
            catch (Exception e)
            {
                MessageBox.Show($"Произошло исключение {e.Message}! Пожалуйста, перезапустите программу и сообщите о проблеме разработчику!");
            }
            finally
            {
                isSyncing = false;
            }
        }

        private async Task LoadTasksAsync()
        {
            string localFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NotesCollection.json");

            if (File.Exists(localFilePath))
            {
                string json = await File.ReadAllTextAsync(localFilePath);
                var loadedNotes = JsonConvert.DeserializeObject<ObservableCollection<TaskModel>>(json);

                if (loadedNotes != null)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        tasks.Clear();
                        foreach (var note in loadedNotes)
                        {
                            tasks.Add(note);
                        }
                    });
                }
            }
        }

        private async Task LoadImagesAsync()
        {
            string localFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NotesCollection.json");

            if (File.Exists(localFilePath))
            {
                string json = await File.ReadAllTextAsync(localFilePath);
                var loadedNotes = JsonConvert.DeserializeObject<ObservableCollection<TaskModel>>(json);
                if (loadedNotes != null)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        tasks.Clear();
                        foreach (var note in loadedNotes)
                        {
                            note.Images.Clear();
                            foreach (var base64 in note.ImagesBase64)
                            {
                                BitmapImage image = new BitmapImage();
                                byte[] imageBytes = Convert.FromBase64String(base64);
                                using (MemoryStream ms = new MemoryStream(imageBytes))
                                {
                                    image.BeginInit();
                                    image.CacheOption = BitmapCacheOption.OnLoad;
                                    image.StreamSource = ms;
                                    image.EndInit();
                                    image.Freeze();
                                }
                                note.Images.Add(image);
                            }
                            tasks.Add(note);
                        }
                    });
                }
            }
        }
    }
}