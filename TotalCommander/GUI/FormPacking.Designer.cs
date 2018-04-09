namespace TotalCommander.GUI
{
    partial class FormPacking
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPacking));
            this.label1 = new System.Windows.Forms.Label();
            this.lblSaveFilePath = new System.Windows.Forms.Label();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cboArchiveFormat = new System.Windows.Forms.ComboBox();
            this.cboCompressionLevel = new System.Windows.Forms.ComboBox();
            this.cboPathMode = new System.Windows.Forms.ComboBox();
            this.cboUpdateMode = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gbEncryption = new System.Windows.Forms.GroupBox();
            this.txtPass1 = new System.Windows.Forms.TextBox();
            this.txtPass2 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkSFX = new System.Windows.Forms.CheckBox();
            this.chkDeleteAfterCompression = new System.Windows.Forms.CheckBox();
            this.btnShowPassword = new System.Windows.Forms.Button();
            this.cboEncryptMethod = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOpenSaveDialog = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.gbEncryption.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Archive:";
            // 
            // lblSaveFilePath
            // 
            this.lblSaveFilePath.AutoSize = true;
            this.lblSaveFilePath.Location = new System.Drawing.Point(60, 4);
            this.lblSaveFilePath.Name = "lblSaveFilePath";
            this.lblSaveFilePath.Size = new System.Drawing.Size(31, 15);
            this.lblSaveFilePath.TabIndex = 1;
            this.lblSaveFilePath.Text = "Path";
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(60, 22);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(518, 23);
            this.txtFileName.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "Archive format:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Compression level:";
            // 
            // cboArchiveFormat
            // 
            this.cboArchiveFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboArchiveFormat.FormattingEnabled = true;
            this.cboArchiveFormat.Location = new System.Drawing.Point(119, 58);
            this.cboArchiveFormat.Name = "cboArchiveFormat";
            this.cboArchiveFormat.Size = new System.Drawing.Size(121, 23);
            this.cboArchiveFormat.TabIndex = 6;
            this.cboArchiveFormat.TabStop = false;
            // 
            // cboCompressionLevel
            // 
            this.cboCompressionLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCompressionLevel.FormattingEnabled = true;
            this.cboCompressionLevel.Location = new System.Drawing.Point(119, 89);
            this.cboCompressionLevel.Name = "cboCompressionLevel";
            this.cboCompressionLevel.Size = new System.Drawing.Size(121, 23);
            this.cboCompressionLevel.TabIndex = 7;
            this.cboCompressionLevel.TabStop = false;
            // 
            // cboPathMode
            // 
            this.cboPathMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPathMode.FormattingEnabled = true;
            this.cboPathMode.Location = new System.Drawing.Point(434, 89);
            this.cboPathMode.Name = "cboPathMode";
            this.cboPathMode.Size = new System.Drawing.Size(180, 23);
            this.cboPathMode.TabIndex = 11;
            this.cboPathMode.TabStop = false;
            // 
            // cboUpdateMode
            // 
            this.cboUpdateMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboUpdateMode.FormattingEnabled = true;
            this.cboUpdateMode.Location = new System.Drawing.Point(434, 58);
            this.cboUpdateMode.Name = "cboUpdateMode";
            this.cboUpdateMode.Size = new System.Drawing.Size(180, 23);
            this.cboUpdateMode.TabIndex = 10;
            this.cboUpdateMode.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(319, 89);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 15);
            this.label4.TabIndex = 9;
            this.label4.Text = "Path mode:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(319, 61);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 15);
            this.label5.TabIndex = 8;
            this.label5.Text = "Update mode:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkDeleteAfterCompression);
            this.groupBox1.Controls.Add(this.chkSFX);
            this.groupBox1.Location = new System.Drawing.Point(7, 128);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(301, 80);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Options";
            // 
            // gbEncryption
            // 
            this.gbEncryption.Controls.Add(this.panel2);
            this.gbEncryption.Controls.Add(this.cboEncryptMethod);
            this.gbEncryption.Controls.Add(this.label8);
            this.gbEncryption.Controls.Add(this.panel1);
            this.gbEncryption.Controls.Add(this.label6);
            this.gbEncryption.Controls.Add(this.label7);
            this.gbEncryption.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gbEncryption.Location = new System.Drawing.Point(314, 128);
            this.gbEncryption.Name = "gbEncryption";
            this.gbEncryption.Size = new System.Drawing.Size(311, 158);
            this.gbEncryption.TabIndex = 13;
            this.gbEncryption.TabStop = false;
            this.gbEncryption.Text = "Encryption";
            // 
            // txtPass1
            // 
            this.txtPass1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPass1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPass1.Location = new System.Drawing.Point(0, 3);
            this.txtPass1.Margin = new System.Windows.Forms.Padding(0);
            this.txtPass1.Name = "txtPass1";
            this.txtPass1.Size = new System.Drawing.Size(290, 22);
            this.txtPass1.TabIndex = 14;
            // 
            // txtPass2
            // 
            this.txtPass2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPass2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPass2.Location = new System.Drawing.Point(0, 3);
            this.txtPass2.Margin = new System.Windows.Forms.Padding(0);
            this.txtPass2.Name = "txtPass2";
            this.txtPass2.Size = new System.Drawing.Size(256, 22);
            this.txtPass2.TabIndex = 15;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 65);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(103, 15);
            this.label6.TabIndex = 15;
            this.label6.Text = "Reenter password:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 19);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(90, 15);
            this.label7.TabIndex = 14;
            this.label7.Text = "Enter password:";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnShowPassword);
            this.panel1.Controls.Add(this.txtPass2);
            this.panel1.Location = new System.Drawing.Point(9, 83);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(290, 30);
            this.panel1.TabIndex = 16;
            // 
            // chkSFX
            // 
            this.chkSFX.AutoSize = true;
            this.chkSFX.Location = new System.Drawing.Point(6, 22);
            this.chkSFX.Name = "chkSFX";
            this.chkSFX.Size = new System.Drawing.Size(123, 19);
            this.chkSFX.TabIndex = 0;
            this.chkSFX.TabStop = false;
            this.chkSFX.Text = "Create SFX archive";
            this.chkSFX.UseVisualStyleBackColor = true;
            // 
            // chkDeleteAfterCompression
            // 
            this.chkDeleteAfterCompression.AutoSize = true;
            this.chkDeleteAfterCompression.Location = new System.Drawing.Point(5, 47);
            this.chkDeleteAfterCompression.Name = "chkDeleteAfterCompression";
            this.chkDeleteAfterCompression.Size = new System.Drawing.Size(181, 19);
            this.chkDeleteAfterCompression.TabIndex = 2;
            this.chkDeleteAfterCompression.TabStop = false;
            this.chkDeleteAfterCompression.Text = "Delete files after compression";
            this.chkDeleteAfterCompression.UseVisualStyleBackColor = true;
            // 
            // btnShowPassword
            // 
            this.btnShowPassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShowPassword.Image = global::TotalCommander.Properties.Resources.Show_Password_24;
            this.btnShowPassword.Location = new System.Drawing.Point(259, -1);
            this.btnShowPassword.Margin = new System.Windows.Forms.Padding(0);
            this.btnShowPassword.Name = "btnShowPassword";
            this.btnShowPassword.Size = new System.Drawing.Size(30, 30);
            this.btnShowPassword.TabIndex = 16;
            this.btnShowPassword.TabStop = false;
            this.btnShowPassword.UseVisualStyleBackColor = true;
            // 
            // cboEncryptMethod
            // 
            this.cboEncryptMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEncryptMethod.FormattingEnabled = true;
            this.cboEncryptMethod.Location = new System.Drawing.Point(126, 125);
            this.cboEncryptMethod.Name = "cboEncryptMethod";
            this.cboEncryptMethod.Size = new System.Drawing.Size(121, 23);
            this.cboEncryptMethod.TabIndex = 15;
            this.cboEncryptMethod.TabStop = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 128);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(112, 15);
            this.label8.TabIndex = 14;
            this.label8.Text = "Encryption method:";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnOK.AutoSize = true;
            this.btnOK.BackColor = System.Drawing.Color.Green;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.ForeColor = System.Drawing.SystemColors.Window;
            this.btnOK.Location = new System.Drawing.Point(24, 246);
            this.btnOK.Margin = new System.Windows.Forms.Padding(0);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(55, 27);
            this.btnOK.TabIndex = 17;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCancel.AutoSize = true;
            this.btnCancel.BackColor = System.Drawing.Color.Red;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.SystemColors.Window;
            this.btnCancel.Location = new System.Drawing.Point(132, 246);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(55, 27);
            this.btnCancel.TabIndex = 18;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // btnOpenSaveDialog
            // 
            this.btnOpenSaveDialog.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnOpenSaveDialog.AutoSize = true;
            this.btnOpenSaveDialog.BackColor = System.Drawing.Color.Green;
            this.btnOpenSaveDialog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenSaveDialog.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenSaveDialog.ForeColor = System.Drawing.SystemColors.Window;
            this.btnOpenSaveDialog.Location = new System.Drawing.Point(582, 19);
            this.btnOpenSaveDialog.Margin = new System.Windows.Forms.Padding(0);
            this.btnOpenSaveDialog.Name = "btnOpenSaveDialog";
            this.btnOpenSaveDialog.Size = new System.Drawing.Size(28, 27);
            this.btnOpenSaveDialog.TabIndex = 19;
            this.btnOpenSaveDialog.TabStop = false;
            this.btnOpenSaveDialog.Text = "...";
            this.btnOpenSaveDialog.UseVisualStyleBackColor = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Window;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.txtPass1);
            this.panel2.Location = new System.Drawing.Point(9, 36);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(290, 30);
            this.panel2.TabIndex = 20;
            // 
            // FormPacking
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 309);
            this.Controls.Add(this.btnOpenSaveDialog);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.gbEncryption);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cboPathMode);
            this.Controls.Add(this.cboUpdateMode);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cboCompressionLevel);
            this.Controls.Add(this.cboArchiveFormat);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtFileName);
            this.Controls.Add(this.lblSaveFilePath);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormPacking";
            this.Text = "Pack Files";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbEncryption.ResumeLayout(false);
            this.gbEncryption.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblSaveFilePath;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboArchiveFormat;
        private System.Windows.Forms.ComboBox cboCompressionLevel;
        private System.Windows.Forms.ComboBox cboPathMode;
        private System.Windows.Forms.ComboBox cboUpdateMode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox gbEncryption;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtPass2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtPass1;
        private System.Windows.Forms.CheckBox chkDeleteAfterCompression;
        private System.Windows.Forms.CheckBox chkSFX;
        private System.Windows.Forms.ComboBox cboEncryptMethod;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnShowPassword;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnOpenSaveDialog;
    }
}