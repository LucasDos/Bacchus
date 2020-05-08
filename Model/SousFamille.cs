using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bacchus.Model
{
    class SousFamille
    {
        public int RefSousFamille { get; set; }
        public Famille RefFamille { get; set; }
        public string nom { get; set; }

        /// <summary>
        /// Constructeur par defaut
        /// </summary>
        public SousFamille()
        {
            this.RefSousFamille = 0;
            this.RefFamille = new Famille();
            this.nom = "";
        }

        /// <summary>
        /// Constructeur de recopie
        /// </summary>
        /// <param name="RefSousFamille"> Reference de la sous famille </param>
        /// <param name="RefFamille"> reference de la famille mere </param>
        /// <param name="Nom"> Nom de la famille </param>
        public SousFamille(int RefSousFamille, Famille RefFamille, string Nom)
        {
            this.RefSousFamille = RefSousFamille;
            this.RefFamille = RefFamille;
            this.nom = Nom;
        }
    }
}
