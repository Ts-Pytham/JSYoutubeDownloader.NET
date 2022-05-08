using JSYoutubeDownloader.NET.Commands;
using JSYoutubeDownloader.NET.Models;
using JSYoutubeDownloader.NET.Services;
using System;
using System.Windows;
using System.Windows.Input;

namespace JSYoutubeDownloader.NET.ViewModels;

internal class MainWindowViewModel : ViewModelBase
{

    #region Properties
    private VideoInfo? _video;

    public VideoInfo Video
    {
        get 
        {
            if (_video == null)
                throw new Exception("Video is null");

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

    #endregion

    #region Commands
    private RelayCommand? videoInfoCommands;
    public ICommand VideoInfoCommands => videoInfoCommands ??= new RelayCommand(VideoInfoCommandExecute);

    #endregion

    public MainWindowViewModel()
    {
        Video = new VideoInfo();
    }

    
    private async void VideoInfoCommandExecute(object commandParameter)
    {

    }

}
