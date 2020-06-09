using Bacchus.Model;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bacchus.DAO
{
    class ArticleDAO
    {
        /// <summary>
        /// Insert un Artcile dans la BDD
        /// </summary>
        /// <param name="article">Article à ajouter</param>
        /// <returns>La référence de l'article</returns>
        public static String Insert(Article article)
        {
            if (article != null)
            {
                // On vérifie si SousFamille à un ID
                // Si non on recherche par nom
                // Sinon on le crée
                int sousFamilleReference = article.SousFamille.RefSousFamille;
                if (article.SousFamille.RefSousFamille == 0)
                {
                    sousFamilleReference = getSousFamilleRef(article);
                }

                // On vérifie si Marque a un ID
                // Si non on recherche par nom
                // Sinon on le crée
                int marqueReference = article.Marque.Reference;
                if (article.Marque.Reference == 0)
                {
                    marqueReference = getMarqueRef(article);
                }

                SQLiteDataReader exists = Database.GetSql("select * from Articles where RefArticle = '" + article.Reference + "';");

                if(!exists.Read())
                {
                    Database.RunSql("insert into Articles('RefArticle', 'Description', 'RefSousFamille', 'RefMarque', 'PrixHT', 'Quantite') values('" + article.Reference + "', '" + article.Description + "', '" + sousFamilleReference + "', '" + marqueReference + "', '" + article.Prix + "', '" + article.Quantite + "');");
                }
                else
                {
                    Database.RunSql("update Articles set Description = '" + article.Description + "', RefSousFamille = " + sousFamilleReference + ", RefMarque = " + marqueReference + ", PrixHT = " + Convert.ToInt32(article.Prix) + ", Quantite = " + article.Quantite + " where RefArticle = '" + article.Reference + "';");
                }

                return article.Reference;
            }
            return null;
        }

        /// <summary>
        /// Récupère tous les articles de la BDD
        /// </summary>
        /// <returns>Liste de tous les Articles</returns>
        public static List<Article> GetAll()
        {
            SQLiteDataReader article = Database.GetSql("select * from Articles;");

            List<Article> list = new List<Article>();
            while (article.Read())
            {
                // Récupère chaque attribut de l'abjet article
                string reference = article.GetString(0);
                string desc = article.GetString(1);
                int refSFam = article.GetInt32(2);
                int refMarque = article.GetInt32(3);
                // float prix = article.GetFloat(4); Ne fonctionne pas 
                float prix = Single.Parse(article.GetString(4));
                int quantite = article.GetInt32(5);

                SousFamille sfam = SousFamilleDAO.GetWhereRef(refSFam);
                Marque marque = MarqueDAO.GetWhereRef(refMarque);

                // Ajoute un objet Article dans la list
                list.Add(new Article(reference, desc, sfam, marque, prix, quantite));
            }

            return list;
        }
        /// <summary>
        /// Recupère la Référence d'une SousFamille d'un article
        /// </summary>
        /// <param name="article">Article à chercher</param>
        /// <returns>La référence de la famille de l'article</returns>
        public static int getSousFamilleRef(Article article)
        {
            SousFamille sousFamille = SousFamilleDAO.GetWhereName(article.SousFamille.Nom);

            // Si une famille avec ce nom existe on retourne sa ref
            if (sousFamille != null)
            {
                return sousFamille.RefSousFamille;
            }
            else // Sinon on la créée
            {
                return SousFamilleDAO.Insert(article.SousFamille);
            }
        }

        /// <summary>
        /// Récupère la Référence d'une marque d'un article
        /// </summary>
        /// <param name="article">Article à chercher</param>
        /// <returns>La référence de la marque del'article</returns>
        public static int getMarqueRef(Article article)
        {
            Marque marque = MarqueDAO.GetWhereName(article.Marque.Nom);

            // Si une famille avec ce nom existe on retourne sa ref
            if (marque != null)
            {
                return marque.Reference;
            }
            else // Sinon on la créée
            {
                return MarqueDAO.Insert(article.Marque);
            }
        }

        /// <summary>
        /// Récupère les articles d'une SousFamille
        /// </summary>
        /// <param name="sf">SousFamille à trouver</param>
        /// <returns>Liste des Articles de cette SousFamille</returns>
        public static List<Article> getArticleBySousFamille(SousFamille sf)
        {
            SQLiteDataReader article = Database.GetSql("select * from Articles where RefSousFamille='" + sf.RefSousFamille + "';");

            List<Article> list = new List<Article>();
            while (article.Read())
            {
                string reference = article.GetString(0);
                string desc = article.GetString(1);
                int refMarque = article.GetInt32(3);
                float prix = Single.Parse(article.GetString(4));
                int quantite = article.GetInt32(5);

                Marque marque = MarqueDAO.GetWhereRef(refMarque);

                list.Add(new Article(reference, desc, sf, marque, prix, quantite));
            }

            return list;
        }

        /// <summary>
        /// récupère les Articles d'une Marque
        /// </summary>
        /// <param name="marque">Marque à chercher</param>
        /// <returns>Liste des articles de cette Marque</returns>
        public static List<Article> getByMarque(Marque marque)
        {
            SQLiteDataReader article = Database.GetSql("select * from Articles where RefMarque='" + marque.Reference + "';");

            List<Article> list = new List<Article>();
            while (article.Read())
            {
                string reference = article.GetString(0);
                string desc = article.GetString(1);
                int refSousFamille = article.GetInt32(2);
                float prix = Single.Parse(article.GetString(4));
                int quantite = article.GetInt32(5);
                SousFamille sousFamille = SousFamilleDAO.GetWhereRef(refSousFamille);

                list.Add(new Article(reference, desc, sousFamille, marque, prix, quantite));
            }
            return list;
        }
        
        public static Article getByDescription(string descritpion)
        {
            SQLiteDataReader article = Database.GetSql("select * from Articles where Description='" + descritpion + "';");

            if(article.Read())
            {
                string reference = article.GetString(0);
                string desc = article.GetString(1);
                int refSousFamille = article.GetInt32(2);
                int refMarque = article.GetInt32(3);
                float prix = Single.Parse(article.GetString(4));
                int quantite = article.GetInt32(5);

                SousFamille sousFamille = SousFamilleDAO.GetWhereRef(refSousFamille);
                Marque marque = MarqueDAO.GetWhereRef(refMarque);

                return new Article(reference, desc, sousFamille, marque, prix, quantite);
            }

            return null;

        }
    
        public static void removeArticle(Article article)
        {
            SQLiteDataReader res = Database.GetSql("delete from Articles where RefArticle='" + article.Reference + "';");
        }

        /// <summary>
        /// Modifie un article existant
        /// </summary>
        /// <param name="article">Article à modifier</param>
        public static void modifyArticle(Article article)
        {
            // Bug
            Database.RunSql("update Articles set " +
                "Description='" + article.Description + "', " +
                "RefSousFamille='" + article.SousFamille.RefSousFamille + "', " +
                "RefMarque='" + article.Marque.Reference + "', " +
                "PrixHT='" + article.Prix + "', " +
                "Quantite='" + article.Quantite + "'" +
                "where RefArticle='" + article.Reference + "'" +
                ";");

        }
    }
}
