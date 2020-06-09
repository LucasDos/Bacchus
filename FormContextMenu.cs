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

        private void add_btn_Click(object sender, EventArgs e)
        {
            FormAddArticle formAdd = new FormAddArticle();
            formAdd.Show();
        }

        private void modif_btn_Click(object sender, EventArgs e)
        {
            FormModifArticle formModif = new FormModifArticle();
            formModif.InitializeDataComponent(this.article);
            formModif.Show();
            this.Close();
        }

        private void suppr_btn_Click(object sender, EventArgs e)
        {
            ArticleDAO.removeArticle(this.article);
            this.Close();
        }
    }
}
