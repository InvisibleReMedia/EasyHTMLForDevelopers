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
    public partial class MasterObjectAdvanced : Form
    {
        private int localeComponentId;

        public MasterObjectAdvanced()
        {
            InitializeComponent();
            this.RegisterControls(ref this.localeComponentId);
        }

        public Library.MasterObject MasterObject
        {
            set { this.masterObjectBindingSource.DataSource = value; }
        }

        private void valider_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
            this.UnregisterControls(ref this.localeComponentId);
        }

        private void annuler_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
            this.UnregisterControls(ref this.localeComponentId);
        }
    }
}
