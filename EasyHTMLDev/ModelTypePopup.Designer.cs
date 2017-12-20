namespace EasyHTMLDev
{
    partial class ModelTypePopup
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
            this.label1 = new System.Windows.Forms.Label();
            this.tabOptions = new System.Windows.Forms.TabControl();
            this.tabImage = new System.Windows.Forms.TabPage();
            this.tabText = new System.Windows.Forms.TabPage();
            this.tabOutil = new System.Windows.Forms.TabPage();
            this.tabMasterObject = new System.Windows.Forms.TabPage();
            this.tabDynamic = new System.Windows.Forms.TabPage();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "ObjectType";
            // 
            // tabOptions
            // 
            this.tabOptions.Controls.Add(this.tabImage);
            this.tabOptions.Controls.Add(this.tabText);
            this.tabOptions.Controls.Add(this.tabOutil);
            this.tabOptions.Controls.Add(this.tabMasterObject);
            this.tabOptions.Controls.Add(this.tabDynamic);
            this.tabOptions.Location = new System.Drawing.Point(6, 105);
            this.tabOptions.Name = "tabOptions";
            this.tabOptions.SelectedIndex = 0;
            this.tabOptions.Size = new System.Drawing.Size(307, 101);
            this.tabOptions.TabIndex = 1;
            // 
            // tabImage
            // 
            this.tabImage.Location = new System.Drawing.Point(4, 22);
            this.tabImage.Name = "tabImage";
            this.tabImage.Padding = new System.Windows.Forms.Padding(3);
            this.tabImage.Size = new System.Drawing.Size(299, 75);
            this.tabImage.TabIndex = 0;
            this.tabImage.Text = "HeaderPicture";
            this.tabImage.UseVisualStyleBackColor = true;
            // 
            // tabText
            // 
            this.tabText.Location = new System.Drawing.Point(4, 22);
            this.tabText.Name = "tabText";
            this.tabText.Padding = new System.Windows.Forms.Padding(3);
            this.tabText.Size = new System.Drawing.Size(299, 75);
            this.tabText.TabIndex = 1;
            this.tabText.Text = "HeaderText";
            this.tabText.UseVisualStyleBackColor = true;
            // 
            // tabOutil
            // 
            this.tabOutil.Location = new System.Drawing.Point(4, 22);
            this.tabOutil.Name = "tabOutil";
            this.tabOutil.Size = new System.Drawing.Size(299, 75);
            this.tabOutil.TabIndex = 2;
            this.tabOutil.Text = "HeaderTool";
            this.tabOutil.UseVisualStyleBackColor = true;
            // 
            // tabMasterObject
            // 
            this.tabMasterObject.Location = new System.Drawing.Point(4, 22);
            this.tabMasterObject.Name = "tabMasterObject";
            this.tabMasterObject.Size = new System.Drawing.Size(299, 75);
            this.tabMasterObject.TabIndex = 3;
            this.tabMasterObject.Text = "HeaderMasterObject";
            this.tabMasterObject.UseVisualStyleBackColor = true;
            // 
            // tabDynamic
            // 
            this.tabDynamic.Location = new System.Drawing.Point(4, 22);
            this.tabDynamic.Name = "tabDynamic";
            this.tabDynamic.Size = new System.Drawing.Size(299, 75);
            this.tabDynamic.TabIndex = 4;
            this.tabDynamic.Text = "HeaderDynamic";
            this.tabDynamic.UseVisualStyleBackColor = true;
            // 
            // cmbType
            // 
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Location = new System.Drawing.Point(78, 42);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(231, 21);
            this.cmbType.TabIndex = 2;
            this.cmbType.SelectedIndexChanged += new System.EventHandler(this.cmbType_SelectedIndexChanged);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(80, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(119, 13);
            this.lblTitle.TabIndex = 3;
            this.lblTitle.Text = "ObjectTypeCreation";
            // 
            // btnCancel
            // 
            this.btnCancel.AutoSize = true;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Image = global::EasyHTMLDev.Properties.Resources.sup;
            this.btnCancel.Location = new System.Drawing.Point(290, -2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(24, 24);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Image = global::EasyHTMLDev.Properties.Resources.ok;
            this.btnOk.Location = new System.Drawing.Point(267, -2);
            this.btnOk.Margin = new System.Windows.Forms.Padding(0);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(24, 24);
            this.btnOk.TabIndex = 4;
            this.btnOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(78, 69);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(231, 20);
            this.txtName.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "ObjectName";
            // 
            // ModelTypePopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.cmbType);
            this.Controls.Add(this.tabOptions);
            this.Controls.Add(this.label1);
            this.Name = "ModelTypePopup";
            this.Size = new System.Drawing.Size(314, 212);
            this.tabOptions.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabOptions;
        private System.Windows.Forms.TabPage tabImage;
        private System.Windows.Forms.TabPage tabText;
        private System.Windows.Forms.TabPage tabOutil;
        private System.Windows.Forms.TabPage tabMasterObject;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TabPage tabDynamic;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label2;
    }
}
