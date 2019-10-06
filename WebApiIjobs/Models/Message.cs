using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiIjobs.Models
{
    public class Message
    {//IdEvenement
        public string Telephone { get; set; }
        public string Email { get; set; }

        public string Titre_evenement { get; set; }
        public DateTime Date_evenement { get; set; }
        public string Heure_evenement { get; set; }
        public string Adresse { get; set; }
        public string Description { get; set; }
        //IdRappel
        public DateTime Date_alarme { get; set; }
        public string Heure_alarme { get; set; }
        public decimal Rap_tel { get; set; }
        public decimal Rap_courriel { get; set; }
        //IdCandidat
        public string Nom { get; set; }
        public string Prenom { get; set; }
        //Contact
        public string NomContact { get; set; }
        public string PrenomContact { get; set; }
   
    }
}
