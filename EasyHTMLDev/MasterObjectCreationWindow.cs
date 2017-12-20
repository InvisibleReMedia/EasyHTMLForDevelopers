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
    public partial class MasterObjectCreationWindow : Form
    {
        private Library.MasterObject mObject;
        private int localeComponentId;

        public MasterObjectCreationWindow()
        {
            InitializeComponent();
            this.RegisterControls(ref this.localeComponentId);
        }

        public MasterObjectCreationWindow(Library.MasterObject mObject)
        {
            this.mObject = mObject;
            InitializeComponent();
            this.RegisterControls(ref this.localeComponentId, mObject.Title);
        }

        public Library.MasterObject MasterObject
        {
            get { return this.mObject; }
        }

        private void btnValider_Click(object sender, EventArgs e)
        {
            this.panel.Confirm();
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

        private void MasterObjectCreationWindow_Load(object sender, EventArgs e)
        {
            this.panel.MasterObject = this.MasterObject;
            this.panel.initialize_cases();
        }
    }
}
