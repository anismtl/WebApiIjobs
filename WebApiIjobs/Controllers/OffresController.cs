using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiIjobs.Models;

namespace WebApiIjobs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCorsAttribute("http://localhost:65251", "*", "*")]
    public class OffresController : ControllerBase
    {
        private readonly ijobsContext _context;

        public OffresController(ijobsContext context)
        {
            _context = context;
        }

        //Teste ok
        // GET: api/Offres
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Offre>>> GetOffre()
        {
            return await _context.Offre.ToListAsync();
        }


        //Teste ok
        // GET: api/Offres/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Offre>> GetOffre(string id)
        {
            var offre = await _context.Offre.FindAsync(id);

            if (offre == null)
            {
                return NotFound();
            }

            return offre;
        }

        //// PUT: api/Offres/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutOffre(string id, Offre offre)
        //{
        //    if (id != offre.IdOffre)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(offre).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!OffreExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Offres

        [HttpPost]
        public async Task<ActionResult<Offre>> PostOffre(Offre offre)
        {
            _context.Offre.Add(offre);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (OffreExists(offre.IdOffre))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetOffre", new { id = offre.IdOffre }, offre);
        }

        //// DELETE: api/Offres/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Offre>> DeleteOffre(string id)
        //{
        //    var offre = await _context.Offre.FindAsync(id);
        //    if (offre == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Offre.Remove(offre);
        //    await _context.SaveChangesAsync();

        //    return offre;
        //}

        private bool OffreExists(string id)
        {
            return _context.Offre.Any(e => e.IdOffre == id);
        }
    }
}
