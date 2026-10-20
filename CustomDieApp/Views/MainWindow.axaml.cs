using Avalonia.Controls;
using Avalonia.Interactivity;
using CustomDieApp.ViewModels;

namespace CustomDieApp.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private async void SaveDie_Click(object? sender, RoutedEventArgs e)
    {
        if (DataContext is MainViewModel viewModel)
        {
            await viewModel.SaveDie(this.StorageProvider);
        }
    }

     private async void LoadDie_Click(object? sender, RoutedEventArgs e)
    {
        if (DataContext is MainViewModel viewModel)
        {
            await viewModel.LoadDie(this.StorageProvider);
        }
    }
}