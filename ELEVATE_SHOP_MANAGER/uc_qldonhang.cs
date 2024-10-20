using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ELEVATE_SHOP_MANAGER
{
    public partial class uc_qldonhang : UserControl
    {
        public String gname, gquyen;
        SqlConnection cn = ketnoidb.Ketnoidata();
        public uc_qldonhang(String name,String quyen)
        {
            InitializeComponent();
            this.gname = name;
            this.gquyen = quyen;
            if (this.gquyen.Trim() == "Technician")
            {
                xóaPhiếuToolStripMenuItem.Enabled = false;
                hủyThaoTácToolStripMenuItem1.Enabled = false;

            }
        }
        public void lockphantu()
        {

            txtmaphieu.Enabled = false;
            txtngaymua.Enabled = false;
            txtmanv.Enabled = false;
            txttenkhach.Enabled = false;
            txtsdt.Enabled = false;
            txtmaphieu2.Enabled = false;
            txtmaxsp.Enabled = false;
            txttensp.Enabled = false;
            txtsoluong.Enabled = false;
            txtgia.Enabled = false;
            //lvdanhsachmua.Enabled = false;


        }
        public void unlockphantu()
        {
            //txtmaphieu.Enabled = true;
            txtngaymua.Enabled = true;
            txtmanv.Enabled = true;
            txttenkhach.Enabled = true;
            txtsdt.Enabled = true;
            //txtmaphieu2.Enabled = true;
            // txtmaxsp.Enabled = true;
            //txttensp.Enabled = true;
            txtsoluong.Enabled = true;
            // txtgia.Enabled = true;
            lvdanhsachmua.Enabled = true;

        }
        public void xoatext()
        {

            txtmaphieu.Clear();
            txtngaymua.Value = DateTime.Now;
            txtmanv.Clear();
            txttenkhach.Clear();
            txtsdt.Clear();
            txtmaphieu2.Clear();
            txtmaxsp.Clear();
            txttensp.Clear();
            txtsoluong.Clear();
            txtgia.Clear();
            lbtongtien.Text = "0";
            lvdanhsachmua.Items.Clear();

        }
        private void Chinhapso(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void uc_qldonhang_Load(object sender, EventArgs e)
        {
            lockphantu();
            loaddata();
            txtsdt.KeyPress += new KeyPressEventHandler(Chinhapso);
            
        }
        public void loaddata()
        {
            if (cn.State == ConnectionState.Closed)
            {
                cn.Open();
            }
            string sql = "select * from PhieuBanHang";
            SqlCommand com = new SqlCommand(sql, cn);
            com.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable(); //tạo một kho ảo để lưu trữ dữ liệu
            da.Fill(dt);  // đổ dữ liệu vào kho
            cn.Close();  // đóng kết nối
            gridviewhoadon.DataSource = dt; //đổ dữ liệu vào datagridview
        }

        public void LoadDataToListView(String maphieu)
        {
            // Câu truy vấn SQL đúng
            string query = "SELECT * FROM ChiTietPhieuBanHang WHERE MaPhieu = @maphieubanhang";

            if (cn.State == ConnectionState.Closed)
            {
                cn.Open();
            }

            try
            {
                SqlCommand cmd = new SqlCommand(query, cn);
                // Thêm tham số cho câu lệnh SQL
                cmd.Parameters.AddWithValue("@maphieubanhang", maphieu);

                SqlDataReader reader = cmd.ExecuteReader();

                // Xóa các item cũ trong ListView
                lvdanhsachmua.Items.Clear();

                // Đọc dữ liệu từ SqlDataReader và đổ vào ListView
                while (reader.Read())
                {
                    // Tạo một ListViewItem với cột đầu tiên là mã phiếu
                    ListViewItem item = new ListViewItem(reader["MaPhieu"].ToString());

                    // Thêm các cột tiếp theo
                    item.SubItems.Add(reader["MaSanPham"].ToString());  // Mã sản phẩm
                    item.SubItems.Add(reader["TenSanPham"].ToString());  // Tên sản phẩm
                    item.SubItems.Add(reader["SeriSP"].ToString()); 
                    item.SubItems.Add(reader["Giaban"].ToString());  // Giá

                    // Thêm item vào ListView
                    lvdanhsachmua.Items.Add(item);
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
        private void tonghoadon(String maphieu)
        {
            string query = "SELECT * FROM HoaDon WHERE MaPhieuBanHang = @maphieubanhang";  // Sửa tên cột chính xác

            if (cn.State == ConnectionState.Closed)
            {
                cn.Open();
            }

            try
            {
                SqlCommand cmd = new SqlCommand(query, cn);
                // Thêm tham số cho câu lệnh SQL
                cmd.Parameters.AddWithValue("@maphieubanhang", maphieu);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    // Lấy giá trị từ cột "SoTienCanThanhToan" và gán cho lbtongtien.Text
                    lbtongtien.Text = reader["SoTienCanThanhToan"].ToString();
                }
                else
                {
                    lbtongtien.Text = "0";
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




        private void gridviewhoadon_SelectionChanged(object sender, EventArgs e)
            {
                int dongchon = -1;
                dongchon = gridviewhoadon.CurrentCellAddress.Y;
                if (dongchon >= 0)
                {
                    txtmaphieu.Text = gridviewhoadon.Rows[dongchon].Cells["MaPhieu"].Value.ToString();
                    //txtmaphieu2.Text = gridviewhoadon.Rows[dongchon].Cells["MaPhieu"].Value.ToString();
                    txttenkhach.Text = gridviewhoadon.Rows[dongchon].Cells["TenKH"].Value.ToString();
                    txtsdt.Text = gridviewhoadon.Rows[dongchon].Cells["SoDienThoaiKH"].Value.ToString();
                    txtmanv.Text = gridviewhoadon.Rows[dongchon].Cells["MaNV"].Value.ToString();
                    txtngaymua.Text = gridviewhoadon.Rows[dongchon].Cells["NgayMuaHang"].Value.ToString();
                LoadDataToListView(txtmaphieu.Text);
                tonghoadon(txtmaphieu.Text);
                }
            }

        private void inPhiếuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_inhoadon finhoadon = new Form_inhoadon(txtmaphieu.Text);
            finhoadon.ShowDialog();
        }

        private void hủyThaoTácToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            loaddata();
            lockphantu();
            xoatext();
        }

        private void bttimkiem_Click(object sender, EventArgs e)
        {
            if (cn.State == ConnectionState.Closed)
            {
                cn.Open();
            }

            string Sdt = txttimkiem.Text.ToLower(); // Chuyển chuỗi nhập từ người dùng thành chữ thường
            string sql = "select * from PhieuBanHang where LOWER(SoDienThoaiKH) like @sdt";

            SqlCommand com = new SqlCommand(sql, cn);
            com.CommandType = CommandType.Text;
            com.Parameters.AddWithValue("@sdt", "%" + Sdt + "%"); // Sử dụng ký tự % để tìm kiếm gần đúng không phân biệt hoa thường

            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable(); // Tạo một kho ảo để lưu trữ dữ liệu
            da.Fill(dt);  // Đổ dữ liệu vào kho
            cn.Close();  // Đóng kết nối

            gridviewhoadon.DataSource = dt; // Đổ dữ liệu vào DataGridView
        }

        private void lvdanhsachmua_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Kiểm tra xem có mục nào được chọn hay không
            if (lvdanhsachmua.SelectedItems.Count > 0)
            {
                // Lấy item đang được chọn
                ListViewItem selectedItem = lvdanhsachmua.SelectedItems[0];

                // Hiển thị giá trị của từng cột (SubItems) lên TextBox tương ứng
                txtmaphieu2.Text = selectedItem.SubItems[0].Text;
                txtmaxsp.Text = selectedItem.SubItems[1].Text;  // Giả sử cột 1 là Mã sản phẩm
                txttensp.Text = selectedItem.SubItems[2].Text;  // Giả sử cột 2 là Tên sản phẩm

                txtsoluong.Text = selectedItem.SubItems[3].Text;// Giả sử cột 3 là Giá bán
                txtgia.Text = selectedItem.SubItems[4].Text;
            }
        }

        private void làmMớiTrangToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loaddata();
            lockphantu();
            xoatext();
        }

        private void xóaPhiếuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (cn.State == ConnectionState.Closed)
                {
                    cn.Open();
                }

                // Bắt đầu giao dịch để đảm bảo tính nhất quán
                SqlTransaction transaction = cn.BeginTransaction();

                try
                {
                    // 1. Đếm số lần xuất hiện của mỗi sản phẩm trong ListView
                    var sanPhamCount = new Dictionary<string, int>();

                    foreach (ListViewItem item in lvdanhsachmua.Items)
                    {
                        string maSanPham = item.SubItems[1].Text; // Mã sản phẩm

                        // Tăng số lượng xuất hiện của sản phẩm trong từ điển
                        if (sanPhamCount.ContainsKey(maSanPham))
                        {
                            sanPhamCount[maSanPham]++;
                        }
                        else
                        {
                            sanPhamCount[maSanPham] = 1;
                        }
                    }

                    // 2. Cập nhật lại tồn kho dựa trên số lần xuất hiện của sản phẩm
                    foreach (var kvp in sanPhamCount)
                    {
                        string maSanPham = kvp.Key;
                        int soLuong = kvp.Value;

                        string sqlUpdateKho = "UPDATE KhoHang SET SoLuongTonKho = SoLuongTonKho + @soLuong WHERE MaSP = @maSanPham";
                        SqlCommand cmdUpdateKho = new SqlCommand(sqlUpdateKho, cn, transaction);

                        cmdUpdateKho.Parameters.AddWithValue("@maSanPham", maSanPham);
                        cmdUpdateKho.Parameters.AddWithValue("@soLuong", soLuong);

                        // Thực thi lệnh cập nhật tồn kho
                        cmdUpdateKho.ExecuteNonQuery();
                    }

                    // 3. Cập nhật tình trạng sản phẩm về mặc định 'Chưa bán'
                    foreach (ListViewItem item in lvdanhsachmua.Items)
                    {
                        string seriSP = item.SubItems[3].Text; // Seri sản phẩm

                        string sqlUpdateTinhTrang = "UPDATE Seri_sanpham SET TinhTrang = 'Chua ban' WHERE SeriSP = @seriSP";
                        SqlCommand cmdUpdateTinhTrang = new SqlCommand(sqlUpdateTinhTrang, cn, transaction);
                        cmdUpdateTinhTrang.Parameters.AddWithValue("@seriSP", seriSP);

                        // Thực thi lệnh cập nhật tình trạng
                        cmdUpdateTinhTrang.ExecuteNonQuery();
                    }

                    // 4. Xóa chi tiết phiếu bán hàng
                    string sqlDeleteChiTiet = "DELETE FROM ChiTietPhieuBanHang WHERE MaPhieu = @maPhieu";
                    SqlCommand cmdDeleteChiTiet = new SqlCommand(sqlDeleteChiTiet, cn, transaction);
                    cmdDeleteChiTiet.Parameters.AddWithValue("@maPhieu", txtmaphieu.Text);
                    cmdDeleteChiTiet.ExecuteNonQuery();

                    // 5. Xóa hóa đơn liên quan
                    string sqlDeleteHoaDon = "DELETE FROM HoaDon WHERE MaPhieuBanHang = @maPhieuBanHang";
                    SqlCommand cmdDeleteHoaDon = new SqlCommand(sqlDeleteHoaDon, cn, transaction);
                    cmdDeleteHoaDon.Parameters.AddWithValue("@maPhieuBanHang", txtmaphieu.Text);
                    cmdDeleteHoaDon.ExecuteNonQuery();

                    // 6. Xóa phiếu bán hàng
                    string sqlDeletePhieu = "DELETE FROM PhieuBanHang WHERE MaPhieu = @maPhieu";
                    SqlCommand cmdDeletePhieu = new SqlCommand(sqlDeletePhieu, cn, transaction);
                    cmdDeletePhieu.Parameters.AddWithValue("@maPhieu", txtmaphieu.Text);
                    cmdDeletePhieu.ExecuteNonQuery();

                    // 7. Commit giao dịch nếu tất cả thành công
                    transaction.Commit();
                    MessageBox.Show("Đơn hàng và hóa đơn đã được xóa thành công.");

                    // Tải lại dữ liệu và làm mới giao diện
                    loaddata();
                    lockphantu();
                    xoatext();
                }
                catch (Exception ex)
                {
                    // Rollback giao dịch nếu có lỗi xảy ra
                    transaction.Rollback();
                    MessageBox.Show("Đã xảy ra lỗi khi xóa đơn hàng: " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message);
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
    }
    

