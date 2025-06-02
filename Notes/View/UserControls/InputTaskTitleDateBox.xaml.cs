using System.Windows;
using System.Windows.Controls;

namespace TasksTracker.View.UserControls
{
    public partial class InputTaskTitleTextBox : UserControl
    {
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text",
            typeof(string),
            typeof(InputTaskTitleTextBox),
            new FrameworkPropertyMetadata(
                string.Empty,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                new PropertyChangedCallback(OnTextChanged)));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public InputTaskTitleTextBox()
        {
            InitializeComponent();
        }

        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as InputTaskTitleTextBox;
            if (control != null)
            {
                control.UpdatePlaceholderVisibility();
            }
        }

        private void UpdatePlaceholderVisibility()
        {
            Placeholder.Visibility = string.IsNullOrEmpty(Text) ? Visibility.Visible : Visibility.Collapsed;
        }

        private void InputedTitle_TextChanged(object sender, TextChangedEventArgs e)
        {
            Text = InputedTitle.Text;
        }
    }
}