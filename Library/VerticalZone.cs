using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library
{
    [Serializable]
    public class VerticalZone : IContainer, IGenerateDesignDIV, IGenerateDesignTable, IGenerateProductionDIV, IGenerateProductionTable, ICloneable
    {
        private Disposition disposition;
        private EnumConstraint constraintWidth;
        private EnumConstraint constraintHeight;
        private string name = "verti" + Project.CurrentProject.IncrementedCounter.ToString();
        private string id = "idVerti" + Project.CurrentProject.IncrementedCounter.ToString();
        private int countLines;
        private int countColumns;
        private uint width;
        private uint height;
        private CodeJavaScript javascript = new CodeJavaScript();
        private CodeJavaScript javascriptOnLoad = new CodeJavaScript();
        private CodeCSS css = new CodeCSS();

        #region Default Constructor
        public VerticalZone() { }
        #endregion

        #region Copy constructor
        private VerticalZone(VerticalZone vz)
        {
            this.disposition = vz.disposition;
            this.constraintWidth = vz.constraintWidth;
            this.constraintHeight = vz.constraintHeight;
            this.width = vz.width;
            this.height = vz.height;
            this.countLines = vz.countLines;
            this.javascript = vz.javascript.Clone() as CodeJavaScript;
            this.javascriptOnLoad = vz.javascriptOnLoad.Clone() as CodeJavaScript;
            this.css = vz.css.Clone() as CodeCSS;
            this.css.Ids = "#" + this.id;
        }
        #endregion

        #region Public Properties

        public Disposition Disposition
        {
            get { return this.disposition; }
            set { this.disposition = value; }
        }

        public string DispositionText
        {
            get { return this.disposition.ToString(); }
            set
            {
                Enum.TryParse(value, out this.disposition);
            }
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

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public string Id
        {
            get { return this.id; }
            set { this.id = value; }
        }

        public int CountLines
        {
            get { return this.countLines; }
            set { this.countLines = value; }
        }

        public int CountColumns
        {
            get { return this.countColumns; }
            set { this.countColumns = value; }
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

        public CodeCSS CSS
        {
            get { return this.css; }
        }

        public CodeJavaScript JavaScript
        {
            get { return this.javascript; }
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

        public string JavaScriptSource
        {
            get { return this.javascript.Code; }
            set { this.javascript.Code = value; }
        }

        public string Stringified
        {
            get { return String.Format(Localization.Strings.GetString("VerticalAreaStringified"), this.name, this.countLines, this.countColumns); }
        }

        #endregion

        #region Public Methods

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

        public OutputHTML GenerateDesignDIV()
        {
            throw new NotImplementedException();
        }

        public OutputHTML GenerateDesignDIV(Page refPage, MasterPage masterRefPage, ParentConstraint parentConstraint)
        {
            OutputHTML output = new OutputHTML();
            CodeCSS myCss = new CodeCSS(this.css);
            string myId = "verti" + Project.IncrementedTraceCounter.ToString();

            ParentConstraint newInfos = Routines.ComputeVerticalZone(parentConstraint, this);
            ConstraintSize cs = new ConstraintSize(newInfos.constraintWidth, newInfos.precedingWidth, newInfos.maximumWidth, newInfos.constraintHeight, newInfos.precedingHeight, newInfos.maximumHeight);

            output.HTML.Append("<div title='" + Routines.PrintTipSize(newInfos.objectName, this.name, cs) + "' id='" + myId + "' name='" + (String.IsNullOrEmpty(newInfos.objectName) ? this.name : newInfos.objectName + "_" + this.name) + "' " + cs.attributeWidth + " " + cs.attributeHeight + ">");

            // set css part
            myCss.Ids = "#" + myId;
            if (!CommonDirectories.ConfigDirectories.RemoveTables)
            {
                if (!myCss.Body.AllKeys.Contains("float"))
                    myCss.Body.Add("float", "left");
            }
            Routines.SetCSSPart(myCss, cs);

            OutputHTML zone = new OutputHTML();

            HTMLObject objPage = refPage.Objects.Find(a => { return a.Container == this.name || (String.IsNullOrEmpty(newInfos.objectName) ? false : a.Container == newInfos.objectName + "_" + this.name); });
            HTMLObject objMasterPage = null;
            if (masterRefPage != null)
                objMasterPage = masterRefPage.Objects.Find(a => { return a.Container == this.name || (String.IsNullOrEmpty(newInfos.objectName) ? false : a.Container == newInfos.objectName + "_" + this.name); });
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
            Routines.SetDIVDisposition(output.HTML, this.disposition, zone.HTML);

            output.HTML.Append("</div>");
            output.CSS.Append(myCss.GenerateCSS(true, true, true));
            output.JavaScript.Append(this.javascript.GeneratedCode);
            return output;
        }

        public OutputHTML GenerateDesignDIV(Page refPage, List<MasterObject> objects, ParentConstraint parentConstraint)
        {
            OutputHTML output = new OutputHTML();
            CodeCSS myCss = new CodeCSS(this.css);
            string myId = "verti" + Project.IncrementedTraceCounter.ToString();

            // compute size
            ParentConstraint newInfos = Routines.ComputeVerticalZone(parentConstraint, this);
            ConstraintSize cs = new ConstraintSize(newInfos.constraintWidth, newInfos.precedingWidth, newInfos.maximumWidth, newInfos.constraintHeight, newInfos.precedingHeight, newInfos.maximumHeight);

            output.HTML.Append("<div title='" + Routines.PrintTipSize(newInfos.objectName, this.name, cs) + "' id='" + myId + "' name='" + (String.IsNullOrEmpty(newInfos.objectName) ? this.name : newInfos.objectName + "_" + this.name) + "' " + cs.attributeWidth + " " + cs.attributeHeight + ">");
            myCss.Ids = "#" + myId;

            // set css part
            if (!CommonDirectories.ConfigDirectories.RemoveTables)
            {
                if (!myCss.Body.AllKeys.Contains("float"))
                    myCss.Body.Add("float", "left");
            }
            Routines.SetCSSPart(myCss, cs);

            OutputHTML zone = new OutputHTML();

            HTMLObject objPage = refPage.Objects.Find(a => { return a.Container == this.name || (String.IsNullOrEmpty(newInfos.objectName) ? false : a.Container == newInfos.objectName + "_" + this.name); });
            List<HTMLObject> list = new List<HTMLObject>();
            foreach (MasterObject mObj in objects)
            {
                list.AddRange(mObj.Objects);
            }
            HTMLObject objHtmlObject = list.Find(a => { return a.Container == this.name || (String.IsNullOrEmpty(newInfos.objectName) ? false : a.Container == newInfos.objectName + "_" + this.name); });
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
            Routines.SetDIVDisposition(output.HTML, this.disposition, zone.HTML);

            output.HTML.Append("</div>");
            output.CSS.Append(myCss.GenerateCSS(true, true, true));
            output.JavaScript.Append(this.javascript.GeneratedCode);
            return output;
        }

        public OutputHTML GenerateDesignDIV(Page refPage, MasterPage masterRefPage, List<MasterObject> objects, ParentConstraint parentConstraint)
        {
            OutputHTML output = new OutputHTML();
            CodeCSS myCss = new CodeCSS(this.css);
            string myId = "verti" + Project.IncrementedTraceCounter.ToString();

            ParentConstraint newInfos = Routines.ComputeVerticalZone(parentConstraint, this);
            ConstraintSize cs = new ConstraintSize(newInfos.constraintWidth, newInfos.precedingWidth, newInfos.maximumWidth, newInfos.constraintHeight, newInfos.precedingHeight, newInfos.maximumHeight);

            output.HTML.Append("<div title='" + Routines.PrintTipSize(newInfos.objectName, this.name, cs) + "' id='" + myId + "' name='" + (String.IsNullOrEmpty(newInfos.objectName) ? this.name : newInfos.objectName + "_" + this.name) + "' " + cs.attributeWidth + " " + cs.attributeHeight + ">");
            myCss.Ids = "#" + myId;

            // set css part
            if (!CommonDirectories.ConfigDirectories.RemoveTables)
            {
                if (!myCss.Body.AllKeys.Contains("float"))
                    myCss.Body.Add("float", "left");
            }
            Routines.SetCSSPart(myCss, cs);

            OutputHTML zone = new OutputHTML();

            HTMLObject objPage = refPage.Objects.Find(a => { return a.Container == this.name || (String.IsNullOrEmpty(newInfos.objectName) ? false : a.Container == newInfos.objectName + "_" + this.name); });
            HTMLObject objMasterPage = null;
            if (masterRefPage != null)
                objMasterPage = masterRefPage.Objects.Find(a => { return a.Container == this.name || (String.IsNullOrEmpty(newInfos.objectName) ? false : a.Container == newInfos.objectName + "_" + this.name); });
            List<HTMLObject> list = new List<HTMLObject>();
            foreach (MasterObject mObj in objects)
            {
                list.AddRange(mObj.Objects);
            }
            HTMLObject objHtmlObject = list.Find(a => { return a.Container == this.name || (String.IsNullOrEmpty(newInfos.objectName) ? false : a.Container == newInfos.objectName + "_" + this.name); });
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
            Routines.SetDIVDisposition(output.HTML, this.disposition, zone.HTML);

            output.HTML.Append("</div>");
            output.CSS.Append(myCss.GenerateCSS(true, true, true));
            output.JavaScript.Append(this.javascript.GeneratedCode);
            return output;
        }


        public OutputHTML GenerateDesignDIV(Page refPage)
        {
            throw new NotImplementedException();
        }

        public OutputHTML GenerateDesignTable(Page refPage)
        {
            throw new NotImplementedException();
        }

        public OutputHTML GenerateDesignTable(Page refPage, MasterPage masterRefPage, ParentConstraint parentConstraint)
        {
            OutputHTML output = new OutputHTML();
            CodeCSS myCss = new CodeCSS(this.css);
            string myId = "verti" + Project.IncrementedTraceCounter.ToString();

            ParentConstraint newInfos = Routines.ComputeVerticalZone(parentConstraint, this);
            ConstraintSize cs = new ConstraintSize(newInfos.constraintWidth, newInfos.precedingWidth, newInfos.maximumWidth, newInfos.constraintHeight, newInfos.precedingHeight, newInfos.maximumHeight);

            output.HTML.Append("<td " + Routines.SetTableDisposition(this.disposition) + " title='" + Routines.PrintTipSize(newInfos.objectName, this.name, cs) + "' rowspan='" + this.countLines.ToString() + "' colspan='" + this.countColumns.ToString() + "' id='" + myId + "' name='" + (String.IsNullOrEmpty(newInfos.objectName) ? this.name : newInfos.objectName + "_" + this.name) + "' " + cs.attributeWidth + " " + cs.attributeHeight + ">");
            myCss.Ids = "#" + myId;

            // set css part
            Routines.SetCSSPart(myCss, cs);

            OutputHTML zone = new OutputHTML();

            HTMLObject objPage = refPage.Objects.Find(a => { return a.Container == this.name || (String.IsNullOrEmpty(newInfos.objectName) ? false : a.Container == newInfos.objectName + "_" + this.name); });
            HTMLObject objMasterPage = null;
            if (masterRefPage != null)
                objMasterPage = masterRefPage.Objects.Find(a => { return a.Container == this.name || (String.IsNullOrEmpty(newInfos.objectName) ? false : a.Container == newInfos.objectName + "_" + this.name); });
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
            output.JavaScript.Append(this.javascript.GeneratedCode);
            return output;
        }

        public OutputHTML GenerateDesignTable(Page refPage, List<MasterObject> objects, ParentConstraint parentConstraint)
        {
            OutputHTML output = new OutputHTML();
            CodeCSS myCss = new CodeCSS(this.css);
            string myId = "verti" + Project.IncrementedTraceCounter.ToString();

            ParentConstraint newInfos = Routines.ComputeVerticalZone(parentConstraint, this);
            ConstraintSize cs = new ConstraintSize(newInfos.constraintWidth, newInfos.precedingWidth, newInfos.maximumWidth, newInfos.constraintHeight, newInfos.precedingHeight, newInfos.maximumHeight);

            output.HTML.Append("<td " + Routines.SetTableDisposition(this.disposition) + " title='" + Routines.PrintTipSize(newInfos.objectName, this.name, cs) + "' rowspan='" + this.countLines.ToString() + "' colspan='" + this.countColumns.ToString() + "' id='" + myId + "' name='" + (String.IsNullOrEmpty(newInfos.objectName) ? this.name : newInfos.objectName + "_" + this.name) + "' " + cs.attributeWidth + " " + cs.attributeHeight + ">");
            myCss.Ids = "#" + myId;

            // set css part
            Routines.SetCSSPart(myCss, cs);

            OutputHTML zone = new OutputHTML();

            HTMLObject objPage = refPage.Objects.Find(a => { return a.Container == this.name || (String.IsNullOrEmpty(newInfos.objectName) ? false : a.Container == newInfos.objectName + "_" + this.name); });
            List<HTMLObject> list = new List<HTMLObject>();
            foreach (MasterObject mObj in objects)
            {
                list.AddRange(mObj.Objects);
            }
            HTMLObject objHtmlObject = list.Find(a => { return a.Container == this.name || (String.IsNullOrEmpty(newInfos.objectName) ? false : a.Container == newInfos.objectName + "_" + this.name); });
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
            output.JavaScript.Append(this.javascript.GeneratedCode);
            return output;
        }

        public OutputHTML GenerateDesignTable(Page refPage, MasterPage masterRefPage, List<MasterObject> objects, ParentConstraint parentConstraint)
        {
            OutputHTML output = new OutputHTML();
            CodeCSS myCss = new CodeCSS(this.css);
            string myId = "verti" + Project.IncrementedTraceCounter.ToString();

            ParentConstraint newInfos = Routines.ComputeVerticalZone(parentConstraint, this);
            ConstraintSize cs = new ConstraintSize(newInfos.constraintWidth, newInfos.precedingWidth, newInfos.maximumWidth, newInfos.constraintHeight, newInfos.precedingHeight, newInfos.maximumHeight);

            output.HTML.Append("<td " + Routines.SetTableDisposition(this.disposition) + " title='" + Routines.PrintTipSize(newInfos.objectName, this.name, cs) + "' rowspan='" + this.countLines.ToString() + "' colspan='" + this.countColumns.ToString() + "' id='" + myId + "' name='" + (String.IsNullOrEmpty(newInfos.objectName) ? this.name : newInfos.objectName + "_" + this.name) + "' " + cs.attributeWidth + " " + cs.attributeHeight + ">");

            myCss.Ids = "#" + myId;
            // set css part
            Routines.SetCSSPart(myCss, cs);

            OutputHTML zone = new OutputHTML();

            HTMLObject objPage = refPage.Objects.Find(a => { return a.Container == this.name || (String.IsNullOrEmpty(newInfos.objectName) ? false : a.Container == newInfos.objectName + "_" + this.name); });
            HTMLObject objMasterPage = null;
            if (masterRefPage != null)
                objMasterPage = masterRefPage.Objects.Find(a => { return a.Container == this.name || (String.IsNullOrEmpty(newInfos.objectName) ? false : a.Container == newInfos.objectName + "_" + this.name); });
            List<HTMLObject> list = new List<HTMLObject>();
            foreach (MasterObject mObj in objects)
            {
                list.AddRange(mObj.Objects);
            }
            HTMLObject objHtmlObject = list.Find(a => { return a.Container == this.name || (String.IsNullOrEmpty(newInfos.objectName) ? false : a.Container == newInfos.objectName + "_" + this.name); });
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
            output.JavaScript.Append(this.javascript.GeneratedCode);
            return output;
        }

        public OutputHTML GenerateProductionDIV(Page refPage)
        {
            throw new NotImplementedException();
        }

        public OutputHTML GenerateProductionDIV(Page refPage, MasterPage masterRefPage, ParentConstraint parentConstraint)
        {
            OutputHTML output = new OutputHTML();
            CodeCSS myCss = new CodeCSS(this.css);
            string myId = "verti" + Project.IncrementedTraceCounter.ToString();

            ParentConstraint newInfos = Routines.ComputeVerticalZone(parentConstraint, this);
            ConstraintSize cs = new ConstraintSize(newInfos.constraintWidth, newInfos.precedingWidth, newInfos.maximumWidth, newInfos.constraintHeight, newInfos.precedingHeight, newInfos.maximumHeight);

            output.HTML.Append("<div id='" + myId + "' name='" + (String.IsNullOrEmpty(newInfos.objectName) ? this.name : newInfos.objectName + "_" + this.name) + "' " + cs.attributeWidth + " " + cs.attributeHeight + ">");

            // set css part
            if (!CommonDirectories.ConfigDirectories.RemoveTables)
            {
                if (!myCss.Body.AllKeys.Contains("float"))
                    myCss.Body.Add("float", "left");
            }
            myCss.Ids = "#" + myId;
            Routines.SetCSSPart(myCss, cs);

            OutputHTML zone = new OutputHTML();

            HTMLObject objPage = refPage.Objects.Find(a => { return a.Container == this.name || (String.IsNullOrEmpty(newInfos.objectName) ? false : a.Container == newInfos.objectName + "_" + this.name); });
            HTMLObject objMasterPage = null;
            if (masterRefPage != null)
                objMasterPage = masterRefPage.Objects.Find(a => { return a.Container == this.name || (String.IsNullOrEmpty(newInfos.objectName) ? false : a.Container == newInfos.objectName + "_" + this.name); });
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
            Routines.SetDIVDisposition(output.HTML, this.disposition, zone.HTML);

            output.HTML.Append("</div>");
            output.CSS.Append(myCss.GenerateCSS(true, true, true));
            output.JavaScript.Append(this.javascript.GeneratedCode);
            return output;
        }

        public OutputHTML GenerateProductionDIV(Page refPage, MasterPage masterRefPage, List<MasterObject> objects, ParentConstraint parentConstraint)
        {
            OutputHTML output = new OutputHTML();
            CodeCSS myCss = new CodeCSS(this.css);
            string myId = "verti" + Project.IncrementedTraceCounter.ToString();

            ParentConstraint newInfos = Routines.ComputeVerticalZone(parentConstraint, this);
            ConstraintSize cs = new ConstraintSize(newInfos.constraintWidth, newInfos.precedingWidth, newInfos.maximumWidth, newInfos.constraintHeight, newInfos.precedingHeight, newInfos.maximumHeight);

            output.HTML.Append("<div id='" + myId + "' name='" + (String.IsNullOrEmpty(newInfos.objectName) ? this.name : newInfos.objectName + "_" + this.name) + "' " + cs.attributeWidth + " " + cs.attributeHeight + ">");
            myCss.Ids = "#" + myId;

            // set css part
            if (!CommonDirectories.ConfigDirectories.RemoveTables)
            {
                if (!myCss.Body.AllKeys.Contains("float"))
                    myCss.Body.Add("float", "left");
            }
            Routines.SetCSSPart(myCss, cs);

            OutputHTML zone = new OutputHTML();

            HTMLObject objPage = refPage.Objects.Find(a => { return a.Container == this.name || (String.IsNullOrEmpty(newInfos.objectName) ? false : a.Container == newInfos.objectName + "_" + this.name); });
            HTMLObject objMasterPage = null;
            if (masterRefPage != null)
                objMasterPage = masterRefPage.Objects.Find(a => { return a.Container == this.name || (String.IsNullOrEmpty(newInfos.objectName) ? false : a.Container == newInfos.objectName + "_" + this.name); });
            List<HTMLObject> list = new List<HTMLObject>();
            foreach (MasterObject mObj in objects)
            {
                list.AddRange(mObj.Objects);
            }
            HTMLObject objHtmlObject = list.Find(a => { return a.Container == this.name || (String.IsNullOrEmpty(newInfos.objectName) ? false : a.Container == newInfos.objectName + "_" + this.name); });
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
            Routines.SetDIVDisposition(output.HTML, this.disposition, zone.HTML);

            output.HTML.Append("</div>");
            output.CSS.Append(myCss.GenerateCSS(true, true, true));
            output.JavaScript.Append(this.javascript.GeneratedCode);
            return output;
        }

        public OutputHTML GenerateProductionTable(Page refPage)
        {
            throw new NotImplementedException();
        }

        public OutputHTML GenerateProductionTable(Page refPage, MasterPage masterRefPage, ParentConstraint parentConstraint)
        {
            OutputHTML output = new OutputHTML();
            CodeCSS myCss = new CodeCSS(this.css);
            string myId = "verti" + Project.IncrementedTraceCounter.ToString();

            ParentConstraint newInfos = Routines.ComputeVerticalZone(parentConstraint, this);
            ConstraintSize cs = new ConstraintSize(newInfos.constraintWidth, newInfos.precedingWidth, newInfos.maximumWidth, newInfos.constraintHeight, newInfos.precedingHeight, newInfos.maximumHeight);

            output.HTML.Append("<td " + Routines.SetTableDisposition(this.disposition) + " rowspan='" + this.countLines.ToString() + "' colspan='" + this.countColumns.ToString() + "' id='" + myId + "' name='" + (String.IsNullOrEmpty(newInfos.objectName) ? this.name : newInfos.objectName + "_" + this.name) + "' " + cs.attributeWidth + " " + cs.attributeHeight + ">");
            myCss.Ids = "#" + myId;

            // set css part
            Routines.SetCSSPart(myCss, cs);

            OutputHTML zone = new OutputHTML();

            HTMLObject objPage = refPage.Objects.Find(a => { return a.Container == this.name || (String.IsNullOrEmpty(newInfos.objectName) ? false : a.Container == newInfos.objectName + "_" + this.name); });
            HTMLObject objMasterPage = null;
            if (masterRefPage != null)
                objMasterPage = masterRefPage.Objects.Find(a => { return a.Container == this.name || (String.IsNullOrEmpty(newInfos.objectName) ? false : a.Container == newInfos.objectName + "_" + this.name); });
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
            output.JavaScript.Append(this.javascript.GeneratedCode);
            return output;
        }

        public OutputHTML GenerateProductionTable(Page refPage, MasterPage masterRefPage, List<MasterObject> objects, ParentConstraint parentConstraint)
        {
            OutputHTML output = new OutputHTML();
            CodeCSS myCss = new CodeCSS(this.css);
            string myId = "verti" + Project.IncrementedTraceCounter.ToString();

            ParentConstraint newInfos = Routines.ComputeVerticalZone(parentConstraint, this);
            ConstraintSize cs = new ConstraintSize(newInfos.constraintWidth, newInfos.precedingWidth, newInfos.maximumWidth, newInfos.constraintHeight, newInfos.precedingHeight, newInfos.maximumHeight);

            output.HTML.Append("<td " + Routines.SetTableDisposition(this.disposition) + " rowspan='" + this.countLines.ToString() + "' colspan='" + this.countColumns.ToString() + "' id='" + myId + "' name='" + (String.IsNullOrEmpty(newInfos.objectName) ? this.name : newInfos.objectName + "_" + this.name) + "' " + cs.attributeWidth + " " + cs.attributeHeight + ">");

            myCss.Ids = "#" + myId;
            // set css part
            Routines.SetCSSPart(myCss, cs);

            OutputHTML zone = new OutputHTML();

            HTMLObject objPage = refPage.Objects.Find(a => { return a.Container == this.name || (String.IsNullOrEmpty(newInfos.objectName) ? false : a.Container == newInfos.objectName + "_" + this.name); });
            HTMLObject objMasterPage = null;
            if (masterRefPage != null)
                objMasterPage = masterRefPage.Objects.Find(a => { return a.Container == this.name || (String.IsNullOrEmpty(newInfos.objectName) ? false : a.Container == newInfos.objectName + "_" + this.name); });
            List<HTMLObject> list = new List<HTMLObject>();
            foreach (MasterObject mObj in objects)
            {
                list.AddRange(mObj.Objects);
            }
            HTMLObject objHtmlObject = list.Find(a => { return a.Container == this.name || (String.IsNullOrEmpty(newInfos.objectName) ? false : a.Container == newInfos.objectName + "_" + this.name); });
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
            output.JavaScript.Append(this.javascript.GeneratedCode);
            return output;
        }

        public object Clone()
        {
            return new VerticalZone(this);
        }

        #endregion
    }
}
