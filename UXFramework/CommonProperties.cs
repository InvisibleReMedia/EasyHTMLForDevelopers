using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UXFramework
{
    /// <summary>
    /// Class for common properties
    /// </summary>
    public class CommonProperties : Marshalling.PersistentDataObject
    {

        #region Fields

        /// <summary>
        /// Name
        /// </summary>
        public static readonly string nameName = "name";
        /// <summary>
        /// Width
        /// </summary>
        public static readonly string widthName = "width";
        /// <summary>
        /// Height
        /// </summary>
        public static readonly string heightName = "height";
        /// <summary>
        /// Back color
        /// </summary>
        public static readonly string backColorName = "backColor";
        /// <summary>
        /// Fore color
        /// </summary>
        public static readonly string foreColorName = "foreColor";
        /// <summary>
        /// Border
        /// </summary>
        public static readonly string borderName = "border";
        /// <summary>
        /// margin
        /// </summary>
        public static readonly string marginName = "margin";
        /// <summary>
        /// Padding
        /// </summary>
        public static readonly string paddingName = "padding";
        /// <summary>
        /// Roll Color
        /// </summary>
        public static readonly string rollColorName = "rollColor";
        /// <summary>
        /// Click color at border
        /// </summary>
        public static readonly string clickBorderColorName = "clickBorderColor";
            
        #endregion

        #region Constructor

        public CommonProperties()
        {

        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the width
        /// </summary>
        public int? Width
        {
            get
            {
                if (this.Exists(widthName))
                    return new Nullable<int>(this.Get(widthName));
                else
                    return null;
            }
            set
            {
                if (value.HasValue)
                    this.Set(widthName, value.Value);
            }
        }

        /// <summary>
        /// Gets or sets the height
        /// </summary>
        public int? Height
        {
            get
            {
                if (this.Exists(heightName))
                    return new Nullable<int>(this.Get(heightName));
                else
                    return null;
            }
            set
            {
                this.Set(heightName, value);
            }
        }

        /// <summary>
        /// Gets or sets the back color
        /// </summary>
        public string BackColor
        {
            get
            {
                if (this.Exists(backColorName))
                    return this.Get(backColorName);
                else
                    return null;
            }
            set
            {
                this.Set(backColorName, value);
            }
        }

        /// <summary>
        /// Gets or sets the fore color
        /// </summary>
        public string ForeColor
        {
            get
            {
                if (this.Exists(foreColorName))
                    return this.Get(foreColorName);
                else
                    return null;
            }
            set
            {
                this.Set(foreColorName, value);
            }
        }

        /// <summary>
        /// Gets or sets the border
        /// </summary>
        public string Border
        {
            get
            {
                if (this.Exists(borderName))
                    return this.Get(borderName);
                else
                    return null;
            }
            set
            {
                this.Set(borderName, value);
            }
        }

        /// <summary>
        /// Gets or sets margin
        /// </summary>
        public string Margin
        {
            get
            {
                if (this.Exists(marginName))
                    return this.Get(marginName);
                else
                    return null;
            }
            set
            {
                this.Set(marginName, value);
            }
        }

        /// <summary>
        /// Gets or sets the width
        /// </summary>
        public string Padding
        {
            get
            {
                if (this.Exists(paddingName))
                    return this.Get(paddingName);
                else
                    return null;
            }
            set
            {
                this.Set(paddingName, value);
            }
        }

        /// <summary>
        /// Gets or sets the roll color
        /// </summary>
        public string RollColor
        {
            get
            {
                if (this.Exists(rollColorName))
                    return this.Get(rollColorName);
                else
                    return null;
            }
            set
            {
                this.Set(rollColorName, value);
            }
        }

        /// <summary>
        /// Gets or sets the fore color
        /// </summary>
        public string ClickBorderColor
        {
            get
            {
                if (this.Exists(clickBorderColorName))
                    return this.Get(clickBorderColorName);
                else
                    return null;
            }
            set
            {
                this.Set(clickBorderColorName, value);
            }
        }

        /// <summary>
        /// Gets or sets the name
        /// (not used)
        /// </summary>
        public string Name
        {
            get
            {
                return this.Get(nameName, "");
            }
            set
            {
                this.Set(nameName, value);
            }
        }

        /// <summary>
        /// Gets or sets the value
        /// (not used)
        /// </summary>
        public dynamic Value
        {
            get
            {
                return this;
            }
            set
            {
                
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Clone this
        /// </summary>
        /// <returns>cloned object</returns>
        public object Clone()
        {
            CommonProperties cp = new CommonProperties();
            cp.Width = this.Width;
            cp.Height = this.Height;
            cp.BackColor = this.BackColor;
            cp.Border = this.Border;
            cp.ClickBorderColor = this.ClickBorderColor;
            cp.ForeColor = this.ForeColor;
            cp.Margin = this.Margin;
            cp.Padding = this.Padding;
            cp.RollColor = this.RollColor;
            return cp;
        }

        #endregion
    }
}
