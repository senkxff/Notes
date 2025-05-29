using System.Windows;
using System.Windows.Input;

namespace TasksTracker.View.Windows.WarningWindows
{
    /// <summary>
    /// Логика взаимодействия для UpdateDataWarningWindow.xaml
    /// </summary>
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
