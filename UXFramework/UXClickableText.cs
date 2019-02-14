﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        /// <param name="t">static text</param>
        public UXClickableText(string t)
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

    }
}