using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UXFramework.BeamConnections;

namespace UXFramework
{
    /// <summary>
    /// UX with a clickable text
    /// </summary>
    public class UXClickableText : UXControl
    {

        #region Fields

        /// <summary>
        /// Text to print
        /// </summary>
        public static readonly string textName = "text";
        /// <summary>
        /// Id
        /// </summary>
        public static readonly string idName = "id";

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="t">static text</param>
        public UXClickableText(string id, string t)
        {
            this.Set(textName, t);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the readonly text content
        /// </summary>
        public string Text
        {
            get { return this.Get(textName); }
            set { this.Set(textName, value); }
        }

        /// <summary>
        /// Gets or sets the Id object
        /// </summary>
        public string Id
        {
            get { return this.Get(idName); }
            set { this.Set(idName, value); }
        }

        #endregion

        #region Methods

        public override void Construct(Marshalling.IMarshalling m, Marshalling.IMarshalling ui)
        {
            base.Construct(m, ui);
            Marshalling.MarshallingHash hash = ui as Marshalling.MarshallingHash;
            TextProperties tp = hash["properties"].Value;
            // enregistrement des elements
            this.Beams.SetPropertyValues(new List<KeyValuePair<string, Beam>> {
                new KeyValuePair<string, Beam>("properties", Beam.Register("properties", this, tp))
            }.ToArray());
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Create clickable text
        /// </summary>
        /// <param name="data">data to show</param>
        /// <param name="ui">ui properties</param>
        public static UXClickableText CreateUXClickableText(Marshalling.MarshallingHash data, Marshalling.MarshallingHash ui)
        {
            UXClickableText ux = new UXClickableText(data["Id"].Value, data["Text"].Value);
            ux.Construct(data, ui);
            return ux;
        }

        #endregion
    
    }
}
