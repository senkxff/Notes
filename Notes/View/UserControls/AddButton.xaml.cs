using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Notes.View.UserControls
{
    /// <summary>
    /// Logic of interapt for AddButton.xaml
    /// </summary>
    public partial class AddButton : UserControl
    {
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
            "Command", 
            typeof(ICommand), 
            typeof(AddButton), 
            new PropertyMetadata(null));



        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public AddButton()
        {
            InitializeComponent();
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Command?.CanExecute(null) == true)
            {
                Command.Execute(null);
            }
        }
    }
}
