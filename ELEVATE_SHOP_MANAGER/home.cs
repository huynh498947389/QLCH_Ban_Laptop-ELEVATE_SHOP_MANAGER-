using System;
using System.Drawing;
using System.Windows.Forms;

namespace ELEVATE_SHOP_MANAGER
{
    public partial class home : Form
    {
        uc_banhang uc_Banhang;
        uc_home uc_Home;
        uc_khohang uc_Khohang;
        uc_nhanvien uc_Nhanvien;
        uc_nhapdon uc_Nhapdon;
        uc_qldonhang uc_Qldonhang;
        uc_thongke uc_Thongke;


        public String gname, gquyen;

        public home(String name, String quyen)
        {
            InitializeComponent();
            this.gname = name;
            this.gquyen = quyen;
           
        }

        private void home_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        

        private void home_Load(object sender, EventArgs e)
        {
            name.Text = " Hi ! " + gname + " ( " + gquyen + " ) ";
            if (uc_Home == null)
            {
                uc_Home = new uc_home();
                uc_Home.Dock = DockStyle.Fill;
                Conten.Controls.Add(uc_Home);
                uc_Home.BringToFront();
            }
            else
            {
                uc_Home.BringToFront();
            }
        }

        private void name_Click(object sender, EventArgs e)
        {

        }

        private void bunifuGroupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void bthome_Click(object sender, EventArgs e)
        {
            if (uc_Home == null)
            {
                uc_Home = new uc_home();
                uc_Home.Dock = DockStyle.Fill;
                Conten.Controls.Add(uc_Home);
                uc_Home.BringToFront();
            }
            else
            {
                uc_Home.BringToFront();
            }
        }

        private void btbanhang_Click(object sender, EventArgs e)
        {
            if (this.gquyen.Trim() == "Admin" || this.gquyen.Trim() == "Sale")
            {
                if (uc_Banhang == null)
                {
                    uc_Banhang = new uc_banhang(gname,gquyen);
                    uc_Banhang.Dock = DockStyle.Fill;
                    Conten.Controls.Add(uc_Banhang);
                    uc_Banhang.BringToFront();
                }
                else
                {
                    uc_Banhang.BringToFront();
                }
            }
            else
            {
                MessageBox.Show("Bạn không có quyền truy cập !");
            }
        }

        private void btqldonhang_Click(object sender, EventArgs e)
        {
            if (uc_Qldonhang == null)
            {
                uc_Qldonhang = new uc_qldonhang(gname,gquyen);
                uc_Qldonhang.Dock = DockStyle.Fill;
                Conten.Controls.Add(uc_Qldonhang);
                uc_Qldonhang.BringToFront();
            }
            else
            {
                uc_Qldonhang.BringToFront();
            }
        }

        private void btkhohang_Click(object sender, EventArgs e)
        {
            if (uc_Khohang == null)
            {
                uc_Khohang = new uc_khohang(gname,gquyen);
                uc_Khohang.Dock = DockStyle.Fill;
                Conten.Controls.Add(uc_Khohang);
                uc_Khohang.BringToFront();
            }
            else
            {
                uc_Khohang.BringToFront();
            }
        }

        private void btnhapdon_Click(object sender, EventArgs e)
        {
            if (this.gquyen.Trim() == "Admin" || this.gquyen.Trim() == "Technician")
                if (uc_Nhapdon == null)
            {
                uc_Nhapdon = new uc_nhapdon();
                uc_Nhapdon.Dock = DockStyle.Fill;
                Conten.Controls.Add(uc_Nhapdon);
                uc_Nhapdon.BringToFront();
            }
            else
            {
                uc_Nhapdon.BringToFront();
            }
            else
            {
                MessageBox.Show("Bạn không có quyền truy cập !");
            }
        }

        private void btthongke_Click(object sender, EventArgs e)
        {
            if (uc_Thongke == null)
            {
                uc_Thongke = new uc_thongke(gname,gquyen);
                uc_Thongke.Dock = DockStyle.Fill;
                Conten.Controls.Add(uc_Thongke);
                uc_Thongke.BringToFront();
            }
            else
            {
                uc_Thongke.BringToFront();
            }
        }

        private void btquanlinhanvien_Click(object sender, EventArgs e)
        {
            if (this.gquyen.Trim() == "Admin")
            { 
                    if (uc_Nhanvien == null)
                    {
                        uc_Nhanvien = new uc_nhanvien();
                        uc_Nhanvien.Dock = DockStyle.Fill;
                        Conten.Controls.Add(uc_Nhanvien);
                        uc_Nhanvien.BringToFront();
                    }
                    else
                    {
                        uc_Nhanvien.BringToFront();
                    }

            }
            else
            {
                MessageBox.Show("Bạn không có quyền truy cập !");
            }

        }

        private void bunifuButton27_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
