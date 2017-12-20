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
    public partial class SimpleObjectView : Form
    {
        private CSSOptions opt;
        private ZonesBinding rbWidth, rbHeight;
        private int localeComponentId;

        public SimpleObjectView()
        {
            InitializeComponent();
            this.rbWidth = new ZonesBinding(typeof(Library.EnumConstraint), this.hTMLObjectBindingSource, "ConstraintWidth", "Width");
            rbWidth.AddControlsIntoGroupBox(this.groupBox3, FlowDirection.LeftToRight);
            this.rbHeight = new ZonesBinding(typeof(Library.EnumConstraint), this.hTMLObjectBindingSource, "ConstraintHeight", "Height");
            rbHeight.AddControlsIntoGroupBox(this.groupBox4, FlowDirection.LeftToRight);
            this.RegisterControls(ref this.localeComponentId);
        }

        public SimpleObjectView(string title)
        {
            InitializeComponent();
            this.rbWidth = new ZonesBinding(typeof(Library.EnumConstraint), this.hTMLObjectBindingSource, "ConstraintWidth", "Width");
            rbWidth.AddControlsIntoGroupBox(this.groupBox3, FlowDirection.LeftToRight);
            this.rbHeight = new ZonesBinding(typeof(Library.EnumConstraint), this.hTMLObjectBindingSource, "ConstraintHeight", "Height");
            rbHeight.AddControlsIntoGroupBox(this.groupBox4, FlowDirection.LeftToRight);
            this.RegisterControls(ref this.localeComponentId, title);
        }

        public Library.HTMLObject HTMLObject
        {
            get { return this.hTMLObjectBindingSource.DataSource as Library.HTMLObject; }
            set { this.hTMLObjectBindingSource.DataSource = value; }
        }

        private void ReloadBrowser(bool firstLoad = false)
        {
            try
            {
                Library.OutputHTML html = this.HTMLObject.GenerateDesign();
                FileStream fs = new FileStream(ConfigDirectories.GetBuildFolder(Library.Project.CurrentProject.Title) + this.HTMLObject.Name + ".html", FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(html.HTML.ToString());
                sw.Close();
                sw.Dispose();
                fs.Close();
                fs.Dispose();
                this.webBrowser1.Navigate(ConfigDirectories.GetBuildFolder(Library.Project.CurrentProject.Title) + this.HTMLObject.Name + ".html");
                this.textBox4.Text = this.HTMLObject.CSSOutput(false);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void textBox4_Validating(object sender, CancelEventArgs e)
        {
            string errorText;
            if (!this.HTMLObject.CSSAdditional.Exists(a => a.Ids == this.HTMLObject.CSS.Ids))
                this.HTMLObject.CSSAdditional.Add(this.HTMLObject.CSS);
            bool isSuccess = CSSValidation.CSSValidate(this.textBox4.Text, false, this.HTMLObject.CSSAdditional, out errorText);
            if (!isSuccess)
            {
                this.epCSS.SetError(this.textBox4, errorText);
            }
            else
            {
                this.epCSS.Clear();
                Library.CodeCSS css = this.HTMLObject.CSSAdditional.Find(a => a.Ids == "#" + this.HTMLObject.Id);
                if (css != null)
                {
                    this.HTMLObject.ImportCSS(css);
                    this.HTMLObject.CSSAdditional.Remove(css);
                }
                else
                {
                    Library.CodeCSS newCss = new Library.CodeCSS();
                    newCss.Ids = "#" + this.HTMLObject.Id;
                    this.HTMLObject.ImportCSS(newCss);
                }
                this.textBox4.Text = this.HTMLObject.CSSOutput(false);
                this.btnValidate1.SetDirty();
            }
        }

        private void actualiser_Click(object sender, EventArgs e)
        {
            this.ReloadBrowser();
        }

        private void Options_Click(object sender, EventArgs e)
        {
            if (this.opt != null && !this.opt.IsDisposed)
            {
                this.opt.Show();
            }
            else
            {
                this.opt = new CSSOptions();
                opt.CSS = this.HTMLObject.CSS;
                opt.modified += new EventHandler(this.hTMLObjectBindingSource_CurrentItemChanged);
                opt.Show();
            }
        }

        private void SimpleObjectView_Load(object sender, EventArgs e)
        {
            this.btnValidate1.Save += btnValidate1_Save;
            this.hTMLObjectBindingSource.CurrentItemChanged += new EventHandler(hTMLObjectBindingSource_CurrentItemChanged);
            this.ReloadBrowser(true);
        }

        void btnValidate1_Save(object sender, EventArgs e)
        {
            // update size and fix size errors
            if (this.cbFix.Checked && this.HTMLObject != null)
            {
                Library.SizeCompute.ComputeHTMLObject(Library.Project.CurrentProject, this.HTMLObject);
            }
        }

        void hTMLObjectBindingSource_CurrentItemChanged(object sender, EventArgs e)
        {
            this.btnValidate1.SetDirty();
        }

        private void SimpleObjectView_FormClosed(object sender, FormClosedEventArgs e)
        {
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
