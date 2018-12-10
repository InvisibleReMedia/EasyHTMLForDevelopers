using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UXFramework
{
    /// <summary>
    /// View for a data table
    /// </summary>
    public class UXViewDataTable : UXTable
    {

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="columnSize">column size</param>
        /// <param name="lineSize">line size</param>
        /// <param name="id">id</param>
        public UXViewDataTable(uint columnSize, uint lineSize, string id) : base(columnSize, lineSize, id)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Bind sequence
        /// </summary>
        /// <param name="a">specific bind function</param>
        public override void Bind(Marshalling.IMarshalling m)
        {

            if (m is Marshalling.MarshallingList)
            {
                Marshalling.MarshallingList list = m as Marshalling.MarshallingList;
                // construction de la table
                uint count = Convert.ToUInt32(list.Count);
                if (count > 0)
                {
                    {
                        // construire le header
                        UXRow newRow = new UXRow(0, this.ColumnCount);
                        int headerIndex = 0;
                        Marshalling.MarshallingHash h = list[0] as Marshalling.MarshallingHash;
                        foreach (string key in h.HashKeys)
                        {
                            UXCell cell = new UXCell(0, (uint)headerIndex);
                            UXReadOnlyText t = new UXReadOnlyText(h[key].Name.ToString());
                            newRow.Children.Add(cell);
                            cell.Children.Add(t);
                            ++headerIndex;
                        }
                        this.Children.Add(newRow);
                    }

                    for (int index = 0; index < count; ++index)
                    {
                        // element de la liste
                        Marshalling.MarshallingHash h = list[index] as Marshalling.MarshallingHash;

                        // construction d'une nouvelle ligne
                        UXRow row = new UXRow((uint)index, this.ColumnCount);
                        row.Bind(h);
                        this.Children.Add(row);
                    }
                }
            }
            else if (m is Marshalling.MarshallingHash)
            {
                Marshalling.MarshallingHash hash = m as Marshalling.MarshallingHash;
                // construction de la table
                uint count = Convert.ToUInt32(hash.HashKeys.Count());
                if (count > 0)
                {
                    {
                        // construire le header
                        UXRow newRow = new UXRow(0, this.ColumnCount);
                        int headerIndex = 0;
                        foreach (string key in hash.HashKeys)
                        {
                            UXReadOnlyText t = new UXReadOnlyText(hash[key].Name.ToString());
                            newRow.Children.Add(t);
                            ++headerIndex;
                        }
                        this.Children.Add(newRow);
                    }

                    for (int index = 0; index < count; ++index)
                    {
                        // construction d'une nouvelle ligne
                        UXRow row = new UXRow((uint)index, this.ColumnCount);
                        row.Bind(hash[hash.HashKeys.ElementAt(index)]);
                        this.Children.Add(row);
                    }
                }
            }

        }

        #endregion
    }
}
