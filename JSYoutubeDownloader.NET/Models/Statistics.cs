namespace JSYoutubeDownloader.NET.Models;

public class Statistics(long viewCount, long likeCount, long dislikeCount) 
    : IStatistics
{
    public long ViewCount { get; set; } = viewCount;
    public long LikeCount { get; set; } = likeCount;
    public long DislikeCount { get; set; } = dislikeCount;
}
