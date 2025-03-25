using System.Text;

namespace Notes.Model
{
    class NoteModel
    {
        private string time;
        private string Time
        {
            get { return time; }
            set { time = value; }
        }

        private string title;
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        private StringBuilder content = new StringBuilder();
        public string Content
        {
            get { return content.ToString(); }
            set { content.Append(value); }
        }

        public NoteModel(string time, StringBuilder content, string title = "new note")
        {
            this.time = time;
            this.title = title;
            this.content = content;
        }
    }
}
