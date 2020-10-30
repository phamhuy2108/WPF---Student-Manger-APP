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
    /// Interaction logic for w_TonGiao.xaml
    /// </summary>
    public partial class w_TonGiao : UserControl
    {
        string TenDangNhap = ""; string MatKhau = ""; string Quyen = "";
        public w_TonGiao(string TenDangNhap, string MatKhau, string Quyen)
        {
            InitializeComponent();
            this.TenDangNhap = TenDangNhap;
            this.MatKhau = MatKhau;
            this.Quyen = Quyen;
            using (var db = new MyEntity())
            {
                dtgTonGiao.ItemsSource = db.TonGiaos.ToList();
            }
        }

        private void btThem_Click(object sender, RoutedEventArgs e)
        {
            if (Quyen == "Admin")
            {
                using (var db = new MyEntity())
                {
                    var tg = new TonGiao(txtMaTonGiao.Text, txtTenTonGiao.Text);
                    if (tg != null)
                    {
                        db.TonGiaos.Add(tg);
                        db.SaveChanges();
                        dtgTonGiao.ItemsSource = db.TonGiaos.ToList();
                    }
                }
                txtMaTonGiao.Text = "";
                txtTenTonGiao.Text = "";
                txtMaTonGiao.Focus();
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
                if (dtgTonGiao.SelectedItem != null)
                {
                    if (MessageBox.Show("Bạn có muốn xóa tôn giáo này không?", "Xoa Ton Giao", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        using (var db = new MyEntity())
                        {
                            TonGiao dt = dtgTonGiao.SelectedItem as TonGiao;
                            try
                            {
                                foreach (var item in db.TonGiaos.ToList())
                                {
                                    if (item.MaTonGiao == dt.MaTonGiao)
                                    {
                                        db.TonGiaos.Remove(item);
                                        db.SaveChanges();
                                        dtgTonGiao.ItemsSource = db.TonGiaos.ToList();
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
                    MessageBox.Show("Bạn phải chọn một tôn giáo");
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
                TonGiao capnhat = new TonGiao();
                capnhat = (TonGiao)dtgTonGiao.SelectedItem;
                if (dtgTonGiao.SelectedItem != null)
                {
                    using (var db = new MyEntity())
                    {
                        try
                        {
                            foreach (var item in db.TonGiaos.ToList())
                            {
                                if (item.MaTonGiao == capnhat.MaTonGiao)
                                {
                                    item.TenTonGiao = txtTenTonGiao.Text;
                                    db.SaveChanges();
                                    dtgTonGiao.ItemsSource = db.TonGiaos.ToList();
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
                    MessageBox.Show("Bạn phải chọn một tôn giáo");
                }
            }
            else
            {
                MessageBox.Show("Bạn không được quyền làm thao tác này", "Xin lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void dtgTonGiao_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var TonGiao = (TonGiao)dtgTonGiao.SelectedItem;
            if (TonGiao != null)
            {
                txtMaTonGiao.Text = TonGiao.MaTonGiao;
                txtTenTonGiao.Text = TonGiao.TenTonGiao;
            }
        }
    }
}
