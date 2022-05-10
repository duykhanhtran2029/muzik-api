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
    public class ArtistSongsController : ControllerBase
    {
        private readonly MusicPlayerDbContext _context;

        public ArtistSongsController(MusicPlayerDbContext context)
        {
            _context = context;
        }

        // GET: api/ArtistSongs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArtistSong>>> GetArtistSong()
        {
            return await _context.ArtistSong.ToListAsync();
        }

        // GET: api/ArtistSongs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ArtistSong>> GetArtistSong(string id)
        {
            var artistSong = await _context.ArtistSong.FindAsync(id);

            if (artistSong == null)
            {
                return NotFound();
            }

            return artistSong;
        }

        // PUT: api/ArtistSongs/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArtistSong(string id, ArtistSong artistSong)
        {
            if (id != artistSong.ArtistId)
            {
                return BadRequest();
            }

            _context.Entry(artistSong).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArtistSongExists(id))
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

        // POST: api/ArtistSongs
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ArtistSong>> PostArtistSong(ArtistSong artistSong)
        {
            _context.ArtistSong.Add(artistSong);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ArtistSongExists(artistSong.ArtistId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetArtistSong", new { id = artistSong.ArtistId }, artistSong);
        }

        // DELETE: api/ArtistSongs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ArtistSong>> DeleteArtistSong(string id)
        {
            var artistSong = await _context.ArtistSong.FindAsync(id);
            if (artistSong == null)
            {
                return NotFound();
            }

            _context.ArtistSong.Remove(artistSong);
            await _context.SaveChangesAsync();

            return artistSong;
        }

        private bool ArtistSongExists(string id)
        {
            return _context.ArtistSong.Any(e => e.ArtistId == id);
        }
    }
}
