using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DeTaiWin
{
    /// <summary>
    /// Interaction logic for w_GiaoVien.xaml
    /// </summary>
    public partial class w_GiaoVien : UserControl
    {
        string TenDangNhap = ""; string MatKhau = ""; string Quyen = "";
        public w_GiaoVien(string TenDangNhap,string MatKhau,string Quyen)
        {
            InitializeComponent();
            this.TenDangNhap = TenDangNhap;
            this.MatKhau = MatKhau;
            this.Quyen = Quyen;
            using (var db = new MyEntity())
            {
                dtgGiaoVien.ItemsSource = db.GiaoViens.ToList();
            }
            using (var ab = new MyEntity())
            {
                cbMaQueQuan.ItemsSource = ab.QueQuans.ToList();
                cbMaQueQuan.DisplayMemberPath = "TenTinhThanhPho";
                // cbPhongBan.SelectedIndex = 0;
                cbMaQueQuan.SelectedValuePath = "MaQueQuan";
            }
            using (var bb = new MyEntity())
            {
                cbMaDanToc.ItemsSource = bb.DanTocs.ToList();
                cbMaDanToc.DisplayMemberPath = "TenDanToc";
                // cbPhongBan.SelectedIndex = 0;
                cbMaDanToc.SelectedValuePath = "MaDanToc";
            }
            using (var cb = new MyEntity())
            {
                cbMaTonGiao.ItemsSource = cb.TonGiaos.ToList();
                cbMaTonGiao.DisplayMemberPath = "TenTonGiao";
                // cbPhongBan.SelectedIndex = 0;
                cbMaTonGiao.SelectedValuePath = "MaTonGiao";
            }
        }
        public w_GiaoVien()
        {
            InitializeComponent();
            using (var db = new MyEntity())
            {
                dtgGiaoVien.ItemsSource = db.GiaoViens.ToList();
            }
            using (var ab = new MyEntity())
            {
                cbMaQueQuan.ItemsSource = ab.QueQuans.ToList();
                cbMaQueQuan.DisplayMemberPath = "TenTinhThanhPho";
                // cbPhongBan.SelectedIndex = 0;
                cbMaQueQuan.SelectedValuePath = "MaQueQuan";
            }
            using (var bb = new MyEntity())
            {
                cbMaDanToc.ItemsSource = bb.DanTocs.ToList();
                cbMaDanToc.DisplayMemberPath = "TenDanToc";
                // cbPhongBan.SelectedIndex = 0;
                cbMaDanToc.SelectedValuePath = "MaDanToc";
            }
            using (var cb = new MyEntity())
            {
                cbMaTonGiao.ItemsSource = cb.TonGiaos.ToList();
                cbMaTonGiao.DisplayMemberPath = "TenTonGiao";
                // cbPhongBan.SelectedIndex = 0;
                cbMaTonGiao.SelectedValuePath = "MaTonGiao";
            }
        }

        private void btThem_Click(object sender, RoutedEventArgs e)
        {
            if (Quyen == "Admin")
            {
                using (var db = new MyEntity())
                {
                    var gv = new GiaoVien(txtMaGiaoVien.Text, txtHoGiaoVien.Text, txtTenGiaoVien.Text, cbGioiTinh.Text, DateTime.Parse(dtpNgaySinh.Text), txtMaKhoa.Text, txtSoDienThoai.Text, txtDiaChi.Text, txtEmail.Text, cbMaQueQuan.SelectedValue.ToString(), cbMaDanToc.SelectedValue.ToString(), cbMaTonGiao.SelectedValue.ToString());
                    if (gv != null)
                    {
                        db.GiaoViens.Add(gv);
                        db.SaveChanges();
                        dtgGiaoVien.ItemsSource = db.GiaoViens.ToList();
                    }
                }
                txtMaGiaoVien.Text = "";
                txtHoGiaoVien.Text = "";
                txtTenGiaoVien.Text = "";
                cbGioiTinh.SelectedIndex = 0;
                dtpNgaySinh.DisplayDate = DateTime.Now;
                txtMaKhoa.Text = "";
                txtSoDienThoai.Text = "";
                txtDiaChi.Text = "";
                txtEmail.Text = "";
                cbMaQueQuan.SelectedIndex = 0;
                cbMaDanToc.SelectedIndex = 0;
                cbMaTonGiao.SelectedIndex = 0;
                txtMaGiaoVien.Focus();
            }
            else if(Quyen == "GiaoVien")
            {
                MessageBox.Show("Bạn không được quyền làm thao tác này", "Xin lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btXoa_Click(object sender, RoutedEventArgs e)
        {
            if (Quyen == "Admin")
            {
                if (dtgGiaoVien.SelectedItem != null)
                {
                    if (MessageBox.Show("Bạn có muốn xóa giáo viên này không?", "Xoa Giao Vien", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        using (var db = new MyEntity())
                        {
                            GiaoVien gv = dtgGiaoVien.SelectedItem as GiaoVien;
                            try
                            {
                                foreach (var item in db.GiaoViens.ToList())
                                {
                                    if (item.MaGiaoVien == gv.MaGiaoVien)
                                    {
                                        db.GiaoViens.Remove(item);
                                        db.SaveChanges();
                                        dtgGiaoVien.ItemsSource = db.GiaoViens.ToList();
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Bạn phải chọn một giáo viên");
                }
            }
            else if (Quyen == "GiaoVien")
            {
                MessageBox.Show("Bạn không được quyền làm thao tác này", "Xin lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btSua_Click(object sender, RoutedEventArgs e)
        {
            if (Quyen == "Admin")
            {
                GiaoVien capnhat = new GiaoVien();
                capnhat = (GiaoVien)dtgGiaoVien.SelectedItem;
                if (dtgGiaoVien.SelectedItem != null)
                {
                    using (var db = new MyEntity())
                    {
                        try
                        {
                            foreach (var item in db.GiaoViens.ToList())
                            {
                                if (item.MaGiaoVien == capnhat.MaGiaoVien)
                                {
                                    item.HoGiaoVien = txtHoGiaoVien.Text;
                                    item.TenGiaoVien = txtTenGiaoVien.Text;
                                    item.GioiTinh = cbGioiTinh.Text;
                                    item.NgaySinh = DateTime.Parse(dtpNgaySinh.Text);
                                    item.MaKhoa = txtMaKhoa.Text;
                                    item.SoDienThoai = txtSoDienThoai.Text;
                                    item.DiaChi = txtDiaChi.Text;
                                    item.Email = txtEmail.Text;
                                    item.MaQueQuan = cbMaQueQuan.SelectedItem.ToString();
                                    item.MaDanToc = cbMaDanToc.SelectedItem.ToString();
                                    item.MaTonGiao = cbMaTonGiao.SelectedItem.ToString();
                                    db.SaveChanges();
                                    dtgGiaoVien.ItemsSource = db.GiaoViens.ToList();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Bạn phải chọn một giáo viên");
                }
            }
            else if (Quyen == "GiaoVien")
            {
                MessageBox.Show("Bạn không được quyền làm thao tác này", "Xin lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void dtgGiaoVien_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var giaovien = (GiaoVien)dtgGiaoVien.SelectedItem;
            if (giaovien != null)
            {
                txtMaGiaoVien.Text = giaovien.MaGiaoVien;
                txtHoGiaoVien.Text = giaovien.HoGiaoVien;
                txtTenGiaoVien.Text = giaovien.TenGiaoVien;
                cbGioiTinh.Text = giaovien.GioiTinh;
                dtpNgaySinh.Text = giaovien.NgaySinh.ToString();
                txtMaKhoa.Text = giaovien.MaKhoa;
                txtSoDienThoai.Text = giaovien.SoDienThoai;
                txtDiaChi.Text = giaovien.DiaChi;
                txtEmail.Text = giaovien.Email;
                    using (var db = new MyEntity())
                    {
                        var quequan = db.QueQuans.Find(giaovien.MaQueQuan);
                        cbMaQueQuan.SelectedIndex = db.QueQuans.ToList().IndexOf(quequan);
                    }
                using (var ab = new MyEntity())
                {
                    var dantoc = ab.DanTocs.Find(giaovien.MaDanToc);
                    cbMaDanToc.SelectedIndex = ab.DanTocs.ToList().IndexOf(dantoc);
                }
                using (var bb = new MyEntity())
                {
                    var tongiao = bb.TonGiaos.Find(giaovien.MaTonGiao);
                    cbMaTonGiao.SelectedIndex = bb.TonGiaos.ToList().IndexOf(tongiao);
                }
            }
        }

        private void btTim_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new MyEntity())
            {
                var query = from p in db.GiaoViens
                            where p.TenGiaoVien == txtTim.Text
                            select p;
                dtgGiaoVien.ItemsSource = query.ToList();

            }
        }

        private void btIn_Click(object sender, RoutedEventArgs e)
        {

        }

        //private void dtgGiaoVien_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    var giaovien = (GiaoVien)dtgGiaoVien.SelectedItem;
        //    if (giaovien != null)
        //    {
        //        txtMaGiaoVien.Text = giaovien.MaGiaoVien;
        //        txtHoGiaoVien.Text = giaovien.HoGiaoVien;
        //        txtTenGiaoVien.Text = giaovien.TenGiaoVien;
        //        cbGioiTinh.Text = giaovien.GioiTinh;
        //        dtpNgaySinh.Text = giaovien.NgaySinh.ToString();
        //        txtMaKhoa.Text = giaovien.MaKhoa;
        //        txtSoDienThoai.Text = giaovien.SoDienThoai;
        //        txtDiaChi.Text = giaovien.DiaChi;
        //        txtEmail.Text = giaovien.Email;
        //        using (var db = new MyEntity())
        //        {
        //            var quequan = db.QueQuans.Find(giaovien.MaQueQuan);
        //            cbMaQueQuan.SelectedIndex = db.QueQuans.ToList().IndexOf(quequan);
        //        }
        //        using (var ab = new MyEntity())
        //        {
        //            var dantoc = ab.DanTocs.Find(giaovien.MaDanToc);
        //            cbMaDanToc.SelectedIndex = ab.DanTocs.ToList().IndexOf(dantoc);
        //        }
        //        using (var bb = new MyEntity())
        //        {
        //            var tongiao = bb.TonGiaos.Find(giaovien.MaTonGiao);
        //            cbMaTonGiao.SelectedIndex = bb.TonGiaos.ToList().IndexOf(tongiao);
        //        }
        //    }
        //}
    }
}
