using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace TotalCommander
{
    /// <seealso cref="https://msdn.microsoft.com/en-us/library/windows/desktop/bb776886(v=vs.85).aspx"/>
    public static class PropertiesDialog
    {

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        static extern bool ShellExecuteEx(ref SHELLEXECUTEINFO lpExecInfo);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct SHELLEXECUTEINFO
        {
            public int cbSize;
            public uint fMask;
            public IntPtr hwnd;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string lpVerb;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string lpFile;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string lpParameters;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string lpDirectory;
            public int nShow;
            public IntPtr hInstApp;
            public IntPtr lpIDList;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string lpClass;
            public IntPtr hkeyClass;
            public uint dwHotKey;
            public IntPtr hIcon;
            public IntPtr hProcess;
        }
        private const int SW_SHOW = 5;
        private const uint SEE_MASK_INVOKEIDLIST = 12;

        #region Import Methods

        [DllImport("shell32.dll", SetLastError = true)]
        static extern int SHMultiFileProperties(System.Windows.Forms.IDataObject pdtobj, int flags);

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr ILCreateFromPath(string path);

        [DllImport("shell32.dll", CharSet = CharSet.None)]
        public static extern void ILFree(IntPtr pidl);

        [DllImport("shell32.dll", CharSet = CharSet.None)]
        public static extern int ILGetSize(IntPtr pidl);

        #endregion

        #region Static Methods

        #region Private

        private static MemoryStream CreateShellIDList(StringCollection filenames)
        {
            // first convert all files into pidls list
            int pos = 0;
            byte[][] pidls = new byte[filenames.Count][];
            foreach (var filename in filenames)
            {
                // Get pidl based on name
                IntPtr pidl = ILCreateFromPath(filename);
                int pidlSize = ILGetSize(pidl);
                // Copy over to our managed array
                pidls[pos] = new byte[pidlSize];
                Marshal.Copy(pidl, pidls[pos++], 0, pidlSize);
                ILFree(pidl);
            }

            // Determine where in CIDL we will start pumping PIDLs
            int pidlOffset = 4 * (filenames.Count + 2);
            // Start the CIDL stream
            var memStream = new MemoryStream();
            var sw = new BinaryWriter(memStream);
            // Initialize CIDL witha count of files
            sw.Write(filenames.Count);
            // Calcualte and write relative offsets of every pidl starting with root
            sw.Write(pidlOffset);
            pidlOffset += 4; // root is 4 bytes
            foreach (var pidl in pidls)
            {
                sw.Write(pidlOffset);
                pidlOffset += pidl.Length;
            }

            // Write the root pidl (0) followed by all pidls
            sw.Write(0);
            foreach (var pidl in pidls) sw.Write(pidl);
            // stream now contains the CIDL
            return memStream;
        }

        #endregion

        #region Public

        /// <summary>
        /// Opens a file properties dialog from windows explorer
        /// </summary>
        public static int Show(IEnumerable<string> Filenames)
        {
            StringCollection Files = new StringCollection();
            foreach (string s in Filenames) Files.Add(s);
            var data = new DataObject();
            data.SetFileDropList(Files);
            data.SetData("Preferred DropEffect", true, new MemoryStream(new byte[] { 5, 0, 0, 0 }));
            data.SetData("Shell IDList Array", true, CreateShellIDList(Files));
            return SHMultiFileProperties(data, 0);
        }

        /// <summary>
        /// Opens a file properties dialog from windows explorer
        /// </summary>
        public static int Show(string[] Filenames)
        {
            return Show(Filenames as IEnumerable<string>);
        }

        /// <summary>
        /// Opens a file properties dialog from windows explorer
        /// </summary>
        public static bool Show(string fileName)
        {
            SHELLEXECUTEINFO info = new SHELLEXECUTEINFO();
            info.cbSize = System.Runtime.InteropServices.Marshal.SizeOf(info);
            info.lpVerb = "properties";
            info.lpFile = fileName;
            info.nShow = SW_SHOW;
            info.fMask = SEE_MASK_INVOKEIDLIST;
            return ShellExecuteEx(ref info);
        }
        #endregion
        #endregion
    }
}

