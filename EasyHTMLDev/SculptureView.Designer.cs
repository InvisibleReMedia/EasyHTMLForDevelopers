namespace EasyHTMLDev
{
    partial class SculptureView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SculptureView));
            this.grpConvert = new System.Windows.Forms.GroupBox();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.hideControls = new System.Windows.Forms.PictureBox();
            this.sheetModel = new EasyHTMLDev.ModelTypePopup();
            this.CurrentCadreModel = new EasyHTMLDev.PropertiesUC();
            this.sPanel = new EasyHTMLDev.SculpturePanel();
            this.btnValidate1 = new EasyHTMLDev.BtnValidate();
            this.sheetColor = new EasyHTMLDev.ColorSchemePopup();
            this.grpConvert.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hideControls)).BeginInit();
            this.SuspendLayout();
            // 
            // grpConvert
            // 
            this.grpConvert.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpConvert.Controls.Add(this.btnGenerate);
            this.grpConvert.Location = new System.Drawing.Point(0, 405);
            this.grpConvert.Name = "grpConvert";
            this.grpConvert.Size = new System.Drawing.Size(422, 64);
            this.grpConvert.TabIndex = 6;
            this.grpConvert.TabStop = false;
            this.grpConvert.Text = "GenerateGroup";
            // 
            // btnGenerate
            // 
            this.btnGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenerate.Location = new System.Drawing.Point(12, 21);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(398, 23);
            this.btnGenerate.TabIndex = 0;
            this.btnGenerate.Text = "GenerateFromSculpture";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // hideControls
            // 
            this.hideControls.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hideControls.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.hideControls.Location = new System.Drawing.Point(0, 0);
            this.hideControls.Name = "hideControls";
            this.hideControls.Size = new System.Drawing.Size(623, 469);
            this.hideControls.TabIndex = 9;
            this.hideControls.TabStop = false;
            this.hideControls.Visible = false;
            // 
            // sheetModel
            // 
            this.sheetModel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sheetModel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sheetModel.Location = new System.Drawing.Point(147, 0);
            this.sheetModel.Name = "sheetModel";
            this.sheetModel.Size = new System.Drawing.Size(328, 210);
            this.sheetModel.TabIndex = 8;
            this.sheetModel.Visible = false;
            // 
            // CurrentCadreModel
            // 
            this.CurrentCadreModel.CurrentObject = null;
            this.CurrentCadreModel.Dock = System.Windows.Forms.DockStyle.Top;
            this.CurrentCadreModel.Location = new System.Drawing.Point(0, 0);
            this.CurrentCadreModel.Name = "CurrentCadreModel";
            this.CurrentCadreModel.Size = new System.Drawing.Size(623, 195);
            this.CurrentCadreModel.TabIndex = 0;
            // 
            // sPanel
            // 
            this.sPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sPanel.Location = new System.Drawing.Point(0, 201);
            this.sPanel.Name = "sPanel";
            this.sPanel.Size = new System.Drawing.Size(623, 204);
            this.sPanel.TabIndex = 5;
            // 
            // btnValidate1
            // 
            this.btnValidate1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnValidate1.Location = new System.Drawing.Point(428, 405);
            this.btnValidate1.Name = "btnValidate1";
            this.btnValidate1.Size = new System.Drawing.Size(195, 64);
            this.btnValidate1.TabIndex = 4;
            // 
            // sheetColor
            // 
            this.sheetColor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sheetColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sheetColor.CurrentColor = System.Drawing.Color.Empty;
            this.sheetColor.Location = new System.Drawing.Point(147, 0);
            this.sheetColor.Name = "sheetColor";
            this.sheetColor.Size = new System.Drawing.Size(328, 210);
            this.sheetColor.TabIndex = 10;
            this.sheetColor.Visible = false;
            // 
            // SculptureView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(623, 470);
            this.Controls.Add(this.sheetColor);
            this.Controls.Add(this.sheetModel);
            this.Controls.Add(this.hideControls);
            this.Controls.Add(this.CurrentCadreModel);
            this.Controls.Add(this.sPanel);
            this.Controls.Add(this.grpConvert);
            this.Controls.Add(this.btnValidate1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(639, 508);
            this.Name = "SculptureView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "SculptureForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SculptureView_FormClosed);
            this.Load += new System.EventHandler(this.SculptureView_Load);
            this.grpConvert.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.hideControls)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private SculpturePanel sPanel;
        public BtnValidate btnValidate1;
        private System.Windows.Forms.GroupBox grpConvert;
        private ModelTypePopup sheetModel;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.PictureBox hideControls;
        public PropertiesUC CurrentCadreModel;
        private ColorSchemePopup sheetColor;
    }
}