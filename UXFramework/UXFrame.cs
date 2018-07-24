using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UXFramework
{
    public class UXFrame : UXControl
    {

        #region Fields

        /// <summary>
        /// Frame source
        /// </summary>
        private string frameSrc;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public UXFrame(string src)
        {
            this.frameSrc = src;
            this.Add("<div id='frm1'></div>");
            this.Add("<script language='javascript' type='text/javascript'>");
            this.Add("//<!CDATA[[" + Environment.NewLine);
            this.Add("function updateFrm1(s) {  var x = document.getElementById('frm1'); x.InnerHTML = s; }");
            this.Add("//]]>" + Environment.NewLine);
            this.Add("</script>");
            this.GetWebBrowser().Document.InvokeScript("updateFrm1", new object[] { this.frameSrc });
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the frame source
        /// </summary>
        public string FrameSource
        {
            get { return this.frameSrc; }
            set { this.frameSrc = value; }
        }

        #endregion

        #region Overriden Methods

        public override void UpdateOne()
        {
            this.GetWebBrowser().Document.InvokeScript("updateFrm1", new object[] { this.frameSrc });
        }

        #endregion

    }
}
