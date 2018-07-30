﻿using System;
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
        /// <summary>
        /// Disposition window
        /// </summary>
        private Library.Disposition disp;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public UXWindow()
        {
            this.Parent = null;
            this.disp = Library.Disposition.CENTER;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the file name to where write html to print
        /// </summary>
        public string FileName
        {
            get { return this.Name + ".html"; }
        }

        /// <summary>
        /// Gets or sets the disposition page
        /// </summary>
        public Library.Disposition Disposition
        {
            get { return this.disp; }
            set { this.disp = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Navigate function (main function)
        /// </summary>
        /// <param name="www">web browser control</param>
        public override void Navigate(WebBrowser www)
        {
            webReference = new WeakReference(www);
            renderer.RenderControl(this);
            www.DocumentCompleted += WWW_DocumentCompleted;
            www.Navigate(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this.FileName));
        }

        /// <summary>
        /// Event for document completed
        /// </summary>
        /// <param name="sender">source</param>
        /// <param name="e">args</param>
        private void WWW_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WebBrowser www = (WebBrowser)sender;
            RecursiveConnect(this);
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
