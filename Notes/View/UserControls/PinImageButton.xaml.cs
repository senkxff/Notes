using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TasksTracker.View.UserControls
{
    /// <summary>
    /// Logic of interapt for PinFileButton.xaml
    /// </summary>
    public partial class PinImageButton : UserControl
    {
        private static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
            "Command",
            typeof(ICommand),
            typeof(PinImageButton), 
            new PropertyMetadata(null)
            );

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public PinImageButton()
        {
            InitializeComponent();
        }

        private void PinImageBtn_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Command?.CanExecute(null) == true)
            {
                Command.Execute(null);
            }
        }
    }
}
