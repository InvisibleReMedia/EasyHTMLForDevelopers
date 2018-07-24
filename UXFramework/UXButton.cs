using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UXFramework
{
    public class UXButton : UXControl
    {

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public UXButton()
        {
            this.Add("<input type='button' value='OK' name='btn' id='btn1'/>");
        }

        #endregion

        #region Overriden Methods

        /// <summary>
        /// Connect for interoperability C#/Web
        /// </summary>
        public override void Connect()
        {
            base.Connect();
            HtmlElement e = this.GetWebBrowser().Document.GetElementById("btn1");
            if (e != null)
            {
                e.Click += UXButton_Click;
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

        #endregion

    }
}
