using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Notes.View.UserControls
{
    /// <summary>
    /// Logic of interapt for AddNoteInJsonButton.xaml
    /// </summary>
    public partial class AddNoteInJsonButton : UserControl
    {
        private static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
            "Command",
            typeof(ICommand),
            typeof(AddNoteInJsonButton),
            new PropertyMetadata(null)
            );

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty); 
            set => SetValue(CommandProperty, value); 
        }

        public AddNoteInJsonButton()
        {
            InitializeComponent();
        }

        private void AddNoteInJsonBtn_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (Command?.CanExecute(null) == true)
            {
                Command?.Execute(null);
            }
        }
    }
}
