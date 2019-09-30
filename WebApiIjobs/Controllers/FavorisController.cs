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
    public class FavorisController : ControllerBase
    {
        private readonly ijobsContext _context;

        public FavorisController(ijobsContext context)
        {
            _context = context;
        }


        //Teste ok
        // GET: api/Favoris
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Favoris>>> GetFavoris()
        {
            return await _context.Favoris.ToListAsync();
        }

        //Teste ok
        // GET: api/Favoris/1/1272242463
        [HttpGet("{idCandidat}/{idOffre}")]
        public async Task<ActionResult<Favoris>> GetFavoris(int idCandidat, string idOffre)
        {
            var favoris = await _context.Favoris.FindAsync(idCandidat, idOffre);

            if (favoris == null)
            {
                return NotFound();
            }

            return favoris;
        }

        //Teste ok
        // PUT: api/Favoris/postuler/1/1272242463
        [HttpPut("postuler/{idCandidat}/{idOffre}")]
        public async Task<ActionResult<Boolean>> PostulerFavoris(int idCandidat, string idOffre)
        {
            var favoris = await _context.Favoris.FindAsync(idCandidat, idOffre);
            if (favoris == null)
            {
                return NotFound();
            }
            favoris.Postule = 1;
            
            await _context.SaveChangesAsync();

            return true;
        }


        //Teste Ok
        // POST: api/Favoris
        [HttpPost]
        public async Task<ActionResult<Favoris>> PostFavoris(Favoris favoris)
        {
            _context.Favoris.Add(favoris);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FavorisExists(favoris.IdCandidat, favoris.IdOffre))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetFavoris", new { id = favoris.IdCandidat }, favoris);
        }

        //Pas reussi Test
        // DELETE: api/Favoris/1/1272242463
        [HttpDelete("{idCandidat}/{idOffre}")]
        public async Task<ActionResult<Favoris>> DeleteFavoris(int idCandidat, string idOffre)
        {
            var favoris = await _context.Favoris.FindAsync(idCandidat, idOffre);
            if (favoris == null)
            {
                return NotFound();
            }

            _context.Favoris.Remove(favoris);
            await _context.SaveChangesAsync();

            return favoris;
        }

        private bool FavorisExists(int idCandidat, string idOffre)
        {
            return _context.Favoris.Any(e => ((e.IdCandidat == idCandidat)&&(e.IdOffre.Equals(idOffre))));
        }
    }
}
