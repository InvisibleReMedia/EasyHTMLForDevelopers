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
        private ZonesBinding rbWidth, rbHeight;

        public MasterObjectCreationForm()
        {
            InitializeComponent();
            rbWidth = new ZonesBinding(typeof(Library.EnumConstraint), this.masterObjectBindingSource, "ConstraintWidth", "Width");
            rbWidth.AddControlsIntoGroupBox(this.groupBox1, FlowDirection.LeftToRight);
            rbHeight = new ZonesBinding(typeof(Library.EnumConstraint), this.masterObjectBindingSource, "ConstraintHeight", "Height");
            rbHeight.AddControlsIntoGroupBox(this.groupBox2, FlowDirection.LeftToRight);
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
            if (this.MasterObject.CountColumns > 0 && this.MasterObject.CountLines > 0 && !String.IsNullOrEmpty(this.MasterObject.Name))
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
                this.UnregisterControls(ref this.localeComponentId);
            }
            else
            {
                MessageBox.Show(Localization.Strings.GetString("MissingData"), Localization.Strings.GetString("MissingDataTitle"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
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

    }
}
