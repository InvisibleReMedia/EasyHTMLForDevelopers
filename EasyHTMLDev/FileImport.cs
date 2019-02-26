using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace EasyHTMLDev
{
    public partial class FileImport : Form
    {
        private int localeComponentId;

        private string startPath;
        private string fileName;
        private string destPath;
        private TreeNode startNode;

        public string StartPath
        {
            get { return this.startPath; }
        }

        public string FileName
        {
            get { return this.fileName; }
        }

        public string DestinationPath
        {
            get { return this.destPath; }
            set { this.destPath = value; }
        }

        public FileImport()
        {
            InitializeComponent();
            this.path.DataBindings.Add("Text", this, "DestinationPath");
            this.RegisterControls(ref this.localeComponentId);
            this.startNode = this.treeView1.Nodes.Add("./");
            this.treeView1.BeforeExpand += new TreeViewCancelEventHandler((o, e) =>
            {
                this.treeView1.SuspendLayout();
                e.Node.Nodes.Clear();
                EnumerateFiles(e.Node, e.Node.Tag.ToString());
                this.treeView1.ResumeLayout();
            });
        }

        private int CountNodes(TreeNode node)
        {
            int total = 0;
            foreach (TreeNode subNode in node.Nodes)
            {
                total += CountNodes(subNode);
            }
            return total;
        }

        private void EnumerateFiles(TreeNode node, string path)
        {
            DirectoryInfo di = new DirectoryInfo(path);
            foreach (DirectoryInfo subDir in di.GetDirectories())
            {
                TreeNode subNode = node.Nodes.Add(subDir.Name);
                subNode.Tag = Path.Combine(path, subDir.Name);
                subNode.Nodes.Add("./");
            }
            foreach (FileInfo fi in di.GetFiles())
            {
                TreeNode subNode = node.Nodes.Add(fi.Name);
                subNode.Tag = Path.Combine(path, fi.Name);
            }
        }

        private void browse_Click(object sender, EventArgs e)
        {
            DialogResult dr = fbd.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                this.startPath = fbd.SelectedPath;
                this.startNode.Tag = this.startPath;
                EnumerateFiles(this.startNode, this.startPath);
            }
        }

        private void valider_Click(object sender, EventArgs e)
        {
            int n = this.treeView1.SelectedNode.GetNodeCount(true);
            if (n > 1)
            {
                DialogResult r = MessageBox.Show(String.Format("Copier {0} éléments ?", n), "Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                if (r == System.Windows.Forms.DialogResult.No)
                    return;
            }
            this.fileName = Path.Combine(this.startPath,  this.treeView1.SelectedNode.Tag.ToString());
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
    }
}
