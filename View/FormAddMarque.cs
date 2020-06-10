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

        /// <summary>
        /// Ajoute la Marque dans le BDD
        /// </summary>
        public void AddMarqueSQL()
        {
            // Retire le caractere '
            string name = name_input.Text;
            name = name.Replace(@"'", "");

            Marque marque = new Marque(0, name);

            if( MarqueDAO.Insert(marque) == 0)
            {
                MessageBox.Show("Cette Marque existe déjà.");
            }
        }

        /// <summary>
        /// Detecte quand on clique sur le bouton ajouter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void add_btn_Click(object sender, EventArgs e)
        {
            if( name_input.Text.Equals(""))
            {
                MessageBox.Show("Veuillez remplir correctement les champs !");
            }
            else
            {
                AddMarqueSQL();

                this.Close();
            }
        }
    }
}
