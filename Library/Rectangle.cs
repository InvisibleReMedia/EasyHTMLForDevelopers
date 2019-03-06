using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Library
{
    /// <summary>
    /// Class for defining a rectangle
    /// </summary>
    [Serializable]
    public class Rectangle : Marshalling.PersistentDataObject, ICloneable
    {

        #region Fields

        /// <summary>
        /// Index name of left value
        /// </summary>
        public static readonly string leftName = "left";
        /// <summary>
        /// Index name of right value
        /// </summary>
        public static readonly string rightName = "right";
        /// <summary>
        /// Index name of top value
        /// </summary>
        public static readonly string topName = "top";
        /// <summary>
        /// Index name of bottom value
        /// </summary>
        public static readonly string bottomName = "bottom";


        #endregion

        #region Constructors

        /// <summary>
        /// Empty constructor
        /// </summary>
        public Rectangle() { }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="left">left value</param>
        /// <param name="right">right value</param>
        /// <param name="top">top value</param>
        /// <param name="bottom">bottom value</param>
        public Rectangle(int left, int right, int top, int bottom)
        {
            this.Set(leftName, left);
            this.Set(rightName, right);
            this.Set(topName, top);
            this.Set(bottomName, bottom);
        }

        /// <summary>
        /// Constructor from a string
        /// </summary>
        /// <param name="rectStr">string representation of a rectangle</param>
        public Rectangle(string rectStr)
        {
            Rectangle r = new Rectangle();
            if (Rectangle.TryParse(rectStr, out r))
            {
                this.Set(leftName, r.Left);
                this.Set(rightName, r.Right);
                this.Set(topName, r.Top);
                this.Set(bottomName, r.Bottom);
            }
            else
            {
                throw new FormatException();
            }
        }

        #endregion

        /// <summary>
        /// Gets or sets the left value
        /// </summary>
        public int Left
        {
            get { return this.Get(leftName, 0); }
            set { this.Set(leftName, value); }
        }

        /// <summary>
        /// Gets or sets the right value
        /// </summary>
        public int Right
        {
            get { return this.Get(rightName, 0); }
            set { this.Set(rightName, value); }
        }

        /// <summary>
        /// Gets or sets the top value
        /// </summary>
        public int Top
        {
            get { return this.Get(topName, 0); }
            set { this.Set(topName, value); }
        }

        /// <summary>
        /// Gets or sets the bottom value
        /// </summary>
        public int Bottom
        {
            get { return this.Get(bottomName, 0); }
            set { this.Set(bottomName, value); }
        }

        /// <summary>
        /// Convert value string to an int
        /// </summary>
        /// <param name="value">value string</param>
        /// <returns>int</returns>
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

        /// <summary>
        /// Gets rectangle to string representation
        /// </summary>
        /// <returns>string representation</returns>
        public override string ToString()
        {
            return this.Left.ToString() + "," + this.Right.ToString() + "," + this.Top.ToString() + "," + this.Bottom.ToString();
        }

        /// <summary>
        /// Set an adapted value
        /// given the value name
        /// </summary>
        /// <param name="type">type name</param>
        /// <param name="value">string value</param>
        public void Set(string type, string value)
        {
            if (type == Rectangle.leftName) { this.Left = Convert.ToInt32(value); }
            else
                if (type == Rectangle.rightName) { this.Right = Convert.ToInt32(value); }
                else
                    if (type == Rectangle.topName) { this.Top = Convert.ToInt32(value); }
                    else
                        if (type == Rectangle.bottomName) { this.Bottom = Convert.ToInt32(value); }
        }

        /// <summary>
        /// Operator to sum rectangle with an another
        /// </summary>
        /// <param name="from">rectangle source and destination</param>
        /// <param name="plus">rectangle to size</param>
        /// <returns>rectangle modified</returns>
        public static Rectangle operator +(Rectangle from, Rectangle plus)
        {
            from.Left += plus.Left;
            from.Right += plus.Right;
            from.Top += plus.Top;
            from.Bottom += plus.Bottom;
            return from;
        }

        /// <summary>
        /// Try parsing a rect string
        /// </summary>
        /// <param name="s">string</param>
        /// <param name="rect">rectangle out</param>
        /// <returns>true if parse ok</returns>
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

        /// <summary>
        /// Test if an empty rectangle
        /// </summary>
        /// <returns>true if empty</returns>
        public bool IsEmpty()
        {
            return !(Left != 0 || Right != 0 || Top != 0 || Bottom != 0);
        }

        /// <summary>
        /// Clone this rectangle
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return new Rectangle(this.Left, this.Right, this.Top, this.Bottom);
        }
    }

    /// <summary>
    /// A rectangle with its size in width and height
    /// </summary>
    [Serializable]
    public class SizedRectangle : Rectangle
    {

        #region Private Field

        /// <summary>
        /// Index name of width value
        /// </summary>
        protected static readonly string widthName = "width";
        /// <summary>
        /// Index name of height value
        /// </summary>
        protected static readonly string heightName = "height";

        #endregion

        #region Public Constructors

        /// <summary>
        /// Empty constructor
        /// </summary>
        public SizedRectangle() { }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="width">width</param>
        /// <param name="height">height</param>
        /// <param name="left">left corner position</param>
        /// <param name="top">top corner position</param>
        public SizedRectangle(int width, int height, int left, int top)
            : base(left, 0, top, 0)
        {
            this.Set(widthName, width);
            this.Set(heightName, height);
        }
        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the width value
        /// </summary>
        public int Width
        {
            get { return this.Get(widthName, 0); }
            set { this.Set(widthName, value); }
        }

        /// <summary>
        /// Gets the height value
        /// </summary>
        public int Height
        {
            get { return this.Get(heightName, 0); }
            set { this.Set(heightName, value); }
        }

        #endregion

        #region Overriden Methods

        /// <summary>
        /// Gets SizedRectangle to string representation
        /// </summary>
        /// <returns>string representation</returns>
        public override string ToString()
        {
            return this.Width.ToString() + "," + this.Height.ToString() + "," + 
                   this.Left.ToString() + "," + this.Top.ToString();
        }

        #endregion

    }

    /// <summary>
    /// An area sized Rectangle
    /// </summary>
    [Serializable]
    public class AreaSizedRectangle : SizedRectangle
    {

        #region Private Field

        /// <summary>
        /// Index name of width value
        /// </summary>
        protected static readonly string countWidthName = "CountWidth";
        /// <summary>
        /// Index name of start width value
        /// </summary>
        protected static readonly string startWidthName = "startWidth";
        /// <summary>
        /// Index name of height value
        /// </summary>
        protected static readonly string countHeightName = "CountHeight";
        /// <summary>
        /// Index name of start height value
        /// </summary>
        protected static readonly string startHeightName = "startHeight";

        #endregion

        #region Public Constructors

        /// <summary>
        /// Empty constructor
        /// </summary>
        public AreaSizedRectangle() { }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="width">width</param>
        /// <param name="height">height</param>
        /// <param name="countWidth">width count</param>
        /// <param name="countHeight">height count</param>
        /// <param name="left">left corner position</param>
        /// <param name="top">top corner position</param>
        public AreaSizedRectangle(int width, int height, int countWidth, int countHeight, int left, int top)
            : base(width, height, left, top)
        {
            this.Set(countWidthName, countWidth);
            this.Set(countHeightName, countHeight);
        }
        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the count width value
        /// </summary>
        public int CountWidth
        {
            get { return this.Get(countWidthName, 0); }
            set { this.Set(countWidthName, value); }
        }

        /// <summary>
        /// Gets the start width value
        /// </summary>
        public int StartWidth
        {
            get { return this.Get(startWidthName, 0); }
            set { this.Set(startWidthName, value); }
        }

        /// <summary>
        /// Gets the count height value
        /// </summary>
        public int CountHeight
        {
            get { return this.Get(countHeightName, 0); }
            set { this.Set(countHeightName, value); }
        }

        /// <summary>
        /// Gets the start height value
        /// </summary>
        public int StartHeight
        {
            get { return this.Get(startHeightName, 0); }
            set { this.Set(startHeightName, value); }
        }


        #endregion

        #region Methods

        /// <summary>
        /// Resize a rectangle
        /// </summary>
        /// <param name="width">new division width</param>
        /// <param name="height">new division height</param>
        public void Resize(double width, double height)
        {
            this.Width = Convert.ToInt32(width * this.CountWidth);
            this.Height = Convert.ToInt32(height * this.CountHeight);
            this.Left = Convert.ToInt32(width * this.StartWidth);
            this.Top = Convert.ToInt32(height * this.StartHeight);
            this.Right = this.Left + this.Width;
            this.Bottom = this.Top + this.Height;

        }

        #endregion

        #region Overriden Methods

        /// <summary>
        /// Gets SizedRectangle to string representation
        /// </summary>
        /// <returns>string representation</returns>
        public override string ToString()
        {
            return this.CountWidth.ToString() + "," + this.CountHeight.ToString() + "," +
                   this.Width.ToString() + "," + this.Height.ToString() + "," +
                   this.Left.ToString() + "," + this.Top.ToString() + "," +
                   this.StartWidth.ToString() + "," + this.StartHeight.ToString();
        }

        #endregion
    }
}
