using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UXFramework
{
    /// <summary>
    /// Image clickable
    /// </summary>
    public class UXClickableImage : UXControl
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
        /// When roll over
        /// </summary>
        private string rollImageFile;
        /// <summary>
        /// When click
        /// </summary>
        private string clickImageFile;
        /// <summary>
        /// Colors for buttons
        /// </summary>
        private string rollBackColor;
        private string rollColor;
        private string clickBorderColor;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="fileName">image file</param>
        public UXClickableImage(string id, string fileName)
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
        /// Gets or sets the roll back color
        /// </summary>
        public string RollBackColor
        {
            get { return this.rollBackColor; }
            set { this.rollBackColor = value; }
        }

        /// <summary>
        /// Gets or sets the roll color
        /// </summary>
        public string RollColor
        {
            get { return this.rollColor; }
            set { this.rollColor = value; }
        }

        /// <summary>
        /// Gets or sets the click border color
        /// </summary>
        public string ClickBorderColor
        {
            get { return this.clickBorderColor; }
            set { this.clickBorderColor = value; }
        }

        /// <summary>
        /// Gets or sets the roll image file
        /// </summary>
        public string RollImageFile
        {
            get { return this.rollImageFile; }
            set { this.rollImageFile = value; }
        }

        /// <summary>
        /// Gets or sets the click image file
        /// </summary>
        public string ClickImageFile
        {
            get { return this.clickImageFile; }
            set { this.clickImageFile = value; }
        }

        #endregion

    }
}
