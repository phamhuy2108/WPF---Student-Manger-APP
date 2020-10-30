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
    /// Interaction logic for w_Lop.xaml
    /// </summary>
    public partial class w_Lop : UserControl
    {
        string TenDangNhap = ""; string MatKhau = ""; string Quyen = "";
        public w_Lop(string TenDangNhap, string MatKhau, string Quyen)
        {
            InitializeComponent();
            this.TenDangNhap = TenDangNhap;
            this.MatKhau = MatKhau;
            this.Quyen = Quyen;
            using (var db = new MyEntity())
            {
                dtgLop.ItemsSource = db.Lops.ToList();
            }
            using (var ab = new MyEntity())
            {
                cbMaGVCN.ItemsSource = ab.GiaoViens.ToList();
                cbMaGVCN.DisplayMemberPath = "TenGiaoVien";
                // cbPhongBan.SelectedIndex = 0;
                cbMaGVCN.SelectedValuePath = "MaGiaoVien";
            }
        }

        private void btThem_Click(object sender, RoutedEventArgs e)
        {
            if (Quyen == "Admin")
            {
                using (var db = new MyEntity())
                {
                    var l = new Lop(txtMaLop.Text, txtTenLop.Text, txtMaNganhHoc.Text, cbMaGVCN.SelectedValue.ToString());
                    if (l != null)
                    {
                        db.Lops.Add(l);
                        db.SaveChanges();
                        dtgLop.ItemsSource = db.Lops.ToList();
                    }
                }
                txtMaLop.Text = "";
                txtTenLop.Text = "";
                txtMaNganhHoc.Text = "";
                cbMaGVCN.Text = "";
                txtMaLop.Focus();
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
                if (dtgLop.SelectedItem != null)
                {
                    if (MessageBox.Show("Bạn có muốn xóa quê quán này không?", "Xoa Que Quan", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        using (var db = new MyEntity())
                        {
                            Lop nv = dtgLop.SelectedItem as Lop;
                            try
                            {
                                foreach (var item in db.Lops.ToList())
                                {
                                    if (item.MaLop == nv.MaLop)
                                    {
                                        db.Lops.Remove(item);
                                        db.SaveChanges();
                                        dtgLop.ItemsSource = db.Lops.ToList();
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
                    MessageBox.Show("Bạn phải chọn một lớp");
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
                Lop capnhat = new Lop();
                capnhat = (Lop)dtgLop.SelectedItem;
                if (dtgLop.SelectedItem != null)
                {
                    using (var db = new MyEntity())
                    {
                        try
                        {
                            foreach (var item in db.Lops.ToList())
                            {
                                if (item.MaLop == capnhat.MaLop)
                                {
                                    item.TenLop = txtTenLop.Text;
                                    item.MaNganhHoc = txtMaNganhHoc.Text;
                                    item.MaGVCN = cbMaGVCN.Text;
                                    db.SaveChanges();
                                    dtgLop.ItemsSource = db.Lops.ToList();
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
                    MessageBox.Show("Bạn phải chọn một lớp");
                }
            }
            else
            {
                MessageBox.Show("Bạn không được quyền làm thao tác này", "Xin lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void dtgLop_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var lop = (Lop)dtgLop.SelectedItem;
            if (lop != null)
            {
                txtMaLop.Text = lop.MaLop;
                txtTenLop.Text = lop.TenLop;
                txtMaNganhHoc.Text = lop.MaNganhHoc;
                using (var db = new MyEntity())
                {
                    var giaovien = db.GiaoViens.Find(lop.MaGVCN);
                    cbMaGVCN.SelectedIndex = db.GiaoViens.ToList().IndexOf(giaovien);
                }
            }
        }

        private void btTim_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new MyEntity())
            {
                var query = from p in db.Lops
                            where p.TenLop == txtTim.Text
                            select p;
                dtgLop.ItemsSource = query.ToList();

            }
        }
    }
}
