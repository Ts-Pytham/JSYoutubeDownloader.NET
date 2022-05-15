using JSYoutubeDownloader.NET.Models.Interfaces;
using JSYoutubeDownloader.NET.Utilities;

namespace JSYoutubeDownloader.NET.Models;

public class Statistics : IStatistics
{
    public long ViewCount { get; set; }
    public long LikeCount { get; set; }
    public long DislikeCount { get; set; }

    public Statistics(long viewCount, long likeCount, long dislikeCount)
    {
        ViewCount = viewCount;
        LikeCount = likeCount;
        DislikeCount = dislikeCount;
    }
}
