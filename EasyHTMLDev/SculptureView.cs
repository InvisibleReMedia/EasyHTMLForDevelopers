using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EasyHTMLDev
{
    public partial class SculptureView : Form
    {
        #region Private Fields
        private Library.SculptureObject sObject;
        private System.Reflection.PropertyInfo colorProperty;
        private object colorSource;

        private int localeComponentId;
        #endregion

        #region Public Constructor
        public SculptureView()
        {
            InitializeComponent();
            this.CurrentCadreModel.NewObject += CurrentCadreModel_NewObject;
            this.CurrentCadreModel.SuppressObject += CurrentCadreModel_SuppressObject;
            this.CurrentCadreModel.OpenModelType += CurrentCadreModel_OpenModelType;
            this.CurrentCadreModel.OpenColorScheme += CurrentCadreModel_OpenColorScheme;
            this.sheetModel.Close += sheetModel_Close;
            this.sheetColor.Close += sheetColor_Close;
            this.RegisterControls(ref this.localeComponentId);
        }
        #endregion

        #region Public Properties
        public float Ratio
        {
            get { return this.sPanel.Ratio; }
        }

        public Library.SculptureObject SculptureObject
        {
            get { return this.sObject; }
            set { this.sObject = value; }
        }
        #endregion

        #region Private Methods
        private void sheetModel_Close(object sender, EventArgs e)
        {
            this.sheetModel.Visible = false;
            this.hideControls.Visible = false;
            this.ControlBox = true;
            this.btnValidate1.SetDirty();
        }

        private void sheetColor_Close(object sender, EventArgs e)
        {
            this.sheetColor.Visible = false;
            this.hideControls.Visible = false;
            this.colorProperty.SetValue(this.colorSource, this.sheetColor.CurrentColor);
            this.ControlBox = true;
            this.btnValidate1.SetDirty();
        }

        private void CurrentCadreModel_OpenModelType(object sender, EventArgs e)
        {
            this.hideControls.Visible = true;
            this.sheetModel.Visible = true;
            this.sheetModel.BringToFront();
            this.sheetModel.ClearBindings();
            this.sheetModel.BindDataToControl(this.CurrentCadreModel.CurrentObject);
            this.ControlBox = false;
        }

        private void CurrentCadreModel_SuppressObject(object sender, EventArgs e)
        {
        }

        private void CurrentCadreModel_NewObject(object sender, EventArgs e)
        {
            CadreUC cu = new CadreUC(this.sPanel);
            cu.Moved += cu_Moved;
            cu.Resized += cu_Resized;
            cu.Selected += cu_Selected;
            cu.CrossRectanglePaint += cu_CrossRectanglePaint;
            cu.Properties.PropertyChanged += Properties_PropertyChanged;
            this.SculptureObject.AddNewCadreModel(cu.Properties);
            this.sPanel.Controls.Add(cu);
            this.CurrentCadreModel.CurrentObject = cu.Properties;
            this.btnValidate1.SetDirty();
        }

        private void CurrentCadreModel_OpenColorScheme(object sender, ColorEventArgs e)
        {
            this.hideControls.Visible = true;
            this.sheetColor.Visible = true;
            this.sheetColor.BringToFront();
            this.ControlBox = false;
            this.colorProperty = e.ColorProperty;
            this.colorSource = e.Source;
            this.sheetColor.CurrentColor = (Color)this.colorProperty.GetValue(e.Source);
        }

        void cu_CrossRectanglePaint(object sender, Library.CadreIndexPaintArgs e)
        {
            List<Library.Rectangle> res = this.SculptureObject.GetCrossRectList(e.CadreModel);
            foreach (Library.Rectangle r in res)
            {
                if (!r.IsEmpty())
                {
                    Rectangle rect = new Rectangle(new Point(r.Left, r.Top), new Size(r.Right - r.Left, r.Bottom - r.Top));
                    e.Paint.Graphics.FillRectangle(Brushes.DarkRed, rect);
                }
            }
        }

        private void Properties_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.btnValidate1.SetDirty();
        }

        private void cu_Selected(object sender, EventArgs e)
        {
            this.CurrentCadreModel.CurrentObject = (sender as CadreUC).Properties;
        }

        private void cu_Resized(object sender, MouseEventArgs e)
        {
            CadreUC uc = sender as CadreUC;
            uc.Width = e.X;
            uc.Height = e.Y;
        }

        private void cu_Moved(object sender, MouseEventArgs e)
        {
            CadreUC uc = sender as CadreUC;
            uc.Position = new Point(uc.Position.X + e.X, uc.Position.Y + e.Y);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.sPanel.Scale(this.CurrentCadreModel.cmbScale.SelectedIndex);
        }

        private void SculptureView_Load(object sender, EventArgs e)
        {
            this.SculptureObject.Reinit();
            foreach (Library.CadreModel cm in this.SculptureObject.Cadres)
            {
                CadreUC cu = new CadreUC(this.sPanel, cm);
                cu.Moved += cu_Moved;
                cu.Resized += cu_Resized;
                cu.Selected += cu_Selected;
                cu.CrossRectanglePaint += cu_CrossRectanglePaint;
                cu.Properties.PropertyChanged += Properties_PropertyChanged;
                this.sPanel.Controls.Add(cu);
            }
            Library.CadreModel.ReinitCounter(this.SculptureObject.Cadres.Count);
        }

        void SculptureObject_Added(object sender, Library.CadreIndexArgs e)
        {
        }

        private void SculptureView_FormClosed(object sender, FormClosedEventArgs e)
        {
            foreach (CadreUC cu in this.sPanel.Controls)
            {
                foreach (Library.CadreModelType ct in cu.Properties.ModelTypes)
                {
                    ct.Reinit();
                }
                cu.Properties.ClearEvents();
            }
            this.UnregisterControls(ref this.localeComponentId);
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            GenerateWithSculptureForm g = null;
            GenerateObjectForm select = null;
            DialogResult dr = System.Windows.Forms.DialogResult.None;
            int state = 0;
            do
            {
                if (state == 0)
                {
                    g = new GenerateWithSculptureForm();
                    dr = g.ShowDialog();
                    if (dr == System.Windows.Forms.DialogResult.OK) dr = System.Windows.Forms.DialogResult.Ignore;
                    state = 1;
                }
                else if (state == 1)
                {
                    string typeName = String.Empty;
                    int count = 0;
                    List<Library.GeneratedSculpture> data = null;
                    switch (g.cmbConversionType.SelectedIndex)
                    {
                        case 0:
                            typeName = Library.RefObject.MasterPage;
                            break;
                        case 1:
                            typeName = Library.RefObject.Page;
                            break;
                        case 2:
                            typeName = Library.RefObject.MasterObject;
                            break;
                        case 3:
                            typeName = Library.RefObject.Tool;
                            break;
                    }
                    data = this.SculptureObject.GetPreviousGeneration(typeName);
                    count = data.Count;
                    select = new GenerateObjectForm(typeName, count, data);
                    dr = select.ShowDialog();
                    if (dr == System.Windows.Forms.DialogResult.Retry) state = 0;
                    else if (dr == System.Windows.Forms.DialogResult.OK)
                    {
                        if (select.CreateNewGeneration)
                        {
                            this.SculptureObject.Generated.Add(select.DirectObject);
                        }
                        state = 2;
                        dr = System.Windows.Forms.DialogResult.Ignore;
                    }
                }
                else if (state == 2)
                {
                    // work to convert the sculpture to an existing object else a newer (use the form's Title property)
                    this.SculptureObject.CreateProjectObject(Library.Project.CurrentProject, select.DirectObject);
                    state = 3;
                    this.btnValidate1.SetDirty();
                }
            } while ((dr != System.Windows.Forms.DialogResult.OK && dr != System.Windows.Forms.DialogResult.Cancel) && state != 3);
        }
        #endregion

    }
}
