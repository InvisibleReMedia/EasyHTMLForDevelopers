namespace EasyHTMLDev
{
    partial class PropertiesUC
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
            this.components = new System.ComponentModel.Container();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnSuppr = new System.Windows.Forms.Button();
            this.grpDatas = new System.Windows.Forms.GroupBox();
            this.btnType = new System.Windows.Forms.Button();
            this.btnForeColor = new System.Windows.Forms.Button();
            this.cadreModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnBorderColor = new System.Windows.Forms.Button();
            this.btnBackColor = new System.Windows.Forms.Button();
            this.lblTextColor = new System.Windows.Forms.Label();
            this.lblBorderColor = new System.Windows.Forms.Label();
            this.lblBackColor = new System.Windows.Forms.Label();
            this.txtBorderY = new System.Windows.Forms.TextBox();
            this.txtBorderX = new System.Windows.Forms.TextBox();
            this.lblBorderY = new System.Windows.Forms.Label();
            this.lblBorderX = new System.Windows.Forms.Label();
            this.txtExpY = new System.Windows.Forms.TextBox();
            this.txtEspX = new System.Windows.Forms.TextBox();
            this.lblPadY = new System.Windows.Forms.Label();
            this.lblPadX = new System.Windows.Forms.Label();
            this.txtY = new System.Windows.Forms.TextBox();
            this.txtX = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSizeY = new System.Windows.Forms.TextBox();
            this.txtSizeX = new System.Windows.Forms.TextBox();
            this.lblType = new System.Windows.Forms.Label();
            this.lblSizeY = new System.Windows.Forms.Label();
            this.lblSizeX = new System.Windows.Forms.Label();
            this.lblScale = new System.Windows.Forms.Label();
            this.cmbScale = new System.Windows.Forms.ComboBox();
            this.grpDatas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cadreModelBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(3, 3);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(75, 23);
            this.btnNew.TabIndex = 0;
            this.btnNew.Text = "NewObject";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnSuppr
            // 
            this.btnSuppr.Enabled = false;
            this.btnSuppr.Location = new System.Drawing.Point(84, 3);
            this.btnSuppr.Name = "btnSuppr";
            this.btnSuppr.Size = new System.Drawing.Size(75, 23);
            this.btnSuppr.TabIndex = 1;
            this.btnSuppr.Text = "Delete";
            this.btnSuppr.UseVisualStyleBackColor = true;
            this.btnSuppr.Click += new System.EventHandler(this.btnSuppr_Click);
            // 
            // grpDatas
            // 
            this.grpDatas.Controls.Add(this.btnType);
            this.grpDatas.Controls.Add(this.btnForeColor);
            this.grpDatas.Controls.Add(this.btnBorderColor);
            this.grpDatas.Controls.Add(this.btnBackColor);
            this.grpDatas.Controls.Add(this.lblTextColor);
            this.grpDatas.Controls.Add(this.lblBorderColor);
            this.grpDatas.Controls.Add(this.lblBackColor);
            this.grpDatas.Controls.Add(this.txtBorderY);
            this.grpDatas.Controls.Add(this.txtBorderX);
            this.grpDatas.Controls.Add(this.lblBorderY);
            this.grpDatas.Controls.Add(this.lblBorderX);
            this.grpDatas.Controls.Add(this.txtExpY);
            this.grpDatas.Controls.Add(this.txtEspX);
            this.grpDatas.Controls.Add(this.lblPadY);
            this.grpDatas.Controls.Add(this.lblPadX);
            this.grpDatas.Controls.Add(this.txtY);
            this.grpDatas.Controls.Add(this.txtX);
            this.grpDatas.Controls.Add(this.label1);
            this.grpDatas.Controls.Add(this.label2);
            this.grpDatas.Controls.Add(this.txtSizeY);
            this.grpDatas.Controls.Add(this.txtSizeX);
            this.grpDatas.Controls.Add(this.lblType);
            this.grpDatas.Controls.Add(this.lblSizeY);
            this.grpDatas.Controls.Add(this.lblSizeX);
            this.grpDatas.Enabled = false;
            this.grpDatas.Location = new System.Drawing.Point(6, 32);
            this.grpDatas.Name = "grpDatas";
            this.grpDatas.Size = new System.Drawing.Size(607, 161);
            this.grpDatas.TabIndex = 25;
            this.grpDatas.TabStop = false;
            this.grpDatas.Text = "Object";
            // 
            // btnType
            // 
            this.btnType.Location = new System.Drawing.Point(83, 10);
            this.btnType.Name = "btnType";
            this.btnType.Size = new System.Drawing.Size(129, 24);
            this.btnType.TabIndex = 4;
            this.btnType.Text = "ChangeType";
            this.btnType.UseVisualStyleBackColor = true;
            this.btnType.Click += new System.EventHandler(this.btnType_Click);
            // 
            // btnForeColor
            // 
            this.btnForeColor.BackColor = System.Drawing.Color.Black;
            this.btnForeColor.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", this.cadreModelBindingSource, "Foreground", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.btnForeColor.Location = new System.Drawing.Point(572, 105);
            this.btnForeColor.Name = "btnForeColor";
            this.btnForeColor.Size = new System.Drawing.Size(26, 23);
            this.btnForeColor.TabIndex = 15;
            this.btnForeColor.UseVisualStyleBackColor = false;
            this.btnForeColor.Click += new System.EventHandler(this.btnForeColor_Click);
            // 
            // cadreModelBindingSource
            // 
            this.cadreModelBindingSource.DataSource = typeof(Library.CadreModel);
            // 
            // btnBorderColor
            // 
            this.btnBorderColor.BackColor = System.Drawing.Color.Black;
            this.btnBorderColor.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", this.cadreModelBindingSource, "Border", true));
            this.btnBorderColor.Location = new System.Drawing.Point(572, 76);
            this.btnBorderColor.Name = "btnBorderColor";
            this.btnBorderColor.Size = new System.Drawing.Size(26, 23);
            this.btnBorderColor.TabIndex = 14;
            this.btnBorderColor.UseVisualStyleBackColor = false;
            this.btnBorderColor.Click += new System.EventHandler(this.btnBorderColor_Click);
            // 
            // btnBackColor
            // 
            this.btnBackColor.BackColor = System.Drawing.Color.White;
            this.btnBackColor.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", this.cadreModelBindingSource, "Background", true));
            this.btnBackColor.Location = new System.Drawing.Point(572, 45);
            this.btnBackColor.Name = "btnBackColor";
            this.btnBackColor.Size = new System.Drawing.Size(26, 23);
            this.btnBackColor.TabIndex = 13;
            this.btnBackColor.UseVisualStyleBackColor = false;
            this.btnBackColor.Click += new System.EventHandler(this.btnBackColor_Click);
            // 
            // lblTextColor
            // 
            this.lblTextColor.AutoSize = true;
            this.lblTextColor.Location = new System.Drawing.Point(485, 110);
            this.lblTextColor.Name = "lblTextColor";
            this.lblTextColor.Size = new System.Drawing.Size(52, 13);
            this.lblTextColor.TabIndex = 93;
            this.lblTextColor.Text = "ForeColor";
            // 
            // lblBorderColor
            // 
            this.lblBorderColor.AutoSize = true;
            this.lblBorderColor.Location = new System.Drawing.Point(485, 81);
            this.lblBorderColor.Name = "lblBorderColor";
            this.lblBorderColor.Size = new System.Drawing.Size(62, 13);
            this.lblBorderColor.TabIndex = 92;
            this.lblBorderColor.Text = "BorderColor";
            // 
            // lblBackColor
            // 
            this.lblBackColor.AutoSize = true;
            this.lblBackColor.Location = new System.Drawing.Point(485, 50);
            this.lblBackColor.Name = "lblBackColor";
            this.lblBackColor.Size = new System.Drawing.Size(56, 13);
            this.lblBackColor.TabIndex = 91;
            this.lblBackColor.Text = "BackColor";
            // 
            // txtBorderY
            // 
            this.txtBorderY.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.cadreModelBindingSource, "VerticalBorder", true));
            this.txtBorderY.Location = new System.Drawing.Point(339, 132);
            this.txtBorderY.Name = "txtBorderY";
            this.txtBorderY.Size = new System.Drawing.Size(129, 20);
            this.txtBorderY.TabIndex = 12;
            // 
            // txtBorderX
            // 
            this.txtBorderX.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.cadreModelBindingSource, "HorizontalBorder", true));
            this.txtBorderX.Location = new System.Drawing.Point(339, 101);
            this.txtBorderX.Name = "txtBorderX";
            this.txtBorderX.Size = new System.Drawing.Size(129, 20);
            this.txtBorderX.TabIndex = 11;
            // 
            // lblBorderY
            // 
            this.lblBorderY.AutoSize = true;
            this.lblBorderY.Location = new System.Drawing.Point(252, 135);
            this.lblBorderY.Name = "lblBorderY";
            this.lblBorderY.Size = new System.Drawing.Size(45, 13);
            this.lblBorderY.TabIndex = 88;
            this.lblBorderY.Text = "YBorder";
            // 
            // lblBorderX
            // 
            this.lblBorderX.AutoSize = true;
            this.lblBorderX.Location = new System.Drawing.Point(252, 104);
            this.lblBorderX.Name = "lblBorderX";
            this.lblBorderX.Size = new System.Drawing.Size(45, 13);
            this.lblBorderX.TabIndex = 87;
            this.lblBorderX.Text = "XBorder";
            // 
            // txtExpY
            // 
            this.txtExpY.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.cadreModelBindingSource, "VerticalPadding", true));
            this.txtExpY.Location = new System.Drawing.Point(83, 132);
            this.txtExpY.Name = "txtExpY";
            this.txtExpY.Size = new System.Drawing.Size(129, 20);
            this.txtExpY.TabIndex = 8;
            // 
            // txtEspX
            // 
            this.txtEspX.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.cadreModelBindingSource, "HorizontalPadding", true));
            this.txtEspX.Location = new System.Drawing.Point(83, 101);
            this.txtEspX.Name = "txtEspX";
            this.txtEspX.Size = new System.Drawing.Size(129, 20);
            this.txtEspX.TabIndex = 7;
            // 
            // lblPadY
            // 
            this.lblPadY.AutoSize = true;
            this.lblPadY.Location = new System.Drawing.Point(19, 135);
            this.lblPadY.Name = "lblPadY";
            this.lblPadY.Size = new System.Drawing.Size(53, 13);
            this.lblPadY.TabIndex = 84;
            this.lblPadY.Text = "YPadding";
            // 
            // lblPadX
            // 
            this.lblPadX.AutoSize = true;
            this.lblPadX.Location = new System.Drawing.Point(19, 104);
            this.lblPadX.Name = "lblPadX";
            this.lblPadX.Size = new System.Drawing.Size(53, 13);
            this.lblPadX.TabIndex = 83;
            this.lblPadX.Text = "XPadding";
            // 
            // txtY
            // 
            this.txtY.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.cadreModelBindingSource, "VerticalPosition", true));
            this.txtY.Location = new System.Drawing.Point(339, 73);
            this.txtY.Name = "txtY";
            this.txtY.Size = new System.Drawing.Size(129, 20);
            this.txtY.TabIndex = 10;
            // 
            // txtX
            // 
            this.txtX.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.cadreModelBindingSource, "HorizontalPosition", true));
            this.txtX.Location = new System.Drawing.Point(339, 42);
            this.txtX.Name = "txtX";
            this.txtX.Size = new System.Drawing.Size(129, 20);
            this.txtX.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(252, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 80;
            this.label1.Text = "YCoordinate";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(252, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 79;
            this.label2.Text = "XCoordinate";
            // 
            // txtSizeY
            // 
            this.txtSizeY.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.cadreModelBindingSource, "Hauteur", true));
            this.txtSizeY.Location = new System.Drawing.Point(83, 73);
            this.txtSizeY.Name = "txtSizeY";
            this.txtSizeY.Size = new System.Drawing.Size(129, 20);
            this.txtSizeY.TabIndex = 6;
            // 
            // txtSizeX
            // 
            this.txtSizeX.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.cadreModelBindingSource, "Largeur", true));
            this.txtSizeX.Location = new System.Drawing.Point(83, 42);
            this.txtSizeX.Name = "txtSizeX";
            this.txtSizeX.Size = new System.Drawing.Size(129, 20);
            this.txtSizeX.TabIndex = 5;
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(6, 16);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(62, 13);
            this.lblType.TabIndex = 76;
            this.lblType.Text = "ObjectType";
            // 
            // lblSizeY
            // 
            this.lblSizeY.AutoSize = true;
            this.lblSizeY.Location = new System.Drawing.Point(19, 76);
            this.lblSizeY.Name = "lblSizeY";
            this.lblSizeY.Size = new System.Drawing.Size(38, 13);
            this.lblSizeY.TabIndex = 74;
            this.lblSizeY.Text = "Height";
            // 
            // lblSizeX
            // 
            this.lblSizeX.AutoSize = true;
            this.lblSizeX.Location = new System.Drawing.Point(19, 45);
            this.lblSizeX.Name = "lblSizeX";
            this.lblSizeX.Size = new System.Drawing.Size(35, 13);
            this.lblSizeX.TabIndex = 73;
            this.lblSizeX.Text = "Width";
            // 
            // lblScale
            // 
            this.lblScale.AutoSize = true;
            this.lblScale.Location = new System.Drawing.Point(381, 8);
            this.lblScale.Name = "lblScale";
            this.lblScale.Size = new System.Drawing.Size(34, 13);
            this.lblScale.TabIndex = 99;
            this.lblScale.Text = "Scale";
            // 
            // cmbScale
            // 
            this.cmbScale.FormattingEnabled = true;
            this.cmbScale.Location = new System.Drawing.Point(441, 5);
            this.cmbScale.Name = "cmbScale";
            this.cmbScale.Size = new System.Drawing.Size(163, 21);
            this.cmbScale.TabIndex = 2;
            // 
            // PropertiesUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblScale);
            this.Controls.Add(this.cmbScale);
            this.Controls.Add(this.btnSuppr);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.grpDatas);
            this.Name = "PropertiesUC";
            this.Size = new System.Drawing.Size(618, 198);
            this.grpDatas.ResumeLayout(false);
            this.grpDatas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cadreModelBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnSuppr;
        private System.Windows.Forms.GroupBox grpDatas;
        private System.Windows.Forms.Button btnForeColor;
        private System.Windows.Forms.Button btnBorderColor;
        private System.Windows.Forms.Button btnBackColor;
        private System.Windows.Forms.Label lblTextColor;
        private System.Windows.Forms.Label lblBorderColor;
        private System.Windows.Forms.Label lblBackColor;
        private System.Windows.Forms.TextBox txtBorderY;
        private System.Windows.Forms.TextBox txtBorderX;
        private System.Windows.Forms.Label lblBorderY;
        private System.Windows.Forms.Label lblBorderX;
        private System.Windows.Forms.TextBox txtExpY;
        private System.Windows.Forms.TextBox txtEspX;
        private System.Windows.Forms.Label lblPadY;
        private System.Windows.Forms.Label lblPadX;
        private System.Windows.Forms.TextBox txtY;
        private System.Windows.Forms.TextBox txtX;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSizeY;
        private System.Windows.Forms.TextBox txtSizeX;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.Label lblSizeY;
        private System.Windows.Forms.Label lblSizeX;
        private System.Windows.Forms.Button btnType;
        private System.Windows.Forms.Label lblScale;
        public System.Windows.Forms.ComboBox cmbScale;
        private System.Windows.Forms.BindingSource cadreModelBindingSource;
    }
}
