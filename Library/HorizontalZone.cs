using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library
{
    /// <summary>
    /// An horizontal area that can contains multiple vertical areas
    /// </summary>
    [Serializable]
    public class HorizontalZone : Marshalling.PersistentDataObject, IContainer, IGenerateDesignDIV, IGenerateDesignTable, IGenerateProductionDIV, IGenerateProductionTable, ICloneable
    {

        #region Fields

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
        /// Index name for counting lines
        /// </summary>
        protected static readonly string countingLinesName = "countingLines";
        /// <summary>
        /// Index name for automatic name
        /// </summary>
        protected static readonly string automaticNameName = "automaticName";
        /// <summary>
        /// Index name for automatic id
        /// </summary>
        protected static readonly string automaticIdName = "automaticId";
        /// <summary>
        /// Index name for vertical areas
        /// </summary>
        protected static readonly string verticalZoneName = "verticalZone";
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
        public HorizontalZone()
        {
            int val = Project.CurrentProject.IncrementedCounter;
            this.Set(automaticNameName, String.Format("horiz{0}", val));
            this.Set(automaticIdName, String.Format("idHoriz{0}", val));
        }

        #endregion

        #region Copy constructor

        /// <summary>
        /// Copy Constructor
        /// </summary>
        /// <param name="hz">horizontal zone source</param>
        private HorizontalZone(HorizontalZone hz)
        {
            int val = Project.CurrentProject.IncrementedCounter;
            this.Set(automaticNameName, String.Format("horiz{0}", val));
            this.Set(automaticIdName, String.Format("idHoriz{0}", val));

            this.ConstraintWidth = hz.ConstraintWidth;
            this.ConstraintHeight = hz.ConstraintHeight;
            this.Width = hz.Width;
            this.Height = hz.Height;
            this.CountLines = hz.CountLines;
            foreach (VerticalZone vz in hz.VerticalZones)
            {
                this.VerticalZones.Add(vz.Clone() as VerticalZone);
            }
            this.Set(eventsName, hz.Events.Clone());
            this.Set(javascriptName, hz.JavaScript.Clone());
            this.Set(javascriptOnloadName, hz.JavaScriptOnLoad.Clone());
            this.Set(cssName, hz.CSS.Clone());
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets width constraint
        /// </summary>
        public EnumConstraint ConstraintWidth
        {
            get { return this.Get(constraintWidthName, EnumConstraint.AUTO); }
            set { this.Set(constraintWidthName, value); }
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
        public int CountLines
        {
            get { return this.Get(countingLinesName, 0); }
            set { this.Set(countingLinesName, value); }
        }

        /// <summary>
        /// Gets vertical areas
        /// </summary>
        public List<VerticalZone> VerticalZones
        {
            get { return this.Get(verticalZoneName, new List<VerticalZone>()); }
        }

        /// <summary>
        /// Gets the total count columns
        /// </summary>
        public uint TotalCountColumns
        {
            get
            {
                uint countColumns = 0;
                foreach (VerticalZone vz in this.VerticalZones)
                {
                    countColumns += (uint)vz.CountColumns;
                }
                return countColumns;
            }
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
        /// Gets events
        /// </summary>
        public Events Events
        {
            get { return this.Get(eventsName, new Events()); }
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
        /// Gets the stringified horizontal area
        /// </summary>
        public string Stringified
        {
            get { return String.Format(Localization.Strings.GetString("HorizontalAreaStringified"), this.Name, this.CountLines); }
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
            foreach (VerticalZone vz in this.VerticalZones)
            {
                done = vz.SearchContainer(containers, objects, searchName, out found);
                if (done) break;
            }
            return done;
        }

        #endregion

        #region Interfaces Implementation

        /// <summary>
        /// Generate an HTML DIV tag from null for design
        /// An horizontal area is hosted in a master page or a master object
        /// This function is not implemented nor called for this class
        /// </summary>
        /// <returns>html output</returns>
        public OutputHTML GenerateDesignDIV()
        {
            throw new NotImplementedException();
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
            if (this.VerticalZones.Count > 0)
            {
                string myId = "horiz" + Project.IncrementedTraceCounter.ToString();
                // compute size
                ParentConstraint newInfos = Routines.ComputeHorizontalZone(parentConstraint, this);
                ConstraintSize cs = new ConstraintSize(newInfos.constraintWidth, newInfos.precedingWidth,
                                                       newInfos.maximumWidth, newInfos.constraintHeight,
                                                       newInfos.precedingHeight, newInfos.maximumHeight);

                output.HTML.Append("<div");
                output.HTML.Append(" id='" + myId + "'");
                output.HTML.Append(" name='" + myId + "'");
                if (!String.IsNullOrEmpty(cs.attributeWidth))
                    output.HTML.Append(" " + cs.attributeWidth);
                if (!String.IsNullOrEmpty(cs.attributeHeight))
                    output.HTML.Append(" " + cs.attributeHeight);
                if (this.Events.Count > 0)
                    output.HTML.Append(" " + this.Events.ToHTMLString());
                output.HTML.Append(">");

                // set CSS part
                myCss.Ids = "#" + myId;
                if (myCss.Body.AllKeys.Contains("border"))
                    myCss.Body.Remove("border");
                Routines.SetCSSPart(myCss, cs);

                List<VerticalZone>.Enumerator e = this.VerticalZones.GetEnumerator();
                VerticalZone lastZone = null;
                if (e.MoveNext())
                {
                    do
                    {
                        if (lastZone != null)
                        {
                            OutputHTML zone = lastZone.GenerateDesignDIV(refPage, masterRefPage, newInfos);
                            output.HTML.Append(zone.HTML.ToString());
                            output.CSS.Append(zone.CSS.ToString());
                            output.JavaScript.Append(zone.JavaScript.ToString());
                            output.JavaScriptOnLoad.Append(zone.JavaScriptOnLoad.ToString());
                        }
                        lastZone = e.Current;
                    } while (e.MoveNext());
                }
                if (lastZone != null)
                {
                    OutputHTML last = lastZone.GenerateDesignDIV(refPage, masterRefPage, newInfos);
                    output.HTML.Append(last.HTML.ToString());
                    output.CSS.Append(last.CSS.ToString());
                    output.JavaScript.Append(last.JavaScript.ToString());
                    output.JavaScriptOnLoad.Append(last.JavaScriptOnLoad.ToString());
                }
                output.HTML.Append("</div>");
                output.CSS.Append(myCss.GenerateCSS(true, true, true));
                output.JavaScript.Append(this.JavaScript.GeneratedCode);
                output.JavaScriptOnLoad.Append(this.JavaScriptOnLoad.GeneratedCode);
            }
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
            if (this.VerticalZones.Count > 0)
            {
                string myId = "horiz" + Project.IncrementedTraceCounter.ToString();
                // compute size
                ParentConstraint newInfos = Routines.ComputeHorizontalZone(parentConstraint, this);
                ConstraintSize cs = new ConstraintSize(newInfos.constraintWidth, newInfos.precedingWidth, newInfos.maximumWidth, newInfos.constraintHeight, newInfos.precedingHeight, newInfos.maximumHeight);

                output.HTML.Append("<div");
                output.HTML.Append(" id='" + myId + "'");
                output.HTML.Append(" name='" + myId + "'");
                if (!String.IsNullOrEmpty(cs.attributeWidth))
                    output.HTML.Append(" " + cs.attributeWidth);
                if (!String.IsNullOrEmpty(cs.attributeHeight))
                    output.HTML.Append(" " + cs.attributeHeight);
                if (this.Events.Count > 0)
                    output.HTML.Append(" " + this.Events.ToHTMLString());
                output.HTML.Append(">");

                // set CSS part
                myCss.Ids = "#" + myId;
                if (myCss.Body.AllKeys.Contains("border"))
                    myCss.Body.Remove("border");
                Routines.SetCSSPart(myCss, cs);

                List<VerticalZone>.Enumerator e = this.VerticalZones.GetEnumerator();
                VerticalZone lastZone = null;
                if (e.MoveNext())
                {
                    do
                    {
                        if (lastZone != null)
                        {
                            OutputHTML zone = lastZone.GenerateDesignDIV(refPage, objects, newInfos);
                            output.HTML.Append(zone.HTML.ToString());
                            output.CSS.Append(zone.CSS.ToString());
                            output.JavaScript.Append(zone.JavaScript.ToString());
                            output.JavaScriptOnLoad.Append(zone.JavaScriptOnLoad.ToString());
                        }
                        lastZone = e.Current;
                    } while (e.MoveNext());
                }
                if (lastZone != null)
                {
                    OutputHTML last = lastZone.GenerateDesignDIV(refPage, objects, newInfos);
                    output.HTML.Append(last.HTML.ToString());
                    output.CSS.Append(last.CSS.ToString());
                    output.JavaScript.Append(last.JavaScript.ToString());
                    output.JavaScriptOnLoad.Append(last.JavaScriptOnLoad.ToString());
                }
                output.HTML.Append("</div>");
                output.CSS.Append(myCss.GenerateCSS(true, true, true));
                output.JavaScript.Append(this.JavaScript.GeneratedCode);
                output.JavaScriptOnLoad.Append(this.JavaScriptOnLoad.GeneratedCode);
            }
            return output;
        }

        /// <summary>
        /// Generate an HTML DIV tag from null for design
        /// An horizontal area is hosted by an inner HTML tag table
        /// this inner HTML tag table must exist to work fine
        /// so, an horizontal area do not generate a direct DIV starting at a page
        /// </summary>
        /// <returns>html output</returns>
        public OutputHTML GenerateDesignDIV(Page refPage)
        {
            throw new NotImplementedException();
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
            if (this.VerticalZones.Count > 0)
            {
                string myId = "horiz" + Project.IncrementedTraceCounter.ToString();

                // compute size
                ParentConstraint newInfos = Routines.ComputeHorizontalZone(parentConstraint, this);
                ConstraintSize cs = new ConstraintSize(newInfos.constraintWidth, newInfos.precedingWidth,
                                                       newInfos.maximumWidth, newInfos.constraintHeight,
                                                       newInfos.precedingHeight, newInfos.maximumHeight);

                output.HTML.Append("<div");
                output.HTML.Append(" id='" + myId + "'");
                output.HTML.Append(" name='" + myId + "'");
                if (!String.IsNullOrEmpty(cs.attributeWidth))
                    output.HTML.Append(" " + cs.attributeWidth);
                if (!String.IsNullOrEmpty(cs.attributeHeight))
                    output.HTML.Append(" " + cs.attributeHeight);
                if (this.Events.Count > 0)
                    output.HTML.Append(" " + this.Events.ToHTMLString());
                output.HTML.Append(">");

                // set CSS part
                myCss.Ids = "#" + myId;
                Routines.SetCSSPart(myCss, cs);

                List<VerticalZone>.Enumerator e = this.VerticalZones.GetEnumerator();
                VerticalZone lastZone = null;
                if (e.MoveNext())
                {
                    do
                    {
                        if (lastZone != null)
                        {
                            OutputHTML zone = lastZone.GenerateDesignDIV(refPage, masterRefPage, objects, newInfos);
                            output.HTML.Append(zone.HTML.ToString());
                            output.CSS.Append(zone.CSS.ToString());
                            output.JavaScript.Append(zone.JavaScript.ToString());
                            output.JavaScriptOnLoad.Append(zone.JavaScriptOnLoad.ToString());
                        }
                        lastZone = e.Current;
                    } while (e.MoveNext());
                }
                if (lastZone != null)
                {
                    OutputHTML last = lastZone.GenerateDesignDIV(refPage, masterRefPage, objects, newInfos);
                    output.HTML.Append(last.HTML.ToString());
                    output.CSS.Append(last.CSS.ToString());
                    output.JavaScript.Append(last.JavaScript.ToString());
                    output.JavaScriptOnLoad.Append(last.JavaScriptOnLoad.ToString());
                }
                output.HTML.Append("</div>");
                output.CSS.Append(myCss.GenerateCSS(true, true, true));
                output.JavaScript.Append(this.JavaScript.GeneratedCode);
                output.JavaScriptOnLoad.Append(this.JavaScriptOnLoad.GeneratedCode);
            }
            return output;
        }


        /// <summary>
        /// Generate an HTML TABLE tag from null for design
        /// An horizontal area is hosted by an inner HTML tag table
        /// this inner HTML tag table must exist to work fine
        /// so, an horizontal area do not generate a direct TABLE starting at a page
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
            string myId = "horiz" + Project.IncrementedTraceCounter.ToString();

            // compute size
            ParentConstraint newInfos = Routines.ComputeHorizontalZone(parentConstraint, this);
            ConstraintSize cs = new ConstraintSize(newInfos.constraintWidth, newInfos.precedingWidth,
                                                   newInfos.maximumWidth, newInfos.constraintHeight,
                                                   newInfos.precedingHeight, newInfos.maximumHeight);

            output.HTML.Append("<tr id='" + myId + "' name='" + myId + "'");
            if (!String.IsNullOrEmpty(cs.attributeWidth))
                output.HTML.Append(" " + cs.attributeWidth);
            if (!String.IsNullOrEmpty(cs.attributeHeight))
                output.HTML.Append(" " + cs.attributeHeight);
            if (this.Events.Count > 0)
                output.HTML.Append(" " + this.Events.ToHTMLString());
            output.HTML.Append(">");

            myCss.Ids = "#" + myId;
            Routines.SetCSSPart(myCss, cs);

            foreach (VerticalZone vz in this.VerticalZones)
            {
                OutputHTML zone = vz.GenerateDesignTable(refPage, masterRefPage, newInfos);
                output.HTML.Append(zone.HTML.ToString());
                output.CSS.Append(zone.CSS.ToString());
                output.JavaScript.Append(zone.JavaScript.ToString());
                output.JavaScriptOnLoad.Append(zone.JavaScriptOnLoad.ToString());
            }
            output.HTML.Append("</tr>");
            output.CSS.Append(myCss.GenerateCSS(true, true, true));
            output.JavaScript.Append(this.JavaScript.GeneratedCode);
            output.JavaScriptOnLoad.Append(this.JavaScriptOnLoad.GeneratedCode);
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
            string myId = "horiz" + Project.IncrementedTraceCounter.ToString();

            // compute size
            ParentConstraint newInfos = Routines.ComputeHorizontalZone(parentConstraint, this);
            ConstraintSize cs = new ConstraintSize(newInfos.constraintWidth, newInfos.precedingWidth,
                                                   newInfos.maximumWidth, newInfos.constraintHeight,
                                                   newInfos.precedingHeight, newInfos.maximumHeight);

            output.HTML.Append("<tr id='" + myId + "' name='" + myId + "'");
            if (!String.IsNullOrEmpty(cs.attributeWidth))
                output.HTML.Append(" " + cs.attributeWidth);
            if (!String.IsNullOrEmpty(cs.attributeHeight))
                output.HTML.Append(" " + cs.attributeHeight);
            if (this.Events.Count > 0)
                output.HTML.Append(" " + this.Events.ToHTMLString());
            output.HTML.Append(">");

            myCss.Ids = "#" + myId;
            Routines.SetCSSPart(myCss, cs);

            foreach (VerticalZone vz in this.VerticalZones)
            {
                OutputHTML zone = vz.GenerateDesignTable(refPage, objects, newInfos);
                output.HTML.Append(zone.HTML.ToString());
                output.CSS.Append(zone.CSS.ToString());
                output.JavaScript.Append(zone.JavaScript.ToString());
                output.JavaScriptOnLoad.Append(zone.JavaScriptOnLoad.ToString());
            }
            output.HTML.Append("</tr>");
            output.CSS.Append(myCss.GenerateCSS(true, true, true));
            output.JavaScript.Append(this.JavaScript.GeneratedCode);
            output.JavaScriptOnLoad.Append(this.JavaScriptOnLoad.GeneratedCode);
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
            string myId = "horiz" + Project.IncrementedTraceCounter.ToString();
            
            // compute size
            ParentConstraint newInfos = Routines.ComputeHorizontalZone(parentConstraint, this);
            ConstraintSize cs = new ConstraintSize(newInfos.constraintWidth, newInfos.precedingWidth,
                                                   newInfos.maximumWidth, newInfos.constraintHeight,
                                                   newInfos.precedingHeight, newInfos.maximumHeight);

            output.HTML.Append("<tr id='" + myId + "' name='" + myId + "'");
            if (!String.IsNullOrEmpty(cs.attributeWidth))
                output.HTML.Append(" " + cs.attributeWidth);
            if (!String.IsNullOrEmpty(cs.attributeHeight))
                output.HTML.Append(" " + cs.attributeHeight);
            if (this.Events.Count > 0)
                output.HTML.Append(" " + this.Events.ToHTMLString());
            output.HTML.Append(">");

            myCss.Ids = "#" + myId;
            Routines.SetCSSPart(myCss, cs);

            foreach (VerticalZone vz in this.VerticalZones)
            {
                OutputHTML zone = vz.GenerateDesignTable(refPage, masterRefPage, objects, newInfos);
                output.HTML.Append(zone.HTML.ToString());
                output.CSS.Append(zone.CSS.ToString());
                output.JavaScript.Append(zone.JavaScript.ToString());
                output.JavaScriptOnLoad.Append(zone.JavaScriptOnLoad.ToString());
            }
            output.HTML.Append("</tr>");
            output.CSS.Append(myCss.GenerateCSS(true, true, true));
            output.JavaScript.Append(this.JavaScript.GeneratedCode);
            output.JavaScriptOnLoad.Append(this.JavaScriptOnLoad.GeneratedCode);
            return output;
        }

        /// <summary>
        /// Generate an HTML DIV tag from null for actual website
        /// An horizontal area is hosted in a master page or a master object
        /// This function is not implemented nor called for this class
        /// </summary>
        /// <returns>html output</returns>
        public OutputHTML GenerateProductionDIV(Page refPage)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Generate an HTML DIV tag for actual website
        /// a given master page generates the page
        /// </summary>
        /// <param name="refPage">page reference</param>
        /// <param name="masterRefPage">master page reference</param>
        /// <param name="parentConstraint">parent constraint</param>
        /// <returns>html output</returns>
        public OutputHTML GenerateProductionDIV(Page refPage, MasterPage masterRefPage, ParentConstraint parentConstraint)
        {
            OutputHTML output = new OutputHTML();
            CodeCSS myCss = new CodeCSS(this.CSS);
            if (this.VerticalZones.Count > 0)
            {
                string myId = "horiz" + Project.IncrementedTraceCounter.ToString();
                // compute size
                ParentConstraint newInfos = Routines.ComputeHorizontalZone(parentConstraint, this);
                ConstraintSize cs = new ConstraintSize(newInfos.constraintWidth, newInfos.precedingWidth,
                                                       newInfos.maximumWidth, newInfos.constraintHeight,
                                                       newInfos.precedingHeight, newInfos.maximumHeight);

                output.HTML.Append("<div");
                output.HTML.Append(" id='" + myId + "'");
                output.HTML.Append(" name='" + myId + "'");
                if (!String.IsNullOrEmpty(cs.attributeWidth))
                    output.HTML.Append(" " + cs.attributeWidth);
                if (!String.IsNullOrEmpty(cs.attributeHeight))
                    output.HTML.Append(" " + cs.attributeHeight);
                if (this.Events.Count > 0)
                    output.HTML.Append(" " + this.Events.ToHTMLString());
                output.HTML.Append(">");

                // set CSS part
                myCss.Ids = "#" + myId;
                Routines.SetCSSPart(myCss, cs);

                List<VerticalZone>.Enumerator e = this.VerticalZones.GetEnumerator();
                VerticalZone lastZone = null;
                if (e.MoveNext())
                {
                    do
                    {
                        if (lastZone != null)
                        {
                            OutputHTML zone = lastZone.GenerateProductionDIV(refPage, masterRefPage, newInfos);
                            output.HTML.Append(zone.HTML.ToString());
                            output.CSS.Append(zone.CSS.ToString());
                            output.JavaScript.Append(zone.JavaScript.ToString());
                            output.JavaScriptOnLoad.Append(zone.JavaScriptOnLoad.ToString());
                        }
                        lastZone = e.Current;
                    } while (e.MoveNext());
                }
                if (lastZone != null)
                {
                    OutputHTML last = lastZone.GenerateProductionDIV(refPage, masterRefPage, newInfos);
                    output.HTML.Append(last.HTML.ToString());
                    output.CSS.Append(last.CSS.ToString());
                    output.JavaScript.Append(last.JavaScript.ToString());
                    output.JavaScriptOnLoad.Append(last.JavaScriptOnLoad.ToString());
                }
                output.HTML.Append("</div>");
                output.CSS.Append(myCss.GenerateCSS(true, true, true));
                output.JavaScript.Append(this.JavaScript.GeneratedCode);
                output.JavaScriptOnLoad.Append(this.JavaScriptOnLoad.GeneratedCode);
            }
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
            if (this.VerticalZones.Count > 0)
            {
                string myId = "horiz" + Project.IncrementedTraceCounter.ToString();

                // compute size
                ParentConstraint newInfos = Routines.ComputeHorizontalZone(parentConstraint, this);
                ConstraintSize cs = new ConstraintSize(newInfos.constraintWidth, newInfos.precedingWidth,
                                                       newInfos.maximumWidth, newInfos.constraintHeight,
                                                       newInfos.precedingHeight, newInfos.maximumHeight);

                output.HTML.Append("<div");
                output.HTML.Append(" id='" + myId + "'");
                output.HTML.Append(" name='" + myId + "'");
                if (!String.IsNullOrEmpty(cs.attributeWidth))
                    output.HTML.Append(" " + cs.attributeWidth);
                if (!String.IsNullOrEmpty(cs.attributeHeight))
                    output.HTML.Append(" " + cs.attributeHeight);
                if (this.Events.Count > 0)
                    output.HTML.Append(" " + this.Events.ToHTMLString());
                output.HTML.Append(">");

                // set CSS part
                myCss.Ids = "#" + myId;
                Routines.SetCSSPart(myCss, cs);

                List<VerticalZone>.Enumerator e = this.VerticalZones.GetEnumerator();
                VerticalZone lastZone = null;
                if (e.MoveNext())
                {
                    do
                    {
                        if (lastZone != null)
                        {
                            OutputHTML zone = lastZone.GenerateProductionDIV(refPage, masterRefPage, objects, newInfos);
                            output.HTML.Append(zone.HTML.ToString());
                            output.CSS.Append(zone.CSS.ToString());
                            output.JavaScript.Append(zone.JavaScript.ToString());
                            output.JavaScriptOnLoad.Append(zone.JavaScriptOnLoad.ToString());
                        }
                        lastZone = e.Current;
                    } while (e.MoveNext());
                }
                if (lastZone != null)
                {
                    OutputHTML last = lastZone.GenerateProductionDIV(refPage, masterRefPage, objects, newInfos);
                    output.HTML.Append(last.HTML.ToString());
                    output.CSS.Append(last.CSS.ToString());
                    output.JavaScript.Append(last.JavaScript.ToString());
                    output.JavaScriptOnLoad.Append(last.JavaScriptOnLoad.ToString());
                }
                output.HTML.Append("</div>");
                output.CSS.Append(myCss.GenerateCSS(true, true, true));
                output.JavaScript.Append(this.JavaScript.GeneratedCode);
                output.JavaScriptOnLoad.Append(this.JavaScriptOnLoad.GeneratedCode);
            }
            return output;
        }

        /// <summary>
        /// Generate an HTML TABLE tag from null for actual website
        /// An horizontal area is hosted in a master page or a master object
        /// This function is not implemented nor called for this class
        /// </summary>
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
            string myId = "horiz" + Project.IncrementedTraceCounter.ToString();

            // compute size
            ParentConstraint newInfos = Routines.ComputeHorizontalZone(parentConstraint, this);
            ConstraintSize cs = new ConstraintSize(newInfos.constraintWidth, newInfos.precedingWidth,
                                                   newInfos.maximumWidth, newInfos.constraintHeight,
                                                   newInfos.precedingHeight, newInfos.maximumHeight);

            output.HTML.Append("<tr id='" + myId + "' name='" + myId + "'");
            if (!String.IsNullOrEmpty(cs.attributeWidth))
                output.HTML.Append(" " + cs.attributeWidth);
            if (!String.IsNullOrEmpty(cs.attributeHeight))
                output.HTML.Append(" " + cs.attributeHeight);
            if (this.Events.Count > 0)
                output.HTML.Append(" " + this.Events.ToHTMLString());
            output.HTML.Append(">");

            myCss.Ids = "#" + myId;
            Routines.SetCSSPart(myCss, cs);

            foreach (VerticalZone vz in this.VerticalZones)
            {
                OutputHTML zone = vz.GenerateProductionTable(refPage, masterRefPage, newInfos);
                output.HTML.Append(zone.HTML.ToString());
                output.CSS.Append(zone.CSS.ToString());
                output.JavaScript.Append(zone.JavaScript.ToString());
                output.JavaScriptOnLoad.Append(zone.JavaScriptOnLoad.ToString());
            }
            output.HTML.Append("</tr>");
            output.CSS.Append(myCss.GenerateCSS(true, true, true));
            output.JavaScript.Append(this.JavaScript.GeneratedCode);
            output.JavaScriptOnLoad.Append(this.JavaScriptOnLoad.GeneratedCode);
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
            string myId = "horiz" + Project.IncrementedTraceCounter.ToString();

            // compute size
            ParentConstraint newInfos = Routines.ComputeHorizontalZone(parentConstraint, this);
            ConstraintSize cs = new ConstraintSize(newInfos.constraintWidth, newInfos.precedingWidth,
                                                   newInfos.maximumWidth, newInfos.constraintHeight,
                                                   newInfos.precedingHeight, newInfos.maximumHeight);

            output.HTML.Append("<tr id='" + myId + "' name='" + myId + "'");
            if (!String.IsNullOrEmpty(cs.attributeWidth))
                output.HTML.Append(" " + cs.attributeWidth);
            if (!String.IsNullOrEmpty(cs.attributeHeight))
                output.HTML.Append(" " + cs.attributeHeight);
            if (this.Events.Count > 0)
                output.HTML.Append(" " + this.Events.ToHTMLString());
            output.HTML.Append(">");

            myCss.Ids = "#" + myId;
            Routines.SetCSSPart(myCss, cs);

            foreach (VerticalZone vz in this.VerticalZones)
            {
                OutputHTML zone = vz.GenerateProductionTable(refPage, masterRefPage, objects, newInfos);
                output.HTML.Append(zone.HTML.ToString());
                output.CSS.Append(zone.CSS.ToString());
                output.JavaScript.Append(zone.JavaScript.ToString());
                output.JavaScriptOnLoad.Append(zone.JavaScriptOnLoad.ToString());
            }
            output.HTML.Append("</tr>");
            output.CSS.Append(myCss.GenerateCSS(true, true, true));
            output.JavaScript.Append(this.JavaScript.GeneratedCode);
            output.JavaScriptOnLoad.Append(this.JavaScriptOnLoad.GeneratedCode);
            return output;
        }

        /// <summary>
        /// Clone this object
        /// </summary>
        /// <returns>cloned object</returns>
        public object Clone()
        {
            HorizontalZone newObject = new HorizontalZone(this);
            return newObject;
        }

        #endregion

    }
}
