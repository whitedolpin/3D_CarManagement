using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Repository.Models;

public partial class Prn3dContext : DbContext
{
    public Prn3dContext()
    {
    }

    public Prn3dContext(DbContextOptions<Prn3dContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Car> Cars { get; set; }

    public virtual DbSet<Position> Positions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\mssqllocaldb;Initial Catalog=PRN_3D;Integrated Security=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Car>(entity =>
        {
            entity.HasKey(e => e.CarId).HasName("PK__Cars__68A0340EB0DB5AF2");

            entity.Property(e => e.CarId).HasColumnName("CarID");
            entity.Property(e => e.AgeRange)
                .HasMaxLength(100)
                .HasColumnName("Age_Range");
            entity.Property(e => e.BrandName).HasMaxLength(150);
            entity.Property(e => e.CarAdvanceFeature).HasMaxLength(500);
            entity.Property(e => e.CarStatus)
                .HasMaxLength(100)
                .HasColumnName("Car_status");
            entity.Property(e => e.CheckBy).HasMaxLength(500);
            entity.Property(e => e.Color).HasMaxLength(100);
            entity.Property(e => e.CreatedBy).HasMaxLength(500);
            entity.Property(e => e.File3D).HasMaxLength(1000);
            entity.Property(e => e.Material).HasMaxLength(100);
            entity.Property(e => e.ModelName).HasMaxLength(200);
            entity.Property(e => e.MonthlyCheck)
                .HasColumnType("date")
                .HasColumnName("Monthly_Check");
            entity.Property(e => e.OtherInfo)
                .HasMaxLength(1000)
                .HasColumnName("Other_info");
            entity.Property(e => e.Packaging).HasMaxLength(200);
            entity.Property(e => e.PositionId).HasColumnName("PositionID");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Scale).HasMaxLength(100);
            entity.Property(e => e.StatusCheck)
                .HasMaxLength(100)
                .HasColumnName("Status_check");

            entity.HasOne(d => d.Position).WithMany(p => p.Cars)
                .HasForeignKey(d => d.PositionId)
                .HasConstraintName("FK__Cars__PositionID__38996AB5");
        });

        modelBuilder.Entity<Position>(entity =>
        {
            entity.HasKey(e => e.PositionId).HasName("PK__Position__B07CF58ECE7930D4");

            entity.ToTable("Position");

            entity.Property(e => e.PositionId).HasColumnName("positionID");
            entity.Property(e => e.Position1).HasColumnName("Position");
            entity.Property(e => e.ShelfNumber).HasColumnName("Shelf_number");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__users__1788CCAC32B3F15E");

            entity.ToTable("users");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Roles).HasMaxLength(100);
            entity.Property(e => e.UserName).HasMaxLength(100);
            entity.Property(e => e.UserPass).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
