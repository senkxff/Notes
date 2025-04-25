using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Notes.View.UserControls
{
    /// <summary>
    /// Логика взаимодействия для GrayButton.xaml
    /// </summary>
    public partial class GrayButton : UserControl
    {
        public GrayButton()
        {
            InitializeComponent();
        }

        private string placeholder;
        public string Placeholder
        {
            get { return placeholder; }
            set
            {
                placeholder = value;

            }
        }
    }
}
