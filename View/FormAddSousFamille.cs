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
    public partial class FormAddSousFamille : Form
    {
        public FormAddSousFamille()
        {
            InitializeComponent();
            InitializeDataComponent();
        }

        /// <summary>
        /// Initialise la CheckBox des Familles
        /// </summary>
        public void InitializeDataComponent()
        {
            List<Famille> familles = FamilleDAO.GetAll();
            foreach (Famille fam in familles)
            {
                famille_cbx.Items.Add(fam.Nom);
            }
        }

        /// <summary>
        /// Ajoute la SOusFamille dans la SQL
        /// </summary>
        public void AddSousFamilleSQL()
        {
            // Retire le caractere '
            string name = name_input.Text;
            name = name.Replace(@"'", "");

            Famille famille = FamilleDAO.GetWhereName(famille_cbx.Text);
            SousFamille sousFamille = new SousFamille(0, famille, name_input.Text);

            if (SousFamilleDAO.Insert(sousFamille) == 0)
            {
                MessageBox.Show("L'ajout de la SOus Famille a échoué !");
            }
        }

        /// <summary>
        /// detecte quand on clique sur le bouton Ajouter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void add_button_Click(object sender, EventArgs e)
        {
            if (famille_cbx.Text.Equals("") || name_input.Text.Equals(""))
            {
                MessageBox.Show("Veuillez remplir correctement tous les champs !");
            }
            else
            {
                AddSousFamilleSQL();

                this.Close();
            }
        }
    }
}
