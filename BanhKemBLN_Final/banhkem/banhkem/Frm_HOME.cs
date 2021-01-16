using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace banhkem
{
    public partial class Frm_HOME : Form
    {
        Frm_ThongKe formThongke;
        Frm_Banghang formBanghang;
        Frm_QuanLy formQuanLy;
        Frm_xemthongtinNV formXemTT;
        Frm_Doi_Mat_Khau formDoiMK;
        Model1 BanhkemDB; /* Khai báo sẵn database*/
        NhanVien user = new NhanVien(); //Khai báo sẵn nhân viên
        public int sukienQL = 0;
        public Frm_HOME()
        {
            InitializeComponent();
        }

        public Frm_HOME(Model1 banhkem, NhanVien a)
        {
            InitializeComponent();
            BanhkemDB = banhkem; // Nhận dữ liệu database từ Form Đăng nhập
            user = a; // Nhận dữ liệu nhân viên đăng nhập
        }

        

        //Nhân viên bán hàng thì chỉ có công việc là bán hàng, không có quyền truy cập vào chức năng quản lý
        //Nhân viên quản lý thì chỉ có công việc là quản lý, không được quyền tác động đến việc buôn bán
        //Admin được quyền truy cập vào bất kỳ chức năng nào. Tuy nhiên vì yêu cầu bảo mật, chỉ nên có 1 Admin duy nhất trong trường hợp khẩn cấp


        private void Frm_HOME_Load(object sender, EventArgs e)
        {
            this.IsMdiContainer = true;
            if (user.ChucVu.MaCV == 3) // Ban hang
            {
                qUẢNLÝToolStripMenuItem.Visible = false;
                tHỐNGKÊToolStripMenuItem.Visible = false;
            }
            if (user.ChucVu.MaCV == 2) // Quan ly
            {
                bÁNHÀNGToolStripMenuItem.Visible = false;
                tHỐNGKÊToolStripMenuItem.Visible = false;
            }

            if (user.ChucVu.MaCV == 4) // Ke toan
            {
                bÁNHÀNGToolStripMenuItem.Visible = false;
                qUẢNLÝToolStripMenuItem.Visible = false;

            }
            
        }

        public Model1 SuKienUpdateNV
        {
            get { return BanhkemDB; }
            set { BanhkemDB = value; }
        }
        // Nếu quản lý có thêm NV ở chức năng Quản lý, thì khi quay về form
        // DangNhap nay sẽ có thể đăng nhập tài khoản dc ngay mà ko cần tắt chương trình + mở lại.

        public void MoFrm_Quanly()
        {

            formQuanLy = new Frm_QuanLy(BanhkemDB, user,sukienQL); //
            formQuanLy.MdiParent = this;
            formQuanLy.Show();
            
            Cursor.Current = Cursors.Arrow;
            BanhkemDB = formQuanLy.SuKienUpdateNV;

        }

        private void thôngTinTàiKhoảnNhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            sukienQL = 1;

            MoFrm_Quanly();
        }

        private void sảnPhẩmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            sukienQL = 2;
            MoFrm_Quanly();
        }

        private void nguyênLiệuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            sukienQL = 3;
            MoFrm_Quanly();
        }

        private void nhàCungCấpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            sukienQL = 4;
            MoFrm_Quanly();
        }

        private void xemHóaĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            sukienQL = 5;
            MoFrm_Quanly();
        }

        private void doanhThuToolStripMenuItem_Click(object sender, EventArgs e)
        {

            formThongke = new Frm_ThongKe(BanhkemDB, user);
            formThongke.Show(); Cursor.Current = Cursors.Arrow;
            formThongke.MdiParent = this;
        }

        private void bÁNHÀNGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            formBanghang = new Frm_Banghang(BanhkemDB, user);
            formBanghang.Show();
            Cursor.Current = Cursors.Arrow;
            formBanghang.MdiParent = this;
        }

        private void xemThôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            formXemTT = new Frm_xemthongtinNV(BanhkemDB, user);
            formXemTT.Show();
            Cursor.Current = Cursors.Arrow;
            formXemTT.MdiParent = this;
        }

        private void đổiMậtKhẩuToolStripMenuItem1_Click(object sender, EventArgs e)
        {
             
            formDoiMK = new Frm_Doi_Mat_Khau(BanhkemDB, user); // Truyền dữ liệu database + nhân viên qua Form Main 
                                                               // để đỡ tốn bộ nhớ khi bên đó gọi lại

            formDoiMK.Show();
            formDoiMK.MdiParent = this;
            Cursor.Current = Cursors.Arrow;
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {

            DialogResult kq = MessageBox.Show("Bạn có thực sự muốn đăng xuất không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (kq == DialogResult.OK)
            {
                this.Close();
            }
       
            
        }

      

        private void tRANGCHỦToolStripMenuItem_Click(object sender, EventArgs e) // 
        {
            foreach (Form frm in this.MdiChildren) // dong cac tab con trong menustrip.
            {
                if (frm == formBanghang)
                {
                    frm.Close();

                }

                if (frm == formQuanLy)
                {
                    frm.Close(); 

                }
                if (frm == formThongke)
                {
                    frm.Close(); 


                }
                if (frm == formXemTT)
                {
                    frm.Close();
                }
                if (frm == formDoiMK)
                {
                    frm.Close();
                }
            }
 
        }
    }
}
