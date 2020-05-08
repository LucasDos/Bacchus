using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bacchus.Model;

namespace Bacchus
{
    class ModelBacchus
    {
        public List<Article> Articles { get; set; }
        public List<Famille> Familles { get; set; }
        public List<Marque> Marques { get; set; }
        public List<SousFamille> SousFamilles { get; set; }

        /// <summary>
        /// Constructeur par defaut
        /// </summary>
        public ModelBacchus()
        {
            this.Articles = new List<Article>();
            this.Familles = new List<Famille>();
            this.Marques = new List<Marque>();  
            this.SousFamilles = new List<SousFamille>();
        }
    }
}
