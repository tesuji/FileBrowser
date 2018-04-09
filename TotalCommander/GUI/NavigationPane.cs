using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Linq;
using Microsoft.VisualBasic.FileIO;
//using Microsoft

namespace TotalCommander.GUI
{
    internal class NavigationPane : System.Windows.Forms.TreeView
    {
        #region DLL import

        /// <summary>
        /// Causes a window to use a different set of visual style information than its class normally uses.
        /// </summary>
        /// <param name="hWnd">Handle to the window whose visual style information is to be changed.</param>
        /// <param name="pszSubAppName">Pointer to a string that contains the application name 
        /// to use in place of the calling application's name. If this parameter is NULL, 
        /// the calling application's name is used.</param>
        /// <param name="pszSubIdList">Pointer to a string that contains a semicolon-separated 
        /// list of CLSID names to use in place of the actual list passed by the window's class. 
        /// If this parameter is NULL, the ID list from the calling class is used.</param>
        /// <returns>If this function succeeds, it returns S_OK. Otherwise, it returns an HRESULT error code.</returns>
        [DllImport("uxtheme.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        private extern static int SetWindowTheme(IntPtr hWnd, string pszSubAppName,
                                                string pszSubIdList);
        #endregion DLL import

        #region Overrided
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (this.SelectedNode.IsExpanded)
                    this.SelectedNode.Collapse();
                else
                    this.SelectedNode.Expand();
            }
            else if (e.KeyData == Keys.F5)
                RefreshContents();
            base.OnKeyDown(e);
        }
        #endregion

        internal void Init()
        {
            this.BeforeExpand += NativeTreeView_BeforeExpand;

            SetWindowTheme(this.Handle, "explorer", null);
            if (!this.ImageList.Images.ContainsKey("FolderIcon"))
            {
                this.ImageList.Images.Add("FolderIcon", ShellIcon.GetLargeFolderIcon());
                this.ImageList.Images.Add("LockFolder", ShellIcon.GetIconFromIndex("shell32.dll", 47));
                /// Detect icon base on Windows version
                var vs = Environment.OSVersion;
                switch (vs.Version.Major)
                {
                    /// Windows vista, 7, 8, 8.1
                    case 6:
                        this.ImageList.Images.Add("ComputerIcon", ShellIcon.GetIconFromIndex("shell32.dll", 15));
                        this.ImageList.Images.Add("LibrariesIcon", ShellIcon.GetIconFromIndex("imageres.dll", 202));
                        this.ImageList.Images.Add("FavoritesIcon", ShellIcon.GetIconFromIndex("imageres.dll", 203));
                        break;
                    /// Windows 10
                    case 10:
                        this.ImageList.Images.Add("ComputerIcon", ShellIcon.GetIconFromIndex("shell32.dll", 15));
                        this.ImageList.Images.Add("LibrariesIcon", ShellIcon.GetIconFromIndex("imageres.dll", 203));
                        this.ImageList.Images.Add("FavoritesIcon", ShellIcon.GetIconFromIndex("imageres.dll", 200));
                        break;
                    default:
                        throw new NotSupportedException("This windows version is not supported");
                }
            }

            this.HideSelection = false;
            // FullRowSelect is ignored if ShowLines is set to true.
            this.ShowLines = false;
            this.FullRowSelect = true;

            AddFavorites();
            AddLibraries();
            AddComputer();

            // change items height
            int nodeHeight = this.Nodes[0].Bounds.Height;
            this.ItemHeight = nodeHeight + 4;
        }

        #region Add to favorites folder
        private void AddDesktop(TreeNode parent)
        {
            string path = SpecialDirectories.Desktop;
            AddImageToList(this.ImageList, path, ShellIcon.GetIcon(path));
            AddNodeToParent(parent, "Desktop", path, path);
        }

        private void AddDownloads(TreeNode parent)
        {
            string userPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string downloadPath = Path.Combine(userPath, "Downloads");
            AddImageToList(this.ImageList, downloadPath, ShellIcon.GetIcon(downloadPath));
            AddNodeToParent(parent, "Downloads", downloadPath, downloadPath);
        }

        void AddFavorites()
        {
            TreeNode favoritesNode = new TreeNode();
            favoritesNode.Name = favoritesNode.Text = "Favorites";
            favoritesNode.SelectedImageKey = favoritesNode.ImageKey = "FavoritesIcon";
            favoritesNode.Tag = @"\\Favorites";
            favoritesNode.Expand();
            AddDesktop(favoritesNode);
            AddDownloads(favoritesNode);
            this.BeginUpdate();
            this.Nodes.Add(favoritesNode);
            this.EndUpdate();
        }
        #endregion Add to favorites folder

