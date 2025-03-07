namespace JSYoutubeDownloader.NET.Utilities;

public static class MessageBoxAsync
{
    public static Task<MessageBoxResult> ShowAsync(
        string messageBoxText,
        string caption,
        MessageBoxButton button,
        MessageBoxImage icon)
    {
        return Task.Run(() => MessageBox.Show(messageBoxText, caption, button, icon));
    }
}
