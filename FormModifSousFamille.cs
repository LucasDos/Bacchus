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
    partial class FormModifSousFamille : Form
    {
        public FormModifSousFamille()
        {
            InitializeComponent();
            InitializeComboBox();
        }

        /// <summary>
        /// Initialise la combobox des Familles
        /// </summary>
        public void InitializeComboBox()
        {
            List<Famille> familles = FamilleDAO.GetAll();

            foreach(Famille famille in familles)
            {
                famille_cbx.Items.Add(famille.Nom);
            }
        }

        /// <summary>
        /// Initialize la fenetre avec les données des SousFamilles
        /// </summary>
        /// <param name="sousFamille">SousFamille à modifier</param>
        public void InitializeDataComponent(SousFamille sousFamille)
        {
            reference_lbl.Text = Convert.ToString(sousFamille.RefSousFamille);
            famille_cbx.Text = sousFamille.RefFamille.Nom;
            name_input.Text = sousFamille.Nom;
        }

        /// <summary>
        /// Detecte quand on clique sur le bouton Modifier
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void modify_btn_Click(object sender, EventArgs e)
        {
            if ( name_input.Text.Equals("") || famille_cbx.Text.Equals(""))
            {
                MessageBox.Show("Veuillez remplir correctement les champs !");
            }
            else
            {
                Famille famille = FamilleDAO.GetWhereName(famille_cbx.Text);

                SousFamille sousFamille = new SousFamille(Convert.ToInt32(reference_lbl.Text), famille, name_input.Text);
                SousFamilleDAO.updateSousFamille(sousFamille);

                this.Close();
            }
        }
    }
}
