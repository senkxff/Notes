﻿using System.Windows;
using System.Windows.Controls;

namespace TasksTracker.View.UserControls
{
    /// <summary>
    /// Logic of interapt for RedButton.xaml
    /// </summary>
    public partial class RedButton : UserControl
    {
        public static readonly DependencyProperty PlaceholderProperty =
             DependencyProperty.Register (
                "Placeholder",
                typeof(string),
                typeof(RedButton),
                new PropertyMetadata(string.Empty)
                );

        public string Placeholder
        { 
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }

        public RedButton()
        {
            InitializeComponent();
        }
    }
}
