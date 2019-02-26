using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Library;

namespace EasyHTMLDev
{
    public partial class Open : Form
    {
        #region Private Fields
        private string _directorySource;
        private string _fileName;
        private int columnSorter;
        #endregion

        public Open(string dir)
        {
            this._directorySource = dir;
            InitializeComponent();
        }

        public string FileName
        {
            get { return this._fileName; }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            List<ListViewItem> list = new List<ListViewItem>();
            this.lvFiles.Items.Clear();
            DirectoryInfo di = new DirectoryInfo(this._directorySource);
            foreach (FileInfo fi in di.GetFiles("*.bin"))
            {
                try
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = Path.GetFileNameWithoutExtension(fi.Name);
                    string cd = "", md = "", rev = "";
                    BinaryFormatter bf = new BinaryFormatter();
                    FileStream fs = fi.Open(FileMode.Open);
                    Project proj = bf.Deserialize(fs) as Project;
                    fs.Close();
                    fs.Dispose();
                    if (proj != null)
                    {
                        cd = proj.CreationDate.ToShortDateString();
                        if (proj.ModificationDate != null)
                            md = proj.ModificationDate.ToShortDateString();
                        rev = proj.Revision.ToString();
                        lvi.SubItems.Add(cd);
                        lvi.SubItems.Add(md);
                        lvi.SubItems.Add(rev);
                        list.Add(lvi);
                    }
                }
                catch { }
            }
            list.Sort(new Comparison<ListViewItem>(delegate(ListViewItem l1, ListViewItem l2)
            {
                int res = 0;
                try
                {
                    if (this.columnSorter == 0)
                    {
                        res = String.Compare(l1.Text, l2.Text);
                    }
                    else if (this.columnSorter == 1)
                    {
                        DateTime dt1 = DateTime.Parse(l1.SubItems[1].Text);
                        DateTime dt2 = DateTime.Parse(l2.SubItems[1].Text);
                        res = DateTime.Compare(dt1, dt2);
                    }
                    else if (this.columnSorter == 2)
                    {
                        DateTime dt1 = DateTime.Parse(l1.SubItems[2].Text);
                        DateTime dt2 = DateTime.Parse(l2.SubItems[2].Text);
                        res = DateTime.Compare(dt1, dt2);
                    }
                    else
                    {
                        Int32 i1 = Int32.Parse(l1.SubItems[3].Text);
                        Int32 i2 = Int32.Parse(l2.SubItems[3].Text);
                        res = i1.CompareTo(i2);
                    }
                }
                catch { }
                return res;
            }));
            foreach (ListViewItem item in list)
            {
                this.lvFiles.Items.Add(item);
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            this._fileName = this.lvFiles.SelectedItems[0].Text + ".bin";
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void lvFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lvFiles.SelectedIndices.Count > 0)
            {
                this.btnOpen.Enabled = true;
                this.btnNew.Enabled = true;
            }
            else
            {
                this.btnOpen.Enabled = false;
                this.btnNew.Enabled = false;
            }
        }

        private void Open_Load(object sender, EventArgs e)
        {
            this.btnRefresh_Click(sender, e);
        }

        private void lvFiles_DoubleClick(object sender, EventArgs e)
        {
            this.btnOpen_Click(sender, e);
        }

        private string SearchNewFileName()
        {
            DirectoryInfo di = new DirectoryInfo(this._directorySource);
            FileInfo[] files = di.GetFiles("Unknown-*.bin");
            int max;
            if (files.Count() > 0)
            {
                max = files.Max(f =>
                {
                    string name = Path.GetFileNameWithoutExtension(f.Name);
                    int pos = name.LastIndexOf('-');
                    return Convert.ToInt32(name.Substring(pos + 1));
                });
            }
            else
            {
                max = 0;
            }
            return "Unknown-" + (max + 1).ToString().PadLeft(3,'0');
        }

        public void btnNew_Click(object sender, EventArgs e)
        {
            FileInfo fi = new FileInfo(this._directorySource + "\\" + this.SearchNewFileName() + ".bin");
            // we create the file now with the correct format
            Project proj = new Project();
            proj.Title = Path.GetFileNameWithoutExtension(fi.Name);
            proj.CreationDate = DateTime.Now;
            string errorText;
            if (Project.Save(fi, proj, out errorText))
            {
                proj.Set(Project.hasErrorSaveName, false);
            }
            else
            {
                proj.Set(Project.hasErrorSaveName, true);
                proj.Set(Project.errorReasonName, errorText);
            }
            // we need to create the respective folder
            CommonDirectories.ConfigDirectories.CreateDirectoryProject(proj.Title);
            this._fileName = fi.Name;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void lvFiles_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            this.columnSorter = e.Column;
            this.btnRefresh_Click(sender, new EventArgs());
        }

        private void lvFiles_Click(object sender, EventArgs e)
        {
            this.lvFiles_DoubleClick(sender, e);
        }
    }
}