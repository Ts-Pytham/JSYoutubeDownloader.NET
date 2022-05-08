using JSYoutubeDownloader.NET.Models;
using System.Threading.Tasks;

namespace JSYoutubeDownloader.NET.Services;

internal interface IVideoInfoService
{
    Task<VideoInfo> GetVideoInfo();
}
