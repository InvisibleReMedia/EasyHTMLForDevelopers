using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using CommonDirectories;

namespace EasyHTMLDev
{
    public partial class MasterPageImg : UserControl
    {
        private Library.MasterPage mPage;

        public MasterPageImg()
        {
            InitializeComponent();
        }

        public Library.MasterPage MasterPage
        {
            get { return this.mPage; }
            set { this.mPage = value; }
        }

        public void PrepareImage()
        {
            try
            {
                Library.OutputHTML html = this.mPage.GenerateThumbnail();
                FileStream fs = new FileStream(ConfigDirectories.GetBuildFolder(Library.Project.CurrentProject.Title) + this.mPage.Name + ".html", FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(html.HTML.ToString());
                sw.Close();
                sw.Dispose();
                fs.Close();
                fs.Dispose();
                this.webBrowser1.Navigate(ConfigDirectories.GetBuildFolder(Library.Project.CurrentProject.Title) + this.mPage.Name + ".html");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public Image GetImage()
        {
            Bitmap im = new Bitmap(this.Width, this.Height);
            this.DrawToBitmap(im, this.Bounds);
            return im;
        }
    }
}
