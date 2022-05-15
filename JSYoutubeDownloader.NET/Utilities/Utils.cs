namespace JSYoutubeDownloader.NET.Utilities
{
    public static class Utils
    {
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
    }
}
