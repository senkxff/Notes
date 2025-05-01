using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Notes.View.UserControls
{
    /// <summary>
    /// Logic of interapt for AddNoteInCollectionButton.xaml
    /// </summary>
    public partial class AddNoteInCollectionButton : UserControl
    {
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
            "Command", 
            typeof(ICommand), 
            typeof(AddNoteInCollectionButton), 
            new PropertyMetadata(null));

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public AddNoteInCollectionButton()
        {
            InitializeComponent();
        }

        private void AddNoteInCollectionBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Command?.CanExecute(null) == true)
            {
                Command.Execute(null);
            }
        }
    }
}
