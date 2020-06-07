using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bacchus.Model
{
    class Article
    {
        public string Reference { get; set; }
        public string Description { get; set; }
        public Famille Famille { get; set; }
        public SousFamille SousFamille { get; set; }
        public Marque Marque { get; set; }
        public double Prix { get; set; }
        public int Quantite { get; set; }

        /// <summary>
        /// Constructeur par defaut
        /// </summary>
        public Article()
        {
            this.Reference = "";
            this.Description = "";
            this.SousFamille = new SousFamille();
            this.Marque = new Marque();
            this.Prix = 0.0;
            this.Quantite = 0;
        }

        /// <summary>
        /// Constructeur de recopie
        /// </summary>
        /// <param name="Reference"> reference de l'article </param>
        /// <param name="Description"> description de l'article </param>
        /// <param name="SousFamille"> sousfamille de l'article </param>
        /// <param name="Marque"> marque de l'article </param>
        /// <param name="Prix"> prix de l'article </param>
        /// <param name="Quantite"> quantite de l'article </param>
        public Article(string Reference, string Description, SousFamille SousFamille, Marque Marque, double Prix, int Quantite)
        {
            this.Reference = Reference;
            this.Description = Description;
            this.SousFamille = SousFamille;
            this.Marque = Marque;
            this.Prix = Prix;
            this.Quantite = Quantite;
        }
    }
}
