using Bacchus.DAO;
using Bacchus.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bacchus
{
    public partial class FormAddMarque : Form
    {
        public FormAddMarque()
        {
            InitializeComponent();
        }

        public void addMarqueSQL()
        {
            Marque marque = new Marque(0, name_input.Text);

            if( MarqueDAO.Insert(marque) == 0)
            {
                MessageBox.Show("L'ajout de la Marque a échoué !");
            }
        }

        private void add_btn_Click(object sender, EventArgs e)
        {
            addMarqueSQL();

            this.Close();
        }
    }
}
