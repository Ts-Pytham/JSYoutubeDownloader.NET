using JSYoutubeDownloader.NET.Models;
using System.Threading.Tasks;
using YoutubeExplode;
using System;
using System.Collections.Generic;
using YoutubeExplode.Common;

namespace JSYoutubeDownloader.NET.Services;

internal class VideoInfoService : IVideoInfoService
{

    public async Task<VideoInfo> GetVideoInfo(string URL)
    {
        if (string.IsNullOrEmpty(URL))
            throw new Exception("La URL está vacía");

        YoutubeClient client = new();

        var video = await client.Videos.GetAsync(URL);

        VideoInfo info = video;
        return info;
        
    }

    public async Task<List<VideoInfo>> GetVideosInfo(string word)
    {
        YoutubeClient client = new();

        List<VideoInfo> videosInfo = new();
        var results = await client.Search.GetVideosAsync(word).CollectAsync(5);
        foreach(var result in results)
        {
            var video = await client.Videos.GetAsync(result.Url);
            VideoInfo info = video;
            videosInfo.Add(info);
        }

        return videosInfo;
    }
}
