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

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public UXImage()
        {
        }

        /// <summary>
        /// Creates elements
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="e">elements</param>
        public UXImage(string name, IDictionary<string, dynamic> e)
            : base(name, e)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the Id
        /// </summary>
        public string Id
        {
            get { return this.Get("Id", string.Empty).Value; }
        }

        /// <summary>
        /// Gets the image file
        /// </summary>
        public string ImageFile
        {
            get { return this.Get("ImageFile", string.Empty).Value; }
        }

        /// <summary>
        /// Gets the image width
        /// </summary>
        public int ImageWidth
        {
            get { return this.Get("ImageWidth", string.Empty).Value; }
        }

        /// <summary>
        /// Gets the image height
        /// </summary>
        public int ImageHeight
        {
            get { return this.Get("ImageHeight", string.Empty).Value; }
        }
        
        #endregion

        #region Static Methods

        /// <summary>
        /// Create editable text
        /// </summary>
        /// <param name="data">data to show</param>
        /// <param name="ui">ui properties</param>
        public static UXImage CreateUXImage(Marshalling.MarshallingHash data, Marshalling.MarshallingHash ui)
        {
            UXImage ux = new UXImage();
            ux.Bind(data);
            ux.Bind(ui);
            return ux;
        }

        #endregion
    }
}
