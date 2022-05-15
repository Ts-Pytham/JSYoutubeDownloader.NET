namespace JSYoutubeDownloader.NET.ViewModels;

public class MainWindowViewModel : ViewModelBase
{

    #region Properties
    private DownloadView? _view;

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
    private RelayCommand? _videoInfoCommands;
    private RelayCommand? _downloadCommand;
    public ICommand VideoInfoCommands => _videoInfoCommands ??= new RelayCommand(VideoInfoCommandExecute);
    public ICommand DownloadCommand => _downloadCommand ??= new RelayCommand(DownloadCommandExecute);
    #endregion

    public MainWindowViewModel()
    {
        _video = new VideoInfo();
        _videos = new ObservableCollection<VideoInfo>();
       _isIndeterminate = false;
        _isDisable = "Hidden";
    }

    private async void DownloadCommandExecute(object commandParameter)
    {
        try
        {
            IsIndeterminate = true;
            DownloadViewModel vm = await DownloadViewModel.Load(Video);

            if (_view == null || PresentationSource.FromVisual(_view) == null)
            {
                _view = new(vm);
                _view.Show();
            }
            
        }
        catch (YoutubeExplode.Exceptions.VideoUnavailableException)
        {
            MessageBox.Show("No hay ningún vídeo para descargar!", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        catch (YoutubeExplode.Exceptions.VideoUnplayableException)
        {
            MessageBox.Show("Este vídeo contiene restricción de edad!", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        finally
        {
            IsIndeterminate = false;
        }
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
                if(Videos.Count == 0)
                {
                    MessageBox.Show("No se encontró ningún vídeo, revisa la URL", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                Video = Videos[0];

                IsDisable = "Visible";
            }
            catch (System.Net.Http.HttpRequestException)
            {
                MessageBox.Show("Es posible que no haya internet", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        catch (System.Net.Http.HttpRequestException)
        {
            MessageBox.Show("Al parecer no tienes internet, intenta conectarte", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        catch (Exception)
        {
            MessageBox.Show("La URL está vacía", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        finally
        {
            IsIndeterminate = false; 
        }

    }

}
