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
    public class UXButton : UXControl
    {

        #region Fields

        /// <summary>
        /// Text for button
        /// </summary>
        public static readonly string textButtonName = "textButton";
        /// <summary>
        /// Id
        /// </summary>
        public static readonly string idName = "idButton";

        /// <summary>
        /// Colors for buttons
        /// </summary>
        public static readonly string rollBackColorName = "rollBackColor";
        public static readonly string rollColorName = "rollColor";
        public static readonly string clickBorderColorName = "clickBorderColorName";

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public UXButton()
        {
        }

        /// <summary>
        /// Constructor with init
        /// <param name="id">id</param>
        /// <param name="title">title</param>
        /// </summary>
        public UXButton(string id, string title)
        {
            this.Id = id;
            this.ButtonText = title;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the Id object
        /// </summary>
        public string Id
        {
            get { return this.Get(idName, "id"); }
            set { this.Set(idName, value); }
        }

        /// <summary>
        /// Gets or sets the button text
        /// </summary>
        public string ButtonText
        {
            get { return this.Get(textButtonName, "Button"); }
            set { this.Set(textButtonName, value); }
        }

        /// <summary>
        /// Gets or sets the roll back color
        /// </summary>
        public string RollBackColor
        {
            get { return this.Get(rollBackColorName, "Gray3"); }
            set { this.Set(rollBackColorName, value); }
        }

        /// <summary>
        /// Gets or sets the roll color
        /// </summary>
        public string RollColor
        {
            get { return this.Get(rollColorName, "Gray4"); }
            set { this.Set(rollColorName, value); }
        }

        /// <summary>
        /// Gets or sets the click border color
        /// </summary>
        public string ClickBorderColor
        {
            get { return this.Get(clickBorderColorName, "Gray1"); }
            set { this.Set(clickBorderColorName, value); }
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
                e.Click += UXButton_Click;
            }

        }

        /// <summary>
        /// Disconnect for interoperability C#/Web
        /// </summary>
        /// <param name="web">web browser</param>
        public override void Disconnect(WebBrowser web)
        {
            base.Disconnect(web);
            HtmlElement e = web.Document.GetElementById(this.Id);
            if (e != null)
            {
                e.Click -= UXButton_Click;
            }
        }

        /// <summary>
        /// Delegate to click
        /// </summary>
        /// <param name="sender">html element</param>
        /// <param name="e">args</param>
        private void UXButton_Click(object sender, HtmlElementEventArgs e)
        {
            this.UpdateOne();
        }

        public override void Construct(Marshalling.IMarshalling m, Marshalling.IMarshalling ui)
        {
            base.Construct(m, ui);
            Marshalling.MarshallingHash hash = ui as Marshalling.MarshallingHash;
            // couleurs
            string rollBackColor, rollColor, clickBorderColor;
            rollBackColor = hash["RollBackColor"].Value;
            rollColor = hash["RollColor"].Value;
            clickBorderColor = hash["ClickBorderColor"].Value;
            // enregistrement des elements
            this.Beams.SetPropertyValues(new List<KeyValuePair<string, Beam>> {
                new KeyValuePair<string, Beam>("RollBackColor", Beam.Register("rollBackColor", this, rollBackColor)),
                new KeyValuePair<string, Beam>("RollColor", Beam.Register("rollColor", this, rollColor)),
                new KeyValuePair<string, Beam>("ClickBorderColor", Beam.Register("clickBorderColor", this, clickBorderColor))
            }.ToArray());
        }
        
        #endregion

        #region Static Methods

        /// <summary>
        /// Create a box
        /// </summary>
        /// <param name="data">data to show</param>
        /// <param name="ui">ui properties</param>
        public static UXButton CreateUXButton(Marshalling.MarshallingHash data, Marshalling.MarshallingHash ui)
        {
            UXButton button = new UXButton(data["Id"].Value, data["Text"].Value);
            button.Construct(data, ui);
            return button;
        }


        #endregion
    }
}
