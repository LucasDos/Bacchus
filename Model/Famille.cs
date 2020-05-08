using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bacchus.Model
{
    class Famille
    {
        public int Reference { get; set; }
        public string Nom { get; set; }

        /// <summary>
        /// Constructeur par defaut
        /// </summary>
        public Famille()
        {
            this.Reference = 0;
            this.Nom = "";
        }

        /// <summary>
        /// Constructeur par recopie
        /// </summary>
        /// <param name="Reference"> Reference de la famille </param>
        /// <param name="Nom"> Nom de la famille </param>
        public Famille(int Reference, string Nom)
        {
            this.Reference = Reference;
            this.Nom = Nom;
        }
    }
}
