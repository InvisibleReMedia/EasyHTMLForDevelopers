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
    public partial class AddObject : Form
    {
        private int localeComponentId;

        public AddObject()
        {
            InitializeComponent();
            this.RegisterControls(ref this.localeComponentId);
        }

        public string SelectedMasterObject
        {
            get
            {
                if (this.treeView1.SelectedNode != null)
                {
                    if (this.treeView1.SelectedNode.Tag is Library.MasterObject)
                    {
                        Library.MasterObject mo = this.treeView1.SelectedNode.Tag as Library.MasterObject;
                        return mo.Name;
                    }
                    else
                        return "";
                }
                else
                    return "";
            }
        }

        public Library.HTMLTool Tool
        {
            get
            {
                if (this.treeView1.SelectedNode != null)
                    return this.treeView1.SelectedNode.Tag as Library.HTMLTool;
                else
                    return null;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.UnregisterControls(ref this.localeComponentId);
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.UnregisterControls(ref this.localeComponentId);
            this.Close();
        }

        private void AddTool(Library.FolderTool tool, TreeNode node)
        {
            foreach (Library.FolderTool ft in tool.Folders)
            {
                TreeNode subNode = node.Nodes.Add(ft.Name);
                AddTool(ft, subNode);
            }

            foreach (Library.HTMLTool ht in tool.Tools)
            {
                TreeNode toolNode = node.Nodes.Add(ht.Title);
                toolNode.Tag = ht;
            }
        }

        private void AddObject_Load(object sender, EventArgs e)
        {
            TreeNode mObjects = this.treeView1.Nodes.Add(Localization.Strings.GetString("MasterObject"));
            foreach(Library.MasterObject mo in Library.Project.CurrentProject.MasterObjects)
            {
                TreeNode subNode = mObjects.Nodes.Add(mo.Title);
                subNode.Tag = mo;
            }
            Library.FolderTool tool = Library.Project.CurrentProject.Tools;
            foreach (Library.FolderTool ft in tool.Folders)
            {
                TreeNode subNode = this.treeView1.Nodes.Add(ft.Name);
                AddTool(ft, subNode);
            }

            foreach (Library.HTMLTool ht in tool.Tools)
            {
                TreeNode toolNode = this.treeView1.Nodes.Add(ht.Title);
                toolNode.Tag = ht;
            }
        }
    }
}
