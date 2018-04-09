using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace TotalCommander.GUI
{
    public partial class FormFindFiles : Form
    {
        public string Pattern { get; set; }

        public FormFindFiles()
        {
            InitializeComponent();
            Init();
        }

        public FormFindFiles(string directoryToFind, ImageList smallImageList)
        {
            InitializeComponent();
            lvwViewer.SmallImageList = smallImageList;
            txtPath.Text = directoryToFind;
            Init();
        }

        private void Init()
        {
            txtPattern.Focus();
            txtPath.KeyDown += txtPath_KeyDown;
            txtPattern.KeyDown += txtPattern_KeyDown;
            lblStatus.Visible = false;

            btnBrowse.Click += btnBrowse_Click;
            btnCancel.Click += btnCancel_Click;
            btnSearch.Click += btnSearch_Click;

            InitListView();
        }

        #region Listview

        void lvwViewer_Resize(object sender, EventArgs e)
        {
            lvwViewer.Columns[0].Width = lvwViewer.Width;
        }

        void InitListView()
        {
            lvwViewer.View = View.Details;
            lvwViewer.FullRowSelect = true;
            lvwViewer.Columns.Add("Name");
            lvwViewer.Columns[0].Width = lvwViewer.Width;
            lvwViewer.Resize += lvwViewer_Resize;
            lvwViewer.HideSelection = false;
            lvwViewer.LabelEdit = false;
            lvwViewer.KeyDown += lvwViewer_KeyDown;
            lvwViewer.MouseDoubleClick += lvwViewer_MouseDoubleClick;
        }

        void lvwViewer_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ListView fBrowser = (ListView)sender;
                ListViewHitTestInfo info = fBrowser.HitTest(e.X, e.Y);
                ListViewItem item = info.Item;
                if (item != null)
                {
                    string fullPath = item.Tag.ToString();
                    Process.Start(fullPath);
                }
            }
        }

        void lvwViewer_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Enter:
                    if (lvwViewer.FocusedItem != null)
                    {
                        string fullPath = lvwViewer.FocusedItem.Tag.ToString();
                        Process.Start(fullPath);
                    }
                    break;
            }
        }

        void AddKeyToImageList(string key, Icon icon)
        {
            if (!lvwViewer.SmallImageList.Images.ContainsKey(key))
            {
                lvwViewer.SmallImageList.Images.Add(key, icon);
            }
        }

        void UpdateResultView()
        {
            if (null == ArrDirsFound || null == ArrFilesFound)
                return;

            List<ListViewItem> lvwItems = new List<ListViewItem>();
            ListViewItem lvwItem = null;
            foreach (DirectoryInfo dir in ArrDirsFound)
            {
                string[] row = { dir.FullName };

                try
                {
                    // check for accessing power through exception
                    dir.GetAccessControl();
                    if (dir.Attributes.HasFlag(FileAttributes.Hidden))
                        lvwItem = new ListViewItem(row, "hidden_folder");
                    else
                        lvwItem = new ListViewItem(row, "FolderIcon");
                }
                catch (UnauthorizedAccessException)
                {
                    lvwItem = new ListViewItem(row, "locked_folder");
                }
                lvwItem.Tag = dir.FullName; // save fullpath to item

                lvwItems.Add(lvwItem);
            }

            foreach (FileInfo file in ArrFilesFound)
            {
                string[] row = { file.FullName };

                // Check to see if the image collection contains an image
                // for this extension, using the extension as a key.
                Icon icon = ShellIcon.GetIcon(file.FullName);
                string ext = file.Extension.Replace(".", "");
                if (icon != null)
                {
                    AddKeyToImageList(file.FullName, icon);
                    lvwItem = new ListViewItem(row, file.FullName);
                }
                else if (!String.IsNullOrEmpty(ext))
                {
                    //Icon iconExt = Icon.ExtractAssociatedIcon(file.FullName);
                    Icon iconExt = ShellIcon.GetSmallIconFromExtension(ext);
                    AddKeyToImageList(ext, iconExt);
                    lvwItem = new ListViewItem(row, ext);
                }
                else
                    lvwItem = new ListViewItem(row, "unknown"); // unknow file icon

                lvwItem.Tag = file.FullName;
                lvwItems.Add(lvwItem);
            }

            lvwViewer.Items.Clear();
            lvwViewer.BeginUpdate();
            lvwViewer.Items.AddRange(lvwItems.ToArray());
            lvwViewer.EndUpdate();
        }

        #endregion Listview

        #region Quick search when pressing enter key
        void txtPattern_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                btnSearch_Click(sender, null);
        }

        void txtPath_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                btnSearch_Click(sender, null);
        }
        #endregion Quick search when pressing enter key

        bool CanAccess(DirectoryInfo info)
        {
            bool canAccess = true;
            try
            {
                info.GetAccessControl();
            }
            catch (UnauthorizedAccessException) { canAccess = false; }
            return canAccess;
        }

        async void btnSearch_Click(object sender, EventArgs e)
        {
            DirectoryInfo info = new DirectoryInfo(txtPath.Text);

            if (!info.Exists)
            {
                MessageBox.Show(this, "Directory not exists.");
                return;
            }
            if (!CanAccess(info))
            {
                MessageBox.Show("Access denied on this directory.");
                return;
            }
            lblStatus.Visible = true;
            lblStatus.Text = "Searching ...";
            btnSearch.Enabled = false;
            this.Cursor = Cursors.WaitCursor;


            Pattern = txtPattern.Text;
            await DoSearch(info);
            btnSearch.Enabled = true;
            int number = ArrFilesFound.Length + ArrDirsFound.Length;
             lblStatus.Text=number > 0 ? string.Format("{0} files, {1} directories", ArrFilesFound.Length, ArrDirsFound.Length):
                 "No file or folder matches the pattern.";
            this.Cursor = Cursors.Default;
            UpdateResultView();
        }

        void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void btnBrowse_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                fbd.Description = "Choose directory to search!";
                fbd.ShowNewFolderButton = false;
                //fbd.RootFolder = txtPath.Text;
                fbd.SelectedPath = txtPath.Text;
                if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    txtPath.Text = fbd.SelectedPath;
                }
            }
        }

        async Task DoSearch(DirectoryInfo info)
        {
            SearchOption option = chkFindSubDirs.Checked ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            FileInfo[] fileResult =null;
            DirectoryInfo[] dirResult=null;
            await Task.Run(() =>
             {
                 fileResult = info.GetFiles(Pattern, option);
                 dirResult = info.GetDirectories(Pattern, option);
             });
            ArrDirsFound = dirResult.Where(d => !(d.Attributes.HasFlag(FileAttributes.Temporary) && d.Attributes.HasFlag(FileAttributes.System))).ToArray();
            ArrFilesFound = fileResult.Where(d => !(d.Attributes.HasFlag(FileAttributes.Temporary) && d.Attributes.HasFlag(FileAttributes.System))).ToArray();
        }

        #region Properties
        public DirectoryInfo[] ArrDirsFound { get; set; }
        public FileInfo[] ArrFilesFound { get; set; }
        #endregion Properties
    }
}
