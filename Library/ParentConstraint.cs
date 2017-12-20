using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library
{
    public struct ParentConstraint
    {
        public string objectName;
        public uint precedingWidth;
        public uint precedingHeight;
        public EnumConstraint constraintWidth;
        public EnumConstraint constraintHeight;
        public uint maximumWidth;
        public uint maximumHeight;
        public Disposition disposition;
        public BorderConstraint border;

        public ParentConstraint(string objectName, uint precedingWidth, uint precedingHeight, EnumConstraint constraintWidth,
                                EnumConstraint constraintHeight, uint maximumWidth, uint maximumHeight,
                                BorderConstraint border)
        {
            this.precedingWidth = precedingWidth;
            this.precedingHeight = precedingHeight;
            this.constraintWidth = constraintWidth;
            this.constraintHeight = constraintHeight;
            this.maximumWidth = maximumWidth;
            this.maximumHeight = maximumHeight;
            this.disposition = Disposition.CENTER;
            this.border = border;
            this.objectName = objectName;
        }

        public ParentConstraint(string objectName, uint precedingWidth, uint precedingHeight, EnumConstraint constraintWidth, 
                                EnumConstraint constraintHeight, uint maximumWidth, uint maximumHeight, Disposition disposition,
                                BorderConstraint border)
        {
            this.precedingWidth = precedingWidth;
            this.precedingHeight = precedingHeight;
            this.constraintWidth = constraintWidth;
            this.constraintHeight = constraintHeight;
            this.maximumWidth = maximumWidth;
            this.maximumHeight = maximumHeight;
            this.disposition = disposition;
            this.border = border;
            this.objectName = objectName;
        }

        public ParentConstraint(string objectName, ParentConstraint parent)
        {
            this.precedingWidth = parent.precedingWidth;
            this.precedingHeight = parent.precedingHeight;
            this.constraintWidth = parent.constraintWidth;
            this.constraintHeight = parent.constraintHeight;
            this.maximumWidth = parent.maximumWidth;
            this.maximumHeight = parent.maximumHeight;
            this.disposition = parent.disposition;
            this.border = parent.border;
            this.objectName = objectName;
        }
    }
}
