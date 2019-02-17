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
    public class UXEditableText : UXControl
    {

        #region Fields

        /// <summary>
        /// Text
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
        public UXEditableText(string id, string t)
        {
            this.Set(idName, id);
            this.Set(textName, t);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the text content
        /// </summary>
        public string Text
        {
            get { return this.Get(textName, string.Empty); }
            set { this.Set(textName, value); }
        }

        /// <summary>
        /// Gets or sets the id object
        /// </summary>
        public string Id
        {
            get { return this.Get(idName, string.Empty); }
            set { this.Set(idName, value); }
        }

        #endregion

        #region Overriden Methods

        /// <summary>
        /// Connect for interoperability C#/Web
        /// </summary>
        /// <param name="web">web browser</param>
        public override void Connect(WebBrowser web)
        {
            base.Connect(web);
            HtmlElement e = web.Document.GetElementById(this.Id);
            if (e != null)
            {
                e.LostFocus += UXEditableText_LostFocus;
            }

        }

        /// <summary>
        /// Disconnect for interoperability C#/Web
        /// </summary>
        public override void Disconnect(WebBrowser web)
        {
            base.Disconnect(web);
            HtmlElement e = web.Document.GetElementById(this.Id);
            if (e != null)
            {
                e.Click -= UXEditableText_LostFocus;
            }

        }

        /// <summary>
        /// Delegate to lost focus
        /// </summary>
        /// <param name="sender">html element</param>
        /// <param name="e">args</param>
        private void UXEditableText_LostFocus(object sender, HtmlElementEventArgs e)
        {
            this.Text = ((HtmlElement)sender).InnerText;
            this.UpdateOne();
        }

        /// <summary>
        /// Constructs ui properties
        /// </summary>
        /// <param name="m">data</param>
        /// <param name="ui">ui properties</param>
        public override void Construct(Marshalling.IMarshalling m, Marshalling.IMarshalling ui)
        {
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
        /// Create editable text
        /// </summary>
        /// <param name="data">data to show</param>
        /// <param name="ui">ui properties</param>
        public static UXEditableText CreateUXEditableText(Marshalling.MarshallingHash data, Marshalling.MarshallingHash ui)
        {
            UXEditableText ux = new UXEditableText(data["Id"].Value, data["Text"].Value);
            ux.Construct(data, ui);
            return ux;
        }

        #endregion

    }
}
