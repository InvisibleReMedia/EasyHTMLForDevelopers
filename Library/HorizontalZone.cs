using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library
{
    [Serializable]
    public class HorizontalZone : IContainer, IGenerateDesignDIV, IGenerateDesignTable, IGenerateProductionDIV, IGenerateProductionTable, ICloneable
    {
        private EnumConstraint constraintWidth;
        private EnumConstraint constraintHeight;
        private uint width;
        private uint height;
        private int countLines;
        private string name = "horiz" + Project.CurrentProject.IncrementedCounter.ToString();
        private string id = "idHoriz" + Project.CurrentProject.IncrementedCounter.ToString();
        private List<VerticalZone> vZones = new List<VerticalZone>();
        private CodeJavaScript javascript = new CodeJavaScript();
        private CodeJavaScript javascriptOnLoad = new CodeJavaScript();
        private CodeCSS css = new CodeCSS();

        #region Default Constructor
        public HorizontalZone() { }
        #endregion

        #region Copy constructor
        private HorizontalZone(HorizontalZone hz)
        {
            this.constraintWidth = hz.constraintWidth;
            this.constraintHeight = hz.constraintHeight;
            this.width = hz.width;
            this.height = hz.height;
            this.countLines = hz.countLines;
            foreach (VerticalZone vz in hz.vZones)
            {
                this.vZones.Add(vz.Clone() as VerticalZone);
            }
            this.javascript = hz.javascript.Clone() as CodeJavaScript;
            this.javascriptOnLoad = hz.javascriptOnLoad.Clone() as CodeJavaScript;
            this.css = hz.css.Clone() as CodeCSS;
        }
        #endregion

        #region Public Properties

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

        public int CountLines
        {
            get { return this.countLines; }
            set { this.countLines = value; }
        }

        public List<VerticalZone> VerticalZones
        {
            get { return this.vZones; }
        }

        public uint TotalCountColumns
        {
            get
            {
                uint countColumns = 0;
                foreach (VerticalZone vz in this.vZones)
                {
                    countColumns += (uint)vz.CountColumns;
                }
                return countColumns;
            }
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
            get { return String.Format(Localization.Strings.GetString("HorizontalAreaStringified"), this.name, this.countLines); }
        }

        #endregion

        #region Public Methods

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

        public OutputHTML GenerateDesignDIV()
        {
            throw new NotImplementedException();
        }

        public OutputHTML GenerateDesignDIV(Page refPage, MasterPage masterRefPage, ParentConstraint parentConstraint)
        {
            OutputHTML output = new OutputHTML();
            CodeCSS myCss = new CodeCSS(this.css);
            if (this.vZones.Count > 0)
            {
                string myId = "horiz" + Project.IncrementedTraceCounter.ToString();
                // compute size
                ParentConstraint newInfos = Routines.ComputeHorizontalZone(parentConstraint, this);
                ConstraintSize cs = new ConstraintSize(newInfos.constraintWidth, newInfos.precedingWidth, newInfos.maximumWidth, newInfos.constraintHeight, newInfos.precedingHeight, newInfos.maximumHeight);

                output.HTML.Append("<div id='" + myId + "' name='" + myId + "' " + cs.attributeWidth + " " + cs.attributeHeight + ">");

                // set CSS part
                myCss.Ids = "#" + myId;
                if (myCss.Body.AllKeys.Contains("border"))
                    myCss.Body.Remove("border");
                Routines.SetCSSPart(myCss, cs);

                List<VerticalZone>.Enumerator e = this.vZones.GetEnumerator();
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
                output.JavaScript.Append(this.javascript.GeneratedCode);
                output.JavaScriptOnLoad.Append(this.javascriptOnLoad.GeneratedCode);
            }
            return output;
        }

        public OutputHTML GenerateDesignDIV(Page refPage, List<MasterObject> objects, ParentConstraint parentConstraint)
        {
            OutputHTML output = new OutputHTML();
            CodeCSS myCss = new CodeCSS(this.css);
            if (this.vZones.Count > 0)
            {
                string myId = "horiz" + Project.IncrementedTraceCounter.ToString();
                // compute size
                ParentConstraint newInfos = Routines.ComputeHorizontalZone(parentConstraint, this);
                ConstraintSize cs = new ConstraintSize(newInfos.constraintWidth, newInfos.precedingWidth, newInfos.maximumWidth, newInfos.constraintHeight, newInfos.precedingHeight, newInfos.maximumHeight);

                output.HTML.Append("<div id='" + myId + "' name='" + myId + "' " + cs.attributeWidth + " " + cs.attributeHeight + ">");

                // set CSS part
                myCss.Ids = "#" + myId;
                if (myCss.Body.AllKeys.Contains("border"))
                    myCss.Body.Remove("border");
                Routines.SetCSSPart(myCss, cs);

                List<VerticalZone>.Enumerator e = this.vZones.GetEnumerator();
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
                output.JavaScript.Append(this.javascript.GeneratedCode);
                output.JavaScriptOnLoad.Append(this.javascriptOnLoad.GeneratedCode);
            }
            return output;
        }

        public OutputHTML GenerateDesignDIV(Page refPage)
        {
            throw new NotImplementedException();
        }

        public OutputHTML GenerateDesignDIV(Page refPage, MasterPage masterRefPage, List<MasterObject> objects, ParentConstraint parentConstraint)
        {
            OutputHTML output = new OutputHTML();
            CodeCSS myCss = new CodeCSS(this.css);
            if (this.vZones.Count > 0)
            {
                string myId = "horiz" + Project.IncrementedTraceCounter.ToString();

                // compute size
                ParentConstraint newInfos = Routines.ComputeHorizontalZone(parentConstraint, this);
                ConstraintSize cs = new ConstraintSize(newInfos.constraintWidth, newInfos.precedingWidth, newInfos.maximumWidth, newInfos.constraintHeight, newInfos.precedingHeight, newInfos.maximumHeight);

                output.HTML.Append("<div id='" + myId + "' name='" + myId + "' " + cs.attributeWidth + " " + cs.attributeHeight + ">");

                // set CSS part
                myCss.Ids = "#" + myId;
                Routines.SetCSSPart(myCss, cs);

                List<VerticalZone>.Enumerator e = this.vZones.GetEnumerator();
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
                output.JavaScript.Append(this.javascript.GeneratedCode);
                output.JavaScriptOnLoad.Append(this.javascriptOnLoad.GeneratedCode);
            }
            return output;
        }

        public OutputHTML GenerateDesignTable(Page refPage)
        {
            throw new NotImplementedException();
        }

        public OutputHTML GenerateDesignTable(Page refPage, MasterPage masterRefPage, ParentConstraint parentConstraint)
        {
            OutputHTML output = new OutputHTML();
            CodeCSS myCss = new CodeCSS(this.css);
            string myId = "horiz" + Project.IncrementedTraceCounter.ToString();

            // compute size
            ParentConstraint newInfos = Routines.ComputeHorizontalZone(parentConstraint, this);
            ConstraintSize cs = new ConstraintSize(newInfos.constraintWidth, newInfos.precedingWidth, newInfos.maximumWidth, newInfos.constraintHeight, newInfos.precedingHeight, newInfos.maximumHeight);

            output.HTML.Append("<tr id='" + myId + "' name='" + myId + "' " + cs.attributeWidth + " " + cs.attributeHeight + ">");
            myCss.Ids = "#" + myId;
            Routines.SetCSSPart(myCss, cs);

            foreach (VerticalZone vz in this.vZones)
            {
                OutputHTML zone = vz.GenerateDesignTable(refPage, masterRefPage, newInfos);
                output.HTML.Append(zone.HTML.ToString());
                output.CSS.Append(zone.CSS.ToString());
                output.JavaScript.Append(zone.JavaScript.ToString());
                output.JavaScriptOnLoad.Append(zone.JavaScriptOnLoad.ToString());
            }
            output.HTML.Append("</tr>");
            output.CSS.Append(myCss.GenerateCSS(true, true, true));
            output.JavaScript.Append(this.javascript.GeneratedCode);
            output.JavaScriptOnLoad.Append(this.javascriptOnLoad.GeneratedCode);
            return output;
        }

        public OutputHTML GenerateDesignTable(Page refPage, List<MasterObject> objects, ParentConstraint parentConstraint)
        {
            OutputHTML output = new OutputHTML();
            CodeCSS myCss = new CodeCSS(this.css);
            string myId = "horiz" + Project.IncrementedTraceCounter.ToString();

            // compute size
            ParentConstraint newInfos = Routines.ComputeHorizontalZone(parentConstraint, this);
            ConstraintSize cs = new ConstraintSize(newInfos.constraintWidth, newInfos.precedingWidth, newInfos.maximumWidth, newInfos.constraintHeight, newInfos.precedingHeight, newInfos.maximumHeight);

            output.HTML.Append("<tr id='" + myId + "' name='" + myId + "' " + cs.attributeWidth + " " + cs.attributeHeight + ">");
            myCss.Ids = "#" + myId;
            Routines.SetCSSPart(myCss, cs);

            foreach (VerticalZone vz in this.vZones)
            {
                OutputHTML zone = vz.GenerateDesignTable(refPage, objects, newInfos);
                output.HTML.Append(zone.HTML.ToString());
                output.CSS.Append(zone.CSS.ToString());
                output.JavaScript.Append(zone.JavaScript.ToString());
                output.JavaScriptOnLoad.Append(zone.JavaScriptOnLoad.ToString());
            }
            output.HTML.Append("</tr>");
            output.CSS.Append(myCss.GenerateCSS(true, true, true));
            output.JavaScript.Append(this.javascript.GeneratedCode);
            output.JavaScriptOnLoad.Append(this.javascriptOnLoad.GeneratedCode);
            return output;
        }

        public OutputHTML GenerateDesignTable(Page refPage, MasterPage masterRefPage, List<MasterObject> objects, ParentConstraint parentConstraint)
        {
            OutputHTML output = new OutputHTML();
            CodeCSS myCss = new CodeCSS(this.css);
            string myId = "horiz" + Project.IncrementedTraceCounter.ToString();
            
            // compute size
            ParentConstraint newInfos = Routines.ComputeHorizontalZone(parentConstraint, this);
            ConstraintSize cs = new ConstraintSize(newInfos.constraintWidth, newInfos.precedingWidth, newInfos.maximumWidth, newInfos.constraintHeight, newInfos.precedingHeight, newInfos.maximumHeight);

            output.HTML.Append("<tr id='" + myId + "' name='" + myId + "' " + cs.attributeWidth + " " + cs.attributeHeight + ">");
            myCss.Ids = "#" + myId;
            Routines.SetCSSPart(myCss, cs);

            foreach (VerticalZone vz in this.vZones)
            {
                OutputHTML zone = vz.GenerateDesignTable(refPage, masterRefPage, objects, newInfos);
                output.HTML.Append(zone.HTML.ToString());
                output.CSS.Append(zone.CSS.ToString());
                output.JavaScript.Append(zone.JavaScript.ToString());
                output.JavaScriptOnLoad.Append(zone.JavaScriptOnLoad.ToString());
            }
            output.HTML.Append("</tr>");
            output.CSS.Append(myCss.GenerateCSS(true, true, true));
            output.JavaScript.Append(this.javascript.GeneratedCode);
            output.JavaScriptOnLoad.Append(this.javascriptOnLoad.GeneratedCode);
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
            if (this.vZones.Count > 0)
            {
                string myId = "horiz" + Project.IncrementedTraceCounter.ToString();
                // compute size
                ParentConstraint newInfos = Routines.ComputeHorizontalZone(parentConstraint, this);
                ConstraintSize cs = new ConstraintSize(newInfos.constraintWidth, newInfos.precedingWidth, newInfos.maximumWidth, newInfos.constraintHeight, newInfos.precedingHeight, newInfos.maximumHeight);

                output.HTML.Append("<div id='" + myId + "' name='" + myId + "' " + cs.attributeWidth + " " + cs.attributeHeight + ">");

                // set CSS part
                myCss.Ids = "#" + myId;
                Routines.SetCSSPart(myCss, cs);

                List<VerticalZone>.Enumerator e = this.vZones.GetEnumerator();
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
                output.JavaScript.Append(this.javascript.GeneratedCode);
                output.JavaScriptOnLoad.Append(this.javascriptOnLoad.GeneratedCode);
            }
            return output;
        }

        public OutputHTML GenerateProductionDIV(Page refPage, MasterPage masterRefPage, List<MasterObject> objects, ParentConstraint parentConstraint)
        {
            OutputHTML output = new OutputHTML();
            CodeCSS myCss = new CodeCSS(this.css);
            if (this.vZones.Count > 0)
            {
                string myId = "horiz" + Project.IncrementedTraceCounter.ToString();

                // compute size
                ParentConstraint newInfos = Routines.ComputeHorizontalZone(parentConstraint, this);
                ConstraintSize cs = new ConstraintSize(newInfos.constraintWidth, newInfos.precedingWidth, newInfos.maximumWidth, newInfos.constraintHeight, newInfos.precedingHeight, newInfos.maximumHeight);

                output.HTML.Append("<div id='" + myId + "' name='" + myId + "' " + cs.attributeWidth + " " + cs.attributeHeight + ">");

                // set CSS part
                myCss.Ids = "#" + myId;
                Routines.SetCSSPart(myCss, cs);

                List<VerticalZone>.Enumerator e = this.vZones.GetEnumerator();
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
                output.JavaScript.Append(this.javascript.GeneratedCode);
                output.JavaScriptOnLoad.Append(this.javascriptOnLoad.GeneratedCode);
            }
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
            string myId = "horiz" + Project.IncrementedTraceCounter.ToString();

            // compute size
            ParentConstraint newInfos = Routines.ComputeHorizontalZone(parentConstraint, this);
            ConstraintSize cs = new ConstraintSize(newInfos.constraintWidth, newInfos.precedingWidth, newInfos.maximumWidth, newInfos.constraintHeight, newInfos.precedingHeight, newInfos.maximumHeight);

            output.HTML.Append("<tr id='" + myId + "' name='" + myId + "' " + cs.attributeWidth + " " + cs.attributeHeight + ">");
            myCss.Ids = "#" + myId;
            Routines.SetCSSPart(myCss, cs);

            foreach (VerticalZone vz in this.vZones)
            {
                OutputHTML zone = vz.GenerateProductionTable(refPage, masterRefPage, newInfos);
                output.HTML.Append(zone.HTML.ToString());
                output.CSS.Append(zone.CSS.ToString());
                output.JavaScript.Append(zone.JavaScript.ToString());
                output.JavaScriptOnLoad.Append(zone.JavaScriptOnLoad.ToString());
            }
            output.HTML.Append("</tr>");
            output.CSS.Append(myCss.GenerateCSS(true, true, true));
            output.JavaScript.Append(this.javascript.GeneratedCode);
            output.JavaScriptOnLoad.Append(this.javascriptOnLoad.GeneratedCode);
            return output;
        }

        public OutputHTML GenerateProductionTable(Page refPage, MasterPage masterRefPage, List<MasterObject> objects, ParentConstraint parentConstraint)
        {
            OutputHTML output = new OutputHTML();
            CodeCSS myCss = new CodeCSS(this.css);
            string myId = "horiz" + Project.IncrementedTraceCounter.ToString();

            // compute size
            ParentConstraint newInfos = Routines.ComputeHorizontalZone(parentConstraint, this);
            ConstraintSize cs = new ConstraintSize(newInfos.constraintWidth, newInfos.precedingWidth, newInfos.maximumWidth, newInfos.constraintHeight, newInfos.precedingHeight, newInfos.maximumHeight);

            output.HTML.Append("<tr id='" + myId + "' name='" + myId + "' " + cs.attributeWidth + " " + cs.attributeHeight + ">");
            myCss.Ids = "#" + myId;
            Routines.SetCSSPart(myCss, cs);

            foreach (VerticalZone vz in this.vZones)
            {
                OutputHTML zone = vz.GenerateProductionTable(refPage, masterRefPage, objects, newInfos);
                output.HTML.Append(zone.HTML.ToString());
                output.CSS.Append(zone.CSS.ToString());
                output.JavaScript.Append(zone.JavaScript.ToString());
                output.JavaScriptOnLoad.Append(zone.JavaScriptOnLoad.ToString());
            }
            output.HTML.Append("</tr>");
            output.CSS.Append(myCss.GenerateCSS(true, true, true));
            output.JavaScript.Append(this.javascript.GeneratedCode);
            output.JavaScriptOnLoad.Append(this.javascriptOnLoad.GeneratedCode);
            return output;
        }

        public object Clone()
        {
            HorizontalZone newObject = new HorizontalZone(this);
            return newObject;
        }

        #endregion
    }
}
