using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library
{
    /// <summary>
    /// Defines a vertical area
    /// A vertical area is a cell from a table with rows and columns
    /// A vertical area is a host container for objects html, master object, tool or table
    /// </summary>
    [Serializable]
    public class VerticalZone : Marshalling.PersistentDataObject, IContainer, IGenerateDesignDIV, IGenerateDesignTable, IGenerateProductionDIV, IGenerateProductionTable, ICloneable
    {

        #region Fields

        /// <summary>
        /// Index name for disposition
        /// </summary>
        protected static readonly string dispositionName = "disposition";
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
        /// Index name for counting lines
        /// </summary>
        protected static readonly string countingLinesName = "countingLines";
        /// <summary>
        /// Index name for counting column
        /// </summary>
        protected static readonly string countingColumnsName = "countingColumns";
        /// <summary>
        /// Index name for width value
        /// </summary>
        protected static readonly string widthName = "width";
        /// <summary>
        /// Index name for height value
        /// </summary>
        protected static readonly string heightName = "height";
        /// <summary>
        /// Index name for events
        /// </summary>
        protected static readonly string eventsName = "events";
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


        #endregion

        #region Default Constructor

        /// <summary>
        /// Empty constructor
        /// </summary>
        public VerticalZone()
        {
            int val = Project.CurrentProject.IncrementedCounter;
            this.Set(automaticNameName, String.Format("verti{0}", val));
            this.Set(automaticIdName, String.Format("idVerti{0}", val));
        }

        #endregion

        #region Copy constructor

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="vz">vertical zone to copy from</param>
        private VerticalZone(VerticalZone vz)
        {
            int val = Project.CurrentProject.IncrementedCounter;
            this.Set(automaticNameName, String.Format("verti{0}", val));
            this.Set(automaticIdName, String.Format("idVerti{0}", val));

            this.Disposition = vz.Disposition;
            this.ConstraintWidth = vz.ConstraintWidth;
            this.ConstraintHeight = vz.ConstraintHeight;
            this.Width = vz.Width;
            this.Height = vz.Height;
            this.CountLines = vz.CountLines;
            this.Set(eventsName, this.Events.Clone());
            this.Set(javascriptName, vz.JavaScript.Clone());
            this.Set(javascriptOnloadName, vz.JavaScriptOnLoad.Clone());
            this.Set(cssName, vz.CSS.Clone());
            this.CSS.Ids = "#" + this.Get(automaticIdName);
        }
        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the disposition
        /// </summary>
        public Disposition Disposition
        {
            get { return this.Get(dispositionName, new Disposition()); }
            set { this.Set(dispositionName, value); }
        }

        /// <summary>
        /// Gets or sets a disposition as a string text
        /// </summary>
        public string DispositionText
        {
            get { return this.Disposition.ToString(); }
            set
            {
                Disposition d;
                if (Enum.TryParse(value, out d))
                    this.Disposition = d;
            }
        }

        /// <summary>
        /// Gets or sets the width constraint
        /// </summary>
        public EnumConstraint ConstraintWidth
        {
            get { return this.Get(constraintWidthName, EnumConstraint.AUTO); }
            set { this.Set(constraintWidthName, value); }
        }

        /// <summary>
        /// Gets or sets the height constraint
        /// </summary>
        public EnumConstraint ConstraintHeight
        {
            get { return this.Get(constraintHeightName, EnumConstraint.AUTO); }
            set { this.Set(constraintHeightName, value); }
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
        /// Gets or sets the automatic id
        /// </summary>
        public string Id
        {
            get { return this.Get(automaticIdName); }
            set { this.Set(automaticIdName, value); }
        }

        /// <summary>
        /// Gets or sets the counting lines
        /// </summary>
        public int CountLines
        {
            get { return this.Get(countingLinesName, 0); }
            set { this.Set(countingLinesName, value); }
        }

        /// <summary>
        /// Gets or sets the counting columns
        /// </summary>
        public int CountColumns
        {
            get { return this.Get(countingColumnsName, 0); }
            set { this.Set(countingColumnsName, value); }
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
        /// Gets the css code
        /// </summary>
        public CodeCSS CSS
        {
            get { return this.Get(cssName, new CodeCSS()); }
        }

        /// <summary>
        /// Gets events
        /// </summary>
        public Events Events
        {
            get { return this.Get(eventsName, new Events()); }
        }

        /// <summary>
        /// Gets the javascript code
        /// </summary>
        public CodeJavaScript JavaScript
        {
            get { return this.Get(javascriptName, new CodeJavaScript()); }
        }

        /// <summary>
        /// Gets the javascript on load code
        /// </summary>
        public CodeJavaScript JavaScriptOnLoad
        {
            get { return this.Get(javascriptOnloadName, new CodeJavaScript()); }
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
        /// Gets or sets the javascript source
        /// </summary>
        public string JavaScriptSource
        {
            get { return this.JavaScript.Code; }
            set { this.JavaScript.Code = value; }
        }

        /// <summary>
        /// Stringified vertical area
        /// for debugging
        /// </summary>
        public string Stringified
        {
            get { return String.Format(Localization.Strings.GetString("VerticalAreaStringified"), this.Name, this.CountLines, this.CountColumns); }
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
            if (this.Name == searchName)
            {
                found = this;
                done = true;
            }
            else
            {
                foreach (IContent cont in objects)
                {
                    if (cont.Container == this.Name && cont.SearchContainer(containers, objects, searchName, out found))
                    {
                        done = true;
                        break;
                    }
                }
            }
            return done;
        }

        /// <summary>
        /// Generate an HTML DIV tag from null for design
        /// A vertical area is hosted by an inner HTML tag
        /// this inner HTML tag must exist to work fine
        /// so, a vertical area do not generate a free DIV
        /// </summary>
        /// <returns>html output</returns>
        public OutputHTML GenerateDesignDIV()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Generate an HTML DIV tag for design
        /// a given master page generates the page
        /// restricted objects in page are computed equally
        /// </summary>
        /// <param name="refPage">page reference</param>
        /// <param name="masterRefPage">master page reference</param>
        /// <param name="parentConstraint">parent constraint</param>
        /// <returns>html output</returns>
        public OutputHTML GenerateDesignDIV(Page refPage, MasterPage masterRefPage, ParentConstraint parentConstraint)
        {
            OutputHTML output = new OutputHTML();
            CodeCSS myCss = new CodeCSS(this.CSS);
            string myId = "verti" + Project.IncrementedTraceCounter.ToString();

            ParentConstraint newInfos = Routines.ComputeVerticalZone(parentConstraint, this);
            ConstraintSize cs = new ConstraintSize(newInfos.constraintWidth, newInfos.precedingWidth, newInfos.maximumWidth, newInfos.constraintHeight, newInfos.precedingHeight, newInfos.maximumHeight);

            output.HTML.Append("<div");
            output.HTML.Append(" title='" + Routines.PrintTipSize(newInfos.objectName, this.Name, cs) + "'");
            output.HTML.Append(" id='" + myId + "'");
            output.HTML.Append(" name='" + (String.IsNullOrEmpty(newInfos.objectName) ? this.Name : newInfos.objectName + "_" + this.Name) + "'");
            if (this.Events.Count > 0)
                output.HTML.Append(" " + this.Events.ToHTMLString());
            if (!String.IsNullOrEmpty(cs.attributeWidth))
                output.HTML.Append(" " + cs.attributeWidth);
            if (!String.IsNullOrEmpty(cs.attributeHeight))
                output.HTML.Append(" " + cs.attributeHeight);
            output.HTML.Append(">");

            // set css part
            myCss.Ids = "#" + myId;
            if (!CommonDirectories.ConfigDirectories.RemoveTables)
            {
                if (!myCss.Body.AllKeys.Contains("float"))
                    myCss.Body.Add("float", "left");
            }
            Routines.SetCSSPart(myCss, cs);

            OutputHTML zone = new OutputHTML();

            HTMLObject objPage = refPage.Objects.Find(a => { return a.Container == this.Name || (String.IsNullOrEmpty(newInfos.objectName) ? false : a.Container == newInfos.objectName + "_" + this.Name); });
            HTMLObject objMasterPage = null;
            if (masterRefPage != null)
                objMasterPage = masterRefPage.Objects.Find(a => { return a.Container == this.Name || (String.IsNullOrEmpty(newInfos.objectName) ? false : a.Container == newInfos.objectName + "_" + this.Name); });
            if (objPage != null)
            {
                zone = objPage.GenerateDesign(refPage, masterRefPage, newInfos);
                refPage.SpecificOutput.CSS.Append(zone.CSS.ToString());
                refPage.SpecificOutput.JavaScript.Append(zone.JavaScript.ToString());
                refPage.SpecificOutput.JavaScriptOnLoad.Append(zone.JavaScriptOnLoad.ToString());
            }
            else if (objMasterPage != null)
            {
                zone = objMasterPage.GenerateDesign(refPage, masterRefPage, newInfos);
                output.CSS.Append(zone.CSS.ToString());
                output.JavaScript.Append(zone.JavaScript.ToString());
                output.JavaScriptOnLoad.Append(zone.JavaScriptOnLoad.ToString());
            }
            else
            {
                zone.HTML.Append("<img " + cs.ComputeStyle() + " src='ehd_ask.png' onclick='callback(this);'/>");
                output.CSS.Append(zone.CSS.ToString());
                output.JavaScript.Append(zone.JavaScript.ToString());
                output.JavaScriptOnLoad.Append(zone.JavaScriptOnLoad.ToString());
            }

            // compute disposition
            Routines.SetDIVDisposition(output.HTML, this.Disposition, zone.HTML);

            output.HTML.Append("</div>");
            output.CSS.Append(myCss.GenerateCSS(true, true, true));
            output.JavaScript.Append(this.JavaScript.GeneratedCode);
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
            string myId = "verti" + Project.IncrementedTraceCounter.ToString();

            // compute size
            ParentConstraint newInfos = Routines.ComputeVerticalZone(parentConstraint, this);
            ConstraintSize cs = new ConstraintSize(newInfos.constraintWidth, newInfos.precedingWidth, newInfos.maximumWidth, newInfos.constraintHeight, newInfos.precedingHeight, newInfos.maximumHeight);

            output.HTML.Append("<div");
            output.HTML.Append(" title='" + Routines.PrintTipSize(newInfos.objectName, this.Name, cs) + "'");
            output.HTML.Append(" id='" + myId + "'");
            output.HTML.Append(" name='" + (String.IsNullOrEmpty(newInfos.objectName) ? this.Name : newInfos.objectName + "_" + this.Name) + "'");
            if (this.Events.Count > 0)
                output.HTML.Append(" " + this.Events.ToHTMLString());
            if (!String.IsNullOrEmpty(cs.attributeWidth))
                output.HTML.Append(" " + cs.attributeWidth);
            if (!String.IsNullOrEmpty(cs.attributeHeight))
                output.HTML.Append(" " + cs.attributeHeight);
            output.HTML.Append(">");

            // set css part
            myCss.Ids = "#" + myId;
            if (!CommonDirectories.ConfigDirectories.RemoveTables)
            {
                if (!myCss.Body.AllKeys.Contains("float"))
                    myCss.Body.Add("float", "left");
            }
            Routines.SetCSSPart(myCss, cs);

            OutputHTML zone = new OutputHTML();

            HTMLObject objPage = refPage.Objects.Find(a => { return a.Container == this.Name || (String.IsNullOrEmpty(newInfos.objectName) ? false : a.Container == newInfos.objectName + "_" + this.Name); });
            List<HTMLObject> list = new List<HTMLObject>();
            foreach (MasterObject mObj in objects)
            {
                list.AddRange(mObj.Objects);
            }
            HTMLObject objHtmlObject = list.Find(a => { return a.Container == this.Name || (String.IsNullOrEmpty(newInfos.objectName) ? false : a.Container == newInfos.objectName + "_" + this.Name); });
            if (objPage != null)
            {
                zone = objPage.GenerateDesign(refPage, objects, newInfos);
                refPage.SpecificOutput.CSS.Append(zone.CSS.ToString());
                refPage.SpecificOutput.JavaScript.Append(zone.JavaScript.ToString());
                refPage.SpecificOutput.JavaScriptOnLoad.Append(zone.JavaScriptOnLoad.ToString());
            }
            else if (objHtmlObject != null)
            {
                zone = objHtmlObject.GenerateDesign(refPage, objects, newInfos);
                output.CSS.Append(zone.CSS.ToString());
                output.JavaScript.Append(zone.JavaScript.ToString());
                output.JavaScriptOnLoad.Append(zone.JavaScriptOnLoad.ToString());
            }
            else
            {
                zone.HTML.Append("<img " + cs.ComputeStyle() + " src='ehd_ask.png' onclick='callback(this);'/>");
                output.CSS.Append(zone.CSS.ToString());
                output.JavaScript.Append(zone.JavaScript.ToString());
                output.JavaScriptOnLoad.Append(zone.JavaScriptOnLoad.ToString());
            }

            // compute disposition
            Routines.SetDIVDisposition(output.HTML, this.Disposition, zone.HTML);

            output.HTML.Append("</div>");
            output.CSS.Append(myCss.GenerateCSS(true, true, true));
            output.JavaScript.Append(this.JavaScript.GeneratedCode);
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
            string myId = "verti" + Project.IncrementedTraceCounter.ToString();

            ParentConstraint newInfos = Routines.ComputeVerticalZone(parentConstraint, this);
            ConstraintSize cs = new ConstraintSize(newInfos.constraintWidth, newInfos.precedingWidth, newInfos.maximumWidth, newInfos.constraintHeight, newInfos.precedingHeight, newInfos.maximumHeight);

            output.HTML.Append("<div");
            output.HTML.Append(" title='" + Routines.PrintTipSize(newInfos.objectName, this.Name, cs) + "'");
            output.HTML.Append(" id='" + myId + "'");
            output.HTML.Append(" name='" + (String.IsNullOrEmpty(newInfos.objectName) ? this.Name : newInfos.objectName + "_" + this.Name) + "'");
            if (this.Events.Count > 0)
                output.HTML.Append(" " + this.Events.ToHTMLString());
            if (!String.IsNullOrEmpty(cs.attributeWidth))
                output.HTML.Append(" " + cs.attributeWidth);
            if (!String.IsNullOrEmpty(cs.attributeHeight))
            output.HTML.Append(" " + cs.attributeHeight);
            output.HTML.Append(">");

            // set css part
            myCss.Ids = "#" + myId;
            if (!CommonDirectories.ConfigDirectories.RemoveTables)
            {
                if (!myCss.Body.AllKeys.Contains("float"))
                    myCss.Body.Add("float", "left");
            }
            Routines.SetCSSPart(myCss, cs);

            OutputHTML zone = new OutputHTML();

            HTMLObject objPage = refPage.Objects.Find(a => { return a.Container == this.Name || (String.IsNullOrEmpty(newInfos.objectName) ? false : a.Container == newInfos.objectName + "_" + this.Name); });
            HTMLObject objMasterPage = null;
            if (masterRefPage != null)
                objMasterPage = masterRefPage.Objects.Find(a => { return a.Container == this.Name || (String.IsNullOrEmpty(newInfos.objectName) ? false : a.Container == newInfos.objectName + "_" + this.Name); });
            List<HTMLObject> list = new List<HTMLObject>();
            foreach (MasterObject mObj in objects)
            {
                list.AddRange(mObj.Objects);
            }
            HTMLObject objHtmlObject = list.Find(a => { return a.Container == this.Name || (String.IsNullOrEmpty(newInfos.objectName) ? false : a.Container == newInfos.objectName + "_" + this.Name); });
            if (objPage != null)
            {
                zone = objPage.GenerateDesign(refPage, masterRefPage, objects, newInfos);
                refPage.SpecificOutput.CSS.Append(zone.CSS.ToString());
                refPage.SpecificOutput.JavaScript.Append(zone.JavaScript.ToString());
                refPage.SpecificOutput.JavaScriptOnLoad.Append(zone.JavaScriptOnLoad.ToString());
            }
            else if (objHtmlObject != null)
            {
                zone = objHtmlObject.GenerateDesign(refPage, masterRefPage, objects, newInfos);
                output.CSS.Append(zone.CSS.ToString());
                output.JavaScript.Append(zone.JavaScript.ToString());
                output.JavaScriptOnLoad.Append(zone.JavaScriptOnLoad.ToString());
            }
            else if (objMasterPage != null)
            {
                zone = objMasterPage.GenerateDesign(refPage, masterRefPage, objects, newInfos);
                output.CSS.Append(zone.CSS.ToString());
                output.JavaScript.Append(zone.JavaScript.ToString());
                output.JavaScriptOnLoad.Append(zone.JavaScriptOnLoad.ToString());
            }
            else
            {
                zone.HTML.Append("<img " + cs.ComputeStyle() + " src='ehd_ask.png' onclick='callback(this);'/>");
                output.CSS.Append(zone.CSS.ToString());
                output.JavaScript.Append(zone.JavaScript.ToString());
                output.JavaScriptOnLoad.Append(zone.JavaScriptOnLoad.ToString());
            }

            // compute disposition
            Routines.SetDIVDisposition(output.HTML, this.Disposition, zone.HTML);

            output.HTML.Append("</div>");
            output.CSS.Append(myCss.GenerateCSS(true, true, true));
            output.JavaScript.Append(this.JavaScript.GeneratedCode);
            return output;
        }


        /// <summary>
        /// Generate an HTML DIV tag from null for design
        /// A vertical area is hosted by an inner HTML tag table
        /// this inner HTML tag table must exist to work fine
        /// so, a vertical area do not generate a direct DIV starting at a page
        /// </summary>
        /// <returns>html output</returns>
        public OutputHTML GenerateDesignDIV(Page refPage)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Generate an HTML TABLE tag from null for design
        /// A vertical area is hosted by an inner HTML tag table
        /// this inner HTML tag table must exist to work fine
        /// so, a vertical area do not generate a direct TABLE starting at a page
        /// </summary>
        /// <returns>html output</returns>
        public OutputHTML GenerateDesignTable(Page refPage)
        {
            throw new NotImplementedException();
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
            string myId = "verti" + Project.IncrementedTraceCounter.ToString();

            ParentConstraint newInfos = Routines.ComputeVerticalZone(parentConstraint, this);
            ConstraintSize cs = new ConstraintSize(newInfos.constraintWidth, newInfos.precedingWidth, newInfos.maximumWidth, newInfos.constraintHeight, newInfos.precedingHeight, newInfos.maximumHeight);

            output.HTML.Append("<td");
            output.HTML.Append(" id='" + myId + "'");
            output.HTML.Append(" name='" + (String.IsNullOrEmpty(newInfos.objectName) ? this.Name : newInfos.objectName) + "'");
            output.HTML.Append(" " + Routines.SetTableDisposition(this.Disposition));
            output.HTML.Append(" title='" + Routines.PrintTipSize(newInfos.objectName, this.Name, cs) + "'");
            output.HTML.Append(" rowspan='" + this.CountLines.ToString() + "'");
            output.HTML.Append(" colspan='" + this.CountColumns.ToString() + "'");
            if (this.Events.Count > 0)
                output.HTML.Append(" " + this.Events.ToHTMLString());
            if (!String.IsNullOrEmpty(cs.attributeWidth))
                output.HTML.Append(" " + cs.attributeWidth);
            if (!String.IsNullOrEmpty(cs.attributeHeight))
                output.HTML.Append(" " + cs.attributeHeight);
            output.HTML.Append(">");

            // set css part
            myCss.Ids = "#" + myId;
            Routines.SetCSSPart(myCss, cs);

            OutputHTML zone = new OutputHTML();

            HTMLObject objPage = refPage.Objects.Find(a => { return a.Container == this.Name || (String.IsNullOrEmpty(newInfos.objectName) ? false : a.Container == newInfos.objectName + "_" + this.Name); });
            HTMLObject objMasterPage = null;
            if (masterRefPage != null)
                objMasterPage = masterRefPage.Objects.Find(a => { return a.Container == this.Name || (String.IsNullOrEmpty(newInfos.objectName) ? false : a.Container == newInfos.objectName + "_" + this.Name); });
            if (objPage != null)
            {
                zone = objPage.GenerateDesign(refPage, masterRefPage, newInfos);
                output.HTML.Append(zone.HTML.ToString());
                refPage.SpecificOutput.CSS.Append(zone.CSS.ToString());
                refPage.SpecificOutput.JavaScript.Append(zone.JavaScript.ToString());
                refPage.SpecificOutput.JavaScriptOnLoad.Append(zone.JavaScriptOnLoad.ToString());
            }
            else if (objMasterPage != null)
            {
                zone = objMasterPage.GenerateDesign(refPage, masterRefPage, newInfos);
                output.HTML.Append(zone.HTML.ToString());
                output.CSS.Append(zone.CSS.ToString());
                output.JavaScript.Append(zone.JavaScript.ToString());
                output.JavaScriptOnLoad.Append(zone.JavaScriptOnLoad.ToString());
            }
            else
            {
                zone.HTML.Append("<img " + cs.ComputeStyle() + " src='ehd_ask.png' onclick='callback(this);'/>");
                output.HTML.Append(zone.HTML.ToString());
                output.CSS.Append(zone.CSS.ToString());
                output.JavaScript.Append(zone.JavaScript.ToString());
                output.JavaScriptOnLoad.Append(zone.JavaScriptOnLoad.ToString());
            }
            output.HTML.Append("</td>");
            output.CSS.Append(myCss.GenerateCSS(true, true, true));
            output.JavaScript.Append(this.JavaScript.GeneratedCode);
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
            string myId = "verti" + Project.IncrementedTraceCounter.ToString();

            ParentConstraint newInfos = Routines.ComputeVerticalZone(parentConstraint, this);
            ConstraintSize cs = new ConstraintSize(newInfos.constraintWidth, newInfos.precedingWidth, newInfos.maximumWidth, newInfos.constraintHeight, newInfos.precedingHeight, newInfos.maximumHeight);

            output.HTML.Append("<td");
            output.HTML.Append(" id='" + myId + "'");
            output.HTML.Append(" name='" + (String.IsNullOrEmpty(newInfos.objectName) ? this.Name : newInfos.objectName) + "'");
            output.HTML.Append(" " + Routines.SetTableDisposition(this.Disposition));
            output.HTML.Append(" title='" + Routines.PrintTipSize(newInfos.objectName, this.Name, cs) + "'");
            output.HTML.Append(" rowspan='" + this.CountLines.ToString() + "'");
            output.HTML.Append(" colspan='" + this.CountColumns.ToString() + "'");
            if (this.Events.Count > 0)
                output.HTML.Append(" " + this.Events.ToHTMLString());
            if (!String.IsNullOrEmpty(cs.attributeWidth))
                output.HTML.Append(" " + cs.attributeWidth);
            if (!String.IsNullOrEmpty(cs.attributeHeight))
                output.HTML.Append(" " + cs.attributeHeight);
            output.HTML.Append(">");

            // set css part
            myCss.Ids = "#" + myId;
            Routines.SetCSSPart(myCss, cs);

            OutputHTML zone = new OutputHTML();

            HTMLObject objPage = refPage.Objects.Find(a => { return a.Container == this.Name || (String.IsNullOrEmpty(newInfos.objectName) ? false : a.Container == newInfos.objectName + "_" + this.Name); });
            List<HTMLObject> list = new List<HTMLObject>();
            foreach (MasterObject mObj in objects)
            {
                list.AddRange(mObj.Objects);
            }
            HTMLObject objHtmlObject = list.Find(a => { return a.Container == this.Name || (String.IsNullOrEmpty(newInfos.objectName) ? false : a.Container == newInfos.objectName + "_" + this.Name); });
            if (objPage != null)
            {
                zone = objPage.GenerateDesign(refPage, objects, newInfos);
                output.HTML.Append(zone.HTML.ToString());
                refPage.SpecificOutput.CSS.Append(zone.CSS.ToString());
                refPage.SpecificOutput.JavaScript.Append(zone.JavaScript.ToString());
                refPage.SpecificOutput.JavaScriptOnLoad.Append(zone.JavaScriptOnLoad.ToString());
            }
            else if (objHtmlObject != null)
            {
                zone = objHtmlObject.GenerateDesign(refPage, objects, newInfos);
                output.HTML.Append(zone.HTML.ToString());
                output.CSS.Append(zone.CSS.ToString());
                output.JavaScript.Append(zone.JavaScript.ToString());
                output.JavaScriptOnLoad.Append(zone.JavaScriptOnLoad.ToString());
            }
            else
            {
                zone.HTML.Append("<img " + cs.ComputeStyle() + " src='ehd_ask.png' onclick='callback(this);'/>");
                output.HTML.Append(zone.HTML.ToString());
                output.CSS.Append(zone.CSS.ToString());
                output.JavaScript.Append(zone.JavaScript.ToString());
                output.JavaScriptOnLoad.Append(zone.JavaScriptOnLoad.ToString());
            }
            output.HTML.Append("</td>");
            output.CSS.Append(myCss.GenerateCSS(true, true, true));
            output.JavaScript.Append(this.JavaScript.GeneratedCode);
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
            string myId = "verti" + Project.IncrementedTraceCounter.ToString();

            ParentConstraint newInfos = Routines.ComputeVerticalZone(parentConstraint, this);
            ConstraintSize cs = new ConstraintSize(newInfos.constraintWidth, newInfos.precedingWidth, newInfos.maximumWidth, newInfos.constraintHeight, newInfos.precedingHeight, newInfos.maximumHeight);

            output.HTML.Append("<td");
            output.HTML.Append(" id='" + myId + "'");
            output.HTML.Append(" name='" + (String.IsNullOrEmpty(newInfos.objectName) ? this.Name : newInfos.objectName) + "'");
            output.HTML.Append(" " + Routines.SetTableDisposition(this.Disposition));
            output.HTML.Append(" title='" + Routines.PrintTipSize(newInfos.objectName, this.Name, cs) + "'");
            output.HTML.Append(" rowspan='" + this.CountLines.ToString() + "'");
            output.HTML.Append(" colspan='" + this.CountColumns.ToString() + "'");
            if (this.Events.Count > 0)
                output.HTML.Append(" " + this.Events.ToHTMLString());
            if (!String.IsNullOrEmpty(cs.attributeWidth))
                output.HTML.Append(" " + cs.attributeWidth);
            if (!String.IsNullOrEmpty(cs.attributeHeight))
                output.HTML.Append(" " + cs.attributeHeight);
            output.HTML.Append(">");

            // set css part
            myCss.Ids = "#" + myId;
            Routines.SetCSSPart(myCss, cs);

            OutputHTML zone = new OutputHTML();

            HTMLObject objPage = refPage.Objects.Find(a => { return a.Container == this.Name || (String.IsNullOrEmpty(newInfos.objectName) ? false : a.Container == newInfos.objectName + "_" + this.Name); });
            HTMLObject objMasterPage = null;
            if (masterRefPage != null)
                objMasterPage = masterRefPage.Objects.Find(a => { return a.Container == this.Name || (String.IsNullOrEmpty(newInfos.objectName) ? false : a.Container == newInfos.objectName + "_" + this.Name); });
            List<HTMLObject> list = new List<HTMLObject>();
            foreach (MasterObject mObj in objects)
            {
                list.AddRange(mObj.Objects);
            }
            HTMLObject objHtmlObject = list.Find(a => { return a.Container == this.Name || (String.IsNullOrEmpty(newInfos.objectName) ? false : a.Container == newInfos.objectName + "_" + this.Name); });
            if (objPage != null)
            {
                zone = objPage.GenerateDesign(refPage, masterRefPage, objects, newInfos);
                output.HTML.Append(zone.HTML.ToString());
                refPage.SpecificOutput.CSS.Append(zone.CSS.ToString());
                refPage.SpecificOutput.JavaScript.Append(zone.JavaScript.ToString());
                refPage.SpecificOutput.JavaScriptOnLoad.Append(zone.JavaScriptOnLoad.ToString());
            }
            else if (objHtmlObject != null)
            {
                zone = objHtmlObject.GenerateDesign(refPage, masterRefPage, objects, newInfos);
                output.HTML.Append(zone.HTML.ToString());
                output.CSS.Append(zone.CSS.ToString());
                output.JavaScript.Append(zone.JavaScript.ToString());
                output.JavaScriptOnLoad.Append(zone.JavaScriptOnLoad.ToString());
            }
            else if (objMasterPage != null)
            {
                zone = objMasterPage.GenerateDesign(refPage, masterRefPage, objects, newInfos);
                output.HTML.Append(zone.HTML.ToString());
                output.CSS.Append(zone.CSS.ToString());
                output.JavaScript.Append(zone.JavaScript.ToString());
                output.JavaScriptOnLoad.Append(zone.JavaScriptOnLoad.ToString());
            }
            else
            {
                zone.HTML.Append("<img " + cs.ComputeStyle() + " src='ehd_ask.png' onclick='callback(this);'/>");
                output.HTML.Append(zone.HTML.ToString());
                output.CSS.Append(zone.CSS.ToString());
                output.JavaScript.Append(zone.JavaScript.ToString());
                output.JavaScriptOnLoad.Append(zone.JavaScriptOnLoad.ToString());
            }
            output.HTML.Append("</td>");
            output.CSS.Append(myCss.GenerateCSS(true, true, true));
            output.JavaScript.Append(this.JavaScript.GeneratedCode);
            return output;
        }

        /// <summary>
        /// Generate an HTML DIV tag from null for actual website
        /// A vertical area is hosted by an inner HTML tag
        /// this inner HTML tag must exist to work fine
        /// so, a vertical area do not generate a free DIV
        /// </summary>
        /// <returns>html output</returns>
        public OutputHTML GenerateProductionDIV(Page refPage)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Generate an HTML DIV tag for actual website
        /// a given master page generates the page
        /// restricted objects in page are computed equally
        /// </summary>
        /// <param name="refPage">page reference</param>
        /// <param name="masterRefPage">master page reference</param>
        /// <param name="parentConstraint">parent constraint</param>
        /// <returns>html output</returns>
        public OutputHTML GenerateProductionDIV(Page refPage, MasterPage masterRefPage, ParentConstraint parentConstraint)
        {
            OutputHTML output = new OutputHTML();
            CodeCSS myCss = new CodeCSS(this.CSS);
            string myId = "verti" + Project.IncrementedTraceCounter.ToString();

            ParentConstraint newInfos = Routines.ComputeVerticalZone(parentConstraint, this);
            ConstraintSize cs = new ConstraintSize(newInfos.constraintWidth, newInfos.precedingWidth, newInfos.maximumWidth, newInfos.constraintHeight, newInfos.precedingHeight, newInfos.maximumHeight);

            output.HTML.Append("<div");
            output.HTML.Append(" id='" + myId + "'");
            output.HTML.Append(" name='" + (String.IsNullOrEmpty(newInfos.objectName) ? this.Name : newInfos.objectName + "_" + this.Name) + "'");
            if (this.Events.Count > 0)
                output.HTML.Append(" " + this.Events.ToHTMLString());
            if (!String.IsNullOrEmpty(cs.attributeWidth))
                output.HTML.Append(" " + cs.attributeWidth);
            if (!String.IsNullOrEmpty(cs.attributeHeight))
                output.HTML.Append(" " + cs.attributeHeight);
            output.HTML.Append(">");

            // set css part
            if (!CommonDirectories.ConfigDirectories.RemoveTables)
            {
                if (!myCss.Body.AllKeys.Contains("float"))
                    myCss.Body.Add("float", "left");
            }
            myCss.Ids = "#" + myId;
            Routines.SetCSSPart(myCss, cs);

            OutputHTML zone = new OutputHTML();

            HTMLObject objPage = refPage.Objects.Find(a => { return a.Container == this.Name || (String.IsNullOrEmpty(newInfos.objectName) ? false : a.Container == newInfos.objectName + "_" + this.Name); });
            HTMLObject objMasterPage = null;
            if (masterRefPage != null)
                objMasterPage = masterRefPage.Objects.Find(a => { return a.Container == this.Name || (String.IsNullOrEmpty(newInfos.objectName) ? false : a.Container == newInfos.objectName + "_" + this.Name); });
            if (objPage != null)
            {
                zone = objPage.GenerateProduction(refPage, masterRefPage, newInfos);
                refPage.SpecificOutput.CSS.Append(zone.CSS.ToString());
                refPage.SpecificOutput.JavaScript.Append(zone.JavaScript.ToString());
                refPage.SpecificOutput.JavaScriptOnLoad.Append(zone.JavaScriptOnLoad.ToString());
            }
            else if (objMasterPage != null)
            {
                zone = objMasterPage.GenerateProduction(refPage, masterRefPage, newInfos);
                output.CSS.Append(zone.CSS.ToString());
                output.JavaScript.Append(zone.JavaScript.ToString());
                output.JavaScriptOnLoad.Append(zone.JavaScriptOnLoad.ToString());
            }

            // compute disposition
            Routines.SetDIVDisposition(output.HTML, this.Disposition, zone.HTML);

            output.HTML.Append("</div>");
            output.CSS.Append(myCss.GenerateCSS(true, true, true));
            output.JavaScript.Append(this.JavaScript.GeneratedCode);
            return output;
        }

        /// <summary>
        /// Generate an HTML DIV tag for actual website
        /// a given master page generates the page
        /// a page contains master objects related
        /// restricted objects in page are computed equally
        /// </summary>
        /// <param name="refPage">page reference</param>
        /// <param name="masterRefPage">master page reference</param>
        /// <param name="objects">master objects list</param>
        /// <param name="parentConstraint">parent constraint</param>
        /// <returns>html output</returns>
        public OutputHTML GenerateProductionDIV(Page refPage, MasterPage masterRefPage, List<MasterObject> objects, ParentConstraint parentConstraint)
        {
            OutputHTML output = new OutputHTML();
            CodeCSS myCss = new CodeCSS(this.CSS);
            string myId = "verti" + Project.IncrementedTraceCounter.ToString();

            ParentConstraint newInfos = Routines.ComputeVerticalZone(parentConstraint, this);
            ConstraintSize cs = new ConstraintSize(newInfos.constraintWidth, newInfos.precedingWidth, newInfos.maximumWidth, newInfos.constraintHeight, newInfos.precedingHeight, newInfos.maximumHeight);

            output.HTML.Append("<div");
            output.HTML.Append(" id='" + myId + "'");
            output.HTML.Append(" name='" + (String.IsNullOrEmpty(newInfos.objectName) ? this.Name : newInfos.objectName + "_" + this.Name) + "'");
            if (this.Events.Count > 0)
                output.HTML.Append(" " + this.Events.ToHTMLString());
            if (!String.IsNullOrEmpty(cs.attributeWidth))
                output.HTML.Append(" " + cs.attributeWidth);
            if (!String.IsNullOrEmpty(cs.attributeHeight))
                output.HTML.Append(" " + cs.attributeHeight);
            output.HTML.Append(">");

            // set css part
            myCss.Ids = "#" + myId;
            if (!CommonDirectories.ConfigDirectories.RemoveTables)
            {
                if (!myCss.Body.AllKeys.Contains("float"))
                    myCss.Body.Add("float", "left");
            }
            Routines.SetCSSPart(myCss, cs);

            OutputHTML zone = new OutputHTML();

            HTMLObject objPage = refPage.Objects.Find(a => { return a.Container == this.Name || (String.IsNullOrEmpty(newInfos.objectName) ? false : a.Container == newInfos.objectName + "_" + this.Name); });
            HTMLObject objMasterPage = null;
            if (masterRefPage != null)
                objMasterPage = masterRefPage.Objects.Find(a => { return a.Container == this.Name || (String.IsNullOrEmpty(newInfos.objectName) ? false : a.Container == newInfos.objectName + "_" + this.Name); });
            List<HTMLObject> list = new List<HTMLObject>();
            foreach (MasterObject mObj in objects)
            {
                list.AddRange(mObj.Objects);
            }
            HTMLObject objHtmlObject = list.Find(a => { return a.Container == this.Name || (String.IsNullOrEmpty(newInfos.objectName) ? false : a.Container == newInfos.objectName + "_" + this.Name); });
            if (objPage != null)
            {
                zone = objPage.GenerateProduction(refPage, masterRefPage, objects, newInfos);
                refPage.SpecificOutput.CSS.Append(zone.CSS.ToString());
                refPage.SpecificOutput.JavaScript.Append(zone.JavaScript.ToString());
                refPage.SpecificOutput.JavaScriptOnLoad.Append(zone.JavaScriptOnLoad.ToString());
            }
            else if (objHtmlObject != null)
            {
                zone = objHtmlObject.GenerateProduction(refPage, masterRefPage, objects, newInfos);
                output.CSS.Append(zone.CSS.ToString());
                output.JavaScript.Append(zone.JavaScript.ToString());
                output.JavaScriptOnLoad.Append(zone.JavaScriptOnLoad.ToString());
            }
            else if (objMasterPage != null)
            {
                zone = objMasterPage.GenerateProduction(refPage, masterRefPage, objects, newInfos);
                output.CSS.Append(zone.CSS.ToString());
                output.JavaScript.Append(zone.JavaScript.ToString());
                output.JavaScriptOnLoad.Append(zone.JavaScriptOnLoad.ToString());
            }

            // compute disposition
            Routines.SetDIVDisposition(output.HTML, this.Disposition, zone.HTML);

            output.HTML.Append("</div>");
            output.CSS.Append(myCss.GenerateCSS(true, true, true));
            output.JavaScript.Append(this.JavaScript.GeneratedCode);
            return output;
        }

        /// <summary>
        /// Generate an HTML TABLE tag from null for actual website
        /// A vertical area is hosted by an inner HTML tag table
        /// this inner HTML tag table must exist to work fine
        /// so, a vertical area do not generate a direct DIV starting at a page
        /// </summary>
        /// <returns>html output</returns>
        public OutputHTML GenerateProductionTable(Page refPage)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Generate an HTML TABLE tag for actual website
        /// a given master page generates the page
        /// restricted objects in page are computed equally
        /// </summary>
        /// <param name="refPage">page reference</param>
        /// <param name="masterRefPage">master page reference</param>
        /// <param name="parentConstraint">parent constraint</param>
        /// <returns>html output</returns>
        public OutputHTML GenerateProductionTable(Page refPage, MasterPage masterRefPage, ParentConstraint parentConstraint)
        {
            OutputHTML output = new OutputHTML();
            CodeCSS myCss = new CodeCSS(this.CSS);
            string myId = "verti" + Project.IncrementedTraceCounter.ToString();

            ParentConstraint newInfos = Routines.ComputeVerticalZone(parentConstraint, this);
            ConstraintSize cs = new ConstraintSize(newInfos.constraintWidth, newInfos.precedingWidth, newInfos.maximumWidth, newInfos.constraintHeight, newInfos.precedingHeight, newInfos.maximumHeight);

            output.HTML.Append("<td");
            output.HTML.Append(" id='" + myId + "'");
            output.HTML.Append(" name='" + (String.IsNullOrEmpty(newInfos.objectName) ? this.Name : newInfos.objectName) + "'");
            output.HTML.Append(" " + Routines.SetTableDisposition(this.Disposition));
            output.HTML.Append(" rowspan='" + this.CountLines.ToString() + "'");
            output.HTML.Append(" colspan='" + this.CountColumns.ToString() + "'");
            if (this.Events.Count > 0)
                output.HTML.Append(" " + this.Events.ToHTMLString());
            if (!String.IsNullOrEmpty(cs.attributeWidth))
                output.HTML.Append(" " + cs.attributeWidth);
            if (!String.IsNullOrEmpty(cs.attributeHeight))
                output.HTML.Append(" " + cs.attributeHeight);
            output.HTML.Append(">");

            // set css part
            myCss.Ids = "#" + myId;
            Routines.SetCSSPart(myCss, cs);

            OutputHTML zone = new OutputHTML();

            HTMLObject objPage = refPage.Objects.Find(a => { return a.Container == this.Name || (String.IsNullOrEmpty(newInfos.objectName) ? false : a.Container == newInfos.objectName + "_" + this.Name); });
            HTMLObject objMasterPage = null;
            if (masterRefPage != null)
                objMasterPage = masterRefPage.Objects.Find(a => { return a.Container == this.Name || (String.IsNullOrEmpty(newInfos.objectName) ? false : a.Container == newInfos.objectName + "_" + this.Name); });
            if (objPage != null)
            {
                zone = objPage.GenerateProduction(refPage, masterRefPage, newInfos);
                output.HTML.Append(zone.HTML.ToString());
                refPage.SpecificOutput.CSS.Append(zone.CSS.ToString());
                refPage.SpecificOutput.JavaScript.Append(zone.JavaScript.ToString());
                refPage.SpecificOutput.JavaScriptOnLoad.Append(zone.JavaScriptOnLoad.ToString());
            }
            else if (objMasterPage != null)
            {
                zone = objMasterPage.GenerateProduction(refPage, masterRefPage, newInfos);
                output.HTML.Append(zone.HTML.ToString());
                output.CSS.Append(zone.CSS.ToString());
                output.JavaScript.Append(zone.JavaScript.ToString());
                output.JavaScriptOnLoad.Append(zone.JavaScriptOnLoad.ToString());
            }
            output.HTML.Append("</td>");
            output.CSS.Append(myCss.GenerateCSS(true, true, true));
            output.JavaScript.Append(this.JavaScript.GeneratedCode);
            return output;
        }

        /// <summary>
        /// Generate an HTML TABLE tag for actual design
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
            string myId = "verti" + Project.IncrementedTraceCounter.ToString();

            ParentConstraint newInfos = Routines.ComputeVerticalZone(parentConstraint, this);
            ConstraintSize cs = new ConstraintSize(newInfos.constraintWidth, newInfos.precedingWidth, newInfos.maximumWidth, newInfos.constraintHeight, newInfos.precedingHeight, newInfos.maximumHeight);

            output.HTML.Append("<td");
            output.HTML.Append(" id='" + myId + "'");
            output.HTML.Append(" name='" + (String.IsNullOrEmpty(newInfos.objectName) ? this.Name : newInfos.objectName) + "'");
            output.HTML.Append(" " + Routines.SetTableDisposition(this.Disposition));
            output.HTML.Append(" rowspan='" + this.CountLines.ToString() + "'");
            output.HTML.Append(" colspan='" + this.CountColumns.ToString() + "'");
            if (this.Events.Count > 0)
                output.HTML.Append(" " + this.Events.ToHTMLString());
            if (!String.IsNullOrEmpty(cs.attributeWidth))
                output.HTML.Append(" " + cs.attributeWidth);
            if (!String.IsNullOrEmpty(cs.attributeHeight))
                output.HTML.Append(" " + cs.attributeHeight);
            output.HTML.Append(">");

            // set css part
            myCss.Ids = "#" + myId;
            Routines.SetCSSPart(myCss, cs);

            OutputHTML zone = new OutputHTML();

            HTMLObject objPage = refPage.Objects.Find(a => { return a.Container == this.Name || (String.IsNullOrEmpty(newInfos.objectName) ? false : a.Container == newInfos.objectName + "_" + this.Name); });
            HTMLObject objMasterPage = null;
            if (masterRefPage != null)
                objMasterPage = masterRefPage.Objects.Find(a => { return a.Container == this.Name || (String.IsNullOrEmpty(newInfos.objectName) ? false : a.Container == newInfos.objectName + "_" + this.Name); });
            List<HTMLObject> list = new List<HTMLObject>();
            foreach (MasterObject mObj in objects)
            {
                list.AddRange(mObj.Objects);
            }
            HTMLObject objHtmlObject = list.Find(a => { return a.Container == this.Name || (String.IsNullOrEmpty(newInfos.objectName) ? false : a.Container == newInfos.objectName + "_" + this.Name); });
            if (objPage != null)
            {
                zone = objPage.GenerateProduction(refPage, masterRefPage, objects, newInfos);
                output.HTML.Append(zone.HTML.ToString());
                refPage.SpecificOutput.CSS.Append(zone.CSS.ToString());
                refPage.SpecificOutput.JavaScript.Append(zone.JavaScript.ToString());
                refPage.SpecificOutput.JavaScriptOnLoad.Append(zone.JavaScriptOnLoad.ToString());
            }
            else if (objHtmlObject != null)
            {
                zone = objHtmlObject.GenerateProduction(refPage, masterRefPage, objects, newInfos);
                output.HTML.Append(zone.HTML.ToString());
                output.CSS.Append(zone.CSS.ToString());
                output.JavaScript.Append(zone.JavaScript.ToString());
                output.JavaScriptOnLoad.Append(zone.JavaScriptOnLoad.ToString());
            }
            else if (objMasterPage != null)
            {
                zone = objMasterPage.GenerateProduction(refPage, masterRefPage, objects, newInfos);
                output.HTML.Append(zone.HTML.ToString());
                output.CSS.Append(zone.CSS.ToString());
                output.JavaScript.Append(zone.JavaScript.ToString());
                output.JavaScriptOnLoad.Append(zone.JavaScriptOnLoad.ToString());
            }
            output.HTML.Append("</td>");
            output.CSS.Append(myCss.GenerateCSS(true, true, true));
            output.JavaScript.Append(this.JavaScript.GeneratedCode);
            return output;
        }

        /// <summary>
        /// Clone this object
        /// </summary>
        /// <returns>cloned object</returns>
        public object Clone()
        {
            return new VerticalZone(this);
        }

        #endregion
    }
}
