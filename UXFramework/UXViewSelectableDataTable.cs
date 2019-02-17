using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UXFramework
{
    /// <summary>
    /// Data table selectable
    /// </summary>
    public class UXViewSelectableDataTable : UXViewDataTable
    {

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="columnSize">column size</param>
        /// <param name="lineSize">line size</param>
        /// <param name="id">id</param>
        public UXViewSelectableDataTable(int columnSize, int lineSize, string id) : base(columnSize, lineSize, id)
        {
        }


        #endregion

    }
}
