using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ELEVATE_SHOP_MANAGER
{
    public partial class uc_home : UserControl
    {
        SqlConnection cn = ketnoidb.Ketnoidata();
        public uc_home()
        {
            InitializeComponent();
        }
        public void loaddata()
        {
            if (cn.State == ConnectionState.Closed)
            {
                cn.Open();
            }
            string sql = "select * from KhoHang where TinhTrang = @tinhtrang AND SoLuongTonKho>0";
            SqlCommand com = new SqlCommand(sql, cn);
            com.Parameters.AddWithValue("@tinhtrang","Kinh doanh");
            com.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable(); //tạo một kho ảo để lưu trữ dữ liệu
            da.Fill(dt);  // đổ dữ liệu vào kho
            cn.Close();  // đóng kết nối
            gridviewsp.DataSource = dt; //đổ dữ liệu vào datagridview
        }

        private void uc_home_Load(object sender, EventArgs e)
        {
            loaddata();
        }

        private void LoadImage(string filePath)
        {
            try
            {
                // Kiểm tra xem file có tồn tại không
                if (!File.Exists(filePath))
                {
                    //MessageBox.Show("File không tồn tại: " + filePath);
                    pictureBox1.Image = null;
                    return;
                }

                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    byte[] buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, (int)fs.Length);
                    using (MemoryStream ms = new MemoryStream(buffer))
                    {
                        pictureBox1.Image = System.Drawing.Image.FromStream(ms);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra khi load ảnh: " + ex.Message);
            }
        }
        private void gridviewsp_SelectionChanged(object sender, EventArgs e)
        {
                String linkanh = " ";
                //Hiển thị data lên textbox
                int dongchon = -1;
                dongchon = gridviewsp.CurrentCellAddress.Y;
                if (dongchon >= 0)
                {
                    lbtensp.Text = gridviewsp.Rows[dongchon].Cells["TenSanPham"].Value.ToString();
                    lbmasp.Text = gridviewsp.Rows[dongchon].Cells["MaSP"].Value.ToString();
                    lbsoluong.Text = gridviewsp.Rows[dongchon].Cells["SoLuongTonKho"].Value.ToString();
                    linkanh = gridviewsp.Rows[dongchon].Cells["AnhSanPham"].Value.ToString();
                    lbhang.Text = gridviewsp.Rows[dongchon].Cells["Hang"].Value.ToString();
                    lbgiaban.Text = gridviewsp.Rows[dongchon].Cells["GiaBan"].Value.ToString();
                    richTextBox.Text = gridviewsp.Rows[dongchon].Cells["MoTa"].Value.ToString();
                    lbtinhtrang.Text = gridviewsp.Rows[dongchon].Cells["TinhTrang"].Value.ToString();
                     try{ LoadImage(linkanh);}
                        catch {
                                pictureBox1.Image = null; // Đặt PictureBox thành rỗng, không hiển thị hình ảnh
                                }
            }


            
        }

        private void bttimkiem_Click(object sender, EventArgs e)
        {
            if (cn.State == ConnectionState.Closed)
            {
                cn.Open();
            }

            string tensanpham = txttimkiem.Text.ToLower(); // Chuyển chuỗi nhập từ người dùng thành chữ thường
            string sql = "select * from KhoHang where LOWER(TenSanPham) like @productName";

            SqlCommand com = new SqlCommand(sql, cn);
            com.CommandType = CommandType.Text;
            com.Parameters.AddWithValue("@productName", "%" + tensanpham + "%"); // Sử dụng ký tự % để tìm kiếm gần đúng không phân biệt hoa thường

            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable(); // Tạo một kho ảo để lưu trữ dữ liệu
            da.Fill(dt);  // Đổ dữ liệu vào kho
            cn.Close();  // Đóng kết nối

            gridviewsp.DataSource = dt; // Đổ dữ liệu vào DataGridView


        }

        private void richTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void lbsoluong_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            loaddata();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
