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
        private ZonesBinding rbWidth, rbHeight;

        public MasterPageCreationForm()
        {
            InitializeComponent();
            rbWidth = new ZonesBinding(typeof(Library.EnumConstraint), this.masterPageBindingSource, "ConstraintWidth", "Width");
            rbWidth.AddControlsIntoGroupBox(this.groupBox1, FlowDirection.LeftToRight);
            rbHeight = new ZonesBinding(typeof(Library.EnumConstraint), this.masterPageBindingSource, "ConstraintHeight", "Height");
            rbHeight.AddControlsIntoGroupBox(this.groupBox2, FlowDirection.LeftToRight);
            this.RegisterControls(ref this.localeComponentId);
        }

        public Library.MasterPage MasterPage
        {
            get { return this.masterPageBindingSource.DataSource as Library.MasterPage; }
        }

        private void validate_Click(object sender, EventArgs e)
        {
            if (this.MasterPage.CountColumns > 0 && this.MasterPage.CountLines > 0 && !String.IsNullOrEmpty(this.MasterPage.Name))
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

    }
}
