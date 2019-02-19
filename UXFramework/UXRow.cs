using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UXFramework
{
    /// <summary>
    /// Bring a row in a table
    /// </summary>
    public class UXRow : UXControl
    {

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public UXRow()
        {
        }

        /// <summary>
        /// Creates elements
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="e">elements</param>
        public UXRow(string name, IDictionary<string, dynamic> e)
            : base(name, e)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the column count
        /// </summary>
        public int ColumnCount
        {
            get { return this.Get("ColumnCount").Value; }
        }

        /// <summary>
        /// Gets Id
        /// </summary>
        public string Id
        {
            get
            {
                if (this.Exists("Id"))
                {
                    return this.Get("Id").Value;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// Gets if this row is selectable
        /// </summary>
        public bool IsSelectable
        {
            get
            {
                if (this.Exists("IsSelectable"))
                {
                    return this.Get("IsSelectable").Value;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Gets if this row is clickable
        /// </summary>
        public bool IsClickable
        {
            get
            {
                if (this.Exists("IsClickable"))
                {
                    return this.Get("IsClickable").Value;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Gets the background ground selectable
        /// </summary>
        public string BackgroundSelectable
        {
            get
            {
                if (this.Exists("Background-Selectable"))
                {
                    return this.Get("Background-Selectable").Value;
                }
                else
                {
                    return "";
                }
            }
        }

        /// <summary>
        /// Gets the background ground clickable
        /// </summary>
        public string BackgroundClickable
        {
            get
            {
                if (this.Exists("Background-Clickable"))
                {
                    return this.Get("Background-Clickable").Value;
                }
                else
                {
                    return "";
                }
            }
        }

        public override void Connect(System.Windows.Forms.WebBrowser web)
        {
            base.Connect(web);
            HtmlElement e = web.Document.GetElementById("serverSideHandler");
            if (e != null)
            {
                e.Click += UXRow_Click;
            }
        }

        public override void Disconnect(WebBrowser web)
        {
            base.Disconnect(web);
            HtmlElement e = web.Document.GetElementById("serverSideHandler");
            if (e != null)
            {
                e.Click -= UXRow_Click;
            }
        }

        /// <summary>
        /// Event raised
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">args</param>
        private void UXRow_Click(object sender, HtmlElementEventArgs e)
        {
            HtmlElement h = sender as HtmlElement;
            if (h.GetAttribute("notif") == "row" && h.GetAttribute("data") == this.Id)
                this.UpdateOne();
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Create UXRow
        /// </summary>
        /// <param name="data">data hash</param>
        /// <param name="ui">ui properties</param>
        /// <returns>row</returns>
        public static UXRow CreateUXRow(Marshalling.MarshallingHash data, Marshalling.MarshallingHash ui)
        {
            UXRow row = new UXRow();
            row.Bind(data);
            row.Bind(ui);
            foreach (Marshalling.IMarshalling m in row.GetProperty("childs").Values)
            {
                row.Add(m.Value);
            }
            return row;
        }

        /// <summary>
        /// Create UXRow
        /// </summary>
        /// <param name="f">function to enter data</param>
        /// <returns>marshalling</returns>
        public static UXRow CreateUXRow(string name, Func<IDictionary<string, dynamic>> f)
        {
            return new UXRow(name, f());
        }

        #endregion

    }
}
