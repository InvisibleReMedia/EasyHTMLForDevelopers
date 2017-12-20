namespace EasyHTMLDev
{
    partial class MasterObjectUC
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
            this.btnCreateMasterObject = new System.Windows.Forms.Button();
            this.cmbMasterObjects = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnCreateMasterObject
            // 
            this.btnCreateMasterObject.Location = new System.Drawing.Point(48, 8);
            this.btnCreateMasterObject.Name = "btnCreateMasterObject";
            this.btnCreateMasterObject.Size = new System.Drawing.Size(202, 23);
            this.btnCreateMasterObject.TabIndex = 7;
            this.btnCreateMasterObject.Text = "ButtonCreateNewMasterObject";
            this.btnCreateMasterObject.UseVisualStyleBackColor = true;
            this.btnCreateMasterObject.Click += new System.EventHandler(this.btnCreateMasterObject_Click);
            // 
            // cmbMasterObjects
            // 
            this.cmbMasterObjects.FormattingEnabled = true;
            this.cmbMasterObjects.Location = new System.Drawing.Point(48, 46);
            this.cmbMasterObjects.Name = "cmbMasterObjects";
            this.cmbMasterObjects.Size = new System.Drawing.Size(202, 21);
            this.cmbMasterObjects.TabIndex = 6;
            // 
            // MasterObjectUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnCreateMasterObject);
            this.Controls.Add(this.cmbMasterObjects);
            this.Name = "MasterObjectUC";
            this.Size = new System.Drawing.Size(299, 75);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCreateMasterObject;
        private System.Windows.Forms.ComboBox cmbMasterObjects;

    }
}
