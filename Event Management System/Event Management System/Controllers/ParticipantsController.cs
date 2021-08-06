using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Event_Management_System.Data;
using Event_Management_System.Models;


namespace Event_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantsController : ControllerBase
    {
        private ApplicationDbContext _context;

        public ParticipantsController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        [Route("byBirthDate")]

        public IActionResult ListParticipants(DateTime BirthDate)
        {
            var participants = _context.Participants.Where
            (
                bd => bd.BirthDate == BirthDate

            )
            .ToList<Participant>();

            return new JsonResult(participants);
        }

        [HttpGet]
        [Route("byGender")]

        public IActionResult ListParticipants(String Gender)
        {
            var genders = _context.Participants.Where
            (
                g => g.Gender == Gender

            )
            .ToList<Participant>();

            return new JsonResult(genders);
        }

        [HttpGet]
        [Route("bylastnameoremailaddressorbirthdate")]

        public IActionResult ListParticipants(string LastName, string Email, DateTime BirthDate)
        {
            var participants = _context.Participants.Where
            (
                p => p.LastName == LastName || p.EmailAddress == Email || p.BirthDate == BirthDate

            )
            .ToList<Participant>();

            return new JsonResult(participants);
        }

        [HttpPost]
        public async Task<ActionResult<Participant>> PostParticipants(Participant participant)
        {
            _context.Participants.Add(participant);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Participant), new { id = participant.ParticipantId }, participant);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutParticipants(int id, Participant Participant)
        {
            if (id != Participant.ParticipantId)
            {
                return BadRequest();
            }

            _context.Entry(Participant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Participant(int id)
        {
            var RemoveParticipant = await _context.Participants.FindAsync(id);
            if (RemoveParticipant == null)
            {
                return NotFound();
            }

            _context.Participants.Remove(RemoveParticipant);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
