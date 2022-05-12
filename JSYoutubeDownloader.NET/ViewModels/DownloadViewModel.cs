using JSYoutubeDownloader.NET.Models;
using JSYoutubeDownloader.NET.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace JSYoutubeDownloader.NET.ViewModels;

public class DownloadViewModel : ViewModelBase
{
    #region Properties


    private List<string> _containers;

    public List<string> Containers
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

    public bool IsIndeterminate
    {
        get { return _isIndeterminate; }
        set { _isIndeterminate = value; OnPropertyChanged(); }
    }

    #endregion


    private DownloadViewModel(List<string> Containers, List<string> Qualities)
    {     
        _containers = Containers;
        _qualities = Qualities;
        _path = "";
   
    }

    public static async Task<DownloadViewModel> Load(VideoInfo video)
    {
        IVideoInfoService service = new VideoInfoService();
        var _containers = await service.GetContainers(video.Id);
        List<string> _qualities = await service.GetQualities(video.Id);
        return new DownloadViewModel(_containers, _qualities);
    }
}
