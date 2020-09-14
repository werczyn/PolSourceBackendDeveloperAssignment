using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PolSourceBackendDeveloperAssignment.Models;

namespace PolSourceBackendDeveloperAssignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly NotesContext _context;

        public NotesController(NotesContext context)
        {
            _context = context;
        }

        // GET: api/Notes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Note>>> GetNotes()
        {
            return await _context.Notes.ToListAsync();
        }

        // GET: api/Notes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Note>> GetNote(int id)
        {
            var note = await _context.Notes.FindAsync(id);

            if (note == null)
            {
                return NotFound();
            }

            return note;
        }

        // PUT: api/Notes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNote(int id, Note note)
        {
            if (id != note.IdNote)
            {
                return BadRequest();
            }
            if (!NoteExists(id) || !NoteHistoryExists(id))
            {
                return NotFound();
            }


            try
            {
                var version = await _context.NoteHistories.Where(n => n.IdNote == id).MaxAsync(n => n.Version);
                var noteHistory = await _context.NoteHistories.FindAsync(id, version);
                ++version;
                _context.NoteHistories.Add(new NoteHistory { IdNote = note.IdNote, Version = version, Title = note.Title, Content = note.Content, Created = noteHistory.Created });
                await _context.SaveChangesAsync();

                noteHistory = await _context.NoteHistories.FindAsync(id, version);

                note.Created = noteHistory.Created;
                note.Modified = DateTime.Now;
                _context.Entry(note).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NoteExists(id))
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

        // POST: api/Notes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Note>> PostNote(Note note)
        {
            if (note.Title == null || note.Content == null)
            {
                return BadRequest();
            }


            Note tmpNote = new Note { Title = note.Title, Content = note.Content };

            _context.Notes.Add(tmpNote);
            await _context.SaveChangesAsync();

            _context.NoteHistories.Add(new NoteHistory { IdNote = tmpNote.IdNote ,Version = 1, Title = tmpNote.Title, Content = tmpNote.Content, Created = tmpNote.Created , Modified = tmpNote.Modified});
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNote", new { id = note.IdNote }, tmpNote);
        }

        // DELETE: api/Notes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Note>> DeleteNote(int id)
        {
            var note = await _context.Notes.FindAsync(id);
            if (note == null)
            {
                return NotFound();
            }

            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();

            return note;
        }

        private bool NoteExists(int id)
        {
            return _context.Notes.Any(e => e.IdNote == id);
        }

        private bool NoteHistoryExists(int id)
        {
            return _context.NoteHistories.Any(n => n.IdNote == id);
        }

    }
}
