using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Library
{
    /// <summary>
    /// Attributes (id, class, CSS)
    /// </summary>
    [Serializable]
    public class Attributes : Marshalling.PersistentDataObject, ICloneable
    {

        #region Fields

        /// <summary>
        /// Index for id
        /// </summary>
        private static readonly string idName = "id";
        /// <summary>
        /// Index if has id
        /// </summary>
        private static readonly string hasIdName = "hasId";
        /// <summary>
        /// Index for CSS class
        /// </summary>
        private static readonly string className = "class";
        /// <summary>
        /// Index if has css class
        /// </summary>
        private static readonly string hasClassName = "hasClass";
        /// <summary>
        /// Index when id is automatic
        /// </summary>
        private static readonly string automaticIdName = "autoId";
        /// <summary>
        /// Index when class is automatic
        /// </summary>
        private static readonly string automaticClassName = "autoClass";
        /// <summary>
        /// Index when using class or id
        /// </summary>
        private static readonly string useClassCSSName = "useClass";
        /// <summary>
        /// Index for unique class name
        /// </summary>
        private static readonly string uniqueClassName = "uniqueClass";
        /// <summary>
        /// Index for unique id name
        /// </summary>
        private static readonly string uniqueIdName = "uniqueId";
        /// <summary>
        /// Unique id
        /// </summary>
        public static UniqueStrings uniqueId = new UniqueStrings();
        /// <summary>
        /// Unique class
        /// </summary>
        public static UniqueStrings uniqueClass = new UniqueStrings();

        #endregion

        #region Constructor

        /// <summary>
        /// Specific constructor
        /// </summary>
        /// <param name="inputId">input id</param>
        public Attributes(string inputId)
        {
            this.HasId = true;
            this.HasClass = false;
            this.IsAutomaticId = false;
            this.IsAutomaticClass = true;
            this.Id = inputId;
            this.IsUsingClassForCSS = false;
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public Attributes()
        {

        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the id
        /// </summary>
        public string Id
        {
            get { return this.Get(idName, string.Empty); }
            set
            {
                this.Set(idName, value);
            }
        }

        /// <summary>
        /// Gets or sets if has id
        /// </summary>
        public bool HasId
        {
            get { return this.Get(hasIdName, false); }
            set { this.Set(hasIdName, value); }
        }

        /// <summary>
        /// Gets or sets the class id
        /// </summary>
        public string Class
        {
            get { return this.Get(className, string.Empty); }
            set
            {
                this.Set(className, value);
            }
        }

        /// <summary>
        /// Gets or sets if has class
        /// </summary>
        public bool HasClass
        {
            get { return this.Get(hasClassName, false); }
            set { this.Set(hasClassName, value); }
        }

        /// <summary>
        /// Gets or sets when id name is automatic
        /// id is manual or automatic
        /// </summary>
        public bool IsAutomaticId
        {
            get { return this.Get(automaticIdName, true); }
            set { this.Set(automaticIdName, value); }
        }

        /// <summary>
        /// Gets or sets when class name is automatic
        /// class is manual or automatic
        /// </summary>
        public bool IsAutomaticClass
        {
            get { return this.Get(automaticClassName, true); }
            set { this.Set(automaticClassName, value); }
        }

        /// <summary>
        /// Gets or sets if using class or id in css
        /// </summary>
        public bool IsUsingClassForCSS
        {
            get { return this.Get(useClassCSSName, true); }
            set
            {
                this.Set(useClassCSSName, value);
            }
        }

        /// <summary>
        /// Gets the automatic id
        /// </summary>
        public string AutomaticId
        {
            get { return this.Get(uniqueIdName, this.UniqueId.ComputeNewString()); }
        }

        /// <summary>
        /// Gets the automatic class
        /// </summary>
        public string AutomaticClass
        {
            get { return this.Get(uniqueClassName, this.UniqueClass.ComputeNewString()); }
        }

        /// <summary>
        /// Gets the unique id
        /// </summary>
        private UniqueStrings UniqueId
        {
            get { return uniqueId; }
        }

        /// <summary>
        /// Gets the unique class
        /// </summary>
        private UniqueStrings UniqueClass
        {
            get { return uniqueClass; }
        }

        /// <summary>
        /// Gets if using CSS
        /// </summary>
        public bool IsUsingCSS
        {
            get
            {
                if (this.IsUsingClassForCSS)
                {
                    return this.HasClass;
                }
                else
                    return this.HasId;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Find css
        /// </summary>
        /// <param name="list">list of css</param>
        /// <returns>css</returns>
        public CodeCSS Find(CSSList list)
        {
            if (this.IsUsingClassForCSS)
            {
                if (this.HasClass)
                {
                    return list.List.Find(x => x.Ids == "." + this.Class);
                }
                else
                    throw new KeyNotFoundException();
            }
            else
            {
                if (this.HasId)
                {
                    return list.List.Find(x => x.Ids == "#" + this.Id);
                }
                else
                    throw new KeyNotFoundException();
            }
        }

        /// <summary>
        /// Rename id
        /// </summary>
        /// <param name="id"></param>
        public void RenameId(string id)
        {
            this.HasId = true;
            this.IsAutomaticId = false;
            this.Id = id;
        }

        /// <summary>
        /// Gets or sets values
        /// </summary>
        public void SetValues()
        {
            if (this.HasId)
            {
                if (this.IsAutomaticId)
                {
                    this.Id = this.AutomaticId;
                }
            }
            if (this.HasClass)
            {
                if (this.IsAutomaticClass)
                {
                    this.Class = this.AutomaticClass;
                }
            }
        }

        /// <summary>
        /// Print the html tag
        /// </summary>
        /// <param name="tag">tag</param>
        /// <param name="name">name</param>
        /// <param name="css">css</param>
        /// <param name="htmlEvents">events</param>
        /// <param name="cssPart">css</param>
        /// <param name="output">result</param>
        /// <returns>true if success</returns>
        public bool ToHTML(string tag, string name, CodeCSS css, Events htmlEvents, StringBuilder cssPart, out string output)
        {
            this.SetValues();
            output = "<";
            output += tag;
            if (!String.IsNullOrEmpty(name))
            {
                output += " name=\"" + name + "\"";
            }
            if (this.HasId)
            {
                output += " id=\"" + this.Id + "\"";
            }
            if (this.HasClass)
            {
                output += " class=\"" + this.Class + "\"";
            }
            if (!this.HasId && !this.HasClass)
            {
                string cssCode = css.GenerateCSS(true, false, true);
                if (!String.IsNullOrEmpty(cssCode))
                {
                    output += " style=\"" + cssCode.Replace(Environment.NewLine, "") + "\"";
                }
            }
            else
            {
                if (this.IsUsingClassForCSS)
                {
                    css.Ids = "." + this.Class;
                    cssPart.Append(css.GenerateCSS(true, true, true));
                }
                else
                {
                    css.Ids = "#" + this.Id;
                    cssPart.Append(css.GenerateCSS(true, true, true));
                }
            }
            if (htmlEvents.Count > 0)
                output += " " + htmlEvents.ToHTMLString();
            output += ">";
            return true;
        }


        /// <summary>
        /// Print the html tag
        /// </summary>
        /// <param name="tag">tag</param>
        /// <param name="onclick">onclick event</param>
        /// <param name="name">name</param>
        /// <param name="title">title</param>
        /// <param name="css">css</param>
        /// <param name="htmlEvents">events</param>
        /// <param name="cssPart">css</param>
        /// <param name="output">output</param>
        /// <returns>true if success</returns>
        public bool ToHTML(string tag, string onclick, string name, string title, CodeCSS css, Events htmlEvents, StringBuilder cssPart, out string output)
        {
            this.SetValues();
            output = "<";
            output += tag;
            if (!String.IsNullOrEmpty(onclick))
                output += " " + onclick;
            if (!String.IsNullOrEmpty(name))
            {
                output += " name=\"" + name + "\"";
            }
            output += " " + "title=\"" + title + "\"";
            if (this.HasId)
            {
                output += " id=\"" + this.Id + "\"";
            }
            if (this.HasClass)
            {
                output += " class=\"" + this.Class + "\"";
            }
            if (!this.HasId && !this.HasClass)
            {
                string cssCode = css.GenerateCSS(true, false, true);
                if (!String.IsNullOrEmpty(cssCode))
                {
                    output += " style=\"" + cssCode.Replace(Environment.NewLine, "") + "\"";
                }
            }
            else
            {
                if (this.IsUsingClassForCSS)
                {
                    css.Ids = "." + this.Class;
                    cssPart.Append(css.GenerateCSS(true, true, true));
                }
                else
                {
                    css.Ids = "#" + this.Id;
                    cssPart.Append(css.GenerateCSS(true, true, true));
                }
            }
            if (htmlEvents.Count > 0)
                output += " " + htmlEvents.ToHTMLString();
            output += ">";
            return true;
        }


        /// <summary>
        /// Print the html tag
        /// </summary>
        /// <param name="tag">tag</param>
        /// <param name="onclick">onclick event</param>
        /// <param name="disposition">disposition</param>
        /// <param name="name">name</param>
        /// <param name="title">title</param>
        /// <param name="rowspan">rowspan</param>
        /// <param name="colspan">colspan</param>
        /// <param name="css">css</param>
        /// <param name="htmlEvents">events</param>
        /// <param name="cssPart">css</param>
        /// <param name="output">output</param>
        /// <returns>true if success</returns>
        public bool ToHTML(string tag, string onclick, string disposition, string name, string title, int rowspan, int colspan,
                           CodeCSS css, Events htmlEvents, StringBuilder cssPart, out string output)
        {
            this.SetValues();
            output = "<";
            output += tag;
            if (!String.IsNullOrEmpty(onclick))
                output += " " + onclick;
            if (!String.IsNullOrEmpty(disposition))
                output += " " + disposition;
            if (!String.IsNullOrEmpty(name))
            {
                output += " name=\"" + name + "\"";
            }
            output += " " + "title=\"" + title + "\"";
            if (this.HasId)
            {
                output += " id=\"" + this.Id + "\"";
            }
            if (this.HasClass)
            {
                output += " class=\"" + this.Class + "\"";
            }
            if (rowspan > 1 || rowspan == 0)
                output += " rowspan=\"" + rowspan.ToString() + "\"";
            if (colspan > 1 || colspan == 0)
                output += " colspan=\"" + colspan.ToString() + "\"";
            if (!this.HasId && !this.HasClass)
            {
                string cssCode = css.GenerateCSS(true, false, true);
                if (!String.IsNullOrEmpty(cssCode))
                {
                    output += " style=\"" + cssCode.Replace(Environment.NewLine, "") + "\"";
                }
            }
            else
            {
                if (this.IsUsingClassForCSS)
                {
                    css.Ids = "." + this.Class;
                    cssPart.Append(css.GenerateCSS(true, true, true));
                }
                else
                {
                    css.Ids = "#" + this.Id;
                    cssPart.Append(css.GenerateCSS(true, true, true));
                }
            }
            if (htmlEvents.Count > 0)
                output += " " + htmlEvents.ToHTMLString();
            output += ">";
            return true;
        }

        /// <summary>
        /// Clone this object
        /// </summary>
        /// <returns>copied object</returns>
        public object Clone()
        {
            Attributes a = new Attributes();
            a.HasId = this.HasId;
            a.HasClass = this.HasClass;
            a.IsAutomaticClass = this.IsAutomaticClass;
            a.IsAutomaticId = this.IsAutomaticId;
            a.IsUsingClassForCSS = this.IsUsingClassForCSS;
            if (this.IsAutomaticId) { a.Id = a.AutomaticId; } else { a.Id = this.Id; }
            if (this.IsAutomaticClass) { a.Class = a.AutomaticClass; } else { a.Class = this.Class; }
            return a;
        }
        #endregion

    }
}
