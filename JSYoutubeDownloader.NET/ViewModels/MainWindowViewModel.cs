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

    #endregion

    #region Commands
    private RelayCommand? videoInfoCommands;
    public ICommand VideoInfoCommands => videoInfoCommands ??= new RelayCommand(VideoInfoCommandExecute);

    #endregion

    public MainWindowViewModel()
    {
        Video = new VideoInfo();
        IsIndeterminate = false;
    }

    
    private async void VideoInfoCommandExecute(object commandParameter)
    {
        try
        {
            IVideoInfoService service = new VideoInfoService();
            IsIndeterminate = true;
            Video = await service.GetVideoInfo(Video.URL);
            
        }
        catch (ArgumentException)
        {
            IVideoInfoService service = new VideoInfoService();
            IsIndeterminate = true;
            Video = (await service.GetVideosInfo(Video.URL))[0];
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
