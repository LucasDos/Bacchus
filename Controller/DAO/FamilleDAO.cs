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
        /// Récupère toutes les <b>Familles</b> de la BDD
        /// </summary>
        /// <returns>Liste des <b>Familles</b></returns>
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
        /// Ajoute une <b>Famille</b> dans la BDD
        /// </summary>
        /// <param name="famille"><b>Famille</b> à ajouter</param>
        /// <returns>La référence de la <b>Famille</b></returns>
        public static int Insert(Famille famille)
        {
            if (famille != null)
            {
                // Vérifie si la Famille exist
                if ( GetWhereName(famille.Nom) != null)
                {
                    return 0;
                }
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
        /// Récupère une <b>Famille</b> avec le Nom
        /// </summary>
        /// <param name="name">Nom de la <b>Famille</b> à chercher</param>
        /// <returns>La <b>Famille</b></returns>
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
        /// Récupère la <b>Famille</b> avec la Référence
        /// </summary>
        /// <param name="reference">Référence de la <b>Famille</b> à chercher</param>
        /// <returns>La <b>Famille</b></returns>
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
        /// Compte le nombre de <b>Famille</b> dans la BDD
        /// </summary>
        /// <returns>Le nombre de <b>Famille</b></returns>
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
