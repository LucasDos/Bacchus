﻿using System;
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
using Bacchus.Controller;

namespace Bacchus
{
    partial class FormMain : Form
    {
        // Ordre de trie pour detecter le changement d'ordre
        private SortOrder Order;

        public FormMain()
        {
            InitializeComponent();
            InitializeTreeView();
            InitializeStripView();
            this.Order = SortOrder.Descending;
        }

                                        /** Fonction d'initialisation */
        /// <summary>
        /// Initialise le composant treeView1
        /// </summary>
        public void InitializeTreeView()
        {
            treeView1.Nodes.Clear();
            treeView1.BeginUpdate();

            // Initialise les Nodes 
            treeView1.Nodes.Add("Tous les articles");
            treeView1.Nodes.Add("Familles");
            treeView1.Nodes.Add("Marques");

            AddFamilles(treeView1.Nodes[1]);
            AddMarques(treeView1.Nodes[2]);

            treeView1.EndUpdate();

            UpdateStripView();
            UpdateListView();
        }

        /// <summary>
        /// Initialise la StripView
        /// </summary>
        public void InitializeStripView()
        {
            nbArticle_SSL.Text = "0";
            nbFamille_SSL.Text = "0";
            nbSF_SSL.Text = "0";
            nbMarque_SSL.Text = "0";
        }

        /// <summary>
        /// Ajoute les famille dans la treeView1
        /// </summary>
        /// <param name="node">Node "Familles"</param>
        public void AddFamilles(TreeNode node)
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
        /// Ajoute les marques dans la treeView1
        /// </summary>
        /// <param name="node">Node "Marques"</param>
        public void AddMarques(TreeNode node)
        {
            List<Marque> marques = MarqueDAO.GetAll();

            foreach (Marque marque in marques)
            {
                node.Nodes.Add(marque.Nom);
            }
        }

                                        /** Controleurs */

        /// <summary>
        /// Detecte le clique sur une node de la treeView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            UpdateListView();
        }

