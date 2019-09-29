using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiIjobs.Models
{
    public class Message
    {//IdEvenement
        public string TitreEvent { get; set; }
        public DateTime DateEvent { get; set; }
        public string HeureEvent { get; set; }
        public string AdresseEvent { get; set; }
        public string DescrEvent { get; set; }
        //IdRappel
        public DateTime DateRappel { get; set; }
        public string HeureRappel { get; set; }
        public decimal TelRappel { get; set; }
        public decimal CourrielRappel { get; set; }
        //IdCandidat
        public string NomCandidat { get; set; }
        public string PrenomCandidat { get; set; }
        //Offre
        public string TitreOffre { get; set; }
        public string CompanieOffre { get; set; }
        //Contact
        public string NomContact { get; set; }
        public string PrenomContact { get; set; }
        public string CourrielContact { get; set; }
        public string Poste { get; set; }
        public string TelContact { get; set; }
    }
}
