using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UXFramework
{
    /// <summary>
    /// UX Object interface
    /// </summary>
    public interface IUXObject : Marshalling.IMarshalling
    {
        /// <summary>
        /// Gets all children
        /// </summary>
        IEnumerable<IUXObject> Children { get; }
        /// <summary>
        /// Add a child into object
        /// </summary>
        /// <param name="child">child to add</param>
        void Add(IUXObject child);
        /// <summary>
        /// Connect for interoperability C#/Web
        /// </summary>
        /// <param name="web">web browser</param>
        void Connect(WebBrowser web);
        /// <summary>
        /// Connect for interoperability C#/Web
        /// </summary>
        /// <param name="web">web browser</param>
        void Disconnect(WebBrowser web);
        /// <summary>
        /// Navigate function (main function)
        /// </summary>
        /// <param name="www">web browser control</param>
        void Navigate(WebBrowser www);
        /// <summary>
        /// Get the current web browser
        /// </summary>
        /// <returns>web browser control</returns>
        WebBrowser GetWebBrowser();
        /// <summary>
        /// Get the top-most window ux
        /// </summary>
        /// <returns>ux window</returns>
        IUXObject GetUXWindow();
        /// <summary>
        /// Update one time for this ux
        /// </summary>
        void UpdateOne();
        /// <summary>
        /// Update for this ux and its children
        /// </summary>
        void UpdateChildren();
        /// <summary>
        /// Set action function to be called
        /// when something changed in web
        /// </summary>
        /// <param name="a">action</param>
        void SetUpdate(Action<IUXObject> a);
        /// <summary>
        /// Bind
        /// </summary>
        /// <param name="m">input</param>
        void Bind(Marshalling.IMarshalling m);
    }
}
