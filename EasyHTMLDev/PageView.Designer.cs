namespace EasyHTMLDev
{
    partial class PageView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PageView));
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btns = new EasyHTMLDev.PopupBtn();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.pageBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.actualiser = new System.Windows.Forms.Button();
            this.btnValidate1 = new EasyHTMLDev.BtnValidate();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pageBindingSource)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // webBrowser1
            // 
            this.webBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowser1.Location = new System.Drawing.Point(0, 245);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(732, 262);
            this.webBrowser1.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btns);
            this.groupBox2.Controls.Add(this.textBox2);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.groupBox1);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(732, 159);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "PageProperties";
            // 
            // btns
            // 
            this.btns.Location = new System.Drawing.Point(596, 48);
            this.btns.Name = "btns";
            this.btns.SelectedName = null;
            this.btns.Size = new System.Drawing.Size(115, 99);
            this.btns.TabIndex = 20;
            this.btns.Visible = false;
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.pageBindingSource, "MasterPageName", true));
            this.textBox2.Enabled = false;
            this.textBox2.Location = new System.Drawing.Point(354, 22);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(366, 20);
            this.textBox2.TabIndex = 3;
            // 
            // pageBindingSource
            // 
            this.pageBindingSource.DataSource = typeof(Library.Page);
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Image = global::EasyHTMLDev.Properties.Resources.center;
            this.button1.Location = new System.Drawing.Point(552, 114);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(38, 33);
            this.button1.TabIndex = 19;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(244, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "MasterPage";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox3);
            this.groupBox1.Location = new System.Drawing.Point(10, 46);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(490, 55);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Width";
            // 
            // textBox3
            // 
            this.textBox3.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.pageBindingSource, "Width", true));
            this.textBox3.Location = new System.Drawing.Point(16, 19);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(85, 20);
            this.textBox3.TabIndex = 6;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBox4);
            this.groupBox3.Location = new System.Drawing.Point(10, 107);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(490, 42);
            this.groupBox3.TabIndex = 22;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Height";
            // 
            // textBox4
            // 
            this.textBox4.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.pageBindingSource, "Height", true));
            this.textBox4.Location = new System.Drawing.Point(16, 16);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(85, 20);
            this.textBox4.TabIndex = 7;
            // 
            // textBox1
            // 
            this.textBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.pageBindingSource, "Name", true));
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(54, 22);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(149, 20);
            this.textBox1.TabIndex = 1;
            // 
            // actualiser
            // 
            this.actualiser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.actualiser.Location = new System.Drawing.Point(12, 554);
            this.actualiser.Name = "actualiser";
            this.actualiser.Size = new System.Drawing.Size(75, 23);
            this.actualiser.TabIndex = 5;
            this.actualiser.Text = "ButtonRefresh";
            this.actualiser.UseVisualStyleBackColor = true;
            this.actualiser.Click += new System.EventHandler(this.actualiser_Click);
            // 
            // btnValidate1
            // 
            this.btnValidate1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnValidate1.Location = new System.Drawing.Point(531, 513);
            this.btnValidate1.Name = "btnValidate1";
            this.btnValidate1.Size = new System.Drawing.Size(189, 64);
            this.btnValidate1.TabIndex = 6;
            // 
            // textBox5
            // 
            this.textBox5.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.pageBindingSource, "Head", true));
            this.textBox5.Location = new System.Drawing.Point(89, 166);
            this.textBox5.Multiline = true;
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(631, 64);
            this.textBox5.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 169);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Head";
            // 
            // PageView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(732, 589);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.btnValidate1);
            this.Controls.Add(this.actualiser);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.groupBox2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(577, 368);
            this.Name = "PageView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "PageForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PageView_FormClosed);
            this.Load += new System.EventHandler(this.PageView_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pageBindingSource)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox3;
        private PopupBtn btns;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.BindingSource pageBindingSource;
        private System.Windows.Forms.Button actualiser;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        public BtnValidate btnValidate1;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label3;
    }
}