using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    /// <summary>
    /// Cadre model type form to create a sculpture
    /// and generate a complete or partial form of an html page, master page, master object or a tool
    /// </summary>
    [Serializable]
    public class CadreModelType : Marshalling.PersistentDataObject, ICloneable, INotifyPropertyChanged
    {

        #region Public Static Constants

        /// <summary>
        /// Image
        /// </summary>
        public static readonly string Image = Localization.Strings.GetString("IMAGE");
        /// <summary>
        /// Texte
        /// </summary>
        public static readonly string Text = Localization.Strings.GetString("TEXTE");
        /// <summary>
        /// Tool
        /// </summary>
        public static readonly string Tool = Localization.Strings.GetString("OUTIL");
        /// <summary>
        /// Master object
        /// </summary>
        public static readonly string MasterObject = Localization.Strings.GetString("MASTER_OBJET");
        /// <summary>
        /// Dynamic
        /// </summary>
        public static readonly string DynamicObject = Localization.Strings.GetString("DYNAMIC");

        #endregion

        #region Fields

        /// <summary>
        /// Event
        /// </summary>
        [NonSerialized]
        private PropertyChangedEventHandler propertyChanged;

        /// <summary>
        /// Index Name for type
        /// </summary>
        protected static readonly string typeName = "type";
        /// <summary>
        /// Index Name for content
        /// </summary>
        protected static readonly string contentName = "content";
        /// <summary>
        /// Index Name for content object
        /// </summary>
        protected static readonly string contentObjectName = "contentObject";

        #endregion

        #region Public Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="type">type name</param>
        public CadreModelType(string type)
        {
            this.Set(typeName, type);
            this.Set(contentName, "");
            this.Set(contentObjectName, null);
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="type">type name</param>
        /// <param name="content">content string</param>
        /// <param name="obj">object as content</param>
        public CadreModelType(string type, string content, dynamic obj)
        {
            this.Set(typeName, type);
            this.Set(contentName, content);
            this.Set(contentObjectName, obj);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the type name
        /// </summary>
        public string Type
        {
            get { return this.Get(typeName); }
        }

        /// <summary>
        /// Gets or sets the content string
        /// </summary>
        public string Content
        {
            get { return this.Get(contentName, string.Empty); }
            set { this.Set(contentName, value); this.UpdateProperty("Content"); }
        }

        /// <summary>
        /// Gets or sets object as content
        /// </summary>
        public dynamic DirectObject
        {
            get { return this.Get(contentObjectName, null); }
            set { this.Set(contentObjectName, value); }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Update property
        /// </summary>
        /// <param name="name">property name</param>
        private void UpdateProperty(string name)
        {
            if (this.propertyChanged != null)
                this.propertyChanged(this, new PropertyChangedEventArgs(name));
        }

        #endregion

        #region Public Events

        /// <summary>
        /// Add or remove event Position Changed
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged
        {
            add { this.propertyChanged += new PropertyChangedEventHandler(value); }
            remove { this.propertyChanged -= new PropertyChangedEventHandler(value); }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Reinitialization
        /// </summary>
        public void Reinit()
        {
            this.propertyChanged = null;
        }

        #endregion

        #region ICloneable Members

        /// <summary>
        /// Clone this object
        /// </summary>
        /// <returns>cloned object</returns>
        public object Clone()
        {
            return new CadreModelType(this.Type, this.Content, this.DirectObject);
        }

        #endregion

    }
}
