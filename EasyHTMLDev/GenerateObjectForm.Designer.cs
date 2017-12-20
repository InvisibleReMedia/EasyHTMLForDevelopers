namespace EasyHTMLDev
{
    partial class GenerateObjectForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GenerateObjectForm));
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.newGeneration1 = new EasyHTMLDev.NewGeneration();
            this.overwriteGeneration1 = new EasyHTMLDev.OverwriteGeneration();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(74, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "GenerateObjectComment";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(170, 230);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(82, 23);
            this.btnCancel.TabIndex = 26;
            this.btnCancel.Text = "ButtonCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnPrevious
            // 
            this.btnPrevious.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnPrevious.Location = new System.Drawing.Point(268, 230);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(87, 23);
            this.btnPrevious.TabIndex = 25;
            this.btnPrevious.Text = "ButtonPrevious";
            this.btnPrevious.UseVisualStyleBackColor = true;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(365, 230);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(98, 23);
            this.btnNext.TabIndex = 24;
            this.btnNext.Text = "ButtonTerminate";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // newGeneration1
            // 
            this.newGeneration1.BackColor = System.Drawing.SystemColors.Highlight;
            this.newGeneration1.Location = new System.Drawing.Point(55, 65);
            this.newGeneration1.Name = "newGeneration1";
            this.newGeneration1.Size = new System.Drawing.Size(364, 135);
            this.newGeneration1.TabIndex = 28;
            // 
            // overwriteGeneration1
            // 
            this.overwriteGeneration1.BackColor = System.Drawing.SystemColors.Highlight;
            this.overwriteGeneration1.Location = new System.Drawing.Point(55, 65);
            this.overwriteGeneration1.Name = "overwriteGeneration1";
            this.overwriteGeneration1.Size = new System.Drawing.Size(364, 135);
            this.overwriteGeneration1.TabIndex = 27;
            // 
            // GenerateObjectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(475, 265);
            this.Controls.Add(this.newGeneration1);
            this.Controls.Add(this.overwriteGeneration1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnPrevious);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GenerateObjectForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ConversionTitle";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Button btnNext;
        private OverwriteGeneration overwriteGeneration1;
        private NewGeneration newGeneration1;
    }
}