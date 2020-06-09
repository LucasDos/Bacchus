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
        public ListViewItemComparer(int ColumnToSort, SortOrder Order)
        {
            this.SortColumn = ColumnToSort;
            this. order = Order;
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
                    this.order = SortOrder.Descending;
                    int firstInt = Convert.ToInt32(firstItem.SubItems[this.SortColumn].Text);
                    int secondInt = Convert.ToInt32(secondItem.SubItems[this.SortColumn].Text);

                    res = firstInt - secondInt;
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
