﻿namespace EasyHTMLDev
{
    partial class MasterPageCreationPanel
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
            this.creationPanel1 = new EasyHTMLDev.CreationPanel();
            this.SuspendLayout();
            // 
            // creationPanel1
            // 
            this.creationPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.creationPanel1.CountColumns = ((uint)(0u));
            this.creationPanel1.CountLines = ((uint)(0u));
            this.creationPanel1.Location = new System.Drawing.Point(3, 3);
            this.creationPanel1.Name = "creationPanel1";
            this.creationPanel1.Size = new System.Drawing.Size(484, 288);
            this.creationPanel1.TabIndex = 0;
            // 
            // MasterPageCreationPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.creationPanel1);
            this.DoubleBuffered = true;
            this.Name = "MasterPageCreationPanel";
            this.Size = new System.Drawing.Size(490, 294);
            this.ResumeLayout(false);

        }

        #endregion

        internal CreationPanel creationPanel1;



    }
}
