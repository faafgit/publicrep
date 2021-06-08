using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkHttp
{
    class mimetypestring
    {
        private static byte[] savestream;
        public static void savecontent(string mimestring,string type,string filename)
        {

            if (type == "data:image/jpeg;base64,")
            {
                string[] spileter={type};
                var content=mimestring.Split(spileter,StringSplitOptions.None);
                savestream = Convert.FromBase64String(content[1]);
                System.IO.File.WriteAllBytes(@"..\..\files\"+filename, savestream);
            }
        }
        public static byte[] readfile(string filename,string foldername)
        {
            System.IO.StreamReader sr = new System.IO.StreamReader(@"..\..\"+foldername+@"\" + filename);
            System.IO.Stream s = sr.BaseStream;
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                s.CopyTo(ms);
                return ms.ToArray();
            }
        }
        public static bool IsBase64String(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
                return false;
            s = s.Trim();
            return (s.Length % 4 == 0) && System.Text.RegularExpressions.Regex.IsMatch(s, @"^[a-zA-Z0-9\+/]*={0,3}$",System.Text.RegularExpressions.RegexOptions.None);
        }
    }
}
