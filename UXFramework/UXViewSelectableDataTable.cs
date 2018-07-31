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
        /// <param name="id">id</param>
        public UXViewSelectableDataTable(string id) : base(id)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Bind sequence
        /// </summary>
        /// <param name="a">specific bind function</param>
        public override void Bind(Action<Marshalling.MarshallingList> a)
        {
            a(list);

            // construction de la table
            uint count = Convert.ToUInt32(this.list.Count);
            if (count > 0)
            {
                // add list element count + one header
                this.LineCount = count + 1;
                // add column items + one left + one right for selecting
                this.ColumnCount = 2 + Convert.ToUInt32((this.list[0] as Marshalling.MarshallingHash).HashKeys.Count());

                for (int index = 0; index < this.LineCount; ++index)
                {
                    SizeArgs s = new SizeArgs(0, (uint)index, 0, 50, 1, 1, 0, index);
                    s.Options(Library.Disposition.CENTER, Library.EnumConstraint.AUTO, Library.EnumConstraint.FIXED, new Library.CSSColor("#99D9EA"), new Library.CSSColor("#47D9EA"), new Library.CSSColor("black"), 1, 5, null);
                    s.isValid = true;
                    this.HorizontalCustomization[index] = s;
                }

                {
                    // côté gauche de la ligne
                    SizeArgs sg = new SizeArgs(0, 0, 10, 10, 1, 1, 0, 0);
                    sg.Options(Library.Disposition.CENTER, Library.EnumConstraint.FIXED, Library.EnumConstraint.FIXED, new Library.CSSColor(), new Library.CSSColor(), new Library.CSSColor("black"), 0, 3, null);
                    sg.isValid = true;
                    this.VerticalCustomization[0, 0] = sg;
                }

                int headerIndex = 1;
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

                {
                    // côté droit de la ligne
                    SizeArgs sd = new SizeArgs((uint)headerIndex, 0, 10, 10, 1, 1, headerIndex, 0);
                    sd.Options(Library.Disposition.CENTER, Library.EnumConstraint.FIXED, Library.EnumConstraint.FIXED, new Library.CSSColor(), new Library.CSSColor(), new Library.CSSColor("black"), 0, 3,null);
                    sd.isValid = true;
                    this.VerticalCustomization[0, headerIndex] = sd;
                }

                UXSelectableText selectText;
                for (int index = 0; index < count; ++index)
                {
                    h = this.list[index] as Marshalling.MarshallingHash;

                    {
                        // côté gauche de la ligne
                        SizeArgs sg = new SizeArgs(0, (uint)(index + 1), 10, 10, 1, 1, 0, index + 1);
                        UXImage img = new UXImage("imgLeft_" + (index + 1).ToString(), "left.png");
                        img.Size = new System.Drawing.Size(5, 9);
                        sg.Options(Library.Disposition.CENTER, Library.EnumConstraint.FIXED, Library.EnumConstraint.FIXED, new Library.CSSColor(), new Library.CSSColor(), new Library.CSSColor("black"), 0, 3, img);
                        sg.isValid = true;
                        this.VerticalCustomization[index + 1, 0] = sg;
                    }

                    headerIndex = 1;
                    foreach (string key in h.HashKeys)
                    {
                        SizeArgs s = new SizeArgs((uint)headerIndex, (uint)(index + 1), 100, 0, 1, 1, headerIndex, index + 1);
                        selectText = new UXSelectableText("text_header" + headerIndex + "_line" + (index + 1), h[key].Value.ToString(), (index + 1));
                        s.Options(Library.Disposition.CENTER, Library.EnumConstraint.FIXED, Library.EnumConstraint.AUTO, new Library.CSSColor("#F9F0D2"), new Library.CSSColor("#47D9EA"), new Library.CSSColor("black"), 2, 3, selectText);
                        s.isValid = true;
                        this.VerticalCustomization[index + 1, headerIndex] = s;
                        ++headerIndex;
                    }

                    {
                        // côté droit de la ligne
                        SizeArgs sd = new SizeArgs((uint)headerIndex, (uint)(index + 1), 10, 10, 1, 1, headerIndex, index + 1);
                        UXImage img = new UXImage("imgRight_" + (index + 1).ToString(), "right.png");
                        img.Size = new System.Drawing.Size(5, 9);
                        sd.Options(Library.Disposition.CENTER, Library.EnumConstraint.FIXED, Library.EnumConstraint.FIXED, new Library.CSSColor(), new Library.CSSColor(), new Library.CSSColor("black"), 0, 3, img);
                        sd.isValid = true;
                        this.VerticalCustomization[index + 1, headerIndex] = sd;
                    }
                }
            }
        }


        #endregion

    }
}
