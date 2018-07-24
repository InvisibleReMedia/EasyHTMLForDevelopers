using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UXFramework
{
    public class UXControl : IUXObject
    {

        #region Fields

        /// <summary>
        /// Parent object
        /// </summary>
        private IUXObject parent;
        /// <summary>
        /// Inner controls
        /// </summary>
        private List<IUXObject> children;
        /// <summary>
        /// delegate to update ux
        /// </summary>
        private Action update;
        /// <summary>
        /// HTML source
        /// </summary>
        protected StringBuilder htmlSrc;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public UXControl()
        {
            this.children = new List<IUXObject>();
            this.htmlSrc = new StringBuilder();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Children UX
        /// </summary>
        public List<IUXObject> Children
        {
            get { return this.children; }
        }

        /// <summary>
        /// Parent UX
        /// </summary>
        public IUXObject Parent
        {
            get
            {
                return this.parent;
            }
            set
            {
                this.parent = value;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Add a new string into HTML destination stream
        /// </summary>
        /// <param name="s">text</param>
        public void Add(string s)
        {
            this.htmlSrc.Append(s);
        }

        /// <summary>
        /// Add a new control in children list
        /// </summary>
        /// <param name="obj">ux object</param>
        public void Add(IUXObject obj)
        {
            obj.Parent = this;
            this.children.Add(obj);
        }

        /// <summary>
        /// Write default text into the stream
        /// </summary>
        /// <param name="sw">stream</param>
        public virtual void Write(StreamWriter sw)
        {
            sw.Write(this.htmlSrc.ToString());
        }

        /// <summary>
        /// Connect for interoperability C#/Web
        /// </summary>
        public virtual void Connect()
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
            if (this.update != null)
                this.update.Invoke();
        }

        /// <summary>
        /// Update for this ux and its children
        /// </summary>
        public virtual void UpdateChildren()
        {
            this.UpdateOne();
            foreach (IUXObject ux in this.children)
            {
                ux.UpdateChildren();
            }
        }

        /// <summary>
        /// Get the top-most window ux
        /// </summary>
        /// <returns>ux window</returns>
        public IUXObject GetUXWindow()
        {
            IUXObject current = this;
            while (current.Parent != null && !(current is UXWindow))
            {
                current = current.Parent;
            }
            return current;
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
        public void SetUpdate(Action a)
        {
            this.update = a;
        }

        #endregion

    }
}
