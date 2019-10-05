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
    public class ContactsController : ControllerBase
    {
        private readonly ijobsContext _context;

        public ContactsController(ijobsContext context)
        {
            _context = context;
        }

        // GET: api/Contacts/{idCandidat}
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contact>>> GetContacts()
        {
            return await _context.Contact.ToListAsync();
        }



       
        // GET: api/Contacts/Candidat/1
        [HttpGet("candidat/{idCandidat}")]
        public async Task<ActionResult<Contact>> GetContactsByCandidat(int idCandidat)
        { 
            var contact = await (from c in _context.Contact
                                 where c.IdCandidat == idCandidat
                                 select new Contact()
                                 {
                                     IdContact = c.IdContact,
                                     IdCandidat = c.IdCandidat,
                                     NomContact = c.NomContact,
                                     PrenomContact = c.PrenomContact,
                                     CourrielContact = c.CourrielContact,
                                     Poste = c.Poste,
                                     TelContact = c.TelContact
                                 })
                                .ToListAsync();
            if (contact == null)
            {
                return NotFound();
            }
            
            return new ObjectResult(contact);
        }



    
        // GET: api/Contacts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Contact>> GetContactById(int id)
        {
            var contact = await _context.Contact.FindAsync(id);

            if (contact == null)
            {
                return NotFound();
            }

            return contact;
        }

    
        // PUT: api/Contacts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContact(int id, Contact contact)
        {
            if (id != contact.IdContact)
            {
                return BadRequest();
            }

            _context.Entry(contact).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactExists(id))
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

   
        // POST: api/Contacts
        [HttpPost]
        public async Task<ActionResult<Contact>> AjouterContact(Contact contact)
        {
            _context.Contact.Add(contact);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContact", new { id = contact.IdContact }, contact);
        }

      
        // DELETE: api/Contacts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Contact>> DeleteContact(int id)
        {
            var contact = await _context.Contact.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }

            _context.Contact.Remove(contact);
            await _context.SaveChangesAsync();

            return contact;
        }

        private bool ContactExists(int id)
        {
            return _context.Contact.Any(e => e.IdContact == id);
        }
    }
}
