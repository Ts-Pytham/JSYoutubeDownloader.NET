using System.Windows;

namespace JSYoutubeDownloader.NET.Views;

/// <summary>
/// Lógica de interacción para DownloadView.xaml
/// </summary>
public partial class DownloadView : Window
{
    public DownloadView()
    {
        InitializeComponent();
    }

    public DownloadView(ViewModels.DownloadViewModel vm)
    {
        InitializeComponent();
        DataContext = vm;
        
    }
}
