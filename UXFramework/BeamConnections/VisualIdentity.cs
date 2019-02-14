using System;
using System.Collections.Generic;
using System.Drawing;
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

        /// <summary>
        /// Couleur du bord
        /// </summary>
        public static readonly string BorderColorName = "borderColor";
        /// <summary>
        /// Couleur du fond
        /// </summary>
        public static readonly string BackgroundColorName = "backColor";
        /// <summary>
        /// Couleur du texte
        /// </summary>
        public static readonly string ForegroundColorName = "foreColor";
        /// <summary>
        /// Font
        /// </summary>
        public static readonly string FontName = "font";
        /// <summary>
        /// Font size
        /// </summary>
        public static readonly string FontSizeName = "fontSize";
        /// <summary>
        /// border size
        /// </summary>
        public static readonly string BorderSizeName = "borderSize";
        /// <summary>
        /// margin size
        /// </summary>
        public static readonly string MarginSizeName = "marginSize";
        /// <summary>
        /// padding size
        /// </summary>
        public static readonly string PaddingSizeName = "paddingSize";
        /// <summary>
        /// selection color
        /// </summary>
        public static readonly string SelectionColorName = "selectionColor";


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
            get { return this.Get(BackgroundColorName, "transparent"); }
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
        /// Gets or set the foreground color
        /// </summary>
        public string SelectionColor
        {
            get { return this.Get(SelectionColorName, "blue"); }
            set { this.Set(SelectionColorName, value); }
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

        /// <summary>
        /// Gets or sets the border size
        /// </summary>
        public Size BorderSize
        {
            get { return this.Get(BorderSizeName, new Size(5,5)); }
            set { this.Set(BorderSizeName, value); }
        }

        /// <summary>
        /// Gets or sets the margin size
        /// </summary>
        public Size MarginSize
        {
            get { return this.Get(MarginSizeName, new Size(10,10)); }
            set { this.Set(MarginSizeName, value); }
        }

        /// <summary>
        /// Gets or sets the margin size
        /// </summary>
        public Size PaddingSize
        {
            get { return this.Get(PaddingSizeName, new Size(2,2)); }
            set { this.Set(PaddingSizeName, value); }
        }

        #endregion

    }
}
