using System.Windows;
using System.Windows.Controls;

namespace Notes.View.UserControls
{
    /// <summary>
    /// Logic of interapt for TextBox.xaml
    /// </summary>
    public partial class TextBox : UserControl
    {
        public static readonly DependencyProperty PlaceholderProperty = DependencyProperty.Register(
            "Placeholder",
            typeof(string),
            typeof(TextBox),
            new PropertyMetadata(PlaceholderProperty)
            );

        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }

        public TextBox()
        {
            InitializeComponent();
        }

        private void MyTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(MyTextBox.Text))
            {
                tbPlaceholder.Visibility = Visibility.Visible;
            }
            else
            {
                tbPlaceholder.Visibility = Visibility.Collapsed;
            }
        }
    }
}
