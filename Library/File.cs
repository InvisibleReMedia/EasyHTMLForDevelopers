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
    [Serializable]
    public class File : Marshalling.PersistentDataObject, IProjectElement
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
        /// <summary>
        /// Index name for the folder
        /// </summary>
        protected static readonly string folderName = "folder";

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
            this.Set(folderName, Path.GetDirectoryName(f));
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

        /// <summary>
        /// Gets the folder
        /// </summary>
        public string Folder
        {
            get { return this.Get(folderName, ""); }
            set { this.Set(folderName, value); }
        }

        /// <summary>
        /// Gets the type name
        /// </summary>
        public string TypeName
        {
            get { return "File"; }
        }

        /// <summary>
        /// Gets the element title
        /// </summary>
        public string ElementTitle
        {
            get { return this.FileName; }
        }

        #endregion

    }
}
