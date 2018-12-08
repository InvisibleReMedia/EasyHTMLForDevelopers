using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BeamConnections
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
        /// <returns>property value</returns>
        string GetPropertyValue(string name);
        /// <summary>
        /// Gets following property values
        /// </summary>
        /// <param name="names">array of names</param>
        /// <returns>property values</returns>
        string[] GetPropertyValues(params string[] names);
        /// <summary>
        /// Sets a property value of this beam
        /// </summary>
        /// <param name="name">property name</param>
        /// <returns>true if succeedeed</returns>
        bool SetPropertyValue(string name);
        /// <summary>
        /// Sets property values regarding properties of this beam
        /// </summary>
        /// <param name="dict">array of key pair value</param>
        /// <returns>true if succeedeed</returns>
        bool SetPropertyValues(KeyValuePair<string,string>[] dict);

        void UpdateSource();
    }
}
