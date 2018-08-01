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
        string NotificationName { get; set; }
        bool IsServerSide { get; }
        List<Func<object, EventArgs, string>> Raise { get; }
        string Catch(object sender, EventArgs e);
    }
}
