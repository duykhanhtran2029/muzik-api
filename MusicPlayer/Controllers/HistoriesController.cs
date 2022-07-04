using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Database.MusicPlayer.Models;

namespace MusicPlayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoriesController : ControllerBase
    {
        private readonly MusicPlayerDbContext _context;

        public HistoriesController(MusicPlayerDbContext context)
        {
            _context = context;
        }

        // GET: api/Histories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<History>>> GetHistory()
        {
            return await _context.History.ToListAsync();
        }

        // GET: api/Histories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<History>> GetHistory(string id)
        {
            var history = await _context.History.FindAsync(id);

            if (history == null)
            {
                return NotFound();
            }

            return history;
        }

        // PUT: api/Histories/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut]
        public async Task<IActionResult> PutHistory([FromBody] History data)
        {

            History history = await _context.History.FirstOrDefaultAsync(s => s.UserId == data.UserId && s.SongId == data.SongId);
            if (history == null)
            {
                data.Count = 1;
                _context.History.Add(data);
            }
            else
            {
                history.Count += 1;
                _context.Entry(history).State = EntityState.Modified;

            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw;
            }



            return NoContent();
        }


        private bool HistoryExists(string id)
        {
            return _context.History.Any(e => e.UserId == id);
        }
    }
}
