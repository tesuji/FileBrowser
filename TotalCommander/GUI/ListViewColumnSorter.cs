using System;
using System.Collections;
using System.Windows.Forms;

namespace TotalCommander.GUI
{
    /// <summary>
    /// This class is an implementation of the 'IComparer' interface.
    /// </summary>
    public class ListViewColumnSorter : IComparer
    {
        #region Fields
        /// <summary>
        /// Specifies the column to be sorted
        /// </summary>
        private int ColumnToSort;
        /// <summary>
        /// Specifies the order in which to sort (i.e. 'Ascending').
        /// </summary>
        private SortOrder OrderOfSort;
        /// <summary>
        /// Case insensitive comparer object
        /// </summary>
        private CaseInsensitiveComparer ObjectCompare;
        #endregion

        /// <summary>
        /// Class constructor.  Initializes various elements
        /// </summary>
        public ListViewColumnSorter()
        {
            // Initialize the column to '0'
            ColumnToSort = 0;

            // Initialize the sort order to 'none'
            OrderOfSort = SortOrder.None;

            // Initialize the CaseInsensitiveComparer object
            ObjectCompare = new CaseInsensitiveComparer();
        }

        /// <summary>
        /// This method is inherited from the IComparer interface.  It compares the two objects passed using a case insensitive comparison.
        /// </summary>
        /// <param name="x">First object to be compared</param>
        /// <param name="y">Second object to be compared</param>
        /// <returns>The result of the comparison. "0" if equal, negative if 'x' is less than 'y' and positive if 'x' is greater than 'y'</returns>
        public int Compare(object x, object y)
        {
            int result = 1;
            string firstItem = ((ListViewItem)x).SubItems[ColumnToSort].Text;
            string secondItem = ((ListViewItem)y).SubItems[ColumnToSort].Text;
            switch (ColumnToSort)
            {
                case 0: // name column
                    string size1 = ((ListViewItem)x).SubItems[2].Text;
                    string size2 = ((ListViewItem)y).SubItems[2].Text;
                    int flag = (IsFolder(size1) ? 1 : 0) + (IsFolder(size2) ? 2 : 0);
                    switch (flag)
                    {
                        // Neither item is a folder => Compare names
                        case 0: goto case 3;
                        // A is a folder, but B isn't => A < B
                        case 1: result = -1; break;
                        // B is a folder, but A isn't => A > B
                        case 2: result = 1; break;
                        // Both items are folders => Compare names
                        case 3: result = firstItem.CompareTo(secondItem); break;
                        // Failsafe
                        default:
                            result = 0;
                            break;
                    }
                    break;
                case 2: // size column
                    // Both items have the same size or are folders
                    if (String.Equals(firstItem, secondItem))
                        result = 0;
                    // A is a folder, but B isn't => A < B
                    else if (IsFolder(firstItem))
                        result = -1;
                    // B is a folder, but A isn't => A > B
                    else if (IsFolder(secondItem))
                        result = 1;
                    // Both items are files with different sizes
                    else
                    {
                        string[] firstSize = firstItem.Split();
                        string[] secondSize = secondItem.Split();
                        long firstByte = GetTotalBytes(firstSize[0], firstSize[1]);
                        long secondByte = GetTotalBytes(secondSize[0], secondSize[1]);
                        result = firstByte.CompareTo(secondByte);
                    }
                    break;
                case 3: // date column
                    DateTime firstDate, secondDate;
                    bool canConvert1 = DateTime.TryParse(firstItem, out firstDate);
                    bool canConvert2 = DateTime.TryParse(secondItem, out secondDate);
                    if (canConvert1 && canConvert2)
                    {
                        // Compare the two dates.
                        result = firstDate.CompareTo(secondDate);
                    }
                    break;
                default: // other columns, compares as string
                    result = firstItem.CompareTo(secondItem);
                    break;
            }
            // Determine whether the sort order is descending.
            return (OrderOfSort == SortOrder.Descending) ? -result : result;
        }

        private bool IsFolder(string size)
        {
            return String.Equals("<DIR>", size);
        }

        private long GetTotalBytes(string number, string suffix)
        {
            long result = 0;
            if (suffix == "B")
            {
                long.TryParse(number, out result);
            }
            else
            {
                double readable;
                if (double.TryParse(number, out readable))
                {
                    readable *= 1024;
                    result = Convert.ToInt64(readable);
                    if (suffix == "MB")
                    {
                        result = (result << 10);
                    }
                    else if (suffix == "GB")
                    {
                        result = (result << 20);
                    }
                }
            }
            return result;
        }

        #region Properties
        /// <summary>
        /// Gets or sets the number of the column to which to apply the sorting operation (Defaults to '0').
        /// </summary>
        public int SortColumn
        {
            set { ColumnToSort = value; }
            get { return ColumnToSort; }
        }

        /// <summary>
        /// Gets or sets the order of sorting to apply (for example, 'Ascending' or 'Descending').
        /// </summary>
        public SortOrder Order
        {
            set { OrderOfSort = value; }
            get { return OrderOfSort; }
        }
        #endregion
    }
}
