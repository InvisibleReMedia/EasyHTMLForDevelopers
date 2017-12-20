using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library
{
    public struct ConstraintSize
    {
        public EnumConstraint constraintWidth, constraintHeight;
        public uint width, height;
        public string widthString, heightString;
        public string attributeWidth, attributeHeight;
        public uint forcedWidth, forcedHeight;

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
    }
}
