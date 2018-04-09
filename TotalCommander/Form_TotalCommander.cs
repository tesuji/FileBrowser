using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace TotalCommander
{
    public partial class Form_TotalCommander : Form
    {
        #region Fields
        private GUI.ShellBrowser m_PreviousFocus;
        #endregion

        public Form_TotalCommander()
        {
            InitializeComponent();

            GUI.ShellBrowser.SmallImageList = smallImgList;

            shellBrowserLeft.Init();
            shellBrowserRight.Init();

            shellBrowserLeft.RecvFocus += Browser_GotFocus;
            shellBrowserRight.RecvFocus += Browser_GotFocus;

            m_PreviousFocus = shellBrowserLeft;
            Init();
        }

        void Browser_GotFocus(object sender, EventArgs e)
        {
            GUI.ShellBrowser tmp = sender as GUI.ShellBrowser;
            if (tmp != null)
            {
                m_PreviousFocus = tmp;
            }
        }

        private void Init()
        {
            InitMenuItems();
            InitBottomItems();
            InitToolbarBtns();
        }

        #region Bottom buttons
        void InitBottomItems()
        {
            this.KeyDown += Form_TotalCommander_KeyDown;
            btnF3View.Click += btnF3View_Click;
            btnF4Edit.Click += btnF4Edit_Click;
            btnF1Copy.Click += btnF1Copy_Click;
            btnF6Move.Click += btnF6Move_Click;
            btnF7NewFolder.Click += btnF7NewFolder_Click;
            btnF8Delete.Click += btnF8Delete_Click;
            btnExit.Click += btnExit_Click;
        }

        void Form_TotalCommander_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.F3:
                    btnF3View_Click(null, null);
                    break;
                case Keys.F4:
                    btnF4Edit_Click(null, null);
                    break;
                case Keys.F1:
                    btnF1Copy_Click(null, null);
                    break;
                case Keys.F6:
                    btnF6Move_Click(null, null);
                    break;
                case Keys.F7:
                    btnF7NewFolder_Click(null, null);
                    break;
                case Keys.F8:
                    btnF8Delete_Click(null, null);
                    break;
                case Keys.Control | Keys.Q:
                    btnExit_Click(null, null);
                    break;
            }
        }

        void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void btnF8Delete_Click(object sender, EventArgs e)
        {
            m_PreviousFocus.DeleteSelectedItems(Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin);
        }

        void btnF7NewFolder_Click(object sender, EventArgs e)
        {
            m_PreviousFocus.CreateNewFolder();
        }

        void btnF6Move_Click(object sender, EventArgs e)
        {
            m_PreviousFocus.CutSelectedItems();
        }

        void btnF1Copy_Click(object sender, EventArgs e)
        {
            m_PreviousFocus.CopySelectedItems();
        }

        void btnF4Edit_Click(object sender, EventArgs e)
        {
            m_PreviousFocus.EditWithNotepad();
        }

        void btnF3View_Click(object sender, EventArgs e)
        {
            m_PreviousFocus.EditWithNotepad();
        }
        #endregion Bottom buttons

        #region Menu items
        void InitMenuItems()
        {
            tsmiViewProperties.Click += tsmiViewProperties_Click;
            tsmiExit.Click += tsmiExit_Click;
            tsmiKeyboards.Click += tsmiKeyboards_Click;
            tsmiAbout.Click += tsmiAbout_Click;
        }

        void tsmiAbout_Click(object sender, EventArgs e)
        {
            string caption = "About Total Commander";
            string text = @"Total Commander Version 2.0.1 (2017-05-20)
15520599 - Tran Cao Phap";
            MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void tsmiKeyboards_Click(object sender, EventArgs e)
        {
            string helpFile = "Keyboards.htm";
            if (File.Exists(helpFile))
            {
                FileInfo info = new FileInfo(helpFile);
                GUI.HtmlBrowser htmlBrowser = new GUI.HtmlBrowser(info.FullName);
                htmlBrowser.Text = "Keyboard shortcuts";
                htmlBrowser.ShowDialog(this);
            }
            else
            {
                GUI.FormPacking.FatalError(this.FindForm(), "Keyboards.htm is missing.");
            }
        }

        void tsmiExit_Click(object sender, EventArgs e)
        {
            btnExit_Click(null, null);
        }

        void tsmiViewProperties_Click(object sender, EventArgs e)
        {
            m_PreviousFocus.OpenPropertiesWindowWithSelectedItems();
        }
        #endregion Menu items

        #region Toolstrip buttons
        void InitToolbarBtns()
        {
            tsbtnGoParent.Click += tsbtnGoParent_Click;
            tsbtnGoBackward.Click += tsbtnGoBackward_Click;
            tsbtnGoForward.Click += tsbtnGoForward_Click;
            tsbtnDetailViewMode.Click += tsbtnDetailViewMode_Click;
            tsbtnListViewMode.Click += tsbtnListViewMode_Click;
            tsbtnTreeView.Click += tsbtnTreeView_Click;
            tsbtnPackFiles.Click += tsbtnPackFiles_Click;
            tsbtnRefreshWindows.Click += tsbtnRefreshWindows_Click;
            tsbtnAddNewFile.Click += tsbtnAddNewFile_Click;
            tsbtnAddNewFolder.Click += tsbtnAddNewFolder_Click;
        }

        void tsbtnAddNewFolder_Click(object sender, EventArgs e)
        {
            m_PreviousFocus.CreateNewFolder();
        }

        void tsbtnAddNewFile_Click(object sender, EventArgs e)
        {
            m_PreviousFocus.CreateNewFile();
        }

        void tsbtnTreeView_Click(object sender, EventArgs e)
        {
            m_PreviousFocus.HideNavigationPane = !(m_PreviousFocus.HideNavigationPane);
        }

        void tsbtnPackFiles_Click(object sender, EventArgs e)
        {
            m_PreviousFocus.PackFiles();
        }

        void tsbtnRefreshWindows_Click(object sender, EventArgs e)
        {
            m_PreviousFocus.RefreshAll();
        }

        private void tsbtnDetailViewMode_Click(object sender, EventArgs e)
        {
            m_PreviousFocus.ChangeViewMode(View.Details);
        }

        private void tsbtnListViewMode_Click(object sender, EventArgs e)
        {
            m_PreviousFocus.ChangeViewMode(View.List);
        }

        #region Navigation Clicks

        private void tsbtnGoBackward_Click(object sender, EventArgs e)
        {
            m_PreviousFocus.GoBackward();
        }

        private void tsbtnGoForward_Click(object sender, EventArgs e)
        {
            m_PreviousFocus.GoForward();
        }

        private void tsbtnGoParent_Click(object sender, EventArgs e)
        {
            m_PreviousFocus.GoParent();
        }

        #endregion

        #endregion Toolstrip buttons

        #region Device detector constants and Structs

        const int WM_DEVICECHANGE = 0x219;
        const int DBT_DEVICEARRIVAL = 0x8000;
        const int DBT_DEVICEREMOVECOMPLETE = 0x8004;
        const int DBT_DEVTYP_VOLUME = 0x00000002;

        [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct DevBroadcastVolume
        {
            public int Size;
            public int DeviceType;
            public int Reserved;
            public int Mask;
            public Int16 Flags;
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            switch (m.Msg)
            {
                case WM_DEVICECHANGE:
                    switch ((int)m.WParam)
                    {
                        case DBT_DEVICEARRIVAL:
                        case DBT_DEVICEREMOVECOMPLETE:
                            shellBrowserLeft.OnDeviceDetected(null, null);
                            shellBrowserRight.OnDeviceDetected(null, null);
                            break;
                    }
                    break;
            }
        }

        #endregion Device detector constants and Structs

    }
}
