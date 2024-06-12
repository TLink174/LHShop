using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace LHShop.Data;

public partial class Lhshop2024Context : DbContext
{
    public Lhshop2024Context()
    {
    }

    public Lhshop2024Context(DbContextOptions<Lhshop2024Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Banbe> Banbes { get; set; }

    public virtual DbSet<Chitiethd> Chitiethds { get; set; }

    public virtual DbSet<Chude> Chudes { get; set; }

    public virtual DbSet<Gopy> Gopies { get; set; }

    public virtual DbSet<Hanghoa> Hanghoas { get; set; }

    public virtual DbSet<Hoadon> Hoadons { get; set; }

    public virtual DbSet<Hoidap> Hoidaps { get; set; }

    public virtual DbSet<Khachhang> Khachhangs { get; set; }

    public virtual DbSet<Loai> Loais { get; set; }

    public virtual DbSet<Nhacungcap> Nhacungcaps { get; set; }

    public virtual DbSet<Nhanvien> Nhanviens { get; set; }

    public virtual DbSet<Phancong> Phancongs { get; set; }

    public virtual DbSet<Phanquyen> Phanquyens { get; set; }

    public virtual DbSet<Phongban> Phongbans { get; set; }

    public virtual DbSet<Trangthai> Trangthais { get; set; }

    public virtual DbSet<Trangweb> Trangwebs { get; set; }

    public virtual DbSet<Vchitiethoadon> Vchitiethoadons { get; set; }

    public virtual DbSet<Yeuthich> Yeuthiches { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql("server=localhost;port=3306;database=lhshop2024;uid=root;pwd=linh17423", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.3.0-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Banbe>(entity =>
        {
            entity.HasKey(e => e.MaBb).HasName("PRIMARY");

            entity.ToTable("banbe");

            entity.HasIndex(e => e.MaKh, "FK_BanBe_KhachHang");

            entity.HasIndex(e => e.MaHh, "FK_QuangBa_HangHoa");

            entity.Property(e => e.MaBb).HasColumnName("MaBB");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.GhiChu).HasColumnType("text");
            entity.Property(e => e.HoTen).HasMaxLength(50);
            entity.Property(e => e.MaHh).HasColumnName("MaHH");
            entity.Property(e => e.MaKh)
                .HasMaxLength(20)
                .HasColumnName("MaKH");
            entity.Property(e => e.NgayGui)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp");

            entity.HasOne(d => d.MaHhNavigation).WithMany(p => p.Banbes)
                .HasForeignKey(d => d.MaHh)
                .HasConstraintName("FK_QuangBa_HangHoa");

            entity.HasOne(d => d.MaKhNavigation).WithMany(p => p.Banbes)
                .HasForeignKey(d => d.MaKh)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_BanBe_KhachHang");
        });

        modelBuilder.Entity<Chitiethd>(entity =>
        {
            entity.HasKey(e => e.MaCt).HasName("PRIMARY");

            entity.ToTable("chitiethd");

            entity.HasIndex(e => e.MaHd, "FK_OrderDetails_Orders");

            entity.HasIndex(e => e.MaHh, "FK_OrderDetails_Products");

            entity.Property(e => e.MaCt).HasColumnName("MaCT");
            entity.Property(e => e.DonGia)
                .HasPrecision(18, 2)
                .HasDefaultValueSql("'0.00'");
            entity.Property(e => e.GiamGia)
                .HasPrecision(18, 2)
                .HasDefaultValueSql("'0.00'");
            entity.Property(e => e.MaHd).HasColumnName("MaHD");
            entity.Property(e => e.MaHh).HasColumnName("MaHH");
            entity.Property(e => e.SoLuong).HasDefaultValueSql("'1'");

            entity.HasOne(d => d.MaHdNavigation).WithMany(p => p.Chitiethds)
                .HasForeignKey(d => d.MaHd)
                .HasConstraintName("FK_OrderDetails_Orders");

            entity.HasOne(d => d.MaHhNavigation).WithMany(p => p.Chitiethds)
                .HasForeignKey(d => d.MaHh)
                .HasConstraintName("FK_OrderDetails_Products");
        });

        modelBuilder.Entity<Chude>(entity =>
        {
            entity.HasKey(e => e.MaCd).HasName("PRIMARY");

            entity.ToTable("chude");

            entity.HasIndex(e => e.MaNv, "FK_ChuDe_NhanVien");

            entity.Property(e => e.MaCd).HasColumnName("MaCD");
            entity.Property(e => e.MaNv)
                .HasMaxLength(50)
                .HasColumnName("MaNV");
            entity.Property(e => e.TenCd)
                .HasMaxLength(50)
                .HasColumnName("TenCD");

            entity.HasOne(d => d.MaNvNavigation).WithMany(p => p.Chudes)
                .HasForeignKey(d => d.MaNv)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ChuDe_NhanVien");
        });

        modelBuilder.Entity<Gopy>(entity =>
        {
            entity.HasKey(e => e.MaGy).HasName("PRIMARY");

            entity.ToTable("gopy");

            entity.HasIndex(e => e.MaCd, "FK_GopY_ChuDe");

            entity.Property(e => e.MaGy)
                .HasMaxLength(50)
                .HasColumnName("MaGY");
            entity.Property(e => e.CanTraLoi).HasDefaultValueSql("'0'");
            entity.Property(e => e.DienThoai).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.HoTen).HasMaxLength(50);
            entity.Property(e => e.MaCd).HasColumnName("MaCD");
            entity.Property(e => e.NgayGy)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("NgayGY");
            entity.Property(e => e.NgayTl).HasColumnName("NgayTL");
            entity.Property(e => e.NoiDung).HasColumnType("text");
            entity.Property(e => e.TraLoi).HasMaxLength(50);

            entity.HasOne(d => d.MaCdNavigation).WithMany(p => p.Gopies)
                .HasForeignKey(d => d.MaCd)
                .HasConstraintName("FK_GopY_ChuDe");
        });

        modelBuilder.Entity<Hanghoa>(entity =>
        {
            entity.HasKey(e => e.MaHh).HasName("PRIMARY");

            entity.ToTable("hanghoa");

            entity.HasIndex(e => e.MaNcc, "FK_HangHoa_NhaCungCap");

            entity.HasIndex(e => e.MaLoai, "FK_Products_Categories");

            entity.Property(e => e.MaHh).HasColumnName("MaHH");
            entity.Property(e => e.DonGia)
                .HasPrecision(18, 2)
                .HasDefaultValueSql("'0.00'");
            entity.Property(e => e.GiamGia)
                .HasPrecision(18, 2)
                .HasDefaultValueSql("'0.00'");
            entity.Property(e => e.Hinh).HasMaxLength(50);
            entity.Property(e => e.MaNcc)
                .HasMaxLength(50)
                .HasColumnName("MaNCC");
            entity.Property(e => e.MoTa).HasColumnType("text");
            entity.Property(e => e.MoTaDonVi).HasMaxLength(50);
            entity.Property(e => e.NgaySx)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("NgaySX");
            entity.Property(e => e.SoLanXem).HasDefaultValueSql("'0'");
            entity.Property(e => e.TenAlias).HasMaxLength(50);
            entity.Property(e => e.TenHh)
                .HasMaxLength(50)
                .HasColumnName("TenHH");

            entity.HasOne(d => d.MaLoaiNavigation).WithMany(p => p.Hanghoas)
                .HasForeignKey(d => d.MaLoai)
                .HasConstraintName("FK_Products_Categories");

            entity.HasOne(d => d.MaNccNavigation).WithMany(p => p.Hanghoas)
                .HasForeignKey(d => d.MaNcc)
                .HasConstraintName("FK_HangHoa_NhaCungCap");
        });

        modelBuilder.Entity<Hoadon>(entity =>
        {
            entity.HasKey(e => e.MaHd).HasName("PRIMARY");

            entity.ToTable("hoadon");

            entity.HasIndex(e => e.MaNv, "FK_HoaDon_NhanVien");

            entity.HasIndex(e => e.MaTrangThai, "FK_HoaDon_TrangThai");

            entity.HasIndex(e => e.MaKh, "FK_Orders_Customers");

            entity.Property(e => e.MaHd).HasColumnName("MaHD");
            entity.Property(e => e.CachThanhToan)
                .HasMaxLength(50)
                .HasDefaultValueSql("'Cash'");
            entity.Property(e => e.CachVanChuyen)
                .HasMaxLength(50)
                .HasDefaultValueSql("'Airline'");
            entity.Property(e => e.DiaChi).HasMaxLength(60);
            entity.Property(e => e.GhiChu).HasMaxLength(50);
            entity.Property(e => e.HoTen).HasMaxLength(50);
            entity.Property(e => e.MaKh)
                .HasMaxLength(20)
                .HasColumnName("MaKH");
            entity.Property(e => e.MaNv)
                .HasMaxLength(50)
                .HasColumnName("MaNV");
            entity.Property(e => e.MaTrangThai).HasDefaultValueSql("'0'");
            entity.Property(e => e.NgayCan).HasColumnType("datetime");
            entity.Property(e => e.NgayDat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.NgayGiao).HasColumnType("datetime");
            entity.Property(e => e.PhiVanChuyen)
                .HasPrecision(18, 2)
                .HasDefaultValueSql("'0.00'");
            entity.Property(e => e.SoDienThoai).HasMaxLength(24);

            entity.HasOne(d => d.MaKhNavigation).WithMany(p => p.Hoadons)
                .HasForeignKey(d => d.MaKh)
                .HasConstraintName("FK_Orders_Customers");

            entity.HasOne(d => d.MaNvNavigation).WithMany(p => p.Hoadons)
                .HasForeignKey(d => d.MaNv)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_HoaDon_NhanVien");

            entity.HasOne(d => d.MaTrangThaiNavigation).WithMany(p => p.Hoadons)
                .HasForeignKey(d => d.MaTrangThai)
                .HasConstraintName("FK_HoaDon_TrangThai");
        });

        modelBuilder.Entity<Hoidap>(entity =>
        {
            entity.HasKey(e => e.MaHd).HasName("PRIMARY");

            entity.ToTable("hoidap");

            entity.HasIndex(e => e.MaNv, "FK_HoiDap_NhanVien");

            entity.Property(e => e.MaHd).HasColumnName("MaHD");
            entity.Property(e => e.CauHoi).HasMaxLength(50);
            entity.Property(e => e.MaNv)
                .HasMaxLength(50)
                .HasColumnName("MaNV");
            entity.Property(e => e.NgayDua)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp");
            entity.Property(e => e.TraLoi).HasMaxLength(50);

            entity.HasOne(d => d.MaNvNavigation).WithMany(p => p.Hoidaps)
                .HasForeignKey(d => d.MaNv)
                .HasConstraintName("FK_HoiDap_NhanVien");
        });

        modelBuilder.Entity<Khachhang>(entity =>
        {
            entity.HasKey(e => e.MaKh).HasName("PRIMARY");

            entity.ToTable("khachhang");

            entity.Property(e => e.MaKh)
                .HasMaxLength(20)
                .HasColumnName("MaKH");
            entity.Property(e => e.DiaChi)
                .HasMaxLength(60)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.DienThoai).HasMaxLength(24);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.GioiTinh)
                .HasMaxLength(10)
                .HasDefaultValueSql("'0'");
            entity.Property(e => e.HieuLuc).HasDefaultValueSql("'0'");
            entity.Property(e => e.Hinh)
                .HasMaxLength(50)
                .HasDefaultValueSql("'Photo.gif'");
            entity.Property(e => e.HoTen)
                .HasMaxLength(50)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.MatKhau).HasMaxLength(50);
            entity.Property(e => e.RandomKey).HasMaxLength(50);
            entity.Property(e => e.VaiTro).HasDefaultValueSql("'0'");
        });

        modelBuilder.Entity<Loai>(entity =>
        {
            entity.HasKey(e => e.MaLoai).HasName("PRIMARY");

            entity.ToTable("loai");

            entity.Property(e => e.Hinh)
                .HasMaxLength(50)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.MoTa).HasColumnType("text");
            entity.Property(e => e.TenLoai)
                .HasMaxLength(50)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.TenLoaiAlias)
                .HasMaxLength(50)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
        });

        modelBuilder.Entity<Nhacungcap>(entity =>
        {
            entity.HasKey(e => e.MaNcc).HasName("PRIMARY");

            entity.ToTable("nhacungcap");

            entity.Property(e => e.MaNcc)
                .HasMaxLength(50)
                .HasColumnName("MaNCC");
            entity.Property(e => e.DiaChi)
                .HasMaxLength(50)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.DienThoai)
                .HasMaxLength(50)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Logo)
                .HasMaxLength(50)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.MoTa).HasColumnType("text");
            entity.Property(e => e.NguoiLienLac)
                .HasMaxLength(50)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.TenCongTy)
                .HasMaxLength(50)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
        });

        modelBuilder.Entity<Nhanvien>(entity =>
        {
            entity.HasKey(e => e.MaNv).HasName("PRIMARY");

            entity.ToTable("nhanvien");

            entity.Property(e => e.MaNv)
                .HasMaxLength(50)
                .HasColumnName("MaNV");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.HoTen).HasMaxLength(50);
            entity.Property(e => e.MatKhau).HasMaxLength(50);
        });

        modelBuilder.Entity<Phancong>(entity =>
        {
            entity.HasKey(e => e.MaPc).HasName("PRIMARY");

            entity.ToTable("phancong");

            entity.HasIndex(e => e.MaNv, "FK_PhanCong_NhanVien");

            entity.HasIndex(e => e.MaPb, "FK_PhanCong_PhongBan");

            entity.Property(e => e.MaPc).HasColumnName("MaPC");
            entity.Property(e => e.HieuLuc).HasColumnType("bit(1)");
            entity.Property(e => e.MaNv)
                .HasMaxLength(50)
                .HasColumnName("MaNV");
            entity.Property(e => e.MaPb)
                .HasMaxLength(7)
                .HasColumnName("MaPB");
            entity.Property(e => e.NgayPc)
                .HasColumnType("datetime")
                .HasColumnName("NgayPC");

            entity.HasOne(d => d.MaNvNavigation).WithMany(p => p.Phancongs)
                .HasForeignKey(d => d.MaNv)
                .HasConstraintName("FK_PhanCong_NhanVien");

            entity.HasOne(d => d.MaPbNavigation).WithMany(p => p.Phancongs)
                .HasForeignKey(d => d.MaPb)
                .HasConstraintName("FK_PhanCong_PhongBan");
        });

        modelBuilder.Entity<Phanquyen>(entity =>
        {
            entity.HasKey(e => e.MaPq).HasName("PRIMARY");

            entity.ToTable("phanquyen");

            entity.HasIndex(e => e.MaPb, "FK_PhanQuyen_PhongBan");

            entity.HasIndex(e => e.MaTrang, "FK_PhanQuyen_TrangWeb");

            entity.Property(e => e.MaPq).HasColumnName("MaPQ");
            entity.Property(e => e.MaPb)
                .HasMaxLength(7)
                .HasColumnName("MaPB");
            entity.Property(e => e.Sua).HasDefaultValueSql("'0'");
            entity.Property(e => e.Them).HasDefaultValueSql("'0'");
            entity.Property(e => e.Xem).HasDefaultValueSql("'0'");
            entity.Property(e => e.Xoa).HasDefaultValueSql("'0'");

            entity.HasOne(d => d.MaPbNavigation).WithMany(p => p.Phanquyens)
                .HasForeignKey(d => d.MaPb)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_PhanQuyen_PhongBan");

            entity.HasOne(d => d.MaTrangNavigation).WithMany(p => p.Phanquyens)
                .HasForeignKey(d => d.MaTrang)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_PhanQuyen_TrangWeb");
        });

        modelBuilder.Entity<Phongban>(entity =>
        {
            entity.HasKey(e => e.MaPb).HasName("PRIMARY");

            entity.ToTable("phongban");

            entity.Property(e => e.MaPb)
                .HasMaxLength(7)
                .HasColumnName("MaPB");
            entity.Property(e => e.TenPb)
                .HasMaxLength(50)
                .HasColumnName("TenPB");
            entity.Property(e => e.ThongTin).HasColumnType("text");
        });

        modelBuilder.Entity<Trangthai>(entity =>
        {
            entity.HasKey(e => e.MaTrangThai).HasName("PRIMARY");

            entity.ToTable("trangthai");

            entity.Property(e => e.MaTrangThai).ValueGeneratedNever();
            entity.Property(e => e.MoTa)
                .HasMaxLength(500)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.TenTrangThai)
                .HasMaxLength(50)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
        });

        modelBuilder.Entity<Trangweb>(entity =>
        {
            entity.HasKey(e => e.MaTrang).HasName("PRIMARY");

            entity.ToTable("trangweb");

            entity.Property(e => e.TenTrang).HasMaxLength(50);
            entity.Property(e => e.Url)
                .HasMaxLength(250)
                .HasColumnName("URL");
        });

        modelBuilder.Entity<Vchitiethoadon>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vchitiethoadon");

            entity.Property(e => e.DonGia)
                .HasPrecision(18, 2)
                .HasDefaultValueSql("'0.00'");
            entity.Property(e => e.GiamGia)
                .HasPrecision(18, 2)
                .HasDefaultValueSql("'0.00'");
            entity.Property(e => e.MaCt).HasColumnName("MaCT");
            entity.Property(e => e.MaHd).HasColumnName("MaHD");
            entity.Property(e => e.MaHh).HasColumnName("MaHH");
            entity.Property(e => e.SoLuong).HasDefaultValueSql("'1'");
            entity.Property(e => e.TenHh)
                .HasMaxLength(50)
                .HasColumnName("TenHH");
        });

        modelBuilder.Entity<Yeuthich>(entity =>
        {
            entity.HasKey(e => e.MaYt).HasName("PRIMARY");

            entity.ToTable("yeuthich");

            entity.HasIndex(e => e.MaKh, "FK_Favorites_Customers");

            entity.HasIndex(e => e.MaHh, "FK_YeuThich_HangHoa");

            entity.Property(e => e.MaYt).HasColumnName("MaYT");
            entity.Property(e => e.MaHh).HasColumnName("MaHH");
            entity.Property(e => e.MaKh)
                .HasMaxLength(20)
                .HasColumnName("MaKH");
            entity.Property(e => e.MoTa).HasMaxLength(255);
            entity.Property(e => e.NgayChon).HasColumnType("datetime");

            entity.HasOne(d => d.MaHhNavigation).WithMany(p => p.Yeuthiches)
                .HasForeignKey(d => d.MaHh)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_YeuThich_HangHoa");

            entity.HasOne(d => d.MaKhNavigation).WithMany(p => p.Yeuthiches)
                .HasForeignKey(d => d.MaKh)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Favorites_Customers");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
