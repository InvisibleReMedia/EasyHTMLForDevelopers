using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Collections;
using Library;

namespace EasyHTMLDev
{
    public partial class ColorSchemePopup : UserControl
    {
        private DialogResult dialogResult;
        private ColorDialog cd;
        private event EventHandler close;
        private Color? currentColor;
        private List<int> colors;
        private SchemeEditor sEditor;
        private int localeComponentId;

        public ColorSchemePopup()
        {
            InitializeComponent();
            this.cd = new ColorDialog();
            this.sEditor = new SchemeEditor();
            this.sEditor.SchemeEditorChanged += sEditor_SchemeEditorChanged;
            this.tbPaletton.Content = sEditor;
            this.colors = new List<int>();
            this.RegisterControls(ref this.localeComponentId);
        }

        void sEditor_SchemeEditorChanged(object sender, EventArgs e)
        {
            Library.Project.CurrentProject.ColorScheme = this.sEditor.txtCSS.Text;
            this.colors.Clear();
            this.colors.AddRange(this.sEditor.Colors);
            this.DataBind();
            this.tbPaletton.IsUp = false;
        }

        public DialogResult DialogResult
        {
            get { return this.dialogResult; }
        }

        public Color? CurrentColor
        {
            get
            {
                return this.currentColor;
            }
            set
            {
                this.currentColor = value;
                this.DataBind();
            }
        }

        public int[] Colors
        {
            get { return this.colors.ToArray(); }
        }

        private void DataBind()
        {
            this.cmbColors.Items.Clear();
            foreach (int argb in this.colors)
            {
                Color c = Color.FromArgb(argb);
                this.cmbColors.Items.Add("#" + Hexadecimal.ToString(c.R, 2) + Hexadecimal.ToString(c.G, 2) + Hexadecimal.ToString(c.B, 2));
            }
            if (this.currentColor != null && this.currentColor.HasValue)
            {
                this.cmbColors.Text = "#" + Hexadecimal.ToString(this.currentColor.Value.R, 2) + Hexadecimal.ToString(this.currentColor.Value.G, 2) + Hexadecimal.ToString(this.currentColor.Value.B, 2);
            }
            else
            {
                this.cmbColors.Text = "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.currentColor != null && this.currentColor.HasValue)
            {
                this.cd.Color = this.currentColor.Value;
            }
            else
            {
                this.cd.Color = Color.Transparent;
            }
            this.cd.CustomColors = (from int i in this.colors select i).ToArray();
            DialogResult res = this.cd.ShowDialog();
            if (res == System.Windows.Forms.DialogResult.OK)
            {
                this.colors.Clear();
                this.colors.AddRange(this.cd.CustomColors);
                this.currentColor = this.cd.Color;
                this.DataBind();
            }
        }

        #region Public Events
        public event EventHandler Close
        {
            add { this.close += new EventHandler(value); }
            remove { this.close -= new EventHandler(value); }
        }
        #endregion

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.dialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.close(sender, e);
            this.UnregisterControls(ref this.localeComponentId);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.dialogResult = System.Windows.Forms.DialogResult.OK;
            Library.Project.CurrentProject.CustomColors.Clear();
            Library.Project.CurrentProject.CustomColors.AddRange(this.colors);
            Library.CSSColor c;
            if (!String.IsNullOrEmpty(this.cmbColors.Text))
            {
                if (Library.CSSColor.TryParse(this.cmbColors.Text, out c))
                {
                    this.currentColor = c.Color;
                }
                else
                {
                    this.currentColor = null;
                }
            }
            else
            {
                this.currentColor = null;
            }
            this.close(sender, e);
            this.UnregisterControls(ref this.localeComponentId);
        }

        private void ColorSchemePopup_Load(object sender, EventArgs e)
        {
            if (Library.Project.CurrentProject != null)
            {
                this.sEditor.txtCSS.Text = Library.Project.CurrentProject.ColorScheme;
                this.colors.Clear();
                foreach (int argb in Library.Project.CurrentProject.CustomColors)
                {
                    this.colors.Add(argb);
                }
                this.DataBind();
            }
        }
    }
}
