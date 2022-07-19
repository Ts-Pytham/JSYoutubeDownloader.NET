namespace JSYoutubeDownloader.NET.Models;

public class VideoInfo : IVideoInfo
{
    public string URL { get; set; }
    public string EmbedURL { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public IAuthor Author { get; set; }

    public string Thumbnail { get; set; }

    public IStatistics Statistics { get; set; }
    public VideoId Id { get; private set; }

    public VideoInfo(string uRL, string embedURL,string title, string description, Author author, string thumbnail, Statistics statistics)
    {
        URL = uRL;
        Title = title;
        Description = description;
        Author = author;
        Thumbnail = thumbnail;
        Statistics = statistics;
        EmbedURL = embedURL;
    }

    public VideoInfo()
    {
        URL = "";
        Title = "";
        Description = "";
        EmbedURL = "";
        Author = new Author("", "");
        Thumbnail = "";
        Statistics = new Statistics(0, 0,0);
    }

    public static implicit operator VideoInfo(Video v)
    {
        VideoInfo video = new()
        {
            URL = v.Url,
            Title = v.Title,
            Description = v.Description,
            Thumbnail = v.Thumbnails.TryGetWithHighestResolution()?.Url ?? "",
            Statistics = new Statistics(v.Engagement.ViewCount, v.Engagement.LikeCount, v.Engagement.DislikeCount),
            Id = v.Id,
            EmbedURL = v.VideoURLToEmbedURL()
        };
        return video;
    }
}
