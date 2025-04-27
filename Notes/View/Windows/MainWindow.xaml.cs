using System.Windows;
using Notes.View.Windows.WarningWindows;

namespace Notes.View.Windows;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void _ExitButton_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        ExitWarningWindow exitWarningWindow = new ExitWarningWindow();

        Window ownerWindow = Window.GetWindow(this);
        exitWarningWindow.Owner = ownerWindow;

        exitWarningWindow.ShowDialog();
    }

    private void _AccountButton_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        RegistrationWindow registrationWindow = new RegistrationWindow();

        Window ownerWindow = Window.GetWindow(this);
        registrationWindow.Owner = ownerWindow;

        registrationWindow.ShowDialog();
    }
}