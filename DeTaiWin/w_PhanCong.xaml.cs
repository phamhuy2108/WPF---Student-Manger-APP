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
    /// Interaction logic for w_PhanCong.xaml
    /// </summary>
    public partial class w_PhanCong : UserControl
    {
        string TenDangNhap = ""; string MatKhau = ""; string Quyen = "";
        public w_PhanCong(string TenDangNhap, string MatKhau, string Quyen)
        {
            InitializeComponent();
            this.TenDangNhap = TenDangNhap;
            this.MatKhau = MatKhau;
            this.Quyen = Quyen;
            using (var db = new MyEntity())
            {
                dtgPhanCong.ItemsSource = db.PhanCongs.ToList();
            }
            using (var ab = new MyEntity())
            {
                cbMaMonHoc.ItemsSource = ab.MonHocs.ToList();
                cbMaMonHoc.DisplayMemberPath = "TenMonHoc";
                // cbPhongBan.SelectedIndex = 0;
                cbMaMonHoc.SelectedValuePath = "MaMonHoc";
            }
            using (var ab = new MyEntity())
            {
                cbMaGiaoVien.ItemsSource = ab.GiaoViens.ToList();
                cbMaGiaoVien.DisplayMemberPath = "TenGiaoVien";
                // cbPhongBan.SelectedIndex = 0;
                cbMaGiaoVien.SelectedValuePath = "MaGiaoVien";
            }
            using (var bb = new MyEntity())
            {
                cbMaLop.ItemsSource = bb.Lops.ToList();
                cbMaLop.DisplayMemberPath = "TenLop";
                // cbPhongBan.SelectedIndex = 0;
                cbMaLop.SelectedValuePath = "MaLop";
            }
        }

        private void dtgPhanCong_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var phancong = (PhanCong)dtgPhanCong.SelectedItem;
            if (phancong != null)
            {
                txtMaPhanCong.Text = phancong.MaPhanCong;
                using (var db = new MyEntity())
                {
                    var monhoc = db.MonHocs.Find(phancong.MaMonHoc);
                    cbMaMonHoc.SelectedIndex = db.MonHocs.ToList().IndexOf(monhoc);
                }
                using (var ab = new MyEntity())
                {
                    var giaovien = ab.GiaoViens.Find(phancong.MaGiaoVien);
                    cbMaGiaoVien.SelectedIndex = ab.GiaoViens.ToList().IndexOf(giaovien);
                }
                using (var bb = new MyEntity())
                {
                    var lop = bb.Lops.Find(phancong.MaLop);
                    cbMaLop.SelectedIndex = bb.Lops.ToList().IndexOf(lop);
                }
                txtHocKy.Text = phancong.HocKy.ToString();
                txtNam.Text = phancong.Nam.ToString();
                dtpNgayBatDau.Text = phancong.NgayBatDau.ToString();
                dtpNgayKetThuc.Text = phancong.NgayKetThuc.ToString();
            }
        }
        private void btThem_Click(object sender, RoutedEventArgs e)
        {
            if (Quyen == "Admin")
            {
                using (var db = new MyEntity())
                {
                    var pc = new PhanCong(txtMaPhanCong.Text, cbMaMonHoc.SelectedValue.ToString(), cbMaGiaoVien.SelectedValue.ToString(), cbMaLop.SelectedValue.ToString(), int.Parse(txtHocKy.Text), int.Parse(txtNam.Text), DateTime.Parse(dtpNgayBatDau.Text), DateTime.Parse(dtpNgayKetThuc.Text));
                    if (pc != null)
                    {
                        db.PhanCongs.Add(pc);
                        db.SaveChanges();
                        dtgPhanCong.ItemsSource = db.PhanCongs.ToList();
                    }
                }
                txtMaPhanCong.Text = "";
                cbMaMonHoc.Text = "";
                cbMaGiaoVien.SelectedIndex = 0;
                cbMaLop.SelectedIndex = 0;
                txtHocKy.Text = "";
                txtNam.Text = "";
                dtpNgayBatDau.DisplayDate = DateTime.Now;
                dtpNgayKetThuc.DisplayDate = DateTime.Now;
            }
            else if (Quyen == "GiaoVien")
            {
                MessageBox.Show("Bạn không được quyền làm thao tác này", "Xin lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            }

        private void btXoa_Click(object sender, RoutedEventArgs e)
        {
            if (Quyen == "Admin")
            {
                if (dtgPhanCong.SelectedItem != null)
                {
                    if (MessageBox.Show("Bạn có muốn xóa phân công này không?", "Xoa Phan Cong", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        using (var db = new MyEntity())
                        {
                            PhanCong pc = dtgPhanCong.SelectedItem as PhanCong;
                            try
                            {
                                foreach (var item in db.PhanCongs.ToList())
                                {
                                    if (item.MaPhanCong == pc.MaPhanCong)
                                    {
                                        db.PhanCongs.Remove(item);
                                        db.SaveChanges();
                                        dtgPhanCong.ItemsSource = db.PhanCongs.ToList();
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
                    MessageBox.Show("Bạn chưa chọn nhân viên");
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
                PhanCong capnhat = new PhanCong();
                capnhat = (PhanCong)dtgPhanCong.SelectedItem;
                if (dtgPhanCong.SelectedItem != null)
                {
                    using (var db = new MyEntity())
                    {
                        try
                        {
                            foreach (var item in db.PhanCongs.ToList())
                            {
                                if (item.MaPhanCong == capnhat.MaPhanCong)
                                {
                                    item.MaPhanCong = txtMaPhanCong.Text;
                                    item.MaMonHoc = cbMaMonHoc.Text;
                                    item.MaGiaoVien = cbMaGiaoVien.Text;
                                    item.MaLop = cbMaLop.Text;
                                    item.HocKy = int.Parse(txtHocKy.Text);
                                    item.Nam = int.Parse(txtNam.Text);
                                    item.NgayBatDau = DateTime.Parse(dtpNgayBatDau.Text);
                                    item.NgayKetThuc = DateTime.Parse(dtpNgayKetThuc.Text);
                                    db.SaveChanges();
                                    dtgPhanCong.ItemsSource = db.PhanCongs.ToList();
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
                    MessageBox.Show("Bạn phải chọn một phân công");
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
                var query = from p in db.PhanCongs
                            where p.MaGiaoVien == txtTim.Text
                            select p;
                dtgPhanCong.ItemsSource = query.ToList();

            }
        }
    }
}
