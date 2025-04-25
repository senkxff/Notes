using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Notes.View.Windows;
using Notes.View.Windows.WarningWindows;

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
            ExitWarningWindow exitWarningWindow = new ExitWarningWindow();

            Window ownerWindow = Window.GetWindow(this);
            exitWarningWindow.Owner = ownerWindow;

            exitWarningWindow.ShowDialog();
        }
    }
}
