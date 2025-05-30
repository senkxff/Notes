using System.Windows;
using System.Windows.Input;

namespace TasksTracker.View.Windows.WarningWindows
{
    /// <summary>
    /// Логика взаимодействия для ChangeAccountWarningWindow.xaml
    /// </summary>
    public partial class ChangeAccountWarningWindow : Window
    {
        public ChangeAccountWarningWindow()
        {
            InitializeComponent();
        }

        private void AgreeButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }
    }
}
