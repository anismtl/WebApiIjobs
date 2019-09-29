using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiIjobs.Models;

namespace WebApiIjobs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly ijobsContext _context;

        public MessagesController(ijobsContext context)
        {
            _context = context;
        }

        // GET: api/Message
        [HttpGet("Rappels/{dateRappel}/{periode}")]
        public List<Message> GetMessageByDateHrPeriode(DateTime dateRappel, int periode)
        {
            var message =       (from ra in _context.Rappel
                                 join ev in _context.Evenement on ra.IdEvenement equals ev.IdEvenement into ra_ev
                                 from raev in ra_ev.DefaultIfEmpty()
                                 join ca in _context.Candidat on raev.IdCandidat equals ca.IdCandidat into ca_raev
                                 from caraev in ca_raev.DefaultIfEmpty()
                                 //join co in _context.Contact on raev.IdContact equals co.IdContact into co_raev
                                 //from coraev in ca_raev.DefaultIfEmpty()
                                 where ((ra.DateRappel >= dateRappel) &&
                                        (ra.DateRappel <= dateRappel.AddMinutes(periode)))
                                 select new Message()
                                 {
                                     TitreEvent = raev.Titre,
                                     DateEvent = raev.DateEvent,
                                     HeureEvent = raev.Heure,
                                     AdresseEvent = raev.Adresse,
                                     DescrEvent = raev.Descr,
                                     DateRappel = ra.DateRappel,
                                     HeureRappel = ra.HeureRappel,
                                     TelRappel = ra.TelRappel,
                                     CourrielRappel = ra.CourrielRappel,
                                     NomCandidat = caraev.NomCandidat,
                                     PrenomCandidat = caraev.PrenomCandidat,
                                     //TitreOffre = of.TitreOffre,
                                     //CompanieOffre = of.CompanieOffre,
                                     //NomContact = co.NomContact,
                                     //PrenomContact = co.PrenomContact,
                                     //CourrielContact = co.CourrielContact,
                                     //Poste = co.Poste,
                                     //TelContact = co.TelContact
                                 });
            return message.ToList();
        }
    }
}