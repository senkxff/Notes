using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace Notes.View.UserControls
{
    public partial class SomeButton : UserControl, INotifyPropertyChanged
    {
        public SomeButton()
        {
            InitializeComponent();
            DataContext = this;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private string text = "Some Text";
        public string Text
        {
            get { return text; }
            set 
            {
                if (text != value)
                {
                    text = value;
                    OnPropertyChanged();
                }
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
