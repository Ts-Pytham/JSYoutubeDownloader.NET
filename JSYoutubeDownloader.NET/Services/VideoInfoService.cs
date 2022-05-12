using JSYoutubeDownloader.NET.Models;
using System.Threading.Tasks;
using YoutubeExplode;
using System;
using System.Collections.Generic;
using YoutubeExplode.Common;
using Author = JSYoutubeDownloader.NET.Models.Author;
using YoutubeExplode.Videos.Streams;
using YoutubeExplode.Videos;
using System.Linq;

namespace JSYoutubeDownloader.NET.Services;

internal class VideoInfoService : IVideoInfoService
{
    
    public async Task<VideoInfo> GetVideoInfo(string URL)
    {
        if (string.IsNullOrEmpty(URL))
            throw new Exception("La URL está vacía");

        YoutubeClient client = new();

        var video = await client.Videos.GetAsync(URL);
        
        var channel = await client.Channels.GetAsync(video.Author.ChannelId);
        VideoInfo info = video;
        
        info.Author = new Author(channel);
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
            
            var channel = await client.Channels.GetAsync(video.Author.ChannelId);
            VideoInfo info = video;
            
            info.Author = new Author(channel);
            videosInfo.Add(info);
        }

        return videosInfo;
    }

    public async Task<List<string>> GetQualities(VideoId id)
    {
        YoutubeClient youtube = new();
        StreamManifest stream = await youtube.Videos.Streams.GetManifestAsync(id);
        return stream.GetVideoStreams().Select(x => x.VideoQuality.Label).Distinct().OrderByDescending(x => x).ToList();
    }

    public async Task<List<string>> GetContainers(VideoId id)
    {

        YoutubeClient youtube = new();
        StreamManifest stream = await youtube.Videos.Streams.GetManifestAsync(id);
        return stream.GetVideoStreams().Select(x => x.Container.Name).Distinct().ToList();
    }
}
