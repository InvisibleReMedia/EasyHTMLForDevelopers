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
    public partial class MasterObjectCreationForm : Form
    {
        private int localeComponentId;

        public MasterObjectCreationForm()
        {
            InitializeComponent();
            this.RegisterControls(ref this.localeComponentId);
        }

        public Library.MasterObject MasterObject;

        private void MasterObjectCreationForm_Load(object sender, EventArgs e)
        {
            this.MasterObject = new Library.MasterObject();
            this.masterObjectBindingSource.DataSource = this.MasterObject;
        }

        private void btnValider_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
            this.UnregisterControls(ref this.localeComponentId);
        }

        private void btnAnnuler_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
            this.UnregisterControls(ref this.localeComponentId);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MasterObjectAdvanced advanced = new MasterObjectAdvanced();
            advanced.MasterObject = this.MasterObject;
            advanced.ShowDialog();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            this.MasterObject.ConstraintWidth = Library.EnumConstraint.AUTO;
            this.MasterObject.ConstraintHeight = Library.EnumConstraint.AUTO;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            this.MasterObject.ConstraintWidth = Library.EnumConstraint.FIXED;
            this.MasterObject.ConstraintHeight = Library.EnumConstraint.FIXED;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            this.MasterObject.ConstraintWidth = Library.EnumConstraint.RELATIVE;
            this.MasterObject.ConstraintHeight = Library.EnumConstraint.RELATIVE;
        }
    }
}
