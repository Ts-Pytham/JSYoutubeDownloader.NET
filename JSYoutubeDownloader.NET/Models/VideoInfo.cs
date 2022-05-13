using JSYoutubeDownloader.NET.Models.Interfaces;
using YoutubeExplode.Common;
using YoutubeExplode.Videos;

namespace JSYoutubeDownloader.NET.Models;

public class VideoInfo : IVideoInfo
{
    public string URL { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public IAuthor Author { get; set; }

    public string Thumbnail { get; set; }

    public IStatistics Statistics { get; set; }
    public VideoId Id { get; private set; }

    public VideoInfo(string uRL, string title, string description, Author author, string thumbnail, Statistics statistics)
    {
        URL = uRL;
        Title = title;
        Description = description;
        Author = author;
        Thumbnail = thumbnail;
        Statistics = statistics;
    }

    public VideoInfo()
    {
        URL = "";
        Title = "";
        Description = "";
        Author = new Author("", "");
        Thumbnail = "";
        Statistics = new Statistics("", "", "");
    }

    public static implicit operator VideoInfo(Video v)
    {
        VideoInfo video = new()
        {
            URL = v.Url,
            Title = v.Title,
            Description = v.Description,
            Thumbnail = v.Thumbnails.TryGetWithHighestResolution()?.Url ?? "",
            Statistics = new Statistics(v.Engagement.ViewCount.ToString(), v.Engagement.LikeCount.ToString(), v.Engagement.DislikeCount.ToString()),
            Id = v.Id
        };
        return video;
    }
}
