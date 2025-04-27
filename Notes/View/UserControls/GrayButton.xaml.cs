using System.ComponentModel;
using System.Runtime.CompilerServices;
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
        public static readonly DependencyProperty PlaceholderProperty = DependencyProperty.Register(
            "Placeholder",
            typeof(string),
            typeof(GrayButton),
            new PropertyMetadata(string.Empty)
            );

        public string Placeholder
        {
            get {  return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }       

        public GrayButton()
        {
            InitializeComponent(); 
        }
    }
}
