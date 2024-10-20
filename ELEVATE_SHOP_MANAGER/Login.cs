using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Security.Cryptography;


namespace ELEVATE_SHOP_MANAGER
{
    public partial class formlogin : Form
    {
        public formlogin()
        {
            InitializeComponent();
        }

        private void bunifuLabel1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuGroupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void bunifuLabel3_Click(object sender, EventArgs e)
        {

        }

        private void bunifuButton21_Click(object sender, EventArgs e)
        {
           
            String quyen;
            String name;
           // String avata;
            String trangthai;
            SqlConnection cn = ketnoidb.Ketnoidata();


            if (cn.State == ConnectionState.Closed)
            {
                cn.Open();
            }
            String manv = txtmanhanvien.Text.Trim();
            String mk = txtmatkhau.Text.Trim();
            


            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.CommandText = "Select * from NhanVien Where MaNV = @username AND MatKhau = @password"; // AND TrangThai = @trangthai";
            sqlcmd.Parameters.AddWithValue("@username", manv);
            sqlcmd.Parameters.AddWithValue("@password", mk);
           // sqlcmd.Parameters.AddWithValue("@trangthai", "ON"); ;
            sqlcmd.Connection = cn;

            SqlDataReader data = sqlcmd.ExecuteReader();
            if (data.Read())
            {

                quyen = data["LoaiTaiKhoan"].ToString();
                name = data["HoTen"].ToString();
                trangthai = data["TrangThai"].ToString();
                if (trangthai == "ON")
                {
                    home homes = new home(name, quyen);
                    homes.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show(" Tài khoản chưa được cấp quyền truy cập !");
                }
            }
            else
            {

                MessageBox.Show("Sai mật khẩu hoặc tài khoản !");
            }
            data.Close();
        }

        private void bunifuButton22_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bunifuLabel1_Click_1(object sender, EventArgs e)
        {

        }

        private void formlogin_Load(object sender, EventArgs e)
        {
        }
    }
}
