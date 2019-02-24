using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Collections;

namespace EasyHTMLDev
{
    public partial class FolderCreate : Form
    {
        private int localeComponentId;

        public FolderCreate()
        {
            InitializeComponent();
            this.RegisterControls(ref this.localeComponentId);
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            Library.Node<string, Library.Accessor> rootFolder = Library.Project.CurrentProject.Hierarchy.Find(Library.Project.FoldersName);
            Library.Node<string, Library.Accessor> currentFolder = rootFolder;
            string[] list = this.textBox1.Text.Split('/');
            currentFolder.Find(list);
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
            this.UnregisterControls(ref this.localeComponentId);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
            this.UnregisterControls(ref this.localeComponentId);
        }
    }
}
