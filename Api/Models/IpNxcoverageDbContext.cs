using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ipNXSalesPortalApis.Models;

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

    public virtual DbSet<GcpgeoCodingApiKey> GcpgeoCodingApiKeys { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=(local);Integrated Security=True;Database=ipNXCoverageDb;Persist Security Info=False;Pooling=False;Multiple Active Result Sets=False;Connect Timeout=60;Encrypt=True;Trust Server Certificate=True;Command Timeout=0");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CoverageLocation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Coverage__3214EC0796BAFC2F");

            entity.Property(e => e.CoverageName).HasMaxLength(500).IsUnicode(false).HasColumnName("coverageName");
            entity.Property(e => e.Latitude).HasColumnName("latitude");
            entity.Property(e => e.Lga).HasMaxLength(255).IsUnicode(false).HasColumnName("lga");
            entity.Property(e => e.Longitude).HasColumnName("longitude");
            entity.Property(e => e.State).HasMaxLength(255).IsUnicode(false).HasColumnName("state");
        });

        modelBuilder.Entity<GcpgeoCodingApiKey>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC075BE1D161");
            entity.ToTable("GCPGeoCodingApiKey");
            entity.Property(e => e.Key).HasMaxLength(500).IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
