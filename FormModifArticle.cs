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

        public void InitializeDataComponent(Article article)
        {
            reference_input.Text = article.Reference;
            description_input.Text = article.Description;
            prix_input.Text = article.Prix.ToString();
            quantite_input.Value = article.Quantite;

            sousfamille_cbx.Text = article.SousFamille.Nom;
            SousFamilleDAO.getAll();
            //sousfamille_cbx.Items.Add
            marque_cbx.Text = article.Marque.Nom;

        }

        private void valider_btn_Click(object sender, EventArgs e)
        {
            SousFamille sousFamille = SousFamilleDAO.GetWhereName(sousfamille_cbx.Text);
            Marque marque = MarqueDAO.GetWhereName(marque_cbx.Text);
            double prix = Convert.ToDouble(prix_input.Text);
            int quantite = Convert.ToInt32(quantite_input.Value);
            

            Article article = new Article(reference_input.Text, description_input.Text, sousFamille, marque, prix, quantite);
        }
    }
}
