using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Notes.Commands;
using Notes.Model;

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

        private ObservableCollection<NoteModel> notes = new ObservableCollection<NoteModel>();
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
            AddNoteCommand = new Notes.Commands.AddNoteCommand(AddNote);
        }

        public ICommand AddNoteCommand { get; }
        private void AddNote()
        {
            var newNote = new NoteModel { Title = "Новая заметка", Content = "" };

            Notes.Add(newNote);
            SelectedNote = newNote;
            InputTitle = string.Empty;
        }
    }
}
