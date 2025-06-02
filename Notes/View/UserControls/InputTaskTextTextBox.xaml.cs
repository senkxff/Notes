using System.Windows.Controls;
using System.Windows;

namespace TasksTracker.View.UserControls
{
    public partial class InputTaskTextTextBox : UserControl
    {
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text",
            typeof(string),
            typeof(InputTaskTextTextBox),
            new PropertyMetadata(string.Empty)
            );

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public InputTaskTextTextBox()
        {
            InitializeComponent();
        }

        private void InputedText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(InputedText.Text))
            {
                Placeholder.Visibility = Visibility.Visible;
            }
            else
            {
                Placeholder.Visibility = Visibility.Collapsed;
            }
        }
    }
}
