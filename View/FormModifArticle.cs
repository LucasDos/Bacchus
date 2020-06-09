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
    partial class FormModifArticle : Form
    {
        public FormModifArticle()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initialise la fenêtre avec les données de l'article
        /// </summary>
        /// <param name="article"></param>
        public void InitializeDataComponent(Article article)
        {
            reference_label.Text = article.Reference;
            description_input.Text = article.Description;
            prix_input.Text = article.Prix.ToString();
            quantite_input.Value = article.Quantite;

            sousfamille_cbx.Text = article.SousFamille.Nom;
            // Rempli la combobox de toutes les Sous Familles
            List<SousFamille> sousFamilles = SousFamilleDAO.GetAll();
            foreach(SousFamille sf in sousFamilles)
            {
                sousfamille_cbx.Items.Add(sf.Nom);
            }


            marque_cbx.Text = article.Marque.Nom;
            List<Marque> marques = MarqueDAO.GetAll();
            foreach(Marque marque in marques)
            {
                marque_cbx.Items.Add(marque.Nom);
            }
        }

        /// <summary>
        /// Detecte quand on clique sur le bouton Modifier
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void valider_btn_Click(object sender, EventArgs e)
        {
            if( description_input.Text.Equals("") || prix_input.Text.Equals("")
                || sousfamille_cbx.Text.Equals("") || marque_cbx.Text.Equals("") )
            {
                MessageBox.Show("Veuillez remplir correctement tous les champs !");
            }
            else
            {
                SousFamille sousFamille = SousFamilleDAO.GetWhereName(sousfamille_cbx.Text);
                Marque marque = MarqueDAO.GetWhereName(marque_cbx.Text);
                float prix = Single.Parse(prix_input.Text);
                int quantite = Convert.ToInt32(quantite_input.Value);

                Article article = new Article(reference_label.Text, description_input.Text, sousFamille, marque, prix, quantite);
                ArticleDAO.ModifyArticle(article);

                this.Close();
            }
        }
    }
}
