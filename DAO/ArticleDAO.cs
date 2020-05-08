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
        public static int Insert(Article article)
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

                Database.RunSql("insert into Article(Description, RefSousFamille, RefMarque, PrixHT, Quantite) values('" + article.Description + "', " + sousFamilleReference + ", " + marqueReference + ", " + article.Prix + ", " + article.Quantite + ");");

                SQLiteDataReader added = Database.GetSql("select max(RefArticle) from Article;");

                if (added.Read())
                {
                    return added.GetInt32(0);
                }
            }
            return 0;
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
