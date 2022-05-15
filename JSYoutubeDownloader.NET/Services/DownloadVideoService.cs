namespace JSYoutubeDownloader.NET.Services
{
    internal class DownloadVideoService : IDownloadVideoService
    {
        public async Task DownloadAudio(VideoInfo video, string path, IProgress<double> progress, CancellationToken token)
        {
            YoutubeClient client = new();

            await client.Videos.DownloadAsync(video.Id, path, p => p.SetContainer("mp3").SetPreset(ConversionPreset.UltraFast), progress, token);
        }

        public async Task DownloadVideo(StreamManifest stream, VideoInfo video, string path, string quality, IProgress<double> progress, CancellationToken token)
        {
            YoutubeClient client = new();

            var videoStreamInfo = stream.GetVideoOnlyStreams().First(s => s.VideoQuality.Label == quality);
            var audioStreamInfo = stream.GetAudioOnlyStreams().GetWithHighestBitrate();

            var streamInfos = new IStreamInfo[] { audioStreamInfo, videoStreamInfo };
            
            await client.Videos.DownloadAsync(
                streamInfos, new ConversionRequestBuilder(path)
                            .SetPreset(ConversionPreset.UltraFast)
                            .Build(), progress, token);

        }
    }
}
