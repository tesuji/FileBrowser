namespace TotalCommander.GUI
{
    partial class ShellBrowser
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
            this.lblBotStatus = new System.Windows.Forms.Label();
            this.lblTopStorageStatus = new System.Windows.Forms.Label();
            this.disksBrowser = new TotalCommander.GUI.DisksBrowser();
            this.splMainView = new System.Windows.Forms.SplitContainer();
            this.navigationPane = new TotalCommander.GUI.NavigationPane();
            this.browser = new TotalCommander.GUI.FileBrowser();
            this.txtPath = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splMainView)).BeginInit();
            this.splMainView.Panel1.SuspendLayout();
            this.splMainView.Panel2.SuspendLayout();
            this.splMainView.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblBotStatus
            // 
            this.lblBotStatus.AutoSize = true;
            this.lblBotStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblBotStatus.Location = new System.Drawing.Point(0, 570);
            this.lblBotStatus.Name = "lblBotStatus";
            this.lblBotStatus.Size = new System.Drawing.Size(82, 15);
            this.lblBotStatus.TabIndex = 22;
            this.lblBotStatus.Text = "Bottom Status";
            // 
            // lblTopStorageStatus
            // 
            this.lblTopStorageStatus.AutoSize = true;
            this.lblTopStorageStatus.Location = new System.Drawing.Point(60, 6);
            this.lblTopStorageStatus.Name = "lblTopStorageStatus";
            this.lblTopStorageStatus.Size = new System.Drawing.Size(79, 15);
            this.lblTopStorageStatus.TabIndex = 21;
            this.lblTopStorageStatus.Text = "StorageStatus";
            this.lblTopStorageStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // disksBrowser
            // 
            this.disksBrowser.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.disksBrowser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.disksBrowser.FormattingEnabled = true;
            this.disksBrowser.Location = new System.Drawing.Point(0, 0);
            this.disksBrowser.Name = "disksBrowser";
            this.disksBrowser.Size = new System.Drawing.Size(60, 24);
            this.disksBrowser.TabIndex = 6;
            this.disksBrowser.TabStop = false;
            // 
            // splMainView
            // 
            this.splMainView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splMainView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splMainView.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splMainView.Location = new System.Drawing.Point(0, 46);
            this.splMainView.Name = "splMainView";
            // 
            // splMainView.Panel1
            // 
            this.splMainView.Panel1.Controls.Add(this.navigationPane);
            // 
            // splMainView.Panel2
            // 
            this.splMainView.Panel2.Controls.Add(this.browser);
            this.splMainView.Size = new System.Drawing.Size(524, 518);
            this.splMainView.SplitterDistance = 173;
            this.splMainView.SplitterWidth = 3;
            this.splMainView.TabIndex = 23;
            this.splMainView.TabStop = false;
            // 
            // navigationPane
            // 
            this.navigationPane.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.navigationPane.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navigationPane.Location = new System.Drawing.Point(0, 0);
            this.navigationPane.Name = "navigationPane";
            this.navigationPane.Size = new System.Drawing.Size(171, 516);
            this.navigationPane.TabIndex = 0;
            this.navigationPane.TabStop = false;
            // 
            // browser
            // 
            this.browser.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.browser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.browser.Location = new System.Drawing.Point(0, 0);
            this.browser.Name = "browser";
            this.browser.Size = new System.Drawing.Size(346, 516);
            this.browser.TabIndex = 0;
            this.browser.UseCompatibleStateImageBehavior = false;
            // 
            // txtPath
            // 
            this.txtPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPath.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtPath.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
            this.txtPath.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txtPath.Location = new System.Drawing.Point(0, 23);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(524, 23);
            this.txtPath.TabIndex = 24;
            this.txtPath.TabStop = false;
            // 
            // ShellBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.splMainView);
            this.Controls.Add(this.lblBotStatus);
            this.Controls.Add(this.disksBrowser);
            this.Controls.Add(this.lblTopStorageStatus);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ShellBrowser";
            this.Size = new System.Drawing.Size(524, 585);
            this.splMainView.Panel1.ResumeLayout(false);
            this.splMainView.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splMainView)).EndInit();
            this.splMainView.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TotalCommander.GUI.DisksBrowser disksBrowser;
        private System.Windows.Forms.Label lblTopStorageStatus;
        private System.Windows.Forms.Label lblBotStatus;
        private FileBrowser browser;
        private System.Windows.Forms.SplitContainer splMainView;
        private NavigationPane navigationPane;
        private System.Windows.Forms.TextBox txtPath;

    }
}
