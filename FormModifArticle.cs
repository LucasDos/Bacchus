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
    public partial class FormModifArticle : Form
    {
        private FormModifArticle article;
        public FormModifArticle()
        {
            InitializeComponent();
        }

        private void InitializeDataComponent(Article article)
        {
            reference_input.Text = article.Reference;
            description_input.Text = article.Description;
            prix_input.Text = article.Prix.ToString();
            quantite_input.Value = article.Quantite;
            sousfamille_cbx.Text = article.SousFamille.Nom;
            marque_cbx.Text = article.Marque.Nom;

        }
    }
}
