using JSYoutubeDownloader.NET.Commands;
using JSYoutubeDownloader.NET.Models;
using JSYoutubeDownloader.NET.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Ookii.Dialogs.Wpf;
using System.Windows;
using IO = System.IO;
using System;

namespace JSYoutubeDownloader.NET.ViewModels;

public class DownloadViewModel : ViewModelBase
{
    #region Properties

    private readonly VideoInfo _videoInfo;

    private readonly IDownloadVideoService _videoService;

    private readonly IProgress<double> _progress;

    private List<string> _containers;

    public List<string> Containers
    {
        get { return _containers; }
        set { _containers = value; OnPropertyChanged(); }
    }

    private string _selectedItemContainer;

    public string SelectedItemContainer
    {
        get { return _selectedItemContainer; }
        set 
        {  
            _selectedItemContainer = value; 

            if(_selectedItemContainer == "mp3")
                IsEnable = false;
            else
                IsEnable = true;

            OnPropertyChanged(); 
        }
    }

    private List<string> _qualities;

    public List<string> Qualities
    {
        get { return _qualities; }
        set { _qualities = value; OnPropertyChanged(); }
    }

    private string _selectedItemQuality;

    public string SelectedItemQuality
    {
        get { return _selectedItemQuality; }
        set { _selectedItemQuality = value; OnPropertyChanged(); }
    }


    private string _path;

    public string Path
    {
        get { return _path; }
        set { _path = value; OnPropertyChanged(); }
    }
    
    private double _duration;

    public double Duration 
    { 
        get { return _duration; } 
        set { _duration = value; OnPropertyChanged(); } 
    }

    private bool _isIndeterminate;

    public bool IsIndeterminate
    {
        get { return _isIndeterminate; }
        set { _isIndeterminate = value; OnPropertyChanged(); }
    }

    private bool _isEnable;

    public bool IsEnable
    {
        get { return _isEnable; }
        set { _isEnable = value; OnPropertyChanged(); }
    }
    #endregion


    private DownloadViewModel(VideoInfo video, List<string> Containers, List<string> Qualities)
    {
        _videoService = new DownloadService();
        _progress = new Progress<double>(p => Duration = 100 * p);
        _videoInfo = video;
        _containers = Containers;
        _selectedItemContainer = Containers[0];
        _qualities = Qualities;
        _selectedItemQuality = Qualities[0];
        _path = "";
        _isEnable = true;
    }

    public static async Task<DownloadViewModel> Load(VideoInfo video)
    {
        IVideoInfoService service = new VideoInfoService();
        var _containers = await service.GetContainers(video.Id);
        List<string> _qualities = await service.GetQualities(video.Id);
        return new DownloadViewModel(video, _containers, _qualities);
    }

    #region Commands
    private RelayCommand? _downloadCommand;
    private RelayCommand? _SearchFolderCommand;

    public ICommand DownloadCommand => _downloadCommand ??= new RelayCommand(DownloadCommandExecute);
    public ICommand SearchFolderCommand => _SearchFolderCommand ??= new RelayCommand(SearchFolderCommandExecute);

    private void SearchFolderCommandExecute(object obj)
    {
        VistaFolderBrowserDialog dialog = new();

        if (dialog.ShowDialog() == true)
        {
            Path = dialog.SelectedPath;
        }
    }

    private async void DownloadCommandExecute(object obj)
    {
        if (string.IsNullOrWhiteSpace(Path))
        {
            MessageBox.Show("La ruta está vacía!");
            return;
        }
        if (!IO.Directory.Exists(Path))
        {
            MessageBox.Show("La ruta no existe, busque una nueva!");
            return;
        }

        if(SelectedItemContainer == "mp3") // Downloading Only Audio
        {
            string _path = $"{Path}/{_videoInfo.Title}.mp3";
            await _videoService.DownloadAudio(_videoInfo, _path, _progress);
            MessageBox.Show("Se descargó correctamente");
            Duration = 0;
        }
        
    }


    #endregion

}
