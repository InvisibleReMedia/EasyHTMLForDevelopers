using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EasyHTMLDev
{
    public partial class BtnClose : UserControl
    {
        private delegate void InvokeClose();
        private InvokeClose close;
        private int localeComponentId;

        public BtnClose()
        {
            InitializeComponent();
            this.close = new InvokeClose(invokeClose);
            this.RegisterControls(ref this.localeComponentId);
        }

        private void BtnValidate_Load(object sender, EventArgs e)
        {
            this.labelGreen.Visible = true;
            this.btnOK.Visible = true;
            this.labelRed.Visible = false;
            this.btnFermer.Visible = false;
        }

        public void SetDirty()
        {
            this.ParentForm.AcceptButton = this.btnFermer;
            this.labelRed.Visible = true;
            this.labelGreen.Visible = false;
            this.btnFermer.Visible = true;
            this.btnOK.Visible = false;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.UnregisterControls(ref this.localeComponentId);
            this.ParentForm.DialogResult = DialogResult.Ignore;
            this.BeginInvoke(this.close);
        }

        private void btnFermer_Click(object sender, EventArgs e)
        {
            this.UnregisterControls(ref this.localeComponentId);
            this.ParentForm.DialogResult = DialogResult.OK;
            this.BeginInvoke(this.close);
        }

        private void invokeClose()
        {
            this.ParentForm.Close();
        }
    }
}
