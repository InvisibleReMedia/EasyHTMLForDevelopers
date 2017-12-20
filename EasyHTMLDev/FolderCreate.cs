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
            Library.Folder rootFolder = Library.Project.CurrentProject.Folders;
            Library.Folder currentFolder = rootFolder;
            string[] list = this.textBox1.Text.Split('/');
            IEnumerator el = list.GetEnumerator();
            if (el.MoveNext())
            {
                do
                {
                    if (!String.IsNullOrEmpty((string)el.Current))
                    {
                        if (!currentFolder.Folders.Exists(a => { return a.Name == (string)el.Current; }))
                        {
                            Library.Folder newFolder = new Library.Folder();
                            newFolder.Name = (string)el.Current;
                            newFolder.Ancestor = currentFolder;
                            currentFolder.Folders.Add(newFolder);
                            currentFolder = newFolder;
                        }
                        else
                        {
                            currentFolder = currentFolder.Folders.Find(a => { return a.Name == (string)el.Current; });
                        }
                    }
                }
                while (el.MoveNext());
            }
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
