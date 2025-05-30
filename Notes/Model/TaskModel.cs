using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;
using Newtonsoft.Json;

namespace TasksTracker.Model
{
    public class TaskModel : INotifyPropertyChanged
    {          
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            if (propertyName == null)
            {
                return;
            }
            else
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private bool isCompleted;
        public bool IsCompleted
        {
            get => isCompleted;
            set
            {
                isCompleted = value;
                OnPropertyChanged();
            }
        }

        private bool isChecked;
        public bool IsChecked
        {
            get => isChecked;
            set
            {
                isChecked = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(DisplayTitle));
            }
        }

        private string dateTask = "Сегодня";
        public string DateTask
        {
            get => dateTask;
            set
            {
                dateTask = value;
                OnPropertyChanged();
            }
        }

        private bool isImportant;
        public bool IsImportant
        {
            get => isImportant;
            set
            {
                isImportant = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(DisplayTitle));
            }
        }

        private string title = "Новая задача";
        public string Title
        {
            get => title;
            set
            {
                title = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(DisplayTitle));
            }
        }

        public string DisplayTitle => (isChecked ? "✔ " : "") + (isImportant ? "★ " : "") + title;

        private string content = "";
        public string Content
        {
            get => content;
            set
            {
                content = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> imagesBase64 = new();
        public ObservableCollection<string> ImagesBase64
        {
            get => imagesBase64;
            set
            {
                imagesBase64 = value;
                OnPropertyChanged();
            }
        }

        private string priority = "Низкий";
        public string Priority
        {
            get => priority;
            set
            {
                priority = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(PriorityColor));
                OnPropertyChanged(nameof(DisplayTitle));
            }
        }

        public string PriorityColor
        {
            get
            {
                switch (priority)
                {
                    case "Низкий":
                        return "#32CD32";   // Зелёный

                    case "Средний":
                        return "Gold";      // Жёлтый

                    case "Высокий":
                        return "#FF6D2D";   // Оранжевый

                    case "Критический":
                        return "Red";       // Красный

                    default:
                        return "#FFFFFFFF"; // Белый
                }
            }
        }

        [JsonIgnore]
        public ObservableCollection<BitmapImage> Images { get; set; } = new ObservableCollection<BitmapImage>();
    }
}