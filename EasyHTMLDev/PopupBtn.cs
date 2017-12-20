using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EasyHTMLDev
{
    public partial class PopupBtn : UserControl
    {
        private Image curImage;
        public event EventHandler selectedChanged;
        private string title;

        public PopupBtn()
        {
            InitializeComponent();
        }

        public Image SelectedImage
        {
            get { return this.curImage; }
        }

        public string SelectedName
        {
            get { return this.title; }
            set
            {
                List<Control> controls = new List<Control>();
                foreach (Control c in this.Controls)
                {
                    controls.Add(c);
                }
                Button btn = controls.Find(a => { return (string)a.Tag == value; }) as Button;
                if (btn != null)
                {
                    this.curImage = btn.Image;
                    this.title = (string)btn.Tag;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.curImage = ((Button)sender).Image;
            this.title = (string)((Button)sender).Tag;
            if (this.selectedChanged != null)
            {
                this.selectedChanged(sender, e);
            }
        }
    }
}
