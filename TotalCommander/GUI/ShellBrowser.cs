using System;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using Microsoft.VisualBasic.FileIO;

namespace TotalCommander.GUI
{
    public partial class ShellBrowser : UserControl
    {
        #region Static fields
        private static readonly string OwnerOrUser = Environment.UserDomainName + "\\" + Environment.UserName;
        public static ImageList SmallImageList;
        #endregion

        #region Fields
        /// <summary>
        /// <see cref="ShellHistory"/>
        /// </summary>
        private ShellHistory m_History;
        private bool m_HideNavigationPane = false;
        private bool CanCut = false;
        private ListViewItem[] m_ListItemCache = null; //array to cache items for the virtual list
        private int m_FirstItem = 0; //stores the index of the first item in the cache
        List<FileSystemInfo> m_ShellItemInfo = new List<FileSystemInfo>();
        public string CurrentPath = Path.GetPathRoot(Environment.SystemDirectory);
        private SortOrder Order = SortOrder.None;
        private int SortColumn = 0;
        #endregion

        #region Event hanlder

        public event EventHandler RecvFocus;

        #endregion Event hanlder

        public ShellBrowser()
        {
            InitializeComponent();
        }

        public void Init()
        {
            /// firstly, assign imagelist to another control
            browser.SmallImageList = SmallImageList;
            navigationPane.ImageList = SmallImageList;

            InitNavigationPane();
            InitBrowser();
            InitHistoryAndPath();
            // open default path
            ProcessFileOrFolderPath(CurrentPath);
            InitDisksBrowser();
            InitTxtPath();
            InitPassingFocus();
        }

        void InitPassingFocus()
        {
            browser.GotFocus += WhenGotFocus;
            txtPath.GotFocus += WhenGotFocus;
            lblBotStatus.GotFocus += WhenGotFocus;
            lblTopStorageStatus.GotFocus += WhenGotFocus;
            navigationPane.GotFocus += WhenGotFocus;
            disksBrowser.GotFocus += WhenGotFocus;
        }

        void WhenGotFocus(object sender, EventArgs e)
        {
            RecvFocus(this, e);
        }

        private void InitHistoryAndPath()
        {
            m_History = new ShellHistory();
        }

        #region Refresh, View Mode, Hide/show directory view

        public bool HideNavigationPane
        {
            get { return m_HideNavigationPane; }
            set
            {
                if (m_HideNavigationPane == value) return;
                m_HideNavigationPane = value;
                if (m_HideNavigationPane)
                {
                    splMainView.Panel1Collapsed = true;
                    splMainView.Panel1.Hide();
                }
                else
                {
                    splMainView.Panel1Collapsed = false;
                    splMainView.Panel1.Show();
                }
            }
        }

        public void ChangeViewMode(View view)
        {
            browser.ChangeViewMode(view);
        }

        public void RefreshAll()
        {
            RefreshListView();
            OnDeviceDetected(null, EventArgs.Empty);
        }

        #endregion Refresh, View Mode

        #region Drive detector: INSERTING OR REMOVING

        public void OnDeviceDetected(object sender, EventArgs e)
        {
            navigationPane.UpdateDisks();
            if (!disksBrowser.UpdateDisks())
            {
                m_History.Clear();
                string path = Path.GetPathRoot(Environment.SystemDirectory);
                ProcessFileOrFolderPath(path);
                disksBrowser.SelectedIndex = 0;
                ComboBoxItem cbi = (ComboBoxItem)disksBrowser.SelectedItem;
                DriveInfo drive = (DriveInfo)cbi.Value;
                SetStorageStatus(drive);
            }
        }

        #endregion Drive detector

        #region Tree View as NavigationPane

        private void InitNavigationPane()
        {
            navigationPane.Init();
            navigationPane.KeyDown += TvwNavigationPane_KeyDown;
            navigationPane.NodeMouseDoubleClick += NavigationPane_NodeMouseDoubleClick;
        }

