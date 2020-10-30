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
    /// Interaction logic for w_Home.xaml
    /// </summary>
    public partial class w_Home : UserControl
    {
        string TenDangNhap = ""; string MatKhau = "";string Quyen = "";
        public w_Home(string TenDangNhap, string MatKhau, string Quyen)
        {
            this.TenDangNhap = TenDangNhap;
            this.MatKhau = MatKhau;
            this.Quyen = Quyen;
            InitializeComponent();
        }

        private void textBlock1_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            DoiMatKhau dmk = new DoiMatKhau(TenDangNhap);
            dmk.ShowDialog();
        }
    }
}
