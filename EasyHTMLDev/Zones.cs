using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
using CommonDirectories;

namespace EasyHTMLDev
{
    public partial class Zones : Form
    {
        private Library.MasterPage mPage;
        private Library.MasterObject mObject;
        private Attributes optHoriz, optVert;
        private BindingSource bsHoriz, bsVert;
        public event EventHandler modified;
        private int localeComponentId;
        private ZonesBinding rbHorizWidth, rbHorizHeight, rbVertWidth, rbVertHeight;

        public Zones()
        {
            InitializeComponent();
            this.bsHoriz = new BindingSource();
            this.bsHoriz.DataMember = "HorizontalZones";
            this.bsHoriz.CurrentItemChanged += new EventHandler(bsHoriz_CurrentItemChanged);
            this.bsVert = new BindingSource();
            this.bsVert.DataMember = "VerticalZones";
            this.bsVert.CurrentItemChanged += new EventHandler(bsVert_CurrentItemChanged);
            this.bsZoneHoriz.RaiseListChangedEvents = false;
            this.bsZoneHoriz.DataSourceChanged += new EventHandler(bsZoneHoriz_DataSourceChanged);
            this.bsZoneVert.RaiseListChangedEvents = false;
            this.bsZoneVert.DataSourceChanged += new EventHandler(bsZoneVert_DataSourceChanged);
            this.rbHorizWidth = new ZonesBinding(typeof(Library.EnumConstraint), this.bsZoneHoriz, "ConstraintWidth", "Width");
            rbHorizWidth.modified += rb_modified;
            rbHorizWidth.AddControlsIntoGroupBox(this.grpHLng, FlowDirection.LeftToRight);
            this.rbHorizHeight = new ZonesBinding( typeof(Library.EnumConstraint), this.bsZoneHoriz, "ConstraintHeight", "Height");
            rbHorizHeight.modified += rb_modified;
            rbHorizHeight.AddControlsIntoGroupBox(this.grpHHt, FlowDirection.LeftToRight);
            this.rbVertWidth = new ZonesBinding(typeof(Library.EnumConstraint), this.bsZoneVert, "ConstraintWidth", "Width");
            rbVertWidth.modified += rb_modified;
            rbVertWidth.AddControlsIntoGroupBox(this.grpVLng, FlowDirection.LeftToRight);
            this.rbVertHeight = new ZonesBinding(typeof(Library.EnumConstraint), this.bsZoneVert, "ConstraintHeight", "Height");
            rbVertHeight.modified += rb_modified;
            rbVertHeight.AddControlsIntoGroupBox(this.grpVHt, FlowDirection.LeftToRight);
            this.RegisterControls(ref this.localeComponentId);
        }

        void rb_modified(object sender, EventArgs e)
        {
            if (this.modified != null)
            {
                this.modified(sender, e);
            }
        }

        void bsZoneVert_DataSourceChanged(object sender, EventArgs e)
        {
            Library.VerticalZone vert = this.bsVert.Current as Library.VerticalZone;
            if (vert != null)
            {
                this.btns.SelectedName = vert.DispositionText;
                this.button1.Image = this.btns.SelectedImage;
                this.grpVLng.Enabled = true;
                this.grpVHt.Enabled = true;
                this.btnOptionsVert.Enabled = true;
                this.cssVert.Enabled = true;
                this.cssVert.Text = "";
                foreach (string key in vert.CSS.Body.AllKeys)
                {
                    this.cssVert.Text += key + ":" + vert.CSS.Body[key] + ";" + Environment.NewLine;
                }
                if (this.optVert != null)
                {
                    this.optVert.Text = String.Format(Localization.Strings.GetString("VerticalAreaStringified"), vert.Name, vert.CountLines, vert.CountColumns);
                    this.optVert.CSS = vert.CSS;
                }
            }
            else
            {
                this.grpVLng.Enabled = false;
                this.grpVHt.Enabled = false;
                this.btnOptionsVert.Enabled = false;
                this.cssVert.Enabled = false;
                this.cssVert.Text = "";
                if (this.optVert != null)
                    this.optVert.Hide();
            }
        }

