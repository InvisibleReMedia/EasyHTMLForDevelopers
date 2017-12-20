using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CommonDirectories;

namespace EasyHTMLDev
{
    public partial class BtnValidate : UserControl
    {
        private delegate void InvokeClose();
        private EventHandler save;
        private InvokeClose close;
        private int localeComponentId;

        public BtnValidate()
        {
            InitializeComponent();
            this.close = new InvokeClose(invokeClose);
            this.RegisterControls(ref this.localeComponentId);
        }

        private void BtnValidate_Load(object sender, EventArgs e)
        {
            this.ParentForm.AcceptButton = this.btnOK;
            this.ParentForm.CancelButton = this.btnOK;
            this.labelGreen.Visible = true;
            this.btnOK.Visible = true;
            this.labelRed.Visible = false;
            this.btnValider.Visible = false;
            this.btnAnnuler.Visible = false;
        }

        public event EventHandler Save
        {
            add
            {
                this.save += value;
            }
            remove
            {
                this.save -= value;
            }
        }

        public void SetDirty()
        {
            this.ParentForm.AcceptButton = new BtnValiderControl(this);
            this.ParentForm.CancelButton = new BtnAnnulerControl(this);
            this.labelRed.Visible = true;
            this.labelGreen.Visible = false;
            this.btnValider.Visible = true;
            this.btnAnnuler.Visible = true;
            this.btnOK.Visible = false;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.ParentForm.DialogResult = DialogResult.Ignore;
            this.BeginInvoke(this.close);
            this.UnregisterControls(ref this.localeComponentId);
        }

        private void btnValider_Click(object sender, EventArgs e)
        {
            if (this.save != null)
                this.save(this, new EventArgs());
            // sauvegarder les données
            Library.Project.Save(Library.Project.CurrentProject, ConfigDirectories.GetDocumentsFolder(), AppDomain.CurrentDomain.GetData("fileName").ToString());
            Library.Project.CurrentProject.ReloadProject();
            this.ParentForm.DialogResult = DialogResult.OK;
            this.BeginInvoke(this.close);
            this.UnregisterControls(ref this.localeComponentId);
        }

        private void btnAnnuler_Click(object sender, EventArgs e)
        {
            // recharger le projet uniquement si aucune autre fenetre ouverte
            DialogResult dr = DialogResult.OK;
            if (Application.OpenForms["Form1"].MdiChildren.Length > 1)
            {
                dr = MessageBox.Show(Localization.Strings.GetString("CarefullWindowsOpenText"), Localization.Strings.GetString("CarefullWindowsOpenTitle"), MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            }
            if (dr == DialogResult.OK)
            {
                Library.Project.Load(ConfigDirectories.GetDocumentsFolder(), AppDomain.CurrentDomain.GetData("fileName").ToString());
                Library.Project.CurrentProject.ReloadProject();
            }
            this.ParentForm.DialogResult = DialogResult.Cancel;
            this.BeginInvoke(this.close);
            this.UnregisterControls(ref this.localeComponentId);
        }

        private void invokeClose()
        {
            this.ParentForm.Close();
        }
    }

    class BtnValiderControl : IButtonControl
    {
        private BtnValidate _control;

        public BtnValiderControl(BtnValidate btn)
        {
            this._control = btn;
        }

        public DialogResult DialogResult
        {
            get
            {
                return System.Windows.Forms.DialogResult.OK;
            }
            set
            {
            }
        }

        public void NotifyDefault(bool value)
        {
            this._control.btnValider.NotifyDefault(value);
        }

        public void PerformClick()
        {
            this._control.btnValider.PerformClick();
        }
    }

    class BtnAnnulerControl : IButtonControl
    {
        private BtnValidate _control;

        public BtnAnnulerControl(BtnValidate btn)
        {
            this._control = btn;
        }

        public DialogResult DialogResult
        {
            get
            {
                return System.Windows.Forms.DialogResult.Cancel;
            }
            set
            {
            }
        }

        public void NotifyDefault(bool value)
        {
            this._control.btnAnnuler.NotifyDefault(value);
        }

        public void PerformClick()
        {
            this._control.btnAnnuler.PerformClick();
        }
    }
}
