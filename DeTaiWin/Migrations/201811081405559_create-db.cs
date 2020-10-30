namespace DeTaiWin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createdb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DanTocs",
                c => new
                    {
                        MaDanToc = c.String(nullable: false, maxLength: 6),
                        TenDanToc = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.MaDanToc);
            
            CreateTable(
                "dbo.GiaoViens",
                c => new
                    {
                        MaGiaoVien = c.String(nullable: false, maxLength: 6),
                        HoGiaoVien = c.String(nullable: false, maxLength: 30),
                        TenGiaoVien = c.String(nullable: false, maxLength: 30),
                        GioiTinh = c.String(nullable: false, maxLength: 3),
                        NgaySinh = c.DateTime(nullable: false),
                        MaKhoa = c.String(nullable: false, maxLength: 6),
                        SoDienThoai = c.String(nullable: false, maxLength: 10),
                        DiaChi = c.String(nullable: false, maxLength: 30),
                        Email = c.String(maxLength: 50),
                        MaQueQuan = c.String(nullable: false, maxLength: 6),
                        MaDanToc = c.String(nullable: false, maxLength: 6),
                        MaTonGiao = c.String(nullable: false, maxLength: 6),
                    })
                .PrimaryKey(t => t.MaGiaoVien)
                .ForeignKey("dbo.DanTocs", t => t.MaDanToc, cascadeDelete: true)
                .ForeignKey("dbo.QueQuans", t => t.MaQueQuan, cascadeDelete: true)
                .ForeignKey("dbo.TonGiaos", t => t.MaTonGiao, cascadeDelete: true)
                .Index(t => t.MaQueQuan)
                .Index(t => t.MaDanToc)
                .Index(t => t.MaTonGiao);
            
            CreateTable(
                "dbo.Lops",
                c => new
                    {
                        MaLop = c.String(nullable: false, maxLength: 6),
                        TenLop = c.String(nullable: false, maxLength: 30),
                        MaNganhHoc = c.String(nullable: false, maxLength: 6),
                        MaGVCN = c.String(nullable: false, maxLength: 6),
                    })
                .PrimaryKey(t => t.MaLop)
                .ForeignKey("dbo.GiaoViens", t => t.MaGVCN, cascadeDelete: true)
                .Index(t => t.MaGVCN);
            
            CreateTable(
                "dbo.PhanCongs",
                c => new
                    {
                        MaPhanCong = c.String(nullable: false, maxLength: 6),
                        MaMonHoc = c.String(nullable: false, maxLength: 6),
                        MaGiaoVien = c.String(nullable: false, maxLength: 6),
                        MaLop = c.String(maxLength: 6),
                        HocKy = c.Int(nullable: false),
                        Nam = c.Int(nullable: false),
                        NgayBatDau = c.DateTime(nullable: false),
                        NgayKetThuc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.MaPhanCong)
                .ForeignKey("dbo.GiaoViens", t => t.MaGiaoVien, cascadeDelete: true)
                .ForeignKey("dbo.Lops", t => t.MaLop)
                .ForeignKey("dbo.MonHocs", t => t.MaMonHoc, cascadeDelete: true)
                .Index(t => t.MaMonHoc)
                .Index(t => t.MaGiaoVien)
                .Index(t => t.MaLop);
            
            CreateTable(
                "dbo.KetQuas",
                c => new
                    {
                        MaPhanCong = c.String(nullable: false, maxLength: 6),
                        MaSinhVien = c.String(nullable: false, maxLength: 6),
                        LanThi = c.Int(nullable: false),
                        Diem = c.Single(nullable: false),
                        GhiChu = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => new { t.MaPhanCong, t.MaSinhVien, t.LanThi })
                .ForeignKey("dbo.PhanCongs", t => t.MaPhanCong, cascadeDelete: true)
                .ForeignKey("dbo.SinhViens", t => t.MaSinhVien, cascadeDelete: true)
                .Index(t => t.MaPhanCong)
                .Index(t => t.MaSinhVien);
            
            CreateTable(
                "dbo.SinhViens",
                c => new
                    {
                        MaSinhVien = c.String(nullable: false, maxLength: 6),
                        HoSinhVien = c.String(nullable: false, maxLength: 30),
                        TenSinhVien = c.String(nullable: false, maxLength: 30),
                        MaLop = c.String(maxLength: 6),
                        GioiTinh = c.String(nullable: false, maxLength: 3),
                        NgaySinh = c.DateTime(nullable: false),
                        DiaChi = c.String(nullable: false, maxLength: 30),
                        MaQueQuan = c.String(maxLength: 6),
                        MaDanToc = c.String(maxLength: 6),
                        MaTonGiao = c.String(maxLength: 6),
                        HocBong = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.MaSinhVien)
                .ForeignKey("dbo.DanTocs", t => t.MaDanToc)
                .ForeignKey("dbo.Lops", t => t.MaLop)
                .ForeignKey("dbo.QueQuans", t => t.MaQueQuan)
                .ForeignKey("dbo.TonGiaos", t => t.MaTonGiao)
                .Index(t => t.MaLop)
                .Index(t => t.MaQueQuan)
                .Index(t => t.MaDanToc)
                .Index(t => t.MaTonGiao);
            
            CreateTable(
                "dbo.QueQuans",
                c => new
                    {
                        MaQueQuan = c.String(nullable: false, maxLength: 6),
                        TenTinhThanhPho = c.String(nullable: false, maxLength: 30),
                        TenQuanHuyen = c.String(nullable: false, maxLength: 30),
                        TenPhuongXa = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.MaQueQuan);
            
            CreateTable(
                "dbo.TonGiaos",
                c => new
                    {
                        MaTonGiao = c.String(nullable: false, maxLength: 6),
                        TenTonGiao = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.MaTonGiao);
            
            CreateTable(
                "dbo.MonHocs",
                c => new
                    {
                        MaMonHoc = c.String(nullable: false, maxLength: 6),
                        TenMonHoc = c.String(nullable: false, maxLength: 30),
                        SoTietLyThuyet = c.Int(nullable: false),
                        SoTietThucHanh = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MaMonHoc);
            
            CreateTable(
                "dbo.PhanQuyens",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 6),
                        TenDangNhap = c.String(nullable: false, maxLength: 30),
                        TenUser = c.String(maxLength: 30),
                        MatKhau = c.String(maxLength: 30),
                        Quyen = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => new { t.ID, t.TenDangNhap });
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Lops", "MaGVCN", "dbo.GiaoViens");
            DropForeignKey("dbo.PhanCongs", "MaMonHoc", "dbo.MonHocs");
            DropForeignKey("dbo.PhanCongs", "MaLop", "dbo.Lops");
            DropForeignKey("dbo.PhanCongs", "MaGiaoVien", "dbo.GiaoViens");
            DropForeignKey("dbo.SinhViens", "MaTonGiao", "dbo.TonGiaos");
            DropForeignKey("dbo.GiaoViens", "MaTonGiao", "dbo.TonGiaos");
            DropForeignKey("dbo.SinhViens", "MaQueQuan", "dbo.QueQuans");
            DropForeignKey("dbo.GiaoViens", "MaQueQuan", "dbo.QueQuans");
            DropForeignKey("dbo.SinhViens", "MaLop", "dbo.Lops");
            DropForeignKey("dbo.KetQuas", "MaSinhVien", "dbo.SinhViens");
            DropForeignKey("dbo.SinhViens", "MaDanToc", "dbo.DanTocs");
            DropForeignKey("dbo.KetQuas", "MaPhanCong", "dbo.PhanCongs");
            DropForeignKey("dbo.GiaoViens", "MaDanToc", "dbo.DanTocs");
            DropIndex("dbo.SinhViens", new[] { "MaTonGiao" });
            DropIndex("dbo.SinhViens", new[] { "MaDanToc" });
            DropIndex("dbo.SinhViens", new[] { "MaQueQuan" });
            DropIndex("dbo.SinhViens", new[] { "MaLop" });
            DropIndex("dbo.KetQuas", new[] { "MaSinhVien" });
            DropIndex("dbo.KetQuas", new[] { "MaPhanCong" });
            DropIndex("dbo.PhanCongs", new[] { "MaLop" });
            DropIndex("dbo.PhanCongs", new[] { "MaGiaoVien" });
            DropIndex("dbo.PhanCongs", new[] { "MaMonHoc" });
            DropIndex("dbo.Lops", new[] { "MaGVCN" });
            DropIndex("dbo.GiaoViens", new[] { "MaTonGiao" });
            DropIndex("dbo.GiaoViens", new[] { "MaDanToc" });
            DropIndex("dbo.GiaoViens", new[] { "MaQueQuan" });
            DropTable("dbo.PhanQuyens");
            DropTable("dbo.MonHocs");
            DropTable("dbo.TonGiaos");
            DropTable("dbo.QueQuans");
            DropTable("dbo.SinhViens");
            DropTable("dbo.KetQuas");
            DropTable("dbo.PhanCongs");
            DropTable("dbo.Lops");
            DropTable("dbo.GiaoViens");
            DropTable("dbo.DanTocs");
        }
    }
}
