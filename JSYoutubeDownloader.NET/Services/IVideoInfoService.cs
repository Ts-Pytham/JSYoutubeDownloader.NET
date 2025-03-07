namespace JSYoutubeDownloader.NET.Services;

internal interface IVideoInfoService
{
    Task<VideoInfo> GetVideoInfoAsync(string URL);

    Task<List<VideoInfo>> GetVideosInfoAsync(string Word);

    Task<List<dynamic>> GetQualitiesAsync(VideoId id);

    Task<List<dynamic>> GetContainersAsync(VideoId id);
}
