using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library
{
    /// <summary>
    /// Constraints
    /// </summary>
    public enum EnumConstraint
    {
        /// <summary>
        /// No contraints
        /// </summary>
        AUTO,
        /// <summary>
        /// a fixed numeric value
        /// </summary>
        FIXED,
        /// <summary>
        /// a relative percent value
        /// </summary>
        RELATIVE,
        /// <summary>
        /// force child box setted
        /// </summary>
        FORCED
    }
}
