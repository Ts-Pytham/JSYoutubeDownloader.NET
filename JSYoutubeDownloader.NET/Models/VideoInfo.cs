using JSYoutubeDownloader.NET.Models.Interfaces;
using System;
using YoutubeExplode.Videos;

namespace JSYoutubeDownloader.NET.Models;

internal class VideoInfo : IVideoInfo
{

    public string URL { get; set; }


    public string Title { get; set; }

    public string Description { get; set; }

    public IAuthor Author { get; set; }

    public string Thumbnail { get; set; }

    public IStatistics Statistics { get; set; }

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
        Statistics = new Statistics(0, 0, 0);
    }

    public static implicit operator VideoInfo(Video v)
    {
        VideoInfo video = new()
        {
            URL = v.Url,
            Title = v.Title,
            Description = v.Description,
            Thumbnail = v.Thumbnails[4].Url,
            Statistics = new Statistics(v.Engagement.ViewCount, v.Engagement.LikeCount, v.Engagement.DislikeCount)
        };
        return video;
    }
}
