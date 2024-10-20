using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ELEVATE_SHOP_MANAGER
{
    public partial class Thaydoigia : Form
    {
        SqlConnection cn = ketnoidb.Ketnoidata();
        public Thaydoigia()
        {
            InitializeComponent();
            txtgiacu.KeyPress += new KeyPressEventHandler(Chinhapso);
            txtgiamoi.KeyPress += new KeyPressEventHandler(Chinhapso);
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
        private void locktxt(){
            txtmanv.Enabled = false;
            txtngaydoi.Enabled = false; 
            txtmathaygia.Enabled = false;
            txtmasp.Enabled = false;
            txtgiamoi.Enabled = false;
            txtgiacu.Enabled = false;
            btluu.Enabled = false;
            button1.Enabled = false;
        }
        private void unlocktxt()
        {
            txtmanv.Enabled = true;
            txtngaydoi.Enabled = true;
            txtmathaygia.Enabled = true;
            txtmasp.Enabled = true;
            txtgiamoi.Enabled = true;
            //txtgiacu.Enabled = true;
            btluu.Enabled = true;
            button1.Enabled = true;
        }
        private void xoatxt()
        {
            txtmanv.Clear();
            txtngaydoi.Value = DateTime.Now;
            txtmathaygia.Clear();
            txtmasp.Clear();
            txtgiamoi.Clear();
            txtgiacu.Clear();
        }
        private void Chinhapso(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }
        public void loaddatalichsu()
        {
            if (cn.State == ConnectionState.Closed)
            {
                cn.Open();
            }
            string sql = "select * from Dulieuthaydoigia";
            SqlCommand com = new SqlCommand(sql, cn);
            com.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable(); //tạo một kho ảo để lưu trữ dữ liệu
            da.Fill(dt);  // đổ dữ liệu vào kho
            cn.Close();  // đóng kết nối
            gridviewsp.DataSource = dt; //đổ dữ liệu vào datagridview
        }

        private void Thaydoigia_Load(object sender, EventArgs e)
        {
            loaddatalichsu();
            locktxt();
            xoatxt();
        }

        private void gridviewsp_SelectionChanged(object sender, EventArgs e)
        {
            //Hiển thị data lên textbox
            int dongchon = -1;
            dongchon = gridviewsp.CurrentCellAddress.Y;
            if (dongchon >= 0)
            {
                txtmasp.Text = gridviewsp.Rows[dongchon].Cells["MaSP"].Value.ToString();
                txtgiacu.Text = gridviewsp.Rows[dongchon].Cells["Giabancu"].Value.ToString();
                txtgiamoi.Text = gridviewsp.Rows[dongchon].Cells["Giabanmoi"].Value.ToString();
                txtmanv.Text = gridviewsp.Rows[dongchon].Cells["MaNV"].Value.ToString();
                txtmathaygia.Text = gridviewsp.Rows[dongchon].Cells["Mathaydoi"].Value.ToString();
                txtngaydoi.Text = gridviewsp.Rows[dongchon].Cells["Ngaythaydoi"].Value.ToString();
            }
        }
        private bool Kiemtrasanpham(string masp)
        {
            try
            {
                if (cn.State == ConnectionState.Closed)
                {
                    cn.Open();
                }

                string sql = "SELECT COUNT(*) FROM KhoHang WHERE MaSP = @MaSP";
                SqlCommand com = new SqlCommand(sql, cn);
                com.Parameters.AddWithValue("@MaSP", masp);

                int count = (int)com.ExecuteScalar();  // ExecuteScalar được sử dụng để lấy một giá trị duy nhất (COUNT)

                return count > 0; // Nếu count lớn hơn 0, sản phẩm tồn tại
            }
            catch (Exception ex)
            {
                // Xử lý lỗi ở đây, ví dụ ghi log hoặc hiển thị thông báo lỗi
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message);
                return false; // Trả về false nếu có lỗi xảy ra
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close(); // Đảm bảo kết nối được đóng trong mọi trường hợp
                }
            }
        }
        private void loadthongtinsptxt(string masp)
        {
            try
            {
                if (cn.State == ConnectionState.Closed)
                {
                    cn.Open();
                }

                string sql = "SELECT * FROM KhoHang WHERE MaSP = @MaSP";
                SqlCommand com = new SqlCommand(sql, cn);
                com.Parameters.AddWithValue("@MaSP", masp);

                SqlDataReader reader = com.ExecuteReader();

                if (reader.Read()) // Kiểm tra nếu có kết quả trả về
                {
                    // Đổ dữ liệu vào các TextBox
                    txtmasp.Text = reader["MaSP"].ToString();
                    txtgiacu.Text = reader["GiaBan"].ToString();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy sản phẩm với mã sản phẩm này.");
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                // Xử lý lỗi ở đây, ví dụ ghi log hoặc hiển thị thông báo lỗi
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message);
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close(); // Đảm bảo kết nối được đóng lại
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
           
            if (string.IsNullOrWhiteSpace(txtmasp.Text))
            {
                MessageBox.Show("Vui lòng nhập mã sản phẩm !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtmasp.Focus();
                return;
            }
            if (Kiemtrasanpham(txtmasp.Text))
            {              
                loadthongtinsptxt(txtmasp.Text);
            }
            else
            {
                MessageBox.Show("Mã sản phẩm không tồn tại trong kho !");
            }

        }

        private void bttimkiem_Click(object sender, EventArgs e)
        {
            if (cn.State == ConnectionState.Closed)
            {
                cn.Open();
            }
            string sql = "select * from Dulieuthaydoigia where MaSP = @masp";
            SqlCommand com = new SqlCommand(sql, cn);
            com.CommandType = CommandType.Text;
            com.Parameters.AddWithValue("@masp",txtmasp.Text);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable(); //tạo một kho ảo để lưu trữ dữ liệu
            da.Fill(dt);  // đổ dữ liệu vào kho
            cn.Close();  // đóng kết nối
            gridviewsp.DataSource = dt; //đổ dữ liệu vào datagridview
        }

        private void inPhiếuToolStripMenuItem_Click(object sender, EventArgs e)

        {
            xoatxt();
            unlocktxt();
        }

        private void hủyThaoTácToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            loaddatalichsu();
            locktxt();
            xoatxt();

        }

        private void loadLạiTrangToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loaddatalichsu();
            locktxt();
            xoatxt();
        }

        private void btluu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtmathaygia.Text))
            {
                MessageBox.Show("Vui lòng nhập mã phiếu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtmathaygia.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtmanv.Text))
            {
                MessageBox.Show("Vui lòng nhập mã nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtmanv.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtmasp.Text))
            {
                MessageBox.Show("Vui lòng nhập mã sản phẩm !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtmasp.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtgiacu.Text))
            {
                MessageBox.Show("Vui lòng giá cũ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtgiamoi.Text))
            {
                MessageBox.Show("Vui lòng nhập giá mới cho sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtgiamoi.Focus();
                return;
            }
            // Lấy dữ liệu từ các ô nhập liệu
            string maThayDoi = txtmathaygia.Text;
            string maSP = txtmasp.Text;
            string maNV = txtmanv.Text;
            decimal giaCu = decimal.Parse(txtgiacu.Text);
            decimal giaMoi = decimal.Parse(txtgiamoi.Text);
            DateTime ngayThayDoi = txtngaydoi.Value;  // Lấy ngày từ DateTimePicker

            // Kết nối với cơ sở dữ liệu
            using (SqlConnection conn = ketnoidb.Ketnoidata())
            {
                try
                {
                    conn.Open();

                    // Bắt đầu transaction để đảm bảo tính toàn vẹn dữ liệu
                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        // Cập nhật giá bán mới trong bảng KhoHang
                        string updateKhoHangQuery = @"
                    UPDATE KhoHang
                    SET GiaBan = @GiaMoi
                    WHERE MaSP = @MaSP";

                        using (SqlCommand cmd = new SqlCommand(updateKhoHangQuery, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@GiaMoi", giaMoi);
                            cmd.Parameters.AddWithValue("@MaSP", maSP);
                            cmd.ExecuteNonQuery();
                        }

                        // Lưu thông tin thay đổi giá vào bảng Dulieuthaydoigia
                        string insertChangeLogQuery = @"
                    INSERT INTO Dulieuthaydoigia (Mathaydoi, MaSP, Ngaythaydoi, Giabancu, Giabanmoi, MaNV)
                    VALUES (@MaThayDoi, @MaSP, @NgayThayDoi, @GiaCu, @GiaMoi, @MaNV)";

                        using (SqlCommand cmd = new SqlCommand(insertChangeLogQuery, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@MaThayDoi", maThayDoi);
                            cmd.Parameters.AddWithValue("@MaSP", maSP);
                            cmd.Parameters.AddWithValue("@NgayThayDoi", ngayThayDoi);
                            cmd.Parameters.AddWithValue("@GiaCu", giaCu);
                            cmd.Parameters.AddWithValue("@GiaMoi", giaMoi);
                            cmd.Parameters.AddWithValue("@MaNV", maNV);
                            cmd.ExecuteNonQuery();
                        }

                        // Commit transaction nếu không có lỗi
                        transaction.Commit();
                        MessageBox.Show("Cập nhật giá thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        loaddatalichsu();
                        locktxt();
                        xoatxt();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
    }
    
}
