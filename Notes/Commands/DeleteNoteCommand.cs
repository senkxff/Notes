using System.Windows.Input;

namespace Notes.Commands
{
    class DeleteNoteCommand : ICommand
    {
        private readonly Action _execute;

        public DeleteNoteCommand(Action execute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        }

        public event EventHandler? CanExecuteChanged
        {
            add { }
            remove { }
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter) => _execute();
    }
}
