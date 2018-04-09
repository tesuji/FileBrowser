using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TotalCommander.GUI
{
    public class DisksBrowser : ComboBox
    {
        public DisksBrowser()
        {
            this.DrawMode = DrawMode.OwnerDrawFixed;
            this.DrawItem += DrawDrivesImage;
            this.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        public void Init()
        {
            this.Items.Clear();
            AddDisks();
        }

        private void AddDisks()
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives().Where(d => d.IsReady).ToArray();
            ComboBoxItem[] arrCbi = new ComboBoxItem[allDrives.Length];
            for (int i = 0; i < allDrives.Length; i++)
            {
                arrCbi[i] = new ComboBoxItem();
                arrCbi[i].Text = allDrives[i].Name;
                arrCbi[i].Value = allDrives[i];
            }
            this.BeginUpdate();
            this.Items.AddRange(arrCbi);
            this.EndUpdate();
        }

        /// <summary>
        /// Updates disks when inserting new device or removing it.
        /// </summary>
        /// <returns>
        /// Returns a Boolean value indicates that the browsing disk is exists.
        /// </returns>
        public bool UpdateDisks()
        {
            string oldDisk = this.SelectedItem.ToString();
            this.Items.Clear();
            AddDisks();

            bool oldDiskExists = false;
            for (int i = 0; i < this.Items.Count; i++)
            {
                if (this.Items[i].ToString().Equals(oldDisk, StringComparison.OrdinalIgnoreCase))
                {
                    oldDiskExists = true;
                    this.SelectedIndex = i;
                }
            }
            return oldDiskExists;
        }

        private void DrawDrivesImage(object sender, DrawItemEventArgs e)
        {
            if (e.Index == -1)
                return;

            e.DrawBackground();
            string path = this.Items[e.Index].ToString();
            Icon icon = ShellIcon.GetIcon(path);
            e.Graphics.DrawIcon(icon, 3, e.Bounds.Top);
            e.Graphics.DrawString(path, this.Font, Brushes.Black, icon.Width + 2, e.Bounds.Top);
            e.DrawFocusRectangle();
        }

    }
}
