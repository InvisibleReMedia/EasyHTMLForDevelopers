using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TomB.Util.JSON;

namespace UXFramework.BeamConnections
{
    /// <summary>
    /// Class to implements an interactive beam
    /// between renderer and ux
    /// </summary>
    public class InteractiveBeam : UXControl, IBeamConnection
    {

        #region Constructor

        public InteractiveBeam(IUXObject x)
        {
            this.SetPropertyValue("Background", BeamConnections.Beam.Register("background-color", x, VisualIdentity.Current.BackgroundColor));
            this.SetPropertyValue("Foreground", BeamConnections.Beam.Register("color", x, VisualIdentity.Current.ForegroundColor));
            this.SetPropertyValue("BorderColor", BeamConnections.Beam.Register("border-color", x, VisualIdentity.Current.BorderColor));
            this.SetPropertyValue("FontName", BeamConnections.Beam.Register("font-family", x, VisualIdentity.Current.Font));
            this.SetPropertyValue("FontSize", BeamConnections.Beam.Register("font-size", x, VisualIdentity.Current.FontSize));
            this.SetPropertyValue("BorderWidth", BeamConnections.Beam.Register("border-width", x, VisualIdentity.Current.BorderSize.Width));
            this.SetPropertyValue("BorderHeight", BeamConnections.Beam.Register("border-height", x, VisualIdentity.Current.BorderSize.Height));
            this.SetPropertyValue("MarginWidth", BeamConnections.Beam.Register("margin-width", x, VisualIdentity.Current.MarginSize.Width));
            this.SetPropertyValue("MarginHeight", BeamConnections.Beam.Register("margin-height", x, VisualIdentity.Current.MarginSize.Height));
            this.SetPropertyValue("PaddingWidth", BeamConnections.Beam.Register("padding-width", x, VisualIdentity.Current.PaddingSize.Width));
            this.SetPropertyValue("PaddingHeight", BeamConnections.Beam.Register("padding-height", x, VisualIdentity.Current.PaddingSize.Height));
        }

        #endregion

        /// <summary>
        /// Gets a property value of this beam
        /// </summary>
        /// <param name="name">property name</param>
        /// <returns>beam</returns>
        public Beam GetPropertyValue(string name)
        {
            return this.Get(name, new Beam());
        }

        /// <summary>
        /// Gets following property values
        /// </summary>
        /// <param name="names">array of names</param>
        /// <returns>beam values</returns>
        public Beam[] GetPropertyValues(params string[] names)
        {
            List<Beam> list = new List<Beam>();
            foreach(string s in names) {
                list.Add(this.Get(s, new Beam()));
            }
            return list.ToArray();
        }

        /// <summary>
        /// Gets all property values
        /// </summary>
        /// <returns>beam values</returns>
        public Beam[] GetAllProperties()
        {
            List<Beam> list = new List<Beam>();
            foreach (string key in this.Keys)
            {
                dynamic r = this.Get(key);
                if (r is Beam)
                    list.Add(r);
            }
            return list.ToArray();
        }

        /// <summary>
        /// Sets a property value of this beam
        /// </summary>
        /// <param name="name">property name</param>
        /// <param name="value">beam value</param>
        /// <returns>true if succeedeed</returns>
        public void SetPropertyValue(string name, Beam value)
        {
            this.Set(name, value);
        }

        /// <summary>
        /// Sets property values regarding properties of this beam
        /// </summary>
        /// <param name="dict">array of key pair value</param>
        /// <returns>true if succeedeed</returns>
        public void SetPropertyValues(KeyValuePair<string, Beam>[] dict)
        {
        }

        /// <summary>
        /// Update all registered properties of this beam
        /// for source
        /// </summary>
        /// <param name="uxObj">UX object</param>
        public void UpdateSource(IUXObject uxObj)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Update all registered properties of this beam
        /// for renderer
        /// </summary>
        /// <param name="target">target renderer</param>
        public void UpdateTarget(IUXRenderer target)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Converts data into JSON for computer graphics
        /// </summary>
        public void ToJSON()
        {
            IJSONDocument doc = JSONDocument.CreateDocument();
            IJSONItemArray arr = doc.CreateItemArray();

            foreach (Beam b in this.GetAllProperties())
            {
                IJSONItem o = b.ToJSON(doc);
                arr.Add(o);
            }
        }

        public event EventHandler ToSource;

        public event EventHandler ToTarget;

    }
}
