using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace UXFramework.BeamConnections
{
    /// <summary>
    /// Is a beam
    /// </summary>
    public class Beam
    {

        #region Fields

        /// <summary>
        /// Property name
        /// </summary>
        private string propName;
        /// <summary>
        /// Handler
        /// </summary>
        private dynamic handler;
        /// <summary>
        /// Source object
        /// </summary>
        private IUXObject source;
        /// <summary>
        /// Event
        /// </summary>
        private event EventHandler raiseCode;

        #endregion

        #region Properties

        /// <summary>
        /// Name of this property
        /// </summary>
        public string PropertyName
        {
            get { return this.propName; }
        }

        /// <summary>
        /// Source UX
        /// </summary>
        public IUXObject Source
        {
            get { return this.source; }
        }

        /// <summary>
        /// Add or remove event
        /// </summary>
        public event EventHandler RaiseCode
        {
            add
            {
                this.raiseCode += value;
            }
            remove
            {
                this.raiseCode -= value;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Read value of the designed property of this beam
        /// </summary>
        /// <returns>value</returns>
        public dynamic ReadProperty()
        {
            return handler;
        }

        /// <summary>
        /// Write a property value into the designed property of this beam
        /// </summary>
        /// <param name="value">value to store</param>
        public void WriteProperty(dynamic value)
        {
            handler = value;
        }

        /// <summary>
        /// Register a new beam
        /// </summary>
        /// <param name="name">name of this beam</param>
        /// <param name="source">source object</param>
        /// <param name="defaultValue">default value</param>
        public static Beam Register(string name, IUXObject source, dynamic defaultValue) {

            Beam b = new Beam();
            b.propName = name;
            b.source = source;
            b.WriteProperty(defaultValue);
            return b;
        }

        #endregion
    }
}
