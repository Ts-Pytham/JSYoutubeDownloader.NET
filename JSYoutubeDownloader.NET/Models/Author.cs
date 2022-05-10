﻿using JSYoutubeDownloader.NET.Models.Interfaces;

namespace JSYoutubeDownloader.NET.Models;

internal class Author : IAuthor
{
    public string Name { get; }

    public string Thumbnail { get; }

    public Author(string name, string thumbnail)
    {
        Name = name;
        Thumbnail = thumbnail;     
    }
}
