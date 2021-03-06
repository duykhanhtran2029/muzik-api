using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Database.MusicPlayer.Models
{
    public partial class MusicPlayerDbContext : DbContext
    {
        public MusicPlayerDbContext()
        {
        }

        public MusicPlayerDbContext(DbContextOptions<MusicPlayerDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Artist> Artist { get; set; }
        public virtual DbSet<ArtistSong> ArtistSong { get; set; }
        public virtual DbSet<Genre> Genre { get; set; }
        public virtual DbSet<GenreSong> GenreSong { get; set; }
        public virtual DbSet<History> History { get; set; }
        public virtual DbSet<Like> Like { get; set; }
        public virtual DbSet<Playlist> Playlist { get; set; }
        public virtual DbSet<PlaylistSong> PlaylistSong { get; set; }
        public virtual DbSet<Song> Song { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // NOTE: No need to override
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Artist>(entity =>
            {
                entity.Property(e => e.ArtistId)
                    .HasColumnName("ArtistID")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.ArtistName).IsRequired();

                entity.Property(e => e.Thumbnail).IsUnicode(false);
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

                entity.HasOne(d => d.Artist)
                    .WithMany(p => p.ArtistSong)
                    .HasForeignKey(d => d.ArtistId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ArtistSon__Artis__37A5467C");

                entity.HasOne(d => d.Song)
                    .WithMany(p => p.ArtistSong)
                    .HasForeignKey(d => d.SongId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ArtistSon__SongI__38996AB5");
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.Property(e => e.GenreId)
                    .HasColumnName("GenreID")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.GenreName).IsRequired();
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

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.GenreSong)
                    .HasForeignKey(d => d.GenreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GenreSong__Genre__398D8EEE");

                entity.HasOne(d => d.Song)
                    .WithMany(p => p.GenreSong)
                    .HasForeignKey(d => d.SongId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GenreSong__SongI__3A81B327");
            });

            modelBuilder.Entity<History>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.SongId })
                    .HasName("PK_UserSong_History");

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.SongId)
                    .HasColumnName("SongID")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.HasOne(d => d.Song)
                    .WithMany(p => p.History)
                    .HasForeignKey(d => d.SongId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__History__SongID__3E52440B");
            });

            modelBuilder.Entity<Like>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.SongId })
                    .HasName("PK_UserSong");

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.SongId)
                    .HasColumnName("SongID")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.HasOne(d => d.Song)
                    .WithMany(p => p.Like)
                    .HasForeignKey(d => d.SongId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Like__SongID__3B75D760");
            });

            modelBuilder.Entity<Playlist>(entity =>
            {
                entity.Property(e => e.PlaylistId)
                    .HasColumnName("PlaylistID")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.PlaylistName).IsRequired();

                entity.Property(e => e.SortDescription).IsUnicode(false);

                entity.Property(e => e.Thumbnail).IsUnicode(false);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnName("UserID")
                    .HasMaxLength(8)
                    .IsUnicode(false);
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

                entity.HasOne(d => d.Playlist)
                    .WithMany(p => p.PlaylistSong)
                    .HasForeignKey(d => d.PlaylistId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PlaylistS__Playl__3C69FB99");

                entity.HasOne(d => d.Song)
                    .WithMany(p => p.PlaylistSong)
                    .HasForeignKey(d => d.SongId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PlaylistS__SongI__3D5E1FD2");
            });

            modelBuilder.Entity<Song>(entity =>
            {
                entity.Property(e => e.SongId)
                    .HasColumnName("SongID")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Link).IsUnicode(false);

                entity.Property(e => e.LinkBeat).IsUnicode(false);

                entity.Property(e => e.LinkLyric).IsUnicode(false);

                entity.Property(e => e.ReleaseDate).HasColumnType("smalldatetime");

                entity.Property(e => e.SongName).IsRequired();

                entity.Property(e => e.Thumbnail).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
