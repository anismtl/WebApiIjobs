using System;
using System.Collections.Generic;

namespace WebApiIjobs.Models

{
    public partial class Rappel
    {
        public int IdRappel { get; set; }
        public int? IdEvenement { get; set; }
        public DateTime DateRappel { get; set; }
        public string HeureRappel { get; set; }
        public decimal TelRappel { get; set; }
        public decimal CourrielRappel { get; set; }

        public virtual Evenement IdEvenementNavigation { get; set; }
    }
}
