namespace JSYoutubeDownloader.NET.Models.Interfaces;

public interface IVideoInfo
{
    string URL { get; }

    string Title { get; }

    string Description { get; }

    IAuthor Author { get; }

    string Thumbnail { get; }

    IStatistics Statistics { get; }
}
