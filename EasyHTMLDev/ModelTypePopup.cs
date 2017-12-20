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

namespace EasyHTMLDev
{
    public partial class ModelTypePopup : UserControl
    {
        #region Private Fields
        private List<Library.CadreModelType> modelTypes;
        private UserControl[] ucRefs;
        private TabPage[] tabRefs;
        private event EventHandler close;
        private int localeComponentId;

        #endregion

        #region Public Constructor
        public ModelTypePopup()
        {
            InitializeComponent();
            this.ucRefs = new UserControl[5];
            this.tabRefs = new TabPage[5];
            this.ucRefs[0] = new ImageUC();
            this.tabRefs[0] = this.tabImage;
            this.tabRefs[0].Controls.Add(this.ucRefs[0]);
            this.ucRefs[1] = new TexteUC();
            this.tabRefs[1] = this.tabText;
            this.tabRefs[1].Controls.Add(this.ucRefs[1]);
            this.ucRefs[2] = new OutilUC();
            this.tabRefs[2] = this.tabOutil;
            this.tabRefs[2].Controls.Add(this.ucRefs[2]);
            this.ucRefs[3] = new MasterObjectUC();
            this.tabRefs[3] = this.tabMasterObject;
            this.tabRefs[3].Controls.Add(this.ucRefs[3]);
            this.ucRefs[4] = new DynamicUC();
            this.tabRefs[4] = this.tabDynamic;
            this.tabRefs[4].Controls.Add(this.ucRefs[4]);
            string items = Localization.Strings.GetString("ObjectTypeList");
            if (!String.IsNullOrEmpty(items))
            {
                string[] tab = items.Split(Environment.NewLine.ToArray());
                foreach (string s in tab)
                {
                    this.cmbType.Items.Add(s);
                }
                if (cmbType.Items.Count > 4)
                    this.cmbType.SelectedIndex = 4;
            }

            this.RegisterControls(ref this.localeComponentId);
        }
        #endregion

        #region Public Properties
        public List<Library.CadreModelType> Models
        {
            get { return this.modelTypes; }
        }
        #endregion

        #region Public Methods
        public void ClearBindings()
        {
            this.cmbType.DataBindings.Clear();
            this.txtName.DataBindings.Clear();
            foreach (IBindingUC t in this.ucRefs)
            {
                t.ClearBindings();
            }
        }

        public void BindDataToControl(Library.CadreModel cm)
        {
            this.modelTypes = new List<Library.CadreModelType>();
            this.modelTypes.AddRange(from Library.CadreModelType m in cm.ModelTypes select (m.Clone() as Library.CadreModelType));
            this.cmbType.DataSource = this.modelTypes;
            this.cmbType.DisplayMember = "Type";
            this.cmbType.DataBindings.Add("SelectedIndex", cm, "SelectedModelTypeIndex");
            for(int index = 0; index < this.modelTypes.Count; ++index)
            {
                IBindingUC t = this.ucRefs[index] as IBindingUC;
                t.BindDataToControl(this.modelTypes[index]);
            }
            this.txtName.DataBindings.Add("Text", cm, "Name");
        }

        #endregion

        #region Private Methods
        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.tabOptions.SelectedTab = this.tabRefs[this.cmbType.SelectedIndex];
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Hide();
            (this.cmbType.DataBindings[0].DataSource as Library.CadreModel).SelectedModelTypeIndex = this.cmbType.SelectedIndex;
            (this.cmbType.DataBindings[0].DataSource as Library.CadreModel).ModelTypes.Clear();
            (this.cmbType.DataBindings[0].DataSource as Library.CadreModel).ModelTypes.AddRange(from Library.CadreModelType m in this.modelTypes select (m.Clone() as Library.CadreModelType));
            this.close(sender, e);
            this.UnregisterControls(ref this.localeComponentId);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.close(sender, e);
            this.UnregisterControls(ref this.localeComponentId);
        }
        #endregion

        #region Public Events
        public event EventHandler Close
        {
            add { this.close += new EventHandler(value); }
            remove { this.close -= new EventHandler(value); }
        }
        #endregion
    }
}
