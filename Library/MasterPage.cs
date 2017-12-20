using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Library
{
    [Serializable]
    public class MasterPage : IContainer, IProjectElement, IGenerateDesign, IGenerateDesignDIV, IGenerateDesignTable, IGenerateProduction, IGenerateProductionDIV, IGenerateProductionTable, ICloneable
    {
        #region Private Fields
        private EnumConstraint constraintWidth, constraintHeight;
        private bool cssOnFile;
        private string cssFileName;
        private bool javascriptOnFile;
        private string javascriptFileName;
        private uint width;
        private uint height;
        private uint countLines;
        private uint countColumns;
        private string name;
        private List<HTMLObject> objects = new List<HTMLObject>();
        private List<HorizontalZone> hZones = new List<HorizontalZone>();
        private CodeJavaScript javascript = new CodeJavaScript();
        private List<string> javascriptFiles = new List<string>();
        private CodeJavaScript javascriptOnLoad = new CodeJavaScript();
        private string meta;
        private CodeCSS css = new CodeCSS("body");
        #endregion

        #region Default Constructor
        public MasterPage() { }
        #endregion

        #region Copy Constructor
        private MasterPage(MasterPage refObj)
        {
            this.constraintWidth = refObj.constraintWidth;
            this.constraintHeight = refObj.constraintHeight;
            this.cssOnFile = refObj.cssOnFile;
            this.cssFileName = ExtensionMethods.CloneThis(refObj.cssFileName);
            this.javascriptOnFile = refObj.javascriptOnFile;
            this.javascriptFileName = ExtensionMethods.CloneThis(refObj.javascriptFileName);
            this.width = refObj.width;
            this.height = refObj.height;
            this.countLines = refObj.countLines;
            this.countColumns = refObj.countColumns;
            this.name = ExtensionMethods.CloneThis(refObj.name);
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
            this.meta = ExtensionMethods.CloneThis(refObj.meta);
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

        public bool IsCSSOnFile
        {
            get { return this.cssOnFile; }
            set { this.cssOnFile = value; }
        }

        public string CSSFileName
        {
            get { return this.cssFileName; }
            set { this.cssFileName = value; }
        }

        public bool IsJavaScriptOnFile
        {
            get { return this.javascriptOnFile; }
            set { this.javascriptOnFile = value; }
        }

        public string JavaScriptFileName
        {
            get { return this.javascriptFileName; }
            set { this.javascriptFileName = value; }
        }

        public List<string> JavascriptFiles
        {
            get { return this.javascriptFiles; }
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

        public string GridSizeString
        {
            get { return this.countColumns.ToString() + "x" + this.countLines.ToString(); }
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

        public string Meta
        {
            get { return this.meta; }
            set { this.meta = value; }
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
                        vz.CountColumns = sr.Right - sr.Left + 1;
                        vz.CountLines = sr.Bottom - sr.Top + 1;
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
        public OutputHTML GenerateDesignDIV()
        {
            throw new NotImplementedException();
        }

        public OutputHTML GenerateDesignDIV(Page refPage)
        {
            DesignPage config = new DesignPage();
            config.constraintWidth = this.constraintWidth;
            config.constraintHeight = this.constraintHeight;
            config.width = this.width;
            config.height = this.height;
            config.cssPart = this.css;
            config.cssOnFile = this.cssOnFile;
            config.cssFile = this.cssFileName;
            config.javascriptPart = this.javascript;
            config.javascriptOnFile = this.javascriptOnFile;
            config.javascriptFile = this.javascriptFileName;
            config.javascriptFiles = this.javascriptFiles;
            config.onload = this.javascriptOnLoad;
            config.zones = this.hZones;
            config.includeContainer = true;
            config.subObjects = new List<HTMLObject>();
            config.subObjects.AddRange(this.objects);
            config.subObjects.AddRange(refPage.Objects);
            return Routines.GenerateDesignPageDIV(refPage, this, config);
        }

        public OutputHTML GenerateDesignDIV(Page refPage, List<MasterObject> objects, ParentConstraint parentConstraint)
        {
            throw new NotImplementedException();
        }

        public OutputHTML GenerateDesignDIV(Page refPage, MasterPage masterRefPage, ParentConstraint parentConstraint)
        {
            throw new NotImplementedException();
        }

        public OutputHTML GenerateDesignDIV(Page refPage, MasterPage masterRefPage, List<MasterObject> objects, ParentConstraint parentConstraint)
        {
            throw new NotImplementedException();
        }

        public OutputHTML GenerateDesignTable(Page refPage)
        {
            DesignPage config = new DesignPage();
            config.constraintWidth = this.constraintWidth;
            config.constraintHeight = this.constraintHeight;
            config.width = this.width;
            config.height = this.height;
            config.cssPart = this.css;
            config.cssOnFile = this.cssOnFile;
            config.cssFile = this.cssFileName;
            config.javascriptPart = this.javascript;
            config.javascriptOnFile = this.javascriptOnFile;
            config.javascriptFile = this.javascriptFileName;
            config.javascriptFiles = this.javascriptFiles;
            config.onload = this.javascriptOnLoad;
            config.zones = this.hZones;
            config.includeContainer = true;
            config.subObjects = new List<HTMLObject>();
            config.subObjects.AddRange(this.objects);
            config.subObjects.AddRange(refPage.Objects);
            return Routines.GenerateDesignPageTable(refPage, this, config);
        }

        public OutputHTML GenerateDesignTable(Page refPage, MasterPage masterRefPage, ParentConstraint parentConstraint)
        {
            throw new NotImplementedException();
        }

        public OutputHTML GenerateDesignTable(Page refPage, MasterPage masterRefPage, List<MasterObject> objects, ParentConstraint parentConstraint)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Génération de la master page sans la page (design)
        /// </summary>
        /// <returns>page html</returns>
        public OutputHTML GenerateDesign()
        {
            Page page = new Page();
            page.ConstraintWidth = EnumConstraint.RELATIVE;
            page.Width = 100;
            page.ConstraintHeight = EnumConstraint.RELATIVE;
            page.Height = 100;
            if (this.constraintWidth == EnumConstraint.RELATIVE || this.constraintHeight == EnumConstraint.RELATIVE)
            {
                OutputHTML html = this.GenerateDesignTable(page);
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
                    html = this.GenerateDesignTable(page);
                }
                else
                {
                    html = this.GenerateDesignDIV(page);
                }
                return html;
            }
        }

        /// <summary>
        /// Mode design de la page
        /// </summary>
        /// <param name="refPage">page à afficher</param>
        /// <returns>page html</returns>
        public OutputHTML GenerateDesign(Page refPage)
        {
            if (this.constraintWidth == EnumConstraint.RELATIVE || this.constraintHeight == EnumConstraint.RELATIVE)
            {
                OutputHTML html = this.GenerateDesignTable(refPage);
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
                    html = this.GenerateDesignTable(refPage);
                }
                else
                {
                    html = this.GenerateDesignDIV(refPage);
                }
                return html;
            }
        }

        public OutputHTML GenerateDesign(Page refPage, MasterPage masterRefPage, ParentConstraint parentConstraint)
        {
            throw new NotImplementedException();
        }

        public OutputHTML GenerateDesign(Page refPage, MasterPage masterRefPage, List<MasterObject> objects, ParentConstraint parentConstraint)
        {
            throw new NotImplementedException();
        }

        public OutputHTML GenerateDesign(Page refPage, List<MasterObject> objects, ParentConstraint parentConstraint)
        {
            throw new NotImplementedException();
        }

        public OutputHTML GenerateDesignDIV(Page refPage, MasterObject refMasterObject, ParentConstraint parentConstraint)
        {
            throw new NotImplementedException();
        }


        public OutputHTML GenerateDesignTable(Page refPage, List<MasterObject> objects, ParentConstraint parentConstraint)
        {
            throw new NotImplementedException();
        }


        public OutputHTML GenerateDesign(Page refPage, MasterObject refMasterObject, ParentConstraint parentConstraint)
        {
            throw new NotImplementedException();
        }

        public OutputHTML GenerateThumbnail()
        {
            Page page = new Page();
            page.ConstraintWidth = EnumConstraint.RELATIVE;
            page.Width = 100;
            page.ConstraintHeight = EnumConstraint.RELATIVE;
            page.Height = 100;
            if (this.constraintWidth == EnumConstraint.RELATIVE || this.constraintHeight == EnumConstraint.RELATIVE)
            {
                DesignPage config = new DesignPage();
                config.constraintWidth = this.constraintWidth;
                config.constraintHeight = this.constraintHeight;
                config.width = this.width;
                config.height = this.height;
                CodeCSS cssThumbnail = new CodeCSS(this.css);
                cssThumbnail.Body.Add("zoom", "0.4");
                config.cssPart = cssThumbnail;
                config.cssOnFile = false;
                config.cssFile = "";
                config.javascriptPart = this.javascript;
                config.javascriptOnFile = false;
                config.javascriptFile = "";
                config.javascriptFiles = this.javascriptFiles;
                config.onload = this.javascriptOnLoad;
                config.zones = this.hZones;
                config.includeContainer = false;
                return Routines.GenerateDesignPageTable(page, this, config);
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
                    DesignPage config = new DesignPage();
                    config.constraintWidth = this.constraintWidth;
                    config.constraintHeight = this.constraintHeight;
                    config.width = this.width;
                    config.height = this.height;
                    CodeCSS cssThumbnail = new CodeCSS(this.css);
                    cssThumbnail.Body.Add("zoom", "0.4");
                    config.cssPart = cssThumbnail;
                    config.cssOnFile = false;
                    config.cssFile = "";
                    config.javascriptPart = this.javascript;
                    config.javascriptOnFile = false;
                    config.javascriptFile = "";
                    config.javascriptFiles = this.javascriptFiles;
                    config.onload = this.javascriptOnLoad;
                    config.zones = this.hZones;
                    config.includeContainer = false;
                    html = Routines.GenerateDesignPageTable(page, this, config);
                }
                else
                {
                    DesignPage config = new DesignPage();
                    config.constraintWidth = this.constraintWidth;
                    config.constraintHeight = this.constraintHeight;
                    config.width = this.width;
                    config.height = this.height;
                    CodeCSS cssThumbnail = new CodeCSS(this.css);
                    cssThumbnail.Body.Add("zoom", "0.4");
                    config.cssPart = cssThumbnail;
                    config.cssOnFile = false;
                    config.cssFile = "";
                    config.javascriptPart = this.javascript;
                    config.javascriptOnFile = false;
                    config.javascriptFile = "";
                    config.javascriptFiles = this.javascriptFiles;
                    config.onload = this.javascriptOnLoad;
                    config.zones = this.hZones;
                    config.includeContainer = false;
                    html = Routines.GenerateDesignPageDIV(page, this, config);
                }
                return html;
            }
        }

        public OutputHTML GenerateProduction()
        {
            throw new NotImplementedException();
        }

        public OutputHTML GenerateProduction(Page refPage)
        {
            if (this.constraintWidth == EnumConstraint.RELATIVE || this.constraintHeight == EnumConstraint.RELATIVE)
            {
                OutputHTML html = this.GenerateProductionTable(refPage);
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
                    html = this.GenerateProductionTable(refPage);
                }
                else
                {
                    html = this.GenerateProductionDIV(refPage);
                }
                return html;
            }
        }

        public OutputHTML GenerateProduction(Page refPage, MasterPage masterRefPage, ParentConstraint parentConstraint)
        {
            throw new NotImplementedException();
        }

        public OutputHTML GenerateProduction(Page refPage, MasterPage masterRefPage, List<MasterObject> objects, ParentConstraint parentConstraint)
        {
            throw new NotImplementedException();
        }

        public OutputHTML GenerateProductionDIV(Page refPage)
        {
            DesignPage config = new DesignPage();
            config.constraintWidth = this.constraintWidth;
            config.constraintHeight = this.constraintHeight;
            config.width = this.width;
            config.height = this.height;
            config.cssPart = this.css;
            config.cssOnFile = this.cssOnFile;
            config.cssFile = this.cssFileName;
            config.javascriptPart = this.javascript;
            config.javascriptOnFile = this.javascriptOnFile;
            config.javascriptFile = this.javascriptFileName;
            config.javascriptFiles = this.javascriptFiles;
            config.includeContainer = true;
            config.subObjects = new List<HTMLObject>();
            config.subObjects.AddRange(this.objects);
            config.subObjects.AddRange(refPage.Objects);
            config.onload = this.javascriptOnLoad;
            config.zones = this.hZones;
            return Routines.GenerateProductionPageDIV(refPage, this, config);
        }

        public OutputHTML GenerateProductionDIV(Page refPage, MasterPage masterRefPage, ParentConstraint parentConstraint)
        {
            throw new NotImplementedException();
        }

        public OutputHTML GenerateProductionDIV(Page refPage, MasterPage masterRefPage, List<MasterObject> objects, ParentConstraint parentConstraint)
        {
            throw new NotImplementedException();
        }

        public OutputHTML GenerateProductionTable(Page refPage)
        {
            DesignPage config = new DesignPage();
            config.constraintWidth = this.constraintWidth;
            config.constraintHeight = this.constraintHeight;
            config.width = this.width;
            config.height = this.height;
            config.cssPart = this.css;
            config.cssOnFile = this.cssOnFile;
            config.cssFile = this.cssFileName;
            config.javascriptPart = this.javascript;
            config.javascriptOnFile = this.javascriptOnFile;
            config.javascriptFile = this.javascriptFileName;
            config.javascriptFiles = this.javascriptFiles;
            config.onload = this.javascriptOnLoad;
            config.zones = this.hZones;
            config.includeContainer = true;
            config.subObjects = new List<HTMLObject>();
            config.subObjects.AddRange(this.objects);
            config.subObjects.AddRange(refPage.Objects);
            return Routines.GenerateProductionPageTable(refPage, this, config);
        }

        public OutputHTML GenerateProductionTable(Page refPage, MasterPage masterRefPage, ParentConstraint parentConstraint)
        {
            throw new NotImplementedException();
        }

        public OutputHTML GenerateProductionTable(Page refPage, MasterPage masterRefPage, List<MasterObject> objects, ParentConstraint parentConstraint)
        {
            throw new NotImplementedException();
        }
        #endregion

        public string TypeName
        {
            get { return "MasterPage"; }
        }

        public string ElementTitle
        {
            get { return this.name; }
        }

        public object Clone()
        {
            return new MasterPage(this);
        }
    }
}
