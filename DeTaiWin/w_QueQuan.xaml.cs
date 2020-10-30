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
    /// Interaction logic for w_QueQuan.xaml
    /// </summary>
    public partial class w_QueQuan : UserControl
    {
        string TenDangNhap = ""; string MatKhau = ""; string Quyen = "";
        public w_QueQuan(string TenDangNhap, string MatKhau, string Quyen)
        {
            InitializeComponent();
            this.TenDangNhap = TenDangNhap;
            this.MatKhau = MatKhau;
            this.Quyen = Quyen;
            using (var db = new MyEntity())
            {
                dtgQueQuan.ItemsSource = db.QueQuans.ToList();
            }
        }

        private void btThem_Click(object sender, RoutedEventArgs e)
        {
            if (Quyen == "Admin")
            {
                using (var db = new MyEntity())
                {
                    var qq = new QueQuan(txtMaQueQuan.Text, txtTenTinhThanhPho.Text, txtTenQuanHuyen.Text, txtTenPhuongXa.Text);
                    if (qq != null)
                    {
                        db.QueQuans.Add(qq);
                        db.SaveChanges();
                        dtgQueQuan.ItemsSource = db.QueQuans.ToList();
                    }
                }
                txtMaQueQuan.Text = "";
                txtTenTinhThanhPho.Text = "";
                txtTenQuanHuyen.Text = "";
                txtTenPhuongXa.Text = "";
                txtMaQueQuan.Focus();
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
                if (dtgQueQuan.SelectedItem != null)
                {
                    if (MessageBox.Show("Bạn có muốn xóa quê quán này không?", "Xoa Que Quan", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        using (var db = new MyEntity())
                        {
                            QueQuan nv = dtgQueQuan.SelectedItem as QueQuan;
                            try
                            {
                                foreach (var item in db.QueQuans.ToList())
                                {
                                    if (item.MaQueQuan == nv.MaQueQuan)
                                    {
                                        db.QueQuans.Remove(item);
                                        db.SaveChanges();
                                        dtgQueQuan.ItemsSource = db.QueQuans.ToList();
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
                    MessageBox.Show("Bạn phải chọn một quê quán");
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
                QueQuan capnhat = new QueQuan();
                capnhat = (QueQuan)dtgQueQuan.SelectedItem;
                if (dtgQueQuan.SelectedItem != null)
                {
                    using (var db = new MyEntity())
                    {
                        try
                        {
                            foreach (var item in db.QueQuans.ToList())
                            {
                                if (item.MaQueQuan == capnhat.MaQueQuan)
                                {
                                    item.TenTinhThanhPho = txtTenTinhThanhPho.Text;
                                    item.TenQuanHuyen = txtTenQuanHuyen.Text;
                                    item.TenPhuongXa = txtTenPhuongXa.Text;
                                    db.SaveChanges();
                                    dtgQueQuan.ItemsSource = db.QueQuans.ToList();
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
                    MessageBox.Show("Bạn phải chọn một quê quán");
                }
            }
            else
            {
                MessageBox.Show("Bạn không được quyền làm thao tác này", "Xin lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void dtgQueQuan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var quequan = (QueQuan)dtgQueQuan.SelectedItem;
            if (quequan != null)
            {
                txtMaQueQuan.Text = quequan.MaQueQuan;
                txtTenTinhThanhPho.Text = quequan.TenTinhThanhPho;
                txtTenQuanHuyen.Text = quequan.TenQuanHuyen.ToString();
                txtTenPhuongXa.Text = quequan.TenPhuongXa.ToString();
            }
        }
    }
}
