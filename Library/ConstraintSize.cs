using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library
{

    /// <summary>
    /// Set of elements to compute size
    /// with respect of constraint enumeration
    /// </summary>
    public struct ConstraintSize
    {

        #region Fields

        /// <summary>
        /// Constraint with and height
        /// </summary>
        public EnumConstraint constraintWidth, constraintHeight;
        /// <summary>
        /// Width and height size
        /// </summary>
        public uint width, height;
        /// <summary>
        /// Width and height string representation
        /// </summary>
        public string widthString, heightString;
        /// <summary>
        /// Width and height attributes
        /// </summary>
        public string attributeWidth, attributeHeight;
        /// <summary>
        /// force Width and height values
        /// </summary>
        public uint forcedWidth, forcedHeight;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="constraintWidth">constraint width enumeration</param>
        /// <param name="width">width size</param>
        /// <param name="preceding_width">width size outbox</param>
        /// <param name="constraintHeight">constraint height enumeration</param>
        /// <param name="height">height size</param>
        /// <param name="preceding_height">height size outbox</param>
        public ConstraintSize(EnumConstraint constraintWidth, uint width, uint preceding_width, EnumConstraint constraintHeight, uint height, uint preceding_height)
        {
            this.constraintWidth = constraintWidth;
            this.constraintHeight = constraintHeight;
            this.width = this.height = 0;
            this.widthString = this.heightString = String.Empty;
            this.attributeHeight = this.attributeWidth = String.Empty;
            this.forcedWidth = preceding_width;
            this.forcedHeight = preceding_height;

            // calcul la taille en fonction du type de contrainte
            switch (this.constraintWidth)
            {
                case EnumConstraint.AUTO:
                    this.width = width;
                    break;
                case EnumConstraint.FIXED:
                    this.width = width;
                    this.forcedWidth = this.width;
                    this.widthString = this.width.ToString() + "px";
                    this.attributeWidth = "width='" + this.widthString + "'";
                    break;
                case EnumConstraint.RELATIVE:
                    this.width = width;
                    this.widthString = this.width.ToString() + "%";
                    this.attributeWidth = "width='" + this.widthString + "'";
                    break;
                case EnumConstraint.FORCED:
                    if (preceding_width != 0)
                    {
                        this.width = preceding_width;
                        this.forcedWidth = this.width;
                        this.widthString = this.width.ToString() + "px";
                        this.attributeWidth = "width='" + this.widthString + "'";
                    }
                    break;
            }

            switch (this.constraintHeight)
            {
                case EnumConstraint.AUTO:
                    this.height = height;
                    break;
                case EnumConstraint.FIXED:
                    this.height = height;
                    this.forcedHeight = this.height;
                    this.heightString = this.height.ToString() + "px";
                    this.attributeHeight = "height='" + this.heightString + "'";
                    break;
                case EnumConstraint.RELATIVE:
                    this.height = height;
                    this.heightString = this.height.ToString() + "%";
                    this.attributeHeight = "height='" + this.heightString + "'";
                    break;
                case EnumConstraint.FORCED:
                    if (preceding_height != 0)
                    {
                        this.height = preceding_height;
                        this.forcedHeight = this.height;
                        this.heightString = this.height.ToString() + "px";
                        this.attributeHeight = "height='" + this.heightString + "'";
                    }
                    break;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Compute the string representation
        /// respect to constraint enumeration
        /// </summary>
        /// <returns>string form</returns>
        public string ComputeStyle()
        {
            string output = String.Empty;
            if (this.constraintWidth == EnumConstraint.FIXED && !String.IsNullOrEmpty(this.widthString))
                output += "width:" + this.widthString + ";";
            if (this.constraintHeight == EnumConstraint.FIXED && !String.IsNullOrEmpty(this.heightString))
                output += "height:" + this.heightString + ";";
            if (!String.IsNullOrEmpty(output))
                output = "style='cursor:pointer;" + output + "'";
            return output;
        }

        #endregion
    }
}
