using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library
{
    [Serializable]
    public class HTMLTool: IProjectElement, IGenerateDesign, ICloneable
    {
        private EnumConstraint constraintWidth;
        private EnumConstraint constraintHeight;
        private uint width;
        private uint height;
        private string title;
        private string path;
        private string name = "tool" + Project.CurrentProject.IncrementedCounter.ToString();
        private string id = "idTool" + Project.CurrentProject.IncrementedCounter.ToString();
        private string html;
        private CodeJavaScript javascript = new CodeJavaScript();
        private CodeJavaScript javascriptOnLoad = new CodeJavaScript();
        private CodeCSS css = new CodeCSS();
        private List<CodeCSS> cssAdditional = new List<CodeCSS>();

        public HTMLTool()
        {
            this.css.Ids = "#" + this.id;
        }

        public EnumConstraint ConstraintWidth
        {
            get { return this.constraintWidth; }
            set { this.constraintWidth = value; }
        }

        public EnumConstraint ConstraintHeight
        {
            get { return this.constraintHeight; }
            set { this.constraintHeight = value; }
        }

        public uint Width
        {
            get { return this.width; }
            set { this.width = value; }
        }

        public uint HtmlWidth
        {
            get { return (uint)(this.width - this.css.Padding.Left - this.css.Padding.Right); }
        }

        public uint Height
        {
            get { return this.height; }
            set { this.height = value; }
        }

        public uint HtmlHeight
        {
            get { return (uint)(this.height - this.css.Padding.Top - this.css.Padding.Bottom); }
        }

        public string SizeString
        {
            get { return this.width.ToString() + "x" + this.height.ToString(); }
        }

        public string Title
        {
            get { return this.title; }
            set { this.title = value; }
        }

        public string Id
        {
            get { return this.id; }
            set { this.id = value; }
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public string Path
        {
            get { return this.path; }
            set { this.path = value; }
        }

        public string HTML
        {
            get { return this.html; }
            set { this.html = value; string result = this.GeneratedHTML; }
        }

        public string GeneratedHTML
        {
            get { return Project.CurrentProject.Configuration.Replace(this.html); }
        }

        public CodeJavaScript JavaScript
        {
            get { return this.javascript; }
        }

        public string JavaScriptSource
        {
            get { return this.javascript.Code; }
            set { this.javascript.Code = value; }
        }

        public CodeJavaScript JavaScriptOnLoad
        {
            get { return this.javascriptOnLoad; }
        }

        public string JavaScriptOnLoadSource
        {
            get { return this.javascriptOnLoad.Code; }
            set { this.javascriptOnLoad.Code = value; }
        }

        public CodeCSS CSS
        {
            get { return this.css; }
        }

        public List<CodeCSS> CSSAdditional
        {
            get { return this.cssAdditional; }
            set { this.cssAdditional = value; }
        }

        public void ImportCSS(CodeCSS css)
        {
            this.css = css.Clone() as CodeCSS;
        }

        public string CSSOutput(bool resolveConfig)
        {
            string output = this.CSS.GenerateCSS(false, true, resolveConfig) + Environment.NewLine;
            List<string> list = this.CSSAdditional.ConvertAll(a => { return a.GenerateCSS(true, true, resolveConfig) + Environment.NewLine; });
            if (list.Count() > 0) output += list.Aggregate((a, b) => a + b);
            return output;
        }

        public OutputHTML GenerateDesign()
        {
            return Routines.GenerateDesignTool(this);
        }

        public OutputHTML GenerateDesign(Page refPage)
        {
            throw new NotImplementedException();
        }

        public OutputHTML GenerateDesign(Page refPage, MasterPage masterRefPage, ParentConstraint parentConstraint)
        {
            throw new NotImplementedException();
        }

        public OutputHTML GenerateDesign(Page refPage, List<MasterObject> objects, ParentConstraint parentConstraint)
        {
            throw new NotImplementedException();
        }

        public OutputHTML GenerateDesign(Page refPage, MasterPage masterRefPage, List<MasterObject> objects, ParentConstraint parentConstraint)
        {
            throw new NotImplementedException();
        }

        public OutputHTML GenerateThumbnail()
        {
            throw new NotImplementedException();
        }

        public object Clone()
        {
            HTMLTool tool = new HTMLTool();
            tool.constraintWidth = this.constraintWidth;
            tool.constraintHeight = this.constraintHeight;
            tool.width = this.width;
            tool.height = this.height;
            tool.title = ExtensionMethods.CloneThis(this.title);
            tool.path = ExtensionMethods.CloneThis(this.path);
            tool.html = ExtensionMethods.CloneThis(this.html);
            tool.javascript = this.javascript.Clone() as CodeJavaScript;
            tool.javascriptOnLoad = this.javascriptOnLoad.Clone() as CodeJavaScript;
            tool.css = new CodeCSS(this.css);
            tool.css.Ids = "#" + tool.id;
            tool.cssAdditional.AddRange(from CodeCSS c in this.cssAdditional select c.Clone() as CodeCSS);
            return tool;
        }

        public string TypeName
        {
            get { return "HTMLTool"; }
        }

        public string ElementTitle
        {
            get { return this.title; }
        }
    }
}
