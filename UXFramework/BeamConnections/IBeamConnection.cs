using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace UXFramework.BeamConnections
{
    /// <summary>
    /// Interface for relations between UX and its implementation
    /// </summary>
    public interface IBeamConnection
    {
        /// <summary>
        /// Gets a property value of this beam
        /// </summary>
        /// <param name="name">property name</param>
        /// <returns>beam</returns>
        Beam GetPropertyValue(string name);
        /// <summary>
        /// Gets following property values
        /// </summary>
        /// <param name="names">array of names</param>
        /// <returns>beams</returns>
        Beam[] GetPropertyValues(params string[] names);
        /// <summary>
        /// Gets all properties
        /// </summary>
        /// <returns>all properties</returns>
        Beam[] GetAllProperties();
        /// <summary>
        /// Sets a property value of this beam
        /// </summary>
        /// <param name="name">property name</param>
        /// <param name="value">beam value</param>
        /// <returns>true if succeedeed</returns>
        void SetPropertyValue(string name, Beam value);
        /// <summary>
        /// Sets property values regarding properties of this beam
        /// </summary>
        /// <param name="dict">array of key pair value</param>
        /// <returns>true if succeedeed</returns>
        void SetPropertyValues(KeyValuePair<string,Beam>[] dict);

        /// <summary>
        /// Update all registered properties of this beam
        /// for source
        /// </summary>
        /// <param name="uxObj">UX object</param>
        void UpdateSource(IUXObject uxObj);

        /// <summary>
        /// Update all registered properties of this beam
        /// for renderer
        /// </summary>
        /// <param name="target">target renderer</param>
        void UpdateTarget(IUXRenderer target);

        /// <summary>
        /// Converts data into JSON for computer graphics
        /// </summary>
        void ToJSON();

        event EventHandler ToSource;
        event EventHandler ToTarget;
    }
}
