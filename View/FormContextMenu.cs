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

        public void saveArticle(Article article)
        {
            this.article = article;
        }

        private void add_btn_Click_1(object sender, EventArgs e)
        {
            FormAddArticle formAdd = new FormAddArticle();
            formAdd.ShowDialog();

            this.Close();
        }

        private void modif_btn_Click_1(object sender, EventArgs e)
        {
            FormModifArticle formModif = new FormModifArticle();
            formModif.InitializeDataComponent(this.article);
            formModif.ShowDialog();

            this.Close();
        }

        private void suppr_btn_Click_1(object sender, EventArgs e)
        {
            var validationMessage = MessageBox.Show(@"Voulez-vous supprimer l'article " + article.Description + @" ?",
                    @"Suppression d'un article", MessageBoxButtons.YesNo);

            // Annule la suppression si "non" 
            if (validationMessage != DialogResult.Yes)
            {
                return;
            }

            ArticleDAO.removeArticle(this.article);

            this.Close();
        }

        private void famAjout_btn_Click(object sender, EventArgs e)
        {
            FormAddFamille formAddFamille = new FormAddFamille();
            formAddFamille.ShowDialog();
        }

        private void sfAjout_btn_Click(object sender, EventArgs e)
        {
            FormAddSousFamille formSF = new FormAddSousFamille();
            formSF.ShowDialog();
        }

        private void marqueAjout_btn_Click(object sender, EventArgs e)
        {
            FormAddMarque formMarque = new FormAddMarque();
            formMarque.ShowDialog();
        }
    }
}
