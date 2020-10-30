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
    /// Interaction logic for w_DanToc.xaml
    /// </summary>
    public partial class w_DanToc : UserControl
    {
        string TenDangNhap = ""; string MatKhau = ""; string Quyen = "";
        public w_DanToc(string TenDangNhap, string MatKhau, string Quyen)
        {
            InitializeComponent();
            this.TenDangNhap = TenDangNhap;
            this.MatKhau = MatKhau;
            this.Quyen = Quyen;
            using (var db = new MyEntity())
            {
                dtgDanToc.ItemsSource = db.DanTocs.ToList();
            }
        }

        private void btThem_Click(object sender, RoutedEventArgs e)
        {
            if (Quyen == "Admin")
            {
                using (var db = new MyEntity())
                {
                    var nv = new DanToc(txtMaDanToc.Text, txtTenDanToc.Text);
                    if (nv != null)
                    {
                        db.DanTocs.Add(nv);
                        db.SaveChanges();
                        dtgDanToc.ItemsSource = db.DanTocs.ToList();
                    }
                }
                txtMaDanToc.Text = "";
                txtTenDanToc.Text = "";
                txtMaDanToc.Focus();
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
                if (dtgDanToc.SelectedItem != null)
                {
                    if (MessageBox.Show("Bạn có muốn xóa dân tộc này không?", "Xoa Dan Toc", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        using (var db = new MyEntity())
                        {
                            DanToc dt = dtgDanToc.SelectedItem as DanToc;
                            try
                            {
                                foreach (var item in db.DanTocs.ToList())
                                {
                                    if (item.MaDanToc == dt.MaDanToc)
                                    {
                                        db.DanTocs.Remove(item);
                                        db.SaveChanges();
                                        dtgDanToc.ItemsSource = db.DanTocs.ToList();
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
                    MessageBox.Show("Bạn phải chọn một dân tộc");
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
                DanToc capnhat = new DanToc();
                capnhat = (DanToc)dtgDanToc.SelectedItem;
                if (dtgDanToc.SelectedItem != null)
                {
                    using (var db = new MyEntity())
                    {
                        try
                        {
                            foreach (var item in db.DanTocs.ToList())
                            {
                                if (item.MaDanToc == capnhat.MaDanToc)
                                {
                                    item.TenDanToc = txtTenDanToc.Text;
                                    db.SaveChanges();
                                    dtgDanToc.ItemsSource = db.DanTocs.ToList();
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
                    MessageBox.Show("Bạn phải chọn một dân tộc");
                }
            }
            else
            {
                MessageBox.Show("Bạn không được quyền làm thao tác này", "Xin lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void dtgDanToc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var dantoc = (DanToc)dtgDanToc.SelectedItem;
            if (dantoc != null)
            {
                txtMaDanToc.Text = dantoc.MaDanToc;
                txtTenDanToc.Text = dantoc.TenDanToc;
            }
        }
    }
}
