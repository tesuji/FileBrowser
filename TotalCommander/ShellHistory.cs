using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TotalCommander
{
    /// <summary>
    /// The history of the shell explorer
    /// </summary>
    public class ShellHistory
    {
        #region Local variables

        private List<string> m_History;
        private int m_Current;

        #endregion

        internal ShellHistory()
        {
            m_Current = -1;
            m_History = new List<string>();
        }

        /// <summary>
        /// Clears the history.
        /// </summary>
        public void Clear()
        {
            if (m_History.Count > 0)
            {
                m_Current = -1;
                m_History.Clear();
            }

        }

        internal void Add(string folder)
        {
            int count = m_History.Count - m_Current - 1;
            if (count > 0)
            {
                m_History.RemoveRange(m_Current + 1, count);
            }
            //while (_Current < _History.Count - 1)
            //{
            //    _History.RemoveAt(_Current + 1);
            //}

            m_History.Add(folder);
            m_Current = m_History.Count - 1;
        }

        internal string MoveBackward()
        {
            if (m_Current < 0)
            {
                throw new InvalidOperationException("Cannot navigate back");
            }
            m_Current -= 1;
            return m_History[m_Current];
        }

        internal string MoveForward()
        {
            if (m_Current == m_History.Count - 1)
            {
                throw new InvalidOperationException("Cannot navigate forward");
            }
            m_Current += 1;
            return m_History[m_Current];
        }

        internal bool CanNavigateBack
        {
            get { return m_Current > 0; }
        }

        internal bool CanNavigateForward
        {
            get { return m_Current != m_History.Count - 1; }
        }
    }
}
