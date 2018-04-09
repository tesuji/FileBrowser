using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TotalCommander.GUI
{
    public partial class FormNewFolder : Form
    {
        public string NewName { get; set; }

        public FormNewFolder()
        {
            NewName = string.Empty;
            this.StartPosition = FormStartPosition.CenterParent;
            InitializeComponent();
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.ShowInTaskbar = false;
        }

        public void Init()
        {
            if (!String.IsNullOrWhiteSpace(NewName))
            {
                txtNewName.Text = NewName;
                txtNewName.SelectionStart = 0;
                txtNewName.SelectionLength = NewName.Length;
                NewName = string.Empty;
            }

            txtNewName.KeyDown += txtNewName_KeyDown;
            btnOK.Click += btnOK_Click;
            btnCancel.Click += btnCancel_Click;
        }

        void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void btnOK_Click(object sender, EventArgs e)
        {
            NewName = txtNewName.Text;
            if (String.IsNullOrEmpty(NewName))
            {
                this.Close();
            }
            else
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        void txtNewName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                btnOK_Click(sender, EventArgs.Empty);
            }
        }




    }
}
