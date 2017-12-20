using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyHTMLDev
{
    public partial class GenerateWithSculptureForm : Form
    {
        private int localeComponentId;

        public GenerateWithSculptureForm()
        {
            InitializeComponent();
            this.RegisterControls(ref this.localeComponentId);
            string items = Localization.Strings.GetString("ConversionType");
            string[] tab = items.Split(Environment.NewLine.ToArray());
            foreach (string s in tab)
            {
                if (!String.IsNullOrEmpty(s))
                    this.cmbConversionType.Items.Add(s);
            }
        }

        private void btnAnnuler_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
            this.UnregisterControls(ref this.localeComponentId);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (this.cmbConversionType.SelectedIndex != -1)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
                this.UnregisterControls(ref this.localeComponentId);
            }
            else
            {
                this.epType.SetError(this.cmbConversionType, Localization.Strings.GetString("EmptyField"));
            }
        }
    }
}
