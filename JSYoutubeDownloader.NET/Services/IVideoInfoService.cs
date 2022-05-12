using JSYoutubeDownloader.NET.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using YoutubeExplode.Videos;
using YoutubeExplode.Videos.Streams;

namespace JSYoutubeDownloader.NET.Services;

internal interface IVideoInfoService
{
    Task<VideoInfo> GetVideoInfo(string URL);

    Task<List<VideoInfo>> GetVideosInfo(string Word);

    Task<List<dynamic>> GetQualities(VideoId id);

    Task<List<dynamic>> GetContainers(VideoId id);
}
