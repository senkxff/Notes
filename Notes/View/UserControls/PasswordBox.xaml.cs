using System.Windows;
using System.Windows.Controls;

namespace Notes.View.UserControls
{
    /// <summary>
    /// Logic of interapt for PasswordBox.xaml
    /// </summary>
    public partial class PasswordBox : UserControl
    {
        public static readonly DependencyProperty PlaceholderProperty = DependencyProperty.Register(
            "Placeholder",
            typeof(string),
            typeof(PasswordBox),
            new PropertyMetadata(PlaceholderProperty)
            );

        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }

        public PasswordBox()
        {
            InitializeComponent();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(MyPasswordBox.Password))
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
