using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TasksTracker.View.UserControls
{
    public partial class CreateImportantButton : UserControl
    {
        private static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
            "Command",
            typeof(ICommand),
            typeof(CreateImportantButton),
            new PropertyMetadata(null)
            );

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public CreateImportantButton()
        {
            InitializeComponent();
        }
    }
}
