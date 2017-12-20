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
    public partial class CSSOptions : Form
    {
        public event EventHandler modified;
        private int localeComponentId;
        private ColorSchemePopup sheetColor;

        public CSSOptions()
        {
            InitializeComponent();
            this.RegisterControls(ref this.localeComponentId);
            Binding bBorder = new Binding("Text", this.cssBindingSource, "Border", true);
            bBorder.Format += new ConvertEventHandler(b_Format);
            bBorder.Parse += new ConvertEventHandler(b_Parse);
            bBorder.DataSourceUpdateMode = DataSourceUpdateMode.OnValidation;
            Binding bPadding = new Binding("Text", this.cssBindingSource, "Padding", true);
            bPadding.Format += new ConvertEventHandler(b_Format);
            bPadding.Parse += new ConvertEventHandler(b_Parse);
            bPadding.DataSourceUpdateMode = DataSourceUpdateMode.OnValidation;
            Binding bMargin = new Binding("Text", this.cssBindingSource, "Margin", true);
            bMargin.Format += new ConvertEventHandler(b_Format);
            bMargin.Parse += new ConvertEventHandler(b_Parse);
            bMargin.DataSourceUpdateMode = DataSourceUpdateMode.OnValidation;
            this.border.DataBindings.Add(bBorder);
            this.espacement.DataBindings.Add(bPadding);
            this.marge.DataBindings.Add(bMargin);
            this.textBox1.DataBindings.Add("Text", this.cssBindingSource, "BackgroundImageURL");
        }

        private void CreateSheetColor()
        {
            if (this.sheetColor != null)
            {
                this.Controls.Remove(this.sheetColor);
                this.sheetColor.Dispose();
                this.sheetColor = null;
            }
            this.sheetColor = new EasyHTMLDev.ColorSchemePopup();
            this.sheetColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sheetColor.CurrentColor = System.Drawing.Color.Empty;
            this.sheetColor.Location = new System.Drawing.Point(121, 12);
            this.sheetColor.Name = "sheetColor";
            this.sheetColor.Size = new System.Drawing.Size(328, 210);
            this.sheetColor.TabIndex = 12;
            this.sheetColor.Visible = false;
            this.Controls.Add(this.sheetColor);

        }

        void borderColor_Close(object sender, EventArgs e)
        {
            this.sheetColor.Close -= borderColor_Close;
            if (this.sheetColor.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (this.sheetColor.CurrentColor != null && this.sheetColor.CurrentColor.HasValue)
                {
                    this.CSS.BorderBottomColor = new Library.CSSColor(this.sheetColor.CurrentColor.Value);
                    this.CSS.BorderTopColor = new Library.CSSColor(this.sheetColor.CurrentColor.Value);
                    this.CSS.BorderLeftColor = new Library.CSSColor(this.sheetColor.CurrentColor.Value);
                    this.CSS.BorderRightColor = new Library.CSSColor(this.sheetColor.CurrentColor.Value);
                }
                else
                {
                    this.CSS.BorderBottomColor = new Library.CSSColor();
                    this.CSS.BorderTopColor = new Library.CSSColor();
                    this.CSS.BorderLeftColor = new Library.CSSColor();
                    this.CSS.BorderRightColor = new Library.CSSColor();
                }
                if (this.modified != null)
                {
                    this.modified(sender, e);
                }
            }
            this.sheetColor.Visible = false;
            this.hideControls.Visible = false;
            this.ControlBox = true;
        }

        void backColor_Close(object sender, EventArgs e)
        {
            this.sheetColor.Close -= backColor_Close;
            if (this.sheetColor.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (this.sheetColor.CurrentColor != null && this.sheetColor.CurrentColor.HasValue)
                {
                    this.CSS.BackgroundColor = new Library.CSSColor(this.sheetColor.CurrentColor.Value);
                }
                else
                {
                    this.CSS.BackgroundColor = new Library.CSSColor();
                }
                if (this.modified != null)
                {
                    this.modified(sender, e);
                }
            }
            this.sheetColor.Visible = false;
            this.hideControls.Visible = false;
            this.ControlBox = true;
        }

        void foreColor_Close(object sender, EventArgs e)
        {
            this.sheetColor.Close -= foreColor_Close;
            if (this.sheetColor.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (this.sheetColor.CurrentColor != null && this.sheetColor.CurrentColor.HasValue)
                {
                    this.CSS.ForegroundColor = new Library.CSSColor(this.sheetColor.CurrentColor.Value);
                }
                else
                {
                    this.CSS.ForegroundColor = new Library.CSSColor();
                }
                if (this.modified != null)
                {
                    this.modified(sender, e);
                }
            }
            this.sheetColor.Visible = false;
            this.hideControls.Visible = false;
            this.ControlBox = true;
        }

        void b_Parse(object sender, ConvertEventArgs e)
        {
            Library.Rectangle padding = null;
            if (Library.Rectangle.TryParse(e.Value.ToString(), out padding))
            {
                e.Value = padding;
                if (this.modified != null)
                {
                    this.modified(sender, e);
                }
            }
        }

        void b_Format(object sender, ConvertEventArgs e)
        {
            e.Value = e.Value.ToString();
        }

        public Library.CodeCSS CSS
        {
            get { return this.cssBindingSource.DataSource as Library.CodeCSS; }
            set { this.cssBindingSource.DataSource = value; }
        }

        private void btnBackground_Click(object sender, EventArgs e)
        {
            this.CreateSheetColor();
            if (this.CSS.BackgroundColor != null)
            {
                if (!this.CSS.BackgroundColor.IsEmpty)
                {
                    this.sheetColor.CurrentColor = this.CSS.BackgroundColor.Color;
                }
                else
                {
                    this.sheetColor.CurrentColor = null;
                }
            }
            else
            {
                this.sheetColor.CurrentColor = null;
            }
            this.sheetColor.Close += backColor_Close;
            this.hideControls.Visible = true;
            this.ControlBox = false;
            this.sheetColor.BringToFront();
            this.sheetColor.Visible = true;

        }

        private void btnForeground_Click(object sender, EventArgs e)
        {
            this.CreateSheetColor();
            if (this.CSS.ForegroundColor != null)
            {
                if (!this.CSS.ForegroundColor.IsEmpty)
                {
                    this.sheetColor.CurrentColor = this.CSS.ForegroundColor.Color;
                }
                else
                {
                    this.sheetColor.CurrentColor = null;
                }
            }
            else
            {
                this.sheetColor.CurrentColor = null;
            }
            this.sheetColor.Close += foreColor_Close;
            this.hideControls.Visible = true;
            this.ControlBox = false;
            this.sheetColor.BringToFront();
            this.sheetColor.Visible = true;
        }

        private void btnBorder_Click(object sender, EventArgs e)
        {
            this.CreateSheetColor();
            if (this.CSS.BorderLeftColor != null)
            {
                if (!this.CSS.BorderLeftColor.IsEmpty)
                {
                    this.sheetColor.CurrentColor = this.CSS.BorderLeftColor.Color;
                }
                else
                {
                    this.sheetColor.CurrentColor = null;
                }
            }
            else
            {
                this.sheetColor.CurrentColor = null;
            }
            this.sheetColor.Close += borderColor_Close;
            this.hideControls.Visible = true;
            this.ControlBox = false;
            this.sheetColor.BringToFront();
            this.sheetColor.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void CSSOptions_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.UnregisterControls(ref this.localeComponentId);
        }
    }
}
