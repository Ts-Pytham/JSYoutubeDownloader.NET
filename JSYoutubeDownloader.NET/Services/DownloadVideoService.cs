using System.Drawing;
using System.Drawing.Imaging;
using System.Net.Http;

namespace JSYoutubeDownloader.NET.Services;

internal class DownloadVideoService : IDownloadVideoService
{
    public async Task DownloadAudio(VideoInfo video, string path, IProgress<double> progress, CancellationToken token)
    {
        YoutubeClient client = new();

        var bytesImage = await GetImageBytesAsnyc(video.Thumbnail);

        await client.Videos.DownloadAsync(video.Id, path, p => p.SetContainer("mp3").SetPreset(ConversionPreset.UltraFast), progress, token);
        DownloadPicture(path, bytesImage);
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

    private static void DownloadPicture(string path, byte[] imageBytes)
    {
        var file = TagLib.File.Create(path);

        using var image = Image.FromStream(new IO.MemoryStream(imageBytes));
        using var resizedImage = new Bitmap(image, new System.Drawing.Size(500, 500));
        using IO.MemoryStream ms = new();
        
        resizedImage.Save(ms, ImageFormat.Jpeg);
        imageBytes = ms.ToArray();

        TagLib.Picture picture = new(new TagLib.ByteVector(imageBytes));
        file.Tag.Pictures = new TagLib.IPicture[] { picture };

        file.Save();
    }

    private static async Task<byte[]> GetImageBytesAsnyc(string img)
    {
        using HttpClient client = new();
        using HttpResponseMessage response = await client.GetAsync(img);
        if (response.IsSuccessStatusCode)
            return await response.Content.ReadAsByteArrayAsync();
        else
            throw new Exception("La imagen no se puede descargar");
    }
}
