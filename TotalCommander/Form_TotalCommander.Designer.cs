namespace TotalCommander
{
    partial class Form_TotalCommander
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_TotalCommander));
            this.smallImgList = new System.Windows.Forms.ImageList(this.components);
            this.tsmiMark = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCommands = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiNet = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiShow = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiConfiguration = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiStart = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.tsmiFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiViewProperties = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiKeyboards = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.tipButtons = new System.Windows.Forms.ToolTip(this.components);
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.tsMain = new System.Windows.Forms.ToolStrip();
            this.tsbtnRefreshWindows = new System.Windows.Forms.ToolStripButton();
            this.tsbtnGoBackward = new System.Windows.Forms.ToolStripButton();
            this.tsbtnGoForward = new System.Windows.Forms.ToolStripButton();
            this.tsbtnGoParent = new System.Windows.Forms.ToolStripButton();
            this.tsbtnPackFiles = new System.Windows.Forms.ToolStripButton();
            this.tsbtnDetailViewMode = new System.Windows.Forms.ToolStripButton();
            this.tsbtnListViewMode = new System.Windows.Forms.ToolStripButton();
            this.tsbtnTreeView = new System.Windows.Forms.ToolStripButton();
            this.tsbtnAddNewFolder = new System.Windows.Forms.ToolStripButton();
            this.tsbtnAddNewFile = new System.Windows.Forms.ToolStripButton();
            this.splMain = new System.Windows.Forms.SplitContainer();
            this.shellBrowserLeft = new TotalCommander.GUI.ShellBrowser();
            this.shellBrowserRight = new TotalCommander.GUI.ShellBrowser();
            this.tlpnBottoms = new System.Windows.Forms.TableLayoutPanel();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnF8Delete = new System.Windows.Forms.Button();
            this.btnF7NewFolder = new System.Windows.Forms.Button();
            this.btnF6Move = new System.Windows.Forms.Button();
            this.btnF1Copy = new System.Windows.Forms.Button();
            this.btnF3View = new System.Windows.Forms.Button();
            this.btnF4Edit = new System.Windows.Forms.Button();
            this.mnuMain.SuspendLayout();
            this.tsMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splMain)).BeginInit();
            this.splMain.Panel1.SuspendLayout();
            this.splMain.Panel2.SuspendLayout();
            this.splMain.SuspendLayout();
            this.tlpnBottoms.SuspendLayout();
            this.SuspendLayout();
            // 
            // smallImgList
            // 
            this.smallImgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("smallImgList.ImageStream")));
            this.smallImgList.TransparentColor = System.Drawing.Color.Transparent;
            this.smallImgList.Images.SetKeyName(0, "hidden_folder");
            this.smallImgList.Images.SetKeyName(1, "unknown");
            // 
            // tsmiMark
            // 
            this.tsmiMark.Name = "tsmiMark";
            this.tsmiMark.Size = new System.Drawing.Size(46, 20);
            this.tsmiMark.Text = "Mark";
            // 
            // tsmiCommands
            // 
            this.tsmiCommands.Name = "tsmiCommands";
            this.tsmiCommands.Size = new System.Drawing.Size(81, 20);
            this.tsmiCommands.Text = "Commands";
            // 
            // tsmiNet
            // 
            this.tsmiNet.Name = "tsmiNet";
            this.tsmiNet.Size = new System.Drawing.Size(38, 20);
            this.tsmiNet.Text = "Net";
            // 
            // tsmiShow
            // 
            this.tsmiShow.Name = "tsmiShow";
            this.tsmiShow.Size = new System.Drawing.Size(48, 20);
            this.tsmiShow.Text = "Show";
            // 
            // tsmiConfiguration
            // 
            this.tsmiConfiguration.Name = "tsmiConfiguration";
            this.tsmiConfiguration.Size = new System.Drawing.Size(93, 20);
            this.tsmiConfiguration.Text = "Configuration";
            // 
            // tsmiStart
            // 
            this.tsmiStart.Name = "tsmiStart";
            this.tsmiStart.Size = new System.Drawing.Size(43, 20);
            this.tsmiStart.Text = "Start";
            // 
            // mnuMain
            // 
            this.mnuMain.BackColor = System.Drawing.SystemColors.Control;
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFile,
            this.tsmiMark,
            this.tsmiCommands,
            this.tsmiNet,
            this.tsmiShow,
            this.tsmiConfiguration,
            this.tsmiStart,
            this.tsmiHelp});
            this.mnuMain.Location = new System.Drawing.Point(0, 0);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.mnuMain.Size = new System.Drawing.Size(1084, 24);
            this.mnuMain.TabIndex = 4;
            this.mnuMain.Text = "menuStrip1";
            // 
            // tsmiFile
            // 
            this.tsmiFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiViewProperties,
            this.toolStripSeparator,
            this.tsmiExit});
            this.tsmiFile.Name = "tsmiFile";
            this.tsmiFile.Size = new System.Drawing.Size(37, 20);
            this.tsmiFile.Text = "&File";
            // 
            // tsmiViewProperties
            // 
            this.tsmiViewProperties.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsmiViewProperties.Name = "tsmiViewProperties";
            this.tsmiViewProperties.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Return)));
            this.tsmiViewProperties.Size = new System.Drawing.Size(193, 22);
            this.tsmiViewProperties.Text = "&Properties...";
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(190, 6);
            // 
            // tsmiExit
            // 
            this.tsmiExit.Name = "tsmiExit";
            this.tsmiExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.tsmiExit.Size = new System.Drawing.Size(193, 22);
            this.tsmiExit.Text = "E&xit";
            // 
            // tsmiHelp
            // 
            this.tsmiHelp.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsmiHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiKeyboards,
            this.toolStripSeparator7,
            this.tsmiAbout});
            this.tsmiHelp.Name = "tsmiHelp";
            this.tsmiHelp.Size = new System.Drawing.Size(44, 20);
            this.tsmiHelp.Text = "&Help";
            // 
            // tsmiKeyboards
            // 
            this.tsmiKeyboards.Name = "tsmiKeyboards";
            this.tsmiKeyboards.Size = new System.Drawing.Size(138, 22);
            this.tsmiKeyboards.Text = "&Keyboards...";
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(135, 6);
            // 
            // tsmiAbout
            // 
            this.tsmiAbout.Name = "tsmiAbout";
            this.tsmiAbout.Size = new System.Drawing.Size(138, 22);
            this.tsmiAbout.Text = "&About...";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(6, 25);
            // 
            // tsMain
            // 
            this.tsMain.AllowMerge = false;
            this.tsMain.CanOverflow = false;
            this.tsMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnRefreshWindows,
            this.toolStripSeparator1,
            this.tsbtnGoBackward,
            this.tsbtnGoForward,
            this.tsbtnGoParent,
            this.toolStripSeparator2,
            this.tsbtnPackFiles,
            this.toolStripSeparator10,
            this.tsbtnDetailViewMode,
            this.tsbtnListViewMode,
            this.tsbtnTreeView,
            this.toolStripSeparator11,
            this.tsbtnAddNewFolder,
            this.tsbtnAddNewFile});
            this.tsMain.Location = new System.Drawing.Point(0, 24);
            this.tsMain.Name = "tsMain";
            this.tsMain.Size = new System.Drawing.Size(1084, 25);
            this.tsMain.Stretch = true;
            this.tsMain.TabIndex = 5;
            // 
            // tsbtnRefreshWindows
            // 
            this.tsbtnRefreshWindows.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnRefreshWindows.Image = global::TotalCommander.Properties.Resources.refresh_icon;
            this.tsbtnRefreshWindows.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnRefreshWindows.Name = "tsbtnRefreshWindows";
            this.tsbtnRefreshWindows.Size = new System.Drawing.Size(23, 22);
            this.tsbtnRefreshWindows.Text = "toolStripButton1";
            this.tsbtnRefreshWindows.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbtnRefreshWindows.ToolTipText = "Reread source";
            // 
            // tsbtnGoBackward
            // 
            this.tsbtnGoBackward.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnGoBackward.Image = global::TotalCommander.Properties.Resources.ic_chevron_left_black_48dp;
            this.tsbtnGoBackward.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnGoBackward.Name = "tsbtnGoBackward";
            this.tsbtnGoBackward.Size = new System.Drawing.Size(23, 22);
            this.tsbtnGoBackward.Text = "toolStripButton2";
            this.tsbtnGoBackward.ToolTipText = "Go back";
            // 
            // tsbtnGoForward
            // 
            this.tsbtnGoForward.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnGoForward.Image = global::TotalCommander.Properties.Resources.ic_chevron_right_black_48dp;
            this.tsbtnGoForward.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnGoForward.Name = "tsbtnGoForward";
            this.tsbtnGoForward.Size = new System.Drawing.Size(23, 22);
            this.tsbtnGoForward.Text = "toolStripButton3";
            this.tsbtnGoForward.ToolTipText = "Go forward";
            // 
            // tsbtnGoParent
            // 
            this.tsbtnGoParent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnGoParent.Image = global::TotalCommander.Properties.Resources.ic_arrow_upward_black_48dp;
            this.tsbtnGoParent.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnGoParent.Name = "tsbtnGoParent";
            this.tsbtnGoParent.Size = new System.Drawing.Size(23, 22);
            this.tsbtnGoParent.ToolTipText = "Go Up";
            // 
            // tsbtnPackFiles
            // 
            this.tsbtnPackFiles.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnPackFiles.Image = global::TotalCommander.Properties.Resources.ic_archive_black_48dp;
            this.tsbtnPackFiles.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnPackFiles.Name = "tsbtnPackFiles";
            this.tsbtnPackFiles.Size = new System.Drawing.Size(23, 22);
            this.tsbtnPackFiles.Text = "toolStripButton4";
            this.tsbtnPackFiles.ToolTipText = "Pack files";
            // 
            // tsbtnDetailViewMode
            // 
            this.tsbtnDetailViewMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnDetailViewMode.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnDetailViewMode.Image")));
            this.tsbtnDetailViewMode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnDetailViewMode.Name = "tsbtnDetailViewMode";
            this.tsbtnDetailViewMode.Size = new System.Drawing.Size(23, 22);
            this.tsbtnDetailViewMode.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbtnDetailViewMode.ToolTipText = "View in Details Mode";
            // 
            // tsbtnListViewMode
            // 
            this.tsbtnListViewMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnListViewMode.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnListViewMode.Image")));
            this.tsbtnListViewMode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnListViewMode.Name = "tsbtnListViewMode";
            this.tsbtnListViewMode.Size = new System.Drawing.Size(23, 22);
            this.tsbtnListViewMode.ToolTipText = "View in List Mode";
            // 
            // tsbtnTreeView
            // 
            this.tsbtnTreeView.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnTreeView.Image = global::TotalCommander.Properties.Resources.document_tree_icon;
            this.tsbtnTreeView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnTreeView.Name = "tsbtnTreeView";
            this.tsbtnTreeView.Size = new System.Drawing.Size(23, 22);
            this.tsbtnTreeView.Text = "Show Navigation Pane";
            // 
            // tsbtnAddNewFolder
            // 
            this.tsbtnAddNewFolder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnAddNewFolder.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnAddNewFolder.Image")));
            this.tsbtnAddNewFolder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnAddNewFolder.Name = "tsbtnAddNewFolder";
            this.tsbtnAddNewFolder.Size = new System.Drawing.Size(23, 22);
            this.tsbtnAddNewFolder.ToolTipText = "Add New Folder";
            // 
            // tsbtnAddNewFile
            // 
            this.tsbtnAddNewFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnAddNewFile.Image = global::TotalCommander.Properties.Resources.Add_File1;
            this.tsbtnAddNewFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnAddNewFile.Name = "tsbtnAddNewFile";
            this.tsbtnAddNewFile.Size = new System.Drawing.Size(23, 22);
            this.tsbtnAddNewFile.ToolTipText = "Add New Text Document";
            // 
            // splMain
            // 
            this.splMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splMain.Location = new System.Drawing.Point(0, 52);
            this.splMain.Name = "splMain";
            // 
            // splMain.Panel1
            // 
            this.splMain.Panel1.Controls.Add(this.shellBrowserLeft);
            // 
            // splMain.Panel2
            // 
            this.splMain.Panel2.Controls.Add(this.shellBrowserRight);
            this.splMain.Size = new System.Drawing.Size(1084, 584);
            this.splMain.SplitterDistance = 524;
            this.splMain.SplitterWidth = 3;
            this.splMain.TabIndex = 23;
            this.splMain.TabStop = false;
            // 
            // shellBrowserLeft
            // 
            this.shellBrowserLeft.AutoSize = true;
            this.shellBrowserLeft.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.shellBrowserLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.shellBrowserLeft.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.shellBrowserLeft.HideNavigationPane = false;
            this.shellBrowserLeft.Location = new System.Drawing.Point(0, 0);
            this.shellBrowserLeft.Name = "shellBrowserLeft";
            this.shellBrowserLeft.Size = new System.Drawing.Size(524, 584);
            this.shellBrowserLeft.TabIndex = 0;
            // 
            // shellBrowserRight
            // 
            this.shellBrowserRight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.shellBrowserRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.shellBrowserRight.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.shellBrowserRight.HideNavigationPane = false;
            this.shellBrowserRight.Location = new System.Drawing.Point(0, 0);
            this.shellBrowserRight.Name = "shellBrowserRight";
            this.shellBrowserRight.Size = new System.Drawing.Size(557, 584);
            this.shellBrowserRight.TabIndex = 0;
            // 
            // tlpnBottoms
            // 
            this.tlpnBottoms.ColumnCount = 7;
            this.tlpnBottoms.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tlpnBottoms.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tlpnBottoms.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tlpnBottoms.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tlpnBottoms.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tlpnBottoms.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tlpnBottoms.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tlpnBottoms.Controls.Add(this.btnExit, 6, 0);
            this.tlpnBottoms.Controls.Add(this.btnF8Delete, 5, 0);
            this.tlpnBottoms.Controls.Add(this.btnF7NewFolder, 4, 0);
            this.tlpnBottoms.Controls.Add(this.btnF6Move, 3, 0);
            this.tlpnBottoms.Controls.Add(this.btnF1Copy, 2, 0);
            this.tlpnBottoms.Controls.Add(this.btnF3View, 0, 0);
            this.tlpnBottoms.Controls.Add(this.btnF4Edit, 1, 0);
            this.tlpnBottoms.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tlpnBottoms.Location = new System.Drawing.Point(0, 637);
            this.tlpnBottoms.Margin = new System.Windows.Forms.Padding(0);
            this.tlpnBottoms.Name = "tlpnBottoms";
            this.tlpnBottoms.RowCount = 1;
            this.tlpnBottoms.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpnBottoms.Size = new System.Drawing.Size(1084, 25);
            this.tlpnBottoms.TabIndex = 24;
            // 
            // btnExit
            // 
            this.btnExit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExit.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(924, 0);
            this.btnExit.Margin = new System.Windows.Forms.Padding(0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(160, 25);
            this.btnExit.TabIndex = 6;
            this.btnExit.Text = "Ctrl+Q Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            // 
            // btnF8Delete
            // 
            this.btnF8Delete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnF8Delete.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnF8Delete.Location = new System.Drawing.Point(770, 0);
            this.btnF8Delete.Margin = new System.Windows.Forms.Padding(0);
            this.btnF8Delete.Name = "btnF8Delete";
            this.btnF8Delete.Size = new System.Drawing.Size(154, 25);
            this.btnF8Delete.TabIndex = 5;
            this.btnF8Delete.Text = "F8 Delete";
            this.btnF8Delete.UseVisualStyleBackColor = true;
            // 
            // btnF7NewFolder
            // 
            this.btnF7NewFolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnF7NewFolder.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnF7NewFolder.Location = new System.Drawing.Point(616, 0);
            this.btnF7NewFolder.Margin = new System.Windows.Forms.Padding(0);
            this.btnF7NewFolder.Name = "btnF7NewFolder";
            this.btnF7NewFolder.Size = new System.Drawing.Size(154, 25);
            this.btnF7NewFolder.TabIndex = 4;
            this.btnF7NewFolder.Text = "F7 New Folder";
            this.btnF7NewFolder.UseVisualStyleBackColor = true;
            // 
            // btnF6Move
            // 
            this.btnF6Move.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnF6Move.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnF6Move.Location = new System.Drawing.Point(462, 0);
            this.btnF6Move.Margin = new System.Windows.Forms.Padding(0);
            this.btnF6Move.Name = "btnF6Move";
            this.btnF6Move.Size = new System.Drawing.Size(154, 25);
            this.btnF6Move.TabIndex = 3;
            this.btnF6Move.Text = "F6 Move";
            this.btnF6Move.UseVisualStyleBackColor = true;
            // 
            // btnF1Copy
            // 
            this.btnF1Copy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnF1Copy.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnF1Copy.Location = new System.Drawing.Point(308, 0);
            this.btnF1Copy.Margin = new System.Windows.Forms.Padding(0);
            this.btnF1Copy.Name = "btnF1Copy";
            this.btnF1Copy.Size = new System.Drawing.Size(154, 25);
            this.btnF1Copy.TabIndex = 2;
            this.btnF1Copy.Text = "F1 Copy";
            this.btnF1Copy.UseVisualStyleBackColor = true;
            // 
            // btnF3View
            // 
            this.btnF3View.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnF3View.FlatAppearance.BorderSize = 0;
            this.btnF3View.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnF3View.Location = new System.Drawing.Point(0, 0);
            this.btnF3View.Margin = new System.Windows.Forms.Padding(0);
            this.btnF3View.Name = "btnF3View";
            this.btnF3View.Size = new System.Drawing.Size(154, 25);
            this.btnF3View.TabIndex = 1;
            this.btnF3View.Text = "F3 View";
            this.btnF3View.UseVisualStyleBackColor = true;
            // 
            // btnF4Edit
            // 
            this.btnF4Edit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnF4Edit.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnF4Edit.Location = new System.Drawing.Point(154, 0);
            this.btnF4Edit.Margin = new System.Windows.Forms.Padding(0);
            this.btnF4Edit.Name = "btnF4Edit";
            this.btnF4Edit.Size = new System.Drawing.Size(154, 25);
            this.btnF4Edit.TabIndex = 0;
            this.btnF4Edit.Text = "F4 Edit";
            this.btnF4Edit.UseVisualStyleBackColor = true;
            // 
            // Form_TotalCommander
            // 
            this.AllowDrop = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1084, 662);
            this.Controls.Add(this.tlpnBottoms);
            this.Controls.Add(this.splMain);
            this.Controls.Add(this.tsMain);
            this.Controls.Add(this.mnuMain);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.mnuMain;
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "Form_TotalCommander";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Total Commander";
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
            this.splMain.Panel1.ResumeLayout(false);
            this.splMain.Panel1.PerformLayout();
            this.splMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splMain)).EndInit();
            this.splMain.ResumeLayout(false);
            this.tlpnBottoms.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList smallImgList;
        private System.Windows.Forms.ToolStripMenuItem tsmiMark;
        private System.Windows.Forms.ToolStripMenuItem tsmiCommands;
        private System.Windows.Forms.ToolStripMenuItem tsmiNet;
        private System.Windows.Forms.ToolStripMenuItem tsmiShow;
        private System.Windows.Forms.ToolStripMenuItem tsmiConfiguration;
        private System.Windows.Forms.ToolStripMenuItem tsmiStart;
        private System.Windows.Forms.MenuStrip mnuMain;
        private System.Windows.Forms.ToolStripMenuItem tsmiFile;
        private System.Windows.Forms.ToolStripMenuItem tsmiViewProperties;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem tsmiExit;
        private System.Windows.Forms.ToolStripMenuItem tsmiHelp;
        private System.Windows.Forms.ToolStripMenuItem tsmiKeyboards;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem tsmiAbout;
        private System.Windows.Forms.ToolTip tipButtons;
        private System.Windows.Forms.ToolStripButton tsbtnRefreshWindows;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbtnGoBackward;
        private System.Windows.Forms.ToolStripButton tsbtnGoForward;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbtnPackFiles;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripButton tsbtnDetailViewMode;
        private System.Windows.Forms.ToolStripButton tsbtnListViewMode;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStrip tsMain;
        private System.Windows.Forms.ToolStripButton tsbtnGoParent;
        private System.Windows.Forms.SplitContainer splMain;
        private GUI.ShellBrowser shellBrowserLeft;
        private GUI.ShellBrowser shellBrowserRight;
        private System.Windows.Forms.ToolStripButton tsbtnTreeView;
        private System.Windows.Forms.ToolStripButton tsbtnAddNewFolder;
        private System.Windows.Forms.ToolStripButton tsbtnAddNewFile;
        private System.Windows.Forms.TableLayoutPanel tlpnBottoms;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnF8Delete;
        private System.Windows.Forms.Button btnF7NewFolder;
        private System.Windows.Forms.Button btnF6Move;
        private System.Windows.Forms.Button btnF1Copy;
        private System.Windows.Forms.Button btnF3View;
        private System.Windows.Forms.Button btnF4Edit;


    }
}

