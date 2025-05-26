using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using Newtonsoft.Json;

namespace TasksTracker.Model
{
    class TaskModel
    {
        private DateOnly date;
        public DateOnly Date
        {
            get { return date; }
            set { date = value; }
        }

        private string title = "Новая задача";
        public string Title
        {
            get { return title; }
            set {  title = value; }
        }

        private string content = string.Empty;
        public string Content
        {
            get { return content; }
            set { content = value; }
        }

        private ObservableCollection<string> imagesBase64 = new();
        public ObservableCollection<string> ImagesBase64
        {
            get { return imagesBase64; }
            set { imagesBase64 = value; }
        }

        [JsonIgnore]
        public ObservableCollection<BitmapImage> Images { get; set; } = new ObservableCollection<BitmapImage>();
    }   
}
