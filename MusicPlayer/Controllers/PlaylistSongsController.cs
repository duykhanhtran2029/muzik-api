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
    public class PlaylistSongsController : ControllerBase
    {
        private readonly MusicPlayerDbContext _context;

        public PlaylistSongsController(MusicPlayerDbContext context)
        {
            _context = context;
        }

        // GET: api/PlaylistSongs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlaylistSong>>> GetPlaylistSong()
        {
            return await _context.PlaylistSong.ToListAsync();
        }

        // GET: api/PlaylistSongs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlaylistSong>> GetPlaylistSong(string id)
        {
            var playlistSong = await _context.PlaylistSong.FindAsync(id);

            if (playlistSong == null)
            {
                return NotFound();
            }

            return playlistSong;
        }

        // PUT: api/PlaylistSongs/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlaylistSong(string id, PlaylistSong playlistSong)
        {
            if (id != playlistSong.PlaylistId)
            {
                return BadRequest();
            }

            _context.Entry(playlistSong).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlaylistSongExists(id))
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

        // POST: api/PlaylistSongs
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PlaylistSong>> PostPlaylistSong(PlaylistSong playlistSong)
        {
            _context.PlaylistSong.Add(playlistSong);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PlaylistSongExists(playlistSong.PlaylistId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPlaylistSong", new { id = playlistSong.PlaylistId }, playlistSong);
        }

        // DELETE: api/PlaylistSongs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PlaylistSong>> DeletePlaylistSong(string id)
        {
            var playlistSong = await _context.PlaylistSong.FindAsync(id);
            if (playlistSong == null)
            {
                return NotFound();
            }

            _context.PlaylistSong.Remove(playlistSong);
            await _context.SaveChangesAsync();

            return playlistSong;
        }

        private bool PlaylistSongExists(string id)
        {
            return _context.PlaylistSong.Any(e => e.PlaylistId == id);
        }
    }
}
