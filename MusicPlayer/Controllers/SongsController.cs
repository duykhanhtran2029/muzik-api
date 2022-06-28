using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Database.MusicPlayer.Models;
using MusicPlayer.Controllers.DTO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

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
                .Where(s => !s.IsDeleted)
                .Select(s => new SongDTO(s, s.ArtistSong.Select(a => a.Artist).ToList()))
                .ToListAsync();
        }

        // GET: api/Songs/trending
        [HttpGet("trending")]
        public async Task<ActionResult<IEnumerable<SongDTO>>> GetTrendingSongs()
        {
            return await _context.Song
                .Where(s => !s.IsDeleted)
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
                .Where(s => !s.IsDeleted)
                .OrderByDescending(s => s.Downloads + s.Likes + s.Listens)
                .ThenByDescending(s => s.ReleaseDate)
                .Select(s => new SongDTO(s, s.ArtistSong.Select(a => a.Artist).ToList()))
                .Take(5)
                .ToListAsync();
        }

        // GET: api/Songs/search
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<SongDTO>>> SearchSongs([FromQuery] string searchKey)
        {
            return await
                _context.Song
                .Where(s => !s.IsDeleted && s.SongName.ToLower().Trim().Contains(searchKey))
                .OrderByDescending(s => s.Downloads + s.Likes + s.Listens)
                .ThenByDescending(s => s.ReleaseDate)
                .Select(s => new SongDTO(s, s.ArtistSong.Select(a => a.Artist).ToList()))
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

        [HttpPut("{id}/listened")]
        public async Task<ActionResult<Song>> ListenedSong(string id)
        {
            var song = await _context.Song.FindAsync(id);
            if (song == null)
            {
                return NotFound();
            }
            song.Listens++;
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

            return song;
        }

        [HttpPut("{id}/downloaded")]
        public async Task<ActionResult<Song>> DownloadedSong(string id)
        {
            var song = await _context.Song.FindAsync(id);
            if (song == null)
            {
                return NotFound();
            }
            song.Downloads++;
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

            return song;
        }


        [HttpPut("{id}/recognizen")]
        public async Task<ActionResult<Song>> RecognizenSong(string id)
        {
            var song = await _context.Song.FindAsync(id);
            if (song == null)
            {
                return NotFound();
            }
            song.IsRecognizable = true;
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

            return song;
        }

        // PUT: api/Songs/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSong(string id, [FromBody] SongDTO data)
        {
            Song song = await _context.Song.Include(s => s.ArtistSong).FirstOrDefaultAsync(s => s.SongId == id);
            song.SongName = data.SongName;
            song.LinkLyric = data.LinkLyric;
            song.Link = data.Link;
            song.LinkBeat = data.LinkBeat;
            song.Thumbnail = data.Thumbnail;

            foreach (var item in song.ArtistSong)
            {
                _context.ArtistSong.Remove(item);
            }

            foreach (var item in data.Artists)
            {
                var artistSong = new ArtistSong();
                artistSong.ArtistId = item.ArtistId;
                artistSong.SongId = song.SongId;
                _context.ArtistSong.Add(artistSong);
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
        public async Task<ActionResult> PostSong([FromBody] SongDTO data)
        {
            var song = new Song();
            song.SongId = data.SongId;
            song.SongName = data.SongName;
            song.LinkLyric = data.LinkLyric;
            song.Link = data.Link;
            song.LinkBeat = data.LinkBeat;
            song.Thumbnail = data.Thumbnail;
            song.ReleaseDate = DateTime.Now;
            song.Likes = song.Listens = song.Downloads = song.Duration = 0;
            song.IsDeleted = false;
            song.IsRecognizable = false;

            _context.Song.Add(song);

            foreach (var item in data.Artists)
            {
                var artistSong = new ArtistSong();
                artistSong.ArtistId = item.ArtistId;
                artistSong.SongId = song.SongId;
                _context.ArtistSong.Add(artistSong);
            }
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

        // POST: api/Songs/songs
        [HttpPost("songs")]
        public async Task<ActionResult<IEnumerable<SongDTO>>> GetSongs([FromBody] string[] songIds)
        {
            return await _context.Song
                .Where(s => !s.IsDeleted && songIds.Any(id => id == s.SongId))
                .Select(s => new SongDTO(s, s.ArtistSong.Select(a => a.Artist).ToList()))
                .ToListAsync();
        }

        // DELETE: api/Songs/ZO09IFDF
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSong(string id)
        {
            var song = await _context.Song.FindAsync(id);
            if (song == null)
            {
                return NotFound();
            }
            song.IsDeleted = true;
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

        private bool SongExists(string id)
        {
            return _context.Song.Any(e => e.SongId == id);
        }
    }
}
