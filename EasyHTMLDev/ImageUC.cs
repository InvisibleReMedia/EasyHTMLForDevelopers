using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using CommonDirectories;

namespace EasyHTMLDev
{
    public partial class ImageUC : UserControl, IBindingUC
    {
        #region Inner Class
        internal class ImageFormatting : IFormatProvider
        {

            public object GetFormat(Type formatType)
            {
                throw new NotImplementedException();
            }
        }
        #endregion

        private int localeComponentId;

        public ImageUC()
        {
            InitializeComponent();
            this.RegisterControls(ref this.localeComponentId);
        }

        #region Private Methods
        private void LoadFileNames(List<string> list, Library.File f)
        {
        }

        private void btnBrowseImage_Click(object sender, EventArgs e)
        {
            FileImport fi = new FileImport();
            DialogResult dr = fi.ShowDialog();
            if (dr == DialogResult.OK)
            {
                
                ConfigDirectories.AddFile(Library.Project.CurrentProject.Title,
                                          Path.Combine(ConfigDirectories.GetBuildFolder(Library.Project.CurrentProject.Title),
                                                       fi.path.Text),
                                          fi.FileNames[0]);
                Library.Project.Save(Library.Project.CurrentProject, ConfigDirectories.GetDocumentsFolder(), AppDomain.CurrentDomain.GetData("fileName").ToString());
                Library.Project.CurrentProject.ReloadProject();
                this.pic.ImageLocation = fi.FileNames[0];
                int index = this.cmbFiles.Items.Add(fi.path.Text);
                this.cmbFiles.Text = fi.path.Text;
            }
        }

        #endregion

        #region IBindingUC Members
        public void ClearBindings()
        {
            this.pic.DataBindings.Clear();
            this.cmbFiles.DataBindings.Clear();
        }

        public void BindDataToControl(Library.CadreModelType data)
        {
        }
        #endregion

        private void cmbFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.pic.ImageLocation = ConfigDirectories.GetBuildFolder(Library.Project.CurrentProject.Title, ConfigDirectories.RemoveLeadSlash(this.cmbFiles.Text));
        }
    }
}
