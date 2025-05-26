using System.Windows.Controls;
using System.Windows;

namespace Notes.View.UserControls
{
    /// <summary>
    /// Logic of interapt for InputNoteTextTextBox.xaml
    /// </summary>
    public partial class InputNoteTextTextBox : UserControl
    {
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text",
            typeof(string),
            typeof(InputNoteTextTextBox),
            new PropertyMetadata(string.Empty)
            );

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public InputNoteTextTextBox()
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
