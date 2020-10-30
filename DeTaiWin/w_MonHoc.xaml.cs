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
    /// Interaction logic for w_MonHoc.xaml
    /// </summary>
    public partial class w_MonHoc : UserControl
    {
        string TenDangNhap = ""; string MatKhau = ""; string Quyen = "";
        public w_MonHoc(string TenDangNhap, string MatKhau, string Quyen)
        {
            InitializeComponent();
            this.TenDangNhap = TenDangNhap;
            this.MatKhau = MatKhau;
            this.Quyen = Quyen;
            using (var db = new MyEntity())
            {
                dtgMonHoc.ItemsSource = db.MonHocs.ToList();
            }
        }

        private void btThem_Click(object sender, RoutedEventArgs e)
        {
            if (Quyen == "Admin")
            {
                using (var db = new MyEntity())
                {
                    var mh = new MonHoc(txtMaMonHoc.Text, txtTenMonHoc.Text, int.Parse(txtSoTietLyThuyet.Text), int.Parse(txtSoTietThucHanh.Text));
                    if (mh != null)
                    {
                        db.MonHocs.Add(mh);
                        db.SaveChanges();
                        dtgMonHoc.ItemsSource = db.MonHocs.ToList();
                    }
                }
                txtMaMonHoc.Text = "";
                txtTenMonHoc.Text = "";
                txtSoTietLyThuyet.Text = "";
                txtSoTietThucHanh.Text = "";
                txtMaMonHoc.Focus();
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
                if (dtgMonHoc.SelectedItem != null)
                {
                    if (MessageBox.Show("Bạn có muốn xóa môn học này không?", "Xoa Mon Hoc", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        using (var db = new MyEntity())
                        {
                            MonHoc nv = dtgMonHoc.SelectedItem as MonHoc;
                            try
                            {
                                foreach (var item in db.MonHocs.ToList())
                                {
                                    if (item.MaMonHoc == nv.MaMonHoc)
                                    {
                                        db.MonHocs.Remove(item);
                                        db.SaveChanges();
                                        dtgMonHoc.ItemsSource = db.MonHocs.ToList();
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
                    MessageBox.Show("Bạn phải chọn một môn học");
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
                MonHoc capnhat = new MonHoc();
                capnhat = (MonHoc)dtgMonHoc.SelectedItem;
                if (dtgMonHoc.SelectedItem != null)
                {
                    using (var db = new MyEntity())
                    {
                        try
                        {
                            foreach (var item in db.MonHocs.ToList())
                            {
                                if (item.MaMonHoc == capnhat.MaMonHoc)
                                {
                                    item.TenMonHoc = txtTenMonHoc.Text;
                                    item.SoTietLyThuyet = int.Parse(txtSoTietLyThuyet.Text);
                                    item.SoTietThucHanh = int.Parse(txtSoTietThucHanh.Text);
                                    db.SaveChanges();
                                    dtgMonHoc.ItemsSource = db.MonHocs.ToList();
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
                    MessageBox.Show("Bạn phải chọn một môn học");
                }
            }
            else
            {
                MessageBox.Show("Bạn không được quyền làm thao tác này", "Xin lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void dtgMonHoc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var monhoc = (MonHoc)dtgMonHoc.SelectedItem;
            if (monhoc != null)
            {
                txtMaMonHoc.Text = monhoc.MaMonHoc;
                txtTenMonHoc.Text = monhoc.TenMonHoc;
                txtSoTietLyThuyet.Text = monhoc.SoTietLyThuyet.ToString();
                txtSoTietThucHanh.Text = monhoc.SoTietThucHanh.ToString();
            }
        }

        private void btTim_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new MyEntity())
            {
                var query = from p in db.MonHocs
                            where p.TenMonHoc == txtTim.Text
                            select p;
                dtgMonHoc.ItemsSource = query.ToList();

            }
        }
    }

    //private void dtgMonHoc_SelectionChanged(object sender, SelectionChangedEventArgs e)
    //{
    //    var monhoc = (MonHoc)dtgMonHoc.SelectedItem;
    //    if (monhoc != null)
    //    {
    //        txtMaMonHoc.Text = monhoc.MaMonHoc;
    //        txtTenMonHoc.Text = monhoc.TenMonHoc;
    //        txtSoTietLyThuyet.Text = monhoc.SoTietLyThuyet.ToString();
    //        txtSoTietThucHanh.Text = monhoc.SoTietThucHanh.ToString();
    //    }
    //}
}

