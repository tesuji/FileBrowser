using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TotalCommander.GUI
{
    public partial class FormPacking : Form
    {
        string[] arrPaths;
        static readonly string Path7za = @"7za.exe";
        static readonly string SevenZipHash256 = "E6855553350FA6FB23E05839C7F3EF140DAD29D9A0E3495DE4D1B17A9FBF5CA4";
        static bool Is7zaExistAndTrusted = false;

        public static string GetSha256HexString(string fullPath)
        {
            string hexString;
            using (var fs = new FileStream(fullPath, FileMode.Open))
            using (BufferedStream bs = new BufferedStream(fs))
            {
                using (var sha256 = new System.Security.Cryptography.SHA256Managed())
                {
                    byte[] hash = sha256.ComputeHash(bs);
                    hexString = BitConverter.ToString(hash).Replace("-", String.Empty);
                }
            }
            return hexString;
        }

        public static bool Check7zaTrusted()
        {
            FileInfo info = new FileInfo(Path7za);
            if (info.Exists && !Is7zaExistAndTrusted)
            {
                string hash = GetSha256HexString(info.FullName);
                Is7zaExistAndTrusted = hash.Equals(SevenZipHash256);
            }
            return Is7zaExistAndTrusted;
        }

        public FormPacking()
        {
            if (File.Exists(Path7za))
                Check7zaTrusted();

            if (!Is7zaExistAndTrusted)
            {
                FatalError(this.FindForm(), "The packer is broken.");
                this.Close();
            }

            InitializeComponent();
            Init();
        }

        public FormPacking(IEnumerable<string> paths): this()
        {
            arrPaths = paths.ToArray();
            if (arrPaths == null) return;
            string directory = Path.GetDirectoryName(arrPaths[0]);
            lblSaveFilePath.Text = directory;

            string archiveName;
            if (arrPaths.Length == 1)
            {
                archiveName = Path.GetFileNameWithoutExtension(arrPaths[0]);
            }
            else
            {
                archiveName = Path.GetFileName(directory);
                if (String.IsNullOrEmpty(archiveName))
                    archiveName = Path.GetPathRoot(directory).Replace(@":\", "");
            }
            txtFileName.Text = archiveName + ".zip";
        }

        void Init()
        {
            this.StartPosition = FormStartPosition.CenterParent;
            this.ShowInTaskbar = false;

            txtPass1.UseSystemPasswordChar = txtPass2.UseSystemPasswordChar = true;
            btnOpenSaveDialog.Click += BtnOpenSaveDialog_Click;
            btnOK.Click += BtnOK_Click;
            btnCancel.Click += BtnCancel_Click;
            btnShowPassword.MouseDown += BtnShowPassword_MouseDown;
            btnShowPassword.MouseUp += BtnShowPassword_MouseUp;

            InitArchiveFormat();
            InitCompressionLevel(ArchiveFormat.p7z);
            InitUpdateMode();
            InitPathMode();
            InitEncryptionMethod(ArchiveFormat.zip);
        }

        #region Button events
        void BtnShowPassword_MouseUp(object sender, MouseEventArgs e)
        {
            txtPass1.UseSystemPasswordChar = txtPass2.UseSystemPasswordChar = true;
        }

        void BtnShowPassword_MouseDown(object sender, MouseEventArgs e)
        {
            txtPass1.UseSystemPasswordChar = txtPass2.UseSystemPasswordChar = false;
        }

        void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        async void BtnOK_Click(object sender, EventArgs e)
        {
            string archiveName = Path.Combine(lblSaveFilePath.Text, txtFileName.Text);
            if (String.IsNullOrWhiteSpace(archiveName)) return;

            string command = "a";

            StringBuilder sbSwitches = new StringBuilder("-y"); // assume yes on all commands
            #region Archive format
            sbSwitches.Append(" -t");
            var format = (ArchiveFormat)cboArchiveFormat.SelectedItem;
            switch (format)
            {
                case ArchiveFormat.p7z:
                    sbSwitches.Append("7z");
                    break;
                default:
                    sbSwitches.Append(format.ToString());
                    break;
            }
            #endregion Archive format

            #region Encryption
            bool useEncrypt = !String.IsNullOrEmpty(txtPass1.Text) && gbEncryption.Enabled;
            string password = "";
            if (useEncrypt)
            {
                password = txtPass1.Text;
                if (!password.Equals(txtPass2.Text))
                {
                    FatalError(this.FindForm(), "Password does not match.");
                    return;
                }
                sbSwitches.AppendFormat(" -p{0}", password);
            }
            #endregion Encryption

            /// Compression level
            var level = (CompressionLevel)((ComboBoxItem)cboCompressionLevel.SelectedItem).Value;
            sbSwitches.AppendFormat(" -mx{0}", (int)level);

            var updateMode = (UpdateMode)((ComboBoxItem)cboUpdateMode.SelectedItem).Value;
            #region Update mode
            switch (updateMode)
            {
                case UpdateMode.AddAndReplace:
                    break;
                case UpdateMode.UpdateAndAdd:
                    command = "u ";
                    break;
                case UpdateMode.FreshenExisting:
                    sbSwitches.Append(" -up1q1r0x1y2z1w2");
                    break;
                case UpdateMode.Synchronize:
                    sbSwitches.Append(" -up1q0r2x1y2z1w2");
                    break;
            }
            #endregion Update mode

            #region Path mode

            #endregion Path mode

            #region combobox
            if (chkSFX.Checked)
                sbSwitches.Append(" -spf");
            if (chkDeleteAfterCompression.Checked)
                sbSwitches.Append(" -sdel");
            #endregion combobox

            #region Run 7za.exe

            var fileLists = String.Concat(Path.GetTempFileName(), Guid.NewGuid().ToString(), ".txt");
            File.WriteAllLines(fileLists, arrPaths);

            var pInfo = new System.Diagnostics.ProcessStartInfo()
            {
                FileName = Path7za,
                Arguments = String.Format("{0} \"{1}\" @\"{2}\" {3}", command, archiveName, fileLists, sbSwitches.ToString()),
                WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden
            };
            System.Diagnostics.Process x = null;
            try
            {
                btnOK.Enabled = false;
                x = System.Diagnostics.Process.Start(pInfo);

                Task task = Task.Run(() => x.WaitForExit());
                await task;
                if (task.IsCompleted)
                {
                    switch (x.ExitCode)
                    {
                        case 0: break;
                        case 1: FatalError(this.FindForm(), @"Warning (Non fatal error(s)).
For example, one or more files were locked by some other application, so they were not compressed.",
                                                                                                       MessageBoxIcon.Information);
                            break;
                        case 2: FatalError(this.FindForm(), "Some errors occur"); break;
                        case 7: FatalError(this.FindForm(), "Arguments errors occur" + Environment.NewLine +
                           pInfo.Arguments );
                            break;
                        case 8: FatalError(this.FindForm(), "Not enough sufficient memory for operation"); break;
                        case 255: FatalError(this.FindForm(), "User stops the operation"); break;
                    }
                }
            }
            finally
            {
                if (null != x) x.Close();
                this.DialogResult = DialogResult.OK;
                File.Delete(fileLists);
            }

            #endregion Run 7za.exe
        }

        void BtnOpenSaveDialog_Click(object sender, EventArgs e)
        {
            string directory = Path.GetDirectoryName(arrPaths[0]);
            using (var ofd = new SaveFileDialog()
            {
                CheckPathExists = true,
                ValidateNames = true,
                InitialDirectory = directory,
                FileName = txtFileName.Text
            })
            {
                var dResult = ofd.ShowDialog(this.FindForm());
                if (dResult == DialogResult.OK)
                {
                    txtFileName.Text = Path.GetFileName(ofd.FileName);
                    lblSaveFilePath.Text = Path.GetDirectoryName(ofd.FileName);
                }
            }
        }
        #endregion Button events

        #region Init combobox contents
        void InitArchiveFormat()
        {
            cboArchiveFormat.DataSource = Enum.GetValues(typeof(ArchiveFormat));
            cboArchiveFormat.SelectionChangeCommitted += CboArchiveFormat_SelectionChangeCommitted;
        }

        void CboArchiveFormat_SelectionChangeCommitted(object sender, EventArgs e)
        {
            var format = (ArchiveFormat)cboArchiveFormat.SelectedItem;
            if (Enum.IsDefined(typeof(ArchiveFormat), format))
            {
                InitCompressionLevel(format);
                bool is7zipOrZip = format == ArchiveFormat.zip || format == ArchiveFormat.p7z;
                if (is7zipOrZip)
                {
                    InitEncryptionMethod(format);
                    gbEncryption.Enabled = true;
                }
                else
                {
                    gbEncryption.Enabled = false;
                }
                string name = txtFileName.Text;
                if (format == ArchiveFormat.p7z)
                {
                    name = Path.ChangeExtension(name, ".7z");
                }
                else
                {
                    name = Path.ChangeExtension(name, "." + format.ToString());
                }

                txtFileName.Text = name;
            }
        }

        void InitCompressionLevel(ArchiveFormat format)
        {
            cboCompressionLevel.Items.Clear();
            ComboBoxItem item = null;
            if (format == ArchiveFormat.p7z || format == ArchiveFormat.zip)
            {
                ComboBoxItem[] arrItems = new ComboBoxItem[6];

                #region Add items
                item = new ComboBoxItem()
                {
                    Text = CompressionLevel.Fast.ToString(),
                    Value = CompressionLevel.Fast
                };
                arrItems[0] = item;

                item = new ComboBoxItem()
                {
                    Text = CompressionLevel.Fastest.ToString(),
                    Value = CompressionLevel.Fastest
                };
                arrItems[1] = item;

                item = new ComboBoxItem()
                {
                    Text = CompressionLevel.Maximum.ToString(),
                    Value = CompressionLevel.Maximum
                };
                arrItems[2] = item;

                item = new ComboBoxItem()
                {
                    Text = CompressionLevel.Normal.ToString(),
                    Value = CompressionLevel.Normal
                };
                arrItems[3] = item;

                item = new ComboBoxItem()
                {
                    Text = CompressionLevel.Store.ToString(),
                    Value = CompressionLevel.Store
                };
                arrItems[4] = item;

                item = new ComboBoxItem()
                {
                    Text = CompressionLevel.Ultra.ToString(),
                    Value = CompressionLevel.Ultra
                };
                arrItems[5] = item;
                #endregion Add items

                cboCompressionLevel.BeginUpdate();
                cboCompressionLevel.Items.AddRange(arrItems);
                cboCompressionLevel.SelectedIndex = 3;
                cboCompressionLevel.EndUpdate();
            }
            else
            {
                item = new ComboBoxItem()
                {
                    Text = CompressionLevel.Store.ToString(),
                    Value = CompressionLevel.Store
                };
                cboCompressionLevel.Items.Add(item);
                cboCompressionLevel.SelectedIndex = 0;
            }
        }

        void InitUpdateMode()
        {
            ComboBoxItem[] cboItems = new ComboBoxItem[4];

            ComboBoxItem item = new ComboBoxItem()
            {
                Text = "Add and replace files",
                Value = UpdateMode.AddAndReplace
            };
            cboItems[0] = item;

            item = new ComboBoxItem()
            {
                Text = "Freshen Existing files",
                Value = UpdateMode.FreshenExisting
            };
            cboItems[1] = item;

            item = new ComboBoxItem()
            {
                Text = "Synchronize files",
                Value = UpdateMode.Synchronize
            };
            cboItems[2] = item;

            item = new ComboBoxItem()
            {
                Text = "Update and add files",
                Value = UpdateMode.UpdateAndAdd
            };
            cboItems[3] = item;

            cboUpdateMode.BeginUpdate();
            cboUpdateMode.Items.AddRange(cboItems);
            cboUpdateMode.EndUpdate();
            cboUpdateMode.SelectedIndex = 0;
        }

        void InitPathMode()
        {
            ComboBoxItem[] cboItems = new ComboBoxItem[3];

            ComboBoxItem item = new ComboBoxItem()
            {
                Text = "Relative pathnames",
                Value = PathMode.Relative
            };
            cboItems[0] = item;

            item = new ComboBoxItem()
            {
                Text = "Full pathnames",
                Value = PathMode.Full
            };
            cboItems[1] = item;

            item = new ComboBoxItem()
            {
                Text = "Absolute pathnames",
                Value = PathMode.Absolute
            };
            cboItems[2] = item;

            cboPathMode.BeginUpdate();
            cboPathMode.Items.AddRange(cboItems);
            cboPathMode.EndUpdate();
            cboPathMode.SelectedIndex = 0;
        }

        void InitEncryptionMethod(ArchiveFormat format)
        {
            cboEncryptMethod.Items.Clear();
            ComboBoxItem item = null;
            switch (format)
            {
                case ArchiveFormat.zip:
                    item = new ComboBoxItem()
                    {
                        Text = EncryptionMethod.ZipCrypto.ToString(),
                        Value = EncryptionMethod.ZipCrypto
                    };
                    cboEncryptMethod.Items.Add(item);
                    item = new ComboBoxItem()
                    {
                        Text = EncryptionMethod.AES256.ToString(),
                        Value = EncryptionMethod.AES256
                    };
                    cboEncryptMethod.Items.Add(item);
                    break;
                case ArchiveFormat.p7z:
                    item = new ComboBoxItem()
                    {
                        Text = EncryptionMethod.AES256.ToString(),
                        Value = EncryptionMethod.AES256
                    };
                    cboEncryptMethod.Items.Add(item);
                    break;
            }
            cboEncryptMethod.SelectedIndex = 0;
        }

        #endregion Init combobox contents

        public static void FatalError(IWin32Window handle, string text, MessageBoxIcon icon = MessageBoxIcon.Error)
        {
            string caption = "Total Commander";
            MessageBox.Show(handle, text, caption, MessageBoxButtons.OK, icon);
        }
    }

    #region Enum Structures
    enum ArchiveFormat { zip = 0, p7z, wim, tar }

    enum CompressionLevel { Normal = 5, Fastest = 1, Fast = 3, Store = 0, Maximum = 7, Ultra = 9 }

    enum UpdateMode { AddAndReplace = 0, UpdateAndAdd, FreshenExisting, Synchronize }

    enum PathMode { Relative = 0, Full, Absolute }

    enum EncryptionMethod { ZipCrypto = 0, AES256 }
    #endregion Enum Structures
}
