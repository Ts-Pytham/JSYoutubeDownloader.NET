namespace JSYoutubeDownloader.NET.Models;

public class Author : IAuthor
{

    public string Name { get; set; }

    public string Thumbnail { get; }

    public Author(string name, string thumbnail)
    {
        Name = name;
        Thumbnail = thumbnail;     
    }

    public Author(YoutubeExplode.Channels.Channel channel)
    {
        Name = channel.Title;
        Thumbnail = channel.Thumbnails[0].Url;
    }

}
