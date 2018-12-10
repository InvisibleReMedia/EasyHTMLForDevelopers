using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UXFramework.BeamConnections
{
    /// <summary>
    /// Visual identities guidelines
    /// La charte graphique
    /// La couleur : les bords, le fond, le texte, entre les bords
    /// le reste : font, taille texte
    /// 
    /// This class is a singleton
    /// </summary>
    public class VisualIdentity : Marshalling.PersistentDataObject
    {

        #region Fields

        public static readonly string BorderColorName = "borderColor";
        public static readonly string BackgroundColorName = "backColor";
        public static readonly string ForegroundColorName = "foreColor";
        public static readonly string InBorderColorName = "inBorderColor";
        public static readonly string FontName = "font";
        public static readonly string FontSizeName = "fontSize";

        private static VisualIdentity visual = new VisualIdentity();

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor (private)
        /// </summary>
        private VisualIdentity()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Get the current visual identity
        /// </summary>
        public static VisualIdentity Current
        {
            get { return visual; }
        }

        /// <summary>
        /// Gets or set the border color
        /// </summary>
        public string BorderColor
        {
            get { return this.Get(BorderColorName, "black"); }
            set { this.Set(BorderColorName, value); }
        }

        /// <summary>
        /// Gets or set the background color
        /// </summary>
        public string BackgroundColor
        {
            get { return this.Get(BackgroundColorName, "green"); }
            set { this.Set(BackgroundColorName, value); }
        }

        /// <summary>
        /// Gets or set the foreground color
        /// </summary>
        public string ForegroundColor
        {
            get { return this.Get(ForegroundColorName, "black"); }
            set { this.Set(ForegroundColorName, value); }
        }

        /// <summary>
        /// Gets or set the in border color
        /// </summary>
        public string InBorderColor
        {
            get { return this.Get(InBorderColor, "blue"); }
            set { this.Set(InBorderColor, value); }
        }

        /// <summary>
        /// Gets or set the font
        /// </summary>
        public string Font
        {
            get { return this.Get(FontName, "Arial"); }
            set { this.Set(FontName, value); }
        }

        /// <summary>
        /// Gets or set the font size
        /// </summary>
        public string FontSize
        {
            get { return this.Get(FontSizeName, "12pt"); }
            set { this.Set(FontSizeName, value); }
        }

        #endregion

    }
}
