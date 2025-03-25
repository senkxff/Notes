using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows;

namespace Notes.View.UserControls
{
    public partial class PasswordBox : UserControl, INotifyPropertyChanged
    {
        public PasswordBox()
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

        private void RoundedPasswordBox_PasswordChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(RoundedPasswordBox.Password))
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
