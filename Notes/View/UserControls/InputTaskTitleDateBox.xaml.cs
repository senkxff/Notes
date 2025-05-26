using System.Windows.Controls;
using System.Windows;

namespace TasksTracker.View.UserControls
{
    /// <summary>
    /// Logic of interapt for InputTaskTitleTextBox.xaml
    /// </summary>
    public partial class InputTaskTitleTextBox : UserControl
    {
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text",
            typeof(string),
            typeof(InputTaskTitleTextBox),
            new PropertyMetadata(string.Empty)
            );

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public InputTaskTitleTextBox()
        {
            InitializeComponent();
        }

        private void InputedTitle_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(InputedTitle.Text))
            {
                Placeholder.Visibility = Visibility.Visible;
            }
            else
            {
                Placeholder.Visibility = Visibility.Collapsed;
            }
        }
    }
}
