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
    public partial class OutilUC : UserControl, IBindingUC
    {
        #region Internal Class
        internal class Tool
        {
            #region Private Fields
            private string title;
            private Library.HTMLTool tool;
            #endregion

            #region Public Constructor
            public Tool(string title, Library.HTMLTool tool)
            {
                this.title = title;
                this.tool = tool;
            }
            #endregion

            #region Public Properties
            public string Title
            {
                get { return this.title; }
            }

            public Library.HTMLTool DirectObject
            {
                get { return this.tool; }
            }
            #endregion
        }
        #endregion

        private int localeComponentId;

        public OutilUC()
        {
            InitializeComponent();
            this.RegisterControls(ref this.localeComponentId);
        }

        private void LoadTools(List<Tool> list, Library.FolderTool t)
        {
            list.AddRange(from Library.HTMLTool s in t.Tools select new Tool(t.Name + "/" + s.Title, s));
            foreach (Library.FolderTool sub in t.Folders)
            {
                this.LoadTools(list, sub);
            }
        }

        public void ClearBindings()
        {
            this.cmbTools.DataBindings.Clear();
        }

        public void BindDataToControl(Library.CadreModelType data)
        {
            List<Tool> tools = new List<Tool>();
            this.LoadTools(tools, Library.Project.CurrentProject.Tools);
            if (tools.Count > 0 && this.cmbTools.Items.Count == 0)
            {
                this.cmbTools.Items.AddRange(tools.ConvertAll(s =>
                {
                    return new Library.CadreModelType(Library.CadreModelType.Tool, s.Title, s.DirectObject);
                }).ToArray());
            }
            this.cmbTools.DisplayMember = "Content";
            this.cmbTools.DataBindings.Add("SelectedItem", data, "DirectObject");
        }

        private void btnCreateTool_Click(object sender, EventArgs e)
        {
            ToolCreate tc = new ToolCreate();
            DialogResult dr = tc.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                Library.Project.Save(Library.Project.CurrentProject, ConfigDirectories.GetDocumentsFolder(), AppDomain.CurrentDomain.GetData("fileName").ToString());
                Library.Project.CurrentProject.ReloadProject();
                int index = this.cmbTools.Items.Add(tc.txtName.Text);
                this.cmbTools.Text = tc.txtName.Text;
            }
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            base.OnHandleDestroyed(e);
            this.UnregisterControls(ref this.localeComponentId);
        }
    }
}
