namespace JSYoutubeDownloader.NET.Models.Interfaces;

public interface IStatistics
{
    long ViewCount { get; set; }
    long LikeCount { get; set; }
    long DislikeCount { get; set; }

}
