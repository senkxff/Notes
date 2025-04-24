using System.Windows.Controls;
using System.Windows;

namespace Notes.View.UserControls
{
    /// <summary>
    /// Logic of interapt for InputTitleTextTextBox.xaml
    /// </summary>
    public partial class InputTitleTextTextBox : UserControl
    {
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
