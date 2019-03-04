using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;

namespace Library
{
    /// <summary>
    /// Class to contain CSS keys
    /// </summary>
    [Serializable]
    public class CodeCSS : Marshalling.PersistentDataObject, ICloneable
    {

        #region Fields

        /// <summary>
        /// Index name for id
        /// Usefull to customize html tag
        /// </summary>
        protected static readonly string idsName = "ids";
        /// <summary>
        /// Index name for an url background image
        /// </summary>
        protected static readonly string backgroundImageUrlName = "backgroundUrl";
        /// <summary>
        /// Index name for a background color
        /// </summary>
        protected static readonly string backgroundColorName = "backgroundColor";
        /// <summary>
        /// Index name for a border left color
        /// </summary>
        protected static readonly string borderLeftColorName = "borderLeftColor";
        /// <summary>
        /// Index name for a border right color
        /// </summary>
        protected static readonly string borderRightColorName = "borderRightColor";
        /// <summary>
        /// Index name for a border top color
        /// </summary>
        protected static readonly string borderTopColorName = "borderTopColor";
        /// <summary>
        /// Index name for a border bottom color
        /// </summary>
        protected static readonly string borderBottomColorName = "borderBottomColor";
        /// <summary>
        /// Index name for a foreground color
        /// </summary>
        protected static readonly string foregroundColorName = "foregroundColor";
        /// <summary>
        /// Index name for a padding
        /// </summary>
        protected static readonly string paddingName = "padding";
        /// <summary>
        /// Index name for a margin
        /// </summary>
        protected static readonly string marginName = "margin";
        /// <summary>
        /// Index name for a border
        /// </summary>
        protected static readonly string borderName = "border";
        /// <summary>
        /// Index name for the body
        /// </summary>
        protected static readonly string bodyName = "body";

        #endregion

        #region Constructors

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="code">css source</param>
        public CodeCSS(CodeCSS code)
        {
            this.Ids = ExtensionMethods.CloneThis(code.Ids);
            this.BackgroundImageURL = ExtensionMethods.CloneThis(code.BackgroundImageURL);
            this.BackgroundColor = ExtensionMethods.CloneThis(code.BackgroundColor);
            this.BorderLeftColor = ExtensionMethods.CloneThis(code.BorderLeftColor);
            this.BorderRightColor = ExtensionMethods.CloneThis(code.BorderRightColor);
            this.BorderTopColor = ExtensionMethods.CloneThis(code.BorderTopColor);
            this.BorderBottomColor = ExtensionMethods.CloneThis(code.BorderBottomColor);
            this.ForegroundColor = ExtensionMethods.CloneThis(code.ForegroundColor);
            this.Padding = ExtensionMethods.CloneThis(code.Padding);
            this.Margin = ExtensionMethods.CloneThis(code.Margin);
            this.Border = ExtensionMethods.CloneThis(code.Border);
            foreach (string key in code.Body.AllKeys)
            {
                this.Body.Add(key, code.Body[key]);
            }
        }

        /// <summary>
        /// Constructor with a specific Id of html tag
        /// </summary>
        /// <param name="id">html tag id</param>
        public CodeCSS(string id) { this.Ids = id; }

        /// <summary>
        /// Default constructor (addressed for the body of page)
        /// </summary>
        public CodeCSS() { this.Ids = "body"; }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets Id command
        /// CSS v3 valid declaration with multiple id
        /// </summary>
        public string Ids
        {
            get { return this.Get(idsName); }
            set { this.Set(idsName, value.Trim()); }
        }

        /// <summary>
        /// Collection of CSS properties
        /// </summary>
        public NameValueCollection Body
        {
            get { return this.Get(bodyName, new NameValueCollection()); }
        }

        /// <summary>
        /// Gets or sets the padding rectangle
        /// </summary>
        public Rectangle Padding
        {
            get { return this.Get(paddingName, new Rectangle()); }
            set { this.Set(paddingName, value); }
        }

        /// <summary>
        /// Gets or sets margin rectangle
        /// </summary>
        public Rectangle Margin
        {
            get { return this.Get(marginName, new Rectangle()); }
            set { this.Set(marginName, value); }
        }

        /// <summary>
        /// Gets or sets border rectangle
        /// </summary>
        public Rectangle Border
        {
            get { return this.Get(borderName, new Rectangle()); }
            set { this.Set(borderName, value); }
        }

        /// <summary>
        /// Gets or sets the background url
        /// </summary>
        public string BackgroundImageURL
        {
            get { return this.Get(backgroundImageUrlName, ""); }
            set { this.Set(backgroundImageUrlName, value); }
        }

