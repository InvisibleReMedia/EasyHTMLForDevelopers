namespace EasyHTMLDev
{
    partial class OutilUC
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
            this.cmbTools = new System.Windows.Forms.ComboBox();
            this.btnCreateTool = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cmbTools
            // 
            this.cmbTools.FormattingEnabled = true;
            this.cmbTools.Location = new System.Drawing.Point(48, 46);
            this.cmbTools.Name = "cmbTools";
            this.cmbTools.Size = new System.Drawing.Size(202, 21);
            this.cmbTools.TabIndex = 4;
            // 
            // btnCreateTool
            // 
            this.btnCreateTool.Location = new System.Drawing.Point(48, 8);
            this.btnCreateTool.Name = "btnCreateTool";
            this.btnCreateTool.Size = new System.Drawing.Size(202, 23);
            this.btnCreateTool.TabIndex = 5;
            this.btnCreateTool.Text = "NewToolCreation";
            this.btnCreateTool.UseVisualStyleBackColor = true;
            this.btnCreateTool.Click += new System.EventHandler(this.btnCreateTool_Click);
            // 
            // OutilUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnCreateTool);
            this.Controls.Add(this.cmbTools);
            this.Name = "OutilUC";
            this.Size = new System.Drawing.Size(299, 75);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbTools;
        private System.Windows.Forms.Button btnCreateTool;

    }
}
