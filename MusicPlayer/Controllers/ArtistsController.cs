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
    public class ArtistsController : ControllerBase
    {
        private readonly MusicPlayerDbContext _context;

        public ArtistsController(MusicPlayerDbContext context)
        {
            _context = context;
        }

        // GET: api/Artists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Artist>>> GetArtist()
        {
            return await _context.Artist.ToListAsync();
        }

        // GET: api/Artists/trending
        [HttpGet("trending")]
        public async Task<ActionResult<IEnumerable<Artist>>> GetTrendingArtist()
        {
            return await _context.Artist
                .OrderByDescending(a => a.ArtistSong.Sum(
                                    artistSong => artistSong.Song.Listens + artistSong.Song.Downloads + artistSong.Song.Likes))
                .Take(10)
                .ToListAsync();
        }

        // GET: api/Artists/id/songs
        [HttpGet("{id}/songs")]
        public async Task<ActionResult<IEnumerable<Song>>> GetSongsByArtistId(string id)
        {
            var artist = await _context.Artist.FindAsync(id);

            if (artist == null)
            {
                return NotFound();
            }
            var songs = await _context.ArtistSong
                .Where(artistSong => artistSong.ArtistId == id)
                .Select(artSong => artSong.Song).ToListAsync();
            if (!songs.Any())
            {
                return NoContent();
            }
            return songs;
        }

        // GET: api/Artists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Artist>> GetArtist(string id)
        {
            var artist = await _context.Artist.FindAsync(id);

            if (artist == null)
            {
                return NotFound();
            }

            return artist;
        }

        // PUT: api/Artists/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArtist(string id, Artist artist)
        {
            if (id != artist.ArtistId)
            {
                return BadRequest();
            }

            _context.Entry(artist).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArtistExists(id))
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

        // POST: api/Artists
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Artist>> PostArtist(Artist artist)
        {
            _context.Artist.Add(artist);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ArtistExists(artist.ArtistId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetArtist", new { id = artist.ArtistId }, artist);
        }

        // DELETE: api/Artists/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Artist>> DeleteArtist(string id)
        {
            var artist = await _context.Artist.FindAsync(id);
            if (artist == null)
            {
                return NotFound();
            }

            _context.Artist.Remove(artist);
            await _context.SaveChangesAsync();

            return artist;
        }

        private bool ArtistExists(string id)
        {
            return _context.Artist.Any(e => e.ArtistId == id);
        }
    }
}
