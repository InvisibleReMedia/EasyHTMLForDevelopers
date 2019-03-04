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
    public partial class ToolView : Form
    {
        private CSSOptions opt;
        private ZonesBinding rbWidth, rbHeight;
        private int localeComponentId;

        public ToolView()
        {
            InitializeComponent();
            this.rbWidth = new ZonesBinding(typeof(Library.EnumConstraint), this.hTMLToolBindingSource, "ConstraintWidth", "Width");
            rbWidth.modified += z_modified;
            rbWidth.AddControlsIntoGroupBox(this.groupBox3, FlowDirection.LeftToRight);
            this.rbHeight = new ZonesBinding(typeof(Library.EnumConstraint), this.hTMLToolBindingSource, "ConstraintHeight", "Height");
            rbHeight.modified += z_modified;
            rbHeight.AddControlsIntoGroupBox(this.groupBox4, FlowDirection.LeftToRight);
            this.RegisterControls(ref this.localeComponentId);
        }

        public ToolView(string title)
        {
            InitializeComponent();
            this.rbWidth = new ZonesBinding(typeof(Library.EnumConstraint), this.hTMLToolBindingSource, "ConstraintWidth", "Width");
            rbWidth.modified += z_modified;
            rbWidth.AddControlsIntoGroupBox(this.groupBox3, FlowDirection.LeftToRight);
            this.rbHeight = new ZonesBinding(typeof(Library.EnumConstraint), this.hTMLToolBindingSource, "ConstraintHeight", "Height");
            rbHeight.modified += z_modified;
            rbHeight.AddControlsIntoGroupBox(this.groupBox4, FlowDirection.LeftToRight);
            this.RegisterControls(ref this.localeComponentId, title);
        }

        public Library.HTMLTool HTMLTool
        {
            get { return this.hTMLToolBindingSource.DataSource as Library.HTMLTool; }
            set { this.hTMLToolBindingSource.DataSource = value; }
        }

        private void ReloadBrowser()
        {
            try
            {
                Library.OutputHTML html = this.HTMLTool.GenerateDesign();
                FileStream fs = new FileStream(ConfigDirectories.GetBuildFolder(Library.Project.CurrentProject.Title) + this.HTMLTool.Name + ".html", FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(html.HTML.ToString());
                sw.Close();
                sw.Dispose();
                fs.Close();
                fs.Dispose();
                this.webBrowser1.Navigate(ConfigDirectories.GetBuildFolder(Library.Project.CurrentProject.Title) + this.HTMLTool.Name + ".html");
                this.textBox4.Text = this.HTMLTool.CSSOutput(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void textBox4_Validating(object sender, CancelEventArgs e)
        {
            string errorText;
            if (!this.HTMLTool.CSSList.List.Exists(a => a.Ids == this.HTMLTool.CSS.Ids))
                this.HTMLTool.CSSList.List.Add(this.HTMLTool.CSS);
            bool isSuccess = Library.CSSValidation.CSSValidate(this.textBox4.Text, false, this.HTMLTool.CSSList.List, out errorText);
            if (!isSuccess)
            {
                this.epCSS.SetError(this.textBox4, errorText);
            }
            else
            {
                this.epCSS.Clear();
                this.textBox4.Text = this.HTMLTool.CSSOutput(false);
                this.btnValidate1.SetDirty();
            }
        }

        private void ToolView_Load(object sender, EventArgs e)
        {
            this.hTMLToolBindingSource.CurrentItemChanged += new EventHandler(hTMLToolBindingSource_CurrentItemChanged);
            this.ReloadBrowser();
        }

        void hTMLToolBindingSource_CurrentItemChanged(object sender, EventArgs e)
        {
            this.btnValidate1.SetDirty();
        }

        private void actualiser_Click(object sender, EventArgs e)
        {
            this.ReloadBrowser();
        }

        void z_modified(object sender, EventArgs e)
        {
            this.btnValidate1.SetDirty();
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
                opt.CSS = this.HTMLTool.CSS;
                opt.modified += new EventHandler(this.hTMLToolBindingSource_CurrentItemChanged);
                opt.Show();
            }
        }

        private void ToolView_FormClosed(object sender, FormClosedEventArgs e)
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
