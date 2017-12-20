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
    public partial class SculptureCreationForm : Form
    {
        private int localeComponentId;

        public SculptureCreationForm()
        {
            InitializeComponent();
            this.RegisterControls(ref this.localeComponentId);
        }

        public Library.SculptureObject SculptureObject;

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

        private void SculptureCreationForm_Load(object sender, EventArgs e)
        {
            this.SculptureObject = new Library.SculptureObject();
            this.sculptureObjectBindingSource.DataSource = this.SculptureObject;
        }

    }
}