        /// <summary>
        /// Détecte le clique sur le bouton Importer de la ToolStripMenuItem
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
        /// Détecte le clique sur le bouton Exporter de la ToolStripMenuItem
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void exporterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormExporter formExport = new FormExporter();
            formExport.ShowDialog();
        }

        /// <summary>
        /// Détecte le clique sur le bouton Actualiser de la ToolStripMenuItem
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void actualiserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateListView();
        }

        /// <summary>
        /// Détecte les touches tapées dans la listView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    if (treeView1.SelectedNode.Level == 0)
                    {
                        // Si on est sur le premier niveau de la listView (Tous les articles / familles / Marques)
                        switch (treeView1.SelectedNode.Text)
                        {
                            case "Tous les articles":
                                ModifyArticle();
                                UpdateListView();
                                break;
                            case "Familles":
                                Modifyfamille();
                                UpdateListView();
                                break;
                            case "Marques":
                                ModifyMarque();
                                UpdateListView();
                                break;
                        }
                    }
                    else
                    {
                        // Si on est dans les Familles ou Marques
                        switch (treeView1.SelectedNode.Parent.Text)
                        {
                            case "Familles":
                                ModifySousFamille();
                                UpdateListView();
                                break;
                            default:
                                ModifyArticle();
                                UpdateListView();
                                break;
                        }
                    }
                    break;
                case Keys.F5:
                    UpdateListView();
                    break;
                case Keys.Delete:
                    RemoveArticle();
                    UpdateListView();
                    break;
            }
        }

        /// <summary>
        /// Détecte le double clique sur une item de la listView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if ((treeView1.SelectedNode.Level == 0) && (treeView1.SelectedNode.Parent == null))
            {
                // Si on est sur le premier niveau de la listView (Tous les articles / familles / Marques)
                switch (treeView1.SelectedNode.Text)
                {
                    case "Tous les articles":
                        ModifyArticle();
                        UpdateListView();
                        break;
                    case "Familles":
                        Modifyfamille();
                        UpdateListView();
                        break;
                    case "Marques":
                        ModifyMarque();
                        UpdateListView();
                        break;
                }
            }
            else
            {
                // Si on est dans les Familles ou Marques
                switch (treeView1.SelectedNode.Parent.Text)
                {
                    case "Familles":
                        ModifySousFamille();
                        UpdateListView();
                        break;
                    default:
                        ModifyArticle();
                        UpdateListView();
                        break;
                }
            }
        }

        /// <summary>
        /// Détecte le clique droit de la souris
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                FormContextMenu contextMenu = new FormContextMenu();
                Article article = ArticleDAO.GetByDescription(listView1.SelectedItems[0].Text);
                contextMenu.SaveArticle(article, this.StartPosition);
                contextMenu.ShowDialog();

                UpdateListView();
                InitializeTreeView();
            }

        }

        /// <summary>
        /// Détecte le click sur une colonne
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Inverse le sens du trie
            if (Order == SortOrder.Descending)
            {
                this.Order = SortOrder.Ascending;
            }
            else
            {
                this.Order = SortOrder.Descending;
            }

            if (e.Column == 3)
            {
                // Trie par Entier
                ListViewItemComparer ItemCompare = new ListViewItemComparer(e.Column, this.Order);
                listView1.ListViewItemSorter = ItemCompare;
            }
            else
            {
                // Trie par Ordre alphabétique
                ListViewItemComparer ItemCompare = new ListViewItemComparer(e.Column, this.Order);
                listView1.ListViewItemSorter = ItemCompare;
            }
        }

                                        /** Foction Update */
        /// <summary>
        /// Met à jour la StripView
        /// </summary>
        public void UpdateStripView()
        {
            nbArticle_SSL.Text = Convert.ToString(ArticleDAO.CountAllArticle());
            nbFamille_SSL.Text = Convert.ToString(FamilleDAO.CountAllFamille());
            nbSF_SSL.Text = Convert.ToString(SousFamilleDAO.CountAllSousFamille());
            nbMarque_SSL.Text = Convert.ToString(MarqueDAO.CountAllMarque());
        }

        /// <summary>
        /// Met à jour la listView1 pour afficher les articles/familles/SousFamilles/Marques
        /// </summary>
        public void UpdateListView()
        {
            listView1.Clear();
            List<Article> articles = null;

            // Initialisation des données 
            if (treeView1.SelectedNode == null)
            {
                articles = ArticleDAO.GetAll();
                UpdateListViewArticle(articles);
            }
            else
            {
                if ((treeView1.SelectedNode.Level == 0) && (treeView1.SelectedNode.Parent == null))
                {
                    // Affichage du de la treeView niveau 0 des Articles/Familles/Marques
                    switch (treeView1.SelectedNode.Text)
                    {
                        case "Tous les articles":
                            // Met a jour l'affichage des articles
                            articles = ArticleDAO.GetAll();
                            UpdateListViewArticle(articles);
                            break;
                        case "Familles":
                            // Met à jour l'affichage des Familles
                            UpdateListViewFamilles();
                            break;
                        case "Marques":
                            // Met à jour l'affichage des Marques
                            UpdateListViewMarques();
                            break;
                    }

                }
                else
                {
                    switch (treeView1.SelectedNode.Parent.Text)
                    {
                        case "Familles":
                            // Affichage de tous les articles d'une famille
                            UpdateListViewSousFamilles();
                            break;
                        case "Marques":
                            // Affichage de tous les articles d'une marque
                            Marque marque = MarqueDAO.GetWhereName(treeView1.SelectedNode.Text);

                            UpdateListViewArticleByMarque(marque);
                            break;
                        default:
                            // Affichage de tous les articles d'un sous familles
                            SousFamille sousFamille = SousFamilleDAO.GetWhereName(treeView1.SelectedNode.Text);

                            UpdateListViewArticleBySousFamille(sousFamille);
                            break;
                    }
                }
            }

            // Met à jour la StripView
            UpdateStripView();
        }

        /// <summary>
        /// Met à jour la listView pour les articles
        /// </summary>
        public void UpdateListViewArticle(List<Article> articles)
        {
            listView1.Name = "ArticleListe";

            listView1.Columns.Add("Description", 400, HorizontalAlignment.Center);
            listView1.Columns.Add("Sous-Familles", 200, HorizontalAlignment.Center);
            listView1.Columns.Add("Marque", 100, HorizontalAlignment.Center);
            listView1.Columns.Add("Quantité", 50, HorizontalAlignment.Center);

            //List<Article> articles = ArticleDAO.GetAll();

            if (articles.Count > 0)
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

        /// <summary>
        /// Met à jour la listView pour les Familles
        /// </summary>
        public void UpdateListViewFamilles()
        {
            listView1.Name = "FamilleList";

            listView1.Columns.Add("Description", -2, HorizontalAlignment.Center);

            List<Famille> familles = FamilleDAO.GetAll();

            if (familles.Count > 0)
            {
                foreach (Famille famille in familles)
                {
                    var row = new string[] { famille.Nom };

                    var lvi = new ListViewItem(row);
                    lvi.Tag = famille;
                    listView1.Items.Add(lvi);
                }
            }
        }

        /// <summary>
        /// Met à jour la listView pour les Marques
        /// </summary>
        public void UpdateListViewMarques()
        {
            listView1.Name = "MarquesList";

            listView1.Columns.Add("Description", -2, HorizontalAlignment.Center);

            List<Marque> marques = MarqueDAO.GetAll();

            if (marques.Count > 0)
            {
                foreach (Marque marque in marques)
                {
                    var row = new string[] { marque.Nom };

                    var lvi = new ListViewItem(row);
                    lvi.Tag = marque;
                    listView1.Items.Add(lvi);
                }
            }
        }

        /// <summary>
        /// Met à jour la listeView pour les SousFamilles
        /// </summary>
        public void UpdateListViewSousFamilles()
        {
            listView1.Name = "DescriptionSousFamilles";
            listView1.Columns.Add("Description", -2, HorizontalAlignment.Center);

            Famille famille = FamilleDAO.GetWhereName(treeView1.SelectedNode.Text);

            if (famille != null)
            {
                List<SousFamille> sousFamilles = SousFamilleDAO.GetWhereFamilleByRef(famille);

                if (sousFamilles.Count > 0)
                {

                    foreach (SousFamille sousFam in sousFamilles)
                    {
                        var row = new string[] { sousFam.Nom };

                        var lvi = new ListViewItem(row);
                        lvi.Tag = sousFam;
                        listView1.Items.Add(lvi);
                    }
                }
            }
        }

        /// <summary>
        /// Met à jour la listView avec les articles d'une Marque
        /// </summary>
        /// <param name="marque">Marque à afficher</param>
        public void UpdateListViewArticleByMarque(Marque marque)
        {
            List<Article> articles = ArticleDAO.GetByMarque(marque);
            UpdateListViewArticle(articles);
        }


        /// <summary>
        /// Met à jour la listeView des articles d'une SousMarque
        /// </summary>
        /// <param name="sousFamille">SousFamille à afficher</param>
        public void UpdateListViewArticleBySousFamille(SousFamille sousFamille)
        {
            List<Article> articles = ArticleDAO.GetArticleBySousFamille(sousFamille);
            UpdateListViewArticle(articles);
        }

                                        /** Fenetres modales */
        /// <summary>
        /// Ouvre une fentre modale de modification d'un article
        /// </summary>
        public void ModifyArticle()
        {
            // Crée un article avec la description sélectionné
            Article article = ArticleDAO.GetByDescription(listView1.SelectedItems[0].Text);

            FormModifArticle formModif = new FormModifArticle();
            formModif.InitializeDataComponent(article);
            formModif.ShowDialog();

            UpdateListView();
        }

        /// <summary>
        /// Ouvre une fentre modale de modification de famille
        /// </summary>
        public void Modifyfamille()
        {
            Famille famille = FamilleDAO.GetWhereName(listView1.SelectedItems[0].Text);

            FormModifFamilles formModif = new FormModifFamilles();
            formModif.InitializeDataComponent(famille);
            formModif.ShowDialog();

            UpdateListView();
        }

        /// <summary>
        /// Ouvre une fenetre modale de modification de SousFamille
        /// </summary>
        public void ModifySousFamille()
        {
            SousFamille sousFamille = SousFamilleDAO.GetWhereName(listView1.SelectedItems[0].Text);

            FormModifSousFamille formModif = new FormModifSousFamille();
            formModif.InitializeDataComponent(sousFamille);
            formModif.ShowDialog();

            UpdateListView();
        }

        /// <summary>
        /// Ouvre une fenetre modale de modification de Marque
        /// </summary>
        public void ModifyMarque()
        {
            Marque marque = MarqueDAO.GetWhereName(listView1.SelectedItems[0].Text);

            FormModifMarque formModif = new FormModifMarque();
            formModif.InitializeDataComponent(marque);
            formModif.ShowDialog();

            UpdateListView();
        }

                                        /** Autres */
        /// <summary>
        /// Supprimer l'article sélectionné
        /// Actualise automatiquement la listView car la ligne sélectionnée n'existe plus
        /// </summary>
        public void RemoveArticle()
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
            Article article = ArticleDAO.GetByDescription(descriptionItem.Text);
            if (article == null)
            {
                MessageBox.Show("L'article n'existe pas");
                return;
            }

            ArticleDAO.RemoveArticle(article);
        }

        /// <summary>
        /// Détecte quand on quitte la fenetre
        /// Pour sauvegarder les données
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Database.CloseDb();
        }
    }
}
