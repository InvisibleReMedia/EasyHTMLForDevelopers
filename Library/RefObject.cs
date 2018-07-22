using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    /// <summary>
    /// A reference object
    /// </summary>
    [Serializable]
    public class RefObject : Marshalling.PersistentDataObject, ICloneable
    {

        #region Public Static Constants

        /// <summary>
        /// Page name
        /// </summary>
        public static readonly string Page = "page";
        /// <summary>
        /// Master page name
        /// </summary>
        public static readonly string MasterPage = "masterPage";
        /// <summary>
        /// Master object name
        /// </summary>
        public static readonly string MasterObject = "masterObject";
        /// <summary>
        /// Tool
        /// </summary>
        public static readonly string Tool = "tool";
        /// <summary>
        /// Instances
        /// </summary>
        public static readonly string Instance = "instance";
        /// <summary>
        /// Files
        /// </summary>
        public static readonly string File = "file";

        #endregion

        #region Protected Fields

        /// <summary>
        /// Index name for object type
        /// </summary>
        protected static readonly string objectTypeName = "objectType";
        /// <summary>
        /// Index name for title
        /// </summary>
        protected static readonly string titleName = "title";
        /// <summary>
        /// Index name for direct object
        /// </summary>
        protected static readonly string directObjectName = "directObject";

        #endregion

        #region Public Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="objectType">an object type name</param>
        /// <param name="instanceName">an instance name</param>
        /// <param name="source">a source object</param>
        public RefObject(string objectType, string instanceName, HTMLObject source)
        {
            this.Set(objectTypeName, objectType);
            this.Set(titleName, instanceName);
            this.Set(directObjectName, source);
        }

        #endregion

        #region Copy Constructor

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="r">ref object to copy</param>
        private RefObject(RefObject r)
        {
            this.Set(objectTypeName, ExtensionMethods.CloneThis(r.Type));
            this.Set(titleName, ExtensionMethods.CloneThis(r.Title));
            // this element is not cloned, use a reference
            this.Set(directObjectName, r.DirectObject);
        }
        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the object type
        /// </summary>
        public string Type
        {
            get { return this.Get(objectTypeName); }
        }

        /// <summary>
        /// Gets the title
        /// </summary>
        public string Title
        {
            get { return this.Get(titleName); }
        }

        /// <summary>
        /// Gets the direct object (uniquely existing)
        /// </summary>
        public HTMLObject DirectObject
        {
            get { return this.Get(directObjectName); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Clone this object
        /// </summary>
        /// <returns>cloned object</returns>
        public object Clone()
        {
            return new RefObject(this);
        }

        #endregion
    }
}
