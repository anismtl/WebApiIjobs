using System;
using System.Collections.Generic;

namespace WebApiIjobs.Models
{
    public partial class Offre
    {
        public Offre()
        {
            Favoris = new HashSet<Favoris>();
        }

        public string IdOffre { get; set; }
        public string Titre { get; set; }
        public string Companie { get; set; }
        public string Location { get; set; }
        public DateTime? DateOffre { get; set; }
        public string Descr { get; set; }
        public string Url { get; set; }

        public virtual ICollection<Favoris> Favoris { get; set; }
    }
}
