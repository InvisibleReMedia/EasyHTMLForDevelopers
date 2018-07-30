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

        #region Fields

        /// <summary>
        /// Data list
        /// </summary>
        private Marshalling.MarshallingList list;
        /// <summary>
        /// Id
        /// </summary>
        private string id;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="id">id</param>
        public UXViewDataTable(string id)
        {
            this.id = id;
            this.list = new Marshalling.MarshallingList("projectList");
        }

        #endregion

        #region Methods

        /// <summary>
        /// Bind sequence
        /// </summary>
        /// <param name="a">specific bind function</param>
        public void Bind(Action<Marshalling.MarshallingList> a)
        {
            a(list);

            // construction de la table
            uint count = Convert.ToUInt32(this.list.Count);
            if (count > 0)
            {
                this.LineCount = count;
                this.ColumnCount = Convert.ToUInt32((this.list[0] as Marshalling.MarshallingHash).HashKeys.Count());
                // construire le header
                Marshalling.MarshallingHash h = this.list[0] as Marshalling.MarshallingHash;

                int headerIndex = 0;
                foreach(string key in h.HashKeys)
                {
                    SizeArgs s = new SizeArgs((uint)headerIndex, 0, 60, 20, 1, 1, headerIndex, 0);
                    UXReadOnlyText t = new UXReadOnlyText(h[key].Name);
                    s.Options(Library.Disposition.CENTER, Library.EnumConstraint.FIXED, Library.EnumConstraint.FIXED, new Library.CSSColor("white"), new Library.CSSColor("black"), new Library.CSSColor("black"), 2, t);
                    s.isValid = true;
                    this.HorizontalCustomization[headerIndex] = s;
                    ++headerIndex;
                }
                for (int index = 0; index < count; ++index)
                {
                    h = this.list[index] as Marshalling.MarshallingHash;

                    headerIndex = 0;
                    foreach (string key in h.HashKeys)
                    {
                        SizeArgs s = new SizeArgs((uint)headerIndex, (uint)(index + 1), 60, 20, 1, 1, headerIndex, 0);
                        UXReadOnlyText t = new UXReadOnlyText(h[key].Value.ToString());
                        s.Options(Library.Disposition.CENTER, Library.EnumConstraint.FIXED, Library.EnumConstraint.FIXED, new Library.CSSColor("white"), new Library.CSSColor("black"), new Library.CSSColor("black"), 2, t);
                        s.isValid = true;
                        this.VerticalCustomization[index, headerIndex] = s;
                        ++headerIndex;
                    }
                }
            }
        }

        #endregion
    }
}
