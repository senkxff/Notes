using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;
using Newtonsoft.Json;

namespace TasksTracker.Model
{
    public class TaskModel : INotifyPropertyChanged
    {
        private string date = DateTime.Now.ToString("dd.MM.yyyy");
        public string DateTask
        {
            get => date;
            set
            {
                date = value;
                OnPropertyChanged();
            }
        }

        private bool isImportant;
        public bool IsImportant
        {
            get => isImportant;
            set
            {
                if (isImportant != value)
                {
                    isImportant = value;
                    OnPropertyChanged();
                }
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
                IsImportant = value.StartsWith("★");
            }
        }

        private string content = string.Empty;
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

        [JsonIgnore]
        public ObservableCollection<BitmapImage> Images { get; set; } = new ObservableCollection<BitmapImage>();

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}