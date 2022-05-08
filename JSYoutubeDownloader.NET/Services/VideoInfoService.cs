using JSYoutubeDownloader.NET.Models;
using System.Threading.Tasks;
using YoutubeExplode;
using System;

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
}
