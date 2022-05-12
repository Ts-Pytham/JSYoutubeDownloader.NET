using JSYoutubeDownloader.NET.Models.Interfaces;
using JSYoutubeDownloader.NET.Utilities;

namespace JSYoutubeDownloader.NET.Models;

public class Statistics : IStatistics
{
    public string ViewCount { get; set; }
    public string LikeCount { get; set; }
    public string DislikeCount { get; set; }

    public Statistics(string viewCount, string likeCount, string dislikeCount)
    {
        ViewCount = Utils.FormatInDecimal('.', viewCount);
        LikeCount = Utils.FormatInDecimal('.', likeCount);
        DislikeCount = dislikeCount;
    }
}
