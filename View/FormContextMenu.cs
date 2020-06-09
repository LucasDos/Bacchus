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
    partial class FormContextMenu : Form
    {
        public Article article;
        public FormContextMenu()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sauvegarde l'article
        /// </summary>
        /// <param name="article"></param>
        public void SaveArticle(Article article)
        {
            this.article = article;
        }

        /// <summary>
        /// Detecte les clique sur le bouton Ajouter Article
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void add_btn_Click_1(object sender, EventArgs e)
        {
            FormAddArticle formAdd = new FormAddArticle();
            formAdd.ShowDialog();

            this.Close();
        }

        /// <summary>
        /// Detecte le clique sur le bouton Modifier Article
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void modif_btn_Click_1(object sender, EventArgs e)
        {
            FormModifArticle formModif = new FormModifArticle();
            formModif.InitializeDataComponent(this.article);
            formModif.ShowDialog();

            this.Close();
        }

        /// <summary>
        /// Detecte le clique sur le bouton supprimer Article
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void suppr_btn_Click_1(object sender, EventArgs e)
        {
            var validationMessage = MessageBox.Show(@"Voulez-vous supprimer l'article " + article.Description + @" ?",
                    @"Suppression d'un article", MessageBoxButtons.YesNo);

            // Annule la suppression si "non" 
            if (validationMessage != DialogResult.Yes)
            {
                return;
            }

            ArticleDAO.RemoveArticle(this.article);

            this.Close();
        }

        /// <summary>
        /// Detecte le clique sur le bouton Ajouter Famille
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void famAjout_btn_Click(object sender, EventArgs e)
        {
            FormAddFamille formAddFamille = new FormAddFamille();
            formAddFamille.ShowDialog();
        }

        /// <summary>
        /// Detecte le clique sur le bouton ajouter SousFamille
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sfAjout_btn_Click(object sender, EventArgs e)
        {
            FormAddSousFamille formSF = new FormAddSousFamille();
            formSF.ShowDialog();
        }

        /// <summary>
        /// Detecte le clique sur le bouton Ajouter Marque
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void marqueAjout_btn_Click(object sender, EventArgs e)
        {
            FormAddMarque formMarque = new FormAddMarque();
            formMarque.ShowDialog();
        }
    }
}
