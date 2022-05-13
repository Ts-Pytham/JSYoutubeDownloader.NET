﻿using JSYoutubeDownloader.NET.Commands;
using JSYoutubeDownloader.NET.Models;
using JSYoutubeDownloader.NET.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Ookii.Dialogs.Wpf;
using System.Windows;
using IO = System.IO;
using System;
using YoutubeExplode.Videos.Streams;
using JSYoutubeDownloader.NET.Utilities;
using System.Threading;

namespace JSYoutubeDownloader.NET.ViewModels;

public class DownloadViewModel : ViewModelBase
{
    #region Properties

    private readonly VideoInfo _videoInfo;

    private readonly IDownloadVideoService _videoService;

    private readonly IProgress<double> _progress;

    private readonly StreamManifest _stream;

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

    private bool _isEnable;

    public bool IsEnable
    {
        get { return _isEnable; }
        set { _isEnable = value; OnPropertyChanged(); }
    }

    private bool _downloading;

    private CancellationTokenSource _token;
    #endregion


    private DownloadViewModel(VideoInfo video, List<string> Containers, List<string> Qualities, StreamManifest stream)
    {
        _videoService = new DownloadService();
        _progress = new Progress<double>(p => Duration = 100 * p);
        _videoInfo = video;
        _token = new CancellationTokenSource();
        _containers = Containers;
        _selectedItemContainer = Containers[0];
        _qualities = Qualities;
        _selectedItemQuality = Qualities[0];
        _path = "";
        _isEnable = true;
        _stream = stream;
        _downloading = false;
    }

    public static async Task<DownloadViewModel> Load(VideoInfo video)
    {
        IVideoInfoService service = new VideoInfoService();
        var _containers = await service.GetContainers(video.Id);

        var _qualities = await service.GetQualities(video.Id);
        return new DownloadViewModel(video, _containers[0], _qualities[0], _containers[1]);
    }

    #region Commands
    private RelayCommand? _downloadCommand;
    private RelayCommand? _searchFolderCommand;
    private RelayCommand? _cancelCommand;
    public ICommand DownloadCommand => _downloadCommand ??= new RelayCommand(DownloadCommandExecute);
    public ICommand SearchFolderCommand => _searchFolderCommand ??= new RelayCommand(SearchFolderCommandExecute);

    public ICommand CancelCommand => _cancelCommand ??= new RelayCommand(CancelCommandExecute);

    private void CancelCommandExecute(object obj)
    {
        if (_downloading)
        {
            _token.Cancel();
        }
        else
        {
            MessageBox.Show("No se está descargando ningún archivo!", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }

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
        if (!_downloading)
        {
            if (string.IsNullOrWhiteSpace(Path))
            {
                MessageBox.Show("La ruta está vacía!", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!IO.Directory.Exists(Path))
            {
                MessageBox.Show("La ruta no existe, busque una nueva!", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string _path = $"{Path}/{Utils.ChangeFormat(_videoInfo.Title)}.{SelectedItemContainer}";
            
            try
            {
                _downloading = true;

                if (SelectedItemContainer == "mp3") // Downloading Only Audio
                {
                    await _videoService.DownloadAudio(_videoInfo, _path, _progress, _token.Token);
                    MessageBox.Show("Se descargó correctamente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                    Duration = 0;
                }
                else
                {
                    await _videoService.DownloadVideo(_stream, _videoInfo, _path, SelectedItemQuality, _progress, _token.Token);
                    MessageBox.Show("Se descargó correctamente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                    Duration = 0;
                }
            }
            catch (TaskCanceledException)
            {
                MessageBox.Show("Se canceló la descarga", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                IO.File.Delete(_path);
                _token = new();
            }
            finally
            {
                _downloading = false;
            }
        }
        else
        {
            MessageBox.Show("Se está descargando el archivo!", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }


    #endregion

}
