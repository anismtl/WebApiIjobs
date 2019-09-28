using System;
using System.Collections.Generic;

namespace WebApiIjobs
{
    public partial class Contact
    {
        public Contact()
        {
            Evenement = new HashSet<Evenement>();
        }

        public int IdContact { get; set; }
        public int? IdCandidat { get; set; }
        public string NomContact { get; set; }
        public string PrenomContact { get; set; }
        public string CourrielContact { get; set; }
        public string Poste { get; set; }
        public string TelContact { get; set; }

        public virtual Candidat IdCandidatNavigation { get; set; }
        public virtual ICollection<Evenement> Evenement { get; set; }
    }
}
