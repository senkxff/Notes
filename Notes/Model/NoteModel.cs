using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;

namespace Notes.Model
{
    class NoteModel
    {
        private string title = string.Empty;
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

        private ObservableCollection<BitmapImage> images = new ObservableCollection<BitmapImage>();
        public ObservableCollection<BitmapImage> Images
        {
            get { return images; }
            set { images = value; }
        }
    }   
}
