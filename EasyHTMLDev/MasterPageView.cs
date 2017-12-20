﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using CommonDirectories;

namespace EasyHTMLDev
{
    public partial class MasterPageView : Form
    {
        private Library.MasterPage mPage;
        private CSSOptions opt;
        private Zones z;
        private int localeComponentId;
        private ZonesBinding rbWidth, rbHeight;

        public MasterPageView()
        {
            InitializeComponent();
            this.rbWidth = new ZonesBinding(typeof(Library.EnumConstraint), this.masterPageBindingSource, "ConstraintWidth", "Width");
            rbWidth.modified += z_modified;
            rbWidth.AddControlsIntoGroupBox(this.groupBox2, FlowDirection.LeftToRight);
            this.rbHeight = new ZonesBinding(typeof(Library.EnumConstraint), this.masterPageBindingSource, "ConstraintHeight", "Height");
            rbHeight.modified += z_modified;
            rbHeight.AddControlsIntoGroupBox(this.groupBox3, FlowDirection.LeftToRight);
            this.RegisterControls(ref this.localeComponentId);
        }

        public MasterPageView(string title)
        {
            InitializeComponent();
            this.rbWidth = new ZonesBinding(typeof(Library.EnumConstraint), this.masterPageBindingSource, "ConstraintWidth", "Width");
            rbWidth.modified += z_modified;
            rbWidth.AddControlsIntoGroupBox(this.groupBox2, FlowDirection.LeftToRight);
            this.rbHeight = new ZonesBinding(typeof(Library.EnumConstraint), this.masterPageBindingSource, "ConstraintHeight", "Height");
            rbHeight.modified += z_modified;
            rbHeight.AddControlsIntoGroupBox(this.groupBox3, FlowDirection.LeftToRight);
            this.RegisterControls(ref this.localeComponentId, title);
        }

        void masterPageBindingSource_CurrentItemChanged(object sender, EventArgs e)
        {
            this.btnValidate1.SetDirty();
        }

        public Library.MasterPage MasterPage
        {
            get { return this.mPage; }
            set { this.mPage = value; }
        }

        private void ReloadBrowser(bool firstLoad = false)
        {
            try
            {
                Library.OutputHTML html = this.mPage.GenerateDesign();
                FileStream fs = new FileStream(ConfigDirectories.GetBuildFolder(Library.Project.CurrentProject.Title) + this.mPage.Name + ".html", FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(html.HTML.ToString());
                sw.Close();
                sw.Dispose();
                fs.Close();
                fs.Dispose();
                this.webBrowser1.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(webBrowser1_DocumentCompleted);
                this.webBrowser1.Navigate(ConfigDirectories.GetBuildFolder(Library.Project.CurrentProject.Title) + this.mPage.Name + ".html");
                this.textBox4.Text = this.mPage.CSS.GenerateCSS(false, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void MasterPageView_Load(object sender, EventArgs e)
        {
            this.btnValidate1.Save += btnValidate1_Save;
            this.masterPageBindingSource.DataSource = this.mPage;
            this.masterPageBindingSource.CurrentItemChanged += new EventHandler(masterPageBindingSource_CurrentItemChanged);
            this.ReloadBrowser(true);
        }

        private void btnValidate1_Save(object sender, EventArgs e)
        {
            // update size and fix size errors
            if (this.cbFix.Checked && this.MasterPage != null)
            {
                Library.SizeCompute.ComputeMasterPage(Library.Project.CurrentProject, this.MasterPage);
            }
        }

        void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            this.webBrowser1.DocumentCompleted -= new WebBrowserDocumentCompletedEventHandler(webBrowser1_DocumentCompleted);
            HtmlElement elem = this.webBrowser1.Document.GetElementById("callback");
            elem.AttachEventHandler("onclick", new EventHandler(click));
            elem = this.webBrowser1.Document.GetElementById("suppress");
            elem.AttachEventHandler("onclick", new EventHandler(suppress));
        }

        private void suppress(object sender, EventArgs e)
        {
            HtmlElement obj = this.webBrowser1.Document.GetElementById("suppress");
            string name = obj.GetAttribute("objectName");
            Library.HTMLObject found = this.MasterPage.Objects.Find(a => { return a.Name == name && a.Container == "globalContainer"; });
            if (found != null)
            {
                this.MasterPage.Objects.Remove(found);
                this.btnValidate1.SetDirty();
            }
            this.ReloadBrowser();
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
                                return this.MasterPage.SearchContainer(containers, objects, searchName, out found);
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
                            return this.MasterPage.SearchContainer(containers, objects, searchName, out found);
                        }))
                        {
                            item.Width = objectContainer.Width;
                            item.Height = objectContainer.Height;
                            item.ConstraintHeight = objectContainer.ConstraintHeight;
                            item.ConstraintWidth = objectContainer.ConstraintWidth;
                        }
                    }

                    item.BelongsTo = this.mPage.Name;
                    proj.Instances.Add(item);
                    this.mPage.Objects.Add(item);
                    this.btnValidate1.SetDirty();
                    this.ReloadBrowser();
                }
            }
        }

        private void MasterPageView_DragDrop(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Link;
        }

        private void textBox4_Validating(object sender, CancelEventArgs e)
        {
            string reason = String.Empty;
            if (CSSValidation.CSSValidate(this.textBox4.Text, false, out reason, this.mPage.CSS))
            {
                this.epCSS.Clear();
                this.btnValidate1.SetDirty();
            }
            else
            {
                this.epCSS.SetError(this.textBox4, reason);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MasterPageOptions options = new MasterPageOptions();
            options.MasterPage = this.mPage;
            options.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.ReloadBrowser();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (z != null && !z.IsDisposed)
            {
                z.Show();
            }
            else
            {
                z = new Zones();
                z.MasterPage = this.MasterPage;
                z.modified += new EventHandler(z_modified);
                z.Show();
            }
        }

        void z_modified(object sender, EventArgs e)
        {
            this.btnValidate1.SetDirty();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.opt != null && !this.opt.IsDisposed)
            {
                this.opt.Show();
            }
            else
            {
                this.opt = new CSSOptions();
                this.opt.modified += new EventHandler(z_modified);
                opt.CSS = this.MasterPage.CSS;
                opt.Show();
            }
        }

        private void MasterPageView_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.z != null && !this.z.IsDisposed)
            {
                this.z.Close();
            }
            if (this.opt != null && !this.opt.IsDisposed)
            {
                this.opt.Close();
            }
            this.UnregisterControls(ref this.localeComponentId);
            this.rbHeight.Close();
            this.rbWidth.Close();
        }
    }
}
