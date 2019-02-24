using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections;
using CommonDirectories;

namespace EasyHTMLDev
{
    public partial class PageView : Form
    {
        private int localeComponentId;
        private RadioButtonBinding rbWidth, rbHeight;

        public PageView()
        {
            InitializeComponent();
            this.rbWidth = new RadioButtonBinding(typeof(Library.EnumConstraint), this.pageBindingSource, "ConstraintWidth");
            rbWidth.AddControlsIntoGroupBox(this.groupBox1, FlowDirection.RightToLeft);
            this.rbHeight = new RadioButtonBinding(typeof(Library.EnumConstraint), this.pageBindingSource, "ConstraintHeight");
            rbHeight.AddControlsIntoGroupBox(this.groupBox3, FlowDirection.RightToLeft);
            this.pageBindingSource.CurrentItemChanged += new EventHandler(pageBindingSource_CurrentItemChanged);
            this.RegisterControls(ref this.localeComponentId);
        }

        public PageView(string title)
        {
            InitializeComponent();
            this.rbWidth = new RadioButtonBinding(typeof(Library.EnumConstraint), this.pageBindingSource, "ConstraintWidth");
            rbWidth.AddControlsIntoGroupBox(this.groupBox1, FlowDirection.RightToLeft);
            this.rbHeight = new RadioButtonBinding(typeof(Library.EnumConstraint), this.pageBindingSource, "ConstraintHeight");
            rbHeight.AddControlsIntoGroupBox(this.groupBox3, FlowDirection.RightToLeft);
            this.pageBindingSource.CurrentItemChanged += new EventHandler(pageBindingSource_CurrentItemChanged);
            this.RegisterControls(ref this.localeComponentId, title);
        }

        public Library.Page Page
        {
            get { return this.pageBindingSource.DataSource as Library.Page; }
            set { this.pageBindingSource.DataSource = value; }
        }

        private void ReloadBrowser(bool firstLoad = false)
        {
            try
            {
                Library.OutputHTML html = this.Page.GenerateDesign();
                if (!String.IsNullOrEmpty(this.Page.Folder))
                {
                    ConfigDirectories.AddFile(Library.Project.CurrentProject.Title, this.Page.Folder + "ehd_ask.png", ConfigDirectories.GetBuildFolder(Library.Project.CurrentProject.Title) + "ehd_ask.png");
                }
                FileStream fs = new FileStream(ConfigDirectories.GetBuildFolder(Library.Project.CurrentProject.Title) + this.Page.Folder + this.Page.Name, FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(html.HTML.ToString());
                sw.Close();
                sw.Dispose();
                fs.Close();
                fs.Dispose();
                this.webBrowser1.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(webBrowser1_DocumentCompleted);
                this.webBrowser1.Navigate(ConfigDirectories.GetBuildFolder(Library.Project.CurrentProject.Title) + this.Page.Folder + this.Page.Name);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void PageView_Load(object sender, EventArgs e)
        {
            this.btns.selectedChanged += new EventHandler(btns_selectedChanged);
            Library.MasterPage mo = Library.Project.CurrentProject.MasterPages.Find(a => { return a.Name == this.Page.MasterPageName; });
            if (mo != null)
            {
                this.textBox2.Text = mo.Name;
            }
            this.ReloadBrowser(true);
        }

        void pageBindingSource_CurrentItemChanged(object sender, EventArgs e)
        {
            this.btnValidate1.SetDirty();
        }

        void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            this.webBrowser1.DocumentCompleted -= new WebBrowserDocumentCompletedEventHandler(webBrowser1_DocumentCompleted);
            HtmlElement elem = this.webBrowser1.Document.GetElementById("callback");
            elem.AttachEventHandler("onclick", new EventHandler(click));
        }

        private void click(object sender, EventArgs e)
        {
            AddObject add = new AddObject();
            Library.Project proj = Library.Project.CurrentProject;
            DialogResult dr = add.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                HtmlElement obj = this.webBrowser1.Document.GetElementById("callback");
                if (obj != null)
                {
                    Library.HTMLObject item = new Library.HTMLObject();
                    item.Title = add.txtName.Text;
                    item.Container = obj.GetAttribute("container");
                    if (!String.IsNullOrEmpty(add.SelectedMasterObject))
                    {
                        item.MasterObjectName = add.SelectedMasterObject;
                        Library.MasterObject mo = proj.MasterObjects.Find(a => a.Name == add.SelectedMasterObject);
                        if (mo != null)
                        {
                            // recherche de l'objet container
                            Library.IContainer objectContainer;
                            if (proj.FindContainer(item.Container, out objectContainer, (List<Library.IContainer> containers,
                                List<Library.IContent> objects, string searchName, out Library.IContainer found) =>
                            {
                                return this.Page.SearchContainer(containers, objects, searchName, out found);
                            }))
                            {
                                item.Width = objectContainer.Width;
                                item.Height = objectContainer.Height;
                                item.ConstraintHeight = objectContainer.ConstraintHeight;
                                item.ConstraintWidth = objectContainer.ConstraintWidth;
                            }
                        }
                    }
                    else if (add.Tool != null)
                    {
                        item = new Library.HTMLObject(add.Tool);
                        item.Title = add.txtName.Text;
                        item.Container = obj.GetAttribute("container");
                        // recherche de l'objet container
                        Library.IContainer objectContainer;
                        if (proj.FindContainer(item.Container, out objectContainer, (List<Library.IContainer> containers,
                            List<Library.IContent> objects, string searchName, out Library.IContainer found) =>
                        {
                            return this.Page.SearchContainer(containers, objects, searchName, out found);
                        }))
                        {
                            item.Width = objectContainer.Width;
                            item.Height = objectContainer.Height;
                            item.ConstraintHeight = objectContainer.ConstraintHeight;
                            item.ConstraintWidth = objectContainer.ConstraintWidth;
                        }
                    }

                    item.BelongsTo = this.Page.Name;
                    proj.Instances.Add(item);
                    this.Page.Objects.Add(item);
                    this.btnValidate1.SetDirty();
                    this.ReloadBrowser();
                }
            }
        }

        void btns_selectedChanged(object sender, EventArgs e)
        {
            this.button1.Image = ((Button)sender).Image;
            this.Page.DispositionText = this.btns.SelectedName;
            this.btns.Hide();
            this.btnValidate1.SetDirty();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!this.btns.Visible)
                this.btns.Show();
            else
                this.btns.Hide();
        }

        private void actualiser_Click(object sender, EventArgs e)
        {
            this.ReloadBrowser();
        }

        private void PageView_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.UnregisterControls(ref this.localeComponentId);
            this.rbHeight.Close();
            this.rbWidth.Close();
        }
    }
}
