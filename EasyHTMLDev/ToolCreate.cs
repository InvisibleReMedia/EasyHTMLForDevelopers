using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Collections;

namespace EasyHTMLDev
{
    public partial class ToolCreate : Form
    {
        private int localeComponentId;

        public ToolCreate()
        {
            InitializeComponent();
            this.RegisterControls(ref this.localeComponentId);
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (Library.Project.AddTool(Library.Project.CurrentProject, new Library.HTMLTool(), this.txtName.Text))
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
                this.UnregisterControls(ref this.localeComponentId);
            }
            else
            {
                MessageBox.Show(Localization.Strings.GetString("ExceptionToolNotCreated"));
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
            this.UnregisterControls(ref this.localeComponentId);
        }
    }
}
