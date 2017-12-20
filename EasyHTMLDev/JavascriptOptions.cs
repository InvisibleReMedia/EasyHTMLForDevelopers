using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EasyHTMLDev
{
    public partial class JavascriptOptions : Form
    {
        private List<string> datas;
        private int localeComponentId;

        public JavascriptOptions()
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
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedIndex != -1)
            {
                this.textBox1.Text = (string)this.listBox1.SelectedItem;
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
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
            this.UnregisterControls(ref this.localeComponentId);
        }

        private void JavascriptOptions_Load(object sender, EventArgs e)
        {
            this.listBox1.DataSource = this.datas;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
            this.UnregisterControls(ref this.localeComponentId);
        }
    }
}
