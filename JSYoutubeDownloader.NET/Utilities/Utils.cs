namespace JSYoutubeDownloader.NET.Utilities;

public static partial class Utils
{
    private struct FullPath(string path, string format)
    {
        public string Path { get; } = path;
        public long Number { get; private set; } = 0;
        public string Format { get; } = format;

        public override readonly string ToString()
        {
            return Number == 0 ? $"{Path}.{Format}" : $"{Path}({Number}).{Format}";
        }

        public void ChangePath()
        {
            Number++;
        }
    }

    public static string ChangeFormat(string str)
    {
        StringBuilder strb = new(str);
        int len = strb.Length;

        for (int i = 0; i != len; ++i)
        {
            var c = strb[i];

            if (c == '|' || c == 92 || c == '/' || c == ':' || c == '?' || c == '<' || c == '>' || c == '"')
            {
                strb[i] = '-';
            }
        }

        return strb.ToString();
    }

    public static async Task<string> CheckFileAsync(string path, string container)
    {

        FullPath fullPath = new(path.ToString(), container);

        while (IO.File.Exists(fullPath.ToString()))
        {
            string message = "Ya existe un archivo con el mismo nombre, ¿quieres que se le agregue un nombre? " +
            $"Si seleccionas la opción 'Si' se le agregará un número, ejemplo: {fullPath}.\n" +
            "Si seleccionas la opción 'No' se reemplazará el archivo actual.";

            var result = await MessageBoxAsync.ShowAsync(message, "Información", MessageBoxButton.YesNo, MessageBoxImage.Information);

            if (result is MessageBoxResult.Yes)
            {
                fullPath.ChangePath();
            }
            else
                break;
        }

        return fullPath.ToString();
    }

}
