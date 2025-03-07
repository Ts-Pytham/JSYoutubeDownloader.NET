namespace JSYoutubeDownloader.NET.Services;

public interface IDownloadVideoService
{
    Task DownloadAudioAsync(VideoInfo video, string path, IProgress<double> progress, CancellationToken token);

    Task DownloadVideoAsync(StreamManifest stream, VideoInfo video, string path, string quality,IProgress<double> progress, CancellationToken token);
}
