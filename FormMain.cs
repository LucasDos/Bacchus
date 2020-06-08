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

        /// <summary>
        /// Initialise le composant treeView1
        /// </summary>
        public void InitializeTreeView()
        {
            
            treeView1.BeginUpdate();

            addFamilles(treeView1.Nodes[1]);
            addMarques(treeView1.Nodes[2]);

            treeView1.EndUpdate();
        }

        /// <summary>
        /// Ajoute les famille dans la treeView1
        /// </summary>
        /// <param name="node">Node "Familles"</param>
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

        /// <summary>
        /// Ajoute les marques dans le treeView1
        /// </summary>
        /// <param name="node">Node "Marques"</param>
        public void addMarques(TreeNode node)
        {
            List<Marque> marques = MarqueDAO.GetAll();

            foreach (Marque marque in marques)
            {
                node.Nodes.Add(marque.Nom);
            }
        }

        /// <summary>
        /// Lucas je sais pas ce que c'est, tu peux mettre cette cartouche de com stp
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void importerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormImporter formImpor = new FormImporter();
            formImpor.ShowDialog();
            InitializeTreeView();
        }

        /// <summary>
        /// Lucas je sais pas ce que c'est, tu peux mettre cette cartouche de com stp
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void exporterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormExporter formExport = new FormExporter();
            formExport.Show();
        }

        /// <summary>
        /// Met a jour la listView1 pour afficher les articles/familles/SousFamilles/Marques
        /// </summary>
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

                        listView1.Name = "DescriptionByFamilles";
                        listView1.Columns.Add("Description", 400, HorizontalAlignment.Center);

                        Famille famille = FamilleDAO.GetWhereName(treeView1.SelectedNode.Text);

                        if (famille != null)
                        {
                            List<SousFamille> sousFamilles = SousFamilleDAO.GetWhereFamilleByRef(famille);

                            if(sousFamilles.Count > 0)
                            {
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
                                        indexArticles.Description
                                    };

                                    var lvi = new ListViewItem(row);
                                    lvi.Tag = indexArticles;
                                    listView1.Items.Add(lvi);
                                }
                            }
                        }                        
                        break;
                    case "Marques":
                        // Affichage de tous les articles d'une marque 

                        listView1.Name = "DescriptionByMarques";
                        listView1.Columns.Add("Description", 400, HorizontalAlignment.Center);

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
                    default:
                        // Affichage par sous familles
                        listView1.Name = "DescriptionByMarques";
                        listView1.Columns.Add("Description", 400, HorizontalAlignment.Center);
                        listView1.Columns.Add("Sous-Familles", 200, HorizontalAlignment.Center);
                        listView1.Columns.Add("Marque", 100, HorizontalAlignment.Center);
                        listView1.Columns.Add("Quantité", 50, HorizontalAlignment.Center);

                        SousFamille sousFamille = SousFamilleDAO.GetWhereName(treeView1.SelectedNode.Text);
                        
                        if(sousFamille != null)
                        {
                            articles = ArticleDAO.getArticleBySousFamille(sousFamille);

                            foreach (Article indexArticles in articles)
                            {
                                var row = new string[]
                                {
                                    indexArticles.Description, indexArticles.SousFamille.Nom,
                                    indexArticles.Marque.Nom, indexArticles.Quantite.ToString()
                                };

                                var lvi = new ListViewItem(row);
                                lvi.Tag = indexArticles;
                                listView1.Items.Add(lvi);
                            }
                        }

                        break;
                }
            }
        }

        /// <summary>
        /// Detecte le click sur une node
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            UpdateListView();
        }

        private void listView1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    // TODO: Modifiy Artcile
                    modifyOption();
                    break;
                case Keys.F5:
                    UpdateListView();
                    break;
                case Keys.Delete:
                    // TODO : Suppr Item
                    removeArticle();
                    break;
            }
        }

        /// <summary>
        /// Supprimer l'article sélectionné
        /// </summary>
        public void removeArticle()
        {
            var descriptionItem = listView1.SelectedItems[0];
            var validationMessage = MessageBox.Show(@"Voulez-vous supprimer l'article " + descriptionItem + @" ?",
                    @"Suppression d'un article", MessageBoxButtons.YesNo);

            // Annule la suppression si "non" 
            if (validationMessage != DialogResult.Yes) 
            { 
                return; 
            }

            // Récupère l'article selectionné dans la BDD
            Article article = ArticleDAO.getByDescription(descriptionItem.Text);
            if (article == null)
            {
                MessageBox.Show("L'article n'existe pas");
                return;
            }

            ArticleDAO.removeArticle(article);
        }

        public void modifyOption()
        {

        }

       
    }
}
