using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
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

        private void InputTaskTitleTextBox_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void CalenderButton_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                calendar.Visibility = calendar.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;

                if (DataContext is TasksViewModel vm && vm.SelectedTask != null)
                {
                    if (DateTime.TryParseExact(vm.SelectedTask.DateTask, "dd.MM.yyyy",
                        null, System.Globalization.DateTimeStyles.None, out var date))
                    {
                        calendar.SelectedDate = date;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось открыть календарь: {ex.Message}");
            }
        }
    }
}