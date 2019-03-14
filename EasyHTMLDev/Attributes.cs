using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyHTMLDev
{
    public partial class Attributes : Form
    {

        private int localeComponentId;
        CSSOptions opt;
        Library.CodeCSS code;
        public event EventHandler modified;
        bool init = true;

        public Attributes()
        {
            InitializeComponent();
            this.RegisterControls(ref this.localeComponentId);
        }

        public Attributes(string name, params string[] pars)
        {
            InitializeComponent();
            this.Text = name;
            this.RegisterControls(ref this.localeComponentId, pars);
        }

        public Library.Attributes Attribs
        {
            get { return this.bsAttributes.DataSource as Library.Attributes; }
            set { this.bsAttributes.DataSource = value; }
        }

        public Library.CodeCSS CSS
        {
            get { return this.code; }
            set { this.code = value; }
        }

        public void UpdateText(string name, params string[] pars)
        {
            this.UpdateText(this.localeComponentId, name, pars);
        }

        private void Attributes_Load(object sender, EventArgs e)
        {
            this.Attribs.SetValues();
            this.rbAutomaticId.Checked = this.Attribs.IsAutomaticId;
            this.rbCustomId.Checked = !this.Attribs.IsAutomaticId;
            this.rbAutomaticClass.Checked = this.Attribs.IsAutomaticClass;
            this.rbCustomClass.Checked = !this.Attribs.IsAutomaticClass;
            this.rbClassCSS.Checked = this.Attribs.IsUsingClassForCSS;
            this.rbIdCSS.Checked = !this.Attribs.IsUsingClassForCSS;
            if (this.Attribs.HasId && !this.Attribs.IsAutomaticId)
                this.txtId.Enabled = true;
            else
                this.txtId.Enabled = false;
            if (this.Attribs.HasClass && !this.Attribs.IsAutomaticClass)
                this.txtClass.Enabled = true;
            else
                this.txtClass.Enabled = false;
            init = false;
        }

        private void txtId_Validating(object sender, CancelEventArgs e)
        {
            if (this.Attribs.HasId)
            {
                if (!this.Attribs.IsAutomaticId)
                {
                    Regex r = new Regex("[a-zA-Z_][0-9a-zA-Z_]*");
                    e.Cancel = !r.Match(this.txtId.Text).Success;
                    if (!e.Cancel && this.modified != null)
                        this.modified(this, new EventArgs());
                }
            }
        }

        private void txtClass_Validating(object sender, CancelEventArgs e)
        {
            if (this.Attribs.HasClass)
            {
                if (!this.Attribs.IsAutomaticClass)
                {
                    Regex r = new Regex(@"([a-zA-Z_][0-9a-zA-Z_#.]*)|\s+");
                    e.Cancel = !r.Match(this.txtId.Text).Success;
                    if (!e.Cancel && this.modified != null)
                        this.modified(this, new EventArgs());
                }
            }
        }

        private void rbAutomaticId_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAutomaticId.Checked)
            {
                this.Attribs.IsAutomaticId = true;
                this.txtId.Enabled = false;
            }
            else
            {
                this.Attribs.IsAutomaticId = false;
                this.txtId.Enabled = true;
            }
            this.bsAttributes.CurrencyManager.Refresh();
            this.Attribs.SetValues();
            if (!init && this.modified != null)
                this.modified(this, new EventArgs());
        }

        private void rbCustomId_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCustomId.Checked)
            {
                this.Attribs.IsAutomaticId = false;
                this.txtId.Enabled = true;
            }
            else
            {
                this.Attribs.IsAutomaticId = true;
                this.txtId.Enabled = false;
            }
            this.bsAttributes.CurrencyManager.Refresh();
            this.Attribs.SetValues();
            if (!init && this.modified != null)
                this.modified(this, new EventArgs());
        }

        private void rbAutomaticClass_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAutomaticClass.Checked)
            {
                this.Attribs.IsAutomaticClass = true;
                this.txtClass.Enabled = false;
            }
            else
            {
                this.Attribs.IsAutomaticClass = false;
                this.txtClass.Enabled = true;
            }
            this.bsAttributes.CurrencyManager.Refresh();
            this.Attribs.SetValues();
            if (!init && this.modified != null)
                this.modified(this, new EventArgs());
        }

        private void rbCustomClass_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCustomClass.Checked)
            {
                this.Attribs.IsAutomaticClass = false;
                this.txtClass.Enabled = true;
            }
            else
            {
                this.Attribs.IsAutomaticClass = true;
                this.txtClass.Enabled = false;
            }
            this.bsAttributes.CurrencyManager.Refresh();
            this.Attribs.SetValues();
            if (!init && this.modified != null)
                this.modified(this, new EventArgs());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
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
                opt.CSS = this.CSS;
                opt.modified += new EventHandler(z_modified);
                opt.Show();
            }
        }

        private void z_modified(object sender, EventArgs e)
        {
            if (modified != null)
                modified(sender, e);
        }

        private void cbId_CheckedChanged(object sender, EventArgs e)
        {
            this.Attribs.HasId = cbId.Checked;
            if (!init && modified != null)
                modified(sender, e);
        }

        private void cbClass_CheckedChanged(object sender, EventArgs e)
        {
            this.Attribs.HasClass = cbClass.Checked;
            if (!init && modified != null)
                modified(sender, e);
        }

        private void rbClassCSS_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbClassCSS.Checked)
                this.Attribs.IsUsingClassForCSS = true;
            if (!init && modified != null)
                modified(sender, e);
        }

        private void rbIdCSS_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbIdCSS.Checked)
                this.Attribs.IsUsingClassForCSS = false;
            if (!init && modified != null)
                modified(sender, e);
        }

        private void Attributes_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

    }
}
