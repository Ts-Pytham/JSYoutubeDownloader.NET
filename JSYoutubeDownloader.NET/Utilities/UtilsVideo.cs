namespace JSYoutubeDownloader.NET.Utilities;

public static partial class Utils
{
    public static string VideoURLToEmbedURL(this Video video)
    {
        return $"https://www.youtube.com/embed/{video.Id}";
    }

    public static void SetVideoWithOther(this VideoInfo video, VideoInfo v)
    {
        video.Title = v.Title;
        video.Statistics = v.Statistics;
        video.Author = v.Author;
        video.Thumbnail = v.Thumbnail;
        video.Description = v.Description;
        video.EmbedURL = v.EmbedURL;
        video.URL = v.URL;
        video.Id = v.Id;
    }
}
