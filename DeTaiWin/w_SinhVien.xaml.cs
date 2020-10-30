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
    /// Interaction logic for w_SinhVien.xaml
    /// </summary>
    public partial class w_SinhVien : UserControl
    {
        string TenDangNhap = ""; string MatKhau = ""; string Quyen = "";
        public w_SinhVien(string TenDangNhap, string MatKhau, string Quyen)
        {
            InitializeComponent();
            this.TenDangNhap = TenDangNhap;
            this.MatKhau = MatKhau;
            this.Quyen = Quyen;
            using (var db = new MyEntity())
            {
                dtgSinhVien.ItemsSource = db.SinhViens.ToList();
            }
            using (var ab = new MyEntity())
            {
                cbMaLop.ItemsSource = ab.Lops.ToList();
                cbMaLop.DisplayMemberPath = "TenLop";
                // cbPhongBan.SelectedIndex = 0;
                cbMaLop.SelectedValuePath = "MaLop";
            }
            using (var eb = new MyEntity())
            {
                cbMaQueQuan.ItemsSource = eb.QueQuans.ToList();
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

        private void dtgSinhVien_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var sinhvien = (SinhVien)dtgSinhVien.SelectedItem;
            if (sinhvien != null)
            {
                txtMaSinhVien.Text = sinhvien.MaSinhVien;
                txtTenSinhVien.Text = sinhvien.TenSinhVien;
                using (var cb = new MyEntity())
                {
                    var lop = cb.Lops.Find(sinhvien.MaLop);
                    cbMaLop.SelectedIndex = cb.Lops.ToList().IndexOf(lop);
                }
                cbMaLop.Text = sinhvien.MaLop;
                cbGioiTinh.Text = sinhvien.GioiTinh;
                dtpNgaySinh.Text = sinhvien.GioiTinh.ToString();
                txtDiaChi.Text = sinhvien.DiaChi;
                using (var db = new MyEntity())
                {
                    var quequan = db.QueQuans.Find(sinhvien.MaQueQuan);
                    cbMaQueQuan.SelectedIndex = db.QueQuans.ToList().IndexOf(quequan);
                }
                using (var ab = new MyEntity())
                {
                    var dantoc = ab.DanTocs.Find(sinhvien.MaDanToc);
                    cbMaDanToc.SelectedIndex = ab.DanTocs.ToList().IndexOf(dantoc);
                }
                using (var bb = new MyEntity())
                {
                    var tongiao = bb.TonGiaos.Find(sinhvien.MaTonGiao);
                    cbMaTonGiao.SelectedIndex = bb.TonGiaos.ToList().IndexOf(tongiao);
                }
                txtHocBong.Text = sinhvien.HocBong;
            }
        }

        private void btThem_Click(object sender, RoutedEventArgs e)
        {
            if (Quyen == "Admin")
            {
                using (var db = new MyEntity())
                {
                    var sv = new SinhVien(txtMaSinhVien.Text, txtHoSinhVien.Text, txtTenSinhVien.Text, cbMaLop.SelectedValue.ToString(), cbGioiTinh.Text, DateTime.Parse(dtpNgaySinh.Text), txtDiaChi.Text, cbMaQueQuan.SelectedValue.ToString(), cbMaDanToc.SelectedValue.ToString(), cbMaTonGiao.SelectedValue.ToString(), txtHocBong.Text);
                    if (sv != null)
                    {
                        db.SinhViens.Add(sv);
                        db.SaveChanges();
                        dtgSinhVien.ItemsSource = db.SinhViens.ToList();
                    }
                }
                txtMaSinhVien.Text = "";
                txtTenSinhVien.Text = "";
                cbMaLop.SelectedIndex = 0;
                cbGioiTinh.SelectedIndex = 0;
                dtpNgaySinh.DisplayDate = DateTime.Now;
                txtDiaChi.Text = "";
                cbMaQueQuan.SelectedIndex = 0;
                cbMaDanToc.SelectedIndex = 0;
                cbMaTonGiao.SelectedIndex = 0;
                txtHocBong.Text = "";
                txtMaSinhVien.Focus();
            }
            else
            {
                MessageBox.Show("Bạn không được quyền làm thao tác này", "Xin lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            }

        private void btXoa_Click(object sender, RoutedEventArgs e)
        {
            if (Quyen == "Admin")
            {
                if (dtgSinhVien.SelectedItem != null)
                {
                    if (MessageBox.Show("Bạn có muốn xóa sinh viên này không?", "Xoa Sinh Vien", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        using (var db = new MyEntity())
                        {
                            SinhVien sv = dtgSinhVien.SelectedItem as SinhVien;
                            try
                            {
                                foreach (var item in db.SinhViens.ToList())
                                {
                                    if (item.MaSinhVien == sv.MaSinhVien)
                                    {
                                        db.SinhViens.Remove(item);
                                        db.SaveChanges();
                                        dtgSinhVien.ItemsSource = db.SinhViens.ToList();
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
                    MessageBox.Show("Bạn phải chọn một sinh viên");
                }
            }
            else if (Quyen == "GiaoVien")
            {
                if (dtgSinhVien.SelectedItem != null)
                {
                    if (MessageBox.Show("Bạn có muốn xóa sinh viên này không?", "Xoa Sinh Vien", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        using (var db = new MyEntity())
                        {
                            SinhVien sv = dtgSinhVien.SelectedItem as SinhVien;
                            try
                            {
                                foreach (var item in db.SinhViens.ToList())
                                {
                                    if (item.MaSinhVien == sv.MaSinhVien)
                                    {
                                        db.SinhViens.Remove(item);
                                        db.SaveChanges();
                                        dtgSinhVien.ItemsSource = db.SinhViens.ToList();
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
                    MessageBox.Show("Bạn phải chọn một sinh viên");
                }
            }
            else
            {
                MessageBox.Show("Bạn không được quyền làm thao tác này", "Xin lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btSua_Click(object sender, RoutedEventArgs e)
        {
            if (Quyen == "Admin")
            {
                SinhVien capnhat = new SinhVien();
                capnhat = (SinhVien)dtgSinhVien.SelectedItem;
                if (dtgSinhVien.SelectedItem != null)
                {
                    using (var db = new MyEntity())
                    {
                        try
                        {
                            foreach (var item in db.SinhViens.ToList())
                            {
                                if (item.MaSinhVien == capnhat.MaSinhVien)
                                {
                                    item.TenSinhVien = txtTenSinhVien.Text;
                                    item.MaLop = cbMaLop.Text;
                                    item.GioiTinh = cbGioiTinh.Text;
                                    item.NgaySinh = DateTime.Parse(dtpNgaySinh.Text);
                                    item.DiaChi = txtDiaChi.Text;
                                    item.MaQueQuan = cbMaQueQuan.Text;
                                    item.MaDanToc = cbMaDanToc.Text;
                                    item.MaTonGiao = cbMaTonGiao.Text;
                                    item.HocBong = txtHocBong.Text;
                                    db.SaveChanges();
                                    dtgSinhVien.ItemsSource = db.SinhViens.ToList();
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
                    MessageBox.Show("Bạn phải chọn một sinh viên");
                }
            }
            else
            {
                MessageBox.Show("Bạn không được quyền làm thao tác này", "Xin lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btTim_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new MyEntity())
            {
                var query = from p in db.SinhViens
                            where p.TenSinhVien == txtTim.Text
                            select p;
                dtgSinhVien.ItemsSource = query.ToList();

            }
        }
    }
}
