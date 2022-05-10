﻿using System;
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
        public virtual DbSet<Like> Like { get; set; }
        public virtual DbSet<Playlist> Playlist { get; set; }
        public virtual DbSet<PlaylistSong> PlaylistSong { get; set; }
        public virtual DbSet<Song> Song { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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

                entity.HasOne(d => d.Artist)
                    .WithMany(p => p.ArtistSong)
                    .HasForeignKey(d => d.ArtistId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ArtistSon__Artis__4AB81AF0");

                entity.HasOne(d => d.Song)
                    .WithMany(p => p.ArtistSong)
                    .HasForeignKey(d => d.SongId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ArtistSon__SongI__4BAC3F29");
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

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.GenreSong)
                    .HasForeignKey(d => d.GenreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GenreSong__Genre__4CA06362");

                entity.HasOne(d => d.Song)
                    .WithMany(p => p.GenreSong)
                    .HasForeignKey(d => d.SongId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GenreSong__SongI__4D94879B");
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
                    .HasConstraintName("FK__Like__SongID__4E88ABD4");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Like)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Like__UserID__4F7CD00D");
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
                    .HasConstraintName("FK__PlaylistS__Playl__5070F446");

                entity.HasOne(d => d.Song)
                    .WithMany(p => p.PlaylistSong)
                    .HasForeignKey(d => d.SongId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PlaylistS__SongI__5165187F");
            });

            modelBuilder.Entity<Song>(entity =>
            {
                entity.Property(e => e.SongId)
                    .HasColumnName("SongID")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.Link).IsUnicode(false);

                entity.Property(e => e.LinkBeat).IsUnicode(false);

                entity.Property(e => e.LinkLyric).IsUnicode(false);

                entity.Property(e => e.ReleaseDate).HasColumnType("smalldatetime");

                entity.Property(e => e.SongName).IsRequired();

                entity.Property(e => e.ThumbnailL).IsUnicode(false);

                entity.Property(e => e.ThumbnailM).IsUnicode(false);

                entity.Property(e => e.ThumbnailS).IsUnicode(false);
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

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}