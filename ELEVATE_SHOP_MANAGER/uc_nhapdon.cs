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
using Image = System.Drawing.Image;

namespace ELEVATE_SHOP_MANAGER
{
    public partial class uc_nhapdon : UserControl
    {
        SqlConnection cn = ketnoidb.Ketnoidata();
        public uc_nhapdon()
        {
            InitializeComponent();
            txtgianhap.KeyPress += new KeyPressEventHandler(Chinhapso);
            txtgiasp.KeyPress += new KeyPressEventHandler(Chinhapso);
            
        }
        private void Chinhapso(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }
        private void lockphantu()
        {
            txtmaphieu.Enabled = false;
            txtmaphieu2.Enabled = false;
            txtmanv.Enabled = false;
            txtngaynhap.Enabled = false;
            txtmasp.Enabled = false;
            txttensp.Enabled = false;
            txthang.Enabled = false;
            txttinhtrang.Enabled = false;   
            txtserial.Enabled = false;
            txtgiasp.Enabled = false;
            txtanhsp.Enabled = false;
            btchonanh.Enabled = false;  
            btcheck.Enabled = false;
            txtcauhinh.Enabled = false;
            btcong.Enabled = false;
            bttru.Enabled = false;
           // lvchitietdonnhap.Enabled = false;
            btadd.Visible = false;
            txttimkiem.Enabled = true;
            bttimkiem.Enabled = true;  
            gridviewdsdonnhap.Enabled = true;
            txtgianhap.Enabled = false;

        }
        private void xoatext()
        {
            txttimkiem.Clear();
            txtmaphieu.Clear();
            txtmaphieu2.Clear();
            txtmanv.Clear();
            txtmasp.Clear();
            txttensp.Clear();
            txthang.Clear();
            txttinhtrang.SelectedIndex = -1;
            txtserial.Clear();
            txtgiasp.Clear();
            txtanhsp.Clear();
            txtcauhinh.Clear();
            txtngaynhap.Value = DateTime.Now;
            lvchitietdonnhap.Items.Clear();
            pictureBox1.Image = null;
            txtgianhap.Clear();

        }
        private void unlockphantu()
        {
            txtmaphieu.Enabled = true;
            txtmaphieu2.Enabled = true;
            txtmanv.Enabled = true;
            txtngaynhap.Enabled = true;
            txtmasp.Enabled = true;
            txttensp.Enabled = true;
            txthang.Enabled = true;
            txttinhtrang.Enabled = true;
            txtserial.Enabled = true;
            txtgiasp.Enabled = true;
            txtanhsp.Enabled = true;
            btchonanh.Enabled = true;
            btcheck.Enabled = true;
            txtcauhinh.Enabled = true;
            btcong.Enabled = true;
            bttru.Enabled = true;
            lvchitietdonnhap.Enabled = true;
            txtgianhap.Enabled = true;
        }


        private void txtmaphieu_TextChanged(object sender, EventArgs e)
        {
            txtmaphieu2.Text=txtmaphieu.Text;
        }

