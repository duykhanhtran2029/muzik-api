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
        public async Task<ActionResult<IEnumerable<SongDTO>>> GetPlaylistSong(string id)
        {
            var playlist = await _context.Playlist.FindAsync(id);

            if (playlist == null)
            {
                return NotFound();
            }

            var songs = await _context.PlaylistSong
               .Where(playlistSong => playlistSong.PlaylistId == id)
               .Select(playlistSong => new SongDTO(playlistSong.Song, (playlistSong.Song.ArtistSong.Select(a => a.Artist).ToList()))).ToListAsync();

            if (!songs.Any())
            {
                return NoContent();
            }

            return songs;

        }

        // PUT: api/PlaylistSongs/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut]
        public async Task<ActionResult<IEnumerable<SongDTO>>> PutPlaylistSong([FromBody] PlaylistSong playlistSong)
        {
            PlaylistSong song = await _context.PlaylistSong.FirstOrDefaultAsync(s => s.SongId == playlistSong.SongId && s.PlaylistId == playlistSong.PlaylistId);
            if (song == null)
            {
                return NotFound();
            }
            _context.PlaylistSong.Remove(song);
            await _context.SaveChangesAsync();

            var songs = await _context.PlaylistSong
       .Where(_playlistSong => _playlistSong.PlaylistId == playlistSong.PlaylistId)
       .Select(playlistSong => new SongDTO(playlistSong.Song, (playlistSong.Song.ArtistSong.Select(a => a.Artist).ToList()))).ToListAsync();

            if (!songs.Any())
            {
                return NoContent();
            }

            return songs;

        }

        // POST: api/PlaylistSongs
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<IEnumerable<SongDTO>>> PostPlaylistSong([FromBody] PlaylistSong playlistSong)
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

            var songs = await _context.PlaylistSong
             .Where(_playlistSong => _playlistSong.PlaylistId == playlistSong.PlaylistId)
             .Select(playlistSong => new SongDTO(playlistSong.Song, (playlistSong.Song.ArtistSong.Select(a => a.Artist).ToList()))).ToListAsync();

            if (!songs.Any())
            {
                return NoContent();
            }

            return songs;
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
