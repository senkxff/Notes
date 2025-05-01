    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;
    using Microsoft.Win32;
    using System.Windows.Media.Imaging;
    using Notes.Commands;
    using Notes.Model;
    using Notes.View.Windows.WarningWindows;

namespace Notes.ViewModel
{
    class NotesViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string inputTitle = "";
        public string InputTitle
        {
            get { return inputTitle; }
            set
            {
                inputTitle = value;
                OnPropertyChanged();
            }
        }

        private string _content = "";
        public string Content
        {
            get => _content;
            set
            {
                _content = value;
                OnPropertyChanged();

            }
        }

        private NoteModel selectedNote = new NoteModel();
        public NoteModel SelectedNote
        {
            get { return selectedNote; }
            set
            {
                selectedNote = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<NoteModel> notes = new ObservableCollection<NoteModel>()
        {
            new NoteModel { Title = "Новая заметка", Content = "" }
        };
        public ObservableCollection<NoteModel> Notes
        {
            get { return notes; }
            set
            {
                notes = value;
                OnPropertyChanged();
            }
        }

        public NotesViewModel()
        {
            AddImageCommand = new CommonCommand(AddImage);
            AddNoteCommand = new CommonCommand(AddNote);
            DeleteNoteCommand = new CommonCommand(DeleteNote);

            SelectedNote = notes[0];
        }

        public ICommand AddNoteCommand { get; }
        private void AddNote()
        {
            var newNote = new NoteModel { Title = "Новая заметка", Content = "" };

            Notes.Add(newNote);
            SelectedNote = newNote;
            InputTitle = string.Empty;
        }

        public ICommand DeleteNoteCommand { get; }
        private void DeleteNote()
        {
            if (SelectedNote != null && Notes.Contains(SelectedNote))
            {
                if (Notes.Count > 1)
                {
                    Notes.Remove(SelectedNote);
                    SelectedNote = Notes[Notes.Count - 1];
                }
                else
                {
                    DeleteAllNotesWarningWindow deleteAllNotesWarningWindow = new DeleteAllNotesWarningWindow();
                    Window ownerWindow = Window.GetWindow(Application.Current.MainWindow);
                    deleteAllNotesWarningWindow.Owner = ownerWindow;
                    deleteAllNotesWarningWindow.ShowDialog();
                }
            }
            else
            {
                DeleteAllNotesWarningWindow deleteAllNotesWarningWindow = new DeleteAllNotesWarningWindow();
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
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(openFileDialog.FileName);
                    bitmap.CacheOption = BitmapCacheOption.OnDemand;
                    bitmap.EndInit();

                    if (SelectedNote != null)
                    {
                        SelectedNote.Images.Add(bitmap);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки изображения: {ex.Message}");
                }
            }
        }
    }
}