using Bacchus.Model;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bacchus.DAO
{
    class MarqueDAO
    {
        /// <summary>
        /// Récupère toutes les Marques de la BDD
        /// </summary>
        /// <returns>Liste des Marques</returns>
        public static List<Marque> GetAll()
        {
            SQLiteDataReader marque = Database.GetSql("select * from Marques;");

            List<Marque> list = new List<Marque>();
            while (marque.Read())
            {
                list.Add(new Marque(marque.GetInt32(0), marque.GetString(1)));
            }
            return list;
        }

        /// <summary>
        /// Ajoute une Marque dans la BDD
        /// </summary>
        /// <param name="marque">Marque à ajouter</param>
        /// <returns>La référence de la marque</returns>
        public static int Insert(Marque marque)
        {
            if(marque != null)
            {
                Database.RunSql("insert into Marques('Nom') values('" + marque.Nom + "');");
                SQLiteDataReader added = Database.GetSql("select max(RefMarque) from Marques;");

                if (added.Read())
                {
                    return added.GetInt32(0);
                }
            }
            return 0;
        }

        /// <summary>
        /// Met à jour une marque dans la BDD
        /// </summary>
        /// <param name="marque">Marque à mettre à jour</param>
        public static void UpdateMarque(Marque marque)
        {
            Database.RunSql("update Marque set" +
                "Nom='" + marque.Nom + "'" +
                "where RefMarque='" + marque.Reference + "'" +
                ";");
        }

        /// <summary>
        /// Récupère une Marque avec son nom
        /// </summary>
        /// <param name="name">Nom de la marque</param>
        /// <returns>La Marque</returns>
        public static Marque GetWhereName(String name)
        {
            SQLiteDataReader marque = Database.GetSql("select * from Marques where Nom = '" + name + "';");

            if(marque.Read())
            {
                return new Marque(marque.GetInt32(0) , marque.GetString(1));
            }
            return null;
        }

        /// <summary>
        /// Récupère une Marque avec sa référence
        /// </summary>
        /// <param name="reference">La Référence de la Marque</param>
        /// <returns>La Marque</returns>
        public static Marque GetWhereRef(int reference)
        {
            SQLiteDataReader marque = Database.GetSql("select * from Marques where RefMarque = '" + reference + "';");

            if (marque.Read())
            {
                return new Marque(marque.GetInt32(0), marque.GetString(1));
            }
            return null;
        }

        /// <summary>
        /// Recupère le nombre de Marque dans la BDD
        /// </summary>
        /// <returns>Le nombre de Marque</returns>
        public static int CountAllMarque()
        {
            SQLiteDataReader count = Database.GetSql("select count(*) from Marques;");

            int res = 0;
            if( count.Read())
            {
                res = count.GetInt32(0);
            }

            return res;
        }
    }
}
