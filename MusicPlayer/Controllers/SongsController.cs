using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Database.MusicPlayer.Models;
using MusicPlayer.Controllers.DTO;

namespace MusicPlayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private readonly MusicPlayerDbContext _context;

        public SongsController(MusicPlayerDbContext context)
        {
            _context = context;
        }

        // GET: api/Songs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SongDTO>>> GetSong()
        {
            return await _context.Song
                .Select(s => new SongDTO(s, s.ArtistSong.Select(a => a.Artist).ToList()))
                .ToListAsync();
        }

        // GET: api/Songs/trending
        [HttpGet("trending")]
        public async Task<ActionResult<IEnumerable<SongDTO>>> GetTrendingSongs()
        {
            return await _context.Song
                .OrderByDescending(s => s.Downloads + s.Likes + s.Listens)
                .Select(s => new SongDTO(s, s.ArtistSong.Select(a => a.Artist).ToList()))
                .Take(10)
                .ToListAsync();
        }

        // GET: api/Songs/newest
        [HttpGet("newest")]
        public async Task<ActionResult<IEnumerable<SongDTO>>> GetNewestSongs()
        {
            return await 
                _context.Song
                .OrderByDescending(s => s.Downloads + s.Likes + s.Listens)
                .OrderByDescending(s => s.ReleaseDate)
                .Select(s => new SongDTO(s,s.ArtistSong.Select(a => a.Artist).ToList()))
                .Take(5)
                .ToListAsync();
        }

        // GET: api/Songs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Song>> GetSong(string id)
        {
            var song = await _context.Song.FindAsync(id);

            if (song == null)
            {
                return NotFound();
            }

            return song;
        }

        // PUT: api/Songs/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSong(string id, Song song)
        {
            if (id != song.SongId)
            {
                return BadRequest();
            }

            _context.Entry(song).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SongExists(id))
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

        // POST: api/Songs
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Song>> PostSong(Song song)
        {
            _context.Song.Add(song);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SongExists(song.SongId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSong", new { id = song.SongId }, song);
        }

        // DELETE: api/Songs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Song>> DeleteSong(string id)
        {
            var song = await _context.Song.FindAsync(id);
            if (song == null)
            {
                return NotFound();
            }

            _context.Song.Remove(song);
            await _context.SaveChangesAsync();

            return song;
        }

        private bool SongExists(string id)
        {
            return _context.Song.Any(e => e.SongId == id);
        }
    }
}
