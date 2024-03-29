﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiIjobs.Models;

namespace WebApiIjobs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RappelsController : ControllerBase
    {
        private readonly ijobsContext _context;

        public RappelsController(ijobsContext context)
        {
            _context = context;
        }


        // GET: api/Rappels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rappel>>> GetRappel()
        {
            return await _context.Rappel.ToListAsync();
        }


        // GET: api/Rappels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Rappel>> GetRappel(int id)
        {
            var rappel = await _context.Rappel.FindAsync(id);

            if (rappel == null)
            {
                return NotFound();
            }

            return rappel;
        }

 
        // Avec join ou san join faite on a le meme resultat
        // GET: api/Rappels/Evenement/1
        [HttpGet("Evenement/{idEvenement}")]
        public async Task<ActionResult<Rappel>> GetRappelByEvenement(int idEvenement)
        {
            var rappel = await (from r in _context.Rappel
                                join e in _context.Evenement on r.IdEvenement equals e.IdEvenement into r_e
                                where r.IdEvenement == idEvenement
                                select new Rappel()
                                {
                                    IdRappel = r.IdRappel,
                                    IdEvenement = r.IdEvenement,
                                    DateRappel = r.DateRappel,
                                    TelRappel = r.TelRappel,
                                    CourrielRappel = r.CourrielRappel
                                })
                                .ToListAsync();

            if (rappel == null)
            {
                return NotFound();
            }

            return new ObjectResult(rappel);
        }

        //Il ne affiche pas l'heure pour le rest c bonne
        // GET: api/Rappels/Candidat/1
        [HttpGet("Candidat/{idCandidat}")]
        public async Task<ActionResult<Rappel>> GetRappelByCandidat(int idCandidat)
        {
            var rappel = await (from r in _context.Rappel
                                join e in _context.Evenement on r.IdEvenement equals e.IdEvenement into r_e
                                from re in r_e.DefaultIfEmpty()
                                join c in _context.Candidat  on re.IdCandidat equals c.IdCandidat
                                where c.IdCandidat == idCandidat
                                select new Rappel()
                                {
                                    IdRappel = r.IdRappel,
                                    IdEvenement = r.IdEvenement,
                                    DateRappel = r.DateRappel,
                                    TelRappel = r.TelRappel,
                                    CourrielRappel = r.CourrielRappel
                                })
                                .ToListAsync();

            if (rappel == null)
            {
                return NotFound();
            }

            return new ObjectResult(rappel);
        }

        // PUT: api/Rappels/1
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRappel(int id, Rappel rappel)
        {
            if (id != rappel.IdRappel)
            {
                return BadRequest();
            }

            _context.Entry(rappel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RappelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Rappels
        [HttpPost]
        public async Task<ActionResult<Rappel>> PostRappel(Rappel rappel)
        {
            _context.Rappel.Add(rappel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRappel", new { id = rappel.IdRappel }, rappel);
        }

        // DELETE: api/Rappels/5
        // La methode verifie dabords si le id rappels exise , si cest le cas  delete send true sinon send false
        [HttpDelete("{id}")]
        public async Task<ActionResult<Boolean>> DeleteRappel(int id)
        {
            if (RappelExists(id))
            {
                var rappel = await _context.Rappel.FindAsync(id);
                _context.Rappel.Remove(rappel);
                await _context.SaveChangesAsync();
                return true;
            } else
            {
                return NotFound();
            }
                      
      
        }


        [HttpGet("Message/{dateRappel}/{heure}")]
        public async Task<IEnumerable<Message>> GetMessageByDateHrPeriode(DateTime dateRappel, string heure)
        {
            var message = await (from ra in _context.Rappel
                                 join ev in _context.Evenement on ra.IdEvenement equals ev.IdEvenement into ra_ev
                                 from raev in ra_ev.DefaultIfEmpty()
                                 join ca in _context.Candidat on raev.IdCandidat equals ca.IdCandidat into ca_raev
                                 from caraev in ca_raev.DefaultIfEmpty()
                                 join co in _context.Contact on raev.IdContact equals co.IdContact into co_raev
                                 from coraev in co_raev.DefaultIfEmpty()
                                 where ((ra.DateRappel == dateRappel) &&
                                        (ra.HeureRappel == heure))
                                 select new Message()
                                 {
                                     Telephone = caraev.Tel,
                                     Email = caraev.Courriel,
                                     Titre_evenement = raev.Titre,
                                     Date_evenement = raev.DateEvent,
                                     Heure_evenement = raev.Heure,
                                     Adresse = raev.Adresse,
                                     Description = raev.Descr,
                                     Date_alarme = ra.DateRappel,
                                     Heure_alarme = ra.HeureRappel,
                                     Rap_tel = ra.TelRappel,
                                     Rap_courriel = ra.CourrielRappel,
                                     Nom = caraev.NomCandidat,
                                     Prenom = caraev.PrenomCandidat,
                                     NomContact = coraev.NomContact,
                                     PrenomContact = coraev.PrenomContact
                                 }).ToListAsync();
                return message;
        }


        private bool RappelExists(int id)
        {
            return _context.Rappel.Any(e => e.IdRappel == id);
        }
    }
}
