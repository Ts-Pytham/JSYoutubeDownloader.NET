using JSYoutubeDownloader.NET.Models.Interfaces;

namespace JSYoutubeDownloader.NET.Models;

internal class VideoInfo : IVideoInfo
{
    public string URL { get; }

    public string Title { get; }

    public string Description { get; }

    public IAuthor Author { get; }

    public string Thumbnail { get; }

    public IStatistics Statistics { get; }

    public VideoInfo(string uRL, string title, string description, Author author, string thumbnail, Statistics statistics)
    {
        URL = uRL;
        Title = title;
        Description = description;
        Author = author;
        Thumbnail = thumbnail;
        Statistics = statistics;
    }
}
