﻿namespace EasyHTMLDev
{
    partial class BtnClose
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

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnOK = new System.Windows.Forms.Button();
            this.btnFermer = new System.Windows.Forms.Button();
            this.labelGreen = new System.Windows.Forms.Label();
            this.labelRed = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(57, 33);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "ButtonValidate";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnFermer
            // 
            this.btnFermer.Location = new System.Drawing.Point(111, 33);
            this.btnFermer.Name = "btnFermer";
            this.btnFermer.Size = new System.Drawing.Size(75, 23);
            this.btnFermer.TabIndex = 1;
            this.btnFermer.Text = "ButtonClose";
            this.btnFermer.UseVisualStyleBackColor = true;
            this.btnFermer.Click += new System.EventHandler(this.btnFermer_Click);
            // 
            // labelGreen
            // 
            this.labelGreen.AutoSize = true;
            this.labelGreen.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelGreen.ForeColor = System.Drawing.Color.Green;
            this.labelGreen.Location = new System.Drawing.Point(11, 11);
            this.labelGreen.Name = "labelGreen";
            this.labelGreen.Size = new System.Drawing.Size(49, 13);
            this.labelGreen.TabIndex = 3;
            this.labelGreen.Text = "IsClean";
            // 
            // labelRed
            // 
            this.labelRed.AutoSize = true;
            this.labelRed.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRed.ForeColor = System.Drawing.Color.Red;
            this.labelRed.Location = new System.Drawing.Point(11, 11);
            this.labelRed.Name = "labelRed";
            this.labelRed.Size = new System.Drawing.Size(122, 13);
            this.labelRed.TabIndex = 4;
            this.labelRed.Text = "PendingModification";
            // 
            // BtnClose
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelRed);
            this.Controls.Add(this.labelGreen);
            this.Controls.Add(this.btnFermer);
            this.Controls.Add(this.btnOK);
            this.Name = "BtnClose";
            this.Size = new System.Drawing.Size(189, 64);
            this.Load += new System.EventHandler(this.BtnValidate_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button btnOK;
        public System.Windows.Forms.Button btnFermer;
        private System.Windows.Forms.Label labelGreen;
        private System.Windows.Forms.Label labelRed;

    }
}
