using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
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
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string inputTitle = "";
        public string InputTitle
        {
            get => inputTitle;
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

        private TaskModel selectedTask;
        public TaskModel SelectedTask
        {
            get => selectedTask;
            set
            {
                if (selectedTask != value)
                {
                    if (selectedTask != null)
                    {
                        selectedTask.PropertyChanged -= Task_PropertyChanged;
                    }
                    selectedTask = value;
                    if (selectedTask != null)
                    {
                        selectedTask.PropertyChanged += Task_PropertyChanged;
                        System.Diagnostics.Debug.WriteLine($"SelectedTask Priority: {selectedTask.Priority}");
                    }
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<TaskModel> tasks = new ObservableCollection<TaskModel>
        {
            new TaskModel { Title = "Новая задача", Content = "", DateTask = "Сегодня", Priority = "Low" }
        };
        public ObservableCollection<TaskModel> Tasks
        {
            get => tasks;
            set
            {
                tasks = value;
                OnPropertyChanged();
            }
        }

        public TasksViewModel()
        {
            AddImageCommand = new CommonCommand(AddImage);
            AddTaskCommand = new CommonCommand(AddTask);
            DeleteTaskCommand = new CommonCommand(DeleteTask);
            SaveTaskCommand = new CommonCommand(async () => await SaveTasksAsync());
            MarkAsImportantCommand = new CommonCommand(MarkAsImportant);
            ChangeAccountCommand = new CommonCommand(async () => await ChangeAccountForButton());
            MarkAsCheckedCommand = new CommonCommand(MarkAsChecked);

            LoadTasksAsync();
            LoadImagesAsync();

            SelectedTask = tasks[0];
            foreach (var task in tasks)
            {
                task.PropertyChanged += Task_PropertyChanged;
            }
        }

        private void Task_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(TaskModel.DateTask) || e.PropertyName == nameof(TaskModel.IsImportant) || e.PropertyName == nameof(TaskModel.Priority))
            {
                System.Diagnostics.Debug.WriteLine($"Property changed: {e.PropertyName}, Task: {((TaskModel)sender).Title}");
                SortTasks();
            }
        }

        private DateTime ParseDateTask(string dateTask)
        {
            if (string.IsNullOrWhiteSpace(dateTask) || dateTask.Equals("Сегодня", StringComparison.OrdinalIgnoreCase))
            {
                return DateTime.Today;
            }

            string[] formats = { "dd.MM.yyyy", "MM/dd/yyyy", "yyyy-MM-dd", "d.M.yyyy", "M/d/yyyy", "dd/MM/yyyy" };
            if (DateTime.TryParseExact(dateTask, formats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime date) ||
                DateTime.TryParse(dateTask, System.Globalization.CultureInfo.InvariantCulture, out date))
            {
                return date;
            }

            System.Diagnostics.Debug.WriteLine($"Invalid DateTask format: {dateTask}");
            return DateTime.MaxValue;
        }

        private int GetPriorityOrder(string priority)
        {
            return priority switch
            {
                "Критический" => 4,
                "Высокий" => 3,
                "Средний" => 2,
                "Низкий" => 1,
                _ => 0
            };
        }

        private void SortTasks()
        {
            var currentSelectedTask = SelectedTask;
            var pinnedTasks = tasks.Where(t => t.IsImportant)
                                  .OrderByDescending(t => GetPriorityOrder(t.Priority))
                                  .ThenByDescending(t => ParseDateTask(t.DateTask))
                                  .ToList();
            var nonPinnedTasks = tasks.Where(t => !t.IsImportant)
                                     .OrderByDescending(t => GetPriorityOrder(t.Priority))
                                     .ThenByDescending(t => ParseDateTask(t.DateTask))
                                     .ToList();

            Application.Current.Dispatcher.Invoke(() =>
            {
                tasks.Clear();
                foreach (var task in pinnedTasks.Concat(nonPinnedTasks))
                {
                    tasks.Add(task);
                }
                OnPropertyChanged(nameof(Tasks));
                if (currentSelectedTask != null && tasks.Contains(currentSelectedTask))
                {
                    SelectedTask = currentSelectedTask;
                }
                else if (tasks.Count > 0)
                {
                    SelectedTask = tasks[0];
                }
            });
        }

        public ICommand MarkAsCheckedCommand { get; }
        private async void MarkAsChecked()
        {
            if (SelectedTask != null)
            {
                SelectedTask.IsChecked = !SelectedTask.IsChecked;
                System.Diagnostics.Debug.WriteLine($"MarkAsChecked: Title={SelectedTask.Title}, IsChecked={SelectedTask.IsChecked}");

                OnPropertyChanged(nameof(SelectedTask));
                await Task.Delay(10000);

                if (Tasks.Contains(SelectedTask))
                {
                    Tasks.Remove(SelectedTask);

                    if (Tasks.Count > 0)
                    {
                        SelectedTask = Tasks[0];
                    }
                    else
                    {
                        var newTask = new TaskModel { Title = "Новая задача", Content = "", DateTask = "Сегодня", Priority = "Низкий" };
                        newTask.PropertyChanged += Task_PropertyChanged;
                        Tasks.Add(newTask);
                        SelectedTask = newTask;
                    }

                    OnPropertyChanged(nameof(Tasks));
                    OnPropertyChanged(nameof(SelectedTask));
                    await SaveTasksAsync();
                }
            }
            else
            {
                MessageBox.Show("Выберите задачу перед пометкой выполненной.");
            }
        }

        public ICommand MarkAsImportantCommand { get; }
        private void MarkAsImportant()
        {
            if (SelectedTask != null)
            {
                SelectedTask.IsImportant = !SelectedTask.IsImportant;
                System.Diagnostics.Debug.WriteLine($"MarkAsImportant: Title={SelectedTask.Title}, IsImportant={SelectedTask.IsImportant}");
                SortTasks();
            }
            else
            {
                MessageBox.Show("Выберите задачу перед пометкой важности.");
            }
        }

        public ICommand AddTaskCommand { get; }
        private void AddTask()
        {
            var newTask = new TaskModel
            {
                Title = "Новая задача",
                Content = "",
                IsImportant = false,
                DateTask = "Сегодня",
                Priority = "Низкий"
            };
            newTask.PropertyChanged += Task_PropertyChanged;
            Tasks.Add(newTask);
            SelectedTask = newTask;
            InputTitle = "";
            SortTasks();
        }

        public ICommand DeleteTaskCommand { get; }
        private void DeleteTask()
        {
            if (SelectedTask != null && Tasks.Contains(SelectedTask))
            {
                if (Tasks.Count > 1)
                {
                    Tasks.Remove(SelectedTask);
                    SelectedTask = Tasks[0];
                    SortTasks();
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
            OpenFileDialog openFileDialog = new OpenFileDialog
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
        private async Task SaveTasksAsync()
        {
            if (isSyncing) return;
            isSyncing = true;

            try
            {
                string localFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TasksCollection.json");

                var settings = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };

                string jsonNotes = JsonConvert.SerializeObject(tasks, settings);
                await File.WriteAllTextAsync(localFilePath, jsonNotes);

                UserCredential credential;
                using (var stream = new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
                {
                    string credPath = "token.json";
                    credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.FromStream(stream).Secrets,
                        new[] { DriveService.Scope.Drive },
                        "user0",
                        CancellationToken.None,
                        new FileDataStore(credPath, true));
                }

                var service = new DriveService(new BaseClientService.Initializer
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "TasksTracker"
                });

                var listRequest = service.Files.List();
                listRequest.Q = "name = 'TasksCollection.json' and trashed = false";
                listRequest.Spaces = "drive";
                var fileList = await listRequest.ExecuteAsync();

                string? fileId = fileList.Files?.FirstOrDefault()?.Id;

                var fileMetadata = new Google.Apis.Drive.v3.Data.File
                {
                    Name = "TasksCollection.json"
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
                                    note.PropertyChanged += Task_PropertyChanged;
                                    tasks.Add(note);
                                }
                                SortTasks();
                            });
                        }
                    }
                }

                await LoadImagesAsync();
                await Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    UpdateDataWarningWindow updateDataWarningWindow = new UpdateDataWarningWindow();
                    Window ownerWindow = Window.GetWindow(Application.Current.MainWindow);
                    updateDataWarningWindow.Owner = ownerWindow;
                    updateDataWarningWindow.ShowDialog();
                });
            }
            catch (Exception e)
            {
                await Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    UpdateDataErrorWindow updateDataErrorWindow = new UpdateDataErrorWindow();
                    updateDataErrorWindow.Owner = Application.Current.MainWindow;
                    updateDataErrorWindow.ShowDialog();
                });
            }
            finally
            {
                isSyncing = false;
            }
        }

        private async Task LoadTasksAsync()
        {
            const string fileName = "TasksCollection.json";
            string localFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

            if (File.Exists(localFilePath))
            {
                try
                {
                    string json = await File.ReadAllTextAsync(localFilePath);
                    var loadedTasks = JsonConvert.DeserializeObject<ObservableCollection<TaskModel>>(json);

                    if (loadedTasks != null)
                    {
                        await Application.Current.Dispatcher.InvokeAsync(() =>
                        {
                            Tasks.Clear();
                            foreach (var task in loadedTasks)
                            {
                                if (string.IsNullOrEmpty(task.DateTask))
                                {
                                    task.DateTask = "Сегодня";
                                }
                                task.PropertyChanged += Task_PropertyChanged;
                                Tasks.Add(task);
                            }
                            SortTasks();
                        });
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки задач: {ex.Message}");
                }
            }
        }

        private async Task LoadImagesAsync()
        {
            string localFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TasksCollection.json");

            if (File.Exists(localFilePath))
            {
                try
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
                                    try
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
                                    catch (Exception ex)
                                    {
                                        System.Diagnostics.Debug.WriteLine($"Error loading image: {ex.Message}");
                                    }
                                }
                                note.PropertyChanged += Task_PropertyChanged;
                                tasks.Add(note);
                            }
                            SortTasks();
                        });
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки изображений: {ex.Message}");
                }
            }
        }

        public ICommand ChangeAccountCommand { get; }
        private async Task ChangeAccountForButton(CancellationToken cancellationToken = default)
        {
            try
            {
                string tokenFile = Path.Combine("token.json", $"Google.Apis.Auth.OAuth2.Responses.TokenResponse-{Environment.UserName}");

                if (File.Exists(tokenFile))
                {
                    File.Delete(tokenFile);
                }

                if (!File.Exists("client_secret.json"))
                {
                    MessageBox.Show("Файл client_secret.json не найден. Убедитесь, что он находится в корневой директории приложения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                using (var stream = new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
                {
                    var credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.FromStream(stream).Secrets,
                        new[] { DriveService.Scope.Drive },
                        Environment.UserName,
                        cancellationToken,
                        new FileDataStore("token.json", true));

                    if (credential != null)
                    {
                        var service = new DriveService(new BaseClientService.Initializer
                        {
                            HttpClientInitializer = credential,
                            ApplicationName = "TasksTracker"
                        });

                        var listRequest = service.Files.List();
                        listRequest.Q = "name = 'TasksCollection.json' and trashed = false";
                        listRequest.Spaces = "drive";
                        var fileList = await listRequest.ExecuteAsync(cancellationToken);

                        if (fileList.Files != null && fileList.Files.Count > 0)
                        {
                            var fileId = fileList.Files.First().Id;
                            var downloadRequest = service.Files.Get(fileId);
                            var memoryStream = new MemoryStream();
                            await downloadRequest.DownloadAsync(memoryStream);
                            memoryStream.Position = 0;

                            string localFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TasksCollection.json");
                            using (var fileStream = File.Create(localFilePath))
                            {
                                memoryStream.Seek(0, SeekOrigin.Begin);
                                await memoryStream.CopyToAsync(fileStream);
                            }

                            await LoadTasksAsync();
                            await LoadImagesAsync();
                        }

                        await Application.Current.Dispatcher.InvokeAsync(() =>
                        {
                            ChangeAccountWarningWindow changeAccountWarningWindow = new ChangeAccountWarningWindow();
                            Window ownerWindow = Window.GetWindow(Application.Current.MainWindow);
                            changeAccountWarningWindow.Owner = ownerWindow;
                            changeAccountWarningWindow.ShowDialog();
                        });
                    }
                    else
                    {
                        MessageBox.Show("Не удалось получить учетные данные. Попробуйте снова.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при смене аккаунта: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}