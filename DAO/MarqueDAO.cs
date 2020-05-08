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
        public static int Insert(Marque marque)
        {
            if(marque != null)
            {
                Database.RunSql("insert into Marques(Nom) values('" + marque.Nom + "');");
                SQLiteDataReader added = Database.GetSql("select max(RefMarque) from Marques;");

                if (added.Read())
                {
                    return added.GetInt32(0);
                }
            }
            return 0;
        }

        public static Marque GetWhereName(String name)
        {
            SQLiteDataReader marque = Database.GetSql("select * from Marques where Nom = " + name);

            if(marque.Read())
            {
                return new Marque(marque.GetInt32(0) , marque.GetString(1));
            }
            return null;
        }
    }
}
