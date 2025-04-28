using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Notes.Model;
using Notes.Commands;
using Notes.View.Windows.WarningWindows;
using System.Windows;

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

        private string content = "";
        public string Content
        {
            get { return content; }
            set
            {
                content = value;
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
            get { return  notes; }
            set
            {
                notes = value;
                OnPropertyChanged();
            }
        }

        public NotesViewModel()
        {
            AddNoteCommand = new AddNoteCommand(AddNote);
            DeleteNoteCommand = new DeleteNoteCommand(DeleteNote);

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
    }
}
