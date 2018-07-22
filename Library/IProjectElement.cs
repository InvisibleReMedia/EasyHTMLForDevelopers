using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{

    /// <summary>
    /// Interface of an element project
    /// </summary>
    public interface IProjectElement
    {
        /// <summary>
        /// Gets the type name
        /// </summary>
        string TypeName { get; }
        /// <summary>
        /// Gets the element title
        /// </summary>
        string ElementTitle { get; }
    }
}
