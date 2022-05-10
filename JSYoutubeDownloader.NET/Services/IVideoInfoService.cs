using JSYoutubeDownloader.NET.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JSYoutubeDownloader.NET.Services;

internal interface IVideoInfoService
{
    Task<VideoInfo> GetVideoInfo(string URL);

    Task<List<VideoInfo>> GetVideosInfo(string Word);
}
