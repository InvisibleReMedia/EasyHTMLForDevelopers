using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library
{
    /// <summary>
    /// HTML tool : a tool is simply a host for any HTML code
    /// </summary>
    [Serializable]
    public class HTMLTool : Marshalling.PersistentDataObject, IProjectElement, IGenerateDesign, ICloneable
    {
        #region Fields

        /// <summary>
        /// Index name for unique id
        /// </summary>
        protected static readonly string uniqueName = "unique";
        /// <summary>
        /// Index name for width constraint
        /// </summary>
        protected static readonly string constraintWidthName = "constraintWidth";
        /// <summary>
        /// Index name for height constraint
        /// </summary>
        protected static readonly string constraintHeightName = "constraintHeight";
        /// <summary>
        /// Index name for width value
        /// </summary>
        protected static readonly string widthName = "width";
        /// <summary>
        /// Index name for height value
        /// </summary>
        protected static readonly string heightName = "height";
        /// <summary>
        /// Index name for path
        /// </summary>
        protected static readonly string pathName = "path";
        /// <summary>
        /// Index name for title
        /// </summary>
        protected static readonly string titleName = "title";
        /// <summary>
        /// Index name for automatic name
        /// </summary>
        protected static readonly string automaticNameName = "automaticName";
        /// <summary>
        /// Index name for automatic id
        /// </summary>
        protected static readonly string automaticIdName = "automaticId";
        /// <summary>
        /// Index name for HTML content
        /// </summary>
        protected static readonly string HTMLContentName = "htmlContent";
        /// <summary>
        /// Index name for events
        /// </summary>
        protected static readonly string eventsName = "events";
        /// <summary>
        /// Index name for javascript code
        /// </summary>
        protected static readonly string javascriptName = "javascript";
        /// <summary>
        /// Index name for javascript on load code
        /// </summary>
        protected static readonly string javascriptOnloadName = "javascriptOnload";
        /// <summary>
        /// Index name for additional css
        /// </summary>
        protected static readonly string additionalCssName = "additionalCss";
        /// <summary>
        /// Index name for css list
        /// </summary>
        protected static readonly string cssListName = "cssList";

        #endregion

        #region Constructor

        /// <summary>
        /// Empty constructor
        /// </summary>
        public HTMLTool()
        {
            int val = Project.CurrentProject.IncrementedCounter;
            this.Set(automaticNameName, String.Format("tool{0}", val));
            this.Set(automaticIdName, String.Format("idTool{0}", val));
            this.CSS.Ids = "#" + this.Id;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the unique id
        /// </summary>
        public string Unique
        {
            get { return this.Get(uniqueName); }
            set { this.Set(uniqueName, value); }
        }

        /// <summary>
        /// Gets or sets the width constraint 
        /// </summary>
        public EnumConstraint ConstraintWidth
        {
            get { return this.Get(constraintWidthName, EnumConstraint.AUTO); }
            set { this.Set(constraintHeightName, value); }
        }

        /// <summary>
        /// Gets or sets height constraint
        /// </summary>
        public EnumConstraint ConstraintHeight
        {
            get { return this.Get(constraintHeightName, EnumConstraint.AUTO); }
            set { this.Set(constraintHeightName, value); }
        }

        /// <summary>
        /// Gets or sets the width value
        /// </summary>
        public uint Width
        {
            get { return this.Get(widthName, 0u); }
            set { this.Set(widthName, value); }
        }

        /// <summary>
        /// Gets inner box width
        /// empty padding css left and right
        /// </summary>
        public uint HtmlWidth
        {
            get { return (uint)(this.Width - this.CSS.Padding.Left - this.CSS.Padding.Right); }
        }

        /// <summary>
        /// Gets or sets the height value
        /// </summary>
        public uint Height
        {
            get { return this.Get(heightName, 0u); }
            set { this.Set(heightName, value); }
        }

        /// <summary>
        /// Gets inner box height
        /// empty padding css top and bottom
        /// </summary>
        public uint HtmlHeight
        {
            get { return (uint)(this.Height - this.CSS.Padding.Top - this.CSS.Padding.Bottom); }
        }

        /// <summary>
        /// Gets the size string for dumping mode
        /// </summary>
        public string SizeString
        {
            get { return this.Width.ToString() + "x" + this.Height.ToString(); }
        }

        /// <summary>
        /// Gets or sets the title of this master object
        /// </summary>
        public string Title
        {
            get { return this.Get(titleName, ""); }
            set { this.Set(titleName, value); }
        }

        /// <summary>
        /// Gets or sets the automatic id
        /// </summary>
        public string Id
        {
            get { return this.Get(automaticIdName); }
            set { this.Set(automaticIdName, value); }
        }

        /// <summary>
        /// Gets or sets the automatic name
        /// </summary>
        public string Name
        {
            get { return this.Get(automaticNameName); }
            set { this.Set(automaticNameName, value); }
        }

        /// <summary>
        /// Gets or sets the path where this tool is placed
        /// </summary>
        public string Path
        {
            get { return this.Get(pathName, ""); }
            set { this.Set(pathName, value); }
        }

        /// <summary>
        /// Gets or sets the HTML content
        /// </summary>
        public string HTML
        {
            get { return this.Get(HTMLContentName, ""); }
            set { this.Set(HTMLContentName, value); string result = this.GeneratedHTML; }
        }

        /// <summary>
        /// Gets the final HTML content after translation of any configuration key
        /// </summary>
        public string GeneratedHTML
        {
            get { return Project.CurrentProject.Configuration.Replace(this.HTML); }
        }

        /// <summary>
        /// Gets events
        /// </summary>
        public Events Events
        {
            get { return this.Get(eventsName, new Events()); }
        }

        /// <summary>
        /// Gets the JavaScript code
        /// </summary>
        public CodeJavaScript JavaScript
        {
            get { return this.Get(javascriptName, new CodeJavaScript()); }
        }

        /// <summary>
        /// Gets or sets the JavaScript source code
        /// </summary>
        public string JavaScriptSource
        {
            get { return this.JavaScript.Code; }
            set { this.JavaScript.Code = value; }
        }

        /// <summary>
        /// Gets or sets the javascript on load
        /// </summary>
        public CodeJavaScript JavaScriptOnLoad
        {
            get { return this.Get(javascriptOnloadName, new CodeJavaScript()); }
            set { this.Set(javascriptOnloadName, value); }
        }

        /// <summary>
        /// Gets or sets the javascript on load source code
        /// </summary>
        public string JavaScriptOnLoadSource
        {
            get { return this.JavaScriptOnLoad.Code; }
            set { this.JavaScriptOnLoad.Code = value; }
        }

        /// <summary>
        /// Gets the css code
        /// </summary>
        public CodeCSS CSS
        {
            get
            {
                CodeCSS c = this.CSSList.List.Find(x => x.Ids == "#" + this.Id);
                if (c == null)
                    this.CSSList.AddCSS(new CodeCSS("#" + this.Id));
                return this.CSSList.List.Find(x => x.Ids == "#" + this.Id);
            }
        }

        /// <summary>
        /// Gets the css list
        /// </summary>
        public CSSList CSSList
        {
            get { return this.Get(cssListName, new CSSList()); }
        }

        /// <summary>
        /// Gets the type name
        /// </summary>
        public string TypeName
        {
            get { return "HTMLTool"; }
        }

        /// <summary>
        /// Gets the element title
        /// </summary>
        public string ElementTitle
        {
            get { return this.Title; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Import a CSS
        /// </summary>
        /// <param name="css">css code</param>
        public void ImportCSS(List<CodeCSS> css)
        {
            this.Set(cssListName, new CSSList(css));
        }

        /// <summary>
        /// Returns the CSS output
        /// </summary>
        /// <param name="resolveConfig">true if resolve configuration</param>
        /// <returns>css</returns>
        public string CSSOutput(bool resolveConfig)
        {
            return HTMLTool.CSSOutput(this.CSSList.List, resolveConfig);
        }

        /// <summary>
        /// Obtain the CSS output
        /// given a switch to resolve configuration key
        /// </summary>
        /// <param name="css">css input</param>
        /// <param name="resolveConfig">true if replace configuration key by its value</param>
        /// <returns>css output</returns>
        public static string CSSOutput(List<CodeCSS> css, bool resolveConfig)
        {
            string output = string.Empty;
            List<string> list = css.ConvertAll(a => { return a.GenerateCSS(true, true, resolveConfig) + Environment.NewLine; });
            if (list.Count() > 0) output += list.Aggregate((a, b) => a + b);
            return output;
        }

        /// <summary>
        /// Generate design from nothing
        /// </summary>
        /// <returns>html output</returns>
        public OutputHTML GenerateDesign()
        {
            return Routines.GenerateDesignTool(this);
        }

        /// <summary>
        /// Generate design from a page reference
        /// No tool is directly inherited from a page
        /// </summary>
        /// <param name="refPage">page reference</param>
        /// <returns>html output</returns>
        public OutputHTML GenerateDesign(Page refPage)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Generate design from a page or a master page
        /// No tool is directly inherited from a page or a master page
        /// </summary>
        /// <param name="refPage">page reference</param>
        /// <param name="masterRefPage">master page reference</param>
        /// <param name="parentConstraint">parent constraint</param>
        /// <returns>html output</returns>
        public OutputHTML GenerateDesign(Page refPage, MasterPage masterRefPage, ParentConstraint parentConstraint)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Generate design from a page or a master page
        /// No tool is directly inherited from a page or a master page
        /// </summary>
        /// <param name="refPage">page reference</param>
        /// <param name="objects">object list</param>
        /// <param name="parentConstraint">parent constraint</param>
        /// <returns>html output</returns>
        public OutputHTML GenerateDesign(Page refPage, List<MasterObject> objects, ParentConstraint parentConstraint)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Generate design from a page or a master page
        /// No tool is directly inherited from a page or a master page
        /// </summary>
        /// <param name="refPage">page reference</param>
        /// <param name="masterRefPage">master page reference</param>
        /// <param name="objects">object list</param>
        /// <param name="parentConstraint">parent constraint</param>
        /// <returns>html output</returns>
        public OutputHTML GenerateDesign(Page refPage, MasterPage masterRefPage, List<MasterObject> objects, ParentConstraint parentConstraint)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Generate a thumbnail
        /// This function should be implemented
        /// </summary>
        /// <returns>html output</returns>
        public OutputHTML GenerateThumbnail()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Clone this object
        /// </summary>
        /// <returns>cloned object</returns>
        public object Clone()
        {

            HTMLTool tool = new HTMLTool();
            tool.ConstraintWidth = this.ConstraintWidth;
            tool.ConstraintHeight = this.ConstraintHeight;
            tool.Width = this.Width;
            tool.Height = this.Height;
            tool.Title = ExtensionMethods.CloneThis(this.Title);
            tool.Path = ExtensionMethods.CloneThis(this.Path);
            tool.HTML = ExtensionMethods.CloneThis(this.HTML);
            tool.Set(eventsName, this.Events.Clone());
            tool.Set(javascriptName, this.JavaScript.Clone());
            tool.Set(javascriptOnloadName, this.JavaScriptOnLoad.Clone());
            tool.CSS.Ids = "#" + tool.Id;
            tool.CSSList.List.AddRange(from CodeCSS c in this.CSSList.List select c.Clone() as CodeCSS);
            return tool;

        }

        #endregion
    }
}
