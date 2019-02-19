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
        /// use Get(name, f()) to set your value if exists
        /// </summary>
        public string Id
        {
            get
            {
                if (this.Exists("Id"))
                {
                    return this.Get("Id").Value;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// Gets the image file
        /// </summary>
        public string ImageFile
        {
            get
            {
                if (this.Exists("ImageFile"))
                {
                    return this.Get("ImageFile").Value;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// Gets the image width
        /// use Get(name, f()) to set your value if exists
        /// </summary>
        public int ImageWidth
        {
            get
            {
                if (this.Exists("ImageWidth"))
                {
                    return this.Get("ImageWidth").Value;
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// Gets the image height
        /// use Get(name, f()) to set your value if exists
        /// </summary>
        public int ImageHeight
        {
            get
            {
                if (this.Exists("ImageHeight"))
                {
                    return this.Get("ImageHeight").Value;
                }
                else
                {
                    return 0;
                }
            }
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

        /// <summary>
        /// Create UXImage
        /// </summary>
        /// <param name="f">function to enter data</param>
        /// <returns>marshalling</returns>
        public static UXImage CreateUXImage(string name, Func<IDictionary<string, dynamic>> f)
        {
            return new UXImage(name, f());
        }

        #endregion
    }
}
