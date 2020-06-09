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
    partial class FormModifMarque : Form
    {
        public FormModifMarque()
        {
            InitializeComponent();
        }

        public void InitializeDataComponent(Marque marque)
        {
            reference_lbl.Text = Convert.ToString(marque.Reference);
            name_input.Text = marque.Nom;
        }
        private void modify_btn_Click(object sender, EventArgs e)
        {
            if( name_input.Text.Equals(""))
            {
                MessageBox.Show("Veuillez remplir correctement les champs !");
            }
            else
            {
                Marque marque = new Marque(Convert.ToInt32(reference_lbl.Text), name_input.Text);
                MarqueDAO.updateMarque(marque);

                this.Close();
            }
        }
    }
}