        void bsZoneHoriz_DataSourceChanged(object sender, EventArgs e)
        {
            Library.HorizontalZone horiz = this.bsHoriz.Current as Library.HorizontalZone;
            if (horiz != null)
            {
                this.grpHHt.Enabled = true;
                this.grpHLng.Enabled = true;
                this.btnOptionsHoriz.Enabled = true;
                this.cssHoriz.Enabled = true;
                this.cssHoriz.Text = "";
                foreach (string key in horiz.CSS.Body.AllKeys)
                {
                    this.cssHoriz.Text += key + ":" + horiz.CSS.Body[key] + ";" + Environment.NewLine;
                }
                this.bsVert.DataSource = this.bsHoriz.Current;
                this.lstVert.DataSource = this.bsVert;
                if (this.optHoriz != null)
                {
                    this.optHoriz.Text = String.Format(Localization.Strings.GetString("HorizontalAreaStringified"), horiz.Name, horiz.CountLines);
                    this.optHoriz.CSS = horiz.CSS;
                }
            }
            else
            {
                this.btnOptionsHoriz.Enabled = false;
                this.grpHHt.Enabled = false;
                this.grpHLng.Enabled = false;
                this.cssHoriz.Enabled = false;
                this.cssHoriz.Text = "";
                if (this.optHoriz != null)
                    this.optHoriz.Hide();
            }
        }

        void CurrentItemChanged(object sender, EventArgs e)
        {
            if (this.modified != null)
                this.modified(this, e);
        }

        public Library.MasterPage MasterPage
        {
            set { this.mPage = value; }
        }

        public Library.MasterObject MasterObject
        {
            set { this.mObject = value; }
        }

        public void SetDirty()
        {
            if (this.modified != null)
            {
                this.modified(this, new EventArgs());
            }
        }

