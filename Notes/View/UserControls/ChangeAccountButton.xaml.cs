using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TasksTracker.View.UserControls
{
    public partial class ChangeAccountButton : UserControl
    {
        private static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
            "Command",
            typeof(ICommand),
            typeof(ChangeAccountButton),
            new PropertyMetadata(null)
            );

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public ChangeAccountButton()
        {
            InitializeComponent();
        }
    }
}
