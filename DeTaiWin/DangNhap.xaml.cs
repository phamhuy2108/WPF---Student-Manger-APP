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
using System.Data;
namespace DeTaiWin
{
    /// <summary>
    /// Interaction logic for DangNhap.xaml
    /// </summary>
    public partial class DangNhap : Window
    {
        public DangNhap()
        {
            InitializeComponent();
        }

        private void btDangNhap_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new MyEntity())
            {
                var query = from p in db.PhanQuyens
                            where p.TenDangNhap == txtTenDangNhap.Text && p.MatKhau == pwbMatKhau.Password
                            select p;
                var queryA = (from p in db.PhanQuyens
                              where p.TenDangNhap == txtTenDangNhap.Text && p.MatKhau == pwbMatKhau.Password
                              select new {Name = p.TenDangNhap }).FirstOrDefault();
                var queryB = (from p in db.PhanQuyens
                              where p.TenDangNhap == txtTenDangNhap.Text && p.MatKhau == pwbMatKhau.Password
                              select p.MatKhau).FirstOrDefault();
                var queryC = (from p in db.PhanQuyens
                              where p.TenDangNhap == txtTenDangNhap.Text && p.MatKhau == pwbMatKhau.Password
                              select new { Quyen = p.Quyen }).FirstOrDefault();
                //khi new ma van dung firstordefaul
                //var queryD = (from p in db.PhanQuyens
                //              where p.TenDangNhap == txtTenDangNhap.Text && p.MatKhau == pwbMatKhau.Password
                //              select p).SingleOrDefault();
                //var queryE = from p in db.PhanQuyens
                //              where p.TenDangNhap == txtTenDangNhap.Text && p.MatKhau == pwbMatKhau.Password
                //              select p;
                var count = query.Count();
                if (count > 0)
                {
                    MessageBox.Show("Đăng nhập thành công", "Chúc mừng", MessageBoxButton.OK, MessageBoxImage.Information);
                    MainWindow md = new MainWindow(queryA.ToString(), queryB.First().ToString(), queryC.ToString());
                    w_DanToc dt = new w_DanToc(queryA.ToString(), queryB.ToString(), queryC.ToString());
                    w_GiaoVien gv = new w_GiaoVien(queryA.ToString(), queryB.ToString(), queryC.ToString());
                    w_KetQua kq = new w_KetQua(queryA.ToString(), queryB.ToString(), queryC.ToString());
                    w_Lop l = new w_Lop(queryA.ToString(), queryB.ToString(), queryC.ToString());
                    w_MonHoc mh = new w_MonHoc(queryA.ToString(), queryB.ToString(), queryC.ToString());
                    w_PhanCong pc = new w_PhanCong(queryA.ToString(), queryB.ToString(), queryC.ToString());
                    w_QueQuan qq = new w_QueQuan(queryA.ToString(), queryB.ToString(), queryC.ToString());
                    w_SinhVien sv = new w_SinhVien(queryA.ToString(), queryB.ToString(), queryC.ToString());
                    w_TonGiao tg = new w_TonGiao(queryA.ToString(), queryB.ToString(), queryC.ToString());
                    w_Home home = new w_Home(queryA.ToString(), queryB.ToString(), queryC.ToString());
                    DoiMatKhau dmk = new DoiMatKhau(queryA.ToString());
                    this.Close();
                    md.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Đăng nhập thất bại", "Xin lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
        }
        private void btThoat_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn Thoát?", "Exit", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                Close();
        }
    }
}
