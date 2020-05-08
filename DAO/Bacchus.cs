using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bacchus.DAO
{
    class Bacchus
    {
        private static SQLiteConnection NewConnexion;

        /// <summary>
        /// Ouvre une connexion a la BDD
        /// </summary>
        /// <param name="FileName"> Chemin d'acces a la BDD </param>
        public static void OpenNewConnexion(String FileName)
        {
            NewConnexion = new SQLiteConnection("Data Source = " + FileName);
        }

        /// <summary>
        /// Ferme la connexion a la BDD
        /// </summary>
        public static void CloseNewConnexion()
        {
            NewConnexion.Close();
        }
    }
}
