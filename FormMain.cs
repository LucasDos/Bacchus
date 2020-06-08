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
            InitializeTreeView();
        }

        public void InitializeTreeView()
        {
            
            treeView1.BeginUpdate();
            treeView1.Nodes.Clear();

            treeView1.Nodes.Add("Tous les articles");
            treeView1.Nodes.Add("Familles");
            treeView1.Nodes.Add("Marques");

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
            Console.WriteLine(treeView1.SelectedNode.Index);

            listView1.Clear();

            if (treeView1.SelectedNode.Index == 0)
            {
                listView1.Name = "ArticleListe";

                listView1.Columns.Add("Description", -2, HorizontalAlignment.Center);
                listView1.Columns.Add("Familles", -2, HorizontalAlignment.Center);
                listView1.Columns.Add("Sous-Familles", -2, HorizontalAlignment.Center);
                listView1.Columns.Add("Marque", -2, HorizontalAlignment.Center);
                listView1.Columns.Add("Quantité", -2, HorizontalAlignment.Center);

                List<Article> articles = ArticleDAO.GetAll();
                foreach (Article article in articles)
                {
                    var row = new string[]
                    {
                                article.Description, article.Famille.Nom,
                                article.SousFamille.Nom, article.Marque.Nom,
                                article.Quantite.ToString()
                    };

                    var lvi = new ListViewItem(row);
                    lvi.Tag = article;
                    listView1.Items.Add(lvi);
                }
            }
            else
            {
                listView1.Name = "DescriptionList";
                listView1.Columns.Add("Description", -2, HorizontalAlignment.Center);

                // Liste d'article globale à afficher
                List<Article> articles = new List<Article>();

                switch (treeView1.SelectedNode.Index)
                {
                    case 1:
                        /** Familles selectionné */
                        Famille famille = FamilleDAO.GetWhereName(treeView1.SelectedNode.Text);
                        List<SousFamille> sousFamilles = SousFamilleDAO.GetWhereFamilleByRef(famille);

                        foreach(SousFamille indexSF in sousFamilles)
                        {
                            List<Article> tmpArticle = ArticleDAO.getArticleBySousFamille(indexSF);
                            foreach(Article article in tmpArticle)
                            {
                                articles.Add(article);
                            }
                        }

                        foreach (Article indexArticle in articles)
                        {
                            var row = new string[]
                            {
                                indexArticle.Description
                            };

                            var lvi = new ListViewItem(row);
                            lvi.Tag = indexArticle;
                            listView1.Items.Add(lvi);
                        }

                        break;
                    case 2:
                        /** Sous Famille sélectionné */
                        SousFamille sousFamille = SousFamilleDAO.GetWhereName(treeView1.SelectedNode.Text);
                        articles = ArticleDAO.getArticleBySousFamille(sousFamille);

                        foreach (Article indexArticle in articles)
                        {
                            var row = new string[]
                            {
                                indexArticle.Description
                            };

                            var lvi = new ListViewItem(row);
                            lvi.Tag = indexArticle;
                            listView1.Items.Add(lvi);
                        }
                        break;
                    case 3:
                        /** Marque selectionné */
                        Marque marque = MarqueDAO.GetWhereName(treeView1.SelectedNode.Text);
                        articles = ArticleDAO.getByMarque(marque);

                        foreach (Article indexArticle in articles)
                        {
                            var row = new string[]
                            {
                                indexArticle.Description
                            };

                            var lvi = new ListViewItem(row);
                            lvi.Tag = indexArticle;
                            listView1.Items.Add(lvi);
                        }
                        break;
                }
            }
        }
    }
}
