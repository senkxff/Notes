using System.Windows.Input;

namespace Notes.Commands
{
    public class AddNoteCommand : ICommand
    {
        private readonly Action _execute;

        public AddNoteCommand(Action execute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        }

        public event EventHandler? CanExecuteChanged
        {
            add { }
            remove { }
        }

        public bool CanExecute(object? parameter) => true;
        public void Execute(object? parameter) => _execute();
    }
}
