using System.Windows;
using System.Windows.Controls;

namespace Notes.View.UserControls
{
    /// <summary>
    /// Logic of interapt for ExitButton.xaml
    /// </summary>
    public partial class ExitButton : UserControl
    {
        public ExitButton()
        {
            InitializeComponent();
        }

        private void _ExitButton_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
