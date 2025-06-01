using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace StartupSearchEngine.Models;

public partial class StartupDbContext : DbContext
{
    public StartupDbContext()
    {
    }

    public StartupDbContext(DbContextOptions<StartupDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CleanedStartup> CleanedStartups { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=DESKTOP-152FRH1;Database=StartupDB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CleanedStartup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CleanedS__3214EC2745689EA7");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.City).HasColumnType("text");
            entity.Property(e => e.Company).HasColumnType("text");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.EmployeeRange).HasColumnType("text");
            entity.Property(e => e.Founders).HasColumnType("text");
            entity.Property(e => e.Industry).HasColumnType("text");
            entity.Property(e => e.InvestmentStage).HasColumnType("text");
            entity.Property(e => e.NoOfInvestors).HasColumnType("text");
            entity.Property(e => e.StartingYear).HasColumnType("text");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
