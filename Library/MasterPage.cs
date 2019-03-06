using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Library
{
    /// <summary>
    /// A master page is a template that acts as the first content generator
    /// all objects of master page and all specific objects for a single page are mixed
    /// object name are dissocied by a prefix name to correspond distinguished as page or master page behave
    /// </summary>
    [Serializable]
    public class MasterPage : Marshalling.PersistentDataObject, IContainer, IProjectElement, IGenerateDesign, IGenerateDesignDIV, IGenerateDesignTable, IGenerateProduction, IGenerateProductionDIV, IGenerateProductionTable, ICloneable
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
        /// Index name for css on file or css inline switch
        /// </summary>
        protected static readonly string cssOnFileName = "cssOnFile";
        /// <summary>
        /// Index name for css file name
        /// </summary>
        protected static readonly string cssFilenameName = "cssFilename";
        /// <summary>
        /// Index name for javascript on file or javascript inline switch
        /// </summary>
        protected static readonly string javascriptOnFileName = "javascriptOnFile";
        /// <summary>
        /// Index name for javascript file name
        /// </summary>
        protected static readonly string javascriptFilenameName = "javascriptFilename";
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
        /// Index name for counting columns
        /// </summary>
        protected static readonly string countingColumnsName = "countingColumns";
        /// <summary>
        /// Index name for name
        /// </summary>
        protected static readonly string nameName = "name";
        /// <summary>
        /// Index name for its own objects
        /// </summary>
        protected static readonly string objectListName = "objects";
        /// <summary>
        /// Index name for horizontal areas
        /// </summary>
        protected static readonly string horizontalZoneListName = "horizontalZones";
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
        /// Index name for meta keywords
        /// </summary>
        protected static readonly string metaName = "meta";
        /// <summary>
        /// Index name for css list
        /// </summary>
        protected static readonly string cssListName = "cssList";

        #endregion

        #region Default Constructor

        /// <summary>
        /// Empty constructor
        /// </summary>
        public MasterPage()
        {
            this.CSSList.AddCSS(new CodeCSS("body"));
        }

        #endregion

        #region Copy Constructor

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="refObj">master page to copy from</param>
        private MasterPage(MasterPage refObj)
        {
            this.ConstraintWidth = refObj.ConstraintWidth;
            this.ConstraintHeight = refObj.ConstraintHeight;
            this.IsCSSOnFile = refObj.IsCSSOnFile;
            this.CSSFileName = ExtensionMethods.CloneThis(refObj.CSSFileName);
            this.IsJavaScriptOnFile = refObj.IsJavaScriptOnFile;
            this.JavaScriptFileName = ExtensionMethods.CloneThis(refObj.JavaScriptFileName);
            this.Width = refObj.Width;
            this.Height = refObj.Height;
            this.CountLines = refObj.CountLines;
            this.CountColumns = refObj.CountColumns;
            this.Name = ExtensionMethods.CloneThis(refObj.Name);
            foreach (HTMLObject obj in refObj.Objects)
            {
                this.Objects.Add(obj.Clone() as HTMLObject);
            }
            foreach (HorizontalZone hz in refObj.HorizontalZones)
            {
                this.HorizontalZones.Add(hz.Clone() as HorizontalZone);
            }
            this.Set(eventsName, refObj.Events.Clone());
            this.Set(javascriptName, refObj.JavaScript.Clone());
            this.Set(javascriptOnloadName, refObj.JavaScriptOnLoad.Clone());
            this.Set(cssListName, refObj.CSSList.Clone());
            this.Meta = ExtensionMethods.CloneThis(refObj.Meta);

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
        /// Gets or sets if the css is exported to a file
        /// </summary>
        public bool IsCSSOnFile
        {
            get { return this.Get(cssOnFileName, false); }
            set { this.Set(cssOnFileName, value); }
        }

        /// <summary>
        /// Gets ot sets the file name where the css code resides
        /// </summary>
        public string CSSFileName
        {
            get { return this.Get(cssFilenameName, ""); }
            set { this.Set(cssFilenameName, value); }
        }

        /// <summary>
        /// Gets or sets if javascript is exported to a file
        /// </summary>
        public bool IsJavaScriptOnFile
        {
            get { return this.Get(javascriptOnFileName, false); }
            set { this.Set(javascriptOnFileName, value); }
        }

        /// <summary>
        /// Gets or sets the file name where the javascript code resides
        /// </summary>
        public string JavaScriptFileName
        {
            get { return this.Get(javascriptFilenameName, ""); }
            set { this.Set(javascriptFilenameName, value); }
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
        /// Gets the grid size for dumping mode
        /// </summary>
        public string GridSizeString
        {
            get { return this.CountColumns.ToString() + "x" + this.CountLines.ToString(); }
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
        /// Gets or sets the name of this master page
        /// </summary>
        public string Name
        {
            get { return this.Get(nameName, ""); }
            set { this.Set(nameName, value); }
        }

        /// <summary>
        /// Gets own objects
        /// </summary>
        public List<HTMLObject> Objects
        {
            get { return this.Get(objectListName, new List<HTMLObject>()); }
        }

        /// <summary>
        /// Gets horizontal areas
        /// </summary>
        public List<HorizontalZone> HorizontalZones
        {
            get { return this.Get(horizontalZoneListName, new List<HorizontalZone>()); }
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
        /// Gets the javascript source code
        /// </summary>
        public string JavaScriptSource
        {
            get { return this.JavaScript.Code; }
            set { this.JavaScript.Code = value; }
        }

        /// <summary>
        /// Gets the css code
        /// </summary>
        public CodeCSS CSS
        {
           get 
           {
               CodeCSS c = this.CSSList.List.Find(x => x.Ids == "body");
               if (c == null)
                   this.CSSList.AddCSS(new CodeCSS("body"));
               return this.CSSList.List.Find(x => x.Ids == "body");
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
        /// Gets or sets the meta keywords string
        /// </summary>
        public string Meta
        {
            get { return this.Get(metaName, ""); }
            set { this.Set(metaName, value); }
        }

        /// <summary>
        /// Gets the type name
        /// </summary>
        public string TypeName
        {
            get { return "MasterPage"; }
        }

        /// <summary>
        /// Gets the element title
        /// </summary>
        public string ElementTitle
        {
            get { return this.Name; }
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
            MasterPage.MakeZones(this.CountColumns, this.CountLines, list, this.HorizontalZones, this.Width, this.Height, this.ConstraintWidth, this.ConstraintHeight);
        }

        /// <summary>
        /// Construct all zones and compute total size
        /// </summary>
        /// <param name="c">column count</param>
        /// <param name="l">list count</param>
        /// <param name="list">list of rectangle the user supplied</param>
        /// <param name="hList">horizontal zones list</param>
        /// <param name="width">width</param>
        /// <param name="height">height</param>
        /// <param name="constraintWidth">constraint width</param>
        /// <param name="constraintHeight">constraint height</param>
        public static void MakeZones(uint c, uint l, List<AreaSizedRectangle> list, List<HorizontalZone> hList, uint width, uint height, EnumConstraint constraintWidth, EnumConstraint constraintHeight)
        {
            AreaSizedRectangle[,] indexes = new AreaSizedRectangle[c, l];
            for (int index = 0; index < list.Count; ++index)
            {
                AreaSizedRectangle current = list[index];
                indexes[current.StartWidth, current.StartHeight] = current;
            }

            double deltaWidth = width / (double)c;
            double deltaHeight = height / (double)l;
            // ranger les données dans la master page
            for (int pos_ligne = 0; pos_ligne < l; ++pos_ligne)
            {
                HorizontalZone hz;
                hz = new HorizontalZone();
                hz.ConstraintWidth = constraintWidth;
                hz.ConstraintHeight = EnumConstraint.AUTO;
                hz.Width = width;
                int maxCountLines;
                uint maxHeight;
                maxHeight = 0;
                maxCountLines = 0;
                for (int pos_colonne = 0; pos_colonne < c; ++pos_colonne)
                {
                    AreaSizedRectangle current = indexes[pos_colonne, pos_ligne];
                    if (current != null)
                    {
                        VerticalZone vz = new VerticalZone();
                        vz.CountLines = current.CountHeight;
                        vz.CountColumns = current.CountWidth;
                        vz.Width = Convert.ToUInt32(deltaWidth * current.CountWidth);
                        vz.Height = Convert.ToUInt32(deltaHeight * current.CountHeight);
                        vz.ConstraintWidth = constraintWidth;
                        vz.ConstraintHeight = constraintHeight;
                        hz.VerticalZones.Add(vz);
                        if (maxCountLines < vz.CountLines)
                            maxCountLines = vz.CountLines;
                        if (maxHeight < vz.Height)
                            maxHeight = vz.Height;
                    }
                }
                hz.CountLines = maxCountLines;
                hz.Height = maxHeight;
                hList.Add(hz);
            }
        }

        #endregion

        #region Interfaces implementations

        /// <summary>
        /// Generate an HTML DIV tag from null for design
        /// A master page is hosted in a page
        /// This function is not implemented nor called
        /// </summary>
        /// <returns>html output</returns>
        public OutputHTML GenerateDesignDIV()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Generate an HTML DIV tag from a page for design
        /// A master page is set for a page
        /// so, a master page is designed to generate a page
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
            config.cssList = this.CSSList;
            config.cssOnFile = this.IsCSSOnFile;
            config.cssFile = this.CSSFileName;
            config.events = this.Events;
            config.javascriptPart = this.JavaScript;
            config.javascriptOnFile = this.IsJavaScriptOnFile;
            config.javascriptFile = this.JavaScriptFileName;
            config.onload = this.JavaScriptOnLoad;
            config.zones = this.HorizontalZones;
            config.includeContainer = true;
            config.subObjects = new List<HTMLObject>();
            config.subObjects.AddRange(this.Objects);
            config.subObjects.AddRange(refPage.Objects);
            return Routines.GenerateDesignPageDIV(refPage, this, config);

        }

        /// <summary>
        /// Generate an HTML DIV tag for design
        /// The master page hosts the complete page, so this function is never implemented
        /// </summary>
        /// <param name="refPage">page reference</param>
        /// <param name="objects">master objects</param>
        /// <param name="parentConstraint">parent constraint</param>
        /// <returns>html output</returns>
        public OutputHTML GenerateDesignDIV(Page refPage, List<MasterObject> objects, ParentConstraint parentConstraint)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Generate an HTML DIV tag for design
        /// a given master page generates the page
        /// The master page hosts the complete page, so this function is never implemented
        /// </summary>
        /// <param name="refPage">page reference</param>
        /// <param name="masterRefPage">master page reference</param>
        /// <param name="parentConstraint">parent constraint</param>
        /// <returns>html output</returns>
        public OutputHTML GenerateDesignDIV(Page refPage, MasterPage masterRefPage, ParentConstraint parentConstraint)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Generate an HTML DIV tag for design
        /// a given master page generates the page
        /// restricted objects in page are computed equally
        /// The master page hosts the complete page, so this function is never implemented
        /// </summary>
        /// <param name="refPage">page reference</param>
        /// <param name="masterRefPage">master page reference</param>
        /// <param name="objects">master objects list</param>
        /// <param name="parentConstraint">parent constraint</param>
        /// <returns>html output</returns>
        public OutputHTML GenerateDesignDIV(Page refPage, MasterPage masterRefPage, List<MasterObject> objects, ParentConstraint parentConstraint)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Generate an HTML TABLE tag from a page for design
        /// A master page is set for a page
        /// so, a master page is designed to generate a page
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
            config.cssList = this.CSSList;
            config.cssOnFile = this.IsCSSOnFile;
            config.cssFile = this.CSSFileName;
            config.events = this.Events;
            config.javascriptPart = this.JavaScript;
            config.javascriptOnFile = this.IsJavaScriptOnFile;
            config.javascriptFile = this.JavaScriptFileName;
            config.onload = this.JavaScriptOnLoad;
            config.zones = this.HorizontalZones;
            config.includeContainer = true;
            config.subObjects = new List<HTMLObject>();
            config.subObjects.AddRange(this.Objects);
            config.subObjects.AddRange(refPage.Objects);
            return Routines.GenerateDesignPageTable(refPage, this, config);

        }

        /// <summary>
        /// Generate an HTML TABLE tag for design
        /// a given master page generates the page
        /// The master page hosts the complete page, so this function is never implemented
        /// </summary>
        /// <param name="refPage">page reference</param>
        /// <param name="masterRefPage">master page reference</param>
        /// <param name="parentConstraint">parent constraint</param>
        /// <returns>html output</returns>
        public OutputHTML GenerateDesignTable(Page refPage, MasterPage masterRefPage, ParentConstraint parentConstraint)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Generate an HTML TABLE tag for design
        /// a given master page generates the page
        /// a page contains master objects related
        /// restricted objects in page are computed equally
        /// The master page hosts the complete page, so this function is never implemented
        /// </summary>
        /// <param name="refPage">page reference</param>
        /// <param name="masterRefPage">master page reference</param>
        /// <param name="objects">master objects list</param>
        /// <param name="parentConstraint">parent constraint</param>
        /// <returns>html output</returns>
        public OutputHTML GenerateDesignTable(Page refPage, MasterPage masterRefPage, List<MasterObject> objects, ParentConstraint parentConstraint)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Génération de la master page sans la page (design)
        /// This is a special case for a master page
        /// A master page can be viewed at design mode.
        /// So, you create a fake page to handle a master page
        /// </summary>
        /// <returns>page html</returns>
        public OutputHTML GenerateDesign()
        {
            // Create a fake page
            Page page = new Page();
            if (this.ConstraintWidth == EnumConstraint.RELATIVE || this.ConstraintHeight == EnumConstraint.RELATIVE)
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
                foreach (HorizontalZone hz in this.HorizontalZones)
                {
                    bool sup = false;
                    foreach (VerticalZone vz in hz.VerticalZones)
                    {
                        if (vz.CountLines < hz.CountLines) sup = true;
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
            if (this.ConstraintWidth == EnumConstraint.RELATIVE || this.ConstraintHeight == EnumConstraint.RELATIVE)
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
                foreach (HorizontalZone hz in this.HorizontalZones)
                {
                    bool sup = false;
                    foreach (VerticalZone vz in hz.VerticalZones)
                    {
                        if (vz.CountLines < hz.CountLines) sup = true;
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

        /// <summary>
        /// Generate design of a page that you were supplied and its master page
        /// The master page object doesn't use any other master page, so this function is not implemented
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
        /// Generate design of a page that you were supplied and its master page
        /// The master page object doesn't use any other master page, so this function is not implemented
        /// restricted objects in page are computed equally
        /// </summary>
        /// <param name="refPage">page reference</param>
        /// <param name="masterRefPage">master page reference</param>
        /// <param name="objects">objects list</param>
        /// <param name="parentConstraint">parent constraint</param>
        /// <returns>html output</returns>
        public OutputHTML GenerateDesign(Page refPage, MasterPage masterRefPage, List<MasterObject> objects, ParentConstraint parentConstraint)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Generate design of a page that you were supplied
        /// restricted objects in page are computed equally
        /// This function is not implemented nor called
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
        /// Generate HTML DIV tag on design of a page that you were supplied and its master page
        /// The master page object doesn't use any other master page, so this function is not implemented
        /// </summary>
        /// <param name="refPage">page reference</param>
        /// <param name="refMasterObject">master object reference</param>
        /// <param name="parentConstraint">parent constraint</param>
        /// <returns>html output</returns>
        public OutputHTML GenerateDesignDIV(Page refPage, MasterObject refMasterObject, ParentConstraint parentConstraint)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Generate HTML TABLE tag on design of a page that you were supplied and its master page
        /// The master page object doesn't play with objects themselves, so this function is not implemented
        /// </summary>
        /// <param name="refPage">page reference</param>
        /// <param name="objects">master object list</param>
        /// <param name="parentConstraint">parent constraint</param>
        /// <returns>html output</returns>
        public OutputHTML GenerateDesignTable(Page refPage, List<MasterObject> objects, ParentConstraint parentConstraint)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Generate design of a page that you were supplied and its master page
        /// The master page object doesn't use any other master page, so this function is not implemented
        /// </summary>
        /// <param name="refPage">page reference</param>
        /// <param name="refMasterObject">master object reference</param>
        /// <param name="parentConstraint">parent constraint</param>
        /// <returns>html output</returns>
        public OutputHTML GenerateDesign(Page refPage, MasterObject refMasterObject, ParentConstraint parentConstraint)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// A master page has a thumbnail visual
        /// </summary>
        /// <returns>html output</returns>
        public OutputHTML GenerateThumbnail()
        {
            Page page = new Page();

            if (this.ConstraintWidth == EnumConstraint.RELATIVE || this.ConstraintHeight == EnumConstraint.RELATIVE)
            {
                DesignPage config = new DesignPage();
                config.constraintWidth = this.ConstraintWidth;
                config.constraintHeight = this.ConstraintHeight;
                config.width = this.Width;
                config.height = this.Height;
                CodeCSS cssThumbnail = new CodeCSS(this.CSS);
                cssThumbnail.Body.Add("zoom", "0.4");
                config.cssList = ExtensionMethods.CloneThis(this.CSSList);
                config.cssOnFile = false;
                config.cssFile = "";
                config.events = this.Events;
                config.javascriptPart = this.JavaScript;
                config.javascriptOnFile = false;
                config.javascriptFile = "";
                config.onload = this.JavaScriptOnLoad;
                config.zones = this.HorizontalZones;
                config.includeContainer = false;
                return Routines.GenerateDesignPageTable(page, this, config);
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
                        if (vz.CountLines < hz.CountLines) sup = true;
                    }
                    if (sup)
                    {
                        cannotUseDiv = true;
                        break;
                    }
                }
                OutputHTML html;
                DesignPage config = new DesignPage();
                config.constraintWidth = this.ConstraintWidth;
                config.constraintHeight = this.ConstraintHeight;
                config.width = this.Width;
                config.height = this.Height;
                CodeCSS cssThumbnail = new CodeCSS(this.CSS);
                cssThumbnail.Body.Add("zoom", "0.4");
                this.CSSList.AddCSS(cssThumbnail);
                config.cssList = this.CSSList;
                config.cssOnFile = false;
                config.cssFile = "";
                config.events = this.Events;
                config.javascriptPart = this.JavaScript;
                config.javascriptOnFile = false;
                config.javascriptFile = "";
                config.onload = this.JavaScriptOnLoad;
                config.zones = this.HorizontalZones;
                config.includeContainer = false;
                if (cannotUseDiv)
                {
                    html = Routines.GenerateDesignPageTable(page, this, config);
                }
                else
                {
                    html = Routines.GenerateDesignPageDIV(page, this, config);
                }
                return html;
            }
        }

        /// <summary>
        /// Génération de la master page sans la page on actual website
        /// A master page can be viewed herself on actual website.
        /// So, the function is not implemented nor called
        /// </summary>
        /// <returns>page html</returns>
        public OutputHTML GenerateProduction()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Mode production de la page
        /// </summary>
        /// <param name="refPage">page à afficher</param>
        /// <returns>page html</returns>
        public OutputHTML GenerateProduction(Page refPage)
        {
            if (this.ConstraintWidth == EnumConstraint.RELATIVE || this.ConstraintHeight == EnumConstraint.RELATIVE)
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
                foreach (HorizontalZone hz in this.HorizontalZones)
                {
                    bool sup = false;
                    foreach (VerticalZone vz in hz.VerticalZones)
                    {
                        if (vz.CountLines < hz.CountLines) sup = true;
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

        /// <summary>
        /// Generate actual website of a page that you were supplied and its master page
        /// The master page object doesn't use any other master page, so this function is not implemented
        /// </summary>
        /// <param name="refPage">page reference</param>
        /// <param name="masterRefPage">master page reference</param>
        /// <param name="parentConstraint">parent constraint</param>
        /// <returns>html output</returns>
        public OutputHTML GenerateProduction(Page refPage, MasterPage masterRefPage, ParentConstraint parentConstraint)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Generate actual website of a page that you were supplied and its master page
        /// The master page object doesn't use any other master page, so this function is not implemented
        /// restricted objects in page are computed equally
        /// </summary>
        /// <param name="refPage">page reference</param>
        /// <param name="masterRefPage">master page reference</param>
        /// <param name="objects">objects list</param>
        /// <param name="parentConstraint">parent constraint</param>
        /// <returns>html output</returns>
        public OutputHTML GenerateProduction(Page refPage, MasterPage masterRefPage, List<MasterObject> objects, ParentConstraint parentConstraint)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Generate an HTML DIV tag from a page for actual website
        /// A master page is set for a page
        /// so, a master page is designed to generate a page
        /// </summary>
        /// <param name="refPage">page reference</param>
        /// <returns>html output</returns>
        public OutputHTML GenerateProductionDIV(Page refPage)
        {

            DesignPage config = new DesignPage();
            config.constraintWidth = this.ConstraintWidth;
            config.constraintHeight = this.ConstraintHeight;
            config.width = this.Width;
            config.height = this.Height;
            config.cssList = this.CSSList;
            config.cssOnFile = this.IsCSSOnFile;
            config.cssFile = this.CSSFileName;
            config.events = this.Events;
            config.javascriptPart = this.JavaScript;
            config.javascriptOnFile = this.IsJavaScriptOnFile;
            config.javascriptFile = this.JavaScriptFileName;
            config.onload = this.JavaScriptOnLoad;
            config.zones = this.HorizontalZones;
            config.includeContainer = true;
            config.subObjects = new List<HTMLObject>();
            config.subObjects.AddRange(this.Objects);
            config.subObjects.AddRange(refPage.Objects);
            return Routines.GenerateProductionPageDIV(refPage, this, config);

        }

        /// <summary>
        /// Generate an HTML DIV tag for actual website
        /// a given master page generates the page
        /// The master page hosts the complete page, so this function is never implemented
        /// </summary>
        /// <param name="refPage">page reference</param>
        /// <param name="masterRefPage">master page reference</param>
        /// <param name="parentConstraint">parent constraint</param>
        /// <returns>html output</returns>
        public OutputHTML GenerateProductionDIV(Page refPage, MasterPage masterRefPage, ParentConstraint parentConstraint)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Generate an HTML DIV tag for actual website
        /// a given master page generates the page
        /// restricted objects in page are computed equally
        /// The master page hosts the complete page, so this function is never implemented
        /// </summary>
        /// <param name="refPage">page reference</param>
        /// <param name="masterRefPage">master page reference</param>
        /// <param name="objects">master objects list</param>
        /// <param name="parentConstraint">parent constraint</param>
        /// <returns>html output</returns>
        public OutputHTML GenerateProductionDIV(Page refPage, MasterPage masterRefPage, List<MasterObject> objects, ParentConstraint parentConstraint)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Generate an HTML TABLE tag from a page for actual website
        /// A master page is set for a page
        /// so, a master page is designed to generate a page
        /// </summary>
        /// <param name="refPage">page reference</param>
        /// <returns>html output</returns>
        public OutputHTML GenerateProductionTable(Page refPage)
        {

            DesignPage config = new DesignPage();
            config.constraintWidth = this.ConstraintWidth;
            config.constraintHeight = this.ConstraintHeight;
            config.width = this.Width;
            config.height = this.Height;
            config.cssList = this.CSSList;
            config.cssOnFile = this.IsCSSOnFile;
            config.cssFile = this.CSSFileName;
            config.events = this.Events;
            config.javascriptPart = this.JavaScript;
            config.javascriptOnFile = this.IsJavaScriptOnFile;
            config.javascriptFile = this.JavaScriptFileName;
            config.onload = this.JavaScriptOnLoad;
            config.zones = this.HorizontalZones;
            config.includeContainer = true;
            config.subObjects = new List<HTMLObject>();
            config.subObjects.AddRange(this.Objects);
            config.subObjects.AddRange(refPage.Objects);
            return Routines.GenerateProductionPageTable(refPage, this, config);

        }

        /// <summary>
        /// Generate an HTML TABLE tag for actual website
        /// a given master page generates the page
        /// The master page hosts the complete page, so this function is never implemented
        /// </summary>
        /// <param name="refPage">page reference</param>
        /// <param name="masterRefPage">master page reference</param>
        /// <param name="parentConstraint">parent constraint</param>
        /// <returns>html output</returns>
        public OutputHTML GenerateProductionTable(Page refPage, MasterPage masterRefPage, ParentConstraint parentConstraint)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Generate an HTML TABLE tag for actual website
        /// a given master page generates the page
        /// a page contains master objects related
        /// restricted objects in page are computed equally
        /// The master page hosts the complete page, so this function is never implemented
        /// </summary>
        /// <param name="refPage">page reference</param>
        /// <param name="masterRefPage">master page reference</param>
        /// <param name="objects">master objects list</param>
        /// <param name="parentConstraint">parent constraint</param>
        /// <returns>html output</returns>
        public OutputHTML GenerateProductionTable(Page refPage, MasterPage masterRefPage, List<MasterObject> objects, ParentConstraint parentConstraint)
        {
            throw new NotImplementedException();
        }

        #endregion

        /// <summary>
        /// Clone this object
        /// </summary>
        /// <returns>cloned object</returns>
        public object Clone()
        {
            return new MasterPage(this);
        }
    }
}
