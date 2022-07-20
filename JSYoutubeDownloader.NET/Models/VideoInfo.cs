namespace JSYoutubeDownloader.NET.Models;

public partial class VideoInfo : IVideoInfo
{
    public string EmbedURL { get; set; }
    public string Description { get; set; }
  
    public IStatistics Statistics { get; set; }
    public VideoId Id { get; set; }


    public VideoInfo()
    {
        _url = "";
        _title = "";
        Description = "";
        EmbedURL = "";
        _author = new Author("", "");
        _thumbnail = "";
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
