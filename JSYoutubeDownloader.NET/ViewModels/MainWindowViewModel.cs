using JSYoutubeDownloader.NET.Commands;
using JSYoutubeDownloader.NET.Models;
using JSYoutubeDownloader.NET.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace JSYoutubeDownloader.NET.ViewModels;

internal class MainWindowViewModel : ViewModelBase
{

    #region Properties
    private VideoInfo _video;

    public VideoInfo Video
    {
        get 
        {
            return _video; 
        }
        set
        {
            if (_video == value)
                return;
            _video = value;
            OnPropertyChanged();
        }
    }

    private ObservableCollection<VideoInfo> _videos;
    public ObservableCollection<VideoInfo> Videos 
    {
        get { return _videos; } 
        set { _videos = value; OnPropertyChanged(); }
    }

    private bool _isIndeterminate;

    public bool IsIndeterminate
    {
        get 
        { 
            return _isIndeterminate; 
        }
        set 
        {
            _isIndeterminate = value; 
            OnPropertyChanged(); 
        }
    }

    private string _isDisable;

    public string IsDisable
    {
        get => _isDisable;
        set { _isDisable = value; OnPropertyChanged(); }
    }

    #endregion

    #region Commands
    private RelayCommand? videoInfoCommands;
    public ICommand VideoInfoCommands => videoInfoCommands ??= new RelayCommand(VideoInfoCommandExecute);

    #endregion

    public MainWindowViewModel()
    {
        _video = new VideoInfo();
        _videos = new ObservableCollection<VideoInfo>();
       _isIndeterminate = false;
        _isDisable = "Hidden";
    }

    
    private async void VideoInfoCommandExecute(object commandParameter)
    {
        try
        {
            IVideoInfoService service = new VideoInfoService();
            IsIndeterminate = true;
            Video = await service.GetVideoInfo(Video.URL);
            IsDisable = "Visible";
        }
        catch (ArgumentException)
        {
            try
            {
                IVideoInfoService service = new VideoInfoService();
                IsIndeterminate = true;

                Videos = new ObservableCollection<VideoInfo>(await service.GetVideosInfo(Video.URL));
                Video = new(Videos[0]);

                IsDisable = "Visible";
            }
            catch (System.Net.Http.HttpRequestException)
            {
                MessageBox.Show("Es posible que no haya internet");
            }
        } 
        catch (Exception)
        {
            MessageBox.Show("La URL está vacía");
        }
        finally
        {
            IsIndeterminate = false; 
        }

    }

}
