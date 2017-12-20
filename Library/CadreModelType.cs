using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    [Serializable]
    public class CadreModelType : ICloneable, INotifyPropertyChanged
    {
        #region Public Static Constants
        public static readonly string Image = Localization.Strings.GetString("IMAGE");
        public static readonly string Text = Localization.Strings.GetString("TEXTE");
        public static readonly string Tool = Localization.Strings.GetString("OUTIL");
        public static readonly string MasterObject = Localization.Strings.GetString("MASTER_OBJET");
        public static readonly string DynamicObject = Localization.Strings.GetString("DYNAMIC");
        #endregion

        #region Private Fields
        [NonSerialized]
        private PropertyChangedEventHandler propertyChanged;
        private string type;
        private string content;
        private dynamic contentObject;
        #endregion

        #region Public Constructor
        public CadreModelType(string type)
        {
            this.type = type;
            this.content = String.Empty;
            this.contentObject = null;
        }

        public CadreModelType(string type, string content, dynamic obj)
        {
            this.type = type;
            this.content = content;
            this.contentObject = obj;
        }
        #endregion

        #region Public Properties
        public string Type
        {
            get { return this.type; }
        }

        public string Content
        {
            get { return this.content; }
            set { this.content = value; this.UpdateProperty("Content"); }
        }

        public dynamic DirectObject
        {
            get { return this.contentObject; }
            set { this.contentObject = value; }
        }
        #endregion

        #region Private Methods
        private void UpdateProperty(string name)
        {
            if (this.propertyChanged != null)
                this.propertyChanged(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        #region Public Events
        public event PropertyChangedEventHandler PropertyChanged
        {
            add { this.propertyChanged += new PropertyChangedEventHandler(value); }
            remove { this.propertyChanged -= new PropertyChangedEventHandler(value); }
        }
        #endregion

        #region Public Methods
        public void Reinit()
        {
            this.propertyChanged = null;
        }
        #endregion

        #region ICloneable Members
        public object Clone()
        {
            return new CadreModelType(this.type, this.content, this.contentObject);
        }
        #endregion

    }
}
