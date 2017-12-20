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
    [Serializable]
    public class CodeCSS : ICloneable
    {
        private string ids;
        private string backgroundImageURL;
        private CSSColor backgroundColor;
        private CSSColor borderLeftColor;
        private CSSColor borderRightColor;
        private CSSColor borderTopColor;
        private CSSColor borderBottomColor;
        private CSSColor foregroundColor;
        private Rectangle padding = new Rectangle();
        private Rectangle margin = new Rectangle();
        private Rectangle border = new Rectangle();
        private NameValueCollection body = new NameValueCollection();

        public CodeCSS(CodeCSS code)
        {
            this.ids = ExtensionMethods.CloneThis(code.ids);
            this.backgroundImageURL = ExtensionMethods.CloneThis(code.backgroundImageURL);
            this.backgroundColor = ExtensionMethods.CloneThis(code.backgroundColor);
            this.borderLeftColor = ExtensionMethods.CloneThis(code.borderLeftColor);
            this.borderRightColor = ExtensionMethods.CloneThis(code.borderRightColor);
            this.borderTopColor = ExtensionMethods.CloneThis(code.borderTopColor);
            this.borderBottomColor = ExtensionMethods.CloneThis(code.borderBottomColor);
            this.foregroundColor = ExtensionMethods.CloneThis(code.foregroundColor);
            this.padding = ExtensionMethods.CloneThis(code.padding);
            this.margin = ExtensionMethods.CloneThis(code.margin);
            this.border = ExtensionMethods.CloneThis(code.border);
            foreach (string key in code.body.AllKeys)
            {
                this.body.Add(key, code.body[key]);
            }
        }

        public CodeCSS(string id) { this.ids = id; }

        public CodeCSS() { this.ids = "#body"; }

        public string Ids
        {
            get { return this.ids; }
            set { this.ids = value.Trim(); }
        }

        public NameValueCollection Body
        {
            get { return this.body; }
        }

        public Rectangle Padding
        {
            get { return this.padding; }
            set { this.padding = value; }
        }

        public Rectangle Margin
        {
            get { return this.margin; }
            set { this.margin = value; }
        }

        public Rectangle Border
        {
            get { return this.border; }
            set { this.border = value; }
        }

        public string BackgroundImageURL
        {
            get { return this.backgroundImageURL; }
            set { this.backgroundImageURL = value; }
        }

        public CSSColor BackgroundColor
        {
            get { return this.backgroundColor; }
            set { this.backgroundColor = value; }
        }

        public CSSColor ForegroundColor
        {
            get { return this.foregroundColor; }
            set { this.foregroundColor = value; }
        }

        public CSSColor BorderLeftColor
        {
            get { return this.borderLeftColor; }
            set { this.borderLeftColor = value; }
        }
        public CSSColor BorderRightColor
        {
            get { return this.borderRightColor; }
            set { this.borderRightColor = value; }
        }
        public CSSColor BorderTopColor
        {
            get { return this.borderTopColor; }
            set { this.borderTopColor = value; }
        }
        public CSSColor BorderBottomColor
        {
            get { return this.borderBottomColor; }
            set { this.borderBottomColor = value; }
        }

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

        public void Discret(string name, string value)
        {
            try
            {
                if (name.StartsWith("padding"))
                {
                    if (name.CompareTo("padding-left") == 0)
                    {
                        this.padding.Set(Rectangle.leftName, value);
                    }
                    else if (name.CompareTo("padding-right") == 0)
                    {
                        this.padding.Set(Rectangle.rightName, value);
                    }
                    else if (name.CompareTo("padding-top") == 0)
                    {
                        this.padding.Set(Rectangle.topName, value);
                    }
                    else if (name.CompareTo("padding-bottom") == 0)
                    {
                        this.padding.Set(Rectangle.bottomName, value);
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
                        this.border.Set(Rectangle.leftName, value);
                    }
                    else if (name.CompareTo("border-right-width") == 0)
                    {
                        this.border.Set(Rectangle.rightName, value);
                    }
                    else if (name.CompareTo("border-top-width") == 0)
                    {
                        this.border.Set(Rectangle.topName, value);
                    }
                    else if (name.CompareTo("border-bottom-width") == 0)
                    {
                        this.border.Set(Rectangle.bottomName, value);
                    }
                    else if (name.CompareTo("border-left-color") == 0)
                    {
                        this.borderLeftColor = new CSSColor(value);
                    }
                    else if (name.CompareTo("border-right-color") == 0)
                    {
                        this.borderRightColor = new CSSColor(value);
                    }
                    else if (name.CompareTo("border-top-color") == 0)
                    {
                        this.borderTopColor = new CSSColor(value);
                    }
                    else if (name.CompareTo("border-bottom-color") == 0)
                    {
                        this.borderBottomColor = new CSSColor(value);
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
                        this.margin.Set(Rectangle.leftName, value);
                    }
                    else if (name.CompareTo("margin-right") == 0)
                    {
                        this.margin.Set(Rectangle.rightName, value);
                    }
                    else if (name.CompareTo("margin-top") == 0)
                    {
                        this.margin.Set(Rectangle.topName, value);
                    }
                    else if (name.CompareTo("margin-bottom") == 0)
                    {
                        this.margin.Set(Rectangle.bottomName, value);
                    }
                    else
                    {
                        this.AddIntoBody(name, value);
                    }
                }
                else if (name.CompareTo("background-color") == 0)
                {
                    this.backgroundColor = new CSSColor(value);
                }
                else if (name.CompareTo("color") == 0)
                {
                    this.foregroundColor = new CSSColor(value);
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

        public bool IsBodyKey(string key)
        {
            return this.body.AllKeys.Contains(key);
        }

        public string GenerateCSS(bool addDefaultKeys, bool displayId, bool resolveConfig=false)
        {
            string output = String.Empty;
            string data = String.Empty;
            if ((displayId && !String.IsNullOrEmpty(this.ids)) || !displayId)
            {
                if (addDefaultKeys)
                {
                    if (!this.padding.IsEmpty() && !this.IsBodyKey("padding"))
                    {
                        if (!this.IsBodyKey("padding-left")) data += "padding-left:" + this.padding.Left.ToString() + "px;" + Environment.NewLine;
                        if (!this.IsBodyKey("padding-right")) data += "padding-right:" + this.padding.Right.ToString() + "px;" + Environment.NewLine;
                        if (!this.IsBodyKey("padding-top")) data += "padding-top:" + this.padding.Top.ToString() + "px;" + Environment.NewLine;
                        if (!this.IsBodyKey("padding-bottom")) data += "padding-bottom:" + this.padding.Bottom.ToString() + "px;" + Environment.NewLine;
                    }
                    if (!this.margin.IsEmpty() && !this.IsBodyKey("margin"))
                    {
                        if (!this.IsBodyKey("margin-left")) data += "margin-left:" + this.margin.Left.ToString() + "px;" + Environment.NewLine;
                        if (!this.IsBodyKey("margin-right")) data += "margin-right:" + this.margin.Right.ToString() + "px;" + Environment.NewLine;
                        if (!this.IsBodyKey("margin-top")) data += "margin-top:" + this.margin.Top.ToString() + "px;" + Environment.NewLine;
                        if (!this.IsBodyKey("margin-bottom")) data += "margin-bottom:" + this.margin.Bottom.ToString() + "px;" + Environment.NewLine;
                    }
                    if (!this.border.IsEmpty() && !this.IsBodyKey("border"))
                    {
                        if (!this.IsBodyKey("border-style")) data += "border-style:solid;" + Environment.NewLine;
                        if (!this.IsBodyKey("border-left-width")) data += "border-left-width:" + this.border.Left.ToString() + "px;" + Environment.NewLine;
                        if (!this.IsBodyKey("border-right-width")) data += "border-right-width:" + this.border.Right.ToString() + "px;" + Environment.NewLine;
                        if (!this.IsBodyKey("border-top-width")) data += "border-top-width:" + this.border.Top.ToString() + "px;" + Environment.NewLine;
                        if (!this.IsBodyKey("border-bottom-width")) data += "border-bottom-width:" + this.border.Bottom.ToString() + "px;" + Environment.NewLine;
                    }
                    if (!String.IsNullOrEmpty(this.backgroundImageURL))
                        if (!this.IsBodyKey("background-image")) { data += "background-image:url('" + this.backgroundImageURL + "');" + Environment.NewLine; }
                    if (!CSSColor.TryEmpty(this.backgroundColor))
                        if (!this.IsBodyKey("background-color")) 
                            if (this.backgroundColor.Color.A != 255)
                                data += "background-color:rgba(" + this.backgroundColor.Color.R.ToString() + "," + this.backgroundColor.Color.G.ToString() + "," + this.backgroundColor.Color.B.ToString() + "," + (this.backgroundColor.Color.A / 255.0).ToString() + ");" + Environment.NewLine;
                            else
                            data += "background-color:rgb(" + this.backgroundColor.Color.R.ToString() + "," + this.backgroundColor.Color.G.ToString() + "," + this.backgroundColor.Color.B.ToString() + ");" + Environment.NewLine;
                    if (!CSSColor.TryEmpty(this.BorderLeftColor))
                        if (!this.IsBodyKey("border-left-color"))
                            if (this.borderLeftColor.Color.A != 255)
                                data += "border-left-color:rgba(" + this.borderLeftColor.Color.R.ToString() + "," + this.borderLeftColor.Color.G.ToString() + "," + this.borderLeftColor.Color.B.ToString() + "," + (this.borderLeftColor.Color.A / 255.0).ToString() + ");" + Environment.NewLine;
                            else
                                data += "border-left-color:rgb(" + this.borderLeftColor.Color.R.ToString() + "," + this.borderLeftColor.Color.G.ToString() + "," + this.borderLeftColor.Color.B.ToString() + ");" + Environment.NewLine;
                    if (!CSSColor.TryEmpty(this.BorderRightColor))
                        if (!this.IsBodyKey("border-right-color"))
                            if (this.borderRightColor.Color.A != 255)
                                data += "border-right-color:rgba(" + this.borderRightColor.Color.R.ToString() + "," + this.borderRightColor.Color.G.ToString() + "," + this.borderRightColor.Color.B.ToString() + "," + (this.borderRightColor.Color.A / 255.0).ToString() + ");" + Environment.NewLine;
                            else
                                data += "border-right-color:rgb(" + this.borderRightColor.Color.R.ToString() + "," + this.borderRightColor.Color.G.ToString() + "," + this.borderRightColor.Color.B.ToString() + ");" + Environment.NewLine;
                    if (!CSSColor.TryEmpty(this.BorderTopColor))
                        if (!this.IsBodyKey("border-top-color"))
                            if (this.borderTopColor.Color.A != 255)
                                data += "border-top-color:rgba(" + this.borderTopColor.Color.R.ToString() + "," + this.borderTopColor.Color.G.ToString() + "," + this.borderTopColor.Color.B.ToString() + "," + (this.borderTopColor.Color.A / 255.0).ToString() + ");" + Environment.NewLine;
                            else
                                data += "border-top-color:rgb(" + this.borderTopColor.Color.R.ToString() + "," + this.borderTopColor.Color.G.ToString() + "," + this.borderTopColor.Color.B.ToString() + ");" + Environment.NewLine;
                    if (!CSSColor.TryEmpty(this.BorderBottomColor))
                        if (!this.IsBodyKey("border-bottom-color"))
                            if (this.borderBottomColor.Color.A != 255)
                                data += "border-bottom-color:rgba(" + this.borderBottomColor.Color.R.ToString() + "," + this.borderBottomColor.Color.G.ToString() + "," + this.borderBottomColor.Color.B.ToString() + "," + (this.borderBottomColor.Color.A / 255.0).ToString() + ");" + Environment.NewLine;
                            else
                                data += "border-bottom-color:rgb(" + this.borderBottomColor.Color.R.ToString() + "," + this.borderBottomColor.Color.G.ToString() + "," + this.borderBottomColor.Color.B.ToString() + ");" + Environment.NewLine;
                    if (!CSSColor.TryEmpty(this.foregroundColor))
                        if (!this.IsBodyKey("color"))
                            if (this.foregroundColor.Color.A != 255)
                                data += "color:rgba(" + this.foregroundColor.Color.R.ToString() + "," + this.foregroundColor.Color.G.ToString() + "," + this.foregroundColor.Color.B.ToString() + "," + (this.foregroundColor.Color.A / 255.0).ToString() + ");" + Environment.NewLine;
                            else
                                data += "color:rgb(" + this.foregroundColor.Color.R.ToString() + "," + this.foregroundColor.Color.G.ToString() + "," + this.foregroundColor.Color.B.ToString() + ");" + Environment.NewLine;
                }
                foreach (string key in body.AllKeys)
                {
                    if (!String.IsNullOrEmpty(body[key]))
                    {
                        data += key + ":" + body[key] + ";" + Environment.NewLine;
                    }
                }
                if (displayId)
                    output += this.ids + "{" + Environment.NewLine;
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

        public object Clone()
        {
            return new CodeCSS(this);
        }
    }
}
