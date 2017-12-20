namespace EasyHTMLDev
{
    partial class CSSOptions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CSSOptions));
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.cssBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnBackground = new System.Windows.Forms.Button();
            this.cd = new System.Windows.Forms.ColorDialog();
            this.label2 = new System.Windows.Forms.Label();
            this.espacement = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.marge = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.border = new System.Windows.Forms.TextBox();
            this.btnBorder = new System.Windows.Forms.Button();
            this.btnForeground = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.hideControls = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.cssBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hideControls)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "URLImage";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(144, 27);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(378, 20);
            this.textBox1.TabIndex = 1;
            // 
            // cssBindingSource
            // 
            this.cssBindingSource.DataSource = typeof(Library.CodeCSS);
            // 
            // btnBackground
            // 
            this.btnBackground.Location = new System.Drawing.Point(38, 211);
            this.btnBackground.Name = "btnBackground";
            this.btnBackground.Size = new System.Drawing.Size(171, 23);
            this.btnBackground.TabIndex = 2;
            this.btnBackground.Text = "BackColor";
            this.btnBackground.UseVisualStyleBackColor = true;
            this.btnBackground.Click += new System.EventHandler(this.btnBackground_Click);
            // 
            // cd
            // 
            this.cd.AnyColor = true;
            this.cd.Color = System.Drawing.Color.DarkRed;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 116);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Border";
            // 
            // espacement
            // 
            this.espacement.Location = new System.Drawing.Point(144, 66);
            this.espacement.Name = "espacement";
            this.espacement.Size = new System.Drawing.Size(135, 20);
            this.espacement.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(305, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Margin";
            // 
            // marge
            // 
            this.marge.Location = new System.Drawing.Point(378, 66);
            this.marge.Name = "marge";
            this.marge.Size = new System.Drawing.Size(144, 20);
            this.marge.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(35, 69);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Padding";
            // 
            // border
            // 
            this.border.Location = new System.Drawing.Point(144, 113);
            this.border.Name = "border";
            this.border.Size = new System.Drawing.Size(135, 20);
            this.border.TabIndex = 8;
            // 
            // btnBorder
            // 
            this.btnBorder.Location = new System.Drawing.Point(215, 172);
            this.btnBorder.Name = "btnBorder";
            this.btnBorder.Size = new System.Drawing.Size(171, 23);
            this.btnBorder.TabIndex = 9;
            this.btnBorder.Text = "BorderColor";
            this.btnBorder.UseVisualStyleBackColor = true;
            this.btnBorder.Click += new System.EventHandler(this.btnBorder_Click);
            // 
            // btnForeground
            // 
            this.btnForeground.Location = new System.Drawing.Point(38, 172);
            this.btnForeground.Name = "btnForeground";
            this.btnForeground.Size = new System.Drawing.Size(171, 23);
            this.btnForeground.TabIndex = 9;
            this.btnForeground.Text = "ForeColor";
            this.btnForeground.UseVisualStyleBackColor = true;
            this.btnForeground.Click += new System.EventHandler(this.btnForeground_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(141, 89);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "RectangleFormat";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(375, 89);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "RectangleFormat";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(141, 136);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(88, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "RectangleFormat";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(446, 211);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "ButtonClose";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // hideControls
            // 
            this.hideControls.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hideControls.Location = new System.Drawing.Point(1, 1);
            this.hideControls.Name = "hideControls";
            this.hideControls.Size = new System.Drawing.Size(563, 246);
            this.hideControls.TabIndex = 13;
            this.hideControls.TabStop = false;
            this.hideControls.Visible = false;
            // 
            // CSSOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 246);
            this.Controls.Add(this.hideControls);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnForeground);
            this.Controls.Add(this.btnBorder);
            this.Controls.Add(this.border);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.marge);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.espacement);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnBackground);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CSSOptions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CSSOptions";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CSSOptions_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.cssBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hideControls)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnBackground;
        private System.Windows.Forms.ColorDialog cd;
        private System.Windows.Forms.BindingSource cssBindingSource;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox espacement;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox marge;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox border;
        private System.Windows.Forms.Button btnBorder;
        private System.Windows.Forms.Button btnForeground;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox hideControls;
    }
}