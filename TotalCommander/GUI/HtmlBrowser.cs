using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TotalCommander.GUI
{
    public partial class HtmlBrowser : Form
    {
        string FullPath = string.Empty;

        public HtmlBrowser()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.StartPosition = FormStartPosition.CenterParent;
            btnOK.Click += (s, _) =>
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            };
        }

        public HtmlBrowser(string path)
            : this()
        {
            FullPath = path;
            wbDisplay.Url = new Uri(FullPath);
        }
    }
}
