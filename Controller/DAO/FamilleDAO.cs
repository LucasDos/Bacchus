using Bacchus.Model;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bacchus.DAO
{
    class FamilleDAO
    {
         /// <summary>
         /// Récupère toutes les Familles de la BDD
         /// </summary>
         /// <returns>Liste des Familles</returns>
        public static List<Famille> GetAll()
        {
            SQLiteDataReader famille = Database.GetSql("select * from Familles;");

            List<Famille> list = new List<Famille>();
            while (famille.Read())
            {
                Famille familleObj = new Famille(famille.GetInt32(0), famille.GetString(1));
                list.Add(familleObj);
            }
            return list;
        }

        /// <summary>
        /// Ajoute une famille dans la BDD
        /// </summary>
        /// <param name="famille">Famille à ajouter</param>
        /// <returns>La référence de la Famille</returns>
        public static int Insert(Famille famille)
        {
            if (famille != null)
            {
                Database.RunSql("insert into Familles('Nom') values('" + famille.Nom + "');");
                SQLiteDataReader added = Database.GetSql("select max(RefFamille) from Familles;");

                if (added.Read())
                {
                    return added.GetInt32(0);
                }
            }
            return 0;
        }

        /// <summary>
        /// Met a jour la famille dans le BDD
        /// </summary>
        /// <param name="famille">Famille à mettre à jour</param>
        public static void UpdateFamille(Famille famille)
        {
            Database.RunSql("update Familles set " +
                "Nom='" + famille.Nom + "'" +
                "where RefFamille='" + famille.Reference + "'" +
                ";");
        }

        /// <summary>
        /// Récupère une Famille avec le Nom
        /// </summary>
        /// <param name="name">Nom de la Famille à chercher</param>
        /// <returns>La Famille</returns>
        public static Famille GetWhereName(String name)
        {
            SQLiteDataReader famille = Database.GetSql("select * from Familles where Nom = '" + name + "';");

            if (famille.Read())
            {
                return new Famille(famille.GetInt32(0), famille.GetString(1));
            }
            return null;
        }

        /// <summary>
        /// Récupère la Famille avec la Référence
        /// </summary>
        /// <param name="reference">Référence de la Famille à chercher</param>
        /// <returns>La Famille</returns>
        public static Famille GetWhereRef(int reference)
        {
            SQLiteDataReader famille = Database.GetSql("select * from Familles where RefFamille = '" + reference + "';");

            if (famille.Read())
            {
                return new Famille(famille.GetInt32(0), famille.GetString(1));
            }
            return null;
        }

        /// <summary>
        /// Compte le nombre de Famille dans la BDD
        /// </summary>
        /// <returns>Le nombre de Famille</returns>
        public static int CountAllFamille()
        {
            SQLiteDataReader count = Database.GetSql("select count(*) from Familles;");

            int res = 0;
            if (count.Read())
            {
                res = count.GetInt32(0);
            }

            return res;
        }
    }
}
