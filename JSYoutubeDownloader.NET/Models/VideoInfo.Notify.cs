namespace JSYoutubeDownloader.NET.Models;

public partial class VideoInfo : INotifyPropertyChanged
{

    private string _url;

    public string URL
    {
        get
        {
            return _url;
        }
        set
        {
            if (_url == value)
                return;
            _url = value;
            OnPropertyChanged();
        }
    }
    private string _title;
    public string Title
    {
        get
        {
            return _title;
        }
        set
        {
            if (_title == value)
                return;
            _title = value;
            OnPropertyChanged();
        }
    }

    private string _thumbnail;
    public string Thumbnail
    {
        get
        {
            return _thumbnail;
        }
        set
        {
            if (_thumbnail == value)
                return;
            _thumbnail = value;
            OnPropertyChanged();
        }
    }

    private IAuthor _author;
    public IAuthor Author
    {
        get
        {
            return _author;
        }
        set
        {
            if (_author == value)
                return;
            _author = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {

        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
