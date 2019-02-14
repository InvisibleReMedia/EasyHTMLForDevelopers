using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UXFramework
{
    /// <summary>
    /// An item in a tree
    /// </summary>
    public class UXTreeItem : UXControl
    {
        #region Fields

        private string text;
        private UXTree subItems;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="t">static text</param>
        public UXTreeItem(string t, Marshalling.MarshallingList subItems)
        {
            this.text = t;
            this.Add(this.text);
            this.subItems = new UXTree(t);
            this.subItems.Bind(subItems);
            this.Name = this.subItems.Name;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the readonly text content
        /// </summary>
        public string Text
        {
            get { return this.text; }
            set { this.text = value; }
        }

        /// <summary>
        /// Gets sub items
        /// </summary>
        public UXTree SubItems
        {
            get
            {
                return this.subItems;
            }
        }

        #endregion
    
    }
}