        #region Add to libraries
        private void AddDocuments(TreeNode parent)
        {
            string path = SpecialDirectories.MyDocuments;
            AddImageToList(this.ImageList, path, ShellIcon.GetIcon(path));
            AddNodeToParent(parent, "Documents", path, path);
        }

        private void AddPictures(TreeNode parent)
        {
            string path = SpecialDirectories.MyPictures;
            AddImageToList(this.ImageList, path, ShellIcon.GetIcon(path));
            AddNodeToParent(parent, "Pictures", path, path);
        }

        private void AddMusics(TreeNode parent)
        {
            string path = SpecialDirectories.MyMusic;
            AddImageToList(this.ImageList, path, ShellIcon.GetIcon(path));
            AddNodeToParent(parent, "Musics", path, path);
        }

        private void AddVideos(TreeNode parent)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
            AddImageToList(this.ImageList, path, ShellIcon.GetIcon(path));
            AddNodeToParent(parent, "Videos", path, path);
        }

        void AddLibraries()
        {
            TreeNode libsNode = new TreeNode();
            libsNode.Name = libsNode.Text = "Libraries";
            libsNode.SelectedImageKey = libsNode.ImageKey = "LibrariesIcon";
            libsNode.Tag = @"\\Libraries";
            libsNode.Expand();
            AddDocuments(libsNode);
            AddPictures(libsNode);
            AddMusics(libsNode);
            AddVideos(libsNode);
            this.BeginUpdate();
            this.Nodes.Add(libsNode);
            this.EndUpdate();
        }

        #endregion Add to libraries

        public static void AddImageToList(ImageList list, string key, System.Drawing.Icon icon)
        {
            if (!list.Images.ContainsKey(key))
            {
                list.Images.Add(key, icon);
            }
        }

        #region Add computer
        private void AddDrivesNode(TreeNode myComputerNode)
        {
            DriveInfo[] allDrives = System.IO.DriveInfo.GetDrives().Where(d => d.IsReady).ToArray();
            System.Drawing.Icon iconForDrive = null;
            foreach (DriveInfo drive in allDrives)
            {
                TreeNode driveNode = new TreeNode();
                driveNode.Name = drive.Name;
                string volumnName = String.IsNullOrEmpty(drive.VolumeLabel) ? GetDriveType(drive.DriveType) : drive.VolumeLabel;
                string name = String.Format("{0} ({1})", volumnName, drive.Name);
                driveNode.Text = name;
                iconForDrive = ShellIcon.GetIcon(drive.Name);
                AddImageToList(this.ImageList, drive.Name, iconForDrive);
                driveNode.ImageKey = drive.Name;
                driveNode.SelectedImageKey = drive.Name;
                driveNode.Tag = drive.Name;
                if (drive.IsReady)
                    driveNode.Nodes.Add(String.Empty);
                myComputerNode.Nodes.Add(driveNode);
            }
        }

        public static string GetDriveType(DriveType type)
        {
            string result = "";
            switch (type)
            {
                case DriveType.CDRom:
                    result = "CDRom Disk";
                    break;
                case DriveType.Fixed:
                    result = "Local Disk";
                    break;
                case DriveType.Network:
                    result = "Network Disk";
                    break;
                case DriveType.NoRootDirectory:
                    result = "Mounted Disk";
                    break;
                case DriveType.Removable:
                    result = "Removable Disk";
                    break;
                default:
                    result = type.ToString();
                    break;
            }
            return result;
        }

        private void AddComputer()
        {
            TreeNode computerNode = new TreeNode();
            computerNode.Name = computerNode.Text = "Computer";
            computerNode.SelectedImageKey = computerNode.ImageKey = "ComputerIcon";
            computerNode.Tag = @"\\Computer";
            computerNode.Expand();
            AddDrivesNode(computerNode);
            this.BeginUpdate();
            this.Nodes.Add(computerNode);
            this.EndUpdate();
        }
        #endregion Add computer

        internal void AddToDirectory(TreeNode parent)
        {
            DirectoryInfo dirs = new DirectoryInfo(parent.Tag.ToString());
            DirectoryInfo[] subDirs = dirs.GetDirectories().Where(x => !x.Attributes.HasFlag(FileAttributes.System)).ToArray();
            foreach (DirectoryInfo subDir in subDirs)
            {
                this.AddNodeToParent(parent, subDir.Name, "FolderIcon", subDir.FullName);
            }
        }

        public static bool BannedAttrExists(FileSystemInfo x)
        {
            return x.Attributes.HasFlag(FileAttributes.System) ||
                    x.Attributes.HasFlag(FileAttributes.Hidden) ||
                    x.Attributes.HasFlag(FileAttributes.Temporary);
        }

        void AddSubNodes(TreeNode parent)
        {
            DirectoryInfo dirs = new DirectoryInfo(parent.Tag.ToString());

            DirectoryInfo[] subDirs = dirs.GetDirectories().Where(x => !BannedAttrExists(x)).ToArray();

            foreach (DirectoryInfo subDir in subDirs)
            {
                TreeNode node = new TreeNode(subDir.Name);
                try
                {
                    //keep the directory's full path in the tag for use later
                    node.Name = subDir.Name;
                    node.Tag = subDir.FullName;
                    if (subDir.Attributes.HasFlag(FileAttributes.Hidden))
                    {
                        node.ImageKey = "hidden_folder";
                        node.SelectedImageKey = "hidden_folder";
                    }
                    else
                    {
                        node.ImageKey = "FolderIcon";
                        node.SelectedImageKey = "FolderIcon";
                    }

                    //if the directory has sub directories add the place holder
                    int dirCount = subDir.GetDirectories().Where(x => !BannedAttrExists(x)).Count();
                    if (dirCount > 0)
                    {
                        node.Nodes.Add(null, String.Empty, "FolderIcon", "FolderIcon");
                    }
                }
                catch (UnauthorizedAccessException)
                {
                    //display a locked folder icon
                    node.ImageKey = "LockFolder";
                    node.SelectedImageKey = "locked_folder";
                }
                finally
                {
                    this.BeginUpdate();
                    parent.Nodes.Add(node);
                    this.EndUpdate();
                }
            }
        }

        private void AddNodeToParent(TreeNode parent, string strName, string imageKey, string tag)
        {
            TreeNode node = new TreeNode();
            node.Name = strName;
            node.Text = strName;
            node.ImageKey = imageKey;
            node.SelectedImageKey = imageKey;
            node.Tag = tag;
            if (HasChildNode(node))
                node.Nodes.Add(null, String.Empty, "FolderIcon", "FolderIcon");
            parent.Nodes.Add(node);
        }

        internal bool HasChildNode(TreeNode node)
        {
            string pathParent = node.Tag.ToString();
            try
            {
                DirectoryInfo dir = new DirectoryInfo(pathParent);
                bool result = false;
                try
                {
                    int dirLenght = dir.GetDirectories().Where(d => !d.Attributes.HasFlag(FileAttributes.System)).Count();
                    if (dirLenght > 0)
                    {
                        result = true;
                    }
                }
                catch (UnauthorizedAccessException)
                {
                    result = false;
                }
                return result;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            return false;
        }

        static internal bool IsSpecialFolders(string path)
        {
            bool val = (path == @"\\Computer") || (path == @"\\Libraries") || (path == @"\\Favorites");
            return val;
        }

        internal void UpdateDisks()
        {
            TreeNode computerNode = this.Nodes[2];
            computerNode.Nodes.Clear();
            AddDrivesNode(computerNode);
        }

        internal void RefreshContents()
        {
            TreeNode currentNode = this.SelectedNode;
            currentNode.Nodes.Clear();
            string currentPath = currentNode.Tag.ToString();
            if (currentPath == @"\\Computer")
                AddDrivesNode(currentNode);
            else if (currentPath == @"\\Libraries")
            {
                AddDocuments(currentNode);
                AddPictures(currentNode);
                AddMusics(currentNode);
                AddVideos(currentNode);
            }
            else if (currentPath == @"\\Favorites")
            {
                AddDesktop(currentNode);
                AddDownloads(currentNode);
            }
            else
            {
                AddSubNodes(currentNode);
            }
            currentNode.Expand();
        }

        void NativeTreeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Nodes.Count > 0)
            {
                if (String.IsNullOrEmpty(e.Node.Nodes[0].Text) && e.Node.Nodes[0].Tag == null)
                {
                    e.Node.Nodes.Clear();
                    AddSubNodes(e.Node);
                }
            }
        }
    }
}
