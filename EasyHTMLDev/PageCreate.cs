using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;

namespace EasyHTMLDev
{
    public partial class PageCreate : Form
    {
        private List<Library.MasterPage> list;
        private Library.Page page;

        private int localeComponentId;

        public PageCreate()
        {
            InitializeComponent();
            this.page = new Library.Page();
            this.RegisterControls(ref this.localeComponentId);
        }

        public Library.Page Page
        {
            get { return this.page; }
        }

        public List<Library.MasterPage> MasterPages
        {
            get { return this.list; }
            set { this.list = value; }
        }

        private void PageCreate_Load(object sender, EventArgs e)
        {
            this.pageBindingSource.DataSource = this.Page;
            this.btns.selectedChanged += new EventHandler(btns_selectedChanged);
            foreach (Library.MasterPage mp in this.MasterPages)
            {
                MasterPageImg img = new MasterPageImg();
                img.webBrowser1.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(webBrowser1_DocumentCompleted);
                img.MasterPage = mp;
                img.PrepareImage();
            }
        }

        void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            Control web = sender as Control;
            Bitmap bm = new Bitmap(web.Width, web.Height);
            web.DrawToBitmap(bm, web.Bounds);
            int index = this.imList.Images.Add(bm, Color.Blue);
            this.listView1.Items.Add(Path.GetFileNameWithoutExtension(e.Url.AbsolutePath), index);
        }

        void btns_selectedChanged(object sender, EventArgs e)
        {
            this.button1.Image = ((Button)sender).Image;
            this.Page.DispositionText = this.btns.SelectedName;
            this.btns.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!this.btns.Visible)
                this.btns.Show();
            else
                this.btns.Hide();
        }

        private void rbFixe_CheckedChanged(object sender, EventArgs e)
        {
            this.Page.ConstraintHeight = Library.EnumConstraint.FIXED;
            this.Page.ConstraintWidth = Library.EnumConstraint.FIXED;
        }

        private void rbAuto_CheckedChanged(object sender, EventArgs e)
        {
            this.Page.ConstraintHeight = Library.EnumConstraint.AUTO;
            this.Page.ConstraintWidth = Library.EnumConstraint.AUTO;
        }

        private void rbRelative_CheckedChanged(object sender, EventArgs e)
        {
            this.Page.ConstraintHeight = Library.EnumConstraint.RELATIVE;
            this.Page.ConstraintWidth = Library.EnumConstraint.RELATIVE;
        }

        private void creer_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(this.textBox1.Text))
            {
                if (Library.Project.AddPage(Library.Project.CurrentProject, this.Page, this.textBox1.Text))
                {
                    this.Page.MasterPageName = this.listView1.SelectedItems[0].Text;
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                    this.UnregisterControls(ref this.localeComponentId);
                }
                else
                {
                    MessageBox.Show(Localization.Strings.GetString("ExceptionPageNotCreated"));
                }
            }
            else
            {
                MessageBox.Show(Localization.Strings.GetString("MissingData"), Localization.Strings.GetString("MissingDataTitle"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void annuler_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
            this.UnregisterControls(ref this.localeComponentId);
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count > 0)
            {
                this.creer.Enabled = true;
            }
            else
            {
                this.creer.Enabled = false;
            }
        }
    }
}
