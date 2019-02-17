using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UXFramework.BeamConnections;

namespace UXFramework
{
    /// <summary>
    /// Image clickable
    /// </summary>
    public class UXClickableImage : UXControl
    {

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public UXClickableImage()
        {
        }

        /// <summary>
        /// Creates elements
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="e">elements</param>
        public UXClickableImage(string name, IDictionary<string, dynamic> e)
            : base(name, e)
        {
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Create clickable image
        /// </summary>
        /// <param name="data">data to show</param>
        /// <param name="ui">ui properties</param>
        public static UXClickableImage CreateUXClickableImage(Marshalling.MarshallingHash data, Marshalling.MarshallingHash ui)
        {
            UXClickableImage ux = new UXClickableImage();
            ux.Bind(data);
            ux.Bind(ui);
            return ux;
        }

        /// <summary>
        /// Create UXClickableImage
        /// </summary>
        /// <param name="f">function to enter data</param>
        /// <returns>marshalling</returns>
        public static UXClickableImage CreateUXClickableImage(string name, Func<IDictionary<string, dynamic>> f)
        {
            return new UXClickableImage(name, f());
        }

        #endregion
    }
}
