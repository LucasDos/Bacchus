using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bacchus.Model
{
    class Marque
    {
        public int Reference { get; set; }
        public string Nom { get; set; }

        /// <summary>
        /// Constructeur par defaut
        /// </summary>
        public Marque()
        {
            this.Reference = 0;
            this.Nom = "";
        }

        /// <summary>
        /// Constructeur de recopie
        /// </summary>
        /// <param name="Reference"> Reference de la marque </param>
        /// <param name="Nom"> Nom de la marque </param>
        public Marque(int Reference, string Nom)
        {
            this.Reference = Reference;
            this.Nom = Nom;
        }
    }
}
