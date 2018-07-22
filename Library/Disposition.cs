using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library
{
    /// <summary>
    /// Disposition
    /// </summary>
    public enum Disposition
    {
        /// <summary>
        /// Center (both)
        /// </summary>
        CENTER,
        /// <summary>
        /// Left and corner
        /// </summary>
        LEFT_TOP,
        /// <summary>
        /// Left and middle height
        /// </summary>
        LEFT_MIDDLE,
        /// <summary>
        /// Left and bottom height
        /// </summary>
        LEFT_BOTTOM,
        /// <summary>
        /// Center top
        /// </summary>
        CENTER_TOP,
        /// <summary>
        /// Center bottom
        /// </summary>
        CENTER_BOTTOM,
        /// <summary>
        /// Right and top
        /// </summary>
        RIGHT_TOP,
        /// <summary>
        /// Right and middle
        /// </summary>
        RIGHT_MIDDLE,
        /// <summary>
        /// Right and bottom
        /// </summary>
        RIGHT_BOTTOM
    }
}
