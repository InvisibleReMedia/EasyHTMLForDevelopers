namespace EasyHTMLDev
{
    partial class OverwriteGeneration
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblInfo = new System.Windows.Forms.Label();
            this.rbOverwrite = new System.Windows.Forms.RadioButton();
            this.cmbVersions = new System.Windows.Forms.ComboBox();
            this.rbNew = new System.Windows.Forms.RadioButton();
            this.txtNew = new System.Windows.Forms.TextBox();
            this.epOverwrite = new System.Windows.Forms.ErrorProvider(this.components);
            this.epNew = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.epOverwrite)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.epNew)).BeginInit();
            this.SuspendLayout();
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.ForeColor = System.Drawing.Color.White;
            this.lblInfo.Location = new System.Drawing.Point(37, 4);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(148, 13);
            this.lblInfo.TabIndex = 0;
            this.lblInfo.Text = "SculptureGenerationComment";
            // 
            // rbOverwrite
            // 
            this.rbOverwrite.AutoSize = true;
            this.rbOverwrite.Checked = true;
            this.rbOverwrite.ForeColor = System.Drawing.Color.White;
            this.rbOverwrite.Location = new System.Drawing.Point(10, 55);
            this.rbOverwrite.Name = "rbOverwrite";
            this.rbOverwrite.Size = new System.Drawing.Size(121, 17);
            this.rbOverwrite.TabIndex = 1;
            this.rbOverwrite.TabStop = true;
            this.rbOverwrite.Text = "RadioErasePrevious";
            this.rbOverwrite.UseVisualStyleBackColor = true;
            // 
            // cmbVersions
            // 
            this.cmbVersions.FormattingEnabled = true;
            this.cmbVersions.Location = new System.Drawing.Point(171, 54);
            this.cmbVersions.Name = "cmbVersions";
            this.cmbVersions.Size = new System.Drawing.Size(172, 21);
            this.cmbVersions.TabIndex = 2;
            // 
            // rbNew
            // 
            this.rbNew.AutoSize = true;
            this.rbNew.ForeColor = System.Drawing.Color.White;
            this.rbNew.Location = new System.Drawing.Point(10, 100);
            this.rbNew.Name = "rbNew";
            this.rbNew.Size = new System.Drawing.Size(110, 17);
            this.rbNew.TabIndex = 3;
            this.rbNew.Text = "RadioNewVersion";
            this.rbNew.UseVisualStyleBackColor = true;
            // 
            // txtNew
            // 
            this.txtNew.Location = new System.Drawing.Point(171, 106);
            this.txtNew.Name = "txtNew";
            this.txtNew.Size = new System.Drawing.Size(172, 20);
            this.txtNew.TabIndex = 4;
            // 
            // epOverwrite
            // 
            this.epOverwrite.ContainerControl = this;
            // 
            // epNew
            // 
            this.epNew.ContainerControl = this;
            // 
            // OverwriteGeneration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Highlight;
            this.Controls.Add(this.txtNew);
            this.Controls.Add(this.rbNew);
            this.Controls.Add(this.cmbVersions);
            this.Controls.Add(this.rbOverwrite);
            this.Controls.Add(this.lblInfo);
            this.Name = "OverwriteGeneration";
            this.Size = new System.Drawing.Size(364, 164);
            ((System.ComponentModel.ISupportInitialize)(this.epOverwrite)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.epNew)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblInfo;
        public System.Windows.Forms.ComboBox cmbVersions;
        public System.Windows.Forms.RadioButton rbNew;
        public System.Windows.Forms.TextBox txtNew;
        private System.Windows.Forms.ErrorProvider epOverwrite;
        private System.Windows.Forms.ErrorProvider epNew;
        public System.Windows.Forms.RadioButton rbOverwrite;
    }
}
