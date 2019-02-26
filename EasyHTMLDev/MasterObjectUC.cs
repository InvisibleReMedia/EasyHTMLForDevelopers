using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommonDirectories;

namespace EasyHTMLDev
{
    public partial class MasterObjectUC : UserControl, IBindingUC
    {
        private int localeComponentId;

        public MasterObjectUC()
        {
            InitializeComponent();
            this.RegisterControls(ref this.localeComponentId);
        }

        public void ClearBindings()
        {
            this.cmbMasterObjects.DataBindings.Clear();
        }

        public void BindDataToControl(Library.CadreModelType data)
        {
            this.cmbMasterObjects.Items.AddRange(Library.Project.CurrentProject.MasterObjects.ConvertAll(s =>
            {
                return new Library.CadreModelType(Library.CadreModelType.MasterObject, s.Title, s);
            }).ToArray());
            this.cmbMasterObjects.DisplayMember = "Content";
            this.cmbMasterObjects.DataBindings.Add("SelectedItem", data, "DirectObject");
        }

        private void btnCreateMasterObject_Click(object sender, EventArgs e)
        {
            MasterObjectCreationForm creation = new MasterObjectCreationForm();
            DialogResult dr = creation.ShowDialog();
            if (dr == DialogResult.OK)
            {
                Library.Project proj = Library.Project.CurrentProject;
                MasterObjectCreationWindow window = new MasterObjectCreationWindow(creation.MasterObject);
                if (!creation.MasterObject.RelativeHeight)
                    window.Height = (int)creation.MasterObject.Height;
                else
                    window.Height = 500;
                if (!creation.MasterObject.RelativeWidth)
                    window.Width = (int)creation.MasterObject.Width;
                else
                    window.Width = 500;
                window.WindowState = FormWindowState.Normal;
                DialogResult dr2 = window.ShowDialog();
                if (dr2 == DialogResult.OK)
                {
                    string[] splitted = creation.MasterObject.Title.Split('/');
                    string path = String.Join("/", splitted.Take(splitted.Count() - 1).ToArray());
                    creation.MasterObject.Title = splitted.Last();
                    proj.Add(creation.MasterObject, path);
                    Library.Project.Save(proj, ConfigDirectories.GetDocumentsFolder(), AppDomain.CurrentDomain.GetData("fileName").ToString());
                    proj.ReloadProject();
                }
                int index = this.cmbMasterObjects.Items.Add(creation.txtTitle.Text);
                this.cmbMasterObjects.Text = creation.txtTitle.Text;
            }
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            base.OnHandleDestroyed(e);
            this.UnregisterControls(ref this.localeComponentId);
        }
    }
}
