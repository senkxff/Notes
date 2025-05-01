using System.Windows;
using System.Windows.Controls;

namespace Notes.View.UserControls
{
    /// <summary>
    /// Logic of interapt for PasswordBox.xaml
    /// </summary>
    public partial class PasswordBox : UserControl
    {
        public PasswordBox()
        {
            InitializeComponent();
        }

        private void InputedPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(InputedPassword.Password))
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
