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
    public partial class GenerateObjectForm : Form
    {
        private string type;
        private string title;
        private Library.GeneratedSculpture sculpture;
        private int localeComponentId;

        public GenerateObjectForm(string type, int totalCountItems, List<Library.GeneratedSculpture> data)
        {
            this.type = type;
            InitializeComponent();
            this.RegisterControls(ref this.localeComponentId);
            if (totalCountItems > 0)
            {
                this.newGeneration1.Visible = false;
                this.overwriteGeneration1.Visible = true;
                this.overwriteGeneration1.cmbVersions.DisplayMember = "Name";
                this.overwriteGeneration1.cmbVersions.DataSource = data;
                Binding b = new Binding("Text", this, "Title");
                b.ControlUpdateMode = ControlUpdateMode.Never;
                this.overwriteGeneration1.txtNew.DataBindings.Add(b);
                b = new Binding("SelectedItem", this, "DirectObject");
                b.ControlUpdateMode = ControlUpdateMode.Never;
                this.overwriteGeneration1.cmbVersions.DataBindings.Add(b);
                this.overwriteGeneration1.cmbVersions.SelectedIndex = 0;
            }
            else
            {
                this.newGeneration1.Visible = true;
                this.newGeneration1.txtNew.DataBindings.Add("Text", this, "Title");
                this.overwriteGeneration1.Visible = false;
            }
        }

        public bool CreateNewGeneration
        {
            get { return !this.overwriteGeneration1.Visible || this.overwriteGeneration1.rbNew.Checked; }
        }

        public string Title
        {
            get
            {
                return this.title;
            }
            set
            {
                this.title = value;
                if (String.IsNullOrWhiteSpace(this.title))
                {
                    this.btnNext.Enabled = false;
                }
                else
                {
                    this.btnNext.Enabled = true;
                }
            }
        }

        public Library.GeneratedSculpture DirectObject
        {
            get { return this.sculpture; }
            set
            {
                this.sculpture = value;
                if (this.sculpture != null)
                {
                    this.btnNext.Enabled = true;
                }
                else
                {
                    this.btnNext.Enabled = false;
                }
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (this.CreateNewGeneration)
            {
                this.sculpture = new Library.GeneratedSculpture(this.title, this.type);
            }
            else
            {
                this.title = this.sculpture.Name;
            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
            this.UnregisterControls(ref this.localeComponentId);
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Retry;
            this.Close();
            this.UnregisterControls(ref this.localeComponentId);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
            this.UnregisterControls(ref this.localeComponentId);
        }
    }
}
