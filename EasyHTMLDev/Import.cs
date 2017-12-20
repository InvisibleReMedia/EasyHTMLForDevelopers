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

        public void DataBind(Library.Node<Library.IProjectElement> node)
        {
            foreach (Library.Node<Library.IProjectElement> subNode in node)
            {
                TreeNode tn = this.treeView1.Nodes.Add(subNode.Object.ElementTitle);
                tn.Tag = subNode;
                this.DataBind(tn, subNode);
            }
        }

        private void DataBind(TreeNode parent, Library.Node<Library.IProjectElement> node)
        {
            foreach (Library.Node<Library.IProjectElement> subNode in node)
            {
                TreeNode tn = parent.Nodes.Add(subNode.Object.ElementTitle);
                tn.Tag = subNode;
                tn.Checked = subNode.IsSelected;
                this.DataBind(tn, subNode);
            }
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

        private void SelectSubNodes(TreeNode tn, bool select)
        {
            tn.Checked = select;
            (tn.Tag as Library.Node<Library.IProjectElement>).IsSelected = tn.Checked;
            foreach (TreeNode sub in tn.Nodes)
            {
                this.SelectSubNodes(sub, select);
            }
        }

        private bool HasSelectedChildren(Library.Node<Library.IProjectElement> current)
        {
            bool atLeastOneSelected = current.IsSelected;
            foreach (Library.Node<Library.IProjectElement> sub in current)
            {
                atLeastOneSelected = atLeastOneSelected || this.HasSelectedChildren(sub);
            }
            return atLeastOneSelected;
        }

        private void SelectParents(TreeNode current, bool select)
        {
            if (!select) {
                if (current.Parent != null)
                {
                    if (current.Parent.Checked)
                    {
                        if (!HasSelectedChildren(current.Parent.Tag as Library.Node<Library.IProjectElement>))
                        {
                            current.Parent.Checked = select;
                            this.SelectParents(current.Parent, select);
                        }
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
                this.SelectSubNodes(e.Node, e.Node.Checked);
                this.SelectParents(e.Node, e.Node.Checked);
            }
        }

        private void treeView1_AfterExpand(object sender, TreeViewEventArgs e)
        {
            if (e.Action == TreeViewAction.Expand)
            {
                this.SelectSubNodes(e.Node, e.Node.Checked);
            }
        }
    }
}
