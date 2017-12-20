using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    [Serializable]
    public class RefObject : ICloneable
    {
        #region Public Static Constants
        public static readonly string Page = "page";
        public static readonly string MasterPage = "masterPage";
        public static readonly string MasterObject = "masterObject";
        public static readonly string ToolInstance = "toolInstance";
        #endregion

        #region Private Fields
        private string objectType;
        private string title;
        private HTMLObject directObject;
        #endregion

        #region Public Constructor
        public RefObject(string objectType, string instanceName, HTMLObject source)
        {
            this.objectType = objectType;
            this.title = instanceName;
            this.directObject = source;
        }
        #endregion

        #region Copy Constructor
        private RefObject(RefObject r)
        {
            this.objectType = ExtensionMethods.CloneThis(r.objectType);
            this.title = ExtensionMethods.CloneThis(r.title);
            this.directObject = r.directObject;
        }
        #endregion

        #region Public Properties
        public string Type
        {
            get { return this.objectType; }
        }

        public string Title
        {
            get { return this.title; }
        }

        public HTMLObject DirectObject
        {
            get { return this.directObject; }
        }
        #endregion

        public object Clone()
        {
            return new RefObject(this);
        }
    }
}
