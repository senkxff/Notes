﻿using System.Windows;
using System.Windows.Controls;

namespace TasksTracker.View.UserControls
{
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
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }       

        public GrayButton()
        {
            InitializeComponent(); 
        }
    }
}
