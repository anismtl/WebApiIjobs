using System;
using System.Collections.Generic;

namespace WebApiIjobs
{
    public partial class Evenement
    {
        public Evenement()
        {
            Rappel = new HashSet<Rappel>();
        }

        public int IdEvenement { get; set; }
        public int? IdCandidat { get; set; }
        public string IdOffre { get; set; }
        public int? IdContact { get; set; }
        public string Titre { get; set; }
        public DateTime DateEvent { get; set; }
        public string Heure { get; set; }
        public string Adresse { get; set; }
        public string Descr { get; set; }

        public virtual Favoris Id { get; set; }
        public virtual Contact IdContactNavigation { get; set; }
        public virtual ICollection<Rappel> Rappel { get; set; }
    }
}
