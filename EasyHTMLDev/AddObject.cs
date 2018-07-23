﻿using System;
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

        private void AddObject_Load(object sender, EventArgs e)
        {
            TreeNode mObjects = this.treeView1.Nodes.Add(Localization.Strings.GetString("MasterObject"));
            var it = Library.Project.CurrentProject.Hierarchy.Find(Library.Project.MasterObjectsName).GetNodesEnumerator();
            while(it.MoveNext())
            {
                TreeNode subNode = mObjects.Nodes.Add(it.Current.Get("title"));
                subNode.Tag = it.Current;
            }

            it = Library.Project.CurrentProject.Hierarchy.Find("Tools").GetNodesEnumerator();
            while (it.MoveNext())
            {
                TreeNode subNode = mObjects.Nodes.Add(it.Current.Get("title"));
                subNode.Tag = it.Current;
            }
        }
    }
}
