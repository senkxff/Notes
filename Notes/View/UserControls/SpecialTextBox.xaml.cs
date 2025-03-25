using System.Windows.Controls;
using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Notes.View.UserControls
{
    public partial class SpecialTextBox : UserControl, INotifyPropertyChanged
    {
        public SpecialTextBox()
        {
            InitializeComponent();
            DataContext = this;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private string placeholder = "";
        public string Placeholder
        {
            get { return placeholder; }
            set
            {
                if (placeholder != value)
                {
                    placeholder = value;
                    OnPropertyChanged();
                }
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void RoundedTextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrEmpty(RoundedTextBox.Text))
        {
            tbPlaceholder.Visibility = Visibility.Visible;
        }
        else
        {
            tbPlaceholder.Visibility = Visibility.Collapsed;
        }
    }
    }
}
