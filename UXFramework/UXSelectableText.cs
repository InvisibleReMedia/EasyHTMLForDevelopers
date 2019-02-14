﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UXFramework
{
    public class UXSelectableText : UXReadOnlyText
    {

        #region Fields

        /// <summary>
        /// Id
        /// </summary>
        private string id;
        /// <summary>
        /// Reference index for selection
        /// </summary>
        private int index;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="t">text</param>
        public UXSelectableText(string id, string t, int refIndex)
            : base(t)
        {
            this.id = id;
            this.index = refIndex;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets id
        /// </summary>
        public string Id
        {
            get { return this.id; }
            set { this.id = value; }
        }

        public int RefIndex
        {
            get { return this.index; }
            set { this.index = value; }
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
                e.MouseEnter += UXSelectableText_MouseEnter;
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
                e.Click -= UXSelectableText_MouseEnter;
            }

        }

        /// <summary>
        /// Delegate to click
        /// </summary>
        /// <param name="sender">html element</param>
        /// <param name="e">args</param>
        private void UXSelectableText_MouseEnter(object sender, HtmlElementEventArgs e)
        {
            this.UpdateOne();
        }

        #endregion
    }
}