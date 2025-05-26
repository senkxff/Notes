using System.Windows.Controls;
using System.Windows;

namespace Notes.View.UserControls
{
    /// <summary>
    /// Logic of interapt for InputNoteTitleTextBox.xaml
    /// </summary>
    public partial class InputNoteTitleTextBox : UserControl
    {
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text",
            typeof(string),
            typeof(InputNoteTitleTextBox),
            new PropertyMetadata(string.Empty)
            );

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public InputNoteTitleTextBox()
        {
            InitializeComponent();
        }

        private void InputedTitle_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(InputedTitle.Text))
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
