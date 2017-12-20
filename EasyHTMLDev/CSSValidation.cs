using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EasyHTMLDev
{
    internal static class CSSValidation
    {
        public static bool CSSValidate(string input, bool directAdd, out string reason, Library.CodeCSS css)
        {
            Regex reg = new Regex(@"(/[*]([^/]|[*])*/)|(([^:]+):([^;]*);?)", RegexOptions.IgnorePatternWhitespace);
            MatchCollection results = reg.Matches(input);
            IEnumerator el = results.GetEnumerator();

            for (int indexKey = 0; indexKey < css.Body.AllKeys.Count(); ++indexKey)
            {
                el.Reset();
                bool found = false;
                while (el.MoveNext())
                {
                    Match elem = el.Current as Match;
                    if (elem.Groups[3].Success)
                    {
                        if (!(String.IsNullOrEmpty(elem.Value.Trim()) || elem.Value.Trim().Contains("\r\n")))
                        {
                            string name = elem.Groups[4].Value.Trim();
                            if (css.Body.AllKeys[indexKey] == name)
                            {
                                found = true;
                                break;
                            }
                        }
                    }
                }
                if (!found)
                {
                    css.Body.Remove(css.Body.AllKeys[indexKey]);
                }
            }
            el.Reset();
            bool matched = false;
            while (el.MoveNext())
            {
                Match elem = el.Current as Match;
                if (elem.Groups[3].Success)
                {
                    if (!(String.IsNullOrEmpty(elem.Value.Trim()) || elem.Value.Trim().Contains("\r\n")))
                    {
                        matched = true;
                        string name = elem.Groups[4].Value.Trim();
                        string value = elem.Groups[5].Value.Trim();
                        if (directAdd)
                            css.AddIntoBody(name, value);
                        else
                            css.Discret(name, value);
                    }
                }
            }
            if (!matched && !String.IsNullOrWhiteSpace(input))
            {
                reason = Localization.Strings.GetString("ErrorIncorrectCSSFormat");
                return false;
            }
            else
            {
                reason = null;
                return true;
            }
        }

        public static bool CSSValidate(string input, bool directAdd, List<Library.CodeCSS> list, out string reason)
        {
            Regex reg = new Regex(@"(/[*]([^/]|[*])*/)|([^{]+)\{((" + Environment.NewLine + @")*|(\s)*|(([^:]+):([^;}]*);?))*\}", RegexOptions.IgnorePatternWhitespace);
            MatchCollection results = reg.Matches(input);
            IEnumerator el = results.GetEnumerator();
            List<Library.CodeCSS> copiedList = new List<Library.CodeCSS>();
            bool matched = false;
            if (el.MoveNext())
            {
                do
                {
                    Match elem = el.Current as Match;
                    if (elem.Success)
                    {
                        if (elem.Groups[3].Success)
                        {
                            matched = true;
                            string id = elem.Groups[3].Value;
                            Library.CodeCSS css = list.Find(a => a.Ids == id);
                            if (css == null)
                            {
                                css = new Library.CodeCSS();
                            }
                            copiedList.Add(css);
                            css.Ids = id;
                            for (int indexKey = css.Body.Count - 1; indexKey >= 0; --indexKey)
                            {
                                bool found = false;
                                for (int index = 0; index < elem.Groups[7].Captures.Count; ++index)
                                {
                                    Capture c = elem.Groups[7].Captures[index];
                                    if (!(String.IsNullOrEmpty(c.Value.Trim()) || c.Value.Trim().Contains("\r\n")))
                                    {
                                        string name = elem.Groups[8].Captures[index].Value.Trim();
                                        if (css.Body.AllKeys[indexKey] == name)
                                        {
                                            found = true;
                                            break;
                                        }
                                    }
                                }
                                if (!found)
                                {
                                    css.Body.Remove(css.Body.AllKeys[indexKey]);
                                }
                            }
                            for (int index = 0; index < elem.Groups[7].Captures.Count; ++index)
                            {
                                Capture c = elem.Groups[7].Captures[index];
                                if (!(String.IsNullOrEmpty(c.Value.Trim()) || c.Value.Trim().Contains("\r\n")))
                                {
                                    string name = elem.Groups[8].Captures[index].Value.Trim();
                                    string value = elem.Groups[9].Captures[index].Value.Trim();
                                    if (directAdd)
                                        css.AddIntoBody(name, value);
                                    else
                                        css.Discret(name, value);
                                }
                            }
                        }
                    }
                    else
                    {
                        break;
                    }
                } while (el.MoveNext());
            }
            if (!matched && !String.IsNullOrWhiteSpace(input))
            {
                reason = Localization.Strings.GetString("ErrorIncorrectCSSFormat");
                return false;
            }
            else
            {
                // add new css id
                foreach (Library.CodeCSS css in copiedList)
                {
                    if (!list.Exists(a => a.Ids == css.Ids))
                    {
                        list.Add(css);
                    }
                }
                // delete old css id and update change
                for (int index = list.Count() - 1; index >= 0; --index)
                {
                    Library.CodeCSS css = list[index];
                    Library.CodeCSS found = copiedList.Find(a => a.Ids == css.Ids);
                    if (found == null)
                    {
                        list.RemoveAt(index);
                    }
                    else
                    {
                        list[index] = found;
                    }
                }
                reason = null;
                return true;
            }
        }
    }
}
