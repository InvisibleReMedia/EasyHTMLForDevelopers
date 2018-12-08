using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UXFramework.BeamConnections
{
    /// <summary>
    /// Class to implements an interactive beam
    /// between renderer and ux
    /// </summary>
    public class InteractiveBeam : Marshalling.PersistentDataObject, IBeamConnection
    {

        /// <summary>
        /// Gets a property value of this beam
        /// </summary>
        /// <param name="name">property name</param>
        /// <returns>beam</returns>
        public Beam GetPropertyValue(string name)
        {
            return this.Get(name, string.Empty);
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
                list.Add(this.Get(s, string.Empty));
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
                list.Add(this.Get(key));
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

        public event EventHandler ToSource;

        public event EventHandler ToTarget;

    }
}
