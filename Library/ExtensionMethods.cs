using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    /// <summary>
    /// Set of extensions
    /// </summary>
    internal static class ExtensionMethods
    {

        /// <summary>
        /// Clone a string
        /// </summary>
        /// <param name="toClone">string input</param>
        /// <returns>a new string with the same content than its input</returns>
        public static String CloneThis(String toClone)
        {
            if (toClone != null)
            {
                return toClone.Clone() as String;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Clone a rectangle
        /// </summary>
        /// <param name="r">rectangle source</param>
        /// <returns>a new rectangle with the same value that its input</returns>
        public static Rectangle CloneThis(Rectangle r)
        {
            if (r != null)
            {
                return r.Clone() as Rectangle;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Clone a color
        /// </summary>
        /// <param name="toClone">color source</param>
        /// <returns>a new color with the same value than the color source</returns>
        public static CSSColor CloneThis(CSSColor toClone)
        {
            if (toClone != null)
            {
                return toClone.Clone() as CSSColor;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Clone anything else
        /// </summary>
        /// <param name="d">object source</param>
        /// <returns>cloned object</returns>
        public static dynamic CloneThis(dynamic d)
        {
            if (d != null)
            {
                return (d as ICloneable).Clone();
            }
            else
            {
                return null;
            }
        }

    }
}
