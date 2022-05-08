using System.Windows;
using System.Windows.Controls;

namespace JSYoutubeDownloader.NET.Styles;


internal class TextBoxEx : TextBox
{
    protected override void OnGotFocus(RoutedEventArgs e)
    {
        if (Text == "Ingresa una URL")
            Text = "";
        base.OnGotFocus(e);
    }

    protected override void OnLostFocus(RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(Text))
            Text = "Enter text here...";

        base.OnLostFocus(e);
    }
}