        void TvwNavigationPane_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                string path = navigationPane.SelectedNode.Tag.ToString();
                NavigationPane_Click(path);
            }
        }

        private void NavigationPane_Click(string path)
        {
            if (ProcessFolder(path))
            {
                m_History.Add(path);
                RefreshDisksBrowser(CurrentPath, disksBrowser.SelectedItem.ToString());
            }
            else if (Directory.Exists(path))
            {
                ProcessFolder(CurrentPath);
            }
        }

        void NavigationPane_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                string path = navigationPane.SelectedNode.Tag.ToString();
                if (string.IsNullOrWhiteSpace(path) || NavigationPane.IsSpecialFolders(path))
                    return;
                NavigationPane_Click(path);
            }
        }

        #endregion Tree View as NavigationPane

        #region Textbox presents Current Path
        private void InitTxtPath()
        {
            txtPath.KeyDown += TxtPath_KeyDown;
            txtPath.LostFocus += TxtPath_LostFocus;
        }

        void TxtPath_LostFocus(object sender, EventArgs e)
        {
            bool isSame = txtPath.Text.Equals(CurrentPath);
            if (!isSame)
                txtPath.Text = CurrentPath;
        }

        void TxtPath_KeyDown(object sender, KeyEventArgs e)
        {
            Keys ourKey = e.KeyData;
            switch (e.KeyData)
            {
                case Keys.Enter:
                    txtPath.Text = txtPath.Text.Trim();
                    if (txtPath.Text.Contains(Path.DirectorySeparatorChar) && ProcessFolder(txtPath.Text))
                        m_History.Add(txtPath.Text);
                    else
                        txtPath.Text = CurrentPath;
                    ComboBoxItem cbi = (ComboBoxItem)disksBrowser.SelectedItem;
                    string path = cbi.Text;
                    RefreshDisksBrowser(CurrentPath, path);
                    break;
                case Keys.Escape:
                    txtPath.Text = CurrentPath;
                    txtPath.SelectionStart = CurrentPath.Length;
                    break;
            }
        }
        #endregion Textbox presents Current Path

        #region File browser

        void InitBrowser()
        {
            browser.Init();
            browser.VirtualMode = true;
            browser.VirtualListSize = 0;

            #region Events

            browser.MouseDoubleClick += Browser_MouseDoubleClick;
            browser.MouseUp += Browser_MouseUp;
            browser.KeyDown += Browser_KeyDown;
            browser.AfterLabelEdit += Browser_AfterLabelEdit;
            browser.ColumnClick += Browser_ColumnClick;

            browser.DragEnter += Browser_DragEnter;
            browser.DragDrop += Browser_DragDrop;
            browser.ItemDrag += Browser_ItemDrag;

            browser.CacheVirtualItems += Browser_CacheVirtualItems;
            browser.RetrieveVirtualItem += Browser_RetrieveVirtualItem;
            browser.SearchForVirtualItem += Browser_SearchForVirtualItem;

            #endregion Events
        }

        private void Browser_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (Order == SortOrder.Ascending)
                    Order = SortOrder.Descending;
                else
                    Order = SortOrder.Ascending;
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                SortColumn = e.Column;
                Order = SortOrder.Ascending;
            }
            // set the sort arrow to a particular column
            browser.SetSortIcon(e.Column, Order);
            Comparison<FileSystemInfo> comparer = null;
            switch (e.Column)
            {
                case 0:
                    comparer = new Comparison<FileSystemInfo>(CompareFileName);
                    break;
                case 1:
                    comparer = new Comparison<FileSystemInfo>(CompareFileExtension);
                    break;
                case 2:
                    comparer = new Comparison<FileSystemInfo>(CompareFileSize);
                    break;
                case 3:
                    comparer = new Comparison<FileSystemInfo>(CompareFileCreationDate);
                    break;
                case 4:
                    comparer = new Comparison<FileSystemInfo>(CompareFileAttributes);
                    break;
                default:
                    goto case 0;
            }
            m_ShellItemInfo.Sort((a, b) =>
            {
                int ret = comparer(a, b);
                if (Order == SortOrder.Descending)
                    ret = -ret;
                return ret;
            });
            m_ListItemCache = null;
            browser.Refresh();
        }

        #region File comparer
        static int CompareFileName(FileSystemInfo a, FileSystemInfo b)
        {
            int result = 0;
            int flag = (a.Attributes.HasFlag(FileAttributes.Directory) ? 1 : 0) +
                (b.Attributes.HasFlag(FileAttributes.Directory) ? 2 : 0);
            switch (flag)
            {
                // Neither item is a folder => Compare names
                case 0: goto case 3; break;
                // A is a folder, but B isn't => A < B
                case 1: result = -1; break;
                // B is a folder, but A isn't => A > B
                case 2: result = 1; break;
                // Both items are folders => Compare names
                case 3: result = a.Name.CompareTo(b.Name); break;
                // Failsafe
                default:
                    result = 0;
                    break;
            }
            return result;
        }

        static int CompareFileSize(FileSystemInfo a, FileSystemInfo b)
        {
            int result = 0;
            int flag = (a.Attributes.HasFlag(FileAttributes.Directory) ? 1 : 0) +
                (b.Attributes.HasFlag(FileAttributes.Directory) ? 2 : 0);
            switch (flag)
            {
                // Neither item is a folder => Compare sizes
                case 0:
                    var x = (FileInfo)a;
                    var y = (FileInfo)b;
                    result = x.Length.CompareTo(y.Length);
                    break;
                // A is a folder, but B isn't => A < B
                case 1: result = -1; break;
                // B is a folder, but A isn't => A > B
                case 2: result = 1; break;
                // Both items are folders => Not compare
                case 3: result = 0; break;
                // Failsafe
                default:
                    result = 0;
                    break;
            }
            return result;
        }

        static int CompareFileCreationDate(FileSystemInfo a, FileSystemInfo b)
        {
            return a.CreationTime.CompareTo(b.CreationTime);
        }

        static int CompareFileExtension(FileSystemInfo a, FileSystemInfo b)
        {
            return a.Extension.CompareTo(b.Extension);
        }

        static int CompareFileAttributes(FileSystemInfo a, FileSystemInfo b)
        {
            var x = ShellInfoItem.GetFileAttributesString(a.Attributes);
            var y = ShellInfoItem.GetFileAttributesString(b.Attributes);
            return x.CompareTo(y);
        }
        #endregion

        #region Virtual listview hanlder functions
        private void Browser_SearchForVirtualItem(object sender, SearchForVirtualItemEventArgs e)
        {
            var y = m_ShellItemInfo.Skip(e.StartIndex).Where(x => x.Name.StartsWith(e.Text, StringComparison.OrdinalIgnoreCase)).ToArray();
            if (y.Length > 0)
            {
                e.Index = m_ShellItemInfo.IndexOf(y[0]);
            }
        }

        private void Browser_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            if (m_ListItemCache != null && e.ItemIndex >= m_FirstItem && e.ItemIndex < m_FirstItem + m_ListItemCache.Length)
            {
                e.Item = m_ListItemCache[e.ItemIndex - m_FirstItem];
            }
            else
            {
                e.Item = InitListviewItem(m_ShellItemInfo[e.ItemIndex]);
            }
        }

        private void Browser_CacheVirtualItems(object sender, CacheVirtualItemsEventArgs e)
        {
            if (m_ListItemCache != null && e.StartIndex >= m_FirstItem && e.EndIndex <= m_FirstItem + m_ListItemCache.Length)
                return;
            m_FirstItem = e.StartIndex;
            int end = e.EndIndex;
            m_ListItemCache = new ListViewItem[end - m_FirstItem + 1];
            int index = 0;
            for (int i = m_FirstItem; i <= end; i++)
            {
                m_ListItemCache[index++] = InitListviewItem(m_ShellItemInfo[i]);
            }
        }
        #endregion

        #region Other operations such as label edit, drag&drop, keydown
        /// <summary>
        /// Renames selected item by pressing F2 key
        /// </summary>
        void Browser_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            if (e.Label != null)
            {
                if (string.IsNullOrEmpty(e.Label))
                {
                    e.CancelEdit = true;
                    return;
                }
                string warningMsg = string.Empty;
                try
                {
                    ListViewItem item = browser.Items[e.Item];
                    string source = item.Tag.ToString();
                    FileInfo info = new FileInfo(source);
                    if (info.Attributes.HasFlag(FileAttributes.Directory))
                        FileSystem.RenameDirectory(source, e.Label);
                    else
                        FileSystem.RenameFile(source, e.Label);
                    string newFullName = Path.Combine(CurrentPath, e.Label);
                    item.Tag = newFullName;

                    #region Renames multiple items
                    if (browser.SelectedIndices.Count > 1)
                    {
                        foreach (int i in browser.SelectedIndices)
                        {
                            if (browser.Items[i] != item)
                            {
                                string target = GetDefaultDirectoryName(CurrentPath, e.Label);
                                source = browser.Items[i].Tag.ToString();
                                info = new FileInfo(source);
                                if (info.Attributes.HasFlag(FileAttributes.Directory))
                                    FileSystem.RenameDirectory(source, target);
                                else
                                    FileSystem.RenameFile(source, target);
                                newFullName = Path.Combine(CurrentPath, target);
                            }
                        }
                        RefreshListView();
                    }
                    #endregion Renames multiple items
                }
                catch (ArgumentException)
                {
                    warningMsg = "The path is not valid for one of the following reasons:" +
                    "it is a zero-length string; it contains only white space; it contains" +
                    @"invalid characters; or it is a device path (starts with \\.\).";
                }
                catch (FileNotFoundException)
                {
                    warningMsg = "This file or directory not exists.";
                }
                catch (PathTooLongException)
                {
                    warningMsg = "The file name exceeds system-defined maximum length.";
                }
                catch (IOException) { warningMsg = "The new file name already exists."; }
                catch (NotSupportedException) { warningMsg = "Invalid name"; }
                catch (UnauthorizedAccessException) { warningMsg = "You don't have enough permission on this file or folder."; }
                finally
                {
                    if (!string.IsNullOrEmpty(warningMsg))
                    {
                        e.CancelEdit = true;
                        MessageBox.Show(warningMsg, "Total Commander", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        void Browser_ItemDrag(object sender, ItemDragEventArgs e)
        {
            FileBrowser fBrowser = (FileBrowser)sender;
            List<string> pathList = new List<string>();
            foreach (int i in fBrowser.SelectedIndices)
            {
                pathList.Add(fBrowser.Items[i].Tag.ToString());
            }
            var items = pathList.ToArray();
            DataObject data = new DataObject(DataFormats.FileDrop, items);
            data.SetData(items);
            fBrowser.DoDragDrop(data, DragDropEffects.Copy);
        }

        async void Browser_DragDrop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop, false)) return;

            string[] dropItems = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            if (null == dropItems) return;

            bool isSameFolder = Path.GetDirectoryName(dropItems[0]).Equals(CurrentPath, StringComparison.OrdinalIgnoreCase);
            if (isSameFolder)
                return;

            List<Task> taskList = new List<Task>();

            foreach (string source in dropItems)
            {
                string target = Path.GetFileName(source);
                target = Path.Combine(CurrentPath, target);
                Task task = null;
                if (File.Exists(source))
                    task = Task.Run(() => FileSystem.CopyFile(source, target, UIOption.OnlyErrorDialogs, UICancelOption.ThrowException));
                else if (Directory.Exists(source))
                    task = Task.Run(() => FileSystem.CopyDirectory(source, target, UIOption.OnlyErrorDialogs, UICancelOption.ThrowException));
                taskList.Add(task);
            }

            foreach (Task task in taskList)
            {
                try
                {
                    await task;
                }
                catch (OperationCanceledException) { }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.StackTrace);
                }
                finally { RefreshListView(); }
            }


        }

        void Browser_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        void Browser_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                FileBrowser fBrowser = (FileBrowser)sender;
                ShellContextMenu menu = new ShellContextMenu();
                if (fBrowser.SelectedIndices.Count > 0)
                {
                    List<FileInfo> fiList = new List<FileInfo>();
                    foreach (int index in fBrowser.SelectedIndices)
                    {
                        FileInfo fInfo = new FileInfo(fBrowser.Items[index].Tag.ToString());
                        fiList.Add(fInfo);
                    }
                    menu.ShowContextMenu(fiList.ToArray(), fBrowser.PointToScreen(e.Location));
                }
                else
                {
                    DirectoryInfo dInfo = new DirectoryInfo(CurrentPath);
                    menu.ShowContextMenu(new DirectoryInfo[] { dInfo }, fBrowser.PointToScreen(e.Location));
                }
            }
        }

        void Browser_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Delete:
                    DeleteSelectedItems(RecycleOption.SendToRecycleBin);
                    break;
                case Keys.Shift | Keys.Delete:
                    DeleteSelectedItems(RecycleOption.DeletePermanently);
                    break;
                case Keys.F2:
                    if (browser.SelectedIndices.Count > 0)
                    {
                        browser.FocusedItem.BeginEdit();
                    }
                    break;
                case Keys.Control | Keys.F:
                    SearchItems();
                    break;
                case Keys.Control | Keys.C:
                    CopySelectedItems();
                    break;
                case Keys.Control | Keys.X:
                    CutSelectedItems();
                    break;
                case Keys.Control | Keys.V:
                    PasteFromClipboard();
                    break;
                case Keys.F5:
                    RefreshListView();
                    break;
                case Keys.Alt | Keys.Enter:
                    OpenPropertiesWindowWithSelectedItems();
                    break;
                case Keys.Alt | Keys.Up:
                    GoParent();
                    break;
                case Keys.Back:
                case Keys.Alt | Keys.Left:
                    GoBackward();
                    break;
                case Keys.Alt | Keys.Right:
                    GoForward();
                    break;
                case Keys.Enter:
                    if (browser.FocusedItem != null)
                    {
                        string path = browser.FocusedItem.Tag.ToString();
                        ProcessFileOrFolderPath(path);
                    }
                    break;
                case Keys.Control | Keys.Shift | Keys.N:
                    CreateNewFolder();
                    break;
                case Keys.Space:
                    if (browser.SelectedIndices.Count == 0)
                    {
                        browser.SelectedIndices.Add(0);
                        browser.EnsureVisible(0);
                    }
                    break;
            }
        }

        void Browser_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                FileBrowser fBrowser = (FileBrowser)sender;
                ListViewHitTestInfo info = fBrowser.HitTest(e.X, e.Y);
                ListViewItem item = info.Item;
                if (item != null)
                {
                    string fullPath = item.Tag.ToString();
                    ProcessFileOrFolderPath(fullPath);
                }
            }
        }
        #endregion

        #endregion File browser

        #region Disks Browser

        void InitDisksBrowser()
        {
            disksBrowser.Init();
            if (disksBrowser.Items.Count > 0)
            {
                disksBrowser.SelectedIndex = 0;
                ComboBoxItem cbi = (ComboBoxItem)disksBrowser.SelectedItem;
                DriveInfo drive = (DriveInfo)cbi.Value;
                SetStorageStatus(drive);
            }

            disksBrowser.SelectionChangeCommitted += DisksBrowser_SelectionChangeCommitted;
        }

        void SetStorageStatus(DriveInfo drive)
        {
            string volumnName = string.IsNullOrEmpty(drive.VolumeLabel) ? NavigationPane.GetDriveType(drive.DriveType) : drive.VolumeLabel;
            lblTopStorageStatus.Text = String.Format(
                "[{0}] {1} free in {2}",
                volumnName,
                ShellInfoItem.GetBytesReadable(drive.TotalFreeSpace),
                ShellInfoItem.GetBytesReadable(drive.TotalSize));
        }

        void DisksBrowser_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (disksBrowser.Items.Count > 0)
            {
                ComboBoxItem cbi = (ComboBoxItem)disksBrowser.SelectedItem;
                DriveInfo drive = (DriveInfo)cbi.Value;
                SetStorageStatus(drive);
                ProcessFileOrFolderPath(drive.Name);
            }
        }

        #endregion Disks Browser

        #region Edit actions with Microsoft.VisualBasic.FileIO class

        public void CopySelectedItems()
        {
            if (browser.SelectedIndices.Count > 0)
            {
                string fileName = string.Empty;
                var pathCollection = new System.Collections.Specialized.StringCollection();
                foreach (int i in browser.SelectedIndices)
                {
                    fileName = browser.Items[i].Tag.ToString();
                    pathCollection.Add(fileName);
                }
                if (pathCollection.Count > 0)
                {
                    CanCut = false;
                    Clipboard.SetFileDropList(pathCollection);
                }
            }
        }

        delegate void DeleteFileDel(string file, UIOption showUI, RecycleOption recycle, UICancelOption onUserCancel);
        public void DeleteSelectedItems(RecycleOption recycle)
        {
            if (browser.SelectedIndices.Count > 0)
            {
                var selectedIndices = browser.SelectedIndices;
                string fileName = string.Empty;
                #region Delete one item
                if (selectedIndices.Count == 1)
                {
                    fileName = browser.Items[selectedIndices[0]].Tag.ToString();
                    DeleteFileDel deleteFunc = null;
                    if (File.Exists(fileName))
                        deleteFunc = FileSystem.DeleteFile;
                    else/* if (Directory.Exists(fileName))*/
                        deleteFunc = FileSystem.DeleteDirectory;

                    try
                    {
                        deleteFunc(fileName, UIOption.AllDialogs, recycle, UICancelOption.ThrowException);
                    }
                    catch (OperationCanceledException) { }
                    catch (Exception ex) { MessageBox.Show(ex.StackTrace); }
                    finally
                    {
                        RefreshListView();
                    }
                }
                #endregion Delete one item
                #region Delete multiple items
                else
                {
                    string text = "Are you sure you want to ";
                    if (recycle == RecycleOption.DeletePermanently)
                        text += "permanently ";
                    text += "delete these " + selectedIndices.Count + " items?";
                    string caption = "Delete Multiple Items";
                    DialogResult dr = MessageBox.Show(this, text, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dr == DialogResult.Yes)
                    {
                        DeleteFileDel deleteFunc = null;
                        List<Tuple<string, DeleteFileDel>> list = new List<Tuple<string, DeleteFileDel>>();
                        foreach (int i in selectedIndices)
                        {
                            fileName = browser.Items[i].Tag.ToString();
                            if (File.Exists(fileName))
                                deleteFunc = FileSystem.DeleteFile;
                            else if (Directory.Exists(fileName))
                                deleteFunc = FileSystem.DeleteDirectory;
                            list.Add(Tuple.Create(fileName, deleteFunc));
                        }
                        foreach (var item in list)
                        {
                            fileName = item.Item1;
                            deleteFunc = item.Item2;
                            try
                            {
                                deleteFunc(fileName, UIOption.OnlyErrorDialogs, recycle, UICancelOption.ThrowException);
                            }
                            catch (OperationCanceledException) { }
                            catch (Exception ex) { MessageBox.Show(ex.Message); }
                            finally
                            {
                                RefreshListView();
                            }
                        }

                    }
                }
                #endregion Delete multiple items

            }
        }

        public void CutSelectedItems()
        {
            CopySelectedItems();
            CanCut = true;
        }

        #region Delegates
        private delegate void PasteFileDel(string source, string dest, UIOption uiOption, UICancelOption cancelOption);
        private delegate void PasteDirDel(string source, string dest, UIOption uiOption, UICancelOption cancelOption);
        #endregion Delegates
        private async void PasteFromClipboard()
        {
            if (!Clipboard.ContainsFileDropList())
                return;

            System.Collections.Specialized.StringCollection fileList = Clipboard.GetFileDropList();
            PasteFileDel PasteFile;
            PasteDirDel PasteDir;

            if (CanCut)
            {
                PasteFile = FileSystem.MoveFile;
                PasteDir = FileSystem.MoveDirectory;
            }
            else
            {
                PasteFile = FileSystem.CopyFile;
                PasteDir = FileSystem.CopyDirectory;
            }

            List<Task> taskList = new List<Task>();
            foreach (string source in fileList)
            {
                string target = Path.GetFileName(source);
                target = Path.Combine(CurrentPath, target);
                Task task = null;
                if (File.Exists(source))
                    task = Task.Run(() => PasteFile(source, target, UIOption.OnlyErrorDialogs, UICancelOption.ThrowException));
                else/* if (Directory.Exists(source))*/
                    task = Task.Run(() => PasteDir(source, target, UIOption.OnlyErrorDialogs, UICancelOption.ThrowException));
                taskList.Add(task);
            }

            foreach (Task task in taskList)
            {
                try
                {
                    await task;
                }
                catch (OperationCanceledException) { }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.StackTrace);
                }
                finally { RefreshListView(); }
            }

            CanCut = false;
        }

        private void RefreshListView()
        {
            ProcessFolder(CurrentPath);
        }

        private void RefreshTreeView()
        {
            navigationPane.Refresh();
        }

        /// <summary>
        /// Open properties window of selected items
        /// </summary>
        public void OpenPropertiesWindowWithSelectedItems()
        {
            var selectedIndices = browser.SelectedIndices;
            if (selectedIndices.Count > 0)
            {
                if (selectedIndices.Count == 1)
                    PropertiesDialog.Show(browser.Items[selectedIndices[0]].Tag.ToString());
                // multiple items
                else
                {
                    List<string> paths = new List<string>(); ;
                    foreach (int i in selectedIndices)
                    {
                        paths.Add(browser.Items[i].Tag.ToString());
                    }
                    PropertiesDialog.Show(paths.ToArray());
                }
            }
        }

        public void PackFiles()
        {
            var selectedIndices = browser.SelectedIndices;
            if (selectedIndices.Count > 0)
            {
                string[] arrPaths = new string[selectedIndices.Count];
                int count = 0;
                foreach (int item in selectedIndices)
                {
                    arrPaths[count++] = browser.Items[item].Tag.ToString();
                }
                using (var frmPacking = new FormPacking(arrPaths))
                {
                    if (!frmPacking.IsDisposed)
                    {
                        var result = frmPacking.ShowDialog(this.FindForm());
                        if (result == DialogResult.OK)
                            RefreshListView();
                    }
                }
            }
        }

        static string NotepadLocation = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.Windows), "notepad.exe");
        /// <summary>
        /// Opens notepad to edit selected items
        /// </summary>
        public void EditWithNotepad()
        {
            var selectedIndices = browser.SelectedIndices;
            if (selectedIndices.Count > 0)
            {
                foreach (int i in selectedIndices)
                {
                    string path = browser.Items[i].Tag.ToString();
                    if (File.Exists(path))
                    {
                        Process.Start(NotepadLocation, path);
                    }
                }
            }
        }

        #region Searchs files and folders with pattern
        void SearchItems()
        {
            FormFindFiles frmFind = new FormFindFiles(CurrentPath, browser.SmallImageList);
            frmFind.ShowDialog(this.FindForm());
        }
        #endregion Searchs files and folders with pattern

        #region Create New Folder and New File

        public void CreateNewFile()
        {
            string name = GetDefaultDirectoryName(CurrentPath, "New Text Document");
            string filter = @"All file (*.*) | *.*";
            var dialog = new SaveFileDialog()
            {
                FileName = name,
                DefaultExt = ".txt",
                InitialDirectory = CurrentPath,
                ValidateNames = true,
                Filter = filter
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var s = File.Create(dialog.FileName);
                    s.Close();
                }
                catch (NotSupportedException)
                {
                    MessageBox.Show("Invalid file name");
                }
                catch (PathTooLongException)
                {
                    MessageBox.Show("The file name is too long");
                }
                catch (IOException)
                {
                    MessageBox.Show("The parent directory of the file to be created is read-only");
                }
                catch (UnauthorizedAccessException)
                {
                    MessageBox.Show("Cannot create file in this folder");
                }
                finally { RefreshListView(); }
            }
        }

        /// <summary>
        /// Creates new folder in current path
        /// </summary>
        public void CreateNewFolder()
        {
            string name = GetDefaultDirectoryName(CurrentPath);

            FormNewFolder frmNewfolder = new FormNewFolder() { NewName = name };
            frmNewfolder.Init();
            frmNewfolder.ShowDialog(this.FindForm());

            if (frmNewfolder.DialogResult == DialogResult.OK)
            {
                name = frmNewfolder.NewName;
                string fullPath = Path.Combine(CurrentPath, name);
                bool exists = Directory.Exists(fullPath);
                try
                {
                    if (!exists)
                    {
                        Directory.CreateDirectory(fullPath);
                        RefreshListView();
                    }
                    else
                    {
                        MessageBox.Show("Directory already exists");
                    }
                }
                catch (NotSupportedException)
                {
                    MessageBox.Show("Invalid directory name");
                }
                catch (PathTooLongException)
                {
                    MessageBox.Show("The directory name is too long");
                }
                catch (IOException)
                {
                    MessageBox.Show("The parent directory of the directory to be created is read-only");
                }
                catch (UnauthorizedAccessException)
                {
                    MessageBox.Show("Cannot create directory in this folder");
                }
            }
        }

        static string GetDefaultDirectoryName(string currentPath, string newName = "New Folder")
        {
            string fullname = Path.Combine(currentPath, newName);
            int suffix = 0;
            while (Directory.Exists(fullname) || File.Exists(fullname))
            {
                fullname = string.Format("{0}\\{1} ({2})",
                    currentPath, newName, ++suffix);
            }
            return Path.GetFileName(fullname);
        }

        #endregion CreateNewFolder

        #endregion Edit actions with Microsoft.VisualBasic.FileIO class

        #region Process Files or Folders

        public static DirectoryInfo[] GetSubDirectories(DirectoryInfo path)
        {
            DirectoryInfo[] subDirs = null;
            try
            {
                SetAccessFolderRule(path.FullName);
                subDirs = path.GetDirectories().Where(x => !NavigationPane.BannedAttrExists(x)).ToArray();
            }
            catch (System.UnauthorizedAccessException)
            {
                return null;
            }
            return subDirs;
        }

        public static FileInfo[] GetSubFiles(DirectoryInfo path)
        {
            FileInfo[] subFiles = null;
            try
            {
                SetAccessFolderRule(path.FullName);
                subFiles = path.GetFiles().Where(x => !NavigationPane.BannedAttrExists(x)).ToArray();
            }
            catch (System.UnauthorizedAccessException)
            {
                MessageBox.Show("Access denied on file" + Environment.NewLine + path.FullName,
                    "Total Commander", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            return subFiles;
        }

        /// <summary>
        /// Process folder path, and gets result indicating that it is a folder,
        /// and we have permissions on this folder
        /// </summary>
        private bool ProcessFolder(string path)
        {
            if (Directory.Exists(path))
            {
                if (Navigate(path))
                {
                    txtPath.Text = CurrentPath;

                    DirectoryInfo dir = new DirectoryInfo(path);
                    lblBotStatus.Text = String.Format("{0} files, {1} directories",
                        GetSubFiles(dir).Length, GetSubDirectories(dir).Length);
                    return true;
                }
            }
            return false;
        }

        private void ProcessFileOrFolderPath(string path)
        {
            if (ProcessFolder(path))
            {
                m_History.Add(path);
            }
            else if (File.Exists(path))
            {
                Process.Start(path);
            }
        }

        #endregion Process Files or Folders

        #region Navigations

        /// <summary>
        /// Do not check if path is exists
        /// </summary>
        /// <returns>Returns values indicated that we have opened the path</returns>
        private bool Navigate(string path)
        {
            var currentDir = new DirectoryInfo(path);
            var subDirs = GetSubDirectories(currentDir);
            if (null == subDirs)
            {
                MessageBox.Show("Access denied on folder" + Environment.NewLine + currentDir.FullName,
                    "Total Commander", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            CurrentPath = path;
            m_ListItemCache = null;
            m_ShellItemInfo.Clear();
            var subFiles = GetSubFiles(currentDir);
            if (subDirs.Length > 0)
                m_ShellItemInfo.AddRange(subDirs);
            if (subFiles.Length > 0)
                m_ShellItemInfo.AddRange(subFiles);
            browser.VirtualListSize = 0;
            browser.Invalidate();
            browser.VirtualListSize = m_ShellItemInfo.Count;
            //UpdateFileBrowserAsync(subFiles, subDirs);
            return true;
        }

        private static ListViewItem InitListviewItem(FileSystemInfo info)
        {
            var list = SmallImageList;
            var item = new ShellInfoItem(info);
            string[] row = item.ToArray();
            int key = list.Images.IndexOfKey("unknown");
            if (info.Attributes.HasFlag(FileAttributes.Directory))
            {
                var dir = (DirectoryInfo)info;
                try
                {
                    dir.GetAccessControl();
                    if (dir.Attributes.HasFlag(FileAttributes.Hidden))
                        key = list.Images.IndexOfKey("hidden_folder");
                    else
                        key = list.Images.IndexOfKey("FolderIcon");
                }
                catch (UnauthorizedAccessException)
                {
                    //key = list.Images.IndexOfKey("locked_folder");
                    key = list.Images.IndexOfKey("LockFolder");
                }
            }
            else
            {
                var icon = ShellIcon.GetIcon(info.FullName);
                string ext = item.Ext;
                if (icon != null)
                {
                    AddKeyToImageList(info.FullName, icon);
                    key = list.Images.IndexOfKey(info.FullName);
                }
                else if (!String.IsNullOrEmpty(ext))
                {
                    icon = ShellIcon.GetSmallIconFromExtension(ext);
                    AddKeyToImageList(ext, icon);
                    key = list.Images.IndexOfKey(ext);
                }
            }
            var lvi = new ListViewItem(row, key) { Tag = info.FullName };
            return lvi;
        }

        static void AddKeyToImageList(string key, Icon icon)
        {
            if (!SmallImageList.Images.ContainsKey(key))
            {
                SmallImageList.Images.Add(key, icon);
            }
        }

        /// <summary>
        /// Syncs file browser and disk browser
        /// </summary>
        private void RefreshDisksBrowser(string path, string oldPath)
        {
            if (String.IsNullOrEmpty(path) || String.IsNullOrEmpty(oldPath))
                return;
            string pathroot = Path.GetPathRoot(path);
            if (!pathroot.Equals(Path.GetPathRoot(oldPath), StringComparison.OrdinalIgnoreCase))
            {
                DriveInfo[] allDrives = DriveInfo.GetDrives();
                int len = allDrives.Length;
                for (int i = 0; i < len; i++)
                {
                    if (allDrives[i].Name.Equals(pathroot, StringComparison.OrdinalIgnoreCase))
                    {
                        disksBrowser.SelectedIndex = i;
                        SetStorageStatus(allDrives[i]);
                        break;
                    }
                }
            }
        }

        public void GoBackward()
        {
            if (m_History.CanNavigateBack)
            {
                string path = m_History.MoveBackward();
                string oldPath = CurrentPath;
                RefreshDisksBrowser(path, CurrentPath);
                ProcessFolder(path);
                int index = FindItemWithPath(oldPath);
                if (index != -1)
                {
                    browser.SelectedIndices.Add(index);
                    browser.EnsureVisible(index);
                }
            }
        }

        public void GoForward()
        {
            if (m_History.CanNavigateForward)
            {
                string path = m_History.MoveForward();
                RefreshDisksBrowser(path, CurrentPath);
                ProcessFolder(path);
            }
        }

        public void GoParent()
        {
            string upPath = Path.GetDirectoryName(CurrentPath);
            if (String.IsNullOrEmpty(upPath))
                return;
            string oldPath = CurrentPath;
            ProcessFileOrFolderPath(upPath);
            int index = FindItemWithPath(oldPath);
            // maybe we go to the banned folder
            if (index != -1)
            {
                browser.SelectedIndices.Add(index);
                browser.EnsureVisible(index);
            }
        }

        private int FindItemWithPath(string tag)
        {
            var y = m_ShellItemInfo.Where(x => x.FullName.Equals(tag, StringComparison.OrdinalIgnoreCase)).ToArray();
            if (y.Length > 0)
            {
                return m_ShellItemInfo.IndexOf(y[0]);
            }
            return -1;
        }

        #endregion Navigations

        #region Check & Set folders permission
        private static bool CanAccessFolder(string folderPath)
        {
            try
            {
                // Attempt to get a list of security permissions from the folder.
                // This will raise an exception if the path is read only or do not have access to view the permissions.
                System.Security.AccessControl.DirectorySecurity ds = Directory.GetAccessControl(folderPath);
                return true;
            }
            catch (UnauthorizedAccessException)
            {
                return false;
            }
        }

        public static void SetAccessFolderRule(string directoryPath)
        {
            System.Security.AccessControl.DirectorySecurity sec = System.IO.Directory.GetAccessControl(directoryPath);
            //string owner = sec.GetOwner(typeof(System.Security.Principal.NTAccount)).ToString();
            System.Security.AccessControl.FileSystemAccessRule accRule =
                new System.Security.AccessControl.FileSystemAccessRule(
                    OwnerOrUser,
                    System.Security.AccessControl.FileSystemRights.FullControl,
                    System.Security.AccessControl.AccessControlType.Allow);
            sec.AddAccessRule(accRule);
        }
        #endregion Check & Set folders permission

    }
}
