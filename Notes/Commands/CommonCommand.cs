using System.Windows.Input;

namespace TasksTracker.Commands
{
    class CommonCommand : ICommand
    {
        private readonly Action _execute;

        public CommonCommand(Action execute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        }

        public event EventHandler? CanExecuteChanged
        {
            add { }
            remove { }
        }

        public bool CanExecute(object? parametr) => true;
        public void Execute(object? parametr) => _execute();
    }
}
