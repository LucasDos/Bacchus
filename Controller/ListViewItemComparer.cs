using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bacchus.Controller
{
    class ListViewItemComparer : IComparer
    {
        private int SortColumn;
        private SortOrder order;

        /// <summary>
        /// Constructeur par defaut
        /// </summary>
        public ListViewItemComparer()
        {
            this.SortColumn = 0;
            this.order = SortOrder.None;
        }

        /// <summary>
        /// Constructeur par de recopie de la colonne
        /// </summary>
        public ListViewItemComparer(int ColumnToSort)
        {
            this.SortColumn = ColumnToSort;
            this. order = SortOrder.Ascending;
        }

        public int Compare(object firstObject, object secondObject)
        {
            int res = 0;
            var firstItem = (ListViewItem)firstObject;
            var secondItem = (ListViewItem)secondObject;

            switch (this.SortColumn)
            {
                case 3:
                    // Si on trie par quantité
                    int firstItemInt = Convert.ToInt32(firstItem.SubItems[0].Text);
                    int secondItemInt = Convert.ToInt32(secondItem.SubItems[0].Text);

                    res = firstItemInt - secondItemInt;
                    break;
                default:
                    // Compare les deux chaines de caractères
                    res = String.Compare(firstItem.SubItems[this.SortColumn].Text, secondItem.SubItems[this.SortColumn].Text);
                    break;
            }

            return res;
        }
    }
}