        private void ReloadBrowser(bool firstLoad = false)
        {
            try
            {
                string fileName = Path.Combine(ConfigDirectories.GetBuildFolder(Library.Project.CurrentProject.Title), "horiz.html");
                Library.OutputHTML html = (this.bsHoriz.Current as Library.HorizontalZone).GenerateDesign();
                FileStream fs = new FileStream(fileName, FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(html.HTML.ToString());
                sw.Close();
                sw.Dispose();
                fs.Close();
                fs.Dispose();
                this.webBrowser1.Navigate(fileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Zones_Load(object sender, EventArgs e)
        {
            this.btns.selectedChanged += new EventHandler(btns_selectedChanged);
            if (this.mPage != null)
            {
                this.bsHoriz.DataSource = this.mPage.HorizontalZones;
                this.lstHoriz.DisplayMember = "Stringified";
                this.lstHoriz.ValueMember = "Name";
                this.lstHoriz.DataSource = this.bsHoriz;
                this.lstVert.DisplayMember = "Stringified";
                this.lstVert.ValueMember = "Name";
            }
            else if (this.mObject != null)
            {
                this.bsHoriz.DataSource = this.mObject.HorizontalZones;
                this.lstHoriz.DisplayMember = "Stringified";
                this.lstHoriz.ValueMember = "Name";
                this.lstHoriz.DataSource = this.bsHoriz;
                this.lstVert.DisplayMember = "Stringified";
                this.lstVert.ValueMember = "Name";
            }
            this.bsZoneHoriz.CurrentItemChanged += new EventHandler(CurrentItemChanged);
            this.bsZoneVert.CurrentItemChanged += new EventHandler(CurrentItemChanged);
            this.ReloadBrowser(true);
        }

        void bsHoriz_CurrentItemChanged(object sender, EventArgs e)
        {
            this.bsZoneHoriz.DataSource = this.bsHoriz.Current;
            this.bsVert.DataSource = this.bsHoriz.Current;
            this.lstVert.DataSource = this.bsVert;
            this.ReloadBrowser();
        }

        void bsVert_CurrentItemChanged(object sender, EventArgs e)
        {
            this.bsZoneVert.DataSource = this.bsVert.Current;
        }

        void btns_selectedChanged(object sender, EventArgs e)
        {
            this.button1.Image = ((Button)sender).Image;
            if (this.bsVert.Current != null)
            {
                (this.bsVert.Current as Library.VerticalZone).DispositionText = this.btns.SelectedName;
            }
            if (this.modified != null) this.modified(this, e);
            this.btns.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!this.btns.Visible)
                this.btns.Show();
            else
                this.btns.Hide();
        }

        private void btnOptionsVert_Click(object sender, EventArgs e)
        {
            if (this.optVert != null && !this.optVert.IsDisposed)
            {
                Library.VerticalZone vz = this.bsVert.Current as Library.VerticalZone;
                this.optVert.Text = String.Format(Localization.Strings.GetString("VerticalAreaStringified"), vz.Name, vz.CountLines, vz.CountColumns);
                this.optVert.CSS = vz.CSS;
                this.optVert.Attribs = vz.Attributes;
                this.optVert.Show();
            }
            else
            {
                this.optVert = new Attributes();
                Library.VerticalZone vz = this.bsVert.Current as Library.VerticalZone;
                this.optVert.Text = String.Format(Localization.Strings.GetString("VerticalAreaStringified"), vz.Name, vz.CountLines, vz.CountColumns);
                this.optVert.CSS = vz.CSS;
                this.optVert.Attribs = vz.Attributes;
                this.optVert.modified += new EventHandler(CurrentItemChanged);
                this.optVert.Show();
            }
        }

        private void btnOptionsHoriz_Click(object sender, EventArgs e)
        {
            if (this.optHoriz != null && !this.optHoriz.IsDisposed)
            {
                Library.HorizontalZone hz = this.bsHoriz.Current as Library.HorizontalZone;
                this.optHoriz.Text = String.Format(Localization.Strings.GetString("HorizontalAreaStringified"), hz.Name, hz.CountLines);
                optHoriz.CSS = hz.CSS;
                optHoriz.Attribs = hz.Attributes;
                this.optHoriz.Show();
            }
            else
            {
                this.optHoriz = new Attributes();
                Library.HorizontalZone hz = this.bsHoriz.Current as Library.HorizontalZone;
                this.optHoriz.Text = String.Format(Localization.Strings.GetString("HorizontalAreaStringified"), hz.Name, hz.CountLines);
                optHoriz.CSS = hz.CSS;
                optHoriz.Attribs = hz.Attributes;
                optHoriz.modified += new EventHandler(this.CurrentItemChanged);
                optHoriz.Show();
            }
        }

        private void cssVert_Validating(object sender, CancelEventArgs e)
        {
            if (this.bsVert.Current != null)
            {
                string reason = String.Empty;
                Library.VerticalZone v = this.bsVert.Current as Library.VerticalZone;
                if (Library.CSSValidation.CSSValidate(this.cssVert.Text, false, out reason, v.CSS))
                {
                    if (this.modified != null)
                    {
                        this.modified(this, e);
                    }
                }
            }
        }

        private void cssHoriz_Validating(object sender, CancelEventArgs e)
        {
            if (this.bsHoriz.Current != null)
            {
                string reason = String.Empty;
                Library.HorizontalZone h = (this.bsHoriz.Current) as Library.HorizontalZone;
                if (Library.CSSValidation.CSSValidate(this.cssHoriz.Text, false, out reason, h.CSS))
                {
                    if (this.modified != null)
                    {
                        this.modified(this, e);
                    }
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Zones_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.UnregisterControls(ref this.localeComponentId);
            this.rbHorizHeight.Close();
            this.rbHorizWidth.Close();
            this.rbVertHeight.Close();
            this.rbVertWidth.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.ReloadBrowser();
        }
    }
}
