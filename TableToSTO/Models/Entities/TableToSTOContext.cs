using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TableToSTO.Models.M0305;

namespace TableToSTO.Models.Entities;

public partial class TableToSTOContext : DbContext
{
    public TableToSTOContext() {}

    public TableToSTOContext(DbContextOptions<TableToSTOContext> options) : base(options) {}

    public virtual DbSet<DmHangHoa> DmHangHoas { get; set; }

    public virtual DbSet<PhieuNhap> PhieuNhaps { get; set; }

    public virtual DbSet<PhieuNhapCt> PhieuNhapCts { get; set; }

    public DbSet<M0305DanhSachPhieuNhapSTO> M0305DanhSachPhieuNhapSTOs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:Connection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DmHangHoa>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DM_HangH__3214EC2718B8914E");

            entity.ToTable("DM_HangHoa");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.TenHangHoa).HasMaxLength(250);
        });

        modelBuilder.Entity<PhieuNhap>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PhieuNha__3214EC27A7FF88D3");

            entity.ToTable("PhieuNhap");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.NgayLapPhieu).HasColumnType("datetime");
            entity.Property(e => e.SoPhieu).HasMaxLength(50);
        });

        modelBuilder.Entity<PhieuNhapCt>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PhieuNha__3214EC27D0D3B007");

            entity.ToTable("PhieuNhapCT");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Idhh).HasColumnName("IDHH");
            entity.Property(e => e.Idpn).HasColumnName("IDPN");

            entity.HasOne(d => d.IdhhNavigation).WithMany(p => p.PhieuNhapCts)
                .HasForeignKey(d => d.Idhh)
                .HasConstraintName("FK__PhieuNhapC__IDHH__3D5E1FD2");

            entity.HasOne(d => d.IdpnNavigation).WithMany(p => p.PhieuNhapCts)
                .HasForeignKey(d => d.Idpn)
                .HasConstraintName("FK__PhieuNhapC__IDPN__3C69FB99");
        });

        modelBuilder.Entity<M0305DanhSachPhieuNhapSTO>().HasNoKey();

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
