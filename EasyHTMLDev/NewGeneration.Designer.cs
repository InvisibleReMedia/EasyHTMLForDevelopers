namespace EasyHTMLDev
{
    partial class NewGeneration
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
            this.txtNew = new System.Windows.Forms.TextBox();
            this.epNew = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.epNew)).BeginInit();
            this.SuspendLayout();
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.ForeColor = System.Drawing.Color.White;
            this.lblInfo.Location = new System.Drawing.Point(9, 28);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(125, 13);
            this.lblInfo.TabIndex = 0;
            this.lblInfo.Text = "NewGenerationComment";
            this.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtNew
            // 
            this.txtNew.Location = new System.Drawing.Point(91, 86);
            this.txtNew.Name = "txtNew";
            this.txtNew.Size = new System.Drawing.Size(172, 20);
            this.txtNew.TabIndex = 4;
            // 
            // epNew
            // 
            this.epNew.ContainerControl = this;
            // 
            // NewGeneration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Highlight;
            this.Controls.Add(this.txtNew);
            this.Controls.Add(this.lblInfo);
            this.Name = "NewGeneration";
            this.Size = new System.Drawing.Size(364, 135);
            ((System.ComponentModel.ISupportInitialize)(this.epNew)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblInfo;
        public System.Windows.Forms.TextBox txtNew;
        private System.Windows.Forms.ErrorProvider epNew;
    }
}
