using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library
{
    [Serializable]
    public class MasterObject : IContainer, IProjectElement, IGenerateDesign, IGenerateDesignDIV, IGenerateDesignTable, IGenerateProduction, IGenerateProductionDIV, IGenerateProductionTable, ICloneable
    {
        #region Private Fields
        private EnumConstraint constraintWidth;
        private EnumConstraint constraintHeight;
        private uint width;
        private uint height;
        private uint countLines;
        private uint countColumns;
        private string container;
        private string title;
        private string name = "mObj" + Project.CurrentProject.IncrementedCounter.ToString();
        private string id = "idMasterObj" + Project.CurrentProject.IncrementedCounter.ToString();
        private List<HTMLObject> objects = new List<HTMLObject>();
        private List<HorizontalZone> hZones = new List<HorizontalZone>();
        private CodeJavaScript javascript = new CodeJavaScript();
        private CodeJavaScript javascriptOnLoad = new CodeJavaScript();
        private CodeCSS css = new CodeCSS();
        private string htmlBefore;
        private string htmlAfter;
        #endregion

        #region Default Constructor
        public MasterObject() { }
        #endregion

        #region Copy Constructor
        private MasterObject(MasterObject refObj)
        {
            this.constraintWidth = refObj.constraintWidth;
            this.constraintHeight = refObj.constraintHeight;
            this.width = refObj.width;
            this.height = refObj.height;
            this.countLines = refObj.countLines;
            this.countColumns = refObj.countColumns;
            this.container = ExtensionMethods.CloneThis(refObj.container);
            this.title = ExtensionMethods.CloneThis(refObj.title);
            foreach (HTMLObject obj in refObj.objects)
            {
                this.objects.Add(obj.Clone() as HTMLObject);
            }
            foreach (HorizontalZone hz in refObj.hZones)
            {
                this.hZones.Add(hz.Clone() as HorizontalZone);
            }
            this.javascript = refObj.javascript.Clone() as CodeJavaScript;
            this.javascriptOnLoad = refObj.javascriptOnLoad.Clone() as CodeJavaScript;
            this.css = refObj.css.Clone() as CodeCSS;
            this.htmlBefore = ExtensionMethods.CloneThis(refObj.htmlBefore);
            this.htmlAfter = ExtensionMethods.CloneThis(refObj.htmlAfter);
        }
        #endregion

        #region Public Properties
        public bool RelativeWidth
        {
            get { return this.constraintWidth == EnumConstraint.RELATIVE; }
            set { this.constraintWidth = EnumConstraint.RELATIVE; }
        }

        public bool RelativeHeight
        {
            get { return this.constraintHeight == EnumConstraint.RELATIVE; }
            set { this.constraintHeight = EnumConstraint.RELATIVE; }
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

        public uint CountLines
        {
            get { return this.countLines; }
            set { this.countLines = value; }
        }

        public uint CountColumns
        {
            get { return this.countColumns; }
            set { this.countColumns = value; }
        }

        public string SizeString
        {
            get { return this.width.ToString() + "x" + this.height.ToString(); }
        }

        public string GridSizeString
        {
            get { return this.countColumns.ToString() + "x" + this.countLines.ToString(); }
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

        public List<HTMLObject> Objects
        {
            get { return this.objects; }
        }

        public List<HorizontalZone> HorizontalZones
        {
            get { return this.hZones; }
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

        public CodeCSS CSS
        {
            get { return this.css; }
        }

        public string HTMLBefore
        {
            get { return this.htmlBefore; }
            set { this.htmlBefore = value; }
        }

        public string HTMLAfter
        {
            get { return this.htmlAfter; }
            set { this.htmlAfter = value; }
        }

        public OutputHTML GenerateDesignDIV()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Public Methods
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

        public void MakeZones(List<SizedRectangle> list)
        {
            SizedRectangle[,] indexes = new SizedRectangle[this.CountLines, this.CountColumns];
            for (int index = 0; index < list.Count; ++index)
            {
                SizedRectangle current = list[index];
                indexes[current.Top, current.Left] = current;
            }

            // ranger les données dans la master page
            for (int pos_ligne = 0; pos_ligne < this.CountLines; ++pos_ligne)
            {
                uint hSize = 0;
                uint vSize = 0;
                Nullable<int> minCountLines = null;
                Library.HorizontalZone hz = new Library.HorizontalZone();
                hz.ConstraintWidth = this.ConstraintWidth;
                hz.ConstraintHeight = this.ConstraintHeight;
                for (int pos_colonne = 0; pos_colonne < this.CountColumns; ++pos_colonne)
                {
                    if (indexes[pos_ligne, pos_colonne] != null)
                    {
                        Library.VerticalZone vz = new Library.VerticalZone();
                        vz.ConstraintWidth = this.ConstraintWidth;
                        vz.ConstraintHeight = this.ConstraintHeight;
                        SizedRectangle sr = indexes[pos_ligne, pos_colonne];
                        vz.CountColumns = sr.Right - sr.Left;
                        vz.CountLines = sr.Bottom - sr.Top;
                        if (sr.Height > 0) { vz.Height = (uint)sr.Height; if (vz.Height > vSize) vSize = vz.Height; }
                        if (sr.Width > 0) { vz.Width = (uint)sr.Width; hSize += vz.Width; }
                        if (minCountLines.HasValue)
                        {
                            if (minCountLines.Value > vz.CountLines)
                            {
                                minCountLines = vz.CountLines;
                            }
                        }
                        else
                        {
                            minCountLines = vz.CountLines;
                        }
                        if (hz == null)
                            hz = new Library.HorizontalZone();
                        hz.VerticalZones.Add(vz);
                    }
                }
                if (hz != null)
                {
                    if (minCountLines.HasValue)
                        hz.CountLines = minCountLines.Value;
                    else
                        hz.CountLines = 0;
                    hz.Width = hSize;
                    hz.Height = vSize;
                    // cette longueur et hauteur servira pour calculer le resize des zones verticales
                    hz.ConstraintWidth = Library.EnumConstraint.AUTO;
                    hz.ConstraintHeight = Library.EnumConstraint.AUTO;
                    this.HorizontalZones.Add(hz);
                }
            }
        }
        #endregion

        #region Interfaces implementations
        public OutputHTML GenerateDesignDIV(Page refPage)
        {
            DesignPage config = new DesignPage();
            config.constraintWidth = this.constraintWidth;
            config.constraintHeight = this.constraintHeight;
            config.width = this.width;
            config.height = this.height;
            config.cssPart = this.css;
            config.javascriptPart = this.javascript;
            config.onload = this.javascriptOnLoad;
            config.zones = this.hZones;
            config.includeContainer = true;
            config.subObjects = this.objects;
            return Routines.GenerateDesignPageDIV(refPage, this, config);
        }

        public OutputHTML GenerateDesignDIV(Page refPage, MasterPage masterRefPage, ParentConstraint parentConstraint)
        {
            OutputHTML output = new OutputHTML();
            CodeCSS myCss = new CodeCSS(this.css);
            string myId = "mObj" + Project.IncrementedTraceCounter.ToString();

            output.HTML.Append(this.htmlBefore);

            ParentConstraint newInfos = Routines.ComputeMasterObject(parentConstraint, this);
            ConstraintSize cs = new ConstraintSize(newInfos.constraintWidth, newInfos.precedingWidth, newInfos.maximumWidth, newInfos.constraintHeight, newInfos.precedingHeight, newInfos.maximumHeight);

            Routines.SetCSSPart(myCss, cs);
            myCss.Ids = "#" + myId;
            Routines.SetObjectDisposition(parentConstraint, myCss, newInfos);
            output.CSS.Append(myCss.GenerateCSS(true, true, true));
            output.JavaScript.Append(this.javascript.GeneratedCode);
            output.JavaScriptOnLoad.Append(this.javascriptOnLoad.GeneratedCode);

            output.HTML.Append("<div id='" + myId + "' name='" + newInfos.objectName + "' " + cs.attributeWidth + " " + cs.attributeHeight + ">");

            List<MasterObject> list = new List<MasterObject>();
            list.Add(this);

            foreach (HorizontalZone hz in this.hZones)
            {
                OutputHTML hzone = hz.GenerateDesignDIV(refPage, masterRefPage, list, newInfos);
                output.HTML.Append(hzone.HTML.ToString());
                output.CSS.Append(hzone.CSS.ToString());
                output.JavaScript.Append(hzone.JavaScript.ToString());
                output.JavaScriptOnLoad.Append(hzone.JavaScriptOnLoad.ToString());
            }
            output.HTML.Append("</div>");
            output.HTML.Append(this.htmlAfter);
            return output;
        }

        public OutputHTML GenerateDesignDIV(Page refPage, List<MasterObject> objects, ParentConstraint parentConstraint)
        {
            OutputHTML output = new OutputHTML();
            CodeCSS myCss = new CodeCSS(this.css);
            string myId = "mObj" + Project.IncrementedTraceCounter.ToString();

            output.HTML.Append(this.htmlBefore);

            ParentConstraint newInfos = Routines.ComputeMasterObject(parentConstraint, this);
            ConstraintSize cs = new ConstraintSize(newInfos.constraintWidth, newInfos.precedingWidth, newInfos.maximumWidth, newInfos.constraintHeight, newInfos.precedingHeight, newInfos.maximumHeight);

            myCss.Ids = "#" + myId;
            Routines.SetCSSPart(myCss, cs);
            Routines.SetObjectDisposition(parentConstraint, myCss, newInfos);
            output.CSS.Append(myCss.GenerateCSS(true, true, true));
            output.JavaScript.Append(this.javascript.GeneratedCode);
            output.JavaScriptOnLoad.Append(this.javascriptOnLoad.GeneratedCode);

            output.HTML.Append("<div id='" + myId + "' name='" + newInfos.objectName + "' " + cs.attributeWidth + " " + cs.attributeHeight + ">");

            objects.Add(this);
            foreach (HorizontalZone hz in this.hZones)
            {
                OutputHTML hzone = hz.GenerateDesignDIV(refPage, objects, newInfos);
                output.HTML.Append(hzone.HTML.ToString());
                output.CSS.Append(hzone.CSS.ToString());
                output.JavaScript.Append(hzone.JavaScript.ToString());
                output.JavaScriptOnLoad.Append(hzone.JavaScriptOnLoad.ToString());
            }
            output.HTML.Append("</div>");
            output.HTML.Append(this.htmlAfter);
            return output;
        }

        public OutputHTML GenerateDesignDIV(Page refPage, MasterPage masterRefPage, List<MasterObject> objects, ParentConstraint parentConstraint)
        {
            OutputHTML output = new OutputHTML();
            CodeCSS myCss = new CodeCSS(this.css);
            string myId = "mObj" + Project.IncrementedTraceCounter.ToString();

            output.HTML.Append(this.htmlBefore);

            ParentConstraint newInfos = Routines.ComputeMasterObject(parentConstraint, this);
            ConstraintSize cs = new ConstraintSize(newInfos.constraintWidth, newInfos.precedingWidth, newInfos.maximumWidth, newInfos.constraintHeight, newInfos.precedingHeight, newInfos.maximumHeight);

            myCss.Ids = "#" + myId;
            Routines.SetCSSPart(myCss, cs);
            Routines.SetObjectDisposition(parentConstraint, myCss, newInfos);
            output.CSS.Append(myCss.GenerateCSS(true, true, true));
            output.JavaScript.Append(this.javascript.GeneratedCode);
            output.JavaScriptOnLoad.Append(this.javascriptOnLoad.GeneratedCode);

            output.HTML.Append("<div id='" + myId + "' name='" + newInfos.objectName + "' " + cs.attributeWidth + " " + cs.attributeHeight + ">");

            objects.Add(this);
            foreach (HorizontalZone hz in this.hZones)
            {
                OutputHTML hzone = hz.GenerateDesignDIV(refPage, masterRefPage, objects, newInfos);
                output.HTML.Append(hzone.HTML.ToString());
                output.CSS.Append(hzone.CSS.ToString());
                output.JavaScript.Append(hzone.JavaScript.ToString());
                output.JavaScriptOnLoad.Append(hzone.JavaScriptOnLoad.ToString());
            }
            output.HTML.Append("</div>");
            output.HTML.Append(this.htmlAfter);
            return output;
        }

        public OutputHTML GenerateDesignTable(Page refPage)
        {
            DesignPage config = new DesignPage();
            config.constraintWidth = this.constraintWidth;
            config.constraintHeight = this.constraintHeight;
            config.width = this.width;
            config.height = this.height;
            config.cssPart = this.css;
            config.javascriptPart = this.javascript;
            config.onload = this.javascriptOnLoad;
            config.zones = this.hZones;
            config.includeContainer = true;
            config.subObjects = this.objects;
            return Routines.GenerateDesignPageTable(refPage, this, config);
        }

        public OutputHTML GenerateDesignTable(Page refPage, MasterPage masterRefPage, ParentConstraint parentConstraint)
        {
            OutputHTML output = new OutputHTML();
            CodeCSS myCss = new CodeCSS(this.css);
            string myId = "mObj" + Project.IncrementedTraceCounter.ToString();

            output.HTML.Append(this.htmlBefore);

            ParentConstraint newInfos = Routines.ComputeMasterObject(parentConstraint, this);
            ConstraintSize cs = new ConstraintSize(newInfos.constraintWidth, newInfos.precedingWidth, newInfos.maximumWidth, newInfos.constraintHeight, newInfos.precedingHeight, newInfos.maximumHeight);

            myCss.Ids = "#" + myId;
            Routines.SetCSSPart(myCss, cs);
            Routines.SetObjectDisposition(parentConstraint, myCss, newInfos);
            output.CSS.Append(myCss.GenerateCSS(true, true, true));
            output.JavaScript.Append(this.javascript.GeneratedCode);
            output.JavaScriptOnLoad.Append(this.javascriptOnLoad.GeneratedCode);

            List<MasterObject> list = new List<MasterObject>();
            list.Add(this);

            output.HTML.Append("<table " + cs.attributeWidth + " " + cs.attributeHeight + " id='" + myId + "' name='" + newInfos.objectName + "' border='0' cellspacing='0' cellpadding='0'>");
            // Si la dernière ligne de la table est vide alors on ne l'ajoute pas
            // raison : compatibité IE/Firefox/Chrome
            // recherche fin de ligne
            int rechfin = this.hZones.Count;
            for (int index = this.hZones.Count - 1; index >= 0; --index)
            {
                if (this.hZones[index].VerticalZones.Count != 0)
                {
                    rechfin = index + 1;
                    break;
                }
            }
            for (int index = 0; index < rechfin; ++index)
            {
                HorizontalZone hz = this.hZones[index];
                if (index + 1 < this.hZones.Count || hz.VerticalZones.Count > 0)
                {
                    OutputHTML hzone = hz.GenerateDesignTable(refPage, masterRefPage, list, newInfos);
                    output.HTML.Append(hzone.HTML.ToString());
                    output.CSS.Append(hzone.CSS.ToString());
                    output.JavaScript.Append(hzone.JavaScript.ToString());
                    output.JavaScriptOnLoad.Append(hzone.JavaScriptOnLoad.ToString());
                }
            }
            output.HTML.Append("</table>");
            output.HTML.Append(this.htmlAfter);
            return output;
        }

        public OutputHTML GenerateDesignTable(Page refPage, List<MasterObject> objects, ParentConstraint parentConstraint)
        {
            OutputHTML output = new OutputHTML();
            CodeCSS myCss = new CodeCSS(this.css);
            string myId = "mObj" + Project.IncrementedTraceCounter.ToString();

            output.HTML.Append(this.htmlBefore);

            ParentConstraint newInfos = Routines.ComputeMasterObject(parentConstraint, this);
            ConstraintSize cs = new ConstraintSize(newInfos.constraintWidth, newInfos.precedingWidth, newInfos.maximumWidth, newInfos.constraintHeight, newInfos.precedingHeight, newInfos.maximumHeight);

            myCss.Ids = "#" + myId;
            Routines.SetCSSPart(myCss, cs);
            Routines.SetObjectDisposition(parentConstraint, myCss, newInfos);
            output.CSS.Append(myCss.GenerateCSS(true, true, true));
            output.JavaScript.Append(this.javascript.GeneratedCode);
            output.JavaScriptOnLoad.Append(this.javascriptOnLoad.GeneratedCode);

            objects.Add(this);

            output.HTML.Append("<table " + cs.attributeWidth + " " + cs.attributeHeight + " id='" + myId + "' name='" + newInfos.objectName + "' border='0' cellspacing='0' cellpadding='0'>");
            // Si la dernière ligne de la table est vide alors on ne l'ajoute pas
            // raison : compatibité IE/Firefox/Chrome
            // recherche fin de ligne
            int rechfin = this.hZones.Count;
            for (int index = this.hZones.Count - 1; index >= 0; --index)
            {
                if (this.hZones[index].VerticalZones.Count != 0)
                {
                    rechfin = index + 1;
                    break;
                }
            }
            for (int index = 0; index < rechfin; ++index)
            {
                HorizontalZone hz = this.hZones[index];
                if (index + 1 < this.hZones.Count || hz.VerticalZones.Count > 0)
                {
                    OutputHTML hzone = hz.GenerateDesignTable(refPage, objects, newInfos);
                    output.HTML.Append(hzone.HTML.ToString());
                    output.CSS.Append(hzone.CSS.ToString());
                    output.JavaScript.Append(hzone.JavaScript.ToString());
                    output.JavaScriptOnLoad.Append(hzone.JavaScriptOnLoad.ToString());
                }
            }
            output.HTML.Append("</table>");
            output.HTML.Append(this.htmlAfter);
            return output;
        }

        public OutputHTML GenerateDesignTable(Page refPage, MasterPage masterRefPage, List<MasterObject> objects, ParentConstraint parentConstraint)
        {
            OutputHTML output = new OutputHTML();
            CodeCSS myCss = new CodeCSS(this.css);
            string myId = "mObj" + Project.IncrementedTraceCounter.ToString();

            output.HTML.Append(this.htmlBefore);

            ParentConstraint newInfos = Routines.ComputeMasterObject(parentConstraint, this);
            ConstraintSize cs = new ConstraintSize(newInfos.constraintWidth, newInfos.precedingWidth, newInfos.maximumWidth, newInfos.constraintHeight, newInfos.precedingHeight, newInfos.maximumHeight);

            myCss.Ids = "#" + myId;
            Routines.SetCSSPart(myCss, cs);
            Routines.SetObjectDisposition(parentConstraint, myCss, newInfos);
            output.CSS.Append(myCss.GenerateCSS(true, true, true));
            output.JavaScript.Append(this.javascript.GeneratedCode);
            output.JavaScriptOnLoad.Append(this.javascriptOnLoad.GeneratedCode);

            objects.Add(this);

            output.HTML.Append("<table " + cs.attributeWidth + " " + cs.attributeHeight + " id='" + myId + "' name='" + newInfos.objectName + "' border='0' cellspacing='0' cellpadding='0'>");
            // Si la dernière ligne de la table est vide alors on ne l'ajoute pas
            // raison : compatibité IE/Firefox/Chrome
            // recherche fin de ligne
            int rechfin = this.hZones.Count;
            for (int index = this.hZones.Count - 1; index >= 0; --index)
            {
                if (this.hZones[index].VerticalZones.Count != 0)
                {
                    rechfin = index + 1;
                    break;
                }
            }

            for (int index = 0; index < rechfin; ++index)
            {
                HorizontalZone hz = this.hZones[index];
                if (index + 1 < this.hZones.Count || hz.VerticalZones.Count > 0)
                {
                    OutputHTML hzone = hz.GenerateDesignTable(refPage, masterRefPage, objects, newInfos);
                    output.HTML.Append(hzone.HTML.ToString());
                    output.CSS.Append(hzone.CSS.ToString());
                    output.JavaScript.Append(hzone.JavaScript.ToString());
                    output.JavaScriptOnLoad.Append(hzone.JavaScriptOnLoad.ToString());
                }
            }
            output.HTML.Append("</table>");
            output.HTML.Append(this.htmlAfter);
            return output;
        }

        public OutputHTML GenerateDesign()
        {
            if (this.constraintWidth == EnumConstraint.RELATIVE || this.constraintHeight == EnumConstraint.RELATIVE)
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
                foreach (HorizontalZone hz in this.hZones)
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

        public OutputHTML GenerateDesign(Page refPage)
        {
            throw new NotImplementedException();
        }

        public OutputHTML GenerateDesign(Page refPage, MasterPage masterRefPage, ParentConstraint parentConstraint)
        {
            if (this.constraintWidth == EnumConstraint.RELATIVE || this.constraintHeight == EnumConstraint.RELATIVE)
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
                foreach (HorizontalZone hz in this.hZones)
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

        public OutputHTML GenerateDesign(Page refPage, MasterPage masterRefPage, List<MasterObject> objects, ParentConstraint parentConstraint)
        {
            if (this.constraintWidth == EnumConstraint.RELATIVE || this.constraintHeight == EnumConstraint.RELATIVE)
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
                foreach (HorizontalZone hz in this.hZones)
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


        public OutputHTML GenerateDesign(Page refPage, List<MasterObject> objects, ParentConstraint parentConstraint)
        {
            if (this.constraintWidth == EnumConstraint.RELATIVE || this.constraintHeight == EnumConstraint.RELATIVE)
            {
                OutputHTML html = this.GenerateDesignTable(refPage, objects, parentConstraint);
                return html;
            }
            else
            {
                // il faut décider si l'on utilise une table ou des div
                // s'il existe une colonne dont countLines > countLines de l'horizontal alors on utilise une table
                bool cannotUseDiv = false;
                foreach (HorizontalZone hz in this.hZones)
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
            if (this.constraintWidth == EnumConstraint.RELATIVE || this.constraintHeight == EnumConstraint.RELATIVE)
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
                foreach (HorizontalZone hz in this.hZones)
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

        public OutputHTML GenerateProduction(Page refPage, MasterPage masterRefPage, List<MasterObject> objects, ParentConstraint parentConstraint)
        {
            if (this.constraintWidth == EnumConstraint.RELATIVE || this.constraintHeight == EnumConstraint.RELATIVE)
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
                foreach (HorizontalZone hz in this.hZones)
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

        public OutputHTML GenerateProductionDIV(Page refPage)
        {
            throw new NotImplementedException();
        }

        public OutputHTML GenerateProductionDIV(Page refPage, MasterPage masterRefPage, ParentConstraint parentConstraint)
        {
            OutputHTML output = new OutputHTML();
            CodeCSS myCss = new CodeCSS(this.css);
            string myId = "mObj" + Project.IncrementedTraceCounter.ToString();
            OutputHTML html = new OutputHTML();

            output.HTML.Append(this.htmlBefore);

            ParentConstraint newInfos = Routines.ComputeMasterObject(parentConstraint, this);
            ConstraintSize cs = new ConstraintSize(newInfos.constraintWidth, newInfos.precedingWidth, newInfos.maximumWidth, newInfos.constraintHeight, newInfos.precedingHeight, newInfos.maximumHeight);

            Routines.SetCSSPart(myCss, cs);
            myCss.Ids = "#" + myId;
            Routines.SetObjectDisposition(parentConstraint, myCss, newInfos);
            html.CSS.Append(myCss.GenerateCSS(true, true, true));
            html.JavaScript.Append(this.javascript.GeneratedCode);
            html.JavaScriptOnLoad.Append(this.javascriptOnLoad.GeneratedCode);

            List<MasterObject> list = new List<MasterObject>();
            list.Add(this);

            // generate global Container
            OutputHTML global = new OutputHTML();
            bool hasGlobalContainer = Routines.WriteProductionGlobalContainer(this.name, this.id, global, this.objects, refPage, masterRefPage, list, parentConstraint, cs);

            html.HTML.Append("<div id='" + myId + "' name='" + newInfos.objectName + "' " + cs.attributeWidth + " " + cs.attributeHeight + ">");

            foreach (HorizontalZone hz in this.hZones)
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
                group.Append("<div style='position:relative' id='group_" + myId + "' name='group_" + this.name + "' " + cs.attributeWidth + " " + cs.attributeHeight + ">");
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

            output.HTML.Append(this.htmlAfter);
            return output;
        }

        public OutputHTML GenerateProductionDIV(Page refPage, MasterPage masterRefPage, List<MasterObject> objects, ParentConstraint parentConstraint)
        {
            OutputHTML output = new OutputHTML();
            CodeCSS myCss = new CodeCSS(this.css);
            string myId = "mObj" + Project.IncrementedTraceCounter.ToString();
            OutputHTML html = new OutputHTML();

            output.HTML.Append(this.htmlBefore);

            ParentConstraint newInfos = Routines.ComputeMasterObject(parentConstraint, this);
            ConstraintSize cs = new ConstraintSize(newInfos.constraintWidth, newInfos.precedingWidth, newInfos.maximumWidth, newInfos.constraintHeight, newInfos.precedingHeight, newInfos.maximumHeight);

            myCss.Ids = "#" + myId;
            Routines.SetCSSPart(myCss, cs);
            Routines.SetObjectDisposition(parentConstraint, myCss, newInfos);
            html.CSS.Append(myCss.GenerateCSS(true, true, true));
            html.JavaScript.Append(this.javascript.GeneratedCode);
            html.JavaScriptOnLoad.Append(this.javascriptOnLoad.GeneratedCode);

            objects.Add(this);

            // generate global Container
            OutputHTML global = new OutputHTML();
            bool hasGlobalContainer = Routines.WriteProductionGlobalContainer(this.name, this.id, global, this.objects, refPage, masterRefPage, objects, parentConstraint, cs);

            html.HTML.Append("<div id='" + myId + "' name='" + newInfos.objectName + "' " + cs.attributeWidth + " " + cs.attributeHeight + ">");

            foreach (HorizontalZone hz in this.hZones)
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
                group.Append("<div style='position:relative' id='group_" + myId + "' name='group_" + this.name + "' " + cs.attributeWidth + " " + cs.attributeHeight + ">");
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
            
            output.HTML.Append(this.htmlAfter);
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
            string myId = "mObj" + Project.IncrementedTraceCounter.ToString();
            OutputHTML html = new OutputHTML();

            output.HTML.Append(this.htmlBefore);

            ParentConstraint newInfos = Routines.ComputeMasterObject(parentConstraint, this);
            ConstraintSize cs = new ConstraintSize(newInfos.constraintWidth, newInfos.precedingWidth, newInfos.maximumWidth, newInfos.constraintHeight, newInfos.precedingHeight, newInfos.maximumHeight);

            myCss.Ids = "#" + myId;
            Routines.SetCSSPart(myCss, cs);
            Routines.SetObjectDisposition(parentConstraint, myCss, newInfos);
            html.CSS.Append(myCss.GenerateCSS(true, true, true));
            html.JavaScript.Append(this.javascript.GeneratedCode);
            html.JavaScriptOnLoad.Append(this.javascriptOnLoad.GeneratedCode);

            List<MasterObject> list = new List<MasterObject>();
            list.Add(this);

            // generate global Container
            OutputHTML global = new OutputHTML();
            bool hasGlobalContainer = Routines.WriteProductionGlobalContainer(this.name, this.id, global, this.objects, refPage, masterRefPage, list, parentConstraint, cs);

            html.HTML.Append("<table " + cs.attributeWidth + " " + cs.attributeHeight + " id='" + myId + "' name='" + newInfos.objectName + "' border='0' cellspacing='0' cellpadding='0'>");

            // Si la dernière ligne de la table est vide alors on ne l'ajoute pas
            // raison : compatibité IE/Firefox/Chrome
            // recherche fin de ligne
            int rechfin = this.hZones.Count;
            for (int index = this.hZones.Count - 1; index >= 0; --index)
            {
                if (this.hZones[index].VerticalZones.Count != 0)
                {
                    rechfin = index + 1;
                    break;
                }
            }
            for (int index = 0; index < rechfin; ++index)
            {
                HorizontalZone hz = this.hZones[index];
                if (index + 1 < this.hZones.Count || hz.VerticalZones.Count > 0)
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
                group.Append("<div style='position:relative' id='group_" + myId + "' name='group_" + this.name + "' " + cs.attributeWidth + " " + cs.attributeHeight + ">");
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

            output.HTML.Append(this.htmlAfter);
            return output;
        }

        public OutputHTML GenerateProductionTable(Page refPage, MasterPage masterRefPage, List<MasterObject> objects, ParentConstraint parentConstraint)
        {
            OutputHTML output = new OutputHTML();
            CodeCSS myCss = new CodeCSS(this.css);
            string myId = "mObj" + Project.IncrementedTraceCounter.ToString();
            OutputHTML html = new OutputHTML();

            output.HTML.Append(this.htmlBefore);

            ParentConstraint newInfos = Routines.ComputeMasterObject(parentConstraint, this);
            ConstraintSize cs = new ConstraintSize(newInfos.constraintWidth, newInfos.precedingWidth, newInfos.maximumWidth, newInfos.constraintHeight, newInfos.precedingHeight, newInfos.maximumHeight);

            myCss.Ids = "#" + myId;
            Routines.SetCSSPart(myCss, cs);
            Routines.SetObjectDisposition(parentConstraint, myCss, newInfos);
            html.CSS.Append(myCss.GenerateCSS(true, true, true));
            html.JavaScript.Append(this.javascript.GeneratedCode);
            html.JavaScriptOnLoad.Append(this.javascriptOnLoad.GeneratedCode);

            // generate global Container
            OutputHTML global = new OutputHTML();
            bool hasGlobalContainer = Routines.WriteProductionGlobalContainer(this.name, this.id, global, this.objects, refPage, masterRefPage, objects, parentConstraint, cs);

            html.HTML.Append("<table " + cs.attributeWidth + " " + cs.attributeHeight + " id='" + myId + "' name='" + newInfos.objectName + "' border='0' cellspacing='0' cellpadding='0'>");

            objects.Add(this);

            // Si la dernière ligne de la table est vide alors on ne l'ajoute pas
            // raison : compatibité IE/Firefox/Chrome
            // recherche fin de ligne
            int rechfin = this.hZones.Count;
            for (int index = this.hZones.Count - 1; index >= 0; --index)
            {
                if (this.hZones[index].VerticalZones.Count != 0)
                {
                    rechfin = index + 1;
                    break;
                }
            }
            for (int index = 0; index < rechfin; ++index)
            {
                HorizontalZone hz = this.hZones[index];
                if (index + 1 < this.hZones.Count || hz.VerticalZones.Count > 0)
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
                group.Append("<div style='position:relative' id='group_" + myId + "' name='group_" + this.name + "' " + cs.attributeWidth + " " + cs.attributeHeight + ">");
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

            output.HTML.Append(this.htmlAfter);
            return output;
        }
        #endregion

        public string TypeName
        {
            get { return "MasterObject"; }
        }

        public string ElementTitle
        {
            get { return this.title; }
        }

        public object Clone()
        {
            MasterObject mo = new MasterObject(this);
            return mo;
        }
    }
}
