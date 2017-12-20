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
    public partial class MasterPageOptions : Form
    {
        private int localeComponentId;

        public MasterPageOptions()
        {
            InitializeComponent();
            this.textBox1.DataBindings.Add("Enabled", this.cssOnFile, "Checked");
            this.textBox2.DataBindings.Add("Enabled", this.javascriptOnFile, "Checked");
            this.RegisterControls(ref this.localeComponentId);
        }

        public Library.MasterPage MasterPage;

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
            this.UnregisterControls(ref this.localeComponentId);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
            this.UnregisterControls(ref this.localeComponentId);
        }

        private void MasterPageOptions_Load(object sender, EventArgs e)
        {
            this.masterPageBindingSource.DataSource = this.MasterPage;
        }
    }
}
