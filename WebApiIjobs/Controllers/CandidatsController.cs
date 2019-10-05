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
    public class CandidatsController : ControllerBase
    {
        private readonly ijobsContext _context;

        public CandidatsController(ijobsContext context)
        {
            _context = context;
        }

       
        // GET: api/Candidats
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Candidat>>> GetCandidat()
        {
            return await _context.Candidat.ToListAsync();
        }

      
        // GET: api/Candidats/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Candidat>> GetCandidat(int id)
        {
            var candidat = await _context.Candidat.FindAsync(id);

            if (candidat == null)
            {
                return NotFound();
            }

            return candidat;
        }


   
        // PUT: api/Candidats/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCandidat(int id, Candidat candidat)
        {
            if (id != candidat.IdCandidat)
            {
                return BadRequest();
            }

            _context.Entry(candidat).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CandidatExists(id))
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

     
        // POST: api/Candidats
        [HttpPost]
        public async Task<ActionResult<Candidat>> CreateCandidat(Candidat candidat)
        {
            _context.Candidat.Add(candidat);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCandidat", new { id = candidat.IdCandidat }, candidat);
        }


        // PUT: api/Candidats/disable/5
        [HttpPut("disable/{id}")]
        public async Task<ActionResult<Boolean>> DisableCandidat(int id)
        {
            var candidat = await _context.Candidat.FindAsync(id);
            if (candidat == null)
            {
                return NotFound();
            }
            candidat.Statut = "disactive";
            
            await _context.SaveChangesAsync();

            return true;
        }


        [HttpGet("authentifier/{user}/{pass}", Name = "IsAuthentified")]
        public async Task<ActionResult<Candidat>> IsAuthentified(string user, string pass)
        {

            var result = await (from c in _context.Candidat
                                where (c.Courriel == user) && (c.MotPasse == pass)
                                select c).FirstOrDefaultAsync();
            if (result == null)
            {
                return NotFound();
            }

            return new ObjectResult(result);

        }

        [HttpGet("fav/{id}", Name = "GetFavorisById")]
        public async Task<ActionResult<CandidatFavoris>> GetFavorisById(int id)
        {
            var list = await (from c in _context.Candidat
                              join ord in _context.Favoris on c.IdCandidat equals ord.IdCandidat into c_o
                              from t in c_o.DefaultIfEmpty()
                              join off in _context.Offre on t.IdOffre equals off.IdOffre into f_o
                              from o in f_o.DefaultIfEmpty()
                              where t.IdCandidat == id
                              select new CandidatFavoris()
                              {
                                  Titre = o.Titre,
                                  Companie = o.Companie,
                                  Location = o.Location,
                                  Date_offre = o.DateOffre.ToString(),
                                  Descr = o.Descr,
                                  Url = o.Url,
                                  Postule = t.Postule,
                                  Date_favoris = t.DateFavoris.ToShortDateString()
                              }).
                       ToListAsync();

            if (list == null)
            {
                return NotFound();
            }

            return new ObjectResult(list);
            
        }

        private bool CandidatExists(int id)
        {
            return _context.Candidat.Any(e => e.IdCandidat == id);
        }
    }
}
