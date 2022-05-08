using JSYoutubeDownloader.NET.Models.Interfaces;

namespace JSYoutubeDownloader.NET.Models;

internal class Statistics : IStatistics
{
    public long ViewCount { get; }
    public long LikeCount { get; }
    public long DislikeCount { get; }

    public Statistics(long viewCount, long likeCount, long dislikeCount)
    {
        ViewCount = viewCount;
        LikeCount = likeCount;
        DislikeCount = dislikeCount;
    }
}
