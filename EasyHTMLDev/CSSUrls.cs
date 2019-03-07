using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections.Specialized;

namespace EasyHTMLDev
{
    public partial class CSSUrls : Form
    {
        private List<string> datas;
        private int localeComponentId;

        public CSSUrls()
        {
            InitializeComponent();
            this.RegisterControls(ref this.localeComponentId);
        }

        public List<string> Datas
        {
            get { return this.datas; }
            set { this.datas = value; }
        }

        private void listBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (this.listBox1.SelectedIndex != -1)
                {
                    int index = this.listBox1.SelectedIndex;
                    this.datas.RemoveAt(this.listBox1.SelectedIndex);
                    this.listBox1.DataSource = null;
                    this.listBox1.DataSource = this.datas;
                    this.listBox1.SelectedIndex = index - 1;
                    this.btnValidate1.SetDirty();
                }
            }
            else if (e.KeyCode == Keys.Space)
            {
                this.listBox1.SelectedIndex = -1;
                e.Handled = true;
            }

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedIndex != -1)
            {
                this.textBox1.Text = (string)this.listBox1.SelectedValue;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(this.textBox1.Text))
            {
                if (this.listBox1.SelectedIndex != -1)
                {
                    this.datas[this.listBox1.SelectedIndex] = this.textBox1.Text;
                    this.listBox1.DataSource = null;
                    this.listBox1.DataSource = this.datas;
                }
                else
                {
                    this.datas.Add(this.textBox1.Text);
                    this.listBox1.DataSource = null;
                    this.listBox1.DataSource = this.datas;
                }
                this.btnValidate1.SetDirty();
            }
        }

        private void ConfigView_Load(object sender, EventArgs e)
        {
            this.listBox1.DataSource = this.datas;
        }

        private void JavaScriptUrls_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.UnregisterControls(ref this.localeComponentId);
        }
    }
}
