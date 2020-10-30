using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeTaiWin
{
    public class MonHoc
    {
        [Key]
        [MaxLength(6)]
        public string MaMonHoc { get; set; }
        [Required]
        [MaxLength(30)]
        public string TenMonHoc { get; set; }
        [Required]
        public int SoTietLyThuyet { get; set; }
        [Required]
        public int SoTietThucHanh { get; set; }
        public ICollection<PhanCong> DSPC { get; set; }
        public MonHoc()
        {
            MaMonHoc = TenMonHoc = null;
            SoTietLyThuyet = SoTietThucHanh = 0;
        }
        public MonHoc(string MaMonHoc, string TenMonHoc, int SoTietLyThuyet, int SoTietThucHanh)
        {
            this.MaMonHoc = MaMonHoc;
            this.TenMonHoc = TenMonHoc;
            this.SoTietLyThuyet = SoTietLyThuyet;
            this.SoTietThucHanh = SoTietThucHanh;
        }
    }
    public class GiaoVien
    {
        [Key]
        [MaxLength(6)]
        public string MaGiaoVien { get; set; }
        [Required]
        [MaxLength(30)]
        public string HoGiaoVien { get; set; }
        [Required]
        [MaxLength(30)]
        public string TenGiaoVien { get; set; }
        [Required]
        [MaxLength(3)]
        public string GioiTinh { get; set; }
        [Required]
        public DateTime NgaySinh { get; set; }
        [Required]
        [MaxLength(6)]
        public string MaKhoa { get; set; }
        [Required]
        [MaxLength(10)]
        public string SoDienThoai { get; set; }
        [Required]
        [MaxLength(30)]
        public string DiaChi { get; set; }
        [MaxLength(50)]
        public string Email { get; set; }
        [Required]
        [MaxLength(6)]
        public string MaQueQuan { get; set; }
        [Required]
        [MaxLength(6)]
        public string MaDanToc { get; set; }
        [Required]
        [MaxLength(6)]
        public string MaTonGiao { get; set; }
        [ForeignKey("MaGVCN")]
        public ICollection<Lop> DSL { get; set; }
        public ICollection<PhanCong> DSPC { get; set; }
        public QueQuan QueQuan { get; set; }
        public DanToc DanToc { get; set; }
        public TonGiao TonGiao { get; set; }
        public GiaoVien()
        {

        }
        public GiaoVien(string MaGiaoVien, string HoGiaoVien, string TenGiaoVien, string GioiTinh, DateTime NgaySinh, string MaKhoa, string SoDienThoai, string DiaChi, string Email, string MaQueQuan, string MaDanToc, string MaTonGiao)
        {
            this.MaGiaoVien = MaGiaoVien;
            this.HoGiaoVien = HoGiaoVien;
            this.TenGiaoVien = TenGiaoVien;
            this.GioiTinh = GioiTinh;
            this.NgaySinh = DateTime.Now;
            this.MaKhoa = MaKhoa;
            this.SoDienThoai = SoDienThoai;
            this.DiaChi = DiaChi;
            this.Email = Email;
            this.MaQueQuan = MaQueQuan;
            this.MaDanToc = MaDanToc;
            this.MaTonGiao = MaTonGiao;
        }
    }
    public class Lop
    {
        [Key]
        [MaxLength(6)]
        public string MaLop { get; set; }
        [Required]
        [MaxLength(30)]
        public string TenLop { get; set; }
        [Required]
        [MaxLength(6)]
        public string MaNganhHoc { get; set; }
        [Required]
        [MaxLength(6)]
        public string MaGVCN { get; set; }
        public ICollection<PhanCong> DSPC { get; set; }
        public ICollection<SinhVien> DSSV { get; set; }
        public GiaoVien GiaoVien { get; set; }
        public Lop()
        {

        }
        public Lop(string MaLop, string TenLop, string MaNganhHoc, string MaGVCN)
        {
            this.MaLop = MaLop;
            this.TenLop = TenLop;
            this.MaNganhHoc = MaNganhHoc;
            this.MaGVCN = MaGVCN;
        }
    }
    public class QueQuan
    {
        [Key]
        [MaxLength(6)]
        public string MaQueQuan { get; set; }
        [Required]
        [MaxLength(30)]
        public string TenTinhThanhPho { get; set; }
        [Required]
        [MaxLength(30)]
        public string TenQuanHuyen { get; set; }
        [Required]
        [MaxLength(30)]
        public string TenPhuongXa { get; set; }
        public QueQuan()
        {

        }
        public ICollection<SinhVien> DSSV { get; set; }
        public ICollection<GiaoVien> DSGV { get; set; }
        public QueQuan(string MaQueQuan, string TenTinhThanhPho, string TenQuanHuyen, string TenPhuongXa)
        {
            this.MaQueQuan = MaQueQuan;
            this.TenTinhThanhPho = TenTinhThanhPho;
            this.TenQuanHuyen = TenQuanHuyen;
            this.TenPhuongXa = TenPhuongXa;
        }
    }
    public class DanToc
    {
        [Key]
        [MaxLength(6)]
        public string MaDanToc { get; set; }
        [Required]
        [MaxLength(30)]
        public string TenDanToc { get; set; }
        public ICollection<SinhVien> DSSV { get; set; }
        public ICollection<GiaoVien> DSGV { get; set; }
        public DanToc()
        {

        }
        public DanToc(string MaDanToc, string TenDanToc)
        {
            this.MaDanToc = MaDanToc;
            this.TenDanToc = TenDanToc;
        }
    }
    public class TonGiao
    {
        [Key]
        [MaxLength(6)]
        public string MaTonGiao { get; set; }
        [Required]
        [MaxLength(30)]
        public string TenTonGiao { get; set; }
        public ICollection<SinhVien> DSSV { get; set; }
        public ICollection<GiaoVien> DSGV { get; set; }
        public TonGiao()
        {

        }
        public TonGiao(string MaTonGiao, string TenTonGiao)
        {
            this.MaTonGiao = MaTonGiao;
            this.TenTonGiao = TenTonGiao;
        }
    }
    public class PhanCong
    {
        [Key]
        [MaxLength(6)]
        public string MaPhanCong { get; set; }
        [Required]
        [MaxLength(6)]
        public string MaMonHoc { get; set; }
        [Required]
        [MaxLength(6)]
        public string MaGiaoVien { get; set; }
        [MaxLength(6)]
        public string MaLop { get; set; }
        [Required]
        public int HocKy { get; set; }
        [Required]
        public int Nam { get; set; }
        [Required]
        public DateTime NgayBatDau { get; set; }
        [Required]
        public DateTime NgayKetThuc { get; set; }
        public ICollection<KetQua> DSKQ { get; set; }
        public Lop Lop { get; set; }
        public GiaoVien GiaoVien { get; set; }
        public MonHoc MonHoc { get; set; }

        public PhanCong()
        {

        }
        public PhanCong(string MaPhanCong, string MaMonHoc, string MaGiaoVien, string MaLop, int HocKy, int Nam, DateTime NgayBatDau, DateTime NgayKetThuc)
        {
            this.MaPhanCong = MaPhanCong;
            this.MaMonHoc = MaMonHoc;
            this.MaGiaoVien = MaGiaoVien;
            this.MaLop = MaLop;
            this.HocKy = HocKy;
            this.Nam = Nam;
            this.NgayBatDau = NgayBatDau;
            this.NgayKetThuc = NgayKetThuc;
        }
    }
    public class SinhVien
    {
        [Key]
        [MaxLength(6)]
        public string MaSinhVien { get; set; }
        [Required]
        [MaxLength(30)]
        public string HoSinhVien { get; set; }
        [Required]
        [MaxLength(30)]
        public string TenSinhVien { get; set; }
        [MaxLength(6)]
        public string MaLop { get; set; }
        [Required]
        [MaxLength(3)]
        public string GioiTinh { get; set; }
        [Required]
        public DateTime NgaySinh { get; set; }
        [Required]
        [MaxLength(30)]
        public string DiaChi { get; set; }
        [MaxLength(6)]
        public string MaQueQuan { get; set; }
        [MaxLength(6)]
        public string MaDanToc { get; set; }
        [MaxLength(6)]
        public string MaTonGiao { get; set; }
        [MaxLength(30)]
        public string HocBong { get; set; }
        public ICollection<KetQua> DSKQ { get; set; }
        public Lop Lop { get; set; }
        public QueQuan QueQuan { get; set; }
        public DanToc DanToc { get; set; }
        public TonGiao TonGiao { get; set; }

        public SinhVien()
        {

        }
        public SinhVien(string MaSinhVien,string HoSinhVien, string TenSinhVien, string MaLop, string GioiTinh, DateTime NgaySinh, string DiaChi, string MaQueQuan, string MaDanToc, string MaTonGiao, string HocBong)
        {
            this.MaSinhVien = MaSinhVien;
            this.HoSinhVien = HoSinhVien;
            this.TenSinhVien = TenSinhVien;
            this.MaLop = MaLop;
            this.GioiTinh = GioiTinh;
            this.NgaySinh = NgaySinh;
            this.DiaChi = DiaChi;
            this.MaQueQuan = MaQueQuan;
            this.MaDanToc = MaDanToc;
            this.MaTonGiao = MaTonGiao;
            this.HocBong = HocBong;
        }
    }
    public class KetQua
    {
        [Key]
        [Column(Order = 1)]
        [MaxLength(6)]
        public string MaPhanCong { get; set; }
        [Key]
        [Column(Order = 2)]
        [MaxLength(6)]
        public string MaSinhVien { get; set; }
        [Key]
        [Column(Order = 3)]
        public int LanThi { get; set; }
        [Required]
        public float Diem { get; set; }
        [MaxLength(10)]
        public string GhiChu { get; set; }
        public PhanCong PhanCong { get; set; }
        public SinhVien SinhVien { get; set; }
        public KetQua()
        {

        }
        public KetQua(string MaPhanCong, string MaSinhVien, int LanThi, float Diem, string GhiChu)
        {
            this.MaPhanCong = MaPhanCong;
            this.MaSinhVien = MaSinhVien;
            this.LanThi = LanThi;
            this.Diem = Diem;
            this.GhiChu = GhiChu;
        }
    }
    public class PhanQuyen
    {
        [Key]
        [Column(Order = 1)]
        [MaxLength(6)]
        public string ID { get; set; }
        [Key]
        [Column(Order = 2)]
        [MaxLength(30)]
        public string TenDangNhap { get; set; }
        [MaxLength(30)]
        public string TenUser { get; set; }
        [MaxLength(30)]
        public string MatKhau { get; set; }
        [MaxLength(10)]
        public string Quyen { get; set; }
        public PhanQuyen()
        {

        }
        public PhanQuyen(string ID,string TenDangNhap,string TenUser, string MatKhau, string Quyen)
        {
            this.ID = ID;
            this.TenDangNhap = TenDangNhap;
            this.TenUser = TenUser;
            this.MatKhau = MatKhau;
            this.Quyen = Quyen;
        }
    }
    public class MyEntity : DbContext
    {
        public MyEntity()
            : base("QuanLySinhVienConnectionString")
        {

        }
        public DbSet<MonHoc> MonHocs { get; set; }
        public DbSet<GiaoVien> GiaoViens { get; set; }
        public DbSet<Lop> Lops { get; set; }
        public DbSet<QueQuan> QueQuans { get; set; }
        public DbSet<DanToc> DanTocs { get; set; }
        public DbSet<TonGiao> TonGiaos { get; set; }
        public DbSet<PhanCong> PhanCongs { get; set; }
        public DbSet<SinhVien> SinhViens { get; set; }
        public DbSet<KetQua> KetQuas { get; set; }
        public DbSet<PhanQuyen> PhanQuyens { get; set; }
    }
}


