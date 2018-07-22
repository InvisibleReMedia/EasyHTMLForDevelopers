using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    /// <summary>
    /// Defines an existing file into a project
    /// the source file resides into the project's build directory
    /// </summary>
    public class File : Marshalling.PersistentDataObject
    {

        #region Fields

        /// <summary>
        /// Index name for the unique id
        /// </summary>
        protected static readonly string uniqueName = "unique";
        /// <summary>
        /// Index name for the file name
        /// </summary>
        protected static readonly string fileNameName = "fileName";

        #endregion

        #region Constructors

        /// <summary>
        /// Empty constructor
        /// </summary>
        public File()
        {

        }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="f">file name</param>
        public File(string f)
        {
            this.Set(fileNameName, Path.GetFileName(f));
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the unique id
        /// </summary>
        public string Unique
        {
            get { return this.Get(uniqueName); }
            set { this.Set(uniqueName, value); }
        }

        /// <summary>
        /// Gets the file name
        /// </summary>
        public string FileName
        {
            get { return this.Get(fileNameName, ""); }
        }

        #endregion

    }
}
