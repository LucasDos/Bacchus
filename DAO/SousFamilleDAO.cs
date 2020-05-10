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

        public static SousFamille GetWhereName(String name)
        {
            SQLiteDataReader sousFamille = Database.GetSql("select * from SousFamilles where Nom = '" + name + "';");

            if (sousFamille.Read())
            {
                return new SousFamille(sousFamille.GetInt32(0), new Famille(), sousFamille.GetString(2));
            }
            return null;
        }

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

        public static List<SousFamille> GetWhereFamille(int reference)
        {
            SQLiteDataReader sousFamille = Database.GetSql("select * from SousFamilles where RefFamille = '" + reference + "';");

            List<SousFamille> list = new List<SousFamille>();
            while (sousFamille.Read())
            {
                list.Add(new SousFamille(sousFamille.GetInt32(0), new Famille(), sousFamille.GetString(2)));
            }
            return list;
        }
    }
}
