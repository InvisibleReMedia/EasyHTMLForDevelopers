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
    public interface IUXObject
    {
        /// <summary>
        /// Gets a name of this ux
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// Children UX
        /// </summary>
        List<IUXObject> Children { get; }
        /// <summary>
        /// Parent UX
        /// </summary>
        IUXObject Parent { get; set; }
        /// <summary>
        /// Beam UX-Plateform
        /// </summary>
        BeamConnections.InteractiveBeam Beam { get; }
        /// <summary>
        /// Add a new string into HTML destination stream
        /// </summary>
        /// <param name="s">text</param>
        void Add(string s);
        /// <summary>
        /// Add a new control in children list
        /// </summary>
        /// <param name="obj">ux object</param>
        void Add(IUXObject obj);
        /// <summary>
        /// Write default text into the stream
        /// </summary>
        /// <param name="sw">stream</param>
        void Write(StreamWriter sw);
        /// <summary>
        /// Connect for interoperability C#/Web
        /// </summary>
        void Connect();
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
        void SetUpdate(Action a);
    }
}
