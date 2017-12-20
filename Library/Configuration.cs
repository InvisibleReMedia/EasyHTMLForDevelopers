using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Text.RegularExpressions;

namespace Library
{
    [Serializable]
    public class Configuration
    {
        private NameValueCollection collection = new NameValueCollection();

        public NameValueCollection Elements
        {
            get { return this.collection; }
        }

        public string Replace(string input)
        {
            string output = String.Empty;
            if (!String.IsNullOrEmpty(input))
            {
                Regex reg = new Regex(@"#\((.*)\)|([^#]+)|(#[^(])", RegexOptions.Multiline);
                MatchCollection results = reg.Matches(input);
                foreach (Match m in results)
                {
                    if (m.Success)
                    {
                        if (m.Groups[1].Success)
                        {
                            if (this.Elements.AllKeys.Contains(m.Groups[1].Value))
                                output += this.Replace(this.Elements[m.Groups[1].Value]);
                            else
                            {
                                string replaced = this.Replace(m.Groups[1].Value);
                                if (this.Elements.AllKeys.Contains(replaced))
                                    output += this.Elements[replaced];
                            }
                        }
                        else if (m.Groups[2].Success)
                        {
                            output += m.Groups[2].Value;
                        }
                        else if (m.Groups[3].Success)
                        {
                            output += m.Groups[3].Value;
                        }
                        else
                        {
                            throw new Exception(String.Format(Localization.Strings.GetString("ExceptionMalFormedContent"), input));
                        }
                    }
                    else
                    {
                        output += m.Value;
                    }
                }
            }
            return output;
        }
    }
}
