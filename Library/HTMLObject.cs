using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library
{
    /// <summary>
    /// An HTML Object is an instance of an existing tool or a master object
    /// Despite a master object is a design for an html complex content,
    /// the HTML Object is the instance of a master object and its sub-contents are unique
    /// If HTML Object is a tool, it means that the HTML tool source is no more linked with.
    /// Any changes in a tool after creating an instance of a tool are not propagated
    /// </summary>
    [Serializable]
    public class HTMLObject : Marshalling.PersistentDataObject, IContent, IContainer, IGenerateDesign, IGenerateProduction, ICloneable
    {

        #region Fields

        /// <summary>
        /// Index name for unique id
        /// </summary>
        protected static readonly string uniqueName = "unique";
        /// <summary>
        /// Index name to handle the object name at which it belongs to
        /// </summary>
        protected static readonly string belongsToName = "belongsTo";
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
        /// Index name to handle the master object of what this html object inherits
        /// </summary>
        protected static readonly string masterObjectName = "masterObject";
        /// <summary>
        /// Index name to handle the tool of what this html object inherits
        /// </summary>
        protected static readonly string toolName = "tool";
        /// <summary>
        /// Index name to handle the container name where this html object resides
        /// </summary>
        protected static readonly string containerName = "container";
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
        /// Index name for javascript code
        /// </summary>
        protected static readonly string javascriptName = "javascript";
        /// <summary>
        /// Index name for javascript on load code
        /// </summary>
        protected static readonly string javascriptOnloadName = "javascriptOnload";
        /// <summary>
        /// Index name for css
        /// </summary>
        protected static readonly string cssName = "css";
        /// <summary>
        /// Index name for additional css
        /// </summary>
        protected static readonly string additionalCssName = "additionalCss";

        #endregion

        #region Public Constructors

        /// <summary>
        /// Default constructor
        /// Create an HTML object that hosts an instance of a tool
        /// </summary>
        /// <param name="htmlTool">HTML Tool import</param>
        public HTMLObject(HTMLTool htmlTool)
        {
            int val = Project.CurrentProject.IncrementedCounter;
            this.Set(automaticNameName, String.Format("object{0}", val));
            this.Set(automaticIdName, String.Format("idObject{0}", val));

            this.Set(masterObjectName, "");
            this.Set(toolName, htmlTool.Path + "/" + htmlTool.Title);
            this.Width = htmlTool.Width;
            this.Height = htmlTool.Height;
            this.ConstraintWidth = htmlTool.ConstraintWidth;
            this.ConstraintHeight = htmlTool.ConstraintHeight;
            this.Title = ExtensionMethods.CloneThis(htmlTool.Title);
            this.HTML = htmlTool.HTML;
            this.Set(javascriptName, htmlTool.JavaScript.Clone());
            this.Set(javascriptOnloadName, htmlTool.JavaScriptOnLoad.Clone());
            this.Set(cssName, new CodeCSS(htmlTool.CSS));
            this.CSS.Ids = "#" + this.Id;
            this.Set(additionalCssName, (from CodeCSS c in htmlTool.CSSAdditional select c.Clone() as CodeCSS).ToList());
        }

        /// <summary>
        /// Constructor for a master object
        /// </summary>
        /// <param name="masterObject">master object to use</param>
        public HTMLObject(MasterObject masterObject)
        {
            int val = Project.CurrentProject.IncrementedCounter;
            this.Set(automaticNameName, String.Format("object{0}", val));
            this.Set(automaticIdName, String.Format("idObject{0}", val));

            this.Set(masterObjectName, masterObject.Name);
            this.Width = masterObject.Width;
            this.Height = masterObject.Height;
            this.ConstraintWidth = masterObject.ConstraintWidth;
            this.ConstraintHeight = masterObject.ConstraintHeight;
            this.Title = ExtensionMethods.CloneThis(masterObject.Title);
            this.Set(javascriptName, masterObject.JavaScript.Clone());
            this.Set(javascriptOnloadName, masterObject.JavaScriptOnLoad.Clone());
            this.Set(cssName, new CodeCSS(masterObject.CSS));
            this.CSS.Ids = "#" + this.Id;
        }

        /// <summary>
        /// Empty constructor
        /// </summary>
        public HTMLObject()
        {
            int val = Project.CurrentProject.IncrementedCounter;
            this.Set(automaticNameName, String.Format("object{0}", val));
            this.Set(automaticIdName, String.Format("idObject{0}", val));
            this.CSS.Ids = "#" + this.Id;
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="obj">object to copy</param>
        private HTMLObject(HTMLObject obj)
        {
            int val = Project.CurrentProject.IncrementedCounter;
            this.Set(automaticNameName, String.Format("object{0}", val));
            this.Set(automaticIdName, String.Format("idObject{0}", val));

            this.Set(toolName, ExtensionMethods.CloneThis(obj.ToolFullPath));
            this.Set(masterObjectName, ExtensionMethods.CloneThis(obj.MasterObjectName));
            this.Width = obj.Width;
            this.Height = obj.Height;
            this.ConstraintWidth = obj.ConstraintWidth;
            this.ConstraintHeight = obj.ConstraintHeight;
            this.Title = ExtensionMethods.CloneThis(obj.Title);
            this.Container = ExtensionMethods.CloneThis(obj.Container);
            this.HTML = ExtensionMethods.CloneThis(obj.HTML);
            this.Set(javascriptName, obj.JavaScript.Clone());
            this.Set(javascriptOnloadName, obj.JavaScriptOnLoad.Clone());
            this.Set(cssName, obj.CSS.Clone());
            this.CSS.Ids = "#" + this.Id;
            this.Set(additionalCssName, (from CodeCSS c in this.CSSAdditional select c.Clone() as CodeCSS).ToList());
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the unique id
        /// </summary>
        public string Unique
        {
            get { return this.Get(uniqueName); }
            set { this.Set(uniqueName, value); }
        }

        /// <summary>
        /// Says if it is a master object
        /// </summary>
        public bool IsMasterObject
        {
            get { return !String.IsNullOrEmpty(this.MasterObjectName); }
        }

        /// <summary>
        /// Says if it is a tool object
        /// </summary>
        public bool IsToolObject
        {
            get { return String.IsNullOrEmpty(this.MasterObjectName); }
        }

        /// <summary>
        /// Gets or sets the parent
        /// </summary>
        public string BelongsTo
        {
            get { return this.Get(belongsToName, ""); }
            set { this.Set(belongsToName, value); }
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
        /// Gets the master object name
        /// </summary>
        public string MasterObjectName
        {
            get { return this.Get(masterObjectName, ""); }
        }

        /// <summary>
        /// Gets the full path of tool
        /// </summary>
        public string ToolFullPath
        {
            get { return this.Get(toolName, ""); }
        }

        /// <summary>
        /// Gets or sets the width value
        /// </summary>
        public uint Width
        {
            get { return this.Get(widthName, 0); }
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
            get { return this.Get(heightName, 0); }
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
        /// Gets or sets the container
        /// </summary>
        public string Container
        {
            get { return this.Get(containerName, ""); }
            set { this.Set(containerName, value); }
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
        /// Gets or sets CSS
        /// </summary>
        public CodeCSS CSS
        {
            get { return this.Get(cssName, new CodeCSS()); }
            set { this.Set(cssName, value); }
        }

        /// <summary>
        /// Gets the CSS additional list
        /// </summary>
        public List<CodeCSS> CSSAdditional
        {
            get { return this.Get(additionalCssName, new List<CodeCSS>()); }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Search container
        /// </summary>
        /// <param name="containers">container list</param>
        /// <param name="objects">object content list</param>
        /// <param name="searchName">container name to search</param>
        /// <param name="found">container result</param>
        /// <returns>true if a container has found</returns>
        public bool SearchContainer(List<IContainer> containers, List<IContent> objects, string searchName, out IContainer found)
        {
            found = null;
            bool done = false;
            if (this.IsMasterObject)
            {
                foreach (IContainer cont in containers)
                {
                    if (cont.Name == this.MasterObjectName && cont.SearchContainer(containers, objects, searchName, out found))
                    {
                        done = true;
                        break;
                    }
                }
            }
            return done;
        }

        /// <summary>
        /// Import a css object
        /// </summary>
        /// <param name="css">css code </param>
        public void ImportCSS(CodeCSS css)
        {
            this.CSS = css.Clone() as CodeCSS;
        }

        /// <summary>
        /// Obtain the CSS output
        /// given a switch to resolve configuration key
        /// </summary>
        /// <param name="resolveConfig">true if replace configuration key by its value</param>
        /// <returns>css output</returns>
        public string CSSOutput(bool resolveConfig)
        {
            string output = this.CSS.GenerateCSS(resolveConfig ? true : false, true, resolveConfig) + Environment.NewLine;
            List<string> list = this.CSSAdditional.ConvertAll(a => { return a.GenerateCSS(true, true, resolveConfig) + Environment.NewLine; });
            if (list.Count() > 0) output += list.Aggregate((a, b) => a + b);
            return output;
        }

        /// <summary>
        /// Generate design from nothing
        /// Function to design this object
        /// a master object or a tool
        /// </summary>
        /// <returns>html output</returns>
        public OutputHTML GenerateDesign()
        {
            if (this.IsMasterObject)
            {
                MasterObject selectedMo = Project.CurrentProject.MasterObjects.Find(mo => { return mo.Name == this.MasterObjectName; });
                if (selectedMo != null)
                {
                    OutputHTML output = selectedMo.GenerateDesign();
                    return output;
                }
                else
                {
                    throw new KeyNotFoundException(String.Format(Localization.Strings.GetString("ExceptionMasterObjectNotExists"), this.MasterObjectName, this.Title));
                }
            }
            else
            {
                return Routines.GenerateDesignObject(this);
            }
        }


        /// <summary>
        /// Generate design from a page reference
        /// No html object is directly inherited from a page
        /// </summary>
        /// <param name="refPage">page reference</param>
        /// <returns>html output</returns>
        public OutputHTML GenerateDesign(Page refPage)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Generate design from a page or a master page
        /// </summary>
        /// <param name="refPage">page reference</param>
        /// <param name="masterRefPage">master page reference</param>
        /// <param name="parentConstraint">parent constraint</param>
        /// <returns>html output</returns>
        public OutputHTML GenerateDesign(Page refPage, MasterPage masterRefPage, ParentConstraint parentConstraint)
        {
            if (this.IsMasterObject)
            {
                MasterObject selectedMo = Project.CurrentProject.MasterObjects.Find(mo => { return mo.Name == this.MasterObjectName; });
                if (selectedMo != null)
                {
                    // calcul de la taille maximum de l'objet
                    ParentConstraint newParent = new ParentConstraint(this.Name, parentConstraint);
                    Routines.MoveConstraint(newParent, selectedMo.Width, selectedMo.Height, selectedMo.ConstraintWidth, selectedMo.ConstraintHeight);
                    OutputHTML output = selectedMo.GenerateDesign(refPage, masterRefPage, newParent);
                    return output;
                }
                else
                {
                    throw new KeyNotFoundException(String.Format(Localization.Strings.GetString("ExceptionMasterObjectNotExists"), this.MasterObjectName, this.Title));
                }
            }
            else
            {
                OutputHTML html = new OutputHTML();
                CodeCSS myCss = new CodeCSS(this.CSS);
                string myId = "obj" + Project.IncrementedTraceCounter.ToString();

                ParentConstraint newInfos = Routines.ComputeObject(parentConstraint, this);
                Routines.SetObjectDisposition(newInfos, myCss, newInfos);
                ConstraintSize cs = new ConstraintSize(newInfos.constraintWidth, newInfos.precedingWidth, newInfos.maximumWidth, newInfos.constraintHeight, newInfos.precedingHeight, newInfos.maximumHeight);
                myCss.Ids = "#" + myId;
                Routines.SetCSSPart(myCss, cs);
                html.HTML.Append("<div id='" + myId + "' name='" + myId + "' " + cs.attributeWidth + " " + cs.attributeHeight + ">");
                html.HTML.Append(this.GeneratedHTML);
                html.HTML.Append("</div>");
                html.CSS.Append(myCss.GenerateCSS(true, true, true) + Environment.NewLine);
                html.AppendCSS(this.CSSAdditional);
                html.JavaScript.Append(this.JavaScript.GeneratedCode);
                html.JavaScriptOnLoad.Append(this.JavaScriptOnLoad.GeneratedCode);
                return html;
            }
        }


        /// <summary>
        /// Generate design from a page and its objects
        /// </summary>
        /// <param name="refPage">page reference</param>
        /// <param name="objects">object list</param>
        /// <param name="parentConstraint">parent constraint</param>
        /// <returns>html output</returns>
        public OutputHTML GenerateDesign(Page refPage, List<MasterObject> objects, ParentConstraint parentConstraint)
        {
            if (this.IsMasterObject)
            {
                MasterObject selectedMo = Project.CurrentProject.MasterObjects.Find(mo => { return mo.Name == this.MasterObjectName; });
                if (selectedMo != null)
                {
                    // calcul de la taille maximum de l'objet
                    ParentConstraint newParent = new ParentConstraint(this.Name, parentConstraint);
                    newParent.maximumWidth = selectedMo.Width;
                    newParent.maximumHeight = selectedMo.Height;
                    Routines.MoveConstraint(newParent, selectedMo.Width, selectedMo.Height, selectedMo.ConstraintWidth, selectedMo.ConstraintHeight);
                    OutputHTML output = selectedMo.GenerateDesign(refPage, objects, newParent);
                    return output;
                }
                else
                {
                    throw new KeyNotFoundException(String.Format(Localization.Strings.GetString("ExceptionMasterObjectNotExists"), this.MasterObjectName, this.Title));
                }
            }
            else
            {
                OutputHTML html = new OutputHTML();
                CodeCSS myCss = new CodeCSS(this.CSS);
                string myId = "obj" + Project.IncrementedTraceCounter.ToString();

                ParentConstraint newInfos = Routines.ComputeObject(parentConstraint, this);
                Routines.SetObjectDisposition(newInfos, myCss, newInfos);
                ConstraintSize cs = new ConstraintSize(newInfos.constraintWidth, newInfos.precedingWidth, newInfos.maximumWidth, newInfos.constraintHeight, newInfos.precedingHeight, newInfos.maximumHeight);
                myCss.Ids = "#" + myId;
                Routines.SetCSSPart(myCss, cs);
                html.HTML.Append("<div id='" + myId + "' name='" + myId + "' " + cs.attributeWidth + " " + cs.attributeHeight + ">");
                html.HTML.Append(this.GeneratedHTML);
                html.HTML.Append("</div>");
                html.CSS.Append(myCss.GenerateCSS(true, true, true) + Environment.NewLine);
                html.AppendCSS(this.CSSAdditional);
                html.JavaScript.Append(this.JavaScript.GeneratedCode);
                html.JavaScriptOnLoad.Append(this.JavaScriptOnLoad.GeneratedCode);
                return html;
            }
        }

        /// <summary>
        /// Generate design from a page or a master page with their objects
        /// </summary>
        /// <param name="refPage">page reference</param>
        /// <param name="masterRefPage">master page reference</param>
        /// <param name="objects">object list</param>
        /// <param name="parentConstraint">parent constraint</param>
        /// <returns>html output</returns>
        public OutputHTML GenerateDesign(Page refPage, MasterPage masterRefPage, List<MasterObject> objects, ParentConstraint parentConstraint)
        {
            if (this.IsMasterObject)
            {
                MasterObject selectedMo = Project.CurrentProject.MasterObjects.Find(mo => { return mo.Name == this.MasterObjectName; });
                if (selectedMo != null)
                {
                    // calcul de la taille maximum de l'objet
                    ParentConstraint newParent = new ParentConstraint(this.Name, parentConstraint);
                    Routines.MoveConstraint(newParent, selectedMo.Width, selectedMo.Height, selectedMo.ConstraintWidth, selectedMo.ConstraintHeight);
                    OutputHTML output = selectedMo.GenerateDesign(refPage, masterRefPage, objects, newParent);
                    return output;
                }
                else
                {
                    throw new KeyNotFoundException(String.Format(Localization.Strings.GetString("ExceptionMasterObjectNotExists"), this.MasterObjectName, this.Title));
                }
            }
            else
            {
                OutputHTML html = new OutputHTML();
                CodeCSS myCss = new CodeCSS(this.CSS);
                string myId = "obj" + Project.IncrementedTraceCounter.ToString();

                ParentConstraint newInfos = Routines.ComputeObject(parentConstraint, this);
                Routines.SetObjectDisposition(newInfos, myCss, newInfos);
                ConstraintSize cs = new ConstraintSize(newInfos.constraintWidth, newInfos.precedingWidth, newInfos.maximumWidth, newInfos.constraintHeight, newInfos.precedingHeight, newInfos.maximumHeight);
                myCss.Ids = "#" + myId;
                Routines.SetCSSPart(myCss, cs);
                html.HTML.Append("<div id='" + myId + "' name='" + myId + "' " + cs.attributeWidth + " " + cs.attributeHeight + ">");
                html.HTML.Append(this.GeneratedHTML);
                html.HTML.Append("</div>");
                html.CSS.Append(myCss.GenerateCSS(true, true, true) + Environment.NewLine);
                html.AppendCSS(this.CSSAdditional);
                html.JavaScript.Append(this.JavaScript.GeneratedCode);
                html.JavaScriptOnLoad.Append(this.JavaScriptOnLoad.GeneratedCode);
                return html;
            }
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
        /// Generate actual website from nothing
        /// </summary>
        /// <returns>html output</returns>
        public OutputHTML GenerateProduction()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Generate design from a page reference
        /// No html object is directly inherited from a page
        /// </summary>
        /// <param name="refPage">page reference</param>
        /// <returns>html output</returns>
        public OutputHTML GenerateProduction(Page refPage)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Generate actual website from a page or a master page
        /// </summary>
        /// <param name="refPage">page reference</param>
        /// <param name="masterRefPage">master page reference</param>
        /// <param name="parentConstraint">parent constraint</param>
        /// <returns>html output</returns>
        public OutputHTML GenerateProduction(Page refPage, MasterPage masterRefPage, ParentConstraint parentConstraint)
        {
            if (this.IsMasterObject)
            {
                MasterObject selectedMo = Project.CurrentProject.MasterObjects.Find(mo => { return mo.Name == this.MasterObjectName; });
                if (selectedMo != null)
                {
                    // calcul de la taille maximum de l'objet
                    ParentConstraint newParent = new ParentConstraint(this.Name, parentConstraint);
                    Routines.MoveConstraint(newParent, selectedMo.Width, selectedMo.Height, selectedMo.ConstraintWidth, selectedMo.ConstraintHeight);
                    OutputHTML output = selectedMo.GenerateProduction(refPage, masterRefPage, newParent);
                    return output;
                }
                else
                {
                    throw new KeyNotFoundException(String.Format(Localization.Strings.GetString("ExceptionMasterObjectNotExists"), this.MasterObjectName, this.Title));
                }
            }
            else
            {
                OutputHTML html = new OutputHTML();
                CodeCSS myCss = new CodeCSS(this.CSS);
                string myId = "obj" + Project.IncrementedTraceCounter.ToString();

                ParentConstraint newInfos = Routines.ComputeObject(parentConstraint, this);
                Routines.SetObjectDisposition(newInfos, myCss, newInfos);
                ConstraintSize cs = new ConstraintSize(newInfos.constraintWidth, newInfos.precedingWidth, newInfos.maximumWidth, newInfos.constraintHeight, newInfos.precedingHeight, newInfos.maximumHeight);
                myCss.Ids = "#" + myId;
                Routines.SetCSSPart(myCss, cs);
                html.HTML.Append("<div id='" + myId + "' name='" + myId + "' " + cs.attributeWidth + " " + cs.attributeHeight + ">");
                html.HTML.Append(this.GeneratedHTML);
                html.HTML.Append("</div>");
                html.CSS.Append(myCss.GenerateCSS(true, true, true) + Environment.NewLine);
                html.AppendCSS(this.CSSAdditional);
                html.JavaScript.Append(this.JavaScript.GeneratedCode);
                html.JavaScriptOnLoad.Append(this.JavaScriptOnLoad.GeneratedCode);
                return html;
            }
        }

        /// <summary>
        /// Generate actual website from a page and its objects
        /// </summary>
        /// <param name="refPage">page reference</param>
        /// <param name="masterRefPage">master page reference</param>
        /// <param name="objects">object list</param>
        /// <param name="parentConstraint">parent constraint</param>
        /// <returns>html output</returns>
        public OutputHTML GenerateProduction(Page refPage, MasterPage masterRefPage, List<MasterObject> objects, ParentConstraint parentConstraint)
        {
            if (this.IsMasterObject)
            {
                MasterObject selectedMo = Project.CurrentProject.MasterObjects.Find(mo => { return mo.Name == this.MasterObjectName; });
                if (selectedMo != null)
                {
                    // calcul de la taille maximum de l'objet
                    ParentConstraint newParent = new ParentConstraint(this.Name, parentConstraint);
                    Routines.MoveConstraint(newParent, selectedMo.Width, selectedMo.Height, selectedMo.ConstraintWidth, selectedMo.ConstraintHeight);
                    OutputHTML output = selectedMo.GenerateProduction(refPage, masterRefPage, objects, newParent);
                    return output;
                }
                else
                {
                    throw new KeyNotFoundException(String.Format(Localization.Strings.GetString("ExceptionMasterObjectNotExists"), this.MasterObjectName, this.Title));
                }
            }
            else
            {
                OutputHTML html = new OutputHTML();
                CodeCSS myCss = new CodeCSS(this.CSS);
                string myId = "obj" + Project.IncrementedTraceCounter.ToString();

                ParentConstraint newInfos = Routines.ComputeObject(parentConstraint, this);
                Routines.SetObjectDisposition(newInfos, myCss, newInfos);
                ConstraintSize cs = new ConstraintSize(newInfos.constraintWidth, newInfos.precedingWidth, newInfos.maximumWidth, newInfos.constraintHeight, newInfos.precedingHeight, newInfos.maximumHeight);
                myCss.Ids = "#" + myId;
                Routines.SetCSSPart(myCss, cs);
                html.HTML.Append("<div id='" + myId + "' name='" + myId + "' " + cs.attributeWidth + " " + cs.attributeHeight + ">");
                html.HTML.Append(this.GeneratedHTML);
                html.HTML.Append("</div>");
                html.CSS.Append(myCss.GenerateCSS(true, true, true) + Environment.NewLine);
                html.AppendCSS(this.CSSAdditional);
                html.JavaScript.Append(this.JavaScript.GeneratedCode);
                html.JavaScriptOnLoad.Append(this.JavaScriptOnLoad.GeneratedCode);
                return html;
            }
        }

        /// <summary>
        /// Clone this object
        /// </summary>
        /// <returns>cloned object</returns>
        public object Clone()
        {
            HTMLObject newObject = new HTMLObject(this);
            return newObject;
        }

        #endregion
    }
}
