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
        protected Marshalling.MarshallingList list;
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
        public virtual void Bind(Action<Marshalling.MarshallingList> a)
        {
            a(list);

            // construction de la table
            uint count = Convert.ToUInt32(this.list.Count);
            if (count > 0)
            {
                /// add list element count + one header
                this.LineCount = count + 1;
                this.ColumnCount = Convert.ToUInt32((this.list[0] as Marshalling.MarshallingHash).HashKeys.Count());

                for (int index = 0; index < this.LineCount; ++index)
                {
                    SizeArgs s = new SizeArgs(0, (uint)index, 0, 50, 1, 1, 0, index);
                    s.Options(Library.Disposition.CENTER, Library.EnumConstraint.AUTO, Library.EnumConstraint.FIXED, new Library.CSSColor("#99D9EA"), new Library.CSSColor("#47D9EA"), new Library.CSSColor("black"), 1, 5, null);
                    s.isValid = true;
                    this.HorizontalCustomization[index] = s;
                }

                int headerIndex = 0;
                // construire le header
                Marshalling.MarshallingHash h = this.list[0] as Marshalling.MarshallingHash;
                foreach (string key in h.HashKeys)
                {
                    SizeArgs s = new SizeArgs((uint)headerIndex, 0, 100, 0, 1, 1, headerIndex, 0);
                    UXReadOnlyText t = new UXReadOnlyText(h[key].Name.ToString());
                    s.Options(Library.Disposition.CENTER, Library.EnumConstraint.FIXED, Library.EnumConstraint.AUTO, new Library.CSSColor("#10F0D2"), new Library.CSSColor("#47D9EA"), new Library.CSSColor("black"), 2, 3, t);
                    s.isValid = true;
                    this.VerticalCustomization[0, headerIndex] = s;
                    ++headerIndex;
                }
                for (int index = 0; index < count; ++index)
                {
                    h = this.list[index] as Marshalling.MarshallingHash;

                    headerIndex = 0;
                    foreach (string key in h.HashKeys)
                    {
                        SizeArgs s = new SizeArgs((uint)headerIndex, (uint)(index + 1), 100, 0, 1, 1, headerIndex, index + 1);
                        UXReadOnlyText t = new UXReadOnlyText(h[key].Value.ToString());
                        s.Options(Library.Disposition.CENTER, Library.EnumConstraint.FIXED, Library.EnumConstraint.AUTO, new Library.CSSColor("#F9F0D2"), new Library.CSSColor("#47D9EA"), new Library.CSSColor("black"), 2, 3, t);
                        s.isValid = true;
                        this.VerticalCustomization[index + 1, headerIndex] = s;
                        ++headerIndex;
                    }
                }
            }
        }

        #endregion
    }
}
