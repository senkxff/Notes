using System.Windows;
using System.Windows.Input;

namespace TasksTracker.View.Windows.WarningWindows
{
    public partial class UpdateDataWarningWindow : Window
    {
        public UpdateDataWarningWindow()
        {
            InitializeComponent();
        }

        private void AgreeButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }
    }
}
