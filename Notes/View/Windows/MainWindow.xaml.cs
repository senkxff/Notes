using System.Windows;
using System.Windows.Controls;
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
        var viewModel = new NotesViewModel();
        viewModel.SetRichTextBox(richTextBox);
        DataContext = viewModel;
        DataContext = new NotesViewModel();
        ((NotesViewModel)DataContext).SetRichTextBox(richTextBox);  
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