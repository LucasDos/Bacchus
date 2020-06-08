using Bacchus.Model;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bacchus.DAO
{
    class SousFamilleDAO
    {
        /// <summary>
        /// Ajoute une SousFamille dans la BDD
        /// </summary>
        /// <param name="sousFamille">SousFamille à ajouter</param>
        /// <returns>La référence de la SousFamille</returns>
        public static int Insert(SousFamille sousFamille)
        {
            if (sousFamille != null)
            {
                int reference = sousFamille.RefFamille.Reference;
                if (sousFamille.RefFamille.Reference == 0)
                {
                    reference = getFamilleRef(sousFamille);
                }

                Database.RunSql("insert into SousFamilles('RefFamille', 'Nom') values(" + reference + ", '" + sousFamille.Nom + "');");

                SQLiteDataReader added = Database.GetSql("select max(RefSousFamille) from SousFamilles;");

                if (added.Read())
                {
                    return added.GetInt32(0);
                }
            }
            return 0;
        }

        /// <summary>
        /// Récupère une SousFamille par son nom
        /// </summary>
        /// <param name="name">Nom de la SousFamille</param>
        /// <returns>La SousFamille</returns>
        public static SousFamille GetWhereName(String name)
        {
            SQLiteDataReader sousFamille = Database.GetSql("select * from SousFamilles where Nom = '" + name + "';");

            if (sousFamille.Read())
            {
                return new SousFamille(sousFamille.GetInt32(0), new Famille(), sousFamille.GetString(2));
            }
            return null;
        }

        /// <summary>
        /// Récupère une SousFamille
        /// </summary>
        /// <param name="reference">La Référence de la SousFamille</param>
        /// <returns>La SousFamille</returns>
        public static SousFamille GetWhereRef(int reference)
        {
            SQLiteDataReader sousFamille = Database.GetSql("select * from SousFamilles where RefSousFamille = '" + reference + "';");

            if (sousFamille.Read())
            {
                Famille fam = FamilleDAO.GetWhereRef(sousFamille.GetInt32(1));
                return new SousFamille(sousFamille.GetInt32(0), fam, sousFamille.GetString(2));
            }
            return null;
        }

        /// <summary>
        /// Récupère la Référence d'une SousFamille
        /// </summary>
        /// <param name="sousFamille">La SousFamille</param>
        /// <returns>La SousFamille</returns>
        private static int getFamilleRef(SousFamille sousFamille)
        {
            Famille famille = FamilleDAO.GetWhereName(sousFamille.RefFamille.Nom);

            // Si une famille avec ce nom existe on retourne sa ref
            if(famille != null)
            {
                return famille.Reference;
            }
            else // Sinon on la créée
            {
                return FamilleDAO.Insert(sousFamille.RefFamille);
            }
        }

        /// <summary>
        /// Récupère toutes les SousFamilles d'une Famille
        /// </summary>
        /// <param name="famille">La Famille</param>
        /// <returns>Liste des SousFamilles</returns>
        public static List<SousFamille> GetWhereFamilleByRef(Famille famille)
        {
            SQLiteDataReader sousFamille = Database.GetSql("select * from SousFamilles where RefFamille = '" + famille.Reference + "';");

            List<SousFamille> list = new List<SousFamille>();
            while (sousFamille.Read())
            {
                list.Add(new SousFamille(sousFamille.GetInt32(0), famille, sousFamille.GetString(2)));
            }
            return list;
        }

        
    }
}
