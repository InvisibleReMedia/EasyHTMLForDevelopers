﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UXFramework
{
    /// <summary>
    /// One cell in a row
    /// </summary>
    public class UXCell : UXControl
    {

        #region Fields

        private static readonly string rowIndexName = "rowIndex";
        private static readonly string cellIndexName = "cellIndex";

        #endregion

        #region Default Constructor

        public UXCell(uint rowIndex, uint cellIndex)
        {
            this.Set(rowIndexName, rowIndex);
            this.Set(cellIndexName, cellIndex);
        }


        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the row index
        /// </summary>
        public uint RowIndex
        {
            get { return this.Get(rowIndexName, 0); }
            set { this.Set(rowIndexName, value); }
        }

        /// <summary>
        /// Gets or sets the cell index
        /// </summary>
        public uint CellIndex
        {
            get { return this.Get(cellIndexName, 0); }
            set { this.Set(cellIndexName, value); }
        }


        #endregion

    }
}