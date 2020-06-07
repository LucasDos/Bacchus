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
        public static String Insert(Article article)
        {
            if (article != null)
            {
                // We verify if sousFamille have an id
                // If not we search the id corresponding by the name
                // Else we create it
                int sousFamilleReference = article.SousFamille.RefSousFamille;
                if (article.SousFamille.RefSousFamille == 0)
                {
                    sousFamilleReference = getSousFamilleRef(article);
                }

                // We verify if marque have an id
                // If not we search the id corresponding by the name
                // Else we create it
                int marqueReference = article.Marque.Reference;
                if (article.Marque.Reference == 0)
                {
                    marqueReference = getMarqueRef(article);
                }

                SQLiteDataReader exists = Database.GetSql("select * from Articles where RefArticle = '" + article.Reference + "';");

                if(!exists.Read())
                {
                    Database.RunSql("insert into Articles('RefArticle', 'Description', 'RefSousFamille', 'RefMarque', 'PrixHT', 'Quantite') values('" + article.Reference + "', '" + article.Description + "', " + sousFamilleReference + ", " + marqueReference + ", " + Convert.ToInt32(article.Prix) + ", " + article.Quantite + ");");
                }
                else
                {
                    Database.RunSql("update Articles set Description = '" + article.Description + "', RefSousFamille = " + sousFamilleReference + ", RefMarque = " + marqueReference + ", PrixHT = " + Convert.ToInt32(article.Prix) + ", Quantite = " + article.Quantite + " where RefArticle = '" + article.Reference + "';");
                }

                return article.Reference;
            }
            return null;
        }

        public static List<Article> GetAll()
        {
            SQLiteDataReader article = Database.GetSql("select * from Article;");

            List<Article> list = new List<Article>();
            while (article.Read())
            {
                string reference = article.GetString(0);
                string desc = article.GetString(1);
                int refSFam = article.GetInt32(2);
                int refMarque = article.GetInt32(3);
                float prix = article.GetFloat(4);
                int quantite = article.GetInt32(5);

                SousFamille sfam = SousFamilleDAO.GetWhereRef(refSFam);
                Marque marque = MarqueDAO.GetWhereRef(refMarque);

                list.Add(new Article(reference, desc, sfam, marque, prix, quantite));
            }

            return list;
        }
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
    }
}
