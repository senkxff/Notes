using System.Windows;
using System.Windows.Input;

namespace Notes.View.Windows.WarningWindows
{
    /// <summary>
    /// Logic of interapt for DeleteAllNotesWarningWindow.xaml
    /// </summary>
    public partial class DeleteAllNotesWarningWindow : Window
    {
        public DeleteAllNotesWarningWindow()
        {
            InitializeComponent();
        }

        private void AgreeButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
