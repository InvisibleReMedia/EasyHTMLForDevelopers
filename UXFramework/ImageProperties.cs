using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UXFramework
{
    /// <summary>
    /// Properties for image
    /// </summary>
    public class ImageProperties : TextProperties
    {

        #region Fields

        /// <summary>
        /// Initial text
        /// </summary>
        public static readonly string initialImageName = "initialImage";
        /// <summary>
        /// Roll over text
        /// </summary>
        public static readonly string rollImageName = "rollImage";
        /// <summary>
        /// text where click
        /// </summary>
        public static readonly string clickImageName = "clickImage";

        #endregion

        #region Construtors

        public ImageProperties()
        {

        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the initial image
        /// </summary>
        public string InitialImage
        {
            get
            {
                if (this.Exists(initialImageName))
                    return this.Get(initialImageName);
                else
                    return null;
            }
            set
            {
                this.Set(initialImageName, value);
            }
        }

        /// <summary>
        /// Gets or sets the roll image
        /// </summary>
        public string RollImage
        {
            get
            {
                if (this.Exists(rollImageName))
                    return this.Get(rollImageName);
                else
                    return null;
            }
            set
            {
                this.Set(rollImageName, value);
            }
        }

        /// <summary>
        /// Gets or sets the click image
        /// </summary>
        public string ClickImage
        {
            get
            {
                if (this.Exists(clickImageName))
                    return this.Get(clickImageName);
                else
                    return null;
            }
            set
            {
                this.Set(clickImageName, value);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Clone this
        /// </summary>
        /// <returns>cloned object</returns>
        public new object Clone()
        {
            ImageProperties ip = new ImageProperties();
            ip.Width = this.Width;
            ip.Height = this.Height;
            ip.BackColor = this.BackColor;
            ip.Border = this.Border;
            ip.ClickBorderColor = this.ClickBorderColor;
            ip.ForeColor = this.ForeColor;
            ip.Margin = this.Margin;
            ip.Padding = this.Padding;
            ip.RollColor = this.RollColor;
            ip.InitialText = this.InitialText;
            ip.RollText = this.RollText;
            ip.ClickText = this.ClickText;
            ip.InitialImage = this.InitialImage;
            ip.RollImage = this.RollImage;
            ip.ClickImage = this.ClickImage;
            return ip;
        }

        #endregion
    }
}
