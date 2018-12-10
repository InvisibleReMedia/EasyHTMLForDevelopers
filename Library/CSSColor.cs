using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Library
{
    /// <summary>
    /// Keep a color
    /// </summary>
    [Serializable]
    public class CSSColor : Marshalling.PersistentDataObject, ICloneable
    {

        #region Private Fields

        /// <summary>
        /// Index name for color
        /// </summary>
        protected static readonly string colorName = "color";

        #endregion

        #region Public Constructors

        /// <summary>
        /// Empty constructor
        /// </summary>
        public CSSColor()
        {
        }

        /// <summary>
        /// Constructor with a specific color
        /// </summary>
        /// <param name="c"></param>
        public CSSColor(Color c)
        {
            this.Set(colorName, c);
        }

        /// <summary>
        /// Constructor with a string representation of a color
        /// </summary>
        /// <param name="colorStr">color</param>
        public CSSColor(string colorStr)
        {
            this.Set(colorName, CSSColor.ParseColor(colorStr).Color);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the color
        /// </summary>
        public Color Color
        {
            get
            {
                if (this.Get(colorName) != null)
                    return this.Get(colorName);
                else
                    return System.Drawing.Color.Transparent;
            }
        }

        /// <summary>
        /// Gets if this object has a setted color
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return !this.Exists(colorName);
            }
        }

        #endregion

        #region Public Static Methods

        /// <summary>
        /// Parse color and return a new CSSColor
        /// </summary>
        /// <param name="colorValue">color value to parse</param>
        /// <returns>object result</returns>
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

        /// <summary>
        /// Try Parse : a same parsing fonction but
        /// do not raise an exception : return true if succeeded; the color result is a param out
        /// </summary>
        /// <param name="s">color value string</param>
        /// <param name="c">color solution</param>
        /// <returns>true if succeeded</returns>
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

        /// <summary>
        /// Clone the object
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            if (this.IsEmpty)
                return new CSSColor();
            else
                return new CSSColor(this.Color);
        }
    }
}
