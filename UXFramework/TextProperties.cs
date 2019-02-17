using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UXFramework
{
    /// <summary>
    /// Properties for text
    /// </summary>
    public class TextProperties : CommonProperties
    {

        #region Fields

        /// <summary>
        /// Initial text
        /// </summary>
        public static readonly string initialTextName = "initialText";
        /// <summary>
        /// Roll over text
        /// </summary>
        public static readonly string rollTextName = "rollText";
        /// <summary>
        /// text where click
        /// </summary>
        public static readonly string clickTextName = "clickText";

        #endregion

        #region Construtors

        public TextProperties()
        {

        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the initial text
        /// </summary>
        public string InitialText
        {
            get
            {
                return this.Get(initialTextName, "Hello World !");
            }
            set
            {
                this.Set(initialTextName, value);
            }
        }

        /// <summary>
        /// Gets or sets the roll text
        /// </summary>
        public string RollText
        {
            get
            {
                return this.Get(rollTextName, "Hello Wald !");
            }
            set
            {
                this.Set(rollTextName, value);
            }
        }

        /// <summary>
        /// Gets or sets the click text
        /// </summary>
        public string ClickText
        {
            get
            {
                return this.Get(clickTextName, "Hello Kitie !");
            }
            set
            {
                this.Set(clickTextName, value);
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
            TextProperties tp = new TextProperties();
            tp.Width = this.Width;
            tp.Height = this.Height;
            tp.BackColor = this.BackColor;
            tp.Border = this.Border;
            tp.ClickBorderColor = this.ClickBorderColor;
            tp.ForeColor = this.ForeColor;
            tp.Margin = this.Margin;
            tp.Padding = this.Padding;
            tp.RollColor = this.RollColor;
            tp.InitialText = this.InitialText;
            tp.RollText = this.RollText;
            tp.ClickText = this.ClickText;
            return tp;
        }

        #endregion
    }
}