        /// <summary>
        /// Gets or sets the background color
        /// </summary>
        public CSSColor BackgroundColor
        {
            get { return this.Get(backgroundColorName, new CSSColor()); }
            set { this.Set(backgroundColorName, value); }
        }

        /// <summary>
        /// Gets or sets the foreground color
        /// </summary>
        public CSSColor ForegroundColor
        {
            get { return this.Get(foregroundColorName, new CSSColor()); }
            set { this.Set(foregroundColorName, value); }
        }

        /// <summary>
        /// Gets or sets the border left color
        /// </summary>
        public CSSColor BorderLeftColor
        {
            get { return this.Get(borderLeftColorName, new CSSColor()); }
            set { this.Set(borderLeftColorName, value); }
        }

        /// <summary>
        /// Gets or sets the border right color
        /// </summary>
        public CSSColor BorderRightColor
        {
            get { return this.Get(borderRightColorName, new CSSColor()); }
            set { this.Set(borderRightColorName, value); }
        }

        /// <summary>
        /// Gets or sets the border top color
        /// </summary>
        public CSSColor BorderTopColor
        {
            get { return this.Get(borderTopColorName, new CSSColor()); }
            set { this.Set(borderTopColorName, value); }
        }

        /// <summary>
        /// Gets or sets the border bottom color
        /// </summary>
        public CSSColor BorderBottomColor
        {
            get { return this.Get(borderBottomColorName, new CSSColor()); }
            set { this.Set(borderBottomColorName, value); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Add a property value to the CSS body
        /// </summary>
        /// <param name="name">property name</param>
        /// <param name="value">property value</param>
        public void AddIntoBody(string name, string value)
        {
            if (this.Body.AllKeys.Contains(name))
            {
                this.Body[name] = value;
            }
            else
            {
                this.Body.Add(name, value);
            }
        }

        /// <summary>
        /// Sets a property that can be border, padding, margin and colors
        /// </summary>
        /// <param name="name">property name</param>
        /// <param name="value">value to set</param>
        public void Discret(string name, string value)
        {
            try
            {
                if (name.StartsWith("padding"))
                {
                    if (name.CompareTo("padding-left") == 0)
                    {
                        this.Padding.Set(Rectangle.leftName, value);
                    }
                    else if (name.CompareTo("padding-right") == 0)
                    {
                        this.Padding.Set(Rectangle.rightName, value);
                    }
                    else if (name.CompareTo("padding-top") == 0)
                    {
                        this.Padding.Set(Rectangle.topName, value);
                    }
                    else if (name.CompareTo("padding-bottom") == 0)
                    {
                        this.Padding.Set(Rectangle.bottomName, value);
                    }
                    else
                    {
                        this.AddIntoBody(name, value);
                    }
                }
                else if (name.StartsWith("border"))
                {
                    if (name.CompareTo("border-left-width") == 0)
                    {
                        this.Border.Set(Rectangle.leftName, value);
                    }
                    else if (name.CompareTo("border-right-width") == 0)
                    {
                        this.Border.Set(Rectangle.rightName, value);
                    }
                    else if (name.CompareTo("border-top-width") == 0)
                    {
                        this.Border.Set(Rectangle.topName, value);
                    }
                    else if (name.CompareTo("border-bottom-width") == 0)
                    {
                        this.Border.Set(Rectangle.bottomName, value);
                    }
                    else if (name.CompareTo("border-left-color") == 0)
                    {
                        this.BorderLeftColor = new CSSColor(value);
                    }
                    else if (name.CompareTo("border-right-color") == 0)
                    {
                        this.BorderRightColor = new CSSColor(value);
                    }
                    else if (name.CompareTo("border-top-color") == 0)
                    {
                        this.BorderTopColor = new CSSColor(value);
                    }
                    else if (name.CompareTo("border-bottom-color") == 0)
                    {
                        this.BorderBottomColor = new CSSColor(value);
                    }
                    else
                    {
                        this.AddIntoBody(name, value);
                    }
                }
                else if (name.StartsWith("margin"))
                {
                    if (name.CompareTo("margin-left") == 0)
                    {
                        this.Margin.Set(Rectangle.leftName, value);
                    }
                    else if (name.CompareTo("margin-right") == 0)
                    {
                        this.Margin.Set(Rectangle.rightName, value);
                    }
                    else if (name.CompareTo("margin-top") == 0)
                    {
                        this.Margin.Set(Rectangle.topName, value);
                    }
                    else if (name.CompareTo("margin-bottom") == 0)
                    {
                        this.Margin.Set(Rectangle.bottomName, value);
                    }
                    else
                    {
                        this.AddIntoBody(name, value);
                    }
                }
                else if (name.CompareTo("background-color") == 0)
                {
                    this.BackgroundColor = new CSSColor(value);
                }
                else if (name.CompareTo("color") == 0)
                {
                    this.ForegroundColor = new CSSColor(value);
                }
                else
                {
                    this.AddIntoBody(name, value);
                }
            }
            catch (FormatException)
            {
                this.AddIntoBody(name, value);
            }
        }

        /// <summary>
        /// Test if a key exists into body
        /// </summary>
        /// <param name="key">key to search</param>
        /// <returns>true of false</returns>
        public bool IsBodyKey(string key)
        {
            return this.Body.AllKeys.Contains(key);
        }

        /// <summary>
        /// Generates the CSS code
        /// </summary>
        /// <param name="addDefaultKeys">add default keys</param>
        /// <param name="displayId">display id and print properties into</param>
        /// <param name="resolveConfig">transforms configuration keys to values</param>
        /// <returns>css string generated code output</returns>
        public string GenerateCSS(bool addDefaultKeys, bool displayId, bool resolveConfig=false)
        {
            string output = String.Empty;
            string data = String.Empty;
            if ((displayId && !String.IsNullOrEmpty(this.Ids)) || !displayId)
            {
                if (addDefaultKeys)
                {
                    if (!this.Padding.IsEmpty() && !this.IsBodyKey("padding"))
                    {
                        if (!this.IsBodyKey("padding-left")) data += "padding-left:" + this.Padding.Left.ToString() + "px;" + Environment.NewLine;
                        if (!this.IsBodyKey("padding-right")) data += "padding-right:" + this.Padding.Right.ToString() + "px;" + Environment.NewLine;
                        if (!this.IsBodyKey("padding-top")) data += "padding-top:" + this.Padding.Top.ToString() + "px;" + Environment.NewLine;
                        if (!this.IsBodyKey("padding-bottom")) data += "padding-bottom:" + this.Padding.Bottom.ToString() + "px;" + Environment.NewLine;
                    }
                    if (!this.Margin.IsEmpty() && !this.IsBodyKey("margin"))
                    {
                        if (!this.IsBodyKey("margin-left")) data += "margin-left:" + this.Margin.Left.ToString() + "px;" + Environment.NewLine;
                        if (!this.IsBodyKey("margin-right")) data += "margin-right:" + this.Margin.Right.ToString() + "px;" + Environment.NewLine;
                        if (!this.IsBodyKey("margin-top")) data += "margin-top:" + this.Margin.Top.ToString() + "px;" + Environment.NewLine;
                        if (!this.IsBodyKey("margin-bottom")) data += "margin-bottom:" + this.Margin.Bottom.ToString() + "px;" + Environment.NewLine;
                    }
                    if (!this.Border.IsEmpty() && !this.IsBodyKey("border"))
                    {
                        if (!this.IsBodyKey("border-style")) data += "border-style:solid;" + Environment.NewLine;
                        if (!this.IsBodyKey("border-left-width")) data += "border-left-width:" + this.Border.Left.ToString() + "px;" + Environment.NewLine;
                        if (!this.IsBodyKey("border-right-width")) data += "border-right-width:" + this.Border.Right.ToString() + "px;" + Environment.NewLine;
                        if (!this.IsBodyKey("border-top-width")) data += "border-top-width:" + this.Border.Top.ToString() + "px;" + Environment.NewLine;
                        if (!this.IsBodyKey("border-bottom-width")) data += "border-bottom-width:" + this.Border.Bottom.ToString() + "px;" + Environment.NewLine;
                    }
                    if (!String.IsNullOrEmpty(this.BackgroundImageURL))
                        if (!this.IsBodyKey("background-image")) { data += "background-image:url('" + this.BackgroundImageURL + "');" + Environment.NewLine; }
                    if (!this.BackgroundColor.IsEmpty)
                        if (!this.IsBodyKey("background-color"))
                            if (this.BackgroundColor.Color.A != 255)
                                data += "background-color:rgba(" + this.BackgroundColor.Color.R.ToString() + "," + this.BackgroundColor.Color.G.ToString() + "," + this.BackgroundColor.Color.B.ToString() + "," + (this.BackgroundColor.Color.A / 255.0).ToString() + ");" + Environment.NewLine;
                            else
                                data += "background-color:rgb(" + this.BackgroundColor.Color.R.ToString() + "," + this.BackgroundColor.Color.G.ToString() + "," + this.BackgroundColor.Color.B.ToString() + ");" + Environment.NewLine;
                    if (!this.BorderLeftColor.IsEmpty)
                        if (!this.IsBodyKey("border-left-color"))
                            if (this.BorderLeftColor.Color.A != 255)
                                data += "border-left-color:rgba(" + this.BorderLeftColor.Color.R.ToString() + "," + this.BorderLeftColor.Color.G.ToString() + "," + this.BorderLeftColor.Color.B.ToString() + "," + (this.BorderLeftColor.Color.A / 255.0).ToString() + ");" + Environment.NewLine;
                            else
                                data += "border-left-color:rgb(" + this.BorderLeftColor.Color.R.ToString() + "," + this.BorderLeftColor.Color.G.ToString() + "," + this.BorderLeftColor.Color.B.ToString() + ");" + Environment.NewLine;
                    if (!this.BorderRightColor.IsEmpty)
                        if (!this.IsBodyKey("border-right-color"))
                            if (this.BorderRightColor.Color.A != 255)
                                data += "border-right-color:rgba(" + this.BorderRightColor.Color.R.ToString() + "," + this.BorderRightColor.Color.G.ToString() + "," + this.BorderRightColor.Color.B.ToString() + "," + (this.BorderRightColor.Color.A / 255.0).ToString() + ");" + Environment.NewLine;
                            else
                                data += "border-right-color:rgb(" + this.BorderRightColor.Color.R.ToString() + "," + this.BorderRightColor.Color.G.ToString() + "," + this.BorderRightColor.Color.B.ToString() + ");" + Environment.NewLine;
                    if (!this.BorderTopColor.IsEmpty)
                        if (!this.IsBodyKey("border-top-color"))
                            if (this.BorderTopColor.Color.A != 255)
                                data += "border-top-color:rgba(" + this.BorderTopColor.Color.R.ToString() + "," + this.BorderTopColor.Color.G.ToString() + "," + this.BorderTopColor.Color.B.ToString() + "," + (this.BorderTopColor.Color.A / 255.0).ToString() + ");" + Environment.NewLine;
                            else
                                data += "border-top-color:rgb(" + this.BorderTopColor.Color.R.ToString() + "," + this.BorderTopColor.Color.G.ToString() + "," + this.BorderTopColor.Color.B.ToString() + ");" + Environment.NewLine;
                    if (!this.BorderBottomColor.IsEmpty)
                        if (!this.IsBodyKey("border-bottom-color"))
                            if (this.BorderBottomColor.Color.A != 255)
                                data += "border-bottom-color:rgba(" + this.BorderBottomColor.Color.R.ToString() + "," + this.BorderBottomColor.Color.G.ToString() + "," + this.BorderBottomColor.Color.B.ToString() + "," + (this.BorderBottomColor.Color.A / 255.0).ToString() + ");" + Environment.NewLine;
                            else
                                data += "border-bottom-color:rgb(" + this.BorderBottomColor.Color.R.ToString() + "," + this.BorderBottomColor.Color.G.ToString() + "," + this.BorderBottomColor.Color.B.ToString() + ");" + Environment.NewLine;
                    if (!this.ForegroundColor.IsEmpty)
                        if (!this.IsBodyKey("color"))
                            if (this.ForegroundColor.Color.A != 255)
                                data += "color:rgba(" + this.ForegroundColor.Color.R.ToString() + "," + this.ForegroundColor.Color.G.ToString() + "," + this.ForegroundColor.Color.B.ToString() + "," + (this.ForegroundColor.Color.A / 255.0).ToString() + ");" + Environment.NewLine;
                            else
                                data += "color:rgb(" + this.ForegroundColor.Color.R.ToString() + "," + this.ForegroundColor.Color.G.ToString() + "," + this.ForegroundColor.Color.B.ToString() + ");" + Environment.NewLine;
                }
                foreach (string key in Body.AllKeys)
                {
                    if (!String.IsNullOrEmpty(Body[key]))
                    {
                        data += key + ":" + Body[key] + ";" + Environment.NewLine;
                    }
                }
                if (displayId)
                    output += this.Ids + "{" + Environment.NewLine;
                if (addDefaultKeys || !String.IsNullOrEmpty(data))
                {
                    output += data;
                }
                if (displayId)
                    output += "}" + Environment.NewLine;
            }
            if (resolveConfig)
                return Project.CurrentProject.Configuration.Replace(output);
            else
                return output;
        }

        /// <summary>
        /// Clone this object
        /// </summary>
        /// <returns>cloned object</returns>
        public object Clone()
        {
            return new CodeCSS(this);
        }

        #endregion
    }
}
