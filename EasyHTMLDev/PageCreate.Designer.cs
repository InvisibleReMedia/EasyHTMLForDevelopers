namespace EasyHTMLDev
{
    partial class PageCreate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PageCreate));
            this.label1 = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.imList = new System.Windows.Forms.ImageList(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lng = new System.Windows.Forms.TextBox();
            this.pageBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ht = new System.Windows.Forms.TextBox();
            this.rbFixe = new System.Windows.Forms.RadioButton();
            this.rbAuto = new System.Windows.Forms.RadioButton();
            this.rbRelative = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.creer = new System.Windows.Forms.Button();
            this.annuler = new System.Windows.Forms.Button();
            this.btns = new EasyHTMLDev.PopupBtn();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pageBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(15, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(517, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "MasterPageSelectionComment";
            // 
            // listView1
            // 
            this.listView1.GridLines = true;
            this.listView1.LargeImageList = this.imList;
            this.listView1.Location = new System.Drawing.Point(15, 86);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(517, 154);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // imList
            // 
            this.imList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imList.ImageSize = new System.Drawing.Size(100, 100);
            this.imList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 303);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Width";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 334);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Height";
            // 
            // lng
            // 
            this.lng.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.pageBindingSource, "Width", true));
            this.lng.Location = new System.Drawing.Point(80, 300);
            this.lng.Name = "lng";
            this.lng.Size = new System.Drawing.Size(100, 20);
            this.lng.TabIndex = 4;
            // 
            // pageBindingSource
            // 
            this.pageBindingSource.DataSource = typeof(Library.Page);
            // 
            // ht
            // 
            this.ht.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.pageBindingSource, "Height", true));
            this.ht.Location = new System.Drawing.Point(80, 331);
            this.ht.Name = "ht";
            this.ht.Size = new System.Drawing.Size(100, 20);
            this.ht.TabIndex = 5;
            // 
            // rbFixe
            // 
            this.rbFixe.AutoSize = true;
            this.rbFixe.Checked = true;
            this.rbFixe.Location = new System.Drawing.Point(80, 261);
            this.rbFixe.Name = "rbFixe";
            this.rbFixe.Size = new System.Drawing.Size(102, 17);
            this.rbFixe.TabIndex = 6;
            this.rbFixe.TabStop = true;
            this.rbFixe.Text = "FixedConstraints";
            this.rbFixe.UseVisualStyleBackColor = true;
            this.rbFixe.CheckedChanged += new System.EventHandler(this.rbFixe_CheckedChanged);
            // 
            // rbAuto
            // 
            this.rbAuto.AutoSize = true;
            this.rbAuto.Location = new System.Drawing.Point(232, 261);
            this.rbAuto.Name = "rbAuto";
            this.rbAuto.Size = new System.Drawing.Size(99, 17);
            this.rbAuto.TabIndex = 7;
            this.rbAuto.Text = "AutoConstraints";
            this.rbAuto.UseVisualStyleBackColor = true;
            this.rbAuto.CheckedChanged += new System.EventHandler(this.rbAuto_CheckedChanged);
            // 
            // rbRelative
            // 
            this.rbRelative.AutoSize = true;
            this.rbRelative.Location = new System.Drawing.Point(378, 261);
            this.rbRelative.Name = "rbRelative";
            this.rbRelative.Size = new System.Drawing.Size(116, 17);
            this.rbRelative.TabIndex = 8;
            this.rbRelative.Text = "RelativeConstraints";
            this.rbRelative.UseVisualStyleBackColor = true;
            this.rbRelative.CheckedChanged += new System.EventHandler(this.rbRelative_CheckedChanged);
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Image = global::EasyHTMLDev.Properties.Resources.center;
            this.button1.Location = new System.Drawing.Point(378, 303);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(38, 33);
            this.button1.TabIndex = 17;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // creer
            // 
            this.creer.Enabled = false;
            this.creer.Location = new System.Drawing.Point(456, 370);
            this.creer.Name = "creer";
            this.creer.Size = new System.Drawing.Size(75, 23);
            this.creer.TabIndex = 19;
            this.creer.Text = "ButtonCreate";
            this.creer.UseVisualStyleBackColor = true;
            this.creer.Click += new System.EventHandler(this.creer_Click);
            // 
            // annuler
            // 
            this.annuler.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.annuler.Location = new System.Drawing.Point(328, 370);
            this.annuler.Name = "annuler";
            this.annuler.Size = new System.Drawing.Size(75, 23);
            this.annuler.TabIndex = 20;
            this.annuler.Text = "ButtonCancel";
            this.annuler.UseVisualStyleBackColor = true;
            this.annuler.Click += new System.EventHandler(this.annuler_Click);
            // 
            // btns
            // 
            this.btns.Location = new System.Drawing.Point(257, 229);
            this.btns.Name = "btns";
            this.btns.SelectedName = null;
            this.btns.Size = new System.Drawing.Size(115, 99);
            this.btns.TabIndex = 18;
            this.btns.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 21;
            this.label4.Text = "Name";
            // 
            // textBox1
            // 
            this.textBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.pageBindingSource, "Name", true));
            this.textBox1.Location = new System.Drawing.Point(103, 10);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(428, 20);
            this.textBox1.TabIndex = 22;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(100, 33);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 13);
            this.label5.TabIndex = 23;
            this.label5.Text = "PageNameComment";
            // 
            // PageCreate
            // 
            this.AcceptButton = this.creer;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.annuler;
            this.ClientSize = new System.Drawing.Size(544, 405);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.annuler);
            this.Controls.Add(this.creer);
            this.Controls.Add(this.btns);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.rbRelative);
            this.Controls.Add(this.rbAuto);
            this.Controls.Add(this.rbFixe);
            this.Controls.Add(this.ht);
            this.Controls.Add(this.lng);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "PageCreate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PageCreationForm";
            this.Load += new System.EventHandler(this.PageCreate_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pageBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ImageList imList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox lng;
        private System.Windows.Forms.TextBox ht;
        private System.Windows.Forms.RadioButton rbFixe;
        private System.Windows.Forms.RadioButton rbAuto;
        private System.Windows.Forms.RadioButton rbRelative;
        private PopupBtn btns;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.BindingSource pageBindingSource;
        private System.Windows.Forms.Button creer;
        private System.Windows.Forms.Button annuler;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label5;
    }
}