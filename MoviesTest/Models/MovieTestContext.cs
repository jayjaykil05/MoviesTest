using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MoviesTest.Models;

public partial class MovieTestContext : DbContext
{
    public MovieTestContext()
    {
    }

    public MovieTestContext(DbContextOptions<MovieTestContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.MovieId).HasName("Movies_pkey");

            entity.Property(e => e.MovieId).HasColumnName("movieId");
            entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");
            entity.Property(e => e.IsRented).HasColumnName("isRented");
            entity.Property(e => e.MovieDescription)
                .HasColumnType("character varying")
                .HasColumnName("movieDescription");
            entity.Property(e => e.MovieTitle)
                .HasColumnType("character varying")
                .HasColumnName("movieTitle");
            entity.Property(e => e.RentalDate).HasColumnName("rentalDate");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("User_pkey");

            entity.ToTable("User");

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("userId");
            entity.Property(e => e.EmailAddress)
                .HasColumnType("character varying")
                .HasColumnName("emailAddress");
            entity.Property(e => e.Fullname)
                .HasColumnType("character varying")
                .HasColumnName("fullname");
            entity.Property(e => e.Password)
                .HasColumnType("character varying")
                .HasColumnName("password");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
