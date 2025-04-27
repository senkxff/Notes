using System.Windows.Controls;
using System.Windows;

namespace Notes.View.UserControls
{
    /// <summary>
    /// Logic of interapt for InputTitleTextTextBox.xaml
    /// </summary>
    public partial class InputTitleTextTextBox : UserControl
    {
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text",
            typeof(string),
            typeof(InputTitleTextTextBox),
            new PropertyMetadata(string.Empty)
            );

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public InputTitleTextTextBox()
        {
            InitializeComponent();
        }

        private void InputTitle_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(InputTitle.Text))
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
