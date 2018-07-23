using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EasyHTMLDev
{
    public partial class Import : Form
    {
        private int localeComponentId;

        public Import()
        {
            InitializeComponent();
            this.RegisterControls(ref this.localeComponentId);
        }


        private void importer_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
            this.UnregisterControls(ref this.localeComponentId);
        }

        private void annuler_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
            this.UnregisterControls(ref this.localeComponentId);
        }

        private void SelectParents(TreeNode current, bool select)
        {
            if (!select) {
                if (current.Parent != null)
                {
                    if (current.Parent.Checked)
                    {
                    }
                }
            }
            else
            {
                if (current.Parent != null)
                {
                    current.Parent.Checked = select;
                    this.SelectParents(current.Parent, select);
                }
            }
        }

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action == TreeViewAction.ByKeyboard || e.Action == TreeViewAction.ByMouse)
            {
                this.SelectParents(e.Node, e.Node.Checked);
            }
        }

        private void treeView1_AfterExpand(object sender, TreeViewEventArgs e)
        {
            if (e.Action == TreeViewAction.Expand)
            {
            }
        }
    }
}
