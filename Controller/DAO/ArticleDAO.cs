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
        /// Insert un <b>Artcile</b> dans la BDD
        /// </summary>
        /// <param name="article"><b>Article</b> à ajouter</param>
        /// <returns>La référence de l'<b>Article</b></returns>
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
                    sousFamilleReference = GetSousFamilleRef(article);
                }

                // On vérifie si Marque a un ID
                // Si non on recherche par nom
                // Sinon on le crée
                int marqueReference = article.Marque.Reference;
                if (article.Marque.Reference == 0)
                {
                    marqueReference = GetMarqueRef(article);
                }

                // Converti le float au format universel ( "9.005")
                string prix = Convert.ToString(article.Prix);
                prix = prix.Replace(",", ".");

                

                SQLiteDataReader exists = Database.GetSql("select * from Articles where RefArticle = '" + article.Reference + "';");

                if(exists.Read())
                {
                    Database.RunSql("update Articles set Description = '" + article.Description + "', RefSousFamille = '" + sousFamilleReference + "', RefMarque = '" + marqueReference + "', PrixHT = '" + prix + "', Quantite = '" + article.Quantite + "' where RefArticle = '" + article.Reference + "';");
                }
                else
                {
                    Database.RunSql("insert into Articles('RefArticle', 'Description', 'RefSousFamille', 'RefMarque', 'PrixHT', 'Quantite') values('" + article.Reference + "', '" + article.Description + "', '" + sousFamilleReference + "', '" + marqueReference + "', '" + prix + "', '" + article.Quantite + "');");
                }

                return article.Reference;
            }
            return null;
        }

        /// <summary>
        /// Récupère tous les <b>Articles</b> de la BDD
        /// </summary>
        /// <returns>Liste de tous les <b>Articles</b></returns>
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
                float prix = article.GetFloat(4);
                int quantite = article.GetInt32(5);

                SousFamille sfam = SousFamilleDAO.GetWhereRef(refSFam);
                Marque marque = MarqueDAO.GetWhereRef(refMarque);

                // Ajoute un objet Article dans la list
                list.Add(new Article(reference, desc, sfam, marque, prix, quantite));
            }

            return list;
        }

        /// <summary>
        /// Récupère la Référence d'une <b>SousFamille</b> d'un <b>Article</b> dans la BDD
        /// </summary>
        /// <param name="article"><b>Article</b> à chercher</param>
        /// <returns>La référence de la <b>SousFamille</b> de l'<b>Article</b></returns>
        public static int GetSousFamilleRef(Article article)
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
        /// Récupère la Référence d'une <b>Marque</b> d'un <b>Article</b> dans la BDD
        /// </summary>
        /// <param name="article"><b>Article</b> à chercher</param>
        /// <returns>La référence de la <b>Marque</b> de l'<b>Article</b></returns>
        public static int GetMarqueRef(Article article)
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
        /// Récupère les <b>Articles</b> d'une <b>SousFamille</b> dans la BDD
        /// </summary>
        /// <param name="sf"><b>SousFamille</b> à trouver</param>
        /// <returns>Liste des <b>Articles</b> de cette <b>SousFamille</b></returns>
        public static List<Article> GetArticleBySousFamille(SousFamille sf)
        {
            SQLiteDataReader article = Database.GetSql("select * from Articles where RefSousFamille='" + sf.RefSousFamille + "';");

            List<Article> list = new List<Article>();
            while (article.Read())
            {
                string reference = article.GetString(0);
                string desc = article.GetString(1);
                int refMarque = article.GetInt32(3);
                float prix = article.GetFloat(4);
                int quantite = article.GetInt32(5);

                Marque marque = MarqueDAO.GetWhereRef(refMarque);

                list.Add(new Article(reference, desc, sf, marque, prix, quantite));
            }

            return list;
        }

        /// <summary>
        /// Récupère les <b>Articles</b> d'une <b>Marque</b> dans la BDD
        /// </summary>
        /// <param name="marque"><b>Marque</b> à chercher</param>
        /// <returns>Liste des <b>Articles/b> de cette <b>Marque</b></returns>
        public static List<Article> GetByMarque(Marque marque)
        {
            SQLiteDataReader article = Database.GetSql("select * from Articles where RefMarque='" + marque.Reference + "';");

            List<Article> list = new List<Article>();
            while (article.Read())
            {
                string reference = article.GetString(0);
                string desc = article.GetString(1);
                int refSousFamille = article.GetInt32(2);
                float prix = article.GetFloat(4);
                int quantite = article.GetInt32(5);
                SousFamille sousFamille = SousFamilleDAO.GetWhereRef(refSousFamille);

                list.Add(new Article(reference, desc, sousFamille, marque, prix, quantite));
            }
            return list;
        }

        /// <summary>
        /// Récupère un <b>Article</b> avec sa description dans la BDD
        /// </summary>
        /// <param name="descritpion">Description de l'<b>Article</b></param>
        /// <returns>L'<b>Article</b></returns>
        public static Article GetByDescription(string descritpion)
        {
            SQLiteDataReader article = Database.GetSql("select * from Articles where Description='" + descritpion + "';");

            if(article.Read())
            {
                string reference = article.GetString(0);
                string desc = article.GetString(1);
                int refSousFamille = article.GetInt32(2);
                int refMarque = article.GetInt32(3);
                float prix = article.GetFloat(4);
                int quantite = article.GetInt32(5);

                SousFamille sousFamille = SousFamilleDAO.GetWhereRef(refSousFamille);
                Marque marque = MarqueDAO.GetWhereRef(refMarque);

                return new Article(reference, desc, sousFamille, marque, prix, quantite);
            }

            return null;

        }

        /// <summary>
        /// Supprime un <b>Article</b> de la BDD
        /// </summary>
        /// <param name="article">Article à supprimer</param>
        public static void RemoveArticle(Article article)
        {
            SQLiteDataReader res = Database.GetSql("delete from Articles where RefArticle='" + article.Reference + "';");
        }

        /// <summary>
        /// Recupère le nombre d'<b>Artcile</b> dans la BDD
        /// </summary>
        /// <returns>Nombre d'<b>Article</b></returns>
        public static int CountAllArticle()
        {
            SQLiteDataReader count = Database.GetSql("select count(*) from Articles;");
            int res = 0;

            if (count.Read())
                res = count.GetInt32(0);

            return res;
        }
    }
}
