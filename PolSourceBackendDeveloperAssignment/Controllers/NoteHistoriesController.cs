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
    public class NoteHistoriesController : ControllerBase
    {
        private readonly NotesContext _context;

        public NoteHistoriesController(NotesContext context)
        {
            _context = context;
        }

        // GET: api/NoteHistories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NoteHistory>>> GetNoteHistories()
        {
            return await _context.NoteHistories.ToListAsync();
        }

        // GET: api/NoteHistories/5
        [HttpGet("{id}")]
        public async Task<IEnumerable<NoteHistory>> GetNoteHistory(int id)
        {

            var noteHistory = await _context.NoteHistories.Where(nh => nh.IdNote == id).ToListAsync();
            return noteHistory;
        }
    }
}
