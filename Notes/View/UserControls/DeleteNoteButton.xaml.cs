using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Notes.View.UserControls
{
    /// <summary>
    /// Logic of interapt for DeleteNoteButton.xaml
    /// </summary>
    public partial class DeleteNoteButton : UserControl
    {
        private readonly static DependencyProperty CommandProperty = DependencyProperty.Register(
            "Command",
            typeof(ICommand),
            typeof(DeleteNoteButton),
            new PropertyMetadata(null)
            );

        public ICommand Command
        { 
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value); 
        }

        public DeleteNoteButton()
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
