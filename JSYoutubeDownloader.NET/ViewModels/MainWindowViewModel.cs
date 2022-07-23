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
    private Mvvm.RelayCommand<KeyEventArgs>? _onKeyDown;
    private Mvvm.RelayCommand<CancelEventArgs>? _windowClosing;
    public ICommand VideoInfoCommands => _videoInfoCommands ??= new RelayCommand(VideoInfoCommandExecute);
    public ICommand DownloadCommand => _downloadCommand ??= new RelayCommand(DownloadCommandExecute);
    public ICommand WindowClosing => _windowClosing ??= new Mvvm.RelayCommand<CancelEventArgs>(WindowClosingCommandExecute);
    public ICommand OnKeyDown => _onKeyDown ??= new Mvvm.RelayCommand<KeyEventArgs>(OnKeyDownHandler);
    #endregion

    public MainWindowViewModel()
    {
        _video = new VideoInfo();
        _videos = new();
       _isIndeterminate = false;
        _isDisable = "Hidden";
        Thread thread = new(CheckVersion);
        thread.Start();

    }

    #region Methods

    private void CheckVersion()
    {
        string version = ConfigurationManager.AppSettings["Version"] ?? "";

        if (version is "") return;

        HtmlWeb web = new();
        string url = "https://github.com/Ts-Pytham/JSYoutubeDownloader.NET";
        var doc = web.Load(url);

        var data = doc.DocumentNode.CssSelect(".ml-2").CssSelect(".css-truncate").First().InnerHtml;
        if(data != version)
        {
            var msg = MessageBox.Show("La versión es diferente, puede descargar la nueva versión en la web.",
                             "Información",MessageBoxButton.OKCancel, MessageBoxImage.Information);

            if (msg == MessageBoxResult.OK)
            {
                string argument = $"/c start {url}/releases/tag/{data.ToLower()}";
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo("cmd", argument) { CreateNoWindow = true });
            }
        }
    }
    #endregion


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
            _ = MessageBoxAsync.Show("No hay ningún vídeo para descargar!", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        catch (YoutubeExplode.Exceptions.VideoUnplayableException)
        {
            _ = MessageBoxAsync.Show("Este vídeo contiene restricción de edad!", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
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
                var videos = await service.GetVideosInfo(Video.URL);
               
                if(videos.Count == 0)
                {
                    _ = MessageBoxAsync.Show("No se encontró ningún vídeo, revisa la URL", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    IsDisable = "Hidden";
                    return;
                }
                if (Videos.Count == 5) // Si ya hay elementos en la lista, solo se reemplazará la información y no crear de nuevo la lista.
                {
                    for (int i = 0; i != 5; ++i)
                    {
                        Videos[i].SetVideoWithOther(videos[i]);
                        
                    }
                }
                else
                {
                    Videos = new ObservableCollection<VideoInfo>(videos);
                }

                Video = Videos[0];

                IsDisable = "Visible";
            }
            catch (System.Net.Http.HttpRequestException)
            {
                _ = MessageBoxAsync.Show("Es posible que no haya internet", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        catch (System.Net.Http.HttpRequestException)
        {
            _ = MessageBoxAsync.Show("Al parecer no tienes internet, intenta conectarte", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            _ = MessageBoxAsync.Show("La URL está vacía!", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        finally
        {
            
            IsIndeterminate = false; 
        }

    }

    #region Events
    private void WindowClosingCommandExecute(CancelEventArgs? e)
    {
        if (e is null) return;

        _view?.Close();

    }

    private void OnKeyDownHandler(KeyEventArgs? e)
    {     
        if (e?.Key == Key.Enter)
        {
            VideoInfoCommandExecute(e);
        }
    }
    #endregion
}
