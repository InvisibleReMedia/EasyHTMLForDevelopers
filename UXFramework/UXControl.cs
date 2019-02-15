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
    public class UXControl : Marshalling.PersistentDataObject, IUXObject, Marshalling.IMarshalling
    {

        #region Fields

        protected static IUXRenderer renderer = new WebImplementation.WebRenderer();
        /// <summary>
        /// Name
        /// </summary>
        public static readonly string nameName = "name";
        /// <summary>
        /// Parent object
        /// </summary>
        public static readonly string parentName = "parent";
        /// <summary>
        /// Inner controls
        /// </summary>
        public static readonly string childrenName = "children";
        /// <summary>
        /// Beam UX-Plateform
        /// </summary>
        public static readonly string beamName = "beam";
        /// <summary>
        /// delegate to update ux
        /// </summary>
        private static readonly string actionName = "action";
        /// <summary>
        /// HTML source
        /// </summary>
        public static readonly string htmlSourceName = "html";

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public UXControl()
        {
            this.Set(childrenName, new List<IUXObject>());
            this.Set(htmlSourceName, new StringBuilder());
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the Width size
        /// </summary>
        public uint? Width
        {
            get
            {
                dynamic d = this.Beams.GetPropertyValue("Width").ReadProperty();
                if (d != null)
                {
                    return d;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Gets the Height size
        /// </summary>
        public uint? Height
        {
            get
            {
                dynamic d = this.Beams.GetPropertyValue("Height").ReadProperty();
                if (d != null)
                {
                    return d;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Gets the background color
        /// </summary>
        public string BackgroundColor
        {
            get
            {
                dynamic d = this.Beams.GetPropertyValue("BackColor").ReadProperty();
                if (d != null)
                {
                    return d;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Gets the fore color
        /// </summary>
        public string ForegroundColor
        {
            get
            {
                dynamic d = this.Beams.GetPropertyValue("ForeColor").ReadProperty();
                if (d != null)
                {
                    return d;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Gets the border color
        /// </summary>
        public string Border
        {
            get
            {
                dynamic d = this.Beams.GetPropertyValue("Border").ReadProperty();
                if (d != null)
                {
                    return d;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Gets the margin size
        /// </summary>
        public string Margin
        {
            get
            {
                dynamic d = this.Beams.GetPropertyValue("Margin").ReadProperty();
                if (d != null)
                {
                    return d;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Gets the padding size
        /// </summary>
        public string Padding
        {
            get
            {
                dynamic d = this.Beams.GetPropertyValue("Padding").ReadProperty();
                if (d != null)
                {
                    return d;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Gets or sets a name of this ux
        /// </summary>
        public string Name
        {
            get { return this.Get(nameName, string.Empty); }
            set { this.Set(nameName, value); }
        }

        /// <summary>
        /// Gets the value of this control
        /// </summary>
        public dynamic Value
        {
            get
            {
                return this;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Children UX
        /// </summary>
        public List<IUXObject> Children
        {
            get { return this.Get(childrenName); }
        }

        /// <summary>
        /// Parent UX
        /// </summary>
        public IUXObject Parent
        {
            get
            {
                return this.Get(parentName);
            }
            set
            {
                this.Set(parentName, value);
            }
        }

        /// <summary>
        /// Gets the interactive beam
        /// </summary>
        public BeamConnections.InteractiveBeam Beams
        {
            get { return this.Get(beamName, new BeamConnections.InteractiveBeam(this)); }
        }

        /// <summary>
        /// Gets the html source
        /// </summary>
        public StringBuilder HtmlSource
        {
            get { return this.Get(htmlSourceName); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Add a new string into HTML destination stream
        /// </summary>
        /// <param name="s">text</param>
        public void Add(string s)
        {
            this.HtmlSource.Append(s);
        }

        /// <summary>
        /// Add a new control in children list
        /// </summary>
        /// <param name="obj">ux object</param>
        public void Add(IUXObject obj)
        {
            obj.Parent = this;
            this.Children.Add(obj);
        }

        /// <summary>
        /// Write default text into the stream
        /// </summary>
        /// <param name="sw">stream</param>
        public virtual void Write(StreamWriter sw)
        {
            sw.Write(this.HtmlSource.ToString());
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
            if (this.Exists(actionName) && this.Get(actionName) != null)
                this.Get(actionName).Invoke();
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
            if (Parent != null)
                return Parent.GetUXWindow();
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
            this.Set(actionName, a);
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
                if (x.Key == childrenName)
                {
                    List<IUXObject> objects = new List<IUXObject>();
                    foreach (Marshalling.IMarshalling a in x.Value)
                    {
                        objects.Add(a.Clone() as IUXObject);
                    }
                    c.Set(childrenName, objects);
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
            // taille de la fenêtre
            uint width, height;
            width = Convert.ToUInt32(hash["Width"].Value);
            height = Convert.ToUInt32(hash["Height"].Value);
            // couleurs
            string backColor, foreColor;
            backColor = hash["BackColor"].Value;
            foreColor = hash["ForeColor"].Value;
            // bords, marge, intervalle (format l,r,t,b)
            string border, marge, interval;
            border = hash["Border"].Value;
            marge = hash["Margin"].Value;
            interval = hash["Padding"].Value;

            // enregistrement des elements
            this.Beams.SetPropertyValues(new List<KeyValuePair<string, Beam>> {
                new KeyValuePair<string, Beam>("Width", Beam.Register("width", this, width)),
                new KeyValuePair<string, Beam>("Height", Beam.Register("height", this, height)),
                new KeyValuePair<string, Beam>("BackColor", Beam.Register("background-color", this, backColor)),
                new KeyValuePair<string, Beam>("ForeColor", Beam.Register("color", this, foreColor)),
                new KeyValuePair<string, Beam>("Border", Beam.Register("border", this, border)),
                new KeyValuePair<string, Beam>("Margin", Beam.Register("margin", this, marge)),
                new KeyValuePair<string, Beam>("Padding", Beam.Register("padding", this, interval))
            }.ToArray());
        }

        #endregion

        #region Static Methods

        public static void CreateUXControl(UXControl parent, Marshalling.MarshallingList childs, Marshalling.MarshallingList uiChilds)
        {
            int index = 0;
            foreach (Marshalling.MarshallingHash h in childs.Values)
            {
                string typeName = h["type"].Value;
                string name = h["name"].Value;
                switch (typeName)
                {
                    case "UXReadOnlyText":
                        UXReadOnlyText.CreateUXReadOnlyText(parent, h, uiChilds.Values.ElementAt(index) as Marshalling.MarshallingHash);
                        break;
                }
                ++index;
            }

        }

        #endregion

    }
}
