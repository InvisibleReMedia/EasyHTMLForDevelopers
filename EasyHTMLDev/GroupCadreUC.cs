using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyHTMLDev
{
    public partial class GroupCadreUC : CadreUC
    {
        #region Private Fields
        private List<CadreUC> inner;
        private Library.GroupCadreModel group;
        #endregion

        public GroupCadreUC(SculpturePanel panel) : base(panel)
        {
            InitializeComponent();
            this.inner = new List<CadreUC>();
            this.group = new Library.GroupCadreModel();
        }

        public GroupCadreUC(SculpturePanel panel, Library.GroupCadreModel group, Library.CadreModel cm) : base(panel, cm)
        {
            InitializeComponent();
            this.group = new Library.GroupCadreModel();
            foreach (CadreUC cadre in panel.Controls)
            {
                if (this.group.Exists(cadre.TabIndex))
                {
                    this.inner.Add(cadre);
                    this.parent.Controls.Remove(cadre);
                    this.Controls.Add(cadre);
                }
            }
        }

        public void Add(CadreUC uc)
        {
            if (!this.IsBelowToThisGroup(uc))
            {
                this.inner.Add(uc);
                this.parent.Controls.Remove(uc);
                this.Controls.Add(uc);
            }
        }

        public void Remove(CadreUC uc)
        {
            if (this.IsBelowToThisGroup(uc))
            {
                this.Controls.Remove(uc);
                this.parent.Controls.Add(uc);
                this.inner.Remove(uc);
            }
        }

        public bool IsBelowToThisGroup(CadreUC uc)
        {
            return this.inner.Exists(a => { return a.TabIndex == uc.TabIndex; });
        }
    }
}
