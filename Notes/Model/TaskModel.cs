using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;
using Newtonsoft.Json;

namespace TasksTracker.Model
{
    public class TaskModel : INotifyPropertyChanged
    {
        private bool isCompleted;
        private bool isChecked;
        private string dateTask = "Сегодня";
        private bool isImportant;
        private string title = "Новая задача";
        private string content = "";
        private string priority = "Низкий";
        private ObservableCollection<string> imagesBase64 = new();

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool IsCompleted
        {
            get => isCompleted;
            set
            {
                isCompleted = value;
                OnPropertyChanged();
            }
        }

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

        public string DateTask
        {
            get => dateTask;
            set
            {
                dateTask = value;
                OnPropertyChanged();
            }
        }

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

        public string Content
        {
            get => content;
            set
            {
                content = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> ImagesBase64
        {
            get => imagesBase64;
            set
            {
                imagesBase64 = value;
                OnPropertyChanged();
            }
        }

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
            get => priority switch
            {
                "Низкий" => "#32CD32",      // Зелёный
                "Средний" => "Gold",   // Жёлтый
                "Высокий" => "#FF6D2D",     // Оранжевый
                "Критический" => "Red", // Красный
                _ => "#FFFFFFFF"           // Белый по умолчанию
            };
        }

        [JsonIgnore]
        public ObservableCollection<BitmapImage> Images { get; set; } = new ObservableCollection<BitmapImage>();
    }
}