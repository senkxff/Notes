using System.Windows;
using System.Windows.Input;

namespace TasksTracker.View.Windows.WarningWindows
{
    public partial class UpdateDataErrorWindow : Window
    {
        public UpdateDataErrorWindow()
        {
            InitializeComponent();
        }

        private void AgreeButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }
    }
}
