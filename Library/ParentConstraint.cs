using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library
{

    /// <summary>
    /// Parent Constraint
    /// It is usefull to transport and transform
    /// width and height contraints
    /// </summary>
    public struct ParentConstraint
    {
        /// <summary>
        /// Object Name
        /// </summary>
        public string objectName;
        /// <summary>
        /// Previous width
        /// </summary>
        public uint precedingWidth;
        /// <summary>
        /// Previous height
        /// </summary>
        public uint precedingHeight;
        /// <summary>
        /// Width constraint
        /// </summary>
        public EnumConstraint constraintWidth;
        /// <summary>
        /// Height constraint
        /// </summary>
        public EnumConstraint constraintHeight;
        /// <summary>
        /// Maximum width allowed
        /// </summary>
        public uint maximumWidth;
        /// <summary>
        /// Maximum height allowed
        /// </summary>
        public uint maximumHeight;
        /// <summary>
        /// Disposition for the object
        /// </summary>
        public Disposition disposition;
        /// <summary>
        /// Border size and constraint
        /// </summary>
        public BorderConstraint border;

        /// <summary>
        /// Constructor without a disposition
        /// The default disposition is CENTER
        /// </summary>
        /// <param name="objectName">object name</param>
        /// <param name="precedingWidth">previous width</param>
        /// <param name="precedingHeight">previous height</param>
        /// <param name="constraintWidth">width constraint</param>
        /// <param name="constraintHeight">height constraint</param>
        /// <param name="maximumWidth">maximum width</param>
        /// <param name="maximumHeight">maximum height</param>
        /// <param name="border">border size and constraint</param>
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

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="objectName">object name</param>
        /// <param name="precedingWidth">previous width</param>
        /// <param name="precedingHeight">previous height</param>
        /// <param name="constraintWidth">width constraint</param>
        /// <param name="constraintHeight">height constraint</param>
        /// <param name="maximumWidth">maximum width</param>
        /// <param name="maximumHeight">maximum height</param>
        /// <param name="disposition">disposition</param>
        /// <param name="border">border size and constraint</param>
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

        /// <summary>
        /// Constructor from a previous parent constraint
        /// </summary>
        /// <param name="objectName">object name</param>
        /// <param name="parent">parent constraint</param>
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
