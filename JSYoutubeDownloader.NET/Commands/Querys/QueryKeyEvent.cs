namespace JSYoutubeDownloader.NET.Commands;

public class QueryKeyEvent : IQuery<KeyEventArgs>
{
    public object? Sender { get; set; }
    public KeyEventArgs Args { get; set; }

    public QueryKeyEvent(KeyEventArgs args)
    {
        Args = args;
    }
}
