using JSYoutubeDownloader.NET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Converter;
using YoutubeExplode.Videos;
using YoutubeExplode.Videos.Streams;
namespace JSYoutubeDownloader.NET.Services
{
    internal class DownloadService : IDownloadVideoService
    {
        public async Task DownloadAudio(VideoInfo video, string path, IProgress<double> progress)
        {
            YoutubeClient client = new();

            await client.Videos.DownloadAsync(video.Id, path, p => p.SetContainer("mp3").SetPreset(ConversionPreset.UltraFast), progress);
        }

        public async void DownloadVideo(VideoInfo video, string path, string quality, IProgress<double> progress)
        {
            throw new NotImplementedException();
        }
    }
}
