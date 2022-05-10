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
    public class GenreSongsController : ControllerBase
    {
        private readonly MusicPlayerDbContext _context;

        public GenreSongsController(MusicPlayerDbContext context)
        {
            _context = context;
        }

        // GET: api/GenreSongs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenreSong>>> GetGenreSong()
        {
            return await _context.GenreSong.ToListAsync();
        }

        // GET: api/GenreSongs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GenreSong>> GetGenreSong(string id)
        {
            var genreSong = await _context.GenreSong.FindAsync(id);

            if (genreSong == null)
            {
                return NotFound();
            }

            return genreSong;
        }

        // PUT: api/GenreSongs/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGenreSong(string id, GenreSong genreSong)
        {
            if (id != genreSong.GenreId)
            {
                return BadRequest();
            }

            _context.Entry(genreSong).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GenreSongExists(id))
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

        // POST: api/GenreSongs
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<GenreSong>> PostGenreSong(GenreSong genreSong)
        {
            _context.GenreSong.Add(genreSong);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (GenreSongExists(genreSong.GenreId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetGenreSong", new { id = genreSong.GenreId }, genreSong);
        }

        // DELETE: api/GenreSongs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<GenreSong>> DeleteGenreSong(string id)
        {
            var genreSong = await _context.GenreSong.FindAsync(id);
            if (genreSong == null)
            {
                return NotFound();
            }

            _context.GenreSong.Remove(genreSong);
            await _context.SaveChangesAsync();

            return genreSong;
        }

        private bool GenreSongExists(string id)
        {
            return _context.GenreSong.Any(e => e.GenreId == id);
        }
    }
}
