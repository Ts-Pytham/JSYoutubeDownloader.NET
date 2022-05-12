using JSYoutubeDownloader.NET.Models;
using JSYoutubeDownloader.NET.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSYoutubeDownloader.NET.ViewModels;

public class DownloadViewModel : ViewModelBase
{
    #region Properties
    private List<string> _containers;

    public List<string> ContainerList
    {
        get { return _containers; }
        set { _containers = value; OnPropertyChanged(); }
    }

    private List<string> _qualities;

    public List<string> Qualities
    {
        get { return _qualities; }
        set { _qualities = value; OnPropertyChanged(); }
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

    public bool Indeterminate
    {
        get { return _isIndeterminate; }
        set { _isIndeterminate = value; OnPropertyChanged(); }
    }

    #endregion

    public DownloadViewModel()
    {
        _containers = new();
        _qualities = new();
        _path = "";
    }
    public DownloadViewModel(VideoInfo video)
    {
        _isIndeterminate = true;
        _containers = new();
        _qualities = new();
        Load(video);
        _path = "";
        _isIndeterminate = false;
    }

    public async void Load(VideoInfo video)
    {
        IVideoInfoService service = new VideoInfoService();
        _containers = await service.GetContainers(video.Id);
        _qualities = await service.GetQualities(video.Id);
    }
}
