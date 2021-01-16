﻿using System;
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
    public partial class Frm_Dangnhap : Form
    {       
        // Email là tên đăng nhập
        // Mật khẩu được lưu trữ trong database



        Model1 BanhkemDB= new Model1(); /* Truy cập sẵn database*/
        List<NhanVien> listNV; // Khai báo danh sách NV
        
        public Frm_Dangnhap()
        {
            InitializeComponent();
        }

        private void chbxHienMK_CheckedChanged(object sender, EventArgs e)
        {
            if (chbxHienMK.Checked)
            {
                txtMatkhau.UseSystemPasswordChar = false;

            }
            else
                txtMatkhau.UseSystemPasswordChar = true;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
          
                this.Close();
           
        }

        private void btndangnhap_Click(object sender, EventArgs e)
        {
            lblNotification.Visible = true;
            foreach(NhanVien a in listNV)
            {
                if(txtTendangnhap.Text != a.Email | txtMatkhau.Text != a.Matkhau)
                {
                    lblNotification.Text = "Sai tên đăng nhập hoặc mật khẩu !";
                }
                //Đối chiếu với database, nếu nhập đúng thì mở form Menu
                if (txtTendangnhap.Text == a.Email && txtMatkhau.Text == a.Matkhau)
                {
                    if (a.Trangthai == "Đã nghỉ việc")
                    {
                        lblNotification.Text = "Tài khoản không còn hiệu lực."; return;
                    }
                    lblNotification.Visible = false;

                    txtMatkhau.Text = "";
                    txtTendangnhap.Text = "";

                    Frm_HOME f = new Frm_HOME(BanhkemDB, a); // Truyền dữ liệu database + nhân viên qua Form Main                                                             // để đỡ tốn bộ nhớ khi bên đó gọi lại
                    MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK);
                    this.Hide();
                    f.ShowDialog(); 
                    Cursor.Current = Cursors.Arrow;
                    BanhkemDB = f.SuKienUpdateNV; // Nếu quản lý có thêm NV ở chức năng Quản lý, thì khi quay về form
                                                  // DangNhap nay sẽ có thể đăng nhập tài khoản dc ngay mà ko cần tắt chương trình + mở lại.
                    this.Show();

                }
            }
      
        }

        private void txtTendangnhap_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                btndangnhap_Click(sender, e);
            }
        }

        private void txtMatkhau_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                btndangnhap_Click(sender, e);
            }
        }

        private void Frm_Dangnhap_Load(object sender, EventArgs e)
        {
            listNV = BanhkemDB.NhanViens.ToList(); //Lấy danh sách NV từ database bỏ vào biến listNV!
        }

        private void label4_Click(object sender, EventArgs e)
        {
            frm_Lay_LaiMK f = new frm_Lay_LaiMK(listNV);
            this.Hide();
            f.ShowDialog();
            this.Show();
            Cursor.Current = Cursors.Arrow;
        }

        private void txtTendangnhap_TextChanged(object sender, EventArgs e)
        {
            if (txtTendangnhap.Text == "") label5.Visible = true;
            else label5.Visible = false;
        }

        private void txtMatkhau_TextChanged(object sender, EventArgs e)
        {
            if (txtMatkhau.Text == "") label6.Visible = true;
            else label6.Visible = false;
        }
    }
}

