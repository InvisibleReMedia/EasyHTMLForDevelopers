using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace UXFramework
{
    /// <summary>
    /// Draw an image
    /// </summary>
    public class UXImage : UXControl
    {

        #region Fields

        /// <summary>
        /// File of image
        /// </summary>
        private string imageFile;
        /// <summary>
        /// Id
        /// </summary>
        private string id;
        /// <summary>
        /// Size of this image
        /// </summary>
        private Size size;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="fileName">image file</param>
        public UXImage(string id, string fileName)
        {
            this.id = id;
            this.imageFile = fileName;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the image file
        /// </summary>
        public string ImageFile
        {
            get { return this.imageFile; }
            set { this.imageFile = value; }
        }

        /// <summary>
        /// Gets or sets the Id object
        /// </summary>
        public string Id
        {
            get { return this.id; }
            set { this.id = value; }
        }

        /// <summary>
        /// Gets or sets the size of this image
        /// </summary>
        public Size Size
        {
            get { return this.size; }
            set { this.size = value; }
        }

        #endregion

    }
}
