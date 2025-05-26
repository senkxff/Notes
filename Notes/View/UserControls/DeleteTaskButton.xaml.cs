using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TasksTracker.View.UserControls
{
    /// <summary>
    /// Logic of interapt for DeleteTaskButton.xaml
    /// </summary>
    public partial class DeleteTaskButton : UserControl
    {
        private readonly static DependencyProperty CommandProperty = DependencyProperty.Register(
            "Command",
            typeof(ICommand),
            typeof(DeleteTaskButton),
            new PropertyMetadata(null)
            );

        public ICommand Command
        { 
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value); 
        }

        public DeleteTaskButton()
        {
            InitializeComponent();
        }

        private void DeleteTaskBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Command?.CanExecute(null) == true)
            {
                Command.Execute(null);
            }
        }
    }
}
