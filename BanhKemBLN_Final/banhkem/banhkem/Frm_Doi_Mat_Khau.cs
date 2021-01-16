﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;
//using System.Runtime.InteropServices;
namespace banhkem
{

    public partial class Frm_Doi_Mat_Khau : Form
    {
      /*  [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeft,
            int nTop,
            int nRight,
            int nBottom,
            int nWidthEllipse,
            int nHeightEllipse
            );*/






        Model1 BanhkemDB; /* Truy cập sẵn database*/
        NhanVien user; // Khai báo danh sách NV
        public Frm_Doi_Mat_Khau()
        {
            InitializeComponent();
        }

        public Frm_Doi_Mat_Khau(Model1 banhkem,NhanVien nguoidung)
        {
            InitializeComponent();
            BanhkemDB = banhkem;
            user = nguoidung;
        }

        private void Frm_Doi_Mat_Khau_Load(object sender, EventArgs e)
        {
            
            txtMk_Moi.UseSystemPasswordChar = true;
            txtMK_cu.UseSystemPasswordChar = true;
            txtXacNhan_MKmoi.UseSystemPasswordChar = true;
        }
         //   btnDoi_MK.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, btnDoi_MK.Width, btnDoi_MK.Height, 30, 30));

        private void btnDoi_MK_Click(object sender, EventArgs e)
        {
            lblNotification.Visible = true;
            if (txtMK_cu.Text != user.Matkhau)
            {
                MessageBox.Show("Nhập sai mật khẩu hiện tại !");return;
            }
                    
            if (txtMk_Moi.Text == txtXacNhan_MKmoi.Text)
            {
                var nhanvien_doimk = BanhkemDB.NhanViens.Find(user.MaNV);

                if (nhanvien_doimk != null)
                {
                    nhanvien_doimk.Matkhau = txtXacNhan_MKmoi.Text;
                    BanhkemDB.Entry(nhanvien_doimk).State = EntityState.Modified;
                    BanhkemDB.SaveChanges();
                    MessageBox.Show("Đổi mật khẩu thành công !");
                    lblNotification.Visible = false;
                }
                else MessageBox.Show("Không tìm thấy nhân viên này !");

            }
            else MessageBox.Show("Nhập sai mật khẩu xác nhận !"); 

        }

        private void txtXacNhan_MKmoi_TextChanged(object sender, EventArgs e)
        {
            pB3_No_txtXacNhan_MKmoi.Visible = false;
            pB3_Yes_txtXacNhan_MKmoi.Visible = false;
            pB2_Yes_txtMk_Moi.Visible = false;
            pB2_No_txtMk_Moi.Visible = false;

            if (txtMk_Moi.Text != txtXacNhan_MKmoi.Text )
            {
                pB3_No_txtXacNhan_MKmoi.Visible = true;
                pB3_No_txtXacNhan_MKmoi.Visible = true;
            }
            else
            {
                pB2_Yes_txtMk_Moi.Visible = true;
                pB3_Yes_txtXacNhan_MKmoi.Visible = true;
            }    
        }

        private void btn_Huy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtMK_cu_TextChanged(object sender, EventArgs e)
        {
            pB1_No_txtMK_cu.Visible = false;
            pB1_Yes_txtMK_cu.Visible = false;
            if (txtMK_cu.Text != user.Matkhau)
            {
                pB1_No_txtMK_cu.Visible = true;
            }
            else pB1_Yes_txtMK_cu.Visible = true;
        }

        private void txtMk_Moi_TextChanged(object sender, EventArgs e)
        {
            pB3_No_txtXacNhan_MKmoi.Visible = false;
            pB3_Yes_txtXacNhan_MKmoi.Visible = false;
            pB2_Yes_txtMk_Moi.Visible = false;
            pB2_No_txtMk_Moi.Visible = false;
        }
    }
}
