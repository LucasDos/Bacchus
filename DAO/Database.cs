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
        private static String fileName = "data/data.csv";

        public static void GetInstance()
        {
            if(db == null)
            {
                db = new SQLiteConnection($"Filename={fileName}");
                db.Open();
            }
        }

        public static SQLiteDataReader GetSql(String sql)
        {
            if(db == null)
            {
                GetInstance();
            }
            SQLiteCommand selectCommand = new SQLiteCommand(sql, db);

            return selectCommand.ExecuteReader();
        }

        public static void RunSql(String sql)
        {
            if (db == null)
            {
                GetInstance();
            }
            SQLiteCommand command = new SQLiteCommand(sql, db);
            command.ExecuteReader();
        }

        /// <summary>
        /// Ferme la connexion a la BDD
        /// </summary>
        public static void CloseNewConnexion()
        {
            db.Close();
        }
    }
}
