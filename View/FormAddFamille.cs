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
    public partial class FormAddFamille : Form
    {
        public FormAddFamille()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Ajoute une famille dans la BDD
        /// </summary>
        public void AddFamilleSQL()
        {
            Famille famille = new Famille(0, name_input.Text);
            if ( FamilleDAO.Insert(famille)==0)
            {
                MessageBox.Show("L'ajout de la Famille a échoué !");
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
                MessageBox.Show("Veuillez remplir correctement tous les champs !");
            }
            else
            {
                AddFamilleSQL();

                this.Close();
            }
        }
    }
}
