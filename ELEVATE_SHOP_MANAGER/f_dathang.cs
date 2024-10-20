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
    public partial class f_dathang : Form
    {
        SqlConnection cn = ketnoidb.Ketnoidata();
        public f_dathang()
        {
            InitializeComponent();
            txtsoluong.KeyPress += new KeyPressEventHandler(Chinhapso);
        }

        private void tạoPhiếuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            unlock();
            btsave.Visible = true;
            btadd.Visible = false;
            txtmaphieu2.Enabled = false;
            txtmaphieu.Enabled = false;
            txttensp.Enabled = false;
            txtmasp.Enabled = false;
            loaddatasp();
            grdsphieudathang.Enabled = true;
            loaddatadondat();
            xoatext();
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
            txtngaydat.Enabled = false;
            txtmanv.Enabled = false;
            txtmaphieu2.Enabled = false;
            txtmasp.Enabled = false;
            txttensp.Enabled = false;
            txtsoluong.Enabled = false;
            gridviewsp.Enabled = false;
            txttimkiem.Enabled = false;
            btcong.Enabled = false;
            bttru.Enabled = false;
            btadd.Visible = false;
            btsave.Visible = false;
        }
        private void unlock()
        {
            txtmaphieu.Enabled = true;
            txtngaydat.Enabled = true;
            txtmanv.Enabled = true;
            txtmaphieu2.Enabled = true;
            txtmasp.Enabled = true;
            txttensp.Enabled = true;
            txtsoluong.Enabled = true;
            gridviewsp.Enabled = true;
            txttimkiem.Enabled = true;
            btcong.Enabled = true;
            bttru.Enabled = true;
        }

        private void xoatext()
        {
            txtmaphieu.Clear();
            txtngaydat.Value = DateTime.Now;
            txtmanv.Clear();
            txtmaphieu2.Clear();
            txtmasp.Clear();
            txttensp.Clear();
            txtsoluong.Clear();
            txttimkiem.Clear();
            lvdanhsach.Items.Clear();

        }
        public void loaddatasp()
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
        public void loaddatadondat()
        {
            if (cn.State == ConnectionState.Closed)
            {
                cn.Open();
            }
            string sql = "select * from DatHang";
            SqlCommand com = new SqlCommand(sql, cn);
            com.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable(); //tạo một kho ảo để lưu trữ dữ liệu
            da.Fill(dt);  // đổ dữ liệu vào kho
            cn.Close();  // đóng kết nối
            grdsphieudathang.DataSource = dt; //đổ dữ liệu vào datagridview
        }

        private void f_dathang_Load(object sender, EventArgs e)
        {
            lockphantu();
            loaddatadondat();
        }

        private void inPhiếuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            unlock();
            btadd.Visible = true;
            btsave.Visible = false;
            txtmaphieu2.Enabled = false;
            txttensp.Enabled = false;
            txtmasp.Enabled = false;    
            loaddatasp();
            grdsphieudathang.Enabled = false;
            loaddatadondat();
            xoatext();
        }

        private void gridviewsp_SelectionChanged(object sender, EventArgs e)
        {
            //Hiển thị data lên textbox
            int dongchon = -1;
            dongchon = gridviewsp.CurrentCellAddress.Y;
            if (dongchon >= 0)
            {
                txtmasp.Text = gridviewsp.Rows[dongchon].Cells["MaSP"].Value.ToString();
                txttensp.Text = gridviewsp.Rows[dongchon].Cells["TenSanPham"].Value.ToString();

            }
        }

        private void txtmaphieu_TextChanged(object sender, EventArgs e)
        {
            txtmaphieu2.Text = txtmaphieu.Text;
        }
        private bool Checkmasp(System.Windows.Forms.ListView listView, string masp)
        {
            foreach (ListViewItem item in listView.Items)
            {
                // Giả sử mã sản phẩm nằm ở cột thứ hai (SubItem[1])
                if (item.SubItems[1].Text == masp)
                {
                    return true; // Mã sản phẩm đã tồn tại
                }
            }
            return false; // Mã sản phẩm chưa tồn tại
        }

        private void btcong_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtmasp.Text))
            {
                MessageBox.Show("Vui lòng nhập mã sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtmasp.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtsoluong.Text))
            {
                MessageBox.Show("Vui lòng nhập số lượng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtsoluong.Focus();
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



            if (string.IsNullOrWhiteSpace(txtngaydat.Text))
            {
                MessageBox.Show("Vui lòng nhập ngày mua!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtngaydat.Focus();
                return;
            }

            // Kiểm tra TextBox mã nhân viên
            if (string.IsNullOrWhiteSpace(txtmanv.Text))
            {
                MessageBox.Show("Vui lòng nhập mã nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtmanv.Focus();
                return;
            }
            if (Checkmasp(lvdanhsach, txtmasp.Text) == false)
            {
                // Tạo một item mới nếu mã sản phẩm chưa tồn tại
                ListViewItem item = new ListViewItem(txtmaphieu.Text);  // Cột đầu tiên là mã phiếu

                // Thêm các subitem tương ứng cho các cột tiếp theo
                item.SubItems.Add(txtmasp.Text);  // Mã sản phẩm
                item.SubItems.Add(txttensp.Text);  // Tên sản phẩm
                item.SubItems.Add(txtsoluong.Text);  // Số lượng

                // Thêm item vào ListView
                lvdanhsach.Items.Add(item);

                // Reset các TextBox sau khi thêm thành công
                txtmasp.Clear();
                txttensp.Clear();
                txtsoluong.Clear();


            }
            else
            {
                MessageBox.Show("Mã sản phẩm đã tồn tại trong danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void bttru_Click(object sender, EventArgs e)
        {
            try
            {
                int index = lvdanhsach.SelectedIndices[0]; // Lấy chỉ số của item được chọn
                if (index >= 0)
                {
                    lvdanhsach.Items.RemoveAt(index); // Xóa item theo chỉ số
                }
                txttensp.Clear();
                txtsoluong.Clear();
                txtmasp.Clear();
            }
            catch
            {
                MessageBox.Show("Vui lòng chọn một mục ở danh sách giỏ hàng để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void hủyThaoTácToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            xoatext();
            lockphantu();
            loaddatadondat();
        }

        private void btadd_Click(object sender, EventArgs e)
        {
            try
            {
                if (cn.State == ConnectionState.Closed)
                {
                    cn.Open();
                }

                // Bắt đầu một giao dịch để đảm bảo tất cả các thao tác đều thành công hoặc rollback
                SqlTransaction transaction = cn.BeginTransaction();

                try
                {
                    // 1. Chèn dữ liệu vào bảng DatHang
                    string sqlInsertDatHang = "INSERT INTO DatHang (MaDonDat, NgayDat, MaNV) VALUES (@maDonDat, @ngayDat, @maNV)";
                    SqlCommand cmdInsertDatHang = new SqlCommand(sqlInsertDatHang, cn, transaction);
                    cmdInsertDatHang.CommandType = CommandType.Text;

                    // Thêm tham số để ngăn chặn SQL injection
                    cmdInsertDatHang.Parameters.AddWithValue("@maDonDat", txtmaphieu.Text);

                    // Chuyển đổi chuỗi thành kiểu DateTime
                    DateTime ngayDat;
                    if (DateTime.TryParse(txtngaydat.Text, out ngayDat))
                    {
                        cmdInsertDatHang.Parameters.AddWithValue("@ngayDat", ngayDat);
                    }
                    else
                    {
                        throw new Exception("Ngày đặt không hợp lệ.");
                    }

                    cmdInsertDatHang.Parameters.AddWithValue("@maNV", txtmanv.Text);

                    // Thực thi lệnh chèn vào bảng DatHang
                    cmdInsertDatHang.ExecuteNonQuery();

                    // 2. Chèn vào bảng ChiTietDonDatHang
                    foreach (ListViewItem item in lvdanhsach.Items) // Giả định ListView chứa các sản phẩm
                    {
                        string maSP = item.SubItems[1].Text; // Cột 1: Mã sản phẩm
                        string madon = item.SubItems[0].Text; // Cột 0: Mã đơn
                        int soLuong;

                        // Kiểm tra số lượng xem có hợp lệ không
                        if (!int.TryParse(item.SubItems[3].Text, out soLuong))
                        {
                            MessageBox.Show("Số lượng không hợp lệ cho sản phẩm: " + maSP + ". Vui lòng kiểm tra lại dữ liệu.", "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return; // Dừng lại nếu phát hiện lỗi
                        }

                        string tenSanPham = item.SubItems[2].Text; // Cột 2: Tên sản phẩm

                        // Chèn vào bảng ChiTietDonDatHang
                        string sqlInsertChiTietDonDatHang = "INSERT INTO ChiTietDonDatHang (MaDonDat, MaSP, SoLuong, TenSanPham) " +
                                                            "VALUES (@maDonDat, @maSP, @soLuong, @tenSanPham)";
                        SqlCommand cmdInsertChiTietDonDatHang = new SqlCommand(sqlInsertChiTietDonDatHang, cn, transaction);
                        cmdInsertChiTietDonDatHang.Parameters.AddWithValue("@maDonDat", madon);
                        cmdInsertChiTietDonDatHang.Parameters.AddWithValue("@maSP", maSP);
                        cmdInsertChiTietDonDatHang.Parameters.AddWithValue("@soLuong", soLuong);
                        cmdInsertChiTietDonDatHang.Parameters.AddWithValue("@tenSanPham", tenSanPham);

                        cmdInsertChiTietDonDatHang.ExecuteNonQuery();
                    }

                    // Commit giao dịch nếu tất cả các thao tác đều thành công
                    transaction.Commit();
                    f_inphieudathang finhoadon = new f_inphieudathang(txtmaphieu.Text);
                    finhoadon.ShowDialog();
                    MessageBox.Show("Dữ liệu đã được thêm vào DatHang và ChiTietDonDatHang thành công.");
                    xoatext();
                    lockphantu();
                    loaddatadondat();
                    grdsphieudathang.Enabled = true;
                }
                catch (Exception ex)
                {
                    // Rollback giao dịch nếu có lỗi xảy ra
                    transaction.Rollback();
                    MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
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

        private void lvdanhsach_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvdanhsach.SelectedItems.Count > 0)
            {
                // Lấy item đang được chọn
                ListViewItem selectedItem = lvdanhsach.SelectedItems[0];

                // Hiển thị giá trị của từng cột (SubItems) lên TextBox tương ứng
                txtmaphieu2.Text = selectedItem.SubItems[0].Text;  // Giả sử cột 1 là Mã sản phẩm
                txtmasp.Text = selectedItem.SubItems[1].Text;  // Giả sử cột 2 là Tên sản phẩm

                txtsoluong.Text = selectedItem.SubItems[3].Text;// Giả sử cột 3 là Giá bán
                txttensp.Text = selectedItem.SubItems[2].Text;
            }
        }
        public void LoadDataToListView(String maphieu)
        {
            // Câu truy vấn SQL đúng
            string query = "SELECT * FROM ChiTietDonDatHang WHERE MaDonDat = @madon";

            if (cn.State == ConnectionState.Closed)
            {
                cn.Open();
            }

            try
            {
                SqlCommand cmd = new SqlCommand(query, cn);
                // Thêm tham số cho câu lệnh SQL
                cmd.Parameters.AddWithValue("@madon", maphieu);

                SqlDataReader reader = cmd.ExecuteReader();

                // Xóa các item cũ trong ListView
                lvdanhsach.Items.Clear();

                // Đọc dữ liệu từ SqlDataReader và đổ vào ListView
                while (reader.Read())
                {
                    // Tạo một ListViewItem với cột đầu tiên là mã phiếu
                    ListViewItem item = new ListViewItem(reader["MaDonDat"].ToString());

                    // Thêm các cột tiếp theo
                    item.SubItems.Add(reader["MaSP"].ToString());  // Mã sản phẩm
                    item.SubItems.Add(reader["TenSanPham"].ToString());  // Tên sản phẩm
                    item.SubItems.Add(reader["SoLuong"].ToString());  // Số lượng                 

                    // Thêm item vào ListView
                    lvdanhsach.Items.Add(item);
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
        private void grdsphieudathang_SelectionChanged(object sender, EventArgs e)
        {
            int dongchon = -1;
            dongchon = grdsphieudathang.CurrentCellAddress.Y;
            if (dongchon >= 0)
            {
                txtmaphieu.Text = grdsphieudathang.Rows[dongchon].Cells["MaDonDat"].Value.ToString();
                txtngaydat.Text = grdsphieudathang.Rows[dongchon].Cells["NgayDat"].Value.ToString();
                txtmanv.Text = grdsphieudathang.Rows[dongchon].Cells["MaNV"].Value.ToString();

                LoadDataToListView(txtmaphieu.Text);
            }

        }

        private void btsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (cn.State == ConnectionState.Closed)
                {
                    cn.Open();
                }

                // Bắt đầu một giao dịch để đảm bảo tất cả các thao tác đều thành công hoặc rollback
                SqlTransaction transaction = cn.BeginTransaction();

                try
                {
                    // 1. Cập nhật dữ liệu vào bảng DatHang
                    string sqlUpdateDatHang = "UPDATE DatHang SET NgayDat = @ngayDat, MaNV = @maNV WHERE MaDonDat = @maDonDat";
                    SqlCommand cmdUpdateDatHang = new SqlCommand(sqlUpdateDatHang, cn, transaction);
                    cmdUpdateDatHang.CommandType = CommandType.Text;

                    // Thêm tham số để ngăn chặn SQL injection
                    cmdUpdateDatHang.Parameters.AddWithValue("@maDonDat", txtmaphieu.Text);

                    // Chuyển đổi chuỗi thành kiểu DateTime
                    DateTime ngayDat;
                    if (DateTime.TryParse(txtngaydat.Text, out ngayDat))
                    {
                        cmdUpdateDatHang.Parameters.AddWithValue("@ngayDat", ngayDat);
                    }
                    else
                    {
                        throw new Exception("Ngày đặt không hợp lệ.");
                    }

                    cmdUpdateDatHang.Parameters.AddWithValue("@maNV", txtmanv.Text);

                    // Thực thi lệnh cập nhật vào bảng DatHang
                    cmdUpdateDatHang.ExecuteNonQuery();

                    // 2. Xóa dữ liệu cũ trong ChiTietDonDatHang trước khi thêm mới
                    string sqlDeleteChiTiet = "DELETE FROM ChiTietDonDatHang WHERE MaDonDat = @maDonDat";
                    SqlCommand cmdDeleteChiTiet = new SqlCommand(sqlDeleteChiTiet, cn, transaction);
                    cmdDeleteChiTiet.Parameters.AddWithValue("@maDonDat", txtmaphieu.Text);
                    cmdDeleteChiTiet.ExecuteNonQuery();

                    // 3. Thêm lại dữ liệu vào bảng ChiTietDonDatHang
                    foreach (ListViewItem item in lvdanhsach.Items) // Giả định ListView chứa các sản phẩm
                    {
                        string maSP = item.SubItems[1].Text; // Cột 1: Mã sản phẩm
                        string madon = item.SubItems[0].Text; // Cột 0: Mã đơn
                        int soLuong;

                        // Kiểm tra số lượng xem có hợp lệ không
                        if (!int.TryParse(item.SubItems[3].Text, out soLuong))
                        {
                            MessageBox.Show("Số lượng không hợp lệ cho sản phẩm: " + maSP + ". Vui lòng kiểm tra lại dữ liệu.", "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return; // Dừng lại nếu phát hiện lỗi
                        }

                        string tenSanPham = item.SubItems[2].Text; // Cột 2: Tên sản phẩm

                        // Chèn lại vào bảng ChiTietDonDatHang
                        string sqlInsertChiTietDonDatHang = "INSERT INTO ChiTietDonDatHang (MaDonDat, MaSP, SoLuong, TenSanPham) " +
                                                            "VALUES (@maDonDat, @maSP, @soLuong, @tenSanPham)";
                        SqlCommand cmdInsertChiTietDonDatHang = new SqlCommand(sqlInsertChiTietDonDatHang, cn, transaction);
                        cmdInsertChiTietDonDatHang.Parameters.AddWithValue("@maDonDat", madon);
                        cmdInsertChiTietDonDatHang.Parameters.AddWithValue("@maSP", maSP);
                        cmdInsertChiTietDonDatHang.Parameters.AddWithValue("@soLuong", soLuong);
                        cmdInsertChiTietDonDatHang.Parameters.AddWithValue("@tenSanPham", tenSanPham);

                        cmdInsertChiTietDonDatHang.ExecuteNonQuery();
                    }

                    // Commit giao dịch nếu tất cả các thao tác đều thành công
                    transaction.Commit();
                    f_inphieudathang finhoadon = new f_inphieudathang(txtmaphieu.Text);
                    finhoadon.ShowDialog();
                    MessageBox.Show("Dữ liệu đã được cập nhật vào DatHang và ChiTietDonDatHang thành công.");
                    
                    lockphantu();
                    loaddatadondat();
                    grdsphieudathang.Enabled = true;
                    xoatext();
                }
                catch (Exception ex)
                {
                    // Rollback giao dịch nếu có lỗi xảy ra
                    transaction.Rollback();
                    MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
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

        private void xóaPhiếuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn xóa đơn đặt này không ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    // Bắt đầu một giao dịch để đảm bảo tất cả các thao tác đều thành công hoặc rollback
                    SqlTransaction transaction = cn.BeginTransaction();

                    try
                    {
                        // 1. Xóa dữ liệu trong bảng ChiTietDonDatHang trước
                        string sqlDeleteChiTiet = "DELETE FROM ChiTietDonDatHang WHERE MaDonDat = @maDonDat";
                        SqlCommand cmdDeleteChiTiet = new SqlCommand(sqlDeleteChiTiet, cn, transaction);
                        cmdDeleteChiTiet.Parameters.AddWithValue("@maDonDat", txtmaphieu.Text);

                        // Thực thi lệnh xóa
                        cmdDeleteChiTiet.ExecuteNonQuery();

                        // 2. Xóa dữ liệu trong bảng DatHang
                        string sqlDeleteDatHang = "DELETE FROM DatHang WHERE MaDonDat = @maDonDat";
                        SqlCommand cmdDeleteDatHang = new SqlCommand(sqlDeleteDatHang, cn, transaction);
                        cmdDeleteDatHang.Parameters.AddWithValue("@maDonDat", txtmaphieu.Text);

                        // Thực thi lệnh xóa
                        cmdDeleteDatHang.ExecuteNonQuery();

                        // Commit giao dịch nếu tất cả các thao tác đều thành công
                        transaction.Commit();
                        MessageBox.Show("Dữ liệu đã được xóa thành công.");

                        // Tải lại dữ liệu hoặc làm mới giao diện sau khi xóa
                        
                        lockphantu();
                        loaddatadondat();
                        grdsphieudathang.Enabled = true;xoatext();
                    }
                    catch (Exception ex)
                    {
                        // Rollback giao dịch nếu có lỗi xảy ra
                        transaction.Rollback();
                        MessageBox.Show("Đã xảy ra lỗi khi xóa: " + ex.Message);
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
            else if (result == DialogResult.No)
            {
                lockphantu();
                loaddatadondat();
                grdsphieudathang.Enabled = true; xoatext();
            }

        }

        private void inPhiếuToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            f_inphieudathang finhoadon = new f_inphieudathang(txtmaphieu.Text);
            finhoadon.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (cn.State == ConnectionState.Closed)
            {
                cn.Open();
            }

            string tensanpham = txttimphieu.Text.ToLower(); // Chuyển chuỗi nhập từ người dùng thành chữ thường
            string sql = "select * from DatHang where LOWER(MaDonDat) like @productName";

            SqlCommand com = new SqlCommand(sql, cn);
            com.CommandType = CommandType.Text;
            com.Parameters.AddWithValue("@productName", "%" + tensanpham + "%"); // Sử dụng ký tự % để tìm kiếm gần đúng không phân biệt hoa thường

            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable(); // Tạo một kho ảo để lưu trữ dữ liệu
            da.Fill(dt);  // Đổ dữ liệu vào kho
            cn.Close();  // Đóng kết nối

            grdsphieudathang.DataSource = dt; // Đổ dữ liệu vào DataGridView
        }
    }
}
