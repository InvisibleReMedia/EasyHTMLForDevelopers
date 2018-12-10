using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library
{
    /// <summary>
    /// Interface for notification
    /// </summary>
    public interface INotify : ICloneable
    {
        /// <summary>
        /// Notification name
        /// </summary>
        string NotificationName { get; set; }
        /// <summary>
        /// True if server side event
        /// </summary>
        bool IsServerSide { get; }
        /// <summary>
        /// Raise event
        /// </summary>
        List<Func<object, EventArgs, string>> Raise { get; }
        /// <summary>
        /// Handle event
        /// </summary>
        /// <param name="sender">source</param>
        /// <param name="e">arguments</param>
        /// <returns>code to insert</returns>
        string Catch(object sender, EventArgs e);
    }
}
