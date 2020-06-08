using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.SQLite;
using Bacchus.Model;
using Bacchus.DAO;

namespace Bacchus
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            
        }

        public void InitializeTreeView()
        {
            
            treeView1.BeginUpdate();
            treeView1.Nodes[1].Tag = "Familles";
            addFamilles(treeView1.Nodes[1]);
            addMarques(treeView1.Nodes[2]);

            treeView1.EndUpdate();
        }

        public void addFamilles(TreeNode node)
        {
            List<Famille> familles = FamilleDAO.GetAll();

            foreach (Famille famille in familles)
            {
                TreeNode newNode = new TreeNode(famille.Nom);
                node.Nodes.Add(newNode);
                List<SousFamille> sousFamilles = SousFamilleDAO.GetWhereFamilleByRef(famille);

                foreach (SousFamille sousFamille in sousFamilles)
                {
                    newNode.Nodes.Add(sousFamille.Nom);
                }
            }
        }

        public void addMarques(TreeNode node)
        {
            List<Marque> marques = MarqueDAO.GetAll();

            foreach (Marque marque in marques)
            {
                node.Nodes.Add(marque.Nom);
            }
        }

        public void importerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormImporter formImpor = new FormImporter();
            formImpor.ShowDialog();
            InitializeTreeView();
        }

        public void exporterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormExporter formExport = new FormExporter();
            formExport.Show();
        }

        public void UpdateListView()
        {
            listView1.Clear();
            List<Article> articles = null;

            if( (treeView1.SelectedNode.Level == 0) )
            {
                // Affichage de tous les articles

                listView1.Name = "ArticleListe";

                listView1.Columns.Add("Description", 400, HorizontalAlignment.Center);
                listView1.Columns.Add("Sous-Familles", 200, HorizontalAlignment.Center);
                listView1.Columns.Add("Marque", 100, HorizontalAlignment.Center);
                listView1.Columns.Add("Quantité", 50, HorizontalAlignment.Center);

                articles = ArticleDAO.GetAll();

                if(articles.Count > 0)
                {
                    foreach (Article article in articles)
                    {
                        var row = new string[]
                        {
                            article.Description, article.SousFamille.Nom, 
                            article.Marque.Nom, article.Quantite.ToString()
                        };

                        var lvi = new ListViewItem(row);
                        lvi.Tag = article;
                        listView1.Items.Add(lvi);
                    }
                }
            }
            else
            {
                switch (treeView1.SelectedNode.Parent.Text)
                {
                    case "Familles":
                        // Affichage de tous les articles d'une famille

                        Console.WriteLine("test");

                        Famille famille = FamilleDAO.GetWhereName(treeView1.SelectedNode.Text);
                        List<SousFamille> sousFamilles = SousFamilleDAO.GetWhereFamilleByRef(famille);

                        articles = new List<Article>();
                        foreach (SousFamille indexSF in sousFamilles)
                        {
                            List<Article> tmpArticle = ArticleDAO.getArticleBySousFamille(indexSF);
                            foreach (Article indexArticle in tmpArticle)
                            {
                                articles.Add(indexArticle);
                            }
                        }

                        foreach (Article indexArticles in articles)
                        {
                            var row = new string[]
                            {
                                indexArticles.Description,
                            };

                            var lvi = new ListViewItem(row);
                            lvi.Tag = indexArticles;
                            listView1.Items.Add(lvi);
                        }
                            break;
                    case "Marques":
                        /** Affichage de tous les articles d'une marque */
                        Marque marque = MarqueDAO.GetWhereName(treeView1.SelectedNode.Text);
                        articles = ArticleDAO.getByMarque(marque);

                        foreach (Article indexArticles in articles)
                        {
                            var row = new string[]
                            {
                                indexArticles.Description,
                            };

                            var lvi = new ListViewItem(row);
                            lvi.Tag = indexArticles;
                            listView1.Items.Add(lvi);
                        }
                        break;
                }
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            UpdateListView();
        }
    }
}
