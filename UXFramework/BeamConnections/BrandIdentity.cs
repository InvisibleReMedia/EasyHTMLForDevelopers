using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UXFramework.BeamConnections
{
    /// <summary>
    /// Charte graphique
    /// </summary>
    public class BrandIdentity : Marshalling.PersistentDataObject
    {

        #region Fields

        /// <summary>
        /// Couleurs
        /// </summary>
        public static readonly string colorsName = "colors";

        /// <summary>
        /// Fonts
        /// </summary>
        public static readonly string fontsName = "fonts";

        /// <summary>
        /// Taille de font
        /// </summary>
        public static readonly string fontSizesName = "fontSizes";

        /// <summary>
        /// Images of this brand
        /// </summary>
        public static readonly string imagesName = "images";

        /// <summary>
        /// Size of known elements
        /// </summary>
        public static readonly string sizesName = "sizes";

        /// <summary>
        /// All box items
        /// </summary>
        public static readonly string boxesName = "boxes";

        private static BrandIdentity brand = new BrandIdentity();

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        private BrandIdentity()
        {
            this.Set(colorsName, new Dictionary<string, string>()); // list of colors
            this.Set(fontsName, new Dictionary<string, string>());  // list of font
            this.Set(fontSizesName, new Dictionary<string, string>());  // list of font sizes
            this.Set(imagesName, new Dictionary<string, string>());     // list of images
            this.Set(sizesName, new Dictionary<string, Size>());        // list of sizes
            this.Set(boxesName, new Dictionary<string, Rectangle>());   // list of boxes
        }

        #endregion

        #region Properties

        /// <summary>
        /// Brand identity
        /// </summary>
        public static BrandIdentity Current
        {
            get { return BrandIdentity.brand; }
        }

        /// <summary>
        /// Gets colors
        /// </summary>
        public Dictionary<string, string> Colors
        {
            get
            {
                return this.Get(colorsName);
            }
        }

        /// <summary>
        /// Gets fonts
        /// </summary>
        public Dictionary<string, string> Fonts
        {
            get
            {
                return this.Get(fontsName);
            }
        }

        /// <summary>
        /// Gets font size
        /// </summary>
        public Dictionary<string, string> FontSizes
        {
            get
            {
                return this.Get(fontSizesName);
            }
        }

        /// <summary>
        /// Gets images
        /// </summary>
        public Dictionary<string, string> Images
        {
            get
            {
                return this.Get(imagesName);
            }
        }

        /// <summary>
        /// Gets sizes
        /// </summary>
        public Dictionary<string, Size> Sizes
        {
            get
            {
                return this.Get(sizesName);
            }
        }

        /// <summary>
        /// Gets sizes
        /// </summary>
        public Dictionary<string, Rectangle> Boxes
        {
            get
            {
                return this.Get(boxesName);
            }
        }

        #endregion


    }
}
