using System.Windows;
using System.Windows.Input;

namespace TasksTracker.View.Windows.WarningWindows
{
    public partial class ExitWarningWindow : Window
    {
        public ExitWarningWindow()
        {
            InitializeComponent();
        }

        private void YesButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void NoButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}