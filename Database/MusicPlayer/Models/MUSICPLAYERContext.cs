using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Database.Models
{
    public partial class MUSICPLAYERContext : DbContext
    {
        public MUSICPLAYERContext()
        {
        }

        public MUSICPLAYERContext(DbContextOptions<MUSICPLAYERContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Album> Album { get; set; }
        public virtual DbSet<AlbumArtist> AlbumArtist { get; set; }
        public virtual DbSet<AlbumSong> AlbumSong { get; set; }
        public virtual DbSet<Artist> Artist { get; set; }
        public virtual DbSet<ArtistSong> ArtistSong { get; set; }
        public virtual DbSet<Genre> Genre { get; set; }
        public virtual DbSet<GenreSong> GenreSong { get; set; }
        public virtual DbSet<Like> Like { get; set; }
        public virtual DbSet<Playlist> Playlist { get; set; }
        public virtual DbSet<PlaylistSong> PlaylistSong { get; set; }
        public virtual DbSet<Song> Song { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=MUSICPLAYER;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Album>(entity =>
            {
                entity.Property(e => e.AlbumId)
                    .HasColumnName("AlbumID")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.AlbumName).IsRequired();

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.ThumbnailL).IsUnicode(false);

                entity.Property(e => e.ThumbnailM).IsUnicode(false);

                entity.Property(e => e.ThumbnailS).IsUnicode(false);
            });

            modelBuilder.Entity<AlbumArtist>(entity =>
            {
                entity.HasKey(e => new { e.AlbumId, e.ArtistId });

                entity.Property(e => e.AlbumId)
                    .HasColumnName("AlbumID")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.ArtistId)
                    .HasColumnName("ArtistID")
                    .HasMaxLength(8)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AlbumSong>(entity =>
            {
                entity.HasKey(e => new { e.AlbumId, e.SongId });

                entity.Property(e => e.AlbumId)
                    .HasColumnName("AlbumID")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.SongId)
                    .HasColumnName("SongID")
                    .HasMaxLength(8)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Artist>(entity =>
            {
                entity.Property(e => e.ArtistId)
                    .HasColumnName("ArtistID")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.ArtistName).IsRequired();

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.ThumbnailL).IsUnicode(false);

                entity.Property(e => e.ThumbnailM).IsUnicode(false);

                entity.Property(e => e.ThumbnailS).IsUnicode(false);
            });

            modelBuilder.Entity<ArtistSong>(entity =>
            {
                entity.HasKey(e => new { e.ArtistId, e.SongId });

                entity.Property(e => e.ArtistId)
                    .HasColumnName("ArtistID")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.SongId)
                    .HasColumnName("SongID")
                    .HasMaxLength(8)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.Property(e => e.GenreId)
                    .HasColumnName("GenreID")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.GenreName).IsRequired();

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<GenreSong>(entity =>
            {
                entity.HasKey(e => new { e.GenreId, e.SongId });

                entity.Property(e => e.GenreId)
                    .HasColumnName("GenreID")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.SongId)
                    .HasColumnName("SongID")
                    .HasMaxLength(8)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Like>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.SongId });

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.SongId)
                    .HasColumnName("SongID")
                    .HasMaxLength(8)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Playlist>(entity =>
            {
                entity.Property(e => e.PlaylistId)
                    .HasColumnName("PlaylistID")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.PlaylistName).IsRequired();

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnName("UserID")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Playlist)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Playlist__UserID__534D60F1");
            });

            modelBuilder.Entity<PlaylistSong>(entity =>
            {
                entity.HasKey(e => new { e.PlaylistId, e.SongId });

                entity.Property(e => e.PlaylistId)
                    .HasColumnName("PlaylistID")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.SongId)
                    .HasColumnName("SongID")
                    .HasMaxLength(8)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Song>(entity =>
            {
                entity.Property(e => e.SongId)
                    .HasColumnName("SongID")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Downloads).HasDefaultValueSql("((0))");

                entity.Property(e => e.Duration).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.Likes).HasDefaultValueSql("((0))");

                entity.Property(e => e.Link).IsUnicode(false);

                entity.Property(e => e.LinkBeat).IsUnicode(false);

                entity.Property(e => e.LinkLyric).IsUnicode(false);

                entity.Property(e => e.Listens).HasDefaultValueSql("((0))");

                entity.Property(e => e.ReleaseDate).HasColumnType("smalldatetime");

                entity.Property(e => e.SongName).IsRequired();

                entity.Property(e => e.ThumbnailL).IsUnicode(false);

                entity.Property(e => e.ThumbnailM).IsUnicode(false);

                entity.Property(e => e.ThumbnailS).IsUnicode(false);

                entity.Property(e => e.Type).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Avatar)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.DateOfBirth).HasColumnType("smalldatetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.FirstName).IsRequired();

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.LastName).IsRequired();

                entity.Property(e => e.Password)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Userame)
                    .IsRequired()
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
