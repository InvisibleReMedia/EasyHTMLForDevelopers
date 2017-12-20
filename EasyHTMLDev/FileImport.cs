using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace EasyHTMLDev
{
    public partial class FileImport : Form
    {
        private int localeComponentId;

        public FileImport()
        {
            InitializeComponent();
            this.RegisterControls(ref this.localeComponentId);
        }

        private void browse_Click(object sender, EventArgs e)
        {
            DialogResult dr = ofd.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                this.path.Text = Path.Combine(this.path.Text, Path.GetFileName(ofd.FileName));
            }
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
