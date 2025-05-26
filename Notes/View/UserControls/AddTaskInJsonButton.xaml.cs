using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TasksTracker.View.UserControls
{
    /// <summary>
    /// Logic of interapt for AddTaskInJsonButton.xaml
    /// </summary>
    public partial class AddTaskInJsonButton : UserControl
    {
        private static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
            "Command",
            typeof(ICommand),
            typeof(AddTaskInJsonButton),
            new PropertyMetadata(null)
            );

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty); 
            set => SetValue(CommandProperty, value); 
        }

        public AddTaskInJsonButton()
        {
            InitializeComponent();
        }

        private void AddTaskInJsonBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Command?.CanExecute(null) == true)
            {
                Command?.Execute(null);
            }
        }
    }
}
