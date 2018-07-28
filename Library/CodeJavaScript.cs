using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library
{
    /// <summary>
    /// Class container for JavaScript code
    /// </summary>
    [Serializable]
    public class CodeJavaScript : Marshalling.PersistentDataObject, ICloneable
    {

        #region Fields

        /// <summary>
        /// Index name to handle code data
        /// </summary>
        protected static readonly string codeName = "code";

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the code
        /// </summary>
        public string Code
        {
            get { return this.Get(codeName, ""); }
            set { this.Set(codeName, value); string result = this.GeneratedCode; }
        }

        /// <summary>
        /// Gets the generated code
        /// Transforms all configuration keys by these values
        /// </summary>
        public string GeneratedCode
        {
            get { return Project.CurrentProject.Configuration.Replace(this.Code); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Clone this object
        /// </summary>
        /// <returns>cloned object</returns>
        public object Clone()
        {
            CodeJavaScript cj = new CodeJavaScript();
            cj.Code = ExtensionMethods.CloneThis(this.Code);
            return cj;
        }

        #endregion

    }
}
