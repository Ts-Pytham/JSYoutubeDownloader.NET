namespace JSYoutubeDownloader.NET.Utilities;

public static partial class Utils
{
    private struct FullPath
    {
        public string Path { get; }
        public long Number { get; private set; }
        public string Format { get; }

        public FullPath(string path, string format)
        {
            Path = path;
            Format = format;
            Number = 0;
        }

        public override string ToString()
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

            if (strb[i] == '|' || strb[i] == 92 || strb[i] == '/' || strb[i] == ':' || strb[i] == '?' || strb[i] == '<' || strb[i] == '>' || strb[i] == '"')
            {
                strb[i] = '-';
            }
        }
        return strb.ToString();
    }

    public static async Task<string> CheckFile(string path, string container)
    {

        FullPath fullPath = new(path.ToString(), container);

        while (IO.File.Exists(fullPath.ToString()))
        {
            string message = "Ya existe un archivo con el mismo nombre, ¿quieres que se le agregue un nombre? " +
            $"Si seleccionas la opción 'Si' se le agregará un número, ejemplo: {fullPath}.\n" +
            "Si seleccionas la opción 'No' se reemplazará el archivo actual.";

            var result = await MessageBoxAsync.Show(message, "Información", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if(result == MessageBoxResult.Yes)
            {
                fullPath.ChangePath();
            }
            else
            {
                break;
            }
        }

        return fullPath.ToString();
    }

}
