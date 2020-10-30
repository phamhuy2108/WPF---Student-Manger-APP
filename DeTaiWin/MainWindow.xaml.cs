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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string TenDangNhap = ""; string MatKhau = ""; string Quyen = "";
        public MainWindow()
        {
            InitializeComponent();
        }
        public MainWindow(string TenDangNhap, string MatKhau, string Quyen)
        {
            InitializeComponent();
            this.TenDangNhap = TenDangNhap;
            this.MatKhau = MatKhau;
            this.Quyen = Quyen;
        }

        private void btSinhVien_Click(object sender, RoutedEventArgs e)
        {
            {
                Stage.Child = new w_SinhVien(TenDangNhap, MatKhau, Quyen);
            }
        }

        private void btGiaoVien_Click(object sender, RoutedEventArgs e)
        {
            if (Quyen == " Quyen = admin ")
            {
                Stage.Child = new w_GiaoVien(TenDangNhap, MatKhau, Quyen);
            }
            else if (Quyen == "GiaoVien")
            {
                Stage.Child = new w_GiaoVien(TenDangNhap, MatKhau, Quyen);
            }
            else
            {
                MessageBox.Show("Bạn không được quyền làm thao tác này");
            }
        }

        private void btMonHoc_Click(object sender, RoutedEventArgs e)
        {
            Stage.Child = new w_MonHoc(TenDangNhap, MatKhau, Quyen);
        }

        private void btKetQua_Click(object sender, RoutedEventArgs e)
        {
            Stage.Child = new w_KetQua(TenDangNhap, MatKhau, Quyen);
        }

        private void btLop_Click(object sender, RoutedEventArgs e)
        {
            Stage.Child = new w_Lop(TenDangNhap, MatKhau, Quyen);
        }

        private void btPhanCong_Click(object sender, RoutedEventArgs e)
        {
            if (Quyen == "Admin")
            {
                Stage.Child = new w_PhanCong(TenDangNhap, MatKhau, Quyen);
            }
            else if (Quyen == "GiaoVien")
            {
                Stage.Child = new w_PhanCong(TenDangNhap, MatKhau, Quyen);
            }
            else
            {
                MessageBox.Show("Bạn không được quyền làm thao tác này");
            }
        }
        private void btQueQuan_Click(object sender, RoutedEventArgs e)
        {
            Stage.Child = new w_QueQuan(TenDangNhap, MatKhau, Quyen);

        }
        private void btDanToc_Click(object sender, RoutedEventArgs e)
        {
            Stage.Child = new w_DanToc(TenDangNhap, MatKhau, Quyen);

        }

        private void btTonGiao_Click(object sender, RoutedEventArgs e)
        {
            Stage.Child = new w_TonGiao(TenDangNhap, MatKhau, Quyen);
        }

        private void btTrangChu_Click(object sender, RoutedEventArgs e)
        {
            Stage.Child = new w_Home(TenDangNhap, MatKhau, Quyen);
        }
    }
}
