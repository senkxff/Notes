﻿using System.Windows;
using System.Windows.Controls;

namespace TasksTracker.View.UserControls
{
    public partial class TextBox : UserControl
    {
        public static readonly DependencyProperty PlaceholderProperty = DependencyProperty.Register(
            "Placeholder",
            typeof(string),
            typeof(TextBox),
            new PropertyMetadata(PlaceholderProperty)
            );

        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }

        public TextBox()
        {
            InitializeComponent();
        }

        private void InputedText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(InputedText.Text))
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
