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
    public class PlaylistsController : ControllerBase
    {
        private readonly MusicPlayerDbContext _context;

        public PlaylistsController(MusicPlayerDbContext context)
        {
            _context = context;
        }

        // GET: api/Playlists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Playlist>>> GetPlaylist()
        {
            return await _context.Playlist.Where(s => !s.IsDeleted).ToListAsync();
        }

        // GET: api/Playlists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Playlist>> GetPlaylist(string id)
        {
            var playlist = await _context.Playlist.FindAsync(id);

            if (playlist == null)
            {
                return NotFound();
            }

            return playlist;
        }


        // POST: api/Playlists
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Playlist>> PostPlaylist(Playlist playlist)
        {
            _context.Playlist.Add(playlist);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PlaylistExists(playlist.PlaylistId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPlaylist", new { id = playlist.PlaylistId }, playlist);
        }

        // DELETE: api/Playlists/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Playlist>> DeletePlaylist(string id)
        {
            var playlist = await _context.Playlist.FindAsync(id);
            if (playlist == null)
            {
                return NotFound();
            }

            playlist.IsDeleted = true;
            _context.Entry(playlist).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlaylistExists(id))
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

        private bool PlaylistExists(string id)
        {
            return _context.Playlist.Any(e => e.PlaylistId == id);
        }

        // GET: api/Playlists/userID/id
        [HttpGet("userID/{id}")]
        public async Task<ActionResult<IEnumerable<Playlist>>> GetSongsByUserId(string id)
        {
            
            var playlists = await _context.Playlist
                .Where(playlist => playlist.UserId == id && !playlist.IsDeleted)
                .Select(playlist => playlist)
                .ToListAsync();

            if (!playlists.Any())
            {
                return NoContent();
            }

            return playlists;
        }


        // PUT: api/Playlists/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlaylist(string id, [FromBody] Playlist data)
        {
            Playlist playlist= await _context.Playlist.FirstOrDefaultAsync(s => s.PlaylistId == id);
            playlist.PlaylistName= data.PlaylistName;
            playlist.Thumbnail = data.Thumbnail;
            playlist.IsPrivate = data.IsPrivate;
            playlist.SortDescription = data.SortDescription;
            playlist.Thumbnail = data.Thumbnail;

            _context.Entry(playlist).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlaylistExists(id))
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

    }
}
