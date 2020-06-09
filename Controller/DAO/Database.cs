using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bacchus.DAO
{
    class Database
    {
        private static SQLiteConnection db = null;
        private static String fileName = "Bacchus.SQLite";
        private static String[] tables = new String[]{"Articles", "Familles", "Marques", "SousFamilles"};

        /// <summary>
        /// Recupère l'instance de la BDD
        /// </summary>
        public static void GetInstance()
        {
            if(db == null)
            {
                db = new SQLiteConnection($"Data Source={fileName}");
                db.Open();
            }
        }

        /// <summary>
        /// Envoi une requete SQL et récupère les données
        /// </summary>
        /// <param name="sql"></param>
        /// <returns>Données de la requete SQL</returns>
        public static SQLiteDataReader GetSql(String sql)
        {
            if(db == null)
            {
                GetInstance();
            }
            SQLiteCommand selectCommand = new SQLiteCommand(sql, db);

            return selectCommand.ExecuteReader();
        }

        /// <summary>
        /// Ennvoi une requete SQLsasrecupération de données
        /// </summary>
        /// <param name="sql"></param>
        public static void RunSql(String sql)
        {
            if (db == null)
            {
                GetInstance();
            }
            SQLiteCommand command = new SQLiteCommand(sql, db);
            command.ExecuteNonQuery();
        }

        public static void Empty()
        {
            foreach (String table in tables)
            {
                RunSql("delete from '" + table + "';");
                RunSql("UPDATE SQLITE_SEQUENCE SET SEQ=0 WHERE NAME='" + table + "';");
            }
        }

        /// <summary>
        /// Ferme la connexion a la BDD
        /// </summary>
        public static void CloseDb()
        {
            db.Close();
        }
    }
}
