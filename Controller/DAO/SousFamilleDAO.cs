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
        /// Ajoute une <b>SousFamille</b> dans la BDD
        /// </summary>
        /// <param name="sousFamille"><b>SousFamille</b> à ajouter</param>
        /// <returns>La référence de la <b>SousFamille</b></returns>
        public static int Insert(SousFamille sousFamille)
        {
            if (sousFamille != null)
            {
                // Vérifie si la Sous Famille existe déjà
                if( GetWhereName(sousFamille.Nom) != null)
                {
                    return 0;
                }

                int reference = sousFamille.RefFamille.Reference;
                if (sousFamille.RefFamille.Reference == 0)
                {
                    reference = GetFamilleRef(sousFamille);
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
        /// Recupère toutes les <b>SousFamilles</b> de la BDD
        /// </summary>
        /// <returns>Liste des <b>SousFamilles</b></returns>
        public static List<SousFamille> GetAll()
        {
            SQLiteDataReader sousFamille = Database.GetSql("select * from SousFamilles;");

            List<SousFamille> list = new List<SousFamille>();
            while (sousFamille.Read())
            {
                int reference = sousFamille.GetInt32(0);
                int referenceFamille = sousFamille.GetInt32(1);
                string name = sousFamille.GetString(2);

                Famille famille = FamilleDAO.GetWhereRef(referenceFamille);

                list.Add(new SousFamille(reference, famille, name));
            }

            return list;

        }

        /// <summary>
        /// Met à jour une <b>SousFamille</b> de la BDD
        /// </summary>
        /// <param name="sousFamille"><b>SousFamille</b> à mettre à jour</param>
        public static void UpdateSousFamille(SousFamille sousFamille)
        {
            Database.RunSql("update SousFamilles set " +
                "Nom='" + sousFamille.Nom + "', " +
                "RefFamille='" + sousFamille.RefFamille.Reference + "'" +
                "where RefSousFamille='" + sousFamille.RefSousFamille + "'" +
                ";");
        }

        /// <summary>
        /// Récupère une <b>SousFamille</b> par son nom dans la BDD
        /// </summary>
        /// <param name="name">Nom de la <b>SousFamille</b></param>
        /// <returns>La <b>SousFamille</b></returns>
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
        /// Récupère une <b>SousFamille</b> dans la BDD
        /// </summary>
        /// <param name="reference">La Référence de la <b>SousFamille</b></param>
        /// <returns>La <b>SousFamille</b></returns>
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
        /// Récupère la Référence d'une <b>SousFamille</b>
        /// </summary>
        /// <param name="sousFamille">La <b>SousFamille</b></param>
        /// <returns>La <b>SousFamille</b></returns>
        private static int GetFamilleRef(SousFamille sousFamille)
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
        /// Récupère toutes les <b>SousFamilles</b> d'une <b>Famille</b>
        /// </summary>
        /// <param name="famille">La <b>Famille</b></param>
        /// <returns>Liste des <b>SousFamilles</b></returns>
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

        /// <summary>
        /// Compte le nombre de <b>SousFamilles</b> dans la BDD
        /// </summary>
        /// <returns>Lombre de <b>SousFamilles</b></returns>
        public static int CountAllSousFamille()
        {
            SQLiteDataReader count = Database.GetSql("select count(*) from SousFamilles;");

            int res = 0;
            if( count.Read())
            {
                res = count.GetInt32(0);
            }

            return res;
        }
    }
}
