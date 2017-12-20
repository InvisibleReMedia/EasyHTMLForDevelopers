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
    public partial class ConfigView : Form
    {
        private NameValueCollection datas;
        private int localeComponentId;

        public ConfigView()
        {
            InitializeComponent();
            this.btnValidate1.btnOK.Click += btnOK_Click;
            this.btnValidate1.btnValider.Click += btnValider_Click;
            this.btnValidate1.btnAnnuler.Click += btnAnnuler_Click;
            this.RegisterControls(ref localeComponentId);
        }

        void btnAnnuler_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Ignore;
            this.UnregisterControls(ref this.localeComponentId);
        }

        void btnValider_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.UnregisterControls(ref this.localeComponentId);
        }

        void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Ignore;
            this.UnregisterControls(ref this.localeComponentId);
        }

        public NameValueCollection Datas
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
                    this.datas.Remove((string)this.listBox1.SelectedValue);
                    this.listBox1.DataSource = null;
                    this.listBox1.DataSource = this.datas.AllKeys;
                    this.listBox1.SelectedIndex = index - 1;
                    this.ValidateChildren();
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
                this.textBox2.Text = this.datas[(string)this.listBox1.SelectedValue];
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(this.textBox1.Text))
            {
                if (this.listBox1.SelectedIndex != -1)
                {
                    this.datas.Remove((string)this.listBox1.SelectedValue);
                    this.datas[(string)this.textBox1.Text] = this.textBox2.Text;
                    this.listBox1.DataSource = null;
                    this.listBox1.DataSource = this.datas.AllKeys;
                }
                else
                {
                    this.datas.Add(this.textBox1.Text, this.textBox2.Text);
                    this.listBox1.DataSource = null;
                    this.listBox1.DataSource = this.datas.AllKeys;
                }
                this.ValidateChildren();
                this.btnValidate1.SetDirty();
            }
        }

        private void ConfigView_Load(object sender, EventArgs e)
        {
            this.listBox1.DataSource = this.datas.AllKeys;
        }
    }
}
