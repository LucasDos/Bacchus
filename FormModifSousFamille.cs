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

        public void InitializeComboBox()
        {
            List<Famille> familles = FamilleDAO.GetAll();

            foreach(Famille famille in familles)
            {
                famille_cbx.Items.Add(famille.Nom);
            }
        }

        public void InitializeDataComponent(SousFamille sousFamille)
        {
            reference_lbl.Text = Convert.ToString(sousFamille.RefSousFamille);
            famille_cbx.Text = sousFamille.RefFamille.Nom;
            name_input.Text = sousFamille.Nom;
        }

        private void modify_btn_Click(object sender, EventArgs e)
        {
            if ( name_input.Text.Equals("") || famille_cbx.Text.Equals(""))
            {
                MessageBox.Show("Veuillez remplir correctement les champs !");
            }
            else
            {
                Famille famille = FamilleDAO.GetWhereName(famille_cbx.Text);
                SousFamille sousFamille = new SousFamille(Convert.ToInt32(reference_lbl), famille, name_input.Text);

                SousFamilleDAO.updateSousFamille(sousFamille);
            }
        }
    }
}
