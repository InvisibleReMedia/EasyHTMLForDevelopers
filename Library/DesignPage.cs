using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library
{
    /// <summary>
    /// Structure to host properties across elements
    /// </summary>
    public struct DesignPage
    {
        /// <summary>
        /// Width
        /// </summary>
        public uint width;
        /// <summary>
        /// Height
        /// </summary>
        public uint height;
        /// <summary>
        /// Width constraint
        /// </summary>
        public EnumConstraint constraintWidth;
        /// <summary>
        /// Height constraint
        /// </summary>
        public EnumConstraint constraintHeight;
        /// <summary>
        /// CSS additional list
        /// </summary>
        public CSSList cssList;
        /// <summary>
        /// Evenements
        /// </summary>
        public Events events;
        /// <summary>
        /// Javascript code
        /// </summary>
        public CodeJavaScript javascriptPart;
        /// <summary>
        /// Javascript on load code
        /// </summary>
        public CodeJavaScript onload;
        /// <summary>
        /// Is CSS exported to a file
        /// </summary>
        public bool cssOnFile;
        /// <summary>
        /// File name where resides CSS code
        /// </summary>
        public string cssFile;
        /// <summary>
        /// Is Javascript exported to a file
        /// </summary>
        public bool javascriptOnFile;
        /// <summary>
        /// File name where resides Javascript code
        /// </summary>
        public string javascriptFile;
        /// <summary>
        /// Horizontal areas
        /// </summary>
        public List<HorizontalZone> zones;
        /// <summary>
        /// Is include into a container
        /// </summary>
        public bool includeContainer;
        /// <summary>
        /// All objects
        /// </summary>
        public List<HTMLObject> subObjects;
    }
}
