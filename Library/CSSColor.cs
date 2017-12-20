using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Library
{
    [Serializable]
    public class CSSColor : ICloneable
    {
        #region Private Fields
        private Color? color;
        #endregion

        #region Public Constructors
        public CSSColor()
        {
            this.color = null;
        }

        public CSSColor(Color c)
        {
            this.color = c;
        }

        public CSSColor(string colorStr)
        {
            this.color = CSSColor.ParseColor(colorStr).Color;
        }
        #endregion

        #region Public Properties
        public Color Color { get { if (this.color != null && this.color.HasValue) return this.color.Value; else return Color.Transparent; } }
        public bool IsEmpty { get { return this.color == null || !this.color.HasValue; } }
        #endregion

        #region Public Static Methods
        public static bool TryEmpty(CSSColor c)
        {
            return (c == null || c.IsEmpty);
        }

        public static CSSColor ParseColor(string colorValue)
        {
            Regex rg = new Regex(@"(#(([0-9a-f][0-9a-f]){3,4}))|(rgba\((([0-9]|\s)+),(([0-9]|\s)+),(([0-9]|\s)+),(([0-9]|\s)+)\))|(#\([^)]+\))|([a-z0-9]+)", RegexOptions.IgnoreCase);
            Match m = rg.Match(colorValue.Trim());
            if (m.Success)
            {
                if (m.Groups[1].Success)
                {
                    Hexadecimal a, r, g, b;
                    bool ok = true;
                    int cap = 0;
                    if (m.Groups[2].Value.Length == 8)
                    {
                        ok &= Hexadecimal.TryParse(m.Groups[3].Captures[cap++].Value, out a);
                    }
                    else
                        a = new Hexadecimal("FF");
                    ok &= Hexadecimal.TryParse(m.Groups[3].Captures[cap++].Value, out r);
                    ok &= Hexadecimal.TryParse(m.Groups[3].Captures[cap++].Value, out g);
                    ok &= Hexadecimal.TryParse(m.Groups[3].Captures[cap++].Value, out b);
                    if (ok)
                        return new CSSColor(Color.FromArgb(a.Value, r.Value, g.Value, b.Value));
                    else
                        throw new FormatException();
                }
                else if (m.Groups[4].Success)
                {
                    float a = 0;
                    byte r = 0, g = 0, b = 0;
                    bool ok = true;
                    ok &= Byte.TryParse(m.Groups[5].Value, out r);
                    ok &= Byte.TryParse(m.Groups[7].Value, out g);
                    ok &= Byte.TryParse(m.Groups[9].Value, out b);
                    ok &= float.TryParse(m.Groups[11].Value, out a);
                    if (ok)
                        return new CSSColor(Color.FromArgb((byte)(a * 0xFF), r, g, b));
                    else
                        throw new FormatException();
                }
                else if (m.Groups[13].Success)
                {
                    throw new FormatException();
                }
                else if (m.Groups[14].Success)
                {
                    return new CSSColor(Color.FromName(m.Groups[14].Value));
                }
                else
                    throw new FormatException();
            }
            else
                throw new FormatException();
        }

        public static bool TryParse(string s, out CSSColor c)
        {
            bool res = false;

            try
            {
                c = CSSColor.ParseColor(s);
                res = true;
            }
            catch
            {
                c = null;
            }
            return res;
        }
        #endregion

        public object Clone()
        {
            if (this.color != null && this.color.HasValue)
            {
                return new CSSColor(this.color.Value);
            }
            else
            {
                return new CSSColor();
            }
        }
    }
}
