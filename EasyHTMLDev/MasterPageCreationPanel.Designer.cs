namespace EasyHTMLDev
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
            this.SuspendLayout();
            // 
            // MasterPagePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.DoubleBuffered = true;
            this.Name = "MasterPagePanel";
            this.Size = new System.Drawing.Size(550, 360);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MasterPagePanel_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MasterPagePanel_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MasterPagePanel_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion

    }
}
