using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library
{
    [Serializable]
    public class HTMLObject : IContent, IContainer, IGenerateDesign, IGenerateProduction, ICloneable
    {
        #region Private Fields

        private string belongsTo;
        private EnumConstraint constraintWidth;
        private EnumConstraint constraintHeight;
        private string masterObjectName;
        private string container;
        private string title;
        private uint width;
        private uint height;
        private string name = "obj" + Project.CurrentProject.IncrementedCounter.ToString();
        private string id = "idObj" + Project.CurrentProject.IncrementedCounter.ToString();
        private string html;
        private CodeJavaScript javascript = new CodeJavaScript();
        private CodeJavaScript javascriptOnLoad = new CodeJavaScript();
        private CodeCSS css = new CodeCSS();
        private List<CodeCSS> cssAdditional = new List<CodeCSS>();
        [NonSerialized]
        private HTMLTool tool;

        #endregion

        #region Public Constructors

        public HTMLObject(HTMLTool hTMLTool)
        {
            this.tool = hTMLTool;
            this.width = hTMLTool.Width;
            this.height = hTMLTool.Height;
            this.constraintWidth = hTMLTool.ConstraintWidth;
            this.constraintHeight = hTMLTool.ConstraintHeight;
            this.title = hTMLTool.Title;
            this.html = hTMLTool.HTML;
            if (hTMLTool.JavaScript.Code != null)
                this.javascript.Code = hTMLTool.JavaScript.Code.Clone() as String;
            if (hTMLTool.JavaScriptOnLoad.Code != null)
                this.javascriptOnLoad.Code = hTMLTool.JavaScriptOnLoad.Code.Clone() as String;
            this.css = new CodeCSS(hTMLTool.CSS);
            this.css.Ids = "#" + this.id;
            this.cssAdditional = (from CodeCSS c in hTMLTool.CSSAdditional select c.Clone() as CodeCSS).ToList();
        }

        public HTMLObject() { this.css.Ids = "#" + this.id; }

        private HTMLObject(HTMLObject obj)
        {
            this.tool = obj.tool;
            this.masterObjectName = ExtensionMethods.CloneThis(obj.masterObjectName);
            this.width = obj.width;
            this.height = obj.height;
            this.constraintWidth = obj.constraintWidth;
            this.constraintHeight = obj.constraintHeight;
            this.title = ExtensionMethods.CloneThis(obj.title);
            this.container = ExtensionMethods.CloneThis(obj.container);
            this.html = ExtensionMethods.CloneThis(obj.html);
            this.javascript = obj.javascript.Clone() as CodeJavaScript;
            this.javascriptOnLoad = obj.javascriptOnLoad.Clone() as CodeJavaScript;
            this.css = obj.css.Clone() as CodeCSS;
            this.css.Ids = "#" + this.id;
            this.cssAdditional.AddRange(from CodeCSS c in this.cssAdditional select c.Clone() as CodeCSS);
        }

        #endregion

        #region Public Properties

        public bool IsMasterObject
        {
            get { return !String.IsNullOrEmpty(this.masterObjectName); }
        }

        public string BelongsTo
        {
            get { return this.belongsTo; }
            set { this.belongsTo = value; }
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

        public string MasterObjectName
        {
            get { return this.masterObjectName; }
            set { this.masterObjectName = value; }
        }

        public HTMLTool Tool
        {
            get { return this.tool; }
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

        public string Container
        {
            get { return this.container; }
            set { this.container = value; }
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

        public CodeJavaScript JavaScriptOnLoad
        {
            get { return this.javascriptOnLoad; }
        }

        public string JavaScriptSource
        {
            get { return this.javascript.Code; }
            set { this.javascript.Code = value; }
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

        #endregion

        #region Public Methods

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

        public void ImportCSS(CodeCSS css)
        {
            this.css = css.Clone() as CodeCSS;
        }

        public string CSSOutput(bool resolveConfig)
        {
            string output = this.CSS.GenerateCSS(resolveConfig ? true : false, true, resolveConfig) + Environment.NewLine;
            List<string> list = this.CSSAdditional.ConvertAll(a => { return a.GenerateCSS(true, true, resolveConfig) + Environment.NewLine; });
            if (list.Count() > 0) output += list.Aggregate((a, b) => a + b);
            return output;
        }

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

        public OutputHTML GenerateDesign(Page refPage)
        {
            throw new NotImplementedException();
        }

        public OutputHTML GenerateDesign(Page refPage, MasterPage masterRefPage, ParentConstraint parentConstraint)
        {
            if (this.IsMasterObject)
            {
                MasterObject selectedMo = Project.CurrentProject.MasterObjects.Find(mo => { return mo.Name == this.masterObjectName; });
                if (selectedMo != null)
                {
                    // calcul de la taille maximum de l'objet
                    ParentConstraint newParent = new ParentConstraint(this.name, parentConstraint);
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
                CodeCSS myCss = new CodeCSS(this.css);
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
                html.AppendCSS(this.cssAdditional);
                html.JavaScript.Append(this.javascript.GeneratedCode);
                html.JavaScriptOnLoad.Append(this.javascriptOnLoad.GeneratedCode);
                return html;
            }
        }

        public OutputHTML GenerateDesign(Page refPage, List<MasterObject> objects, ParentConstraint parentConstraint)
        {
            if (this.IsMasterObject)
            {
                MasterObject selectedMo = Project.CurrentProject.MasterObjects.Find(mo => { return mo.Name == this.masterObjectName; });
                if (selectedMo != null)
                {
                    // calcul de la taille maximum de l'objet
                    ParentConstraint newParent = new ParentConstraint(this.name, parentConstraint);
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
                CodeCSS myCss = new CodeCSS(this.css);
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
                html.AppendCSS(this.cssAdditional);
                html.JavaScript.Append(this.javascript.GeneratedCode);
                html.JavaScriptOnLoad.Append(this.javascriptOnLoad.GeneratedCode);
                return html;
            }
        }

        public OutputHTML GenerateDesign(Page refPage, MasterPage masterRefPage, List<MasterObject> objects, ParentConstraint parentConstraint)
        {
            if (this.IsMasterObject)
            {
                MasterObject selectedMo = Project.CurrentProject.MasterObjects.Find(mo => { return mo.Name == this.masterObjectName; });
                if (selectedMo != null)
                {
                    // calcul de la taille maximum de l'objet
                    ParentConstraint newParent = new ParentConstraint(this.name, parentConstraint);
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
                CodeCSS myCss = new CodeCSS(this.css);
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
                html.AppendCSS(this.cssAdditional);
                html.JavaScript.Append(this.javascript.GeneratedCode);
                html.JavaScriptOnLoad.Append(this.javascriptOnLoad.GeneratedCode);
                return html;
            }
        }

        public OutputHTML GenerateThumbnail()
        {
            throw new NotImplementedException();
        }

        public OutputHTML GenerateProduction()
        {
            throw new NotImplementedException();
        }

        public OutputHTML GenerateProduction(Page refPage)
        {
            throw new NotImplementedException();
        }

        public OutputHTML GenerateProduction(Page refPage, MasterPage masterRefPage, ParentConstraint parentConstraint)
        {
            if (this.IsMasterObject)
            {
                MasterObject selectedMo = Project.CurrentProject.MasterObjects.Find(mo => { return mo.Name == this.masterObjectName; });
                if (selectedMo != null)
                {
                    // calcul de la taille maximum de l'objet
                    ParentConstraint newParent = new ParentConstraint(this.name, parentConstraint);
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
                CodeCSS myCss = new CodeCSS(this.css);
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
                html.AppendCSS(this.cssAdditional);
                html.JavaScript.Append(this.javascript.GeneratedCode);
                html.JavaScriptOnLoad.Append(this.javascriptOnLoad.GeneratedCode);
                return html;
            }
        }

        public OutputHTML GenerateProduction(Page refPage, MasterPage masterRefPage, List<MasterObject> objects, ParentConstraint parentConstraint)
        {
            if (this.IsMasterObject)
            {
                MasterObject selectedMo = Project.CurrentProject.MasterObjects.Find(mo => { return mo.Name == this.masterObjectName; });
                if (selectedMo != null)
                {
                    // calcul de la taille maximum de l'objet
                    ParentConstraint newParent = new ParentConstraint(this.name, parentConstraint);
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
                CodeCSS myCss = new CodeCSS(this.css);
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
                html.AppendCSS(this.cssAdditional);
                html.JavaScript.Append(this.javascript.GeneratedCode);
                html.JavaScriptOnLoad.Append(this.javascriptOnLoad.GeneratedCode);
                return html;
            }
        }

        public object Clone()
        {
            HTMLObject newObject = new HTMLObject(this);
            return newObject;
        }

        #endregion
    }
}
