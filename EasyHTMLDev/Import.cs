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

        private Library.Project proj;

        public Import()
        {
            InitializeComponent();
            this.RegisterControls(ref this.localeComponentId);
        }


        public Library.Project Current
        {
            get { return proj; }
            set { this.proj = value; }
        }

        public void DataBind(Library.Node<string, Library.Accessor> node)
        {
            foreach (Library.Node<string, Library.Accessor> subNode in node.SubNodes)
            {
                TreeNode tn = this.treeView1.Nodes.Add(subNode.NodeValue);
                tn.Tag = subNode;
                this.DataBind(tn, subNode);
            }
        }

        private void DataBind(TreeNode parent, Library.Node<string, Library.Accessor> node)
        {
            foreach (Library.Node<string, Library.Accessor> subNode in node.SubNodes)
            {
                if (subNode.NodeValue != Library.Project.FoldersName)
                {
                    TreeNode tn = parent.Nodes.Add(subNode.NodeValue);
                    tn.Tag = subNode;
                    this.DataBind(tn, subNode);
                }
            }
            foreach (Library.Leaf<Library.Accessor> subNode in node.Elements)
            {
                dynamic obj = subNode.Object.GetObject(this.Current);
                TreeNode tn = parent.Nodes.Add(Form1.StandardTitle(obj.ElementTitle));
                tn.Tag = subNode;
                tn.Checked = subNode.IsSelected;
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
            tn.Checked = true;
            if (tn.Tag is Library.Leaf<Library.Accessor>)
                (tn.Tag as Library.Leaf<Library.Accessor>).IsSelected = select;
            else
                (tn.Tag as Library.Node<string, Library.Accessor>).IsSelected = select;
            foreach (TreeNode sub in tn.Nodes)
            {
                this.SelectSubNodes(sub, select);
            }
        }

        private bool HasSelectedChildren(Library.Node<string, Library.Accessor> current)
        {
            bool atLeastOneSelected = current.IsSelected;
            if (!atLeastOneSelected)
            {
                foreach (Library.Leaf<Library.Accessor> sub in current.Elements)
                {
                    if (sub.IsSelected) return true;
                }
                foreach (Library.Node<string, Library.Accessor> sub in current.SubNodes)
                {
                    atLeastOneSelected = atLeastOneSelected || this.HasSelectedChildren(sub);
                    if (atLeastOneSelected) return atLeastOneSelected;
                }
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
                        if (current.Parent.Tag != null)
                        {
                            // parents are only Node
                            if (!HasSelectedChildren(current.Parent.Tag as Library.Node<string, Library.Accessor>))
                            {
                                current.Parent.Checked = select;
                                (current.Parent.Tag as Library.Node<string, Library.Accessor>).IsSelected = select;
                                this.SelectParents(current.Parent, select);
                            }
                        }
                    }
                }
            }
            else
            {
                if (current.Parent != null)
                {
                    current.Parent.Checked = select;
                    if (current.Parent.Tag != null)
                        // parents are only node
                        (current.Parent.Tag as Library.Node<string, Library.Accessor>).IsSelected = select;
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

        private void Import_Load(object sender, EventArgs e)
        {
            this.DataBind(this.Current.Hierarchy);
        }
    }
}
