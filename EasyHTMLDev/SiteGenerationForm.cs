using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using CommonDirectories;

namespace EasyHTMLDev
{
    public partial class SiteGenerationForm : Form
    {
        private int localeComponentId;

        public SiteGenerationForm()
        {
            InitializeComponent();
            ffd.SelectedPath = ConfigDirectories.GetDefaultProductionFolder(Library.Project.CurrentProject.Title);
            this.RegisterControls(ref this.localeComponentId);
        }

        private void browse_Click(object sender, EventArgs e)
        {
            DialogResult dr = ffd.ShowDialog();
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
