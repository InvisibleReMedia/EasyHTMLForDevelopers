namespace EasyHTMLDev
{
    partial class Zones
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Zones));
            this.lstHoriz = new System.Windows.Forms.ListBox();
            this.lstVert = new System.Windows.Forms.ListBox();
            this.grpHLng = new System.Windows.Forms.GroupBox();
            this.grpHHt = new System.Windows.Forms.GroupBox();
            this.grpVLng = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.grpVHt = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cssHoriz = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cssVert = new System.Windows.Forms.TextBox();
            this.btnOptionsHoriz = new System.Windows.Forms.Button();
            this.btnOptionsVert = new System.Windows.Forms.Button();
            this.bsZoneHoriz = new System.Windows.Forms.BindingSource(this.components);
            this.bsZoneVert = new System.Windows.Forms.BindingSource(this.components);
            this.btns = new EasyHTMLDev.PopupBtn();
            this.btnClose = new System.Windows.Forms.Button();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.bsZoneHoriz)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsZoneVert)).BeginInit();
            this.SuspendLayout();
            // 
            // lstHoriz
            // 
            this.lstHoriz.FormattingEnabled = true;
            this.lstHoriz.Location = new System.Drawing.Point(13, 13);
            this.lstHoriz.Name = "lstHoriz";
            this.lstHoriz.Size = new System.Drawing.Size(328, 173);
            this.lstHoriz.TabIndex = 0;
            // 
            // lstVert
            // 
            this.lstVert.FormattingEnabled = true;
            this.lstVert.Location = new System.Drawing.Point(369, 13);
            this.lstVert.Name = "lstVert";
            this.lstVert.Size = new System.Drawing.Size(328, 173);
            this.lstVert.TabIndex = 1;
            // 
            // grpHLng
            // 
            this.grpHLng.Enabled = false;
            this.grpHLng.Location = new System.Drawing.Point(13, 309);
            this.grpHLng.Name = "grpHLng";
            this.grpHLng.Size = new System.Drawing.Size(149, 113);
            this.grpHLng.TabIndex = 15;
            this.grpHLng.TabStop = false;
            this.grpHLng.Text = "Width";
            // 
            // grpHHt
            // 
            this.grpHHt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpHHt.Enabled = false;
            this.grpHHt.Location = new System.Drawing.Point(169, 309);
            this.grpHHt.Name = "grpHHt";
            this.grpHHt.Size = new System.Drawing.Size(172, 113);
            this.grpHHt.TabIndex = 16;
            this.grpHHt.TabStop = false;
            this.grpHHt.Text = "Height";
            // 
            // grpVLng
            // 
            this.grpVLng.Enabled = false;
            this.grpVLng.Location = new System.Drawing.Point(369, 309);
            this.grpVLng.Name = "grpVLng";
            this.grpVLng.Size = new System.Drawing.Size(149, 113);
            this.grpVLng.TabIndex = 15;
            this.grpVLng.TabStop = false;
            this.grpVLng.Text = "Width";
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Image = global::EasyHTMLDev.Properties.Resources.center;
            this.button1.Location = new System.Drawing.Point(372, 428);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(38, 33);
            this.button1.TabIndex = 5;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // grpVHt
            // 
            this.grpVHt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpVHt.Enabled = false;
            this.grpVHt.Location = new System.Drawing.Point(525, 309);
            this.grpVHt.Name = "grpVHt";
            this.grpVHt.Size = new System.Drawing.Size(172, 113);
            this.grpVHt.TabIndex = 16;
            this.grpVHt.TabStop = false;
            this.grpVHt.Text = "Height";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 193);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "CodeCSS";
            // 
            // cssHoriz
            // 
            this.cssHoriz.AcceptsReturn = true;
            this.cssHoriz.Enabled = false;
            this.cssHoriz.Location = new System.Drawing.Point(13, 215);
            this.cssHoriz.Multiline = true;
            this.cssHoriz.Name = "cssHoriz";
            this.cssHoriz.Size = new System.Drawing.Size(328, 88);
            this.cssHoriz.TabIndex = 21;
            this.cssHoriz.Validating += new System.ComponentModel.CancelEventHandler(this.cssHoriz_Validating);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(369, 193);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 20;
            this.label2.Text = "CodeCSS";
            // 
            // cssVert
            // 
            this.cssVert.AcceptsReturn = true;
            this.cssVert.Enabled = false;
            this.cssVert.Location = new System.Drawing.Point(369, 215);
            this.cssVert.Multiline = true;
            this.cssVert.Name = "cssVert";
            this.cssVert.Size = new System.Drawing.Size(328, 88);
            this.cssVert.TabIndex = 21;
            this.cssVert.Validating += new System.ComponentModel.CancelEventHandler(this.cssVert_Validating);
            // 
            // btnOptionsHoriz
            // 
            this.btnOptionsHoriz.Location = new System.Drawing.Point(205, 189);
            this.btnOptionsHoriz.Name = "btnOptionsHoriz";
            this.btnOptionsHoriz.Size = new System.Drawing.Size(136, 20);
            this.btnOptionsHoriz.TabIndex = 22;
            this.btnOptionsHoriz.Text = "CSSOptions";
            this.btnOptionsHoriz.UseVisualStyleBackColor = true;
            this.btnOptionsHoriz.Click += new System.EventHandler(this.btnOptionsHoriz_Click);
            // 
            // btnOptionsVert
            // 
            this.btnOptionsVert.Location = new System.Drawing.Point(557, 189);
            this.btnOptionsVert.Name = "btnOptionsVert";
            this.btnOptionsVert.Size = new System.Drawing.Size(140, 20);
            this.btnOptionsVert.TabIndex = 22;
            this.btnOptionsVert.Text = "CSSOptions";
            this.btnOptionsVert.UseVisualStyleBackColor = true;
            this.btnOptionsVert.Click += new System.EventHandler(this.btnOptionsVert_Click);
            // 
            // bsZoneHoriz
            // 
            this.bsZoneHoriz.AllowNew = false;
            this.bsZoneHoriz.DataSource = typeof(Library.HorizontalZone);
            // 
            // bsZoneVert
            // 
            this.bsZoneVert.AllowNew = false;
            this.bsZoneVert.DataSource = typeof(Library.VerticalZone);
            // 
            // btns
            // 
            this.btns.Location = new System.Drawing.Point(416, 379);
            this.btns.Name = "btns";
            this.btns.SelectedName = null;
            this.btns.Size = new System.Drawing.Size(115, 99);
            this.btns.TabIndex = 6;
            this.btns.Visible = false;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(974, 432);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(91, 33);
            this.btnClose.TabIndex = 23;
            this.btnClose.Text = "ButtonClose";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(712, 13);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(353, 409);
            this.webBrowser1.TabIndex = 24;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(591, 432);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(106, 33);
            this.button2.TabIndex = 25;
            this.button2.Text = "ButtonRefresh";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Zones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1077, 477);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btns);
            this.Controls.Add(this.btnOptionsVert);
            this.Controls.Add(this.btnOptionsHoriz);
            this.Controls.Add(this.cssVert);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cssHoriz);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.grpVHt);
            this.Controls.Add(this.grpVLng);
            this.Controls.Add(this.grpHHt);
            this.Controls.Add(this.grpHLng);
            this.Controls.Add(this.lstVert);
            this.Controls.Add(this.lstHoriz);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(200, 200);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Zones";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Areas";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Zones_FormClosed);
            this.Load += new System.EventHandler(this.Zones_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bsZoneHoriz)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsZoneVert)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstHoriz;
        private System.Windows.Forms.ListBox lstVert;
        private System.Windows.Forms.GroupBox grpHLng;
        private System.Windows.Forms.GroupBox grpHHt;
        private System.Windows.Forms.GroupBox grpVLng;
        private System.Windows.Forms.GroupBox grpVHt;
        private System.Windows.Forms.BindingSource bsZoneVert;
        private System.Windows.Forms.Button button1;
        private PopupBtn btns;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox cssHoriz;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox cssVert;
        private System.Windows.Forms.Button btnOptionsHoriz;
        private System.Windows.Forms.Button btnOptionsVert;
        private System.Windows.Forms.BindingSource bsZoneHoriz;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Button button2;
    }
}