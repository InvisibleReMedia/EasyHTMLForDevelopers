using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UXFramework
{
    /// <summary>
    /// Data context
    /// </summary>
    public interface IMLData
    {
        /// <summary>
        /// Delegate to export
        /// </summary>
        /// <returns>ux object</returns>
        event EventHandler ExportEvent;
        /// <summary>
        /// Export ux from data
        /// </summary>
        /// <returns>ux instance</returns>
        IUXObject Export();
    }
}
