using System.Windows;
using System.Windows.Controls;

namespace TasksTracker.View.UserControls
{
    /// <summary>
    /// Логика взаимодействия для FindTextBox.xaml
    /// </summary>
    public partial class FindTextBox : UserControl
    {
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text",
            typeof(string),
            typeof(FindTextBox),
            new FrameworkPropertyMetadata(
                string.Empty,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                new PropertyChangedCallback(OnTextChanged)));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        public FindTextBox()
        {
            InitializeComponent();
        }

        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as FindTextBox;
            if (control != null)
            {
                control.UpdatePlaceholderVisibility();
            }
        }

        public void UpdatePlaceholderVisibility()
        {
            Placeholder.Visibility = string.IsNullOrEmpty(Text) ? Visibility.Visible : Visibility.Collapsed;
        }

        private void InputedSearchString_TextChanged(object sender, TextChangedEventArgs e)
        {
            Text = InputedSearchString.Text;
        }
    }
}
