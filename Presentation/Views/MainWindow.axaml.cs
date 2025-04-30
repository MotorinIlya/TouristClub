using Avalonia.Controls;
using Avalonia.ReactiveUI;
using TouristClub.Presentation.ViewModels;

namespace TouristClub.Presentation.Views;

public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    public MainWindow()
    {
        InitializeComponent();
    }
}