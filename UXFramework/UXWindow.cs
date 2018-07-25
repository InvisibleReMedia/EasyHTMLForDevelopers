using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UXFramework
{
    public class UXWindow : UXControl
    {

        #region Fields

        /// <summary>
        /// Weak reference of the current browser
        /// </summary>
        private WeakReference webReference;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public UXWindow()
        {
            this.Parent = null;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Navigate function (main function)
        /// </summary>
        /// <param name="www">web browser control</param>
        public override void Navigate(WebBrowser www)
        {
            using (FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "source.html", FileMode.Create, FileAccess.Write, FileShare.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    Write(sw);
                    foreach (IUXObject ux in this.Children)
                    {
                        ux.Write(sw);
                    }
                    sw.Close();
                }
                fs.Close();
            }
            www.DocumentCompleted += WWW_DocumentCompleted;
            www.Navigate(AppDomain.CurrentDomain.BaseDirectory + "source.html");
        }

        /// <summary>
        /// Event for document completed
        /// </summary>
        /// <param name="sender">source</param>
        /// <param name="e">args</param>
        private void WWW_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WebBrowser www = (WebBrowser)sender;
            webReference = new WeakReference(www);
            Connect();
            foreach (IUXObject ux in this.Children)
            {
                ux.Connect();
            }
            www.DocumentCompleted -= WWW_DocumentCompleted;
        }

        /// <summary>
        /// Get the current web browser
        /// </summary>
        /// <returns>web browser control</returns>
        public override WebBrowser GetWebBrowser()
        {
            if (webReference != null && webReference.IsAlive)
            {
                return (WebBrowser)webReference.Target;
            }
            else
            {
                throw new NullReferenceException("WebBrowser lost!");
            }
        }

        #endregion

    }
}
