namespace EasyHTMLDev
{
    partial class Attributes
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
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtId = new System.Windows.Forms.TextBox();
            this.bsAttributes = new System.Windows.Forms.BindingSource(this.components);
            this.rbCustomId = new System.Windows.Forms.RadioButton();
            this.rbAutomaticId = new System.Windows.Forms.RadioButton();
            this.cbId = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtClass = new System.Windows.Forms.TextBox();
            this.rbCustomClass = new System.Windows.Forms.RadioButton();
            this.cbClass = new System.Windows.Forms.CheckBox();
            this.rbAutomaticClass = new System.Windows.Forms.RadioButton();
            this.rbIdCSS = new System.Windows.Forms.RadioButton();
            this.rbClassCSS = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsAttributes)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtId);
            this.groupBox1.Controls.Add(this.rbCustomId);
            this.groupBox1.Controls.Add(this.rbAutomaticId);
            this.groupBox1.Controls.Add(this.cbId);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(462, 56);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Id";
            // 
            // txtId
            // 
            this.txtId.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsAttributes, "Id", true));
            this.txtId.Location = new System.Drawing.Point(234, 18);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(82, 20);
            this.txtId.TabIndex = 3;
            this.txtId.Validating += new System.ComponentModel.CancelEventHandler(this.txtId_Validating);
            // 
            // bsAttributes
            // 
            this.bsAttributes.DataSource = typeof(Library.Attributes);
            // 
            // rbCustomId
            // 
            this.rbCustomId.AutoSize = true;
            this.rbCustomId.Location = new System.Drawing.Point(155, 19);
            this.rbCustomId.Name = "rbCustomId";
            this.rbCustomId.Size = new System.Drawing.Size(59, 17);
            this.rbCustomId.TabIndex = 2;
            this.rbCustomId.TabStop = true;
            this.rbCustomId.Text = "custom";
            this.rbCustomId.UseVisualStyleBackColor = true;
            this.rbCustomId.CheckedChanged += new System.EventHandler(this.rbCustomId_CheckedChanged);
            // 
            // rbAutomaticId
            // 
            this.rbAutomaticId.AutoSize = true;
            this.rbAutomaticId.Location = new System.Drawing.Point(78, 19);
            this.rbAutomaticId.Name = "rbAutomaticId";
            this.rbAutomaticId.Size = new System.Drawing.Size(71, 17);
            this.rbAutomaticId.TabIndex = 1;
            this.rbAutomaticId.TabStop = true;
            this.rbAutomaticId.Text = "automatic";
            this.rbAutomaticId.UseVisualStyleBackColor = true;
            this.rbAutomaticId.CheckedChanged += new System.EventHandler(this.rbAutomaticId_CheckedChanged);
            // 
            // cbId
            // 
            this.cbId.AutoSize = true;
            this.cbId.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.bsAttributes, "HasId", true));
            this.cbId.Location = new System.Drawing.Point(7, 20);
            this.cbId.Name = "cbId";
            this.cbId.Size = new System.Drawing.Size(52, 17);
            this.cbId.TabIndex = 0;
            this.cbId.Text = "exists";
            this.cbId.UseVisualStyleBackColor = true;
            this.cbId.CheckedChanged += new System.EventHandler(this.cbId_CheckedChanged);
            this.cbId.Validated += new System.EventHandler(this.cb_Validated);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtClass);
            this.groupBox2.Controls.Add(this.rbCustomClass);
            this.groupBox2.Controls.Add(this.cbClass);
            this.groupBox2.Controls.Add(this.rbAutomaticClass);
            this.groupBox2.Location = new System.Drawing.Point(12, 75);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(462, 56);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Class";
            // 
            // txtClass
            // 
            this.txtClass.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsAttributes, "Class", true));
            this.txtClass.Location = new System.Drawing.Point(235, 16);
            this.txtClass.Name = "txtClass";
            this.txtClass.Size = new System.Drawing.Size(82, 20);
            this.txtClass.TabIndex = 3;
            this.txtClass.Validating += new System.ComponentModel.CancelEventHandler(this.txtClass_Validating);
            // 
            // rbCustomClass
            // 
            this.rbCustomClass.AutoSize = true;
            this.rbCustomClass.Location = new System.Drawing.Point(156, 19);
            this.rbCustomClass.Name = "rbCustomClass";
            this.rbCustomClass.Size = new System.Drawing.Size(59, 17);
            this.rbCustomClass.TabIndex = 2;
            this.rbCustomClass.TabStop = true;
            this.rbCustomClass.Text = "custom";
            this.rbCustomClass.UseVisualStyleBackColor = true;
            this.rbCustomClass.CheckedChanged += new System.EventHandler(this.rbCustomClass_CheckedChanged);
            // 
            // cbClass
            // 
            this.cbClass.AutoSize = true;
            this.cbClass.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.bsAttributes, "HasClass", true));
            this.cbClass.Location = new System.Drawing.Point(8, 20);
            this.cbClass.Name = "cbClass";
            this.cbClass.Size = new System.Drawing.Size(52, 17);
            this.cbClass.TabIndex = 0;
            this.cbClass.Text = "exists";
            this.cbClass.UseVisualStyleBackColor = true;
            this.cbClass.CheckedChanged += new System.EventHandler(this.cbClass_CheckedChanged);
            this.cbClass.Validated += new System.EventHandler(this.cb_Validated);
            // 
            // rbAutomaticClass
            // 
            this.rbAutomaticClass.AutoSize = true;
            this.rbAutomaticClass.Location = new System.Drawing.Point(79, 19);
            this.rbAutomaticClass.Name = "rbAutomaticClass";
            this.rbAutomaticClass.Size = new System.Drawing.Size(71, 17);
            this.rbAutomaticClass.TabIndex = 1;
            this.rbAutomaticClass.TabStop = true;
            this.rbAutomaticClass.Text = "automatic";
            this.rbAutomaticClass.UseVisualStyleBackColor = true;
            this.rbAutomaticClass.CheckedChanged += new System.EventHandler(this.rbAutomaticClass_CheckedChanged);
            // 
            // rbIdCSS
            // 
            this.rbIdCSS.AutoSize = true;
            this.rbIdCSS.Location = new System.Drawing.Point(91, 137);
            this.rbIdCSS.Name = "rbIdCSS";
            this.rbIdCSS.Size = new System.Drawing.Size(92, 17);
            this.rbIdCSS.TabIndex = 1;
            this.rbIdCSS.TabStop = true;
            this.rbIdCSS.Text = "use id for CSS";
            this.rbIdCSS.UseVisualStyleBackColor = true;
            this.rbIdCSS.CheckedChanged += new System.EventHandler(this.rbIdCSS_CheckedChanged);
            // 
            // rbClassCSS
            // 
            this.rbClassCSS.AutoSize = true;
            this.rbClassCSS.Location = new System.Drawing.Point(224, 137);
            this.rbClassCSS.Name = "rbClassCSS";
            this.rbClassCSS.Size = new System.Drawing.Size(108, 17);
            this.rbClassCSS.TabIndex = 1;
            this.rbClassCSS.TabStop = true;
            this.rbClassCSS.Text = "use class for CSS";
            this.rbClassCSS.UseVisualStyleBackColor = true;
            this.rbClassCSS.CheckedChanged += new System.EventHandler(this.rbClassCSS_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(384, 164);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(89, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "CSSOptions";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(20, 164);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "ButtonClose";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Attributes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(487, 208);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.rbClassCSS);
            this.Controls.Add(this.rbIdCSS);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Attributes";
            this.Text = "Attributes";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Attributes_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsAttributes)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.RadioButton rbCustomId;
        private System.Windows.Forms.RadioButton rbAutomaticId;
        private System.Windows.Forms.CheckBox cbId;
        private System.Windows.Forms.TextBox txtClass;
        private System.Windows.Forms.RadioButton rbCustomClass;
        private System.Windows.Forms.CheckBox cbClass;
        private System.Windows.Forms.RadioButton rbAutomaticClass;
        private System.Windows.Forms.RadioButton rbIdCSS;
        private System.Windows.Forms.RadioButton rbClassCSS;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.BindingSource bsAttributes;
    }
}