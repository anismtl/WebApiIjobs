using System;
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
    public class EvenementsController : ControllerBase
    {
        private readonly ijobsContext _context;

        public EvenementsController(ijobsContext context)
        {
            _context = context;
        }

        //Test ok
        // GET: api/Evenements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Evenement>>> GetEvenement()
        {
            return await _context.Evenement.ToListAsync();
        }

        //Test ok
        // GET: api/Evenements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Evenement>> GetEvenement(int id)
        {
            var evenement = await _context.Evenement.FindAsync(id);

            if (evenement == null)
            {
                return NotFound();
            }

            return evenement;
        }

        //Test ok
        // GET: api/Evenements/Candidat/1
        [HttpGet("Candidat/{idCandidat}")]
        public async Task<ActionResult<Evenement>> GetEvenementByCandidat(int idCandidat)
        {
            var evenement = await (from e in _context.Evenement
                                   join c in _context.Candidat on e.IdCandidat equals c.IdCandidat into e_c
                                   where e.IdCandidat == idCandidat
                                   select new Evenement()
                                   {
                                       IdEvenement = e.IdEvenement,
                                       IdCandidat = e.IdCandidat,
                                       IdOffre = e.IdOffre,
                                       IdContact = e.IdContact,
                                       Titre = e.Titre,
                                       DateEvent = e.DateEvent,
                                       Heure = e.Heure,
                                       Adresse = e.Adresse,
                                       Descr = e.Descr
                                   })
                                .ToListAsync();

            if (evenement == null)
            {
                return NotFound();
            }

            return new ObjectResult(evenement);
        }

        //Test Yuni
        //Cette methode produit le même resultat que la methode precedent
        //Dans la methode precedent on fait un jointure mais le resultat obtenu ici ne diffère pas 
                

        //// GET: api/Evenements/Candidat/1
        //[HttpGet("Candidat/{idCandidat}")]
        //public async Task<ActionResult<Evenement>> GetEvenementByCandidat(int idCandidat)
        //{
        //    var evenement = await (from e in _context.Evenement
        //                           where e.IdCandidat == idCandidat
        //                           select new Evenement()
        //                           {
        //                               IdEvenement = e.IdEvenement,
        //                               IdCandidat = e.IdCandidat,
        //                               IdOffre = e.IdOffre,
        //                               IdContact = e.IdContact,
        //                               Titre = e.Titre,
        //                               DateEvent = e.DateEvent,
        //                               Heure = e.Heure,
        //                               Adresse = e.Adresse,
        //                               Descr = e.Descr
        //                           })
        //                        .ToListAsync();

        //    if (evenement == null)
        //    {
        //        return NotFound();
        //    }

        //    return new ObjectResult(evenement);
        //}


        //Test ok
        [HttpGet("CandidatOffre/{idCandidat}/{idOffre}")]
        public async Task<ActionResult<Evenement>> GetEvenementByCandidatOffre(int idCandidat, string idOffre)
        {
            var evenement = await (from e in _context.Evenement
                                   where ((e.IdCandidat == idCandidat)&&(e.IdOffre == idOffre))
                                   select new Evenement()
                                   {
                                       IdEvenement = e.IdEvenement,
                                       IdCandidat = e.IdCandidat,
                                       IdOffre = e.IdOffre,
                                       IdContact = e.IdContact,
                                       Titre = e.Titre,
                                       DateEvent = e.DateEvent,
                                       Heure = e.Heure,
                                       Adresse = e.Adresse,
                                       Descr = e.Descr
                                   })
                                .ToListAsync();

            if (evenement == null)
            {
                return NotFound();
            }

            return new ObjectResult(evenement);
        }


        //Test ok
        // PUT: api/Evenements/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvenement(int id, Evenement evenement)
        {
            if (id != evenement.IdEvenement)
            {
                return BadRequest();
            }

            _context.Entry(evenement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EvenementExists(id))
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


        //Test ok
        // POST: api/Evenements
        [HttpPost]
        public async Task<ActionResult<Evenement>> PostEvenement(Evenement evenement)
        {
            _context.Evenement.Add(evenement);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEvenement", new { id = evenement.IdEvenement }, evenement);
        }


        //Test ok 
        // DELETE: api/Evenements/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Evenement>> DeleteEvenement(int id)
        {
            var evenement = await _context.Evenement.FindAsync(id);
            if (evenement == null)
            {
                return NotFound();
            }

            _context.Evenement.Remove(evenement);
            await _context.SaveChangesAsync();

            return evenement;
        }

        private bool EvenementExists(int id)
        {
            return _context.Evenement.Any(e => e.IdEvenement == id);
        }
    }
}