        private void uc_nhapdon_Load(object sender, EventArgs e)
        {
            loaddataphieu();
            lockphantu();
            xoatext();
        }
        private void LoadImage(string filePath)
        {
            try
            {
                // Kiểm tra xem file có tồn tại không
                if (!File.Exists(filePath))
                {
                    //MessageBox.Show("File không tồn tại: " + filePath);
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

        private void hủyThaoTácToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            loaddataphieu();
            xoatext();
            lockphantu();
        }

        private void inPhiếuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            xoatext();
            btadd.Visible = true;
            txtmaphieu.Enabled = true;
            txtmanv.Enabled = true;
            txtngaynhap.Enabled = true;
            txtmasp.Enabled = true;
            btcheck.Enabled = true;
            btcong.Enabled = true;
            bttru.Enabled = true;
            lvchitietdonnhap.Enabled = true;
            txttimkiem.Enabled = false;
            bttimkiem.Enabled = false;
            gridviewdsdonnhap.Enabled = false;
        }
        public void loaddataphieu()
        {
            if (cn.State == ConnectionState.Closed)
            {
                cn.Open();
            }
            string sql = "select * from DonNHap";
            SqlCommand com = new SqlCommand(sql, cn);
            com.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable(); //tạo một kho ảo để lưu trữ dữ liệu
            da.Fill(dt);  // đổ dữ liệu vào kho
            cn.Close();  // đóng kết nối
            gridviewdsdonnhap.DataSource = dt; //đổ dữ liệu vào datagridview
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
                    txttensp.Text = reader["TenSanPham"].ToString();
                    txthang.Text = reader["Hang"].ToString();
                    txtgiasp.Text = reader["GiaBan"].ToString();
                    txtgianhap.Text = reader["GiaCost"].ToString();
                    txttinhtrang.Text = reader["TinhTrang"].ToString();
                    txtcauhinh.Text = reader["MoTa"].ToString();
                    // Giả sử bạn có trường lưu đường dẫn ảnh
                    txtanhsp.Text = reader["AnhSanPham"].ToString();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy sản phẩm với mã sản phẩm này.");
                }

                reader.Close();
                LoadImage(txtanhsp.Text);
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
        public String masanphamcheck;
        private void btcheck_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtmaphieu.Text))
            {
                MessageBox.Show("Vui lòng nhập mã phiếu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtmaphieu.Focus();
                return;
            }
            // Kiểm tra TextBox ngày mua
            if (string.IsNullOrWhiteSpace(txtngaynhap.Text))
            {
                MessageBox.Show("Vui lòng nhập ngày nhập dơn !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtngaynhap.Focus();
                return;
            }

            // Kiểm tra TextBox mã nhân viên
            if (string.IsNullOrWhiteSpace(txtmanv.Text))
            {
                MessageBox.Show("Vui lòng nhập mã nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtmanv.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtmasp.Text))
            {
                MessageBox.Show("Vui lòng nhập mã sản phẩm !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtmasp.Focus();
                return;
            }
            masanphamcheck = txtmasp.Text;
            if (Kiemtrasanpham(txtmasp.Text))
            {
                MessageBox.Show("Sản phẩm đã tồn tại trong kho vui lòng nhập serial cho sản phẩm ");
                txtserial.Enabled = true;
                //txtmasp.Enabled = false;
                txttensp.Enabled = false;
                txthang.Enabled = false;
                txttinhtrang.Enabled = false;           
                txtgiasp.Enabled = false;
                txtanhsp.Enabled = false;
                btchonanh.Enabled = false;
                txtcauhinh.Enabled = false;
                txtgianhap.Enabled = false;

                loadthongtinsptxt(txtmasp.Text);
            }
            else {
                MessageBox.Show("Sản phẩm đã chưa tồn tại trong kho ! vui lòng nhập chi tiết của sản phẩm ");
                txtmasp.Enabled = true;
                txttensp.Enabled = true;
                txthang.Enabled = true;
                txttinhtrang.Enabled = true;
                txtserial.Enabled = true;
                txtgiasp.Enabled = true;
                txtanhsp.Enabled = true;
                btchonanh.Enabled = true;
                txtcauhinh.Enabled = true;
                txtgianhap.Enabled = true;
                txttensp.Clear();
                txthang.Clear();
                txttinhtrang.SelectedIndex = -1;
                txtserial.Clear();
                txtgianhap.Clear();
                txtgiasp.Clear();
                txtanhsp.Clear();
                txtcauhinh.Clear();
                pictureBox1.Image = null;
            }

        }
        private void addspvaokho()
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

                if (string.IsNullOrWhiteSpace(txtgiasp.Text))
                {
                    MessageBox.Show("Vui lòng nhập giá sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtgiasp.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txthang.Text))
                {
                    MessageBox.Show("Vui lòng nhập hãng sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txthang.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtserial.Text))
                {
                    MessageBox.Show("Vui lòng nhập serial !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtserial.Focus();
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
                string query = "INSERT INTO KhoHang (MaSP, TenSanPham, MoTa, AnhSanPham, GiaBan,GiaCost, SoLuongTonKho, Hang, TinhTrang) " +
                               "VALUES (@MaSP, @TenSanPham, @MoTa, @AnhSanPham, @GiaBan,@GiaCost, @SoLuongTonKho, @Hang, @TinhTrang)";

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
                    command.Parameters.AddWithValue("@AnhSanPham", txtanhsp.Text);
                    command.Parameters.AddWithValue("@GiaBan", Convert.ToDecimal(txtgiasp.Text));
                    command.Parameters.AddWithValue("@GiaCost", Convert.ToDecimal(txtgianhap.Text));
                    command.Parameters.AddWithValue("@SoLuongTonKho",0);
                    command.Parameters.AddWithValue("@Hang", txthang.Text);
                    command.Parameters.AddWithValue("@TinhTrang", txttinhtrang.SelectedItem.ToString());

                    // Thực thi truy vấn
                    command.ExecuteNonQuery();
                    MessageBox.Show("Thêm sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SaveImage();
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
                string relativePath = @"Images_sanpham\" + Path.GetFileName(selectedFilePath);

                // Hiển thị đường dẫn tương đối lên TextBox
                txtanhsp.Text = relativePath;

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
        private bool Checkmasp(System.Windows.Forms.ListView listView, string masp)
        {
            foreach (ListViewItem item in listView.Items)
            {
                // Giả sử mã sản phẩm nằm ở cột thứ hai (SubItem[1])
                if (item.SubItems[2].Text == masp)
                {
                    return true; // Mã sản phẩm đã tồn tại
                }
            }
            return false; // Mã sản phẩm chưa tồn tại
        }
        private void addlistviewdssp()
        {
            // Kiểm tra các TextBox có bị trống hay không
            if (string.IsNullOrWhiteSpace(txtmasp.Text))
            {
                MessageBox.Show("Vui lòng nhập mã sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtmasp.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtserial.Text))
            {
                MessageBox.Show("Vui lòng nhập số lượng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtserial.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtmaphieu.Text))
            {
                MessageBox.Show("Vui lòng nhập mã phiếu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtmaphieu.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txttensp.Text))
            {
                MessageBox.Show("Vui lòng nhập tên sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txttensp.Focus();
                return;
            }            
            // Kiểm tra TextBox ngày mua
            if (string.IsNullOrWhiteSpace(txtngaynhap.Text))
            {
                MessageBox.Show("Vui lòng nhập ngày mua!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtngaynhap.Focus();
                return;
            }

            // Kiểm tra TextBox mã nhân viên
            if (string.IsNullOrWhiteSpace(txtmanv.Text))
            {
                MessageBox.Show("Vui lòng nhập mã nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtmanv.Focus();
                return;
            }
            // Lấy mã sản phẩm từ TextBox
            string serial = txtserial.Text;

            // Kiểm tra nếu mã sản phẩm đã tồn tại
            if (Checkmasp(lvchitietdonnhap, serial) == false)
            {
               
                    // Tạo một item mới nếu mã sản phẩm chưa tồn tại
                    ListViewItem item = new ListViewItem(txtmaphieu2.Text);  // Cột đầu tiên là mã phiếu

                    // Thêm các subitem tương ứng cho các cột tiếp theo
                     item.SubItems.Add(txtmasp.Text);  // Mã sản phẩm
                     item.SubItems.Add(txtserial.Text); 
                     item.SubItems.Add(txttensp.Text);  // Tên sản phẩm  
                    // Thêm item vào ListView
                    lvchitietdonnhap.Items.Add(item);
               
            }
            else
            {
                MessageBox.Show("Serial sản phẩm đã tồn tại trong danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btcong_Click(object sender, EventArgs e)
        {
           
                // Kiểm tra và thêm sản phẩm vào ListView nếu mã sản phẩm đã tồn tại
                if (Kiemtrasanpham(txtmasp.Text))
                {
                if ( !string.IsNullOrWhiteSpace(txtserial.Text))            
                {
                    addlistviewdssp();
                    if (txtmasp.Text != masanphamcheck)
                    {
                        // Nếu tất cả các TextBox đều hợp lệ, thì mới xóa các TextBox sau khi thêm thành công
                        //txtmasp.Clear();
                        txttensp.Clear();
                        txthang.Clear();
                        txttinhtrang.SelectedIndex = -1;
                        
                        txtgiasp.Clear();
                        txtgianhap.Clear();
                        txtanhsp.Clear();
                        txtcauhinh.Clear();
                        //txtngaynhap.Value = DateTime.Now;
                        pictureBox1.Image = null;
                    }
                    txtserial.Clear();
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin trước khi thêm sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
                else
                {
                    // Thêm sản phẩm vào kho nếu chưa tồn tại
                if (!string.IsNullOrWhiteSpace(txtmasp.Text) &&
                    !string.IsNullOrWhiteSpace(txttensp.Text) &&
                    !string.IsNullOrWhiteSpace(txthang.Text) &&
                    !string.IsNullOrWhiteSpace(txtserial.Text) &&
                    !string.IsNullOrWhiteSpace(txtgiasp.Text) &&
                    !string.IsNullOrWhiteSpace(txtcauhinh.Text) &&
                    txttinhtrang.SelectedIndex != -1)  // Kiểm tra xem tình trạng đã được chọn chưa
                {
                    addspvaokho();
                    addlistviewdssp();
                    if (txtmasp.Text != masanphamcheck)
                    {
                        // Nếu tất cả các TextBox đều hợp lệ, thì mới xóa các TextBox sau khi thêm thành công
                        //txtmasp.Clear();
                        txttensp.Clear();
                        txthang.Clear();
                        txttinhtrang.SelectedIndex = -1;
                        
                        txtgiasp.Clear();
                        txtgianhap.Clear();
                        txtanhsp.Clear();
                        txtcauhinh.Clear();
                        //txtngaynhap.Value = DateTime.Now;
                        pictureBox1.Image = null;
                    }
                    txtserial.Clear();
                    txttensp.Enabled = false;
                    txthang.Enabled = false;
                    txttinhtrang.Enabled = false;

                    txtgiasp.Enabled = false;
                    txtgianhap.Enabled = false;
                    txtanhsp.Enabled = false;
                    txtcauhinh.Enabled = false;
                  
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin trước khi thêm sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

      
                
            }

        private void bttru_Click(object sender, EventArgs e)
        {
            try
            {
                int index = lvchitietdonnhap.SelectedIndices[0]; // Lấy chỉ số của item được chọn
                if (index >= 0)
                {
                    lvchitietdonnhap.Items.RemoveAt(index); // Xóa item theo chỉ số
                }
                //txtmasp.Clear();
                txttensp.Clear();
                txthang.Clear();
                txttinhtrang.SelectedIndex = -1;
                txtserial.Clear();
                txtgianhap.Clear();
                txtgiasp.Clear();
                txtanhsp.Clear();
                txtcauhinh.Clear();
                pictureBox1.Image = null;
            }
            catch
            {
                MessageBox.Show("Vui lòng chọn một mục ở danh sách để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void lvchitietdonnhap_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvchitietdonnhap.SelectedItems.Count > 0)
            {
                // Lấy item đang được chọn
                ListViewItem selectedItem = lvchitietdonnhap.SelectedItems[0];

                // Hiển thị giá trị của từng cột (SubItems) lên TextBox tương ứng
                txtmasp.Text = selectedItem.SubItems[1].Text;  // Giả sử cột 1 là Mã sản phẩm
                txttensp.Text = selectedItem.SubItems[3].Text;  // Giả sử cột 2 là Tên sản phẩm

                txtserial.Text = selectedItem.SubItems[2].Text;// Giả sử cột 3 là Giá bán              
            }
            loadthongtinsptxt(txtmasp.Text);

        }

        private void btadd_Click(object sender, EventArgs e)
        {
            try
{
    if (cn.State == ConnectionState.Closed)
    {
        cn.Open();
    }

    // Bắt đầu giao dịch
    SqlTransaction transaction = cn.BeginTransaction();

    try
    {
        // 1. Thêm dữ liệu vào bảng DonNhap
        string sqlInsertDonNhap = "INSERT INTO DonNhap (MaDonNhap, NgayNhap, MaNV) VALUES (@maDonNhap, @ngayNhap, @maNV)";
        SqlCommand cmdInsertDonNhap = new SqlCommand(sqlInsertDonNhap, cn, transaction);
        cmdInsertDonNhap.Parameters.AddWithValue("@maDonNhap", txtmaphieu.Text);
        cmdInsertDonNhap.Parameters.AddWithValue("@ngayNhap", DateTime.Parse(txtngaynhap.Text));
        cmdInsertDonNhap.Parameters.AddWithValue("@maNV", txtmanv.Text);

        cmdInsertDonNhap.ExecuteNonQuery();

        // Tạo từ điển để đếm số lượng theo MaSP
        Dictionary<string, int> productCounts = new Dictionary<string, int>();

        // 2. Thêm dữ liệu vào bảng ChiTietDonNhap và Seri_sanpham
        foreach (ListViewItem item in lvchitietdonnhap.Items)
        {
            string maSP = item.SubItems[1].Text;
            string seriSP = item.SubItems[2].Text;
            string maDonNhap = item.SubItems[0].Text;
            string tenSanPham = item.SubItems[3].Text;

            // Đếm số lượng sản phẩm theo MaSP
            if (productCounts.ContainsKey(maSP))
            {
                productCounts[maSP]++;
            }
            else
            {
                productCounts[maSP] = 1;
            }

            // Thêm vào bảng ChiTietDonNhap
            string sqlInsertChiTietDonNhap = "INSERT INTO ChiTietDonNhap (MaDonNhap, MaSanPham, SeriSP, TenSanPham) " +
                                             "VALUES (@maDonNhap, @maSanPham, @seriSP, @tenSanPham)";
            SqlCommand cmdInsertChiTietDonNhap = new SqlCommand(sqlInsertChiTietDonNhap, cn, transaction);
            cmdInsertChiTietDonNhap.Parameters.AddWithValue("@maDonNhap", maDonNhap);
            cmdInsertChiTietDonNhap.Parameters.AddWithValue("@maSanPham", maSP);
            cmdInsertChiTietDonNhap.Parameters.AddWithValue("@seriSP", seriSP);
            cmdInsertChiTietDonNhap.Parameters.AddWithValue("@tenSanPham", tenSanPham);

            cmdInsertChiTietDonNhap.ExecuteNonQuery();

                        // Kiểm tra xem SeriSP có trong ChiTietPhieuBanHang không
                        string sqlCheckSeriSP = "SELECT COUNT(*) FROM ChiTietPhieuBanHang WHERE SeriSP = @seriSP";
                        SqlCommand cmdCheckSeriSP = new SqlCommand(sqlCheckSeriSP, cn, transaction);
                        cmdCheckSeriSP.Parameters.AddWithValue("@seriSP", seriSP);

                        int count = (int)cmdCheckSeriSP.ExecuteScalar();
                        string tinhTrang = count > 0 ? "Da ban" : "Chua ban";

                        // Thêm vào bảng Seri_sanpham với trạng thái phù hợp
                        string sqlInsertSeriSanPham = "INSERT INTO Seri_sanpham (MaSP, SeriSP, TinhTrang) VALUES (@maSP, @seriSP, @tinhTrang)";
                        SqlCommand cmdInsertSeriSanPham = new SqlCommand(sqlInsertSeriSanPham, cn, transaction);
                        cmdInsertSeriSanPham.Parameters.AddWithValue("@maSP", maSP);
                        cmdInsertSeriSanPham.Parameters.AddWithValue("@seriSP", seriSP);
                        cmdInsertSeriSanPham.Parameters.AddWithValue("@tinhTrang", tinhTrang);

                        cmdInsertSeriSanPham.ExecuteNonQuery();
                    }

        // 3. Cập nhật tồn kho
        foreach (var entry in productCounts)
        {
            string maSP = entry.Key;
            int soLuongNhap = entry.Value;

            // Kiểm tra tồn kho hiện tại
            string sqlCheckTonKho = "SELECT SoLuongTonKho FROM KhoHang WHERE MaSP = @maSP";
            SqlCommand cmdCheckTonKho = new SqlCommand(sqlCheckTonKho, cn, transaction);
            cmdCheckTonKho.Parameters.AddWithValue("@maSP", maSP);

            object result = cmdCheckTonKho.ExecuteScalar();
            int soLuongHienCo = result != null ? Convert.ToInt32(result) : 0;

            // Cập nhật tồn kho
            int soLuongMoi = soLuongHienCo + soLuongNhap;
            string sqlUpdateTonKho;

                // Nếu đã có tồn kho, cập nhật số lượng
                sqlUpdateTonKho = "UPDATE KhoHang SET SoLuongTonKho = @soLuongMoi WHERE MaSP = @maSP";
            

            SqlCommand cmdUpdateTonKho = new SqlCommand(sqlUpdateTonKho, cn, transaction);
            cmdUpdateTonKho.Parameters.AddWithValue("@maSP", maSP);
            cmdUpdateTonKho.Parameters.AddWithValue("@soLuongMoi", soLuongMoi);

            cmdUpdateTonKho.ExecuteNonQuery();
        }

        // Commit giao dịch nếu không có lỗi
        transaction.Commit();
        xoatext();
        lockphantu();
        MessageBox.Show("Thêm dữ liệu thành công và cập nhật tồn kho.");
    }
    catch (Exception ex)
    {
        // Rollback giao dịch nếu có lỗi
        transaction.Rollback();
        MessageBox.Show("Có lỗi xảy ra: " + ex.Message);
    }
}
catch (Exception ex)
{
    MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message);
}
finally
{
    if (cn.State == ConnectionState.Open)
    {
        cn.Close();
    }
}


        }
        public void LoadDataToListView(String maphieu)
        {
            // Câu truy vấn SQL đúng
            string query = "SELECT * FROM ChiTietDonNhap WHERE MaDonNhap = @maphieunhap";

            if (cn.State == ConnectionState.Closed)
            {
                cn.Open();
            }

            try
            {
                SqlCommand cmd = new SqlCommand(query, cn);
                // Thêm tham số cho câu lệnh SQL
                cmd.Parameters.AddWithValue("@maphieunhap", maphieu);

                SqlDataReader reader = cmd.ExecuteReader();

                // Xóa các item cũ trong ListView
                lvchitietdonnhap.Items.Clear();

                // Đọc dữ liệu từ SqlDataReader và đổ vào ListView
                while (reader.Read())
                {
                    // Tạo một ListViewItem với cột đầu tiên là mã phiếu
                    ListViewItem item = new ListViewItem(reader["MaDonNhap"].ToString());

                    // Thêm các cột tiếp theo
                    item.SubItems.Add(reader["MaSanPham"].ToString());  // Mã sản phẩm
                    
                    item.SubItems.Add(reader["SeriSP"].ToString());  // Số lượng
                   item.SubItems.Add(reader["TenSanPham"].ToString());  // Tên sản phẩm
                     

                    // Thêm item vào ListView
                    lvchitietdonnhap.Items.Add(item);
                }

                reader.Close(); // Đóng SqlDataReader
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close(); // Đóng kết nối sau khi hoàn thành
                }
            }
        }

        private void gridviewdsdonnhap_SelectionChanged(object sender, EventArgs e)
        {   xoatext();
            int dongchon = -1;
            dongchon = gridviewdsdonnhap.CurrentCellAddress.Y;
            if (dongchon >= 0)
            {
                txtmaphieu.Text = gridviewdsdonnhap.Rows[dongchon].Cells["MaDonNhap"].Value.ToString();
                txtmanv.Text = gridviewdsdonnhap.Rows[dongchon].Cells["MaNV"].Value.ToString();
                txtngaynhap.Text = gridviewdsdonnhap.Rows[dongchon].Cells["NgayNhap"].Value.ToString();
                
                LoadDataToListView(txtmaphieu.Text);
            }
        }

        private void tạoPhiếuToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void btsave_Click(object sender, EventArgs e)
        {
            
        }

        private void xóaPhiếuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result2 = MessageBox.Show("Bạn có chắc chắn muốn xóa phiếu nhập này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result2 == DialogResult.Yes)
            {
                // Thực hiện hành động xóa phiếu nhập
                try
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    // Bắt đầu giao dịch
                    SqlTransaction transaction = cn.BeginTransaction();

                    try
                    {
                        string maDonNhap = txtmaphieu.Text;

                        // 1. Tạo từ điển lưu số lượng sản phẩm để cập nhật tồn kho
                        Dictionary<string, int> productCounts = new Dictionary<string, int>();

                        // 2. Duyệt qua ListView để lấy thông tin sản phẩm và seri
                        foreach (ListViewItem item in lvchitietdonnhap.Items)
                        {
                            string maSP = item.SubItems[1].Text;  // Mã sản phẩm
                            string seriSP = item.SubItems[2].Text;  // Seri sản phẩm

                            // Tính số lượng sản phẩm
                            if (productCounts.ContainsKey(maSP))
                            {
                                productCounts[maSP]++;
                            }
                            else
                            {
                                productCounts[maSP] = 1;
                            }

                            // Xóa từng bản ghi trong Seri_sanpham
                            string sqlDeleteSeri = "DELETE FROM Seri_sanpham WHERE MaSP = @maSP AND SeriSP = @seriSP";
                            SqlCommand cmdDeleteSeri = new SqlCommand(sqlDeleteSeri, cn, transaction);
                            cmdDeleteSeri.Parameters.AddWithValue("@maSP", maSP);
                            cmdDeleteSeri.Parameters.AddWithValue("@seriSP", seriSP);

                            cmdDeleteSeri.ExecuteNonQuery();
                            // 4. Xóa dữ liệu trong ChiTietDonNhap
                            string sqlDeleteChiTiet = "DELETE FROM ChiTietDonNhap WHERE MaDonNhap = @maDonNhap";
                            SqlCommand cmdDeleteChiTiet = new SqlCommand(sqlDeleteChiTiet, cn, transaction);
                            cmdDeleteChiTiet.Parameters.AddWithValue("@maDonNhap", maDonNhap);
                            cmdDeleteChiTiet.ExecuteNonQuery();
                        }

                        // 3. Cập nhật tồn kho dựa trên số lượng sản phẩm đã xóa
                        foreach (var entry in productCounts)
                        {
                            string maSP = entry.Key;
                            int soLuongXoa = entry.Value;

                            // Lấy tồn kho hiện tại
                            string sqlCheckTonKho = "SELECT SoLuongTonKho FROM KhoHang WHERE MaSP = @maSP";
                            SqlCommand cmdCheckTonKho = new SqlCommand(sqlCheckTonKho, cn, transaction);
                            cmdCheckTonKho.Parameters.AddWithValue("@maSP", maSP);

                            object result = cmdCheckTonKho.ExecuteScalar();
                            int soLuongHienCo = result != null ? Convert.ToInt32(result) : 0;

                            // Cập nhật tồn kho
                            int soLuongMoi = soLuongHienCo - soLuongXoa;

                            string sqlUpdateTonKho = "UPDATE KhoHang SET SoLuongTonKho = @soLuongMoi WHERE MaSP = @maSP";
                            SqlCommand cmdUpdateTonKho = new SqlCommand(sqlUpdateTonKho, cn, transaction);
                            cmdUpdateTonKho.Parameters.AddWithValue("@maSP", maSP);
                            cmdUpdateTonKho.Parameters.AddWithValue("@soLuongMoi", soLuongMoi);

                            cmdUpdateTonKho.ExecuteNonQuery();
                        }



                        // 5. Xóa phiếu nhập trong DonNhap
                        string sqlDeleteDonNhap = "DELETE FROM DonNhap WHERE MaDonNhap = @maDonNhap";
                        SqlCommand cmdDeleteDonNhap = new SqlCommand(sqlDeleteDonNhap, cn, transaction);
                        cmdDeleteDonNhap.Parameters.AddWithValue("@maDonNhap", maDonNhap);
                        cmdDeleteDonNhap.ExecuteNonQuery();

                        // Commit giao dịch nếu thành công
                        transaction.Commit();
                        xoatext();  // Xóa dữ liệu trên giao diện
                        lockphantu();  // Khóa giao diện sau khi xóa
                        MessageBox.Show("Xóa dữ liệu thành công và cập nhật tồn kho.");
                    }
                    catch (Exception ex)
                    {
                        // Rollback giao dịch nếu có lỗi
                        transaction.Rollback();
                        MessageBox.Show("Có lỗi xảy ra: " + ex.Message);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message);
                }
                finally
                {
                    if (cn.State == ConnectionState.Open)
                    {
                        cn.Close();
                    }
                }

            }
            else
            {
                // Nếu người dùng chọn No, không làm gì cả
                MessageBox.Show("Phiếu nhập chưa được xóa.");
            }

        }

        private void inPhiếuToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            f_inphieunhap obj = new f_inphieunhap(txtmaphieu.Text); // Khởi tạo đối tượng
            obj.ShowDialog();

        }

        private void bttimkiem_Click(object sender, EventArgs e)
        {

            if (cn.State == ConnectionState.Closed)
            {
                cn.Open();
            }

            string madon = txttimkiem.Text.ToLower(); // Chuyển chuỗi nhập từ người dùng thành chữ thường
            string sql = "select * from DonNhap where LOWER(MaDonNhap) like @productName";

            SqlCommand com = new SqlCommand(sql, cn);
            com.CommandType = CommandType.Text;
            com.Parameters.AddWithValue("@productName", "%" + madon + "%"); // Sử dụng ký tự % để tìm kiếm gần đúng không phân biệt hoa thường

            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable(); // Tạo một kho ảo để lưu trữ dữ liệu
            da.Fill(dt);  // Đổ dữ liệu vào kho
            cn.Close();  // Đóng kết nối

            gridviewdsdonnhap.DataSource = dt; // Đổ dữ liệu vào DataGridView
        }

        private void làmMớiTrangToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loaddataphieu();
            xoatext();
            lockphantu();
        }
    }
    
}
// sửa đến phần checkmasp
