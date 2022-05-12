using JSYoutubeDownloader.NET.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JSYoutubeDownloader.NET.Services
{
    public interface IDownloadVideoService
    {
        Task DownloadAudio(VideoInfo video, string path, IProgress<double> progress);

        void DownloadVideo(VideoInfo video, string path, string quality,IProgress<double> progress);
    }
}
