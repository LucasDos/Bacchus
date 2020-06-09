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

        public void InitializeDataComponent()
        {
            List<Famille> familles = FamilleDAO.GetAll();
            foreach (Famille fam in familles)
            {
                famille_cbx.Items.Add(fam.Nom);
            }
        }

        public void addSousFamilleSQL()
        {
            Famille famille = FamilleDAO.GetWhereName(famille_cbx.Text);
            SousFamille sousFamille = new SousFamille(0, famille, name_input.Text);

            if (SousFamilleDAO.Insert(sousFamille) == 0)
            {
                MessageBox.Show("L'ajout de la SOus Famille a échoué !");
            }
        }

        private void add_button_Click(object sender, EventArgs e)
        {
            addSousFamilleSQL();

            this.Close();
        }
    }
}
