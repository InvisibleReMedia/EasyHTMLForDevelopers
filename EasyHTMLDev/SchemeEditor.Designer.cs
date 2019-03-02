namespace EasyHTMLDev
{
    partial class SchemeEditor
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
            this.txtCSS = new System.Windows.Forms.TextBox();
            this.linkPaletton = new System.Windows.Forms.LinkLabel();
            this.lblComment = new System.Windows.Forms.Label();
            this.epCSS = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnAdd = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.epCSS)).BeginInit();
            this.SuspendLayout();
            // 
            // txtCSS
            // 
            this.txtCSS.AcceptsReturn = true;
            this.txtCSS.AcceptsTab = true;
            this.txtCSS.Location = new System.Drawing.Point(23, 52);
            this.txtCSS.Multiline = true;
            this.txtCSS.Name = "txtCSS";
            this.txtCSS.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtCSS.Size = new System.Drawing.Size(266, 113);
            this.txtCSS.TabIndex = 14;
            this.txtCSS.Validating += new System.ComponentModel.CancelEventHandler(this.txtCSS_Validating);
            // 
            // linkPaletton
            // 
            this.linkPaletton.AutoSize = true;
            this.linkPaletton.Location = new System.Drawing.Point(23, 28);
            this.linkPaletton.Name = "linkPaletton";
            this.linkPaletton.Size = new System.Drawing.Size(99, 13);
            this.linkPaletton.TabIndex = 13;
            this.linkPaletton.TabStop = true;
            this.linkPaletton.Text = "http://paletton.com";
            // 
            // lblComment
            // 
            this.lblComment.AutoSize = true;
            this.lblComment.Location = new System.Drawing.Point(23, 11);
            this.lblComment.Name = "lblComment";
            this.lblComment.Size = new System.Drawing.Size(114, 13);
            this.lblComment.TabIndex = 12;
            this.lblComment.Text = "SchemeColorComment";
            // 
            // epCSS
            // 
            this.epCSS.ContainerControl = this;
            this.epCSS.RightToLeft = true;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(214, 171);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 15;
            this.btnAdd.Text = "ButtonAdd";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // SchemeEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.txtCSS);
            this.Controls.Add(this.linkPaletton);
            this.Controls.Add(this.lblComment);
            this.Name = "SchemeEditor";
            this.Size = new System.Drawing.Size(313, 204);
            ((System.ComponentModel.ISupportInitialize)(this.epCSS)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel linkPaletton;
        private System.Windows.Forms.Label lblComment;
        private System.Windows.Forms.ErrorProvider epCSS;
        public System.Windows.Forms.TextBox txtCSS;
        private System.Windows.Forms.Button btnAdd;
    }
}
