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
        public static int Insert(Famille famille)
        {
            if (famille != null)
            {
                Database.RunSql("insert into Famille(Nom) values('" + famille.Nom + "');");
                SQLiteDataReader added = Database.GetSql("select max(RefFamille) from Familles;");

                if (added.Read())
                {
                    return added.GetInt32(0);
                }
            }
            return 0;
        }

        public static Famille GetWhereName(String name)
        {
            SQLiteDataReader famille = Database.GetSql("select * from Marques where Nom = " + name);

            if (famille.Read())
            {
                return new Famille(famille.GetInt32(0), famille.GetString(1));
            }
            return null;
        }
    }
}
