using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public static string FormatInDecimal(char letter, string sentence)
        {
            int len = sentence.Length;
            int j = 0;
            if (len <= 3) return sentence;
            for (int i = 0; i != len; ++i)
            {
                if (j == 3)
                {
                    sentence = sentence.Insert(len - i, letter.ToString());
                    j = 0;
                }
                j++;
            }
            return sentence;
        }
    }
}
