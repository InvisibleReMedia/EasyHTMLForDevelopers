using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    internal static class ExtensionMethods
    {
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

        public static Folder CloneThis(Folder f)
        {
            if (f != null)
            {
                return f.Clone() as Folder;
            }
            else
            {
                return null;
            }
        }

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
