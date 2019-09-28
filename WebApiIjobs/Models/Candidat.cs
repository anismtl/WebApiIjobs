using System;
using System.Collections.Generic;

namespace WebApiIjobs.Models
{
    public partial class Candidat
    {
        public Candidat()
        {
            Contact = new HashSet<Contact>();
            Favoris = new HashSet<Favoris>();
        }

        public int IdCandidat { get; set; }
        public string NomCandidat { get; set; }
        public string PrenomCandidat { get; set; }
        public string Courriel { get; set; }
        public string MotPasse { get; set; }
        public string Tel { get; set; }
        public string Statut { get; set; }

        public virtual ICollection<Contact> Contact { get; set; }
        public virtual ICollection<Favoris> Favoris { get; set; }
    }
}
