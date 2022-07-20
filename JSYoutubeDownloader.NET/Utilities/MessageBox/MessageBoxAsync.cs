namespace JSYoutubeDownloader.NET.Utilities;

public static class MessageBoxAsync
{
    public static Task<MessageBoxResult> Show(string messageBoxText)
    {
        return Task.Run(() => MessageBox.Show(messageBoxText));
    }

    public static Task<MessageBoxResult> Show(string messageBoxText, string caption)
    {
        return Task.Run(() => MessageBox.Show(messageBoxText, caption));
    }

    public static Task<MessageBoxResult> Show(string messageBoxText, string caption, MessageBoxButton button)
    {
        return Task.Run(() => MessageBox.Show(messageBoxText, caption, button));
    }

    public static Task<MessageBoxResult> Show(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon)
    {
        return Task.Run(() => MessageBox.Show(messageBoxText, caption, button, icon));
    }

    public static Task<MessageBoxResult> Show(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult)
    {
        return Task.Run(() => MessageBox.Show(messageBoxText, caption, button, icon, defaultResult));
    }

    public static Task<MessageBoxResult> Show(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult, MessageBoxOptions options)
    {
        return Task.Run(() => MessageBox.Show(messageBoxText, caption, button, icon, defaultResult, options));
    }
}
