using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;
using TasksTracker.View.Windows.WarningWindows;
using TasksTracker.ViewModel;

namespace TasksTracker.View.Windows
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new TasksViewModel();

            calendar.Loaded += Calendar_Loaded;
            calendar.SelectedDatesChanged += Calendar_SelectedDatesChanged;

            if (DataContext is TasksViewModel vm)
            {
                vm.Tasks.CollectionChanged += (s, e) => UpdateCalendarMarks();
            }
        }

        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (calendar.SelectedDate.HasValue && DataContext is TasksViewModel vm && vm.SelectedTask != null)
            {
                vm.SelectedTask.DateTask = calendar.SelectedDate.Value.ToString("dd.MM.yyyy");
                calendar.Visibility = Visibility.Collapsed;
            }
        }

        private void Calendar_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateCalendarMarks();
        }

        private void UpdateCalendarMarks()
        {
            if (!(DataContext is TasksViewModel vm)) return;

            var style = new Style(typeof(CalendarDayButton));

            var datePriorities = new Dictionary<DateTime, string>();

            foreach (var task in vm.Tasks)
            {
                if (DateTime.TryParseExact(task.DateTask, "dd.MM.yyyy", null,
                    System.Globalization.DateTimeStyles.None, out DateTime date))
                {
                    if (datePriorities.ContainsKey(date))
                    {
                        var currentPriority = datePriorities[date];
                        if (GetPriorityLevel(task.Priority) > GetPriorityLevel(currentPriority))
                        {
                            datePriorities[date] = task.Priority;
                        }
                    }
                    else
                    {
                        datePriorities.Add(date, task.Priority);
                    }
                }
            }

            foreach (var datePriority in datePriorities)
            {
                var trigger = new DataTrigger
                {
                    Binding = new Binding("Date"),
                    Value = datePriority.Key
                };

                var priorityColor = GetPriorityColor(datePriority.Value);
                trigger.Setters.Add(new Setter(Control.BackgroundProperty, priorityColor));
                trigger.Setters.Add(new Setter(Control.BorderBrushProperty, priorityColor));

                style.Triggers.Add(trigger);
            }

            calendar.CalendarDayButtonStyle = style;
        }

        private int GetPriorityLevel(string priority)
        {
            switch (priority)
            {
                case "Критический": return 4;
                case "Высокий": return 3;
                case "Средний": return 2;
                case "Низкий": return 1;
                default: return 0;
            }
        }

        private Brush GetPriorityColor(string priority)
        {
            switch (priority)
            {
                case "Критический": return Brushes.Red;
                case "Высокий": return Brushes.Orange;
                case "Средний": return Brushes.Yellow;
                case "Низкий": return Brushes.LightGreen;
                default: return Brushes.White;
            }
        }

        private void ExitBtn_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ExitWarningWindow exitWarningWindow = new();
            exitWarningWindow.Owner = this;
            exitWarningWindow.ShowDialog();
        }

        private void CalenderButton_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                calendar.Visibility = calendar.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;

                if (DataContext is TasksViewModel vm && vm.SelectedTask != null)
                {
                    if (DateTime.TryParseExact(vm.SelectedTask.DateTask, "dd.MM.yyyy", null,
                        System.Globalization.DateTimeStyles.None, out var date))
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