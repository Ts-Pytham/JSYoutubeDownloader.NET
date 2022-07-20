namespace JSYoutubeDownloader.NET.Commands;

public interface IQuery<T> where T : EventArgs
{
    public object? Sender { get; set; }
    public T Args { get; set; }
}
