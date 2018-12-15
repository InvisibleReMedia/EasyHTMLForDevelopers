using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UXFramework
{
    public class UXCombo : UXControl
    {

        #region Fields

        /// <summary>
        /// Collection of name/value
        /// </summary>
        private NameValueCollection list;
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
        /// <param name="options">name/value iteration</param>
        public UXCombo(params KeyValuePair<string, string>[] options)
        {
            this.list = new NameValueCollection();
            foreach (KeyValuePair<string, string> kv in options)
            {
                this.list.Add(kv.Key, kv.Value);
            }
            this.Add("<select>");
            foreach (string key in this.list.Keys)
            {
                this.Add("<option value='" + key + "'>" + this.list[key] + "</option>");
            }
            this.Add("</select>");
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

        #region Methods

        /// <summary>
        /// Get the list of options
        /// </summary>
        /// <returns>collection name/value</returns>
        public NameValueCollection GetList()
        {
            return this.list;
        }

        /// <summary>
        /// Rewrite the list
        /// </summary>
        /// <param name="options">name/value iteration</param>
        public void SetList(params KeyValuePair<string, string>[] options)
        {
            this.list.Clear();
            foreach (KeyValuePair<string, string> kv in options)
            {
                this.list.Add(kv.Key, kv.Value);
            }
            this.HtmlSource.Clear();
            this.Add("<select id='cmb1'>");
            foreach (string key in this.list.Keys)
            {
                this.Add("<option value='" + key + "'>" + this.list[key] + "</option>");
            }
            this.Add("</select>");
        }

        #endregion

        #region Overriden Methods

        /// <summary>
        /// Connect for interoperability C#/Web
        /// </summary>
        public override void Connect(WebBrowser web)
        {
            base.Connect(web);
            HtmlElement e = web.Document.GetElementById(this.Id);
            if (e != null)
            {
                e.Click += UXCombo_Click;
            }

        }

        /// <summary>
        /// Delegate to click
        /// </summary>
        /// <param name="sender">html element</param>
        /// <param name="e">args</param>
        private void UXCombo_Click(object sender, HtmlElementEventArgs e)
        {
            this.UpdateOne();
        }

        #endregion

    }
}
