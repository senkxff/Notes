using System.Windows;
using System.Windows.Input;

namespace TasksTracker.View.Windows.WarningWindows
{
    public partial class DeleteAllTasksWarningWindow : Window
    {
        public DeleteAllTasksWarningWindow()
        {
            InitializeComponent();
        }

        private void AgreeButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }
    }
}
