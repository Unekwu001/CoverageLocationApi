using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Api.Models;

public partial class CoverageDbContext : DbContext
{
    public CoverageDbContext()
    {
    }

    public CoverageDbContext(DbContextOptions<CoverageDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CoverageLocation> CoverageLocations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=(local);Integrated Security=True;Database=ipNXCoverageDb;Persist Security Info=False;Pooling=False;Multiple Active Result Sets=False;Connect Timeout=60;Encrypt=True;Trust Server Certificate=True;Command Timeout=0");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CoverageLocation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Coverage__3214EC070D2C4000");

            entity.Property(e => e.CoverageName).HasMaxLength(500).IsUnicode(false).HasColumnName("coverageName");
            entity.Property(e => e.Latitude).HasMaxLength(500).IsUnicode(false).HasColumnName("latitude");
            entity.Property(e => e.Lga).HasMaxLength(255).IsUnicode(false).HasColumnName("lga");
            entity.Property(e => e.Longitude).HasMaxLength(500).IsUnicode(false).HasColumnName("longitude");
            entity.Property(e => e.State).HasMaxLength(255).IsUnicode(false).HasColumnName("state");
        });
        OnModelCreatingPartial(modelBuilder);
    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
