using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library
{
    /// <summary>
    /// A master object hosts a template for an object that can be instanciate
    /// to set a container with one instance of this
    /// </summary>
    [Serializable]
    public class MasterObject : Marshalling.PersistentDataObject, IContainer, IProjectElement, IGenerateDesign, IGenerateDesignDIV, IGenerateDesignTable, IGenerateProduction, IGenerateProductionDIV, IGenerateProductionTable, ICloneable
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
        /// Index name for automatic name
        /// </summary>
        protected static readonly string automaticNameName = "automaticName";
        /// <summary>
        /// Index name for automatic id
        /// </summary>
        protected static readonly string automaticIdName = "automaticId";
        /// <summary>
        /// Index name for title
        /// </summary>
        protected static readonly string titleName = "title";
        /// <summary>
        /// Index name for counting lines
        /// </summary>
        protected static readonly string countingLinesName = "countingLines";
        /// <summary>
        /// Index name for counting column
        /// </summary>
        protected static readonly string countingColumnsName = "countingColumns";
        /// <summary>
        /// Index name for container name
        /// </summary>
        protected static readonly string containerName = "containerName";
        /// <summary>
        /// Index name for width value
        /// </summary>
        protected static readonly string widthName = "width";
        /// <summary>
        /// Index name for height value
        /// </summary>
        protected static readonly string heightName = "height";
        /// <summary>
        /// Index name for javascript code
        /// </summary>
        protected static readonly string javascriptName = "javascript";
        /// <summary>
        /// Index name for javascript onload code
        /// </summary>
        protected static readonly string javascriptOnloadName = "javascriptOnload";
        /// <summary>
        /// Index name for css styles
        /// </summary>
        protected static readonly string cssName = "css";
        /// <summary>
        /// Index name for the html header
        /// </summary>
        protected static readonly string htmlHeaderName = "htmlHeader";
        /// <summary>
        /// Index name for the html footer
        /// </summary>
        protected static readonly string htmlFooterName = "htmlFooter";
        /// <summary>
        /// Index name for its own objects
        /// </summary>
        protected static readonly string objectListName = "objects";
        /// <summary>
        /// Index name for horizontal areas
        /// </summary>
        protected static readonly string horizontalZoneName = "horizontalZone";

        #endregion

        #region Default Constructor

        /// <summary>
        /// Empty constructor
        /// </summary>
        public MasterObject()
        {
            int val = Project.CurrentProject.IncrementedCounter;
            this.Set(automaticNameName, String.Format("masterObj{0}", val));
            this.Set(automaticIdName, String.Format("idMasterObj{0}", val));
        }
        
        #endregion

        #region Copy Constructor

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="refObj">object source</param>
        private MasterObject(MasterObject refObj)
        {

            int val = Project.CurrentProject.IncrementedCounter;
            this.Set(automaticNameName, String.Format("masterObj{0}", val));
            this.Set(automaticIdName, String.Format("idMasterObj{0}", val));

            this.ConstraintWidth = refObj.ConstraintWidth;
            this.ConstraintHeight = refObj.ConstraintHeight;
            this.Width = refObj.Width;
            this.Height = refObj.Height;
            this.CountLines = refObj.CountLines;
            this.CountColumns = refObj.CountColumns;
            this.Container = ExtensionMethods.CloneThis(refObj.Container);
            this.Title = ExtensionMethods.CloneThis(refObj.Title);
            foreach (HTMLObject obj in refObj.Objects)
            {
                this.Objects.Add(obj.Clone() as HTMLObject);
            }
            foreach (HorizontalZone hz in refObj.HorizontalZones)
            {
                this.HorizontalZones.Add(hz.Clone() as HorizontalZone);
            }
            this.Set(javascriptName, refObj.JavaScript.Clone());
            this.Set(javascriptOnloadName, refObj.JavaScriptOnLoad.Clone());
            this.CSS = refObj.CSS.Clone() as CodeCSS;
            this.HTMLBefore = ExtensionMethods.CloneThis(refObj.HTMLBefore);
            this.HTMLAfter = ExtensionMethods.CloneThis(refObj.HTMLAfter);
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
        /// Test if width constraint is relative
        /// Or set the constraint to relative
        /// </summary>
        public bool RelativeWidth
        {
            get { return this.ConstraintWidth == EnumConstraint.RELATIVE; }
            set { this.ConstraintWidth = EnumConstraint.RELATIVE; }
        }

        /// <summary>
        /// Test if height constraint is relative
        /// Or set the constraint to relative
        /// </summary>
        public bool RelativeHeight
        {
            get { return this.ConstraintHeight == EnumConstraint.RELATIVE; }
            set { this.ConstraintHeight = EnumConstraint.RELATIVE; }
        }

        /// <summary>
        /// Gets or sets width constraint
        /// </summary>
        public EnumConstraint ConstraintWidth
        {
            get { return this.Get(constraintWidthName, EnumConstraint.AUTO); }
            set { this.Get(constraintWidthName, value); }
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
        /// Gets or sets the counting lines
        /// </summary>
        public uint CountLines
        {
            get { return this.Get(countingLinesName, 0u); }
            set { this.Set(countingLinesName, value); }
        }

        /// <summary>
        /// Gets or sets the counting columns
        /// </summary>
        public uint CountColumns
        {
            get { return this.Get(countingColumnsName, 0u); }
            set { this.Set(countingColumnsName, value); }
        }

        /// <summary>
        /// Gets the size string for dumping mode
        /// </summary>
        public string SizeString
        {
            get { return this.Width.ToString() + "x" + this.Height.ToString(); }
        }

        /// <summary>
        /// Gets the grid size for dumping mode
        /// </summary>
        public string GridSizeString
        {
            get { return this.CountColumns.ToString() + "x" + this.CountLines.ToString(); }
        }

        /// <summary>
        /// Gets or sets the container name
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
        /// Gets its own objects
        /// </summary>
        public List<HTMLObject> Objects
        {
            get { return this.Get(objectListName, new List<HTMLObject>()); }
        }

        /// <summary>
        /// Gets the horizontal areas
        /// </summary>
        public List<HorizontalZone> HorizontalZones
        {
            get { return this.Get(horizontalZoneName, new List<HorizontalZone>()); }
        }

        /// <summary>
        /// Gets or sets the javascript code
        /// </summary>
        public CodeJavaScript JavaScript
        {
            get { return this.Get(javascriptName, new CodeJavaScript()); }
            set { this.Set(javascriptName, value); }
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
        /// Gets or sets the javascript source code
        /// </summary>
        public string JavaScriptSource
        {
            get { return this.JavaScript.Code; }
            set { this.JavaScript.Code = value; }
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
        /// Gets or sets a plain text html header
        /// </summary>
        public string HTMLBefore
        {
            get { return this.Get(htmlHeaderName, ""); }
            set { this.Set(htmlHeaderName, value); }
        }

        /// <summary>
        /// Gets or sets a plain text html footer
        /// </summary>
        public string HTMLAfter
        {
            get { return this.Get(htmlFooterName, ""); }
            set { this.Set(htmlFooterName, value); }
        }

        /// <summary>
        /// Gets the type name
        /// </summary>
        public string TypeName
        {
            get { return "MasterObject"; }
        }

        /// <summary>
        /// Gets the element title
        /// </summary>
        public string ElementTitle
        {
            get { return this.Title; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Search a container by name existing from all containers, all content
        /// and returns the result
        /// </summary>
        /// <param name="containers">all containers</param>
        /// <param name="objects">all contents</param>
        /// <param name="searchName">container to search</param>
        /// <param name="found">container result</param>
        /// <returns>true if the container has found</returns>
        public bool SearchContainer(List<IContainer> containers, List<IContent> objects, string searchName, out IContainer found)
        {
            found = null;
            bool done = false;
            List<IContent> subContents = new List<IContent>(objects);
            subContents.AddRange(this.Objects.Cast<IContent>());
            foreach (HorizontalZone hz in this.HorizontalZones)
            {
                done = hz.SearchContainer(containers, subContents, searchName, out found);
                if (done) break;
            }
            return done;
        }

        /// <summary>
        /// Construct all zones and compute total size
        /// </summary>
        /// <param name="list">list of rectangle the user supplied</param>
        public void MakeZones(List<AreaSizedRectangle> list)
        {
            MasterPage.MakeZones(this.CountColumns, this.CountLines, list, this.HorizontalZones);
        }
        #endregion

        #region Interfaces implementations

        /// <summary>
        /// Generate an HTML DIV tag from null for design
        /// A master object is hosted in a page
        /// This function is not implemented nor called
        /// </summary>
        /// <returns>html output</returns>
        public OutputHTML GenerateDesignDIV()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Generate an HTML DIV tag from a page for design
        /// This function is used because the design for
        /// the master object converted in html is visible into
        /// the dialog box of a master object
        /// </summary>
        /// <param name="refPage">page reference</param>
        /// <returns>html output</returns>
        public OutputHTML GenerateDesignDIV(Page refPage)
        {
            DesignPage config = new DesignPage();
            config.constraintWidth = this.ConstraintWidth;
            config.constraintHeight = this.ConstraintHeight;
            config.width = this.Width;
            config.height = this.Height;
            config.cssPart = this.CSS;
            config.javascriptPart = this.JavaScript;
            config.onload = this.JavaScriptOnLoad;
            config.zones = this.HorizontalZones;
            config.includeContainer = true;
            config.subObjects = this.Objects;
            return Routines.GenerateDesignPageDIV(refPage, this, config);
        }

        /// <summary>
        /// Generate an HTML DIV tag for design
        /// a given master object exists in a page or a master page
        /// </summary>
        /// <param name="refPage">page reference</param>
        /// <param name="masterRefPage">master page reference</param>
        /// <param name="parentConstraint">parent constraint</param>
        /// <returns>html output</returns>
        public OutputHTML GenerateDesignDIV(Page refPage, MasterPage masterRefPage, ParentConstraint parentConstraint)
        {
            OutputHTML output = new OutputHTML();
            CodeCSS myCss = new CodeCSS(this.CSS);
            string myId = "masterObj" + Project.IncrementedTraceCounter.ToString();

            output.HTML.Append(this.HTMLBefore);

            ParentConstraint newInfos = Routines.ComputeMasterObject(parentConstraint, this);
            ConstraintSize cs = new ConstraintSize(newInfos.constraintWidth, newInfos.precedingWidth, newInfos.maximumWidth, newInfos.constraintHeight, newInfos.precedingHeight, newInfos.maximumHeight);

            Routines.SetCSSPart(myCss, cs);
            myCss.Ids = "#" + myId;
            Routines.SetObjectDisposition(parentConstraint, myCss, newInfos);
            output.CSS.Append(myCss.GenerateCSS(true, true, true));
            output.JavaScript.Append(this.JavaScript.GeneratedCode);
            output.JavaScriptOnLoad.Append(this.JavaScriptOnLoad.GeneratedCode);

            output.HTML.Append("<div id='" + myId + "' name='" + newInfos.objectName + "' " + cs.attributeWidth + " " + cs.attributeHeight + ">");

            List<MasterObject> list = new List<MasterObject>();
            list.Add(this);

            foreach (HorizontalZone hz in this.HorizontalZones)
            {
                OutputHTML hzone = hz.GenerateDesignDIV(refPage, masterRefPage, list, newInfos);
                output.HTML.Append(hzone.HTML.ToString());
                output.CSS.Append(hzone.CSS.ToString());
                output.JavaScript.Append(hzone.JavaScript.ToString());
                output.JavaScriptOnLoad.Append(hzone.JavaScriptOnLoad.ToString());
            }
            output.HTML.Append("</div>");

            output.HTML.Append(this.HTMLAfter);
            return output;
        }

        /// <summary>
        /// Generate an HTML DIV tag for design
        /// a page contains master objects related
        /// </summary>
        /// <param name="refPage">page reference</param>
        /// <param name="objects">master objects</param>
        /// <param name="parentConstraint">parent constraint</param>
        /// <returns>html output</returns>
        public OutputHTML GenerateDesignDIV(Page refPage, List<MasterObject> objects, ParentConstraint parentConstraint)
        {
            OutputHTML output = new OutputHTML();
            CodeCSS myCss = new CodeCSS(this.CSS);
            string myId = "masterObj" + Project.IncrementedTraceCounter.ToString();

            output.HTML.Append(this.HTMLBefore);

            ParentConstraint newInfos = Routines.ComputeMasterObject(parentConstraint, this);
            ConstraintSize cs = new ConstraintSize(newInfos.constraintWidth, newInfos.precedingWidth, newInfos.maximumWidth, newInfos.constraintHeight, newInfos.precedingHeight, newInfos.maximumHeight);

            myCss.Ids = "#" + myId;
            Routines.SetCSSPart(myCss, cs);
            Routines.SetObjectDisposition(parentConstraint, myCss, newInfos);
            output.CSS.Append(myCss.GenerateCSS(true, true, true));
            output.JavaScript.Append(this.JavaScript.GeneratedCode);
            output.JavaScriptOnLoad.Append(this.JavaScriptOnLoad.GeneratedCode);

            output.HTML.Append("<div id='" + myId + "' name='" + newInfos.objectName + "' " + cs.attributeWidth + " " + cs.attributeHeight + ">");

            objects.Add(this);
            foreach (HorizontalZone hz in this.HorizontalZones)
            {
                OutputHTML hzone = hz.GenerateDesignDIV(refPage, objects, newInfos);
                output.HTML.Append(hzone.HTML.ToString());
                output.CSS.Append(hzone.CSS.ToString());
                output.JavaScript.Append(hzone.JavaScript.ToString());
                output.JavaScriptOnLoad.Append(hzone.JavaScriptOnLoad.ToString());
            }
            output.HTML.Append("</div>");

            output.HTML.Append(this.HTMLAfter);
            return output;
        }

        /// <summary>
        /// Generate an HTML DIV tag for design
        /// a given master page generates the page
        /// a page contains master objects related
        /// restricted objects in page are computed equally
        /// </summary>
        /// <param name="refPage">page reference</param>
        /// <param name="masterRefPage">master page reference</param>
        /// <param name="objects">master objects list</param>
        /// <param name="parentConstraint">parent constraint</param>
        /// <returns>html output</returns>
        public OutputHTML GenerateDesignDIV(Page refPage, MasterPage masterRefPage, List<MasterObject> objects, ParentConstraint parentConstraint)
        {
            OutputHTML output = new OutputHTML();
            CodeCSS myCss = new CodeCSS(this.CSS);
            string myId = "masterObj" + Project.IncrementedTraceCounter.ToString();

            output.HTML.Append(this.HTMLBefore);

            ParentConstraint newInfos = Routines.ComputeMasterObject(parentConstraint, this);
            ConstraintSize cs = new ConstraintSize(newInfos.constraintWidth, newInfos.precedingWidth, newInfos.maximumWidth, newInfos.constraintHeight, newInfos.precedingHeight, newInfos.maximumHeight);

            myCss.Ids = "#" + myId;
            Routines.SetCSSPart(myCss, cs);
            Routines.SetObjectDisposition(parentConstraint, myCss, newInfos);
            output.CSS.Append(myCss.GenerateCSS(true, true, true));
            output.JavaScript.Append(this.JavaScript.GeneratedCode);
            output.JavaScriptOnLoad.Append(this.JavaScriptOnLoad.GeneratedCode);

            output.HTML.Append("<div id='" + myId + "' name='" + newInfos.objectName + "' " + cs.attributeWidth + " " + cs.attributeHeight + ">");

            objects.Add(this);
            foreach (HorizontalZone hz in this.HorizontalZones)
            {
                OutputHTML hzone = hz.GenerateDesignDIV(refPage, masterRefPage, objects, newInfos);
                output.HTML.Append(hzone.HTML.ToString());
                output.CSS.Append(hzone.CSS.ToString());
                output.JavaScript.Append(hzone.JavaScript.ToString());
                output.JavaScriptOnLoad.Append(hzone.JavaScriptOnLoad.ToString());
            }
            output.HTML.Append("</div>");

            output.HTML.Append(this.HTMLAfter);
            return output;
        }

        /// <summary>
        /// Generate an HTML TABLE tag from a page for design
        /// This function is used because the design for
        /// the master object converted in html is visible into
        /// the dialog box of a master object
        /// </summary>
        /// <param name="refPage">page reference</param>
        /// <returns>html output</returns>
        public OutputHTML GenerateDesignTable(Page refPage)
        {

            DesignPage config = new DesignPage();
            config.constraintWidth = this.ConstraintWidth;
            config.constraintHeight = this.ConstraintHeight;
            config.width = this.Width;
            config.height = this.Height;
            config.cssPart = this.CSS;
            config.javascriptPart = this.JavaScript;
            config.onload = this.JavaScriptOnLoad;
            config.zones = this.HorizontalZones;
            config.includeContainer = true;
            config.subObjects = this.Objects;
            return Routines.GenerateDesignPageTable(refPage, this, config);

        }


        /// <summary>
        /// Generate an HTML TABLE tag for design
        /// a given master page generates the page
        /// </summary>
        /// <param name="refPage">page reference</param>
        /// <param name="masterRefPage">master page reference</param>
        /// <param name="parentConstraint">parent constraint</param>
        /// <returns>html output</returns>
        public OutputHTML GenerateDesignTable(Page refPage, MasterPage masterRefPage, ParentConstraint parentConstraint)
        {
            OutputHTML output = new OutputHTML();
            CodeCSS myCss = new CodeCSS(this.CSS);
            string myId = "masterObj" + Project.IncrementedTraceCounter.ToString();

            output.HTML.Append(this.HTMLBefore);

            ParentConstraint newInfos = Routines.ComputeMasterObject(parentConstraint, this);
            ConstraintSize cs = new ConstraintSize(newInfos.constraintWidth, newInfos.precedingWidth, newInfos.maximumWidth, newInfos.constraintHeight, newInfos.precedingHeight, newInfos.maximumHeight);

            myCss.Ids = "#" + myId;
            Routines.SetCSSPart(myCss, cs);
            Routines.SetObjectDisposition(parentConstraint, myCss, newInfos);
            output.CSS.Append(myCss.GenerateCSS(true, true, true));
            output.JavaScript.Append(this.JavaScript.GeneratedCode);
            output.JavaScriptOnLoad.Append(this.JavaScriptOnLoad.GeneratedCode);

            List<MasterObject> list = new List<MasterObject>();
            list.Add(this);

            output.HTML.Append("<table " + cs.attributeWidth + " " + cs.attributeHeight + " id='" + myId + "' name='" + newInfos.objectName + "' border='0' cellspacing='0' cellpadding='0'>");
            // Si la dernière ligne de la table est vide alors on ne l'ajoute pas
            // raison : compatibité IE/Firefox/Chrome
            // recherche fin de ligne
            int rechfin = this.HorizontalZones.Count;
            for (int index = this.HorizontalZones.Count - 1; index >= 0; --index)
            {
                if (this.HorizontalZones[index].VerticalZones.Count != 0)
                {
                    rechfin = index + 1;
                    break;
                }
            }
            for (int index = 0; index < rechfin; ++index)
            {
                HorizontalZone hz = this.HorizontalZones[index];
                if (index + 1 < this.HorizontalZones.Count || hz.VerticalZones.Count > 0)
                {
                    OutputHTML hzone = hz.GenerateDesignTable(refPage, masterRefPage, list, newInfos);
                    output.HTML.Append(hzone.HTML.ToString());
                    output.CSS.Append(hzone.CSS.ToString());
                    output.JavaScript.Append(hzone.JavaScript.ToString());
                    output.JavaScriptOnLoad.Append(hzone.JavaScriptOnLoad.ToString());
                }
            }
            output.HTML.Append("</table>");

            output.HTML.Append(this.HTMLAfter);
            return output;
        }

        /// <summary>
        /// Generate an HTML TABLE tag for design
        /// a page contains master objects related
        /// </summary>
        /// <param name="refPage">page reference</param>
        /// <param name="objects">master objects</param>
        /// <param name="parentConstraint">parent constraint</param>
        /// <returns>html output</returns>
        public OutputHTML GenerateDesignTable(Page refPage, List<MasterObject> objects, ParentConstraint parentConstraint)
        {
            OutputHTML output = new OutputHTML();
            CodeCSS myCss = new CodeCSS(this.CSS);
            string myId = "masterObj" + Project.IncrementedTraceCounter.ToString();

            output.HTML.Append(this.HTMLBefore);

            ParentConstraint newInfos = Routines.ComputeMasterObject(parentConstraint, this);
            ConstraintSize cs = new ConstraintSize(newInfos.constraintWidth, newInfos.precedingWidth, newInfos.maximumWidth, newInfos.constraintHeight, newInfos.precedingHeight, newInfos.maximumHeight);

            myCss.Ids = "#" + myId;
            Routines.SetCSSPart(myCss, cs);
            Routines.SetObjectDisposition(parentConstraint, myCss, newInfos);
            output.CSS.Append(myCss.GenerateCSS(true, true, true));
            output.JavaScript.Append(this.JavaScript.GeneratedCode);
            output.JavaScriptOnLoad.Append(this.JavaScriptOnLoad.GeneratedCode);

            objects.Add(this);

            output.HTML.Append("<table " + cs.attributeWidth + " " + cs.attributeHeight + " id='" + myId + "' name='" + newInfos.objectName + "' border='0' cellspacing='0' cellpadding='0'>");
            // Si la dernière ligne de la table est vide alors on ne l'ajoute pas
            // raison : compatibité IE/Firefox/Chrome
            // recherche fin de ligne
            int rechfin = this.HorizontalZones.Count;
            for (int index = this.HorizontalZones.Count - 1; index >= 0; --index)
            {
                if (this.HorizontalZones[index].VerticalZones.Count != 0)
                {
                    rechfin = index + 1;
                    break;
                }
            }
            for (int index = 0; index < rechfin; ++index)
            {
                HorizontalZone hz = this.HorizontalZones[index];
                if (index + 1 < this.HorizontalZones.Count || hz.VerticalZones.Count > 0)
                {
                    OutputHTML hzone = hz.GenerateDesignTable(refPage, objects, newInfos);
                    output.HTML.Append(hzone.HTML.ToString());
                    output.CSS.Append(hzone.CSS.ToString());
                    output.JavaScript.Append(hzone.JavaScript.ToString());
                    output.JavaScriptOnLoad.Append(hzone.JavaScriptOnLoad.ToString());
                }
            }
            output.HTML.Append("</table>");

            output.HTML.Append(this.HTMLAfter);
            return output;
        }

        /// <summary>
        /// Generate an HTML TABLE tag for design
        /// a given master page generates the page
        /// a page contains master objects related
        /// restricted objects in page are computed equally
        /// </summary>
        /// <param name="refPage">page reference</param>
        /// <param name="masterRefPage">master page reference</param>
        /// <param name="objects">master objects list</param>
        /// <param name="parentConstraint">parent constraint</param>
        /// <returns>html output</returns>
        public OutputHTML GenerateDesignTable(Page refPage, MasterPage masterRefPage, List<MasterObject> objects, ParentConstraint parentConstraint)
        {
            OutputHTML output = new OutputHTML();
            CodeCSS myCss = new CodeCSS(this.CSS);
            string myId = "masterObj" + Project.IncrementedTraceCounter.ToString();

            output.HTML.Append(this.HTMLBefore);

            ParentConstraint newInfos = Routines.ComputeMasterObject(parentConstraint, this);
            ConstraintSize cs = new ConstraintSize(newInfos.constraintWidth, newInfos.precedingWidth, newInfos.maximumWidth, newInfos.constraintHeight, newInfos.precedingHeight, newInfos.maximumHeight);

            myCss.Ids = "#" + myId;
            Routines.SetCSSPart(myCss, cs);
            Routines.SetObjectDisposition(parentConstraint, myCss, newInfos);
            output.CSS.Append(myCss.GenerateCSS(true, true, true));
            output.JavaScript.Append(this.JavaScript.GeneratedCode);
            output.JavaScriptOnLoad.Append(this.JavaScriptOnLoad.GeneratedCode);

            objects.Add(this);

            output.HTML.Append("<table " + cs.attributeWidth + " " + cs.attributeHeight + " id='" + myId + "' name='" + newInfos.objectName + "' border='0' cellspacing='0' cellpadding='0'>");
            // Si la dernière ligne de la table est vide alors on ne l'ajoute pas
            // raison : compatibité IE/Firefox/Chrome
            // recherche fin de ligne
            int rechfin = this.HorizontalZones.Count;
            for (int index = this.HorizontalZones.Count - 1; index >= 0; --index)
            {
                if (this.HorizontalZones[index].VerticalZones.Count != 0)
                {
                    rechfin = index + 1;
                    break;
                }
            }

            for (int index = 0; index < rechfin; ++index)
            {
                HorizontalZone hz = this.HorizontalZones[index];
                if (index + 1 < this.HorizontalZones.Count || hz.VerticalZones.Count > 0)
                {
                    OutputHTML hzone = hz.GenerateDesignTable(refPage, masterRefPage, objects, newInfos);
                    output.HTML.Append(hzone.HTML.ToString());
                    output.CSS.Append(hzone.CSS.ToString());
                    output.JavaScript.Append(hzone.JavaScript.ToString());
                    output.JavaScriptOnLoad.Append(hzone.JavaScriptOnLoad.ToString());
                }
            }
            output.HTML.Append("</table>");

            output.HTML.Append(this.HTMLAfter);
            return output;
        }


        /// <summary>
        /// Génération du master object sans la page (design)
        /// This is a special case for a master object
        /// A master object can be viewed at design mode.
        /// </summary>
        /// <returns>page html</returns>
        public OutputHTML GenerateDesign()
        {
            if (this.ConstraintWidth == EnumConstraint.RELATIVE || this.ConstraintHeight == EnumConstraint.RELATIVE)
            {
                OutputHTML html = this.GenerateDesignTable(new Page());
                return html;
            }
            else
            {
                // il faut décider si l'on utilise une table ou des div
                // s'il existe une colonne dont countLines > countLines de l'horizontal alors on utilise une table
                // sinon on peut utiliser des div
                bool cannotUseDiv = false;
                foreach (HorizontalZone hz in this.HorizontalZones)
                {
                    bool sup = false;
                    foreach (VerticalZone vz in hz.VerticalZones)
                    {
                        if (vz.CountLines > hz.CountLines) sup = true;
                    }
                    if (sup)
                    {
                        cannotUseDiv = true;
                        break;
                    }
                }
                OutputHTML html;
                if (cannotUseDiv)
                {
                    html = this.GenerateDesignTable(new Page());
                }
                else
                {
                    html = this.GenerateDesignDIV(new Page());
                }
                return html;
            }
        }

        /// <summary>
        /// A master object is not obtained from a single page
        /// but inner page, master page and inner master object
        /// This function is then not implemented nor called
        /// </summary>
        /// <param name="refPage">page à afficher</param>
        /// <returns>page html</returns>
        public OutputHTML GenerateDesign(Page refPage)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Generate design of a master object
        /// </summary>
        /// <param name="refPage">page reference</param>
        /// <param name="masterRefPage">master page reference</param>
        /// <param name="parentConstraint">parent constraint</param>
        /// <returns>html output</returns>
        public OutputHTML GenerateDesign(Page refPage, MasterPage masterRefPage, ParentConstraint parentConstraint)
        {
            if (this.ConstraintWidth == EnumConstraint.RELATIVE || this.ConstraintHeight == EnumConstraint.RELATIVE)
            {
                OutputHTML html = this.GenerateDesignTable(refPage, masterRefPage, parentConstraint);
                return html;
            }
            else
            {
                // il faut décider si l'on utilise une table ou des div
                // s'il existe une colonne dont countLines > countLines de l'horizontal alors on utilise une table
                // sinon on peut utiliser des div
                bool cannotUseDiv = false;
                foreach (HorizontalZone hz in this.HorizontalZones)
                {
                    bool sup = false;
                    foreach (VerticalZone vz in hz.VerticalZones)
                    {
                        if (vz.CountLines > hz.CountLines) sup = true;
                    }
                    if (sup)
                    {
                        cannotUseDiv = true;
                        break;
                    }
                }
                OutputHTML html;
                if (cannotUseDiv)
                {
                    html = this.GenerateDesignTable(refPage, masterRefPage, parentConstraint);
                }
                else
                {
                    html = this.GenerateDesignDIV(refPage, masterRefPage, parentConstraint);
                }
                return html;
            }
        }

        /// <summary>
        /// Generate design of a master object that is declared in a page or a master page
        /// </summary>
        /// <param name="refPage">page reference</param>
        /// <param name="masterRefPage">master page reference</param>
        /// <param name="objects">object list of master object</param>
        /// <param name="parentConstraint">parent constraint</param>
        /// <returns>html output</returns>
        public OutputHTML GenerateDesign(Page refPage, MasterPage masterRefPage, List<MasterObject> objects, ParentConstraint parentConstraint)
        {
            if (this.ConstraintWidth == EnumConstraint.RELATIVE || this.ConstraintHeight == EnumConstraint.RELATIVE)
            {
                OutputHTML html = this.GenerateDesignTable(refPage, masterRefPage, objects, parentConstraint);
                return html;
            }
            else
            {
                // il faut décider si l'on utilise une table ou des div
                // s'il existe une colonne dont countLines > countLines de l'horizontal alors on utilise une table
                // sinon on peut utiliser des div
                bool cannotUseDiv = false;
                foreach (HorizontalZone hz in this.HorizontalZones)
                {
                    bool sup = false;
                    foreach (VerticalZone vz in hz.VerticalZones)
                    {
                        if (vz.CountLines > hz.CountLines) sup = true;
                    }
                    if (sup)
                    {
                        cannotUseDiv = true;
                        break;
                    }
                }
                OutputHTML html;
                if (cannotUseDiv)
                {
                    html = this.GenerateDesignTable(refPage, masterRefPage, objects, parentConstraint);
                }
                else
                {
                    html = this.GenerateDesignDIV(refPage, masterRefPage, objects, parentConstraint);
                }
                return html;
            }
        }

        /// <summary>
        /// Generate design of a master object that is declared in a page
        /// </summary>
        /// <param name="refPage">page reference</param>
        /// <param name="objects">object list of master object</param>
        /// <param name="parentConstraint">parent constraint</param>
        /// <returns>html output</returns>
        public OutputHTML GenerateDesign(Page refPage, List<MasterObject> objects, ParentConstraint parentConstraint)
        {
            if (this.ConstraintWidth == EnumConstraint.RELATIVE || this.ConstraintHeight == EnumConstraint.RELATIVE)
            {
                OutputHTML html = this.GenerateDesignTable(refPage, objects, parentConstraint);
                return html;
            }
            else
            {
                // il faut décider si l'on utilise une table ou des div
                // s'il existe une colonne dont countLines > countLines de l'horizontal alors on utilise une table
                bool cannotUseDiv = false;
                foreach (HorizontalZone hz in this.HorizontalZones)
                {
                    bool sup = false;
                    foreach (VerticalZone vz in hz.VerticalZones)
                    {
                        if (vz.CountLines > hz.CountLines) sup = true;
                    }
                    if (sup)
                    {
                        cannotUseDiv = true;
                        break;
                    }
                }
                OutputHTML html;
                if (cannotUseDiv)
                {
                    html = this.GenerateDesignTable(refPage, objects, parentConstraint);
                }
                else
                {
                    html = this.GenerateDesignDIV(refPage, objects, parentConstraint);
                }
                return html;
            }
        }

        /// <summary>
        /// A master object doesn't have a thumbnail visual
        /// </summary>
        /// <returns>html output</returns>
        public OutputHTML GenerateThumbnail()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Génération du master object sans la page (actual website)
        /// A master object is not solely called
        /// </summary>
        /// <returns>page html</returns>
        public OutputHTML GenerateProduction()
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// A master object is not obtained from a single page
        /// but inner page, master page and inner master object
        /// This function is then not implemented nor called
        /// </summary>
        /// <param name="refPage">page à afficher</param>
        /// <returns>page html</returns>
        public OutputHTML GenerateProduction(Page refPage)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Generate actual website of a master object
        /// </summary>
        /// <param name="refPage">page reference</param>
        /// <param name="masterRefPage">master page reference</param>
        /// <param name="parentConstraint">parent constraint</param>
        /// <returns>html output</returns>
        public OutputHTML GenerateProduction(Page refPage, MasterPage masterRefPage, ParentConstraint parentConstraint)
        {
            if (this.ConstraintWidth == EnumConstraint.RELATIVE || this.ConstraintHeight == EnumConstraint.RELATIVE)
            {
                OutputHTML html = this.GenerateProductionTable(refPage, masterRefPage, parentConstraint);
                return html;
            }
            else
            {
                // il faut décider si l'on utilise une table ou des div
                // s'il existe une colonne dont countLines > countLines de l'horizontal alors on utilise une table
                // sinon on peut utiliser des div
                bool cannotUseDiv = false;
                foreach (HorizontalZone hz in this.HorizontalZones)
                {
                    bool sup = false;
                    foreach (VerticalZone vz in hz.VerticalZones)
                    {
                        if (vz.CountLines > hz.CountLines) sup = true;
                    }
                    if (sup)
                    {
                        cannotUseDiv = true;
                        break;
                    }
                }
                OutputHTML html;
                if (cannotUseDiv)
                {
                    html = this.GenerateProductionTable(refPage, masterRefPage, parentConstraint);
                }
                else
                {
                    html = this.GenerateProductionDIV(refPage, masterRefPage, parentConstraint);
                }
                return html;
            }
        }

        /// <summary>
        /// Generate actual website of a master object that is declared in a page or a master page
        /// </summary>
        /// <param name="refPage">page reference</param>
        /// <param name="masterRefPage">master page reference</param>
        /// <param name="objects">object list of master object</param>
        /// <param name="parentConstraint">parent constraint</param>
        /// <returns>html output</returns>
        public OutputHTML GenerateProduction(Page refPage, MasterPage masterRefPage, List<MasterObject> objects, ParentConstraint parentConstraint)
        {
            if (this.ConstraintWidth == EnumConstraint.RELATIVE || this.ConstraintHeight == EnumConstraint.RELATIVE)
            {
                OutputHTML html = this.GenerateProductionTable(refPage, masterRefPage, objects, parentConstraint);
                return html;
            }
            else
            {
                // il faut décider si l'on utilise une table ou des div
                // s'il existe une colonne dont countLines > countLines de l'horizontal alors on utilise une table
                // sinon on peut utiliser des div
                bool cannotUseDiv = false;
                foreach (HorizontalZone hz in this.HorizontalZones)
                {
                    bool sup = false;
                    foreach (VerticalZone vz in hz.VerticalZones)
                    {
                        if (vz.CountLines > hz.CountLines) sup = true;
                    }
                    if (sup)
                    {
                        cannotUseDiv = true;
                        break;
                    }
                }
                OutputHTML html;
                if (cannotUseDiv)
                {
                    html = this.GenerateProductionTable(refPage, masterRefPage, objects, parentConstraint);
                }
                else
                {
                    html = this.GenerateProductionDIV(refPage, masterRefPage, objects, parentConstraint);
                }
                return html;
            }
        }

        /// <summary>
        /// A master object is indirectly a child of the page
        /// This function is not implemented nor called
        /// </summary>
        /// <param name="refPage">page reference</param>
        /// <returns>html output</returns>
        public OutputHTML GenerateProductionDIV(Page refPage)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Generate an HTML DIV tag for actual website
        /// a given master object exists in a page or a master page
        /// </summary>
        /// <param name="refPage">page reference</param>
        /// <param name="masterRefPage">master page reference</param>
        /// <param name="parentConstraint">parent constraint</param>
        /// <returns>html output</returns>
        public OutputHTML GenerateProductionDIV(Page refPage, MasterPage masterRefPage, ParentConstraint parentConstraint)
        {
            OutputHTML output = new OutputHTML();
            CodeCSS myCss = new CodeCSS(this.CSS);
            string myId = "masterObj" + Project.IncrementedTraceCounter.ToString();
            OutputHTML html = new OutputHTML();

            output.HTML.Append(this.HTMLBefore);

            ParentConstraint newInfos = Routines.ComputeMasterObject(parentConstraint, this);
            ConstraintSize cs = new ConstraintSize(newInfos.constraintWidth, newInfos.precedingWidth, newInfos.maximumWidth, newInfos.constraintHeight, newInfos.precedingHeight, newInfos.maximumHeight);

            Routines.SetCSSPart(myCss, cs);
            myCss.Ids = "#" + myId;
            Routines.SetObjectDisposition(parentConstraint, myCss, newInfos);
            html.CSS.Append(myCss.GenerateCSS(true, true, true));
            html.JavaScript.Append(this.JavaScript.GeneratedCode);
            html.JavaScriptOnLoad.Append(this.JavaScriptOnLoad.GeneratedCode);

            List<MasterObject> list = new List<MasterObject>();
            list.Add(this);

            // generate global Container
            OutputHTML global = new OutputHTML();
            bool hasGlobalContainer = Routines.WriteProductionGlobalContainer(this.Name, this.Id, global, this.Objects, refPage, masterRefPage, list, parentConstraint, cs);

            html.HTML.Append("<div id='" + myId + "' name='" + newInfos.objectName + "' " + cs.attributeWidth + " " + cs.attributeHeight + ">");

            foreach (HorizontalZone hz in this.HorizontalZones)
            {
                OutputHTML hzone = hz.GenerateProductionDIV(refPage, masterRefPage, list, newInfos);
                html.HTML.Append(hzone.HTML.ToString());
                html.CSS.Append(hzone.CSS.ToString());
                html.JavaScript.Append(hzone.JavaScript.ToString());
                html.JavaScriptOnLoad.Append(hzone.JavaScriptOnLoad.ToString());
            }
            html.HTML.Append("</div>");

            if (hasGlobalContainer)
            {
                StringBuilder group = new StringBuilder();
                group.Append("<div style='position:relative' id='group_" + myId + "' name='group_" + this.Name + "' " + cs.attributeWidth + " " + cs.attributeHeight + ">");
                group.Append(global.HTML.ToString());
                group.Append(html.HTML.ToString());
                group.Append("</div>");
                output.HTML.Append(group.ToString());
                output.CSS.Append(global.CSS.ToString());
                output.JavaScript.Append(global.JavaScript.ToString());
                output.JavaScriptOnLoad.Append(global.JavaScriptOnLoad.ToString());
                output.CSS.Append(html.CSS.ToString());
                output.JavaScript.Append(html.JavaScript.ToString());
                output.JavaScriptOnLoad.Append(html.JavaScriptOnLoad.ToString());
            }
            else
            {
                output.HTML.Append(html.HTML.ToString());
                output.CSS.Append(html.CSS.ToString());
                output.JavaScript.Append(html.JavaScript.ToString());
                output.JavaScriptOnLoad.Append(html.JavaScriptOnLoad.ToString());
            }

            output.HTML.Append(this.HTMLAfter);
            return output;
        }

        /// <summary>
        /// Generate an HTML DIV tag for actual website
        /// a page contains master objects related
        /// </summary>
        /// <param name="refPage">page reference</param>
        /// <param name="masterRefPage">master page reference</param>
        /// <param name="objects">master objects</param>
        /// <param name="parentConstraint">parent constraint</param>
        /// <returns>html output</returns>
        public OutputHTML GenerateProductionDIV(Page refPage, MasterPage masterRefPage, List<MasterObject> objects, ParentConstraint parentConstraint)
        {
            OutputHTML output = new OutputHTML();
            CodeCSS myCss = new CodeCSS(this.CSS);
            string myId = "masterObj" + Project.IncrementedTraceCounter.ToString();
            OutputHTML html = new OutputHTML();

            output.HTML.Append(this.HTMLBefore);

            ParentConstraint newInfos = Routines.ComputeMasterObject(parentConstraint, this);
            ConstraintSize cs = new ConstraintSize(newInfos.constraintWidth, newInfos.precedingWidth, newInfos.maximumWidth, newInfos.constraintHeight, newInfos.precedingHeight, newInfos.maximumHeight);

            myCss.Ids = "#" + myId;
            Routines.SetCSSPart(myCss, cs);
            Routines.SetObjectDisposition(parentConstraint, myCss, newInfos);
            html.CSS.Append(myCss.GenerateCSS(true, true, true));
            html.JavaScript.Append(this.JavaScript.GeneratedCode);
            html.JavaScriptOnLoad.Append(this.JavaScriptOnLoad.GeneratedCode);

            objects.Add(this);

            // generate global Container
            OutputHTML global = new OutputHTML();
            bool hasGlobalContainer = Routines.WriteProductionGlobalContainer(this.Name, this.Id, global, this.Objects, refPage, masterRefPage, objects, parentConstraint, cs);

            html.HTML.Append("<div id='" + myId + "' name='" + newInfos.objectName + "' " + cs.attributeWidth + " " + cs.attributeHeight + ">");

            foreach (HorizontalZone hz in this.HorizontalZones)
            {
                OutputHTML hzone = hz.GenerateProductionDIV(refPage, masterRefPage, objects, newInfos);
                html.HTML.Append(hzone.HTML.ToString());
                html.CSS.Append(hzone.CSS.ToString());
                html.JavaScript.Append(hzone.JavaScript.ToString());
                html.JavaScriptOnLoad.Append(hzone.JavaScriptOnLoad.ToString());
            }
            html.HTML.Append("</div>");

            if (hasGlobalContainer)
            {
                StringBuilder group = new StringBuilder();
                group.Append("<div style='position:relative' id='group_" + myId + "' name='group_" + this.Name + "' " + cs.attributeWidth + " " + cs.attributeHeight + ">");
                group.Append(global.HTML.ToString());
                group.Append(html.HTML.ToString());
                group.Append("</div>");
                output.HTML.Append(group.ToString());
                output.CSS.Append(global.CSS.ToString());
                output.JavaScript.Append(global.JavaScript.ToString());
                output.JavaScriptOnLoad.Append(global.JavaScriptOnLoad.ToString());
                output.CSS.Append(html.CSS.ToString());
                output.JavaScript.Append(html.JavaScript.ToString());
                output.JavaScriptOnLoad.Append(html.JavaScriptOnLoad.ToString());
            }
            else
            {
                output.HTML.Append(html.HTML.ToString());
                output.CSS.Append(html.CSS.ToString());
                output.JavaScript.Append(html.JavaScript.ToString());
                output.JavaScriptOnLoad.Append(html.JavaScriptOnLoad.ToString());
            }
            
            output.HTML.Append(this.HTMLAfter);
            return output;
        }

        /// <summary>
        /// Generate an HTML TABLE tag from a page for actual website
        /// This function is not used for actual website but in design only
        /// </summary>
        /// <param name="refPage">page reference</param>
        /// <returns>html output</returns>
        public OutputHTML GenerateProductionTable(Page refPage)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Generate an HTML TABLE tag for actual website
        /// a given master page generates the page
        /// </summary>
        /// <param name="refPage">page reference</param>
        /// <param name="masterRefPage">master page reference</param>
        /// <param name="parentConstraint">parent constraint</param>
        /// <returns>html output</returns>
        public OutputHTML GenerateProductionTable(Page refPage, MasterPage masterRefPage, ParentConstraint parentConstraint)
        {
            OutputHTML output = new OutputHTML();
            CodeCSS myCss = new CodeCSS(this.CSS);
            string myId = "masterObj" + Project.IncrementedTraceCounter.ToString();
            OutputHTML html = new OutputHTML();

            output.HTML.Append(this.HTMLBefore);

            ParentConstraint newInfos = Routines.ComputeMasterObject(parentConstraint, this);
            ConstraintSize cs = new ConstraintSize(newInfos.constraintWidth, newInfos.precedingWidth, newInfos.maximumWidth, newInfos.constraintHeight, newInfos.precedingHeight, newInfos.maximumHeight);

            myCss.Ids = "#" + myId;
            Routines.SetCSSPart(myCss, cs);
            Routines.SetObjectDisposition(parentConstraint, myCss, newInfos);
            html.CSS.Append(myCss.GenerateCSS(true, true, true));
            html.JavaScript.Append(this.JavaScript.GeneratedCode);
            html.JavaScriptOnLoad.Append(this.JavaScriptOnLoad.GeneratedCode);

            List<MasterObject> list = new List<MasterObject>();
            list.Add(this);

            // generate global Container
            OutputHTML global = new OutputHTML();
            bool hasGlobalContainer = Routines.WriteProductionGlobalContainer(this.Name, this.Id, global, this.Objects, refPage, masterRefPage, list, parentConstraint, cs);

            html.HTML.Append("<table " + cs.attributeWidth + " " + cs.attributeHeight + " id='" + myId + "' name='" + newInfos.objectName + "' border='0' cellspacing='0' cellpadding='0'>");

            // Si la dernière ligne de la table est vide alors on ne l'ajoute pas
            // raison : compatibité IE/Firefox/Chrome
            // recherche fin de ligne
            int rechfin = this.HorizontalZones.Count;
            for (int index = this.HorizontalZones.Count - 1; index >= 0; --index)
            {
                if (this.HorizontalZones[index].VerticalZones.Count != 0)
                {
                    rechfin = index + 1;
                    break;
                }
            }
            for (int index = 0; index < rechfin; ++index)
            {
                HorizontalZone hz = this.HorizontalZones[index];
                if (index + 1 < this.HorizontalZones.Count || hz.VerticalZones.Count > 0)
                {
                    OutputHTML hzone = hz.GenerateProductionTable(refPage, masterRefPage, list, newInfos);
                    html.HTML.Append(hzone.HTML.ToString());
                    html.CSS.Append(hzone.CSS.ToString());
                    html.JavaScript.Append(hzone.JavaScript.ToString());
                    html.JavaScriptOnLoad.Append(hzone.JavaScriptOnLoad.ToString());
                }
            }
            html.HTML.Append("</table>");

            if (hasGlobalContainer)
            {
                StringBuilder group = new StringBuilder();
                group.Append("<div style='position:relative' id='group_" + myId + "' name='group_" + this.Name + "' " + cs.attributeWidth + " " + cs.attributeHeight + ">");
                group.Append(global.HTML.ToString());
                group.Append(html.HTML.ToString());
                group.Append("</div>");
                output.HTML.Append(group.ToString());
                output.CSS.Append(global.CSS.ToString());
                output.JavaScript.Append(global.JavaScript.ToString());
                output.JavaScriptOnLoad.Append(global.JavaScriptOnLoad.ToString());
                output.CSS.Append(html.CSS.ToString());
                output.JavaScript.Append(html.JavaScript.ToString());
                output.JavaScriptOnLoad.Append(html.JavaScriptOnLoad.ToString());
            }
            else
            {
                output.HTML.Append(html.HTML.ToString());
                output.CSS.Append(html.CSS.ToString());
                output.JavaScript.Append(html.JavaScript.ToString());
                output.JavaScriptOnLoad.Append(html.JavaScriptOnLoad.ToString());
            }

            output.HTML.Append(this.HTMLAfter);
            return output;
        }

        /// <summary>
        /// Generate an HTML TABLE tag for actual website
        /// a given master page generates the page
        /// a page contains master objects related
        /// restricted objects in page are computed equally
        /// </summary>
        /// <param name="refPage">page reference</param>
        /// <param name="masterRefPage">master page reference</param>
        /// <param name="objects">master objects list</param>
        /// <param name="parentConstraint">parent constraint</param>
        /// <returns>html output</returns>
        public OutputHTML GenerateProductionTable(Page refPage, MasterPage masterRefPage, List<MasterObject> objects, ParentConstraint parentConstraint)
        {
            OutputHTML output = new OutputHTML();
            CodeCSS myCss = new CodeCSS(this.CSS);
            string myId = "masterObj" + Project.IncrementedTraceCounter.ToString();
            OutputHTML html = new OutputHTML();

            output.HTML.Append(this.HTMLBefore);

            ParentConstraint newInfos = Routines.ComputeMasterObject(parentConstraint, this);
            ConstraintSize cs = new ConstraintSize(newInfos.constraintWidth, newInfos.precedingWidth, newInfos.maximumWidth, newInfos.constraintHeight, newInfos.precedingHeight, newInfos.maximumHeight);

            myCss.Ids = "#" + myId;
            Routines.SetCSSPart(myCss, cs);
            Routines.SetObjectDisposition(parentConstraint, myCss, newInfos);
            html.CSS.Append(myCss.GenerateCSS(true, true, true));
            html.JavaScript.Append(this.JavaScript.GeneratedCode);
            html.JavaScriptOnLoad.Append(this.JavaScriptOnLoad.GeneratedCode);

            // generate global Container
            OutputHTML global = new OutputHTML();
            bool hasGlobalContainer = Routines.WriteProductionGlobalContainer(this.Name, this.Id, global, this.Objects, refPage, masterRefPage, objects, parentConstraint, cs);

            html.HTML.Append("<table " + cs.attributeWidth + " " + cs.attributeHeight + " id='" + myId + "' name='" + newInfos.objectName + "' border='0' cellspacing='0' cellpadding='0'>");

            objects.Add(this);

            // Si la dernière ligne de la table est vide alors on ne l'ajoute pas
            // raison : compatibité IE/Firefox/Chrome
            // recherche fin de ligne
            int rechfin = this.HorizontalZones.Count;
            for (int index = this.HorizontalZones.Count - 1; index >= 0; --index)
            {
                if (this.HorizontalZones[index].VerticalZones.Count != 0)
                {
                    rechfin = index + 1;
                    break;
                }
            }
            for (int index = 0; index < rechfin; ++index)
            {
                HorizontalZone hz = this.HorizontalZones[index];
                if (index + 1 < this.HorizontalZones.Count || hz.VerticalZones.Count > 0)
                {
                    OutputHTML hzone = hz.GenerateProductionTable(refPage, masterRefPage, objects, newInfos);
                    html.HTML.Append(hzone.HTML.ToString());
                    html.CSS.Append(hzone.CSS.ToString());
                    html.JavaScript.Append(hzone.JavaScript.ToString());
                    html.JavaScriptOnLoad.Append(hzone.JavaScriptOnLoad.ToString());
                }
            }
            html.HTML.Append("</table>");

            if (hasGlobalContainer)
            {
                StringBuilder group = new StringBuilder();
                group.Append("<div style='position:relative' id='group_" + myId + "' name='group_" + this.Name + "' " + cs.attributeWidth + " " + cs.attributeHeight + ">");
                group.Append(global.HTML.ToString());
                group.Append(html.HTML.ToString());
                group.Append("</div>");
                output.HTML.Append(group.ToString());
                output.CSS.Append(global.CSS.ToString());
                output.JavaScript.Append(global.JavaScript.ToString());
                output.JavaScriptOnLoad.Append(global.JavaScriptOnLoad.ToString());
                output.CSS.Append(html.CSS.ToString());
                output.JavaScript.Append(html.JavaScript.ToString());
                output.JavaScriptOnLoad.Append(html.JavaScriptOnLoad.ToString());
            }
            else
            {
                output.HTML.Append(html.HTML.ToString());
                output.CSS.Append(html.CSS.ToString());
                output.JavaScript.Append(html.JavaScript.ToString());
                output.JavaScriptOnLoad.Append(html.JavaScriptOnLoad.ToString());
            }

            output.HTML.Append(this.HTMLAfter);
            return output;
        }
        #endregion

        /// <summary>
        /// Clone this object
        /// </summary>
        /// <returns>cloned object</returns>
        public object Clone()
        {
            MasterObject mo = new MasterObject(this);
            return mo;
        }
    }
}
