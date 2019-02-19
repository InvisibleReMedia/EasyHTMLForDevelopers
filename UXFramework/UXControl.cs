using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UXFramework.BeamConnections;

namespace UXFramework
{
    public class UXControl : Marshalling.MarshallingHash, IUXObject
    {

        #region Fields

        protected static IUXRenderer renderer = new WebImplementation.WebRenderer();

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public UXControl()
            : base("UXControl")
        {
        }

        /// <summary>
        /// Creates elements
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="e">elements</param>
        public UXControl(string name, IDictionary<string, dynamic> e)
            : base(name, e)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets Children
        /// </summary>
        public IEnumerable<IUXObject> Children
        {
            get
            {
                if (this.Exists("children"))
                    return this.GetProperty("children").Conversion<IUXObject>();
                else
                    return new List<IUXObject>();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Add a child into object
        /// </summary>
        /// <param name="child">child to add</param>
        public void Add(IUXObject child)
        {
            this.GetProperty("children").Add(() =>
            {
                return new List<dynamic>() { child };
            });
        }

        /// <summary>
        /// Bind function
        /// </summary>
        /// <param name="m">input</param>
        public override void Bind(Marshalling.IMarshalling m)
        {
            m.Copy(false, this);
        }

        /// <summary>
        /// Recursive connect function call
        /// </summary>
        /// <param name="ux">ux</param>
        /// <param name="web">web browser</param>
        protected void RecursiveConnect(IUXObject ux, WebBrowser web)
        {
            ux.Connect(web);
            foreach (IUXObject child in ux.Children)
            {
                RecursiveConnect(child, web);
            }
        }

        /// <summary>
        /// Recursive disconnect function call
        /// </summary>
        /// <param name="ux">ux</param>
        /// <param name="web">web browser</param>
        protected void RecursiveDisconnect(IUXObject ux, WebBrowser web)
        {
            ux.Disconnect(web);
            foreach (IUXObject child in ux.Children)
            {
                RecursiveDisconnect(child, web);
            }
        }

        /// <summary>
        /// Connect for interoperability C#/Web
        /// </summary>
        /// <param name="web">web browser</param>
        public virtual void Connect(WebBrowser web)
        {
        }

        /// <summary>
        /// Disconnect for interoperability C#/Web
        /// </summary>
        /// <param name="web">web browser</param>
        public virtual void Disconnect(WebBrowser web)
        {
        }

        /// <summary>
        /// Navigate function (main function)
        /// </summary>
        /// <param name="www">web browser control</param>
        public virtual void Navigate(WebBrowser www)
        {
        }

        /// <summary>
        /// Update one time for this ux
        /// </summary>
        public virtual void UpdateOne()
        {
            if (this.Exists("action") && this.Get("action") != null)
                this.Get("action").Invoke(this);
        }

        /// <summary>
        /// Update for this ux and its children
        /// </summary>
        public virtual void UpdateChildren()
        {
            this.UpdateOne();
            foreach (IUXObject ux in this.Children)
            {
                ux.UpdateChildren();
            }
        }

        /// <summary>
        /// Get the top-most window ux
        /// </summary>
        /// <returns>ux window</returns>
        public virtual IUXObject GetUXWindow()
        {
            IUXObject prop = this.GetProperty("parent") as IUXObject;
            if (prop != null)
                return prop.GetUXWindow();
            else
                throw new NullReferenceException("parent vide");
        }

        /// <summary>
        /// Get the current web browser
        /// </summary>
        /// <returns>web browser control</returns>
        public virtual WebBrowser GetWebBrowser()
        {
            return this.GetUXWindow().GetWebBrowser();
        }

        /// <summary>
        /// Set action function to be called
        /// when something changed in web
        /// </summary>
        /// <param name="a">action</param>
        public void SetUpdate(Action<IUXObject> a)
        {
            this.Set("action", a);
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Create UXControl
        /// </summary>
        /// <param name="f">function to enter data</param>
        /// <returns>marshalling</returns>
        public static UXControl CreateUXControl(string name, Func<IDictionary<string, dynamic>> f)
        {
            return new UXControl(name, f());
        }

        #endregion

    }
}
