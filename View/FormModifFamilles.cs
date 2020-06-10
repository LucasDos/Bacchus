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
    partial class FormModifFamilles : Form
    {
        public FormModifFamilles()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initialise la fenetre avec les données de la Famille
        /// </summary>
        /// <param name="famille">Famille à modifier</param>
        public void InitializeDataComponent(Famille famille)
        {
            referece_lbl.Text = Convert.ToString(famille.Reference);
            name_input.Text = famille.Nom;
        }

        /// <summary>
        /// Detecte quand on clique sur le bouton modifier
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void modify_btn_Click(object sender, EventArgs e)
        {
            if( name_input.Text.Equals(""))
            {
                MessageBox.Show("Veuillez rentrer un nom différent.");
            }
            else
            {
                string name = name_input.Text;

                // Replace '
                name = name.Replace(@"'", "");

                Famille famille = new Famille(Convert.ToInt32(referece_lbl.Text), name);
                FamilleDAO.Insert(famille);

                this.Close();
            }
        }
    }
}
