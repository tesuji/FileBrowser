using System;
using System.Windows.Forms;

namespace TotalCommander.GUI
{
    internal class FileBrowser : ListView
    {
        #region Fields
        //private ListViewColumnSorter m_ColumnSorter = new ListViewColumnSorter();
        #endregion Fields

        #region Overrided functions

        protected override bool IsInputKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Right:
                case Keys.Left:
                case Keys.Up:
                case Keys.Down:
                    return true;
            }
            return base.IsInputKey(keyData);
        }

        #region Cannot use ListViewItemSorter in virtual mode
        //protected override void OnColumnClick(ColumnClickEventArgs e)
        //{
        //    // Determine if clicked column is already the column that is being sorted.
        //    if (e.Column == SortColumn)
        //    {
        //        // Reverse the current sort direction for this column.
        //        if (Order == SortOrder.Ascending)
        //            Order = SortOrder.Descending;
        //        else
        //            Order = SortOrder.Ascending;
        //    }
        //    else
        //    {
        //        // Set the column number that is to be sorted; default to ascending.
        //        SortColumn = e.Column;
        //        Order = SortOrder.Ascending;
        //    }
        //    // set the sort arrow to a particular column
        //    this.SetSortIcon(e.Column, Order);
        //    // Perform the sort with these new sort options.
        //    //this.Sort();
        //    //this.ListViewItemSorter = m_ColumnSorter;
        //    base.OnColumnClick(e);
        //}
        #endregion

        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Control | Keys.A:
                    for (int i = 0; i < this.VirtualListSize; i++)
                    {
                        this.SelectedIndices.Add(i);
                    }
                    break;
            }
            base.OnKeyDown(e);
        }

        #endregion Overrided functions

        internal void Init()
        {
            this.HideSelection = false;
            this.MultiSelect = true;
            this.AllowDrop = true;
            this.ShowGroups = false;
            this.FullRowSelect = true;
            this.LabelEdit = true;
            this.DoubleBuffered = true;

            DefineColumn();
            this.SetSortIcon(0, SortOrder.Ascending);

            this.Resize += FileBrowser_Resize;
        }
        
        private void DefineColumn()
        {
            this.View = View.Details;
            this.HeaderStyle = ColumnHeaderStyle.Clickable;
            this.Columns.Add("Name");      // 0
            this.Columns.Add("Ext", 40);   // 1
            this.Columns.Add("Size", 60);  // 2
            this.Columns.Add("Date", 100); // 3
            this.Columns.Add("Attr", 35);  // 4
            int tmp = this.Width - 235;
            this.Columns[0].Width = (tmp > 60) ? tmp : 60;
        }

        public void ChangeViewMode(View view)
        {
            if (this.View == view) return;
            switch (view)
            {
                case View.List:
                    this.HeaderStyle = ColumnHeaderStyle.None;
                    this.Columns.Clear();
                    this.View = View.List;
                    break;
                case View.Details:
                    DefineColumn();
                    break;
            }
        }

        /// <summary>
        /// Sets to Name columns be the longest column
        /// </summary>
        void FileBrowser_Resize(object sender, EventArgs e)
        {
            if (this.View == View.Details)
            {
                int total = 0;
                for (int i = 1; i < 5; i++)
                {
                    total += this.Columns[i].Width;
                }
                int tmp = this.Width - total;
                this.Columns[0].Width = (tmp > 60) ? tmp : 60;
            }
        }
        
    }
}
