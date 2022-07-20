namespace JSYoutubeDownloader.NET.Services;

public interface IDownloadVideoService
{
    Task DownloadAudio(VideoInfo video, string path, IProgress<double> progress, CancellationToken token);

    Task DownloadVideo(StreamManifest stream, VideoInfo video, string path, string quality,IProgress<double> progress, CancellationToken token);
}
