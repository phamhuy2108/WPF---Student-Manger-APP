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
    /// Interaction logic for w_KetQua.xaml
    /// </summary>
    public partial class w_KetQua : UserControl
    {
        string TenDangNhap = ""; string MatKhau = ""; string Quyen = "";
        public w_KetQua(string TenDangNhap, string MatKhau, string Quyen)
        {
            InitializeComponent();
            this.TenDangNhap = TenDangNhap;
            this.MatKhau = MatKhau;
            this.Quyen = Quyen;
            using (var db = new MyEntity())
            {
                dtgKetQua.ItemsSource = db.KetQuas.ToList();
            }
            using (var ab = new MyEntity())
            {
                cbMaPhanCong.ItemsSource = ab.PhanCongs.ToList();
                cbMaPhanCong.DisplayMemberPath = "MaPhanCong";
                // cbPhongBan.SelectedIndex = 0;
                cbMaPhanCong.SelectedValuePath = "MaPhanCong";
            }
            using (var bb = new MyEntity())
            {
                cbMaSinhVien.ItemsSource = bb.SinhViens.ToList();
                cbMaSinhVien.DisplayMemberPath = "MaSinhVien";
                // cbPhongBan.SelectedIndex = 0;
                cbMaSinhVien.SelectedValuePath = "MaSinhVien";
            }
        }

        private void btThem_Click(object sender, RoutedEventArgs e)
        {
            if (Quyen == "Admin")
            {
                using (var db = new MyEntity())
                {
                    var nv = new KetQua(cbMaPhanCong.SelectedValue.ToString(), cbMaSinhVien.SelectedValue.ToString(), int.Parse(txtLanThi.Text), float.Parse(txtDiem.Text), txtGhiChu.Text);
                    if (nv != null)
                    {
                        db.KetQuas.Add(nv);
                        db.SaveChanges();
                        dtgKetQua.ItemsSource = db.KetQuas.ToList();
                    }
                }
                cbMaPhanCong.Text = "";
                cbMaSinhVien.Text = "";
                txtLanThi.Text = "";
                txtDiem.Text = "";
                txtGhiChu.Text = "";
                cbMaPhanCong.Focus();
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
                if (dtgKetQua.SelectedItem != null)
                {
                    if (MessageBox.Show("Bạn có muốn xóa kết quả này không?", "Xoa Ket Qua", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        using (var db = new MyEntity())
                        {
                            KetQua kq = dtgKetQua.SelectedItem as KetQua;
                            try
                            {
                                foreach (var item in db.KetQuas.ToList())
                                {
                                    if (item.MaPhanCong == kq.MaPhanCong)
                                    {
                                        db.KetQuas.Remove(item);
                                        db.SaveChanges();
                                        dtgKetQua.ItemsSource = db.KetQuas.ToList();
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
                    MessageBox.Show("Bạn phải chọn một kết quả");
                }
            }
            else if (Quyen == "GiaoVien")
            {
                if (dtgKetQua.SelectedItem != null)
                {
                    if (MessageBox.Show("Bạn có muốn xóa kết quả này không?", "Xoa Ket Qua", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        using (var db = new MyEntity())
                        {
                            KetQua kq = dtgKetQua.SelectedItem as KetQua;
                            try
                            {
                                foreach (var item in db.KetQuas.ToList())
                                {
                                    if (item.MaPhanCong == kq.MaPhanCong)
                                    {
                                        db.KetQuas.Remove(item);
                                        db.SaveChanges();
                                        dtgKetQua.ItemsSource = db.KetQuas.ToList();
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
                    MessageBox.Show("Bạn phải chọn một kết quả");
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
                KetQua capnhat = new KetQua();
                capnhat = (KetQua)dtgKetQua.SelectedItem;
                if (dtgKetQua.SelectedItem != null)
                {
                    using (var db = new MyEntity())
                    {
                        try
                        {
                            foreach (var item in db.KetQuas.ToList())
                            {
                                if (item.MaPhanCong == capnhat.MaPhanCong)
                                {
                                    item.LanThi = int.Parse(txtLanThi.Text);
                                    item.Diem = float.Parse(txtDiem.Text);
                                    item.GhiChu = txtGhiChu.Text;
                                    db.SaveChanges();
                                    dtgKetQua.ItemsSource = db.KetQuas.ToList();
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
                    MessageBox.Show("Bạn phải chọn một kết quả");
                }
            }
        }

        private void dtgKetQua_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var ketqua = (KetQua)dtgKetQua.SelectedItem;
            if (ketqua != null)
            {
                using (var db = new MyEntity())
                {
                    var phancong = db.PhanCongs.Find(ketqua.MaPhanCong);
                    cbMaPhanCong.SelectedIndex = db.PhanCongs.ToList().IndexOf(phancong);
                }
                using (var ab = new MyEntity())
                {
                    var sinhvien = ab.SinhViens.Find(ketqua.MaSinhVien);
                    cbMaSinhVien.SelectedIndex = ab.SinhViens.ToList().IndexOf(sinhvien);
                }
                txtLanThi.Text = ketqua.LanThi.ToString();
                txtDiem.Text = ketqua.Diem.ToString();
            }
        }

        private void btTim_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new MyEntity())
            {
                var query = from p in db.KetQuas
                            where p.MaSinhVien == txtTim.Text
                            select p;
                dtgKetQua.ItemsSource = query.ToList();

            }

        }

        private void btIn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

