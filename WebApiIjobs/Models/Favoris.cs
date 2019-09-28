using System;
using System.Collections.Generic;

namespace WebApiIjobs.Models
{
    public partial class Favoris
    {
        public Favoris()
        {
            Evenement = new HashSet<Evenement>();
        }

        public int IdCandidat { get; set; }
        public string IdOffre { get; set; }
        public DateTime DateFavoris { get; set; }
        public decimal Postule { get; set; }

        public virtual Candidat IdCandidatNavigation { get; set; }
        public virtual Offre IdOffreNavigation { get; set; }
        public virtual ICollection<Evenement> Evenement { get; set; }
    }
}
