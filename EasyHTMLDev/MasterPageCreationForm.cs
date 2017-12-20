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
    public partial class MasterPageCreationForm : Form
    {
        private int localeComponentId;

        public MasterPageCreationForm()
        {
            InitializeComponent();
            this.RegisterControls(ref this.localeComponentId);
        }

        public Library.MasterPage MasterPage
        {
            get { return this.masterPageBindingSource.DataSource as Library.MasterPage; }
        }

        private void validate_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
            this.UnregisterControls(ref this.localeComponentId);
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
            this.UnregisterControls(ref this.localeComponentId);
        }

        private void MasterPageForm_Load(object sender, EventArgs e)
        {
            this.masterPageBindingSource.DataSource = new Library.MasterPage();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            this.MasterPage.ConstraintHeight = Library.EnumConstraint.AUTO;
            this.MasterPage.ConstraintWidth = Library.EnumConstraint.AUTO;
            this.width.Enabled = false;
            this.height.Enabled = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            this.MasterPage.ConstraintHeight = Library.EnumConstraint.FIXED;
            this.MasterPage.ConstraintWidth = Library.EnumConstraint.FIXED;
            this.width.Enabled = true;
            this.height.Enabled = true;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            this.MasterPage.ConstraintHeight = Library.EnumConstraint.RELATIVE;
            this.MasterPage.ConstraintWidth = Library.EnumConstraint.RELATIVE;
            this.width.Enabled = true;
            this.height.Enabled = true;
        }
    }
}
