using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TotalCommander
{
    public sealed class ShellInfoItem
    {
        public string FileName { get; private set; }
        public string Ext { get; private set; }
        public string SizeFile { get; private set; }
        public string Date { get; private set; }
        public string Attr { get; private set; }

        #region Constructors
        public ShellInfoItem(FileInfo info): this((FileSystemInfo)info) { }

        public ShellInfoItem(DirectoryInfo info) : this((FileSystemInfo)info) { }

        public ShellInfoItem(FileSystemInfo info)
        {
            FileName = info.Name;
            Date = GetFavoriteDateString(info.CreationTime);
            Attr = GetFileAttributesString(info.Attributes);
            if (info.Attributes.HasFlag(FileAttributes.Directory))
            {
                Ext = string.Empty;
                SizeFile = "<DIR>";
            }
            else
            {
                Ext = info.Extension;
                SizeFile = GetBytesReadable(((FileInfo)info).Length);
            }
        }
        #endregion Constructors

        public string[] ToArray()
        {
            string[] result = new string[] {
                FileName,
                Ext.Replace(".", "").ToLower(), /* No period */
                SizeFile,
                Date,
                Attr };
            return result;
        }

        #region Gets favorite results
        /// <summary>
        /// Returns the favorite time for an arbitrary file. 
        /// Example: 23/03/2017 21:11:11 to 23/03/2017 21:11
        /// </summary>
        internal static string GetFavoriteDateString(DateTime myDateTime)
        {
            return myDateTime.ToString("dd/MM/yyyy hh:mm");
        }
        /// <summary>
        /// Returns the human-readable file size for an arbitrary, 64-bit file size 
        /// The default format is "0.### XB", e.g. "4.2 KB" or "1.434 GB"
        /// </summary>
        internal static string GetBytesReadable(long i)
        {
            // Get absolute value
            long absolute_i = (i < 0 ? -i : i);
            // Determine the suffix and readable value
            string suffix;
            double readable;
            if (absolute_i >= 0x40000000) // Gigabyte
            {
                suffix = " GB";
                readable = (i >> 20);
            }
            else if (absolute_i >= 0x100000) // Megabyte
            {
                suffix = " MB";
                readable = (i >> 10);
            }
            else if (absolute_i >= 0x400) // Kilobyte
            {
                suffix = " KB";
                readable = i;
            }
            else
            {
                return i.ToString("0 B"); // Byte
            }
            // Divide by 1024 to get fractional value
            readable = readable / 1024;
            // Return formatted number with suffix
            return readable.ToString("0.##") + suffix;
        }

        /// <summary>
        /// Returns the short format string file attribute
        /// </summary>
        internal static string GetFileAttributesString(FileAttributes fa)
        {
            string attr = string.Empty;
            if (fa.HasFlag(FileAttributes.ReadOnly)) attr += "r";
            else attr += "-";
            if (fa.HasFlag(FileAttributes.Archive)) attr += "a";
            else attr += "-";
            if (fa.HasFlag(FileAttributes.Hidden)) attr += "h";
            else attr += "-";
            if (fa.HasFlag(FileAttributes.System)) attr += "s";
            else attr += "-";
            return attr;
        }

        #endregion Gets favorite results

    }
}
