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
    public partial class MasterPageCreationWindow : Form
    {
        private Library.MasterPage mPage;
        private int localeComponentId;

        public MasterPageCreationWindow()
        {
            InitializeComponent();
            this.RegisterControls(ref this.localeComponentId);
        }

        public MasterPageCreationWindow(Library.MasterPage mp)
        {
            this.mPage = mp;
            InitializeComponent();
            this.RegisterControls(ref this.localeComponentId, mp.Name);
        }

        private void MasterPageWindow_Load(object sender, EventArgs e)
        {
            this.panel.MasterPage = this.mPage;
            this.panel.init();
        }

        private void ok_Click(object sender, EventArgs e)
        {
            this.panel.Confirm();
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
