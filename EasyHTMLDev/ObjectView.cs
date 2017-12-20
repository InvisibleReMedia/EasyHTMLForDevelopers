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
    public partial class ObjectView : Form
    {
        private int localeComponentId;

        public ObjectView()
        {
            InitializeComponent();
            this.RegisterControls(ref this.localeComponentId);
        }

        public ObjectView(string title)
        {
            InitializeComponent();
            this.RegisterControls(ref this.localeComponentId, title);
        }

        public Library.HTMLObject HTMLObject
        {
            get { return this.hTMLObjectBindingSource.DataSource as Library.HTMLObject; }
            set { this.hTMLObjectBindingSource.DataSource = value; }
        }

        private void ToolView_Load(object sender, EventArgs e)
        {
            Library.MasterObject mo = Library.Project.CurrentProject.MasterObjects.Find(a => { return a.Name == this.HTMLObject.MasterObjectName; });
            if (mo != null)
            {
                this.textBox2.Text = mo.Title;
            }
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ObjectView_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.UnregisterControls(ref this.localeComponentId);
        }
    }
}
