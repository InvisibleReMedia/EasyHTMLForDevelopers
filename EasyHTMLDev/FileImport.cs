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
        private List<string> fileNames;
        private string destPath;
        private TreeNode startNode;

        public string StartPath
        {
            get { return this.startPath; }
        }

        public List<string> FileNames
        {
            get { return this.fileNames; }
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
            this.valider.Enabled = false;
            this.RegisterControls(ref this.localeComponentId);
            this.InitTreeView();
        }

        private void InitTreeView()
        {
            this.startNode = this.treeView1.Nodes.Add("./");
            this.startNode.Tag = this.startPath;
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
                this.treeView1.Nodes.Clear();
                this.InitTreeView();
                EnumerateFiles(this.startNode, this.startPath);
            }
        }


        private void valider_Click(object sender, EventArgs e)
        {
            List<TreeNode> checkedNodes = this.treeView1.Nodes.Descendants().Where(c => c.Checked).ToList();
            int n = 0;
            foreach (TreeNode node in checkedNodes)
            {
                n += node.GetNodeCount(true);
            }
            if (n > 10)
            {
                DialogResult r = MessageBox.Show(String.Format("Copier {0} éléments ?", n), "Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                if (r == System.Windows.Forms.DialogResult.No)
                    return;
            }
            this.fileNames = this.treeView1.Nodes.Descendants().Where(c => c.Checked).Select(x => x.Tag.ToString()).ToList();
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

        private void path_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(this.DestinationPath))
            {
                this.valider.Enabled = true;
            }
            else
            {
                this.valider.Enabled = false;
            }
        }
    }
}
