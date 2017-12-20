using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Localization
{
    public static class Names
    {
        public static void Validate()
        {
            using (FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "names.txt", FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        if (Strings.GetString(line) == line)
                        {
                            foreach (string key in Strings.Languages.Keys)
                            {
                                string lang = Strings.Languages[key];
                                Strings.SelectLanguage(lang);
                                System.Diagnostics.Trace.WriteLine("the word '" + line + "' does not exist in '" + lang + "'");
                            }
                        }
                    }
                    sr.Close();
                }
            }
        }
    }
}
