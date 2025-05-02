using System.Windows;
using Notes.View.Windows.WarningWindows;
using Notes.ViewModel;

namespace Notes.View.Windows;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new NotesViewModel();
    }

    private void ExitBtn_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        ExitWarningWindow exitWarningWindow = new();

        Window ownerWindow = GetWindow(this);
        exitWarningWindow.Owner = ownerWindow;

        exitWarningWindow.ShowDialog();
    }
}