namespace EasyHTMLDev
{
    partial class MasterPageOptions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MasterPageOptions));
            this.cssOnFile = new System.Windows.Forms.CheckBox();
            this.masterPageBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.javascriptOnFile = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.masterPageBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // cssOnFile
            // 
            this.cssOnFile.AutoSize = true;
            this.cssOnFile.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.masterPageBindingSource, "IsCSSOnFile", true));
            this.cssOnFile.Location = new System.Drawing.Point(13, 13);
            this.cssOnFile.Name = "cssOnFile";
            this.cssOnFile.Size = new System.Drawing.Size(100, 17);
            this.cssOnFile.TabIndex = 0;
            this.cssOnFile.Text = "RadioWriteCSS";
            this.cssOnFile.UseVisualStyleBackColor = true;
            // 
            // masterPageBindingSource
            // 
            this.masterPageBindingSource.DataSource = typeof(Library.MasterPage);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "WriteCSSFile";
            // 
            // textBox1
            // 
            this.textBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.masterPageBindingSource, "CSSFileName", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, "saisir un nom de fichier"));
            this.textBox1.Location = new System.Drawing.Point(163, 34);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(336, 20);
            this.textBox1.TabIndex = 2;
            // 
            // javascriptOnFile
            // 
            this.javascriptOnFile.AutoSize = true;
            this.javascriptOnFile.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.masterPageBindingSource, "IsJavaScriptOnFile", true));
            this.javascriptOnFile.Location = new System.Drawing.Point(13, 63);
            this.javascriptOnFile.Name = "javascriptOnFile";
            this.javascriptOnFile.Size = new System.Drawing.Size(127, 17);
            this.javascriptOnFile.TabIndex = 3;
            this.javascriptOnFile.Text = "RadioWriteJavascript";
            this.javascriptOnFile.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "WriteJavascriptFile";
            // 
            // textBox2
            // 
            this.textBox2.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.masterPageBindingSource, "JavaScriptFileName", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, "saisir un nom de fichier"));
            this.textBox2.Location = new System.Drawing.Point(163, 84);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(336, 20);
            this.textBox2.TabIndex = 5;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(423, 144);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "ButtonValidate";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(325, 144);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "ButtonCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // MasterPageOptions
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(511, 179);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.javascriptOnFile);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cssOnFile);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MasterPageOptions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MasterPageOptions";
            this.Load += new System.EventHandler(this.MasterPageOptions_Load);
            ((System.ComponentModel.ISupportInitialize)(this.masterPageBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cssOnFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.CheckBox javascriptOnFile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.BindingSource masterPageBindingSource;
    }
}