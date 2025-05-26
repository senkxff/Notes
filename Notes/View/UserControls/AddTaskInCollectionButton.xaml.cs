using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TasksTracker.View.UserControls
{
    /// <summary>
    /// Logic of interapt for AddTaskInCollectionButton.xaml
    /// </summary>
    public partial class AddTaskInCollectionButton : UserControl
    {
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
            "Command", 
            typeof(ICommand), 
            typeof(AddTaskInCollectionButton), 
            new PropertyMetadata(null));

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public AddTaskInCollectionButton()
        {
            InitializeComponent();
        }

        private void AddTaskInCollectionBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Command?.CanExecute(null) == true)
            {
                Command.Execute(null);
            }
        }
    }
}
