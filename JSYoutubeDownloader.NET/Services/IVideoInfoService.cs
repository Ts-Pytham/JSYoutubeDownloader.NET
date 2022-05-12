﻿using JSYoutubeDownloader.NET.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using YoutubeExplode.Videos;

namespace JSYoutubeDownloader.NET.Services;

internal interface IVideoInfoService
{
    Task<VideoInfo> GetVideoInfo(string URL);

    Task<List<VideoInfo>> GetVideosInfo(string Word);

    Task<List<string>> GetQualities(VideoId id);

    Task<List<string>> GetContainers(VideoId id);
}
