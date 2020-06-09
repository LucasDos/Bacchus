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
    public partial class FormAddArticle : Form
    {
        private List<SousFamille> sousFamilles;
        private List<Marque> marques;

        public FormAddArticle()
        {
            InitializeComponent();
            InitializeCombobox();
        }

        /// <summary>
        /// Initialise les ComboBox de SousFamille et Marque
        /// </summary>
        public void InitializeCombobox()
        {
            List<SousFamille> sousFamilles = SousFamilleDAO.getAll();
            List<Marque> marques = MarqueDAO.GetAll();

            foreach (SousFamille sf in sousFamilles)
            {
                sousfamille_cbx.Items.Add(sf.Nom);
            }
            foreach (Marque marque in marques)
            {
                marque_cbx.Items.Add(marque.Nom);
            }
        }

        /// <summary>
        /// Ajoute l'article dans la BDD
        /// </summary>
        public void addArticleSQL()
        {
            string refArticle = reference_input.Text;
            string description = description_input.Text;
            float prix = Single.Parse(prix_input.Text);
            int quantite = Convert.ToInt32(quantite_input.Value);
            SousFamille sousFamille = SousFamilleDAO.GetWhereName(sousfamille_cbx.Text);
            Marque marque = MarqueDAO.GetWhereName(marque_cbx.Text);

            if(ArticleDAO.Insert(new Article(refArticle, description, sousFamille, marque, prix, quantite)) == null)
            {
                MessageBox.Show("L'ajout de l'Article a échoué !");
            }
        }

        /// <summary>
        /// Detecte le clique sur le bouton Ajouter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void valider_btn_Click(object sender, EventArgs e)
        {
            string defDescription = "Tapez la description de l'article ici...";
            if ( (reference_input.Text).Equals("") || (description_input.Text.Equals(defDescription)) 
                || (prix_input.Text.Equals("")) || sousfamille_cbx.Text.Equals("") || marque_cbx.Text.Equals(""))
            {
                MessageBox.Show("Veuillez remplir correctement tous les champs !");
            }
            else
            {
                addArticleSQL();

                this.Close();
            }
            
        }
    }
}
