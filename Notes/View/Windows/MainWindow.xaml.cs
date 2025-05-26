using System.Windows;
using TasksTracker.View.Windows.WarningWindows;
using TasksTracker.ViewModel;

namespace TasksTracker.View.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new TasksViewModel();
        }

        private void ExitBtn_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ExitWarningWindow exitWarningWindow = new();

            Window ownerWindow = GetWindow(this);
            exitWarningWindow.Owner = ownerWindow;

            exitWarningWindow.ShowDialog();
        }
    }
}