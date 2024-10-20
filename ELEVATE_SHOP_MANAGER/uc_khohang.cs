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
    public partial class uc_khohang : UserControl
    {
        SqlConnection cn = ketnoidb.Ketnoidata();
        String gname,gquyen;
        public uc_khohang(String name, String quyen)
        {
            InitializeComponent();
            this.gname = name;
            this.gquyen = quyen;
            if (this.gquyen.Trim() == "Sale")
            {
                tạoPhiếuToolStripMenuItem.Enabled = false;
                hủyThaoTácToolStripMenuItem1.Enabled = false;
                đặtToolStripMenuItem.Enabled=false;
            }
                txtgia.KeyPress += new KeyPressEventHandler(Chinhapso);
                txtsoluong.KeyPress += new KeyPressEventHandler(Chinhapso);
                txtgianhap.KeyPress += new KeyPressEventHandler(Chinhapso);
            loadhangsp();
        }

        private void đặtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            f_dathang f_Dathang = new f_dathang();
            f_Dathang.ShowDialog();
        }
        public void lockphantu()
        {

            txttensp.Enabled = false;
            txtmasp.Enabled = false;
            txtgia.Enabled = false;
            txthang.Enabled = false;
            txtsoluong.Enabled = false;
            txtcauhinh.Enabled = false;
            txtanh.Enabled = false;
            txttinhtrang.Enabled = false;
            btchoanh.Enabled = false;         
            btsave.Visible = false;
            lvserial.Enabled = false;
            txtgianhap.Enabled = false;
        }
        public void unlockphantu()
        {
            txttensp.Enabled = true;
            txtmasp.Enabled = true;
            txtgia.Enabled = true;
            txthang.Enabled = true;
            txtsoluong.Enabled = true;
            txtcauhinh.Enabled = true;
            //txtanh.Enabled = true;
            txttinhtrang.Enabled = true;
            btchoanh.Enabled = true;
            txtgianhap.Enabled=true;

        }
        public void xoatext()
        {
            txttinhtrang.SelectedIndex = -1;  // Bỏ chọn mục hiện tại
            txttensp.Clear();
            txtmasp.Clear();
            txtgia.Clear();
            txthang.Clear();
            txtsoluong.Clear();
            txtcauhinh.Clear();
            txtgianhap.Clear();
            txtanh.Clear();
            pictureBox1.Image = null;
        }
        private void loaddata()
        {
            if (cn.State == ConnectionState.Closed)
            {
                cn.Open();
            }
            string sql = "select * from KhoHang";
            SqlCommand com = new SqlCommand(sql, cn);
            com.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable(); //tạo một kho ảo để lưu trữ dữ liệu
            da.Fill(dt);  // đổ dữ liệu vào kho
            cn.Close();  // đóng kết nối
            gridviewsp.DataSource = dt; //đổ dữ liệu vào datagridview

        }
        public void loadhangsp()
        {

            cn.Open();
            SqlCommand cmd = new SqlCommand("SELECT Hang FROM KhoHang", cn);

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string seriSP = reader["Hang"].ToString();

                // Kiểm tra nếu hang chưa tồn tại trong ComboBox thì mới thêm vào
                if (!cbhang.Items.Contains(seriSP))
                {
                    cbhang.Items.Add(seriSP);
                }
            }


            cn.Close();

        }
        public void HienThiSeriSanPham(ListView listView, string maSP, string tinhTrang)
        {
            // Xóa các mục cũ trước khi thêm mới
            listView.Items.Clear();

            // Câu lệnh SQL để lấy dữ liệu dựa trên MaSP và TinhTrang
            string query = "SELECT MaSP, SeriSP, TinhTrang FROM Seri_sanpham WHERE MaSP = @MaSP AND TinhTrang = @TinhTrang";

                try
                {
                    cn.Open();
                    SqlCommand command = new SqlCommand(query, cn);
                    command.Parameters.AddWithValue("@MaSP", maSP);
                    command.Parameters.AddWithValue("@TinhTrang", tinhTrang);
                    SqlDataReader reader = command.ExecuteReader();

                    // Đọc dữ liệu và thêm vào ListView
                    while (reader.Read())
                    {
                        ListViewItem item = new ListViewItem(reader["MaSP"].ToString());
                        item.SubItems.Add(reader["SeriSP"].ToString());
                        item.SubItems.Add(reader["TinhTrang"].ToString());
                        listView.Items.Add(item);
                    }

                    reader.Close();
                cn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            
        }

            private void uc_khohang_Load(object sender, EventArgs e)
        {
            loaddata();
            lockphantu();

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
        private void SaveImage()
        {
            if (currentImagePath == null)
            {
                MessageBox.Show("Chưa có ảnh nào được chọn.");
                return;
            }

            // Tạo đường dẫn tương đối đến thư mục "Images" trong project
            string relativePath = @"Images_sanpham";
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
                MessageBox.Show("File ảnh không tồn tại: " + relativeFilePath);
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

        public string anhcanxoa;
        private void gridviewsp_SelectionChanged(object sender, EventArgs e)
        {
            lvserial.Items.Clear();
            String linkanh = " ";
            //Hiển thị data lên textbox
            int dongchon = -1;
            dongchon = gridviewsp.CurrentCellAddress.Y;
            if (dongchon >= 0)
            {
                txttensp.Text = gridviewsp.Rows[dongchon].Cells["TenSanPham"].Value.ToString();
                txtmasp.Text = gridviewsp.Rows[dongchon].Cells["MaSP"].Value.ToString();
                txtsoluong.Text = gridviewsp.Rows[dongchon].Cells["SoLuongTonKho"].Value.ToString();
                linkanh = gridviewsp.Rows[dongchon].Cells["AnhSanPham"].Value.ToString();
                txthang.Text = gridviewsp.Rows[dongchon].Cells["Hang"].Value.ToString();
                txtgianhap.Text = gridviewsp.Rows[dongchon].Cells["GiaCost"].Value.ToString();
                txtgia.Text = gridviewsp.Rows[dongchon].Cells["GiaBan"].Value.ToString();
                txtcauhinh.Text = gridviewsp.Rows[dongchon].Cells["MoTa"].Value.ToString();
                txttinhtrang.Text = gridviewsp.Rows[dongchon].Cells["TinhTrang"].Value.ToString();
                txtanh.Text = linkanh.ToString();
                anhcanxoa = linkanh.ToString();
                try { LoadImage(linkanh); }
                catch
                {
                    pictureBox1.Image = null; // Đặt PictureBox thành rỗng, không hiển thị hình ảnh
                }
                HienThiSeriSanPham(lvserial, txtmasp.Text, "Chua ban");
            }


        }

        private void hủyThaoTácToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            loaddata();
            lvserial.Items.Clear();
            pictureBox1.Image = null;
            xoatext();
            lockphantu();

        }

        private void tạoPhiếuToolStripMenuItem_Click(object sender, EventArgs e)
        {//button suwar thoong tin
            unlockphantu();
            btsave.Visible = true;
            txtmasp.Enabled = false;
            txtsoluong.Enabled = false;
            txtgia.Enabled = false; 
        }

        private void inPhiếuToolStripMenuItem_Click(object sender, EventArgs e)
        {//Buttonadd
           
        }
        private void Chinhapso(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != '-')
            {
                e.Handled = true;
            }

        }
        public string currentImagePath = null;
        private void btchoanh_Click(object sender, EventArgs e)
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
                string relativePath = @"Images_sanpham\" + Path.GetFileName(selectedFilePath);

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

        private void btsave_Click(object sender, EventArgs e)
        {
            {
                try
                {
                    // Kiểm tra từng TextBox trước khi tiếp tục
                    if (string.IsNullOrWhiteSpace(txttensp.Text))
                    {
                        MessageBox.Show("Vui lòng nhập tên sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txttensp.Focus();
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(txtmasp.Text))
                    {
                        MessageBox.Show("Vui lòng nhập mã sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtmasp.Focus();
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(txtgia.Text))
                    {
                        MessageBox.Show("Vui lòng nhập giá sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtgia.Focus();
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(txthang.Text))
                    {
                        MessageBox.Show("Vui lòng nhập hãng sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txthang.Focus();
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(txtsoluong.Text))
                    {
                        MessageBox.Show("Vui lòng nhập số lượng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtsoluong.Focus();
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(txtgianhap.Text))
                    {
                        MessageBox.Show("Vui lòng nhập giá nhập của sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtsoluong.Focus();
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(txtcauhinh.Text))
                    {
                        MessageBox.Show("Vui lòng nhập cấu hình sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtcauhinh.Focus();
                        return;
                    }

                    if (txttinhtrang.SelectedIndex == -1)  // Kiểm tra ComboBox tình trạng
                    {
                        MessageBox.Show("Vui lòng chọn tình trạng sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txttinhtrang.Focus();
                        return;
                    }

                    // Chuỗi truy vấn UPDATE để sửa thông tin sản phẩm
                    string query = "UPDATE KhoHang SET TenSanPham = @TenSanPham, MoTa = @MoTa, AnhSanPham = @AnhSanPham, GiaCost = @GiaCost, " +
                                   "SoLuongTonKho = @SoLuongTonKho, Hang = @Hang, TinhTrang = @TinhTrang WHERE MaSP = @MaSP";

                    // Mở kết nối và thực hiện truy vấn
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    using (SqlCommand command = new SqlCommand(query, cn))
                    {
                        // Gán tham số cho truy vấn
                        command.Parameters.AddWithValue("@MaSP", txtmasp.Text); // MaSP để xác định sản phẩm cần sửa
                        command.Parameters.AddWithValue("@TenSanPham", txttensp.Text);
                        command.Parameters.AddWithValue("@MoTa", txtcauhinh.Text);
                        command.Parameters.AddWithValue("@AnhSanPham", txtanh.Text);
                        command.Parameters.AddWithValue("@GiaCost", Convert.ToDecimal(txtgianhap.Text));
                        command.Parameters.AddWithValue("@SoLuongTonKho", Convert.ToInt32(txtsoluong.Text));
                        command.Parameters.AddWithValue("@Hang", txthang.Text);
                        command.Parameters.AddWithValue("@TinhTrang", txttinhtrang.SelectedItem.ToString());

                        // Thực thi truy vấn
                        command.ExecuteNonQuery();
                        MessageBox.Show("Sửa thông tin sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if (anhcanxoa != txtanh.Text)
                        {
                            SaveImage();
                            pictureBox1.Image = null;
                            DeleteFile(anhcanxoa);
                        }
                        loaddata(); // Tải lại dữ liệu sau khi sửa                    
                        xoatext(); // Xóa nội dung các ô sau khi sửa
                        lockphantu(); // Khóa các phần tử sau khi hoàn thành
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

        }

        private void btadd_Click(object sender, EventArgs e)//Thêm sản phẩm vào trong kho
        {/*
            try
            {
                // Kiểm tra từng TextBox trước khi tiếp tục
                if (string.IsNullOrWhiteSpace(txttensp.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txttensp.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtmasp.Text))
                {
                    MessageBox.Show("Vui lòng nhập mã sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtmasp.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtgia.Text))
                {
                    MessageBox.Show("Vui lòng nhập giá sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtgia.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txthang.Text))
                {
                    MessageBox.Show("Vui lòng nhập hãng sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txthang.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtsoluong.Text))
                {
                    MessageBox.Show("Vui lòng nhập số lượng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtsoluong.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtcauhinh.Text))
                {
                    MessageBox.Show("Vui lòng nhập cấu hình sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtcauhinh.Focus();
                    return;
                }              

                if (txttinhtrang.SelectedIndex == -1)  // Kiểm tra ComboBox tình trạng
                {
                    MessageBox.Show("Vui lòng chọn tình trạng sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txttinhtrang.Focus();
                    return;
                }

                // Chuỗi truy vấn
                string query = "INSERT INTO KhoHang (MaSP, TenSanPham, MoTa, AnhSanPham, GiaBan, SoLuongTonKho, Hang, TinhTrang) " +
                               "VALUES (@MaSP, @TenSanPham, @MoTa, @AnhSanPham, @GiaBan, @SoLuongTonKho, @Hang, @TinhTrang)";

                // Mở kết nối và thực hiện truy vấn
                if (cn.State == ConnectionState.Closed)
                {
                    cn.Open();
                }

                using (SqlCommand command = new SqlCommand(query, cn))
                {
                    // Gán tham số cho truy vấn
                    command.Parameters.AddWithValue("@MaSP", txtmasp.Text);
                    command.Parameters.AddWithValue("@TenSanPham", txttensp.Text);
                    command.Parameters.AddWithValue("@MoTa", txtcauhinh.Text);
                    command.Parameters.AddWithValue("@AnhSanPham", txtanh.Text);
                    command.Parameters.AddWithValue("@GiaBan", Convert.ToDecimal(txtgia.Text));
                    command.Parameters.AddWithValue("@SoLuongTonKho", Convert.ToInt32(txtsoluong.Text));
                    command.Parameters.AddWithValue("@Hang", txthang.Text);
                    command.Parameters.AddWithValue("@TinhTrang", txttinhtrang.SelectedItem.ToString());

                    // Thực thi truy vấn
                    command.ExecuteNonQuery();
                    MessageBox.Show("Thêm sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loaddata();
                    pictureBox1.Image = null;
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
            */
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string tinhTrang = cbtinhtrang.SelectedItem?.ToString() ?? "";
            string hang = cbhang.SelectedItem?.ToString() ?? "";

            string query = "SELECT * FROM KhoHang WHERE 1=1";

            if (!string.IsNullOrEmpty(tinhTrang))
            {
                query += " AND TinhTrang = @TinhTrang";
            }
            if (!string.IsNullOrEmpty(hang))
            {
                query += " AND Hang = @Hang";
            }

            try
            {
                if (cn.State == ConnectionState.Closed)
                {
                    cn.Open();  // Mở kết nối nếu đang đóng
                }

                SqlCommand command = new SqlCommand(query, cn);

                if (!string.IsNullOrEmpty(tinhTrang))
                {
                    command.Parameters.AddWithValue("@TinhTrang", tinhTrang);
                }
                if (!string.IsNullOrEmpty(hang))
                {
                    command.Parameters.AddWithValue("@Hang", hang);
                }

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                cn.Close();
                if (dataTable.Rows.Count > 0)
                {
                    gridviewsp.DataSource = dataTable; // Gán dữ liệu vào GridView
                }
                else
                {
                    gridviewsp.DataSource = null;
                    MessageBox.Show("Không có sản phẩm nào thỏa mãn điều kiện lọc.",
                                    "Thông báo",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();  // Đảm bảo đóng kết nối sau khi hoàn tất
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            loaddata();
            cbhang.SelectedIndex = -1;
            cbtinhtrang.SelectedIndex = -1;
        }

        private void thayĐổiGiáToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thaydoigia f = new Thaydoigia();
            f.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            HienThiSeriSanPham(lvserial, txtmasp.Text, "Chua ban");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            HienThiSeriSanPham(lvserial, txtmasp.Text, "Da ban");
        }

        private void làmMớiTrangToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loaddata();
            lvserial.Items.Clear();
            pictureBox1.Image = null;
            xoatext();
            lockphantu();

        }
    }
}