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
using System.Windows.Shapes;

namespace DeTaiWin
{
    /// <summary>
    /// Interaction logic for DoiMatKhau.xaml
    /// </summary>
    public partial class DoiMatKhau : Window
    {
        string TenDangNhap = "";
        public DoiMatKhau(string TenDangNhap)
        {
            this.TenDangNhap = TenDangNhap;
            InitializeComponent();
           
        }

        private void btDongY_Click(object sender, RoutedEventArgs e)
        {
            var db = new MyEntity();
            using (db)
            {
                var query = (from p in db.PhanQuyens
                            where p.TenDangNhap == TenDangNhap && p.MatKhau == pwbOPass.Password
                            select p).FirstOrDefault();;
                if (query != null && pwbNPass.Password == pwbCPass.Password)
                {
                    query.MatKhau = pwbNPass.Password;
                    db.SaveChanges();
                    MessageBox.Show("Đổi mật khẩu thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                else
                {
                    MessageBox.Show("Đổi mật khẩu thất bại", "Xin lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btThoat_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn Thoát?", "Exit", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                Close();
        }
    }


    //using (var db = new MyEntity())
    //{
    //    var query = from p in db.PhanQuyens
    //                where p.TenDangNhap == txtUser.Text && p.MatKhau == pwbOPass.Password
    //                select p;
    //    var count = query.Count();
    //    if (count > 0 && pwbNPass.Password==pwbCPass.Password)
    //    {
    //        PhanQuyen pq = new PhanQuyen();
    //        try
    //        {
    //            foreach (var item in db.PhanQuyens.ToList())
    //            {
    //                if (item.ID == pq.ID)
    //                {
    //                    item.MatKhau = pwbNPass.Password;
    //                    db.SaveChanges();
    //                }
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            MessageBox.Show(ex.Message);
    //        }
    //    }

}

