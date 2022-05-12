namespace JSYoutubeDownloader.NET.Models.Interfaces;

public interface IStatistics
{
    long ViewCount { get; }
    long LikeCount { get; }
    long DislikeCount { get; }

}
