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
using System.Web.UI.WebControls;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ELEVATE_SHOP_MANAGER
{

    public partial class uc_nhanvien : UserControl
    {
        SqlConnection cn = ketnoidb.Ketnoidata();
        public uc_nhanvien()
        {
            InitializeComponent();
            txtsodienthoai.KeyPress += new KeyPressEventHandler(Chinhapso);
        }
        private void lockphantu()
        {
            txtmanhanvien.Enabled = false;
            txtmatkhau.Enabled = false;
            txttennhanvien.Enabled = false;
            txtsodienthoai.Enabled = false;
            txtmail.Enabled = false;
            txttrangthai.Enabled = false;
            txtchucvu.Enabled = false;
            txtloaitaikhoan.Enabled = false;
            txtanh.Enabled = false;
            btadd.Visible = false;
            btsave.Visible = false;
            btchonanh.Enabled=false;
        }
        private void unlockphantu()
        {
            txtmanhanvien.Enabled = true;
            txtmatkhau.Enabled = true;
            txttennhanvien.Enabled = true;
            txtsodienthoai.Enabled = true;
            txtmail.Enabled = true;
            txttrangthai.Enabled = true;
            txtchucvu.Enabled = true;
            txtloaitaikhoan.Enabled = true;
            btchonanh.Enabled = true;
            // txtanh.Enabled = true;
        }
        private void xoatext()
        {
            txtmanhanvien.Clear();
            txtmatkhau.Clear();
            txttennhanvien.Clear();
            txtsodienthoai.Clear();
            txtmail.Clear();

            // ComboBox - Đặt lại về trạng thái chưa chọn
            txttrangthai.SelectedIndex = -1;
            txtchucvu.Clear();

            // ComboBox - Đặt lại về trạng thái chưa chọn
            txtloaitaikhoan.SelectedIndex = -1;

            txtanh.Clear();
            pictureBox1.Image = null;
        }


        public void loaddata()
        {
            if (cn.State == ConnectionState.Closed)
            {
                cn.Open();
            }
            string sql = "select * from NhanVien";
            SqlCommand com = new SqlCommand(sql, cn);
            com.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable(); //tạo một kho ảo để lưu trữ dữ liệu
            da.Fill(dt);  // đổ dữ liệu vào kho
            cn.Close();  // đóng kết nối
            gridviewsp.DataSource = dt; //đổ dữ liệu vào datagridview
        }
        private void Chinhapso(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }     
        private void uc_nhanvien_Load(object sender, EventArgs e)
        {
            lockphantu();
            loaddata();
            xoatext();
        }

        private void bttimkiem_Click(object sender, EventArgs e)
        {
            if (cn.State == ConnectionState.Closed)
            {
                cn.Open();
            }

            string hoten = txttimkiem.Text.ToLower(); // Chuyển chuỗi nhập từ người dùng thành chữ thường
            string sql = "select * from NhanVien where LOWER(HoTen) like @Name";

            SqlCommand com = new SqlCommand(sql, cn);
            com.CommandType = CommandType.Text;
            com.Parameters.AddWithValue("@Name", "%" + hoten + "%"); // Sử dụng ký tự % để tìm kiếm gần đúng không phân biệt hoa thường

            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable(); // Tạo một kho ảo để lưu trữ dữ liệu
            da.Fill(dt);  // Đổ dữ liệu vào kho
            cn.Close();  // Đóng kết nối

            gridviewsp.DataSource = dt; // Đổ dữ liệu vào DataGridView


        }

        private void hủyThaoTácToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            lockphantu();
            xoatext();
        }

        private void inPhiếuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            xoatext();
            unlockphantu();
            btadd.Visible = true;
        }

        private void tạoPhiếuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            unlockphantu();
            btsave.Visible = true;
            txtmanhanvien.Enabled = false;
        }
          // Biến để lưu trữ tên ảnh hiện tại
        public string currentImagePath = null;
        private void btchonanh_Click(object sender, EventArgs e)
        {
            


            // Tạo đối tượng OpenFileDialog
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp"; // Chỉ cho phép chọn file ảnh
            openFileDialog.Title = "Chọn một hình ảnh";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Lấy đường dẫn file ảnh đã chọn
                string selectedFilePath = openFileDialog.FileName;

                // Tạo đường dẫn tương đối đến thư mục "Images" trong project
                string relativePath = @"Images\" + Path.GetFileName(selectedFilePath);

                // Hiển thị đường dẫn tương đối lên TextBox
                txtanh.Text = relativePath;

                // Hiển thị ảnh trong PictureBox
                try
                {
                    pictureBox1.Image = System.Drawing.Image.FromFile(selectedFilePath);
                    currentImagePath = selectedFilePath;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra khi hiển thị ảnh: " + ex.Message);
                }
            }
        }
        private void SaveImage()
        {
            if (currentImagePath == null)
            {
                MessageBox.Show("Chưa có ảnh nào được chọn.");
                return;
            }

            // Tạo đường dẫn tương đối đến thư mục "Images" trong project
            string relativePath = @"Images";
            string projectPath = Application.StartupPath; // Thư mục gốc của ứng dụng
            string destinationFolder = Path.Combine(projectPath, relativePath); // Đường dẫn tới thư mục "Images"

            // Tạo thư mục "Images" nếu chưa tồn tại
            if (!Directory.Exists(destinationFolder))
            {
                Directory.CreateDirectory(destinationFolder);
            }

            // Tạo tên file đích để lưu trong thư mục "Images"
            string fileName = Path.GetFileName(currentImagePath); // Lấy tên file từ đường dẫn gốc
            string destinationFilePath = Path.Combine(destinationFolder, fileName); // Đường dẫn đích

            // Lưu ảnh mới
            try
            {
                File.Copy(currentImagePath, destinationFilePath, true); // Ghi đè nếu file đã tồn tại
                //MessageBox.Show("Ảnh đã được lưu vào: " + destinationFilePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra khi sao chép ảnh: " + ex.Message);
            }
        }
        // Hàm để xóa một file dựa trên đường dẫn tương đối
        private void DeleteFile(string relativeFilePath)
        {
            // Tạo đường dẫn đầy đủ từ đường dẫn tương đối
            string projectPath = Application.StartupPath; // Thư mục gốc của ứng dụng
            string filePath = Path.Combine(projectPath, relativeFilePath); // Đường dẫn đầy đủ tới file

            // Kiểm tra xem file có tồn tại hay không
            if (File.Exists(filePath))
            {
                try
                {
                    // Kiểm tra xem file có đang bị khóa bởi FileStream không
                    using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.None))
                    {
                        // Nếu đoạn mã này chạy mà không ném lỗi, file không bị khóa
                        fs.Close(); // Đảm bảo đóng file
                    }
                }
                catch (IOException ex)
                {
                    MessageBox.Show("File đang bị sử dụng bởi một quá trình khác: " + ex.Message);
                    return; // Dừng thực hiện tiếp nếu file bị khóa
                }

                // Giải phóng ảnh trong PictureBox nếu nó đang sử dụng file
                if (pictureBox1.Image != null)
                {
                    pictureBox1.Image.Dispose(); // Giải phóng tài nguyên ảnh
                    pictureBox1.Image = null; // Đặt lại thuộc tính Image về null
                }

                // Xóa file
                try
                {
                    File.Delete(filePath); // Xóa file
                    MessageBox.Show("Đã xóa file: " + relativeFilePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra khi xóa file: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("File không tồn tại: " + relativeFilePath);
            }
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




        public string anhcanxoa;
        private void gridviewsp_SelectionChanged(object sender, EventArgs e)
        {
            String linkanh = " ";
            //Hiển thị data lên textbox
            int dongchon = -1;
            dongchon = gridviewsp.CurrentCellAddress.Y;
            if (dongchon >= 0)
            {
                txtmanhanvien.Text = gridviewsp.Rows[dongchon].Cells["MaNV"].Value.ToString();
                txttennhanvien.Text = gridviewsp.Rows[dongchon].Cells["HoTen"].Value.ToString();
                txtmatkhau.Text = gridviewsp.Rows[dongchon].Cells["MatKhau"].Value.ToString();
                linkanh = gridviewsp.Rows[dongchon].Cells["AnhNhanVien"].Value.ToString();
                txtsodienthoai.Text = gridviewsp.Rows[dongchon].Cells["SoDienThoai"].Value.ToString();
                txtmail.Text = gridviewsp.Rows[dongchon].Cells["Email"].Value.ToString();
                txttrangthai.Text = gridviewsp.Rows[dongchon].Cells["TrangThai"].Value.ToString();
                txtchucvu.Text = gridviewsp.Rows[dongchon].Cells["ChucVu"].Value.ToString();
                txtloaitaikhoan.Text = gridviewsp.Rows[dongchon].Cells["LoaiTaiKhoan"].Value.ToString();
                txtanh.Text = linkanh.ToString();
                anhcanxoa = linkanh.ToString();
                try {
                    LoadImage(linkanh);
                }
                catch
                {
                    pictureBox1.Image = null; // Đặt PictureBox thành rỗng, không hiển thị hình ảnh
                }
            }


        }

        private void btadd_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra dữ liệu đầu vào
                if (string.IsNullOrWhiteSpace(txtmanhanvien.Text))
                {
                    MessageBox.Show("Vui lòng nhập mã nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtmanhanvien.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txttennhanvien.Text))
                {
                    MessageBox.Show("Vui lòng nhập họ tên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txttennhanvien.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtsodienthoai.Text))
                {
                    MessageBox.Show("Vui lòng nhập số điện thoại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtsodienthoai.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtmail.Text))
                {
                    MessageBox.Show("Vui lòng nhập email!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtmail.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtchucvu.Text))
                {
                    MessageBox.Show("Vui lòng nhập chức vụ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtchucvu.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtloaitaikhoan.Text))
                {
                    MessageBox.Show("Vui lòng nhập loại tài khoản!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtloaitaikhoan.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtmatkhau.Text))
                {
                    MessageBox.Show("Vui lòng nhập mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtmatkhau.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txttrangthai.Text))
                {
                    MessageBox.Show("Vui lòng nhập trạng thái!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txttrangthai.Focus();
                    return;
                }

                // Chuỗi truy vấn chèn dữ liệu vào bảng NhanVien
                string query = "INSERT INTO NhanVien (MaNV, HoTen, SoDienThoai, Email, ChucVu, LoaiTaiKhoan, MatKhau, AnhNhanVien, TrangThai) " +
                               "VALUES (@MaNV, @HoTen, @SoDienThoai, @Email, @ChucVu, @LoaiTaiKhoan, @MatKhau, @AnhNhanVien, @TrangThai)";

                // Mở kết nối
                if (cn.State == ConnectionState.Closed)
                {
                    cn.Open();
                }

                using (SqlCommand command = new SqlCommand(query, cn))
                {
                    // Gán các tham số cho câu truy vấn
                    command.Parameters.AddWithValue("@MaNV", txtmanhanvien.Text);  // Đúng là txtmanhanvien.Text
                    command.Parameters.AddWithValue("@HoTen", txttennhanvien.Text);
                    command.Parameters.AddWithValue("@SoDienThoai", txtsodienthoai.Text);
                    command.Parameters.AddWithValue("@Email", txtmail.Text);
                    command.Parameters.AddWithValue("@ChucVu", txtchucvu.Text);
                    command.Parameters.AddWithValue("@LoaiTaiKhoan", txtloaitaikhoan.Text);
                    command.Parameters.AddWithValue("@MatKhau", txtmatkhau.Text);
                    command.Parameters.AddWithValue("@AnhNhanVien", txtanh.Text);  // Đảm bảo bạn đã có dữ liệu hình ảnh
                    command.Parameters.AddWithValue("@TrangThai", txttrangthai.Text);

                    // Thực thi truy vấn
                    command.ExecuteNonQuery();
                    MessageBox.Show("Thêm nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SaveImage();
                    // Làm mới dữ liệu sau khi thêm thành công (nếu cần)
                    loaddata();
                    xoatext();
                    lockphantu();
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Đảm bảo đóng kết nối
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
            }

        }

        private void btsave_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra dữ liệu đầu vào
                if (string.IsNullOrWhiteSpace(txtmanhanvien.Text))
                {
                    MessageBox.Show("Vui lòng nhập mã nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtmanhanvien.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txttennhanvien.Text))
                {
                    MessageBox.Show("Vui lòng nhập họ tên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txttennhanvien.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtsodienthoai.Text))
                {
                    MessageBox.Show("Vui lòng nhập số điện thoại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtsodienthoai.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtmail.Text))
                {
                    MessageBox.Show("Vui lòng nhập email!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtmail.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtchucvu.Text))
                {
                    MessageBox.Show("Vui lòng nhập chức vụ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtchucvu.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtloaitaikhoan.Text))
                {
                    MessageBox.Show("Vui lòng nhập loại tài khoản!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtloaitaikhoan.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtmatkhau.Text))
                {
                    MessageBox.Show("Vui lòng nhập mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtmatkhau.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txttrangthai.Text))
                {
                    MessageBox.Show("Vui lòng nhập trạng thái!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txttrangthai.Focus();
                    return;
                }

                // Chuỗi truy vấn cập nhật dữ liệu trong bảng NhanVien
                string query = "UPDATE NhanVien SET HoTen = @HoTen, SoDienThoai = @SoDienThoai, Email = @Email, ChucVu = @ChucVu, " +
                               "LoaiTaiKhoan = @LoaiTaiKhoan, MatKhau = @MatKhau, AnhNhanVien = @AnhNhanVien, TrangThai = @TrangThai " +
                               "WHERE MaNV = @MaNV";

                // Mở kết nối
                if (cn.State == ConnectionState.Closed)
                {
                    cn.Open();
                }

                using (SqlCommand command = new SqlCommand(query, cn))
                {
                    // Gán các tham số cho câu truy vấn
                    command.Parameters.AddWithValue("@MaNV", txtmanhanvien.Text);  // Mã nhân viên để xác định bản ghi cần cập nhật
                    command.Parameters.AddWithValue("@HoTen", txttennhanvien.Text);
                    command.Parameters.AddWithValue("@SoDienThoai", txtsodienthoai.Text);
                    command.Parameters.AddWithValue("@Email", txtmail.Text);
                    command.Parameters.AddWithValue("@ChucVu", txtchucvu.Text);
                    command.Parameters.AddWithValue("@LoaiTaiKhoan", txtloaitaikhoan.Text);
                    command.Parameters.AddWithValue("@MatKhau", txtmatkhau.Text);
                    command.Parameters.AddWithValue("@AnhNhanVien", txtanh.Text);  // Đảm bảo bạn đã có dữ liệu hình ảnh
                    command.Parameters.AddWithValue("@TrangThai", txttrangthai.Text);

                    // Thực thi truy vấn
                    command.ExecuteNonQuery();
                    MessageBox.Show("Cập nhật thông tin nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (anhcanxoa != txtanh.Text)
                    { 
                    SaveImage();
                    pictureBox1.Image = null;
                    DeleteFile(anhcanxoa);

                    }
                   
                    // Làm mới dữ liệu sau khi cập nhật thành công (nếu cần)
                    anhcanxoa = txtanh.Text;
                    loaddata();
                    xoatext();
                    lockphantu();
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Đảm bảo đóng kết nối
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
            }

        }

        private void làmMớiTrangToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lockphantu();
            xoatext();
        }
    }
}
