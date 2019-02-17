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

        #endregion

        #region Methods

        /// <summary>
        /// Gets all property names
        /// </summary>
        /// <returns>properties</returns>
        public override string[] GetProperties()
        {
            return new string[] { "name", "children", "action", "parent", "beams" };
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
        /// Add a child UX
        /// </summary>
        /// <param name="obj">child UX</param>
        public void Add(IUXObject obj)
        {
            ((Marshalling.MarshallingList)this.GetProperty("children")).Add(obj);
        }

        /// <summary>
        /// Recursive connect function call
        /// </summary>
        /// <param name="ux">ux</param>
        /// <param name="web">web browser</param>
        protected void RecursiveConnect(IUXObject ux, WebBrowser web)
        {
            ux.Connect(web);
            foreach (IUXObject child in ux.GetProperty("children"))
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
            foreach (IUXObject child in ux.GetProperty("children"))
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
                this.Get("action").Invoke();
        }

        /// <summary>
        /// Update for this ux and its children
        /// </summary>
        public virtual void UpdateChildren()
        {
            this.UpdateOne();
            foreach (IUXObject ux in this.GetProperty("children"))
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
        public void SetUpdate(Action a)
        {
            this.Set("action", a);
        }

        /// <summary>
        /// Clone this
        /// </summary>
        /// <returns>new object</returns>
        public object Clone()
        {
            UXControl c = new UXControl();
            foreach (KeyValuePair<string, dynamic> x in this.Data)
            {
                if (x.Key == "children")
                {
                    List<IUXObject> objects = new List<IUXObject>();
                    foreach (IUXObject a in x.Value)
                    {
                        objects.Add(a.Clone() as IUXObject);
                    }
                    c.Set("children", new Marshalling.MarshallingHash("children", objects));
                }
                else
                    c.Set(x.Key, x.Value);
            }
            return c;
        }

        /// <summary>
        /// Constructs UX by marshalling information
        /// </summary>
        /// <param name="m">element for construction</param>
        /// <param name="ui">ui properties</param>
        public virtual void Construct(Marshalling.IMarshalling m, Marshalling.IMarshalling ui)
        {
            Marshalling.MarshallingHash hash = ui as Marshalling.MarshallingHash;
            CommonProperties cp = hash["properties"].Value;
            // enregistrement des elements
            this.Get("beams").SetPropertyValues(new List<KeyValuePair<string, Beam>> {
                new KeyValuePair<string, Beam>("properties", Beam.Register("properties", this, cp))
            }.ToArray());
        }

        #endregion

        #region Static Methods

        public static void CreateUXControls(Marshalling.MarshallingList childs, Marshalling.MarshallingList uiChilds)
        {
            int index = 0;
            foreach (Marshalling.MarshallingHash h in childs.Values)
            {
                string typeName = h["type"].Value;
                string name = h["name"].Value;
                switch (typeName)
                {
                    case "UXReadOnlyText":
                        UXReadOnlyText.CreateUXReadOnlyText(h, uiChilds.Values.ElementAt(index) as Marshalling.MarshallingHash);
                        break;
                    case "UXClickableText":
                        UXClickableText.CreateUXClickableText(h, uiChilds.Values.ElementAt(index) as Marshalling.MarshallingHash);
                        break;
                    case "UXSelectableText":
                        UXSelectableText.CreateUXSelectableText(h, uiChilds.Values.ElementAt(index) as Marshalling.MarshallingHash);
                        break;
                    case "UXEditableText":
                        UXEditableText.CreateUXEditableText(h, uiChilds.Values.ElementAt(index) as Marshalling.MarshallingHash);
                        break;
                }
                ++index;
            }

        }

        #endregion

    }
}
