using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Library
{
    [Serializable]
    public class Rectangle : ICloneable
    {
        private int left;
        private int right;
        private int top;
        private int bottom;

        public static string leftName = "left";
        public static string rightName = "right";
        public static string topName = "top";
        public static string bottomName = "bottom";

        public Rectangle() { }

        public Rectangle(int left, int right, int top, int bottom)
        {
            this.left = left;
            this.right = right;
            this.top = top;
            this.bottom = bottom;
        }

        public Rectangle(string rectStr)
        {
            Rectangle r = new Rectangle();
            if (Rectangle.TryParse(rectStr, out r))
            {
                this.left = r.left;
                this.right = r.right;
                this.top = r.top;
                this.bottom = r.bottom;
            }
            else
            {
                throw new FormatException();
            }
        }

        public int Left
        {
            get { return this.left; }
            set { this.left = value; }
        }

        public int Right
        {
            get { return this.right; }
            set { this.right = value; }
        }

        public int Top
        {
            get { return this.top; }
            set { this.top = value; }
        }

        public int Bottom
        {
            get { return this.bottom; }
            set { this.bottom = value; }
        }

        private int ConvertToInt(string value)
        {
            Regex r = new Regex(@"([0-9]+)");
            Match m = r.Match(value);
            if (m.Success)
            {
                return Convert.ToInt16(m.Groups[1].Value);
            }
            else
            {
                throw new FormatException();
            }
        }

        public override string ToString()
        {
            return left.ToString() + "," + right.ToString() + "," + top.ToString() + "," + bottom.ToString();
        }

        public void Set(string type, string value)
        {
            if (type == Rectangle.leftName) { this.left = Convert.ToInt32(value); }
            else
                if (type == Rectangle.rightName) { this.right = Convert.ToInt32(value); }
                else
                    if (type == Rectangle.topName) { this.top = Convert.ToInt32(value); }
                    else
                        if (type == Rectangle.bottomName) { this.bottom = Convert.ToInt32(value); }
        }

        public static Rectangle operator +(Rectangle from, Rectangle plus)
        {
            from.left += plus.left;
            from.right += plus.right;
            from.top += plus.top;
            from.bottom += plus.bottom;
            return from;
        }

        public static bool TryParse(string s, out Rectangle rect)
        {
            rect = new Rectangle();
            Regex reg = new Regex(@"(#\([^)]+\))|(([+-]?\d+)[^,]*,([+-]?\d+)[^,]*,([+-]?\d+)[^,]*,([+-]?\d+)[^,]*)");
            Match m = reg.Match(s);
            if (m.Success)
            {
                if (m.Groups[1].Success)
                {
                    return false;
                }
                else
                {
                    int left, right, top, bottom;
                    if (!Int32.TryParse(m.Groups[3].Value, out left))
                    {
                        return false;
                    }
                    if (!Int32.TryParse(m.Groups[4].Value, out right))
                    {
                        return false;
                    }
                    if (!Int32.TryParse(m.Groups[5].Value, out top))
                    {
                        return false;
                    }
                    if (!Int32.TryParse(m.Groups[6].Value, out bottom))
                    {
                        return false;
                    }
                    rect = new Rectangle(left, right, top, bottom);
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        public bool IsEmpty()
        {
            return !(left != 0 || right != 0 || top != 0 || bottom != 0);
        }

        public object Clone()
        {
            return new Rectangle(this.left, this.right, this.top, this.bottom);
        }
    }

    [Serializable]
    public class SizedRectangle : Rectangle
    {
        #region Private Field
        private int width = 0;
        private int height = 0;
        #endregion

        #region Public Constructors
        public SizedRectangle() { }

        public SizedRectangle(int width, int height, int left, int right, int top, int bottom)
            : base(left, right, top, bottom)
        {
            this.width = width;
            this.height = height;
        }
        #endregion

        #region Public Properties
        public int Width
        {
            get { return this.width; }
        }

        public int Height
        {
            get { return this.height; }
        }
        #endregion
    }
}
