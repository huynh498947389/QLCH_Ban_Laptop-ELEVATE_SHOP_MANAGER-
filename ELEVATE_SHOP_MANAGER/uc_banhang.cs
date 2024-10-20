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
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ELEVATE_SHOP_MANAGER
{
    public partial class uc_banhang : UserControl
    {
        SqlConnection cn = ketnoidb.Ketnoidata();
        public String gname, gquyen;
        public uc_banhang(String name ,String quyen)
        {
            InitializeComponent();
            this.gname = name;
            this.gquyen = quyen;
        }
        public void loaddata()
        {
            if (cn.State == ConnectionState.Closed)
            {
                cn.Open();
            }
            string sql = "select * from KhoHang where TinhTrang = @tinhtrang  AND SoLuongTonKho>0";
            SqlCommand com = new SqlCommand(sql, cn);
            com.Parameters.AddWithValue("@tinhtrang", "Kinh doanh");
            com.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable(); //tạo một kho ảo để lưu trữ dữ liệu
            da.Fill(dt);  // đổ dữ liệu vào kho
            cn.Close();  // đóng kết nối
            gridviewsp.DataSource = dt; //đổ dữ liệu vào datagridview
        }
        public void loadseri(string masp)
        {
            
                cn.Open();
                SqlCommand cmd = new SqlCommand("SELECT SeriSP FROM Seri_sanpham where MaSP = @Masp AND TinhTrang = @tinhtrang", cn);

                // Thêm tham số trước khi thực thi lệnh
                cmd.Parameters.AddWithValue("@Masp", masp);
                cmd.Parameters.AddWithValue("@tinhtrang", "Chua ban");

                SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string seriSP = reader["SeriSP"].ToString();

                // Kiểm tra nếu seri chưa tồn tại trong ComboBox thì mới thêm vào
                if (!txtseri.Items.Contains(seriSP))
                {
                    txtseri.Items.Add(seriSP);
                }
            }
        

                cn.Close();
            
        }


        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void lbgiaban_Click(object sender, EventArgs e)
        {

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
            txtseri.Enabled = false;
            txtgia.Enabled = false;
            btthemds.Enabled = false;
            btxoads.Enabled = false;
            lvdanhsachmua.Enabled = false;
            btluu.Visible = false;


        }
        public void unlockphantu()
        {
            txtmaphieu.Enabled = true;
            txtngaymua.Enabled = true;
            txtmanv.Enabled = true;
            txttenkhach.Enabled = true;
            txtsdt.Enabled = true;
            //txtmaphieu2.Enabled = true;
            // txtmaxsp.Enabled = true;
            //txttensp.Enabled = true;
            txtseri.Enabled = true;
            // txtgia.Enabled = true;
            btthemds.Enabled = true;
            btxoads.Enabled = true;
            lvdanhsachmua.Enabled = true;
            btluu.Visible = true;

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
            txtseri.SelectedIndex = -1;
            txtgia.Clear();
            lbtongtien.Text = "0";
            lvdanhsachmua.Items.Clear();

        }

        private void uc_banhang_Load(object sender, EventArgs e)
        {
            lockphantu();
            loaddata();
            txtsdt.KeyPress += new KeyPressEventHandler(Chinhapso);
        }

        private void hủyThaoTácToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lockphantu();
            xoatext();
        }

        private void tạoPhiếuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            unlockphantu();
        }
        private void Chinhapso(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }
        String tinhtrang = null;
        private void gridviewsp_SelectionChanged(object sender, EventArgs e)
        {
            //Hiển thị data lên textbox
            int dongchon = -1;
            dongchon = gridviewsp.CurrentCellAddress.Y;
            if (dongchon >= 0)
            {
                txtseri.Items.Clear();
                txtseri.Text = "";
                txtmaxsp.Text = gridviewsp.Rows[dongchon].Cells["MaSP"].Value.ToString();
                txttensp.Text = gridviewsp.Rows[dongchon].Cells["TenSanPham"].Value.ToString();
                txtgia.Text = gridviewsp.Rows[dongchon].Cells["GiaBan"].Value.ToString();
                tinhtrang = gridviewsp.Rows[dongchon].Cells["TinhTrang"].Value.ToString();
                loadseri(gridviewsp.Rows[dongchon].Cells["MaSP"].Value.ToString());
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
      
        private ulong Tinhtongthanhtoan(System.Windows.Forms.ListView listView)
        {
            ulong tongthanhtoan = 0;

            // Duyệt qua tất cả các mục (items) trong ListView
            foreach (ListViewItem item in listView.Items)
            {
                // Kiểm tra và chuyển đổi giá trị trong cột cần tính tổng
                if (ulong.TryParse(item.SubItems[4].Text, out ulong giaBan))
                {
                    // Cộng giá trị của cột (ở đây là cột thứ 6 - SubItems[5])
                    tongthanhtoan += giaBan;
                }
            }

            return tongthanhtoan;
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
                if (item.SubItems[3].Text == masp)
                {
                    return true; // Mã sản phẩm đã tồn tại
                }
            }
            return false; // Mã sản phẩm chưa tồn tại
        }

        private void btthemds_Click(object sender, EventArgs e)
        {
                // Kiểm tra các TextBox có bị trống hay không
                if (string.IsNullOrWhiteSpace(txtmaxsp.Text))
                {
                    MessageBox.Show("Vui lòng nhập mã sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtmaxsp.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtseri.Text))
                {
                    MessageBox.Show("Vui lòng chọn seri sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtseri.Focus();
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

                if (string.IsNullOrWhiteSpace(txtgia.Text))
                {
                    MessageBox.Show("Vui lòng nhập giá sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtgia.Focus();
                    return;
                }
            // Kiểm tra TextBox ngày mua
            if (string.IsNullOrWhiteSpace(txtngaymua.Text))
            {
                MessageBox.Show("Vui lòng nhập ngày mua!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtngaymua.Focus();
                return;
            }

            // Kiểm tra TextBox mã nhân viên
            if (string.IsNullOrWhiteSpace(txtmanv.Text))
            {
                MessageBox.Show("Vui lòng nhập mã nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtmanv.Focus();
                return;
            }

            // Kiểm tra TextBox tên khách hàng
            if (string.IsNullOrWhiteSpace(txttenkhach.Text))
            {
                MessageBox.Show("Vui lòng nhập tên khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txttenkhach.Focus();
                return;
            }

            // Kiểm tra TextBox số điện thoại
            if (string.IsNullOrWhiteSpace(txtsdt.Text))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtsdt.Focus();
                return;
            }

            // Lấy mã sản phẩm từ TextBox
            string serisp = txtseri.Text;

                // Kiểm tra nếu mã sản phẩm đã tồn tại
                if (Checkmasp(lvdanhsachmua, serisp) == false)
                {
                    if(tinhtrang == "Kinh doanh") { 
                    // Tạo một item mới nếu mã sản phẩm chưa tồn tại
                    ListViewItem item = new ListViewItem(txtmaphieu.Text);  // Cột đầu tiên là mã phiếu

                    // Thêm các subitem tương ứng cho các cột tiếp theo
                    item.SubItems.Add(txtmaxsp.Text);  // Mã sản phẩm
                    item.SubItems.Add(txttensp.Text);  // Tên sản phẩm
                    item.SubItems.Add(txtseri.Text);  // Số lượng
                    item.SubItems.Add(txtgia.Text);  // Giá sản phẩm

                    // Thêm item vào ListView
                    lvdanhsachmua.Items.Add(item);

                    // Reset các TextBox sau khi thêm thành công
                    //txtmaxsp.Clear();
                    //txttensp.Clear();
                    // txtseri.SelectedIndex = -1;
                    // txtgia.Clear();
                    txtseri.Text = "";
                    txtseri.Items.Remove(serisp);
                }
                else
                {
                    MessageBox.Show("Sản phẩm này không thuộc nhóm hàng được kinh doanh !");
                }
                }
                else
                {
                    MessageBox.Show("Mã sản phẩm đã tồn tại trong danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                lbtongtien.Text = Tinhtongthanhtoan(lvdanhsachmua).ToString();
            }

        private void lvdanhsachmua_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Kiểm tra xem có mục nào được chọn hay không
            if (lvdanhsachmua.SelectedItems.Count > 0)
            {
                // Lấy item đang được chọn
                ListViewItem selectedItem = lvdanhsachmua.SelectedItems[0];

                // Hiển thị giá trị của từng cột (SubItems) lên TextBox tương ứng
                txtmaxsp.Text = selectedItem.SubItems[1].Text;  // Giả sử cột 1 là Mã sản phẩm
                txttensp.Text = selectedItem.SubItems[2].Text;  // Giả sử cột 2 là Tên sản phẩm
               
                txtseri.Text = selectedItem.SubItems[3].Text;// Giả sử cột 3 là Giá bán
                txtgia.Text = selectedItem.SubItems[4].Text;
            }
        }

        private void btxoads_Click(object sender, EventArgs e)
        {
            try
            { 
                int index = lvdanhsachmua.SelectedIndices[0]; // Lấy chỉ số của item được chọn
                if (index >= 0)
                    {
                lvdanhsachmua.Items.RemoveAt(index); // Xóa item theo chỉ số
                   }
                lbtongtien.Text = Tinhtongthanhtoan(lvdanhsachmua).ToString();

            }
            catch{
                MessageBox.Show("Vui lòng chọn một mục ở danh sách giỏ hàng để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }
        private void ThemHoaDon()
        {
            try
            {
                if (cn.State == ConnectionState.Closed)
                {
                    cn.Open();
                }

                // Bắt đầu một giao dịch để đảm bảo tất cả các thao tác đều thành công hoặc bị rollback
                SqlTransaction transaction = cn.BeginTransaction();

                try
                {
                    // Chèn dữ liệu vào bảng HoaDon
                    string sqlInsertHoaDon = "INSERT INTO HoaDon (MaHoaDon, MaPhieuBanHang, TenKH,Ngaytt, SDT_KH, SoTienCanThanhToan) " +
                                             "VALUES (@maHoaDon, @maPhieuBanHang, @tenKH,@ngaytt, @sdtKH, @soTienCanThanhToan)";
                    SqlCommand cmdInsertHoaDon = new SqlCommand(sqlInsertHoaDon, cn, transaction);
                    cmdInsertHoaDon.CommandType = CommandType.Text;

                    // Thêm tham số để ngăn chặn SQL injection
                    cmdInsertHoaDon.Parameters.AddWithValue("@maHoaDon", txtmaphieu2.Text); // Giả sử txtMaHoaDon là TextBox để nhập liệu
                    cmdInsertHoaDon.Parameters.AddWithValue("@maPhieuBanHang", txtmaphieu.Text); // Giả sử txtMaPhieuBanHang là TextBox để nhập liệu
                    cmdInsertHoaDon.Parameters.AddWithValue("@tenKH", txttenkhach.Text); // Giả sử txtTenKH là TextBox để nhập liệu
                    cmdInsertHoaDon.Parameters.AddWithValue("@sdtKH", txtsdt.Text); // Giả sử txtSDT_KH là TextBox để nhập liệu
                    DateTime ngayMua;
                    if (DateTime.TryParse(txtngaymua.Text, out ngayMua))
                    {
                        cmdInsertHoaDon.Parameters.AddWithValue("@ngaytt", ngayMua);
                    }
                    else
                    {
                        throw new Exception("Ngày mua hàng không hợp lệ.");
                    }

                    cmdInsertHoaDon.Parameters.AddWithValue("@soTienCanThanhToan", Tinhtongthanhtoan(lvdanhsachmua).ToString());
                 

                    // Thực thi lệnh chèn
                    cmdInsertHoaDon.ExecuteNonQuery();

                    // Commit giao dịch nếu thành công
                    transaction.Commit();
                    MessageBox.Show("Đơn hàng đã được tạo và tồn kho đã cập nhật thành công.");
                    Form_inhoadon finhoadon = new Form_inhoadon(txtmaphieu.Text);
                    finhoadon.ShowDialog();
                    lockphantu();
                    xoatext();

                }
                catch (Exception ex)
                {
                    // Rollback giao dịch nếu xảy ra lỗi
                    transaction.Rollback();
                    MessageBox.Show("Đã xảy ra lỗi khi thêm hóa đơn: " + ex.Message);
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


        private void addphieumuahangvadanhsachsanpham()
        {
            try
            {
                if (cn.State == ConnectionState.Closed)
                {
                    cn.Open();
                }

                // Bắt đầu một giao dịch để đảm bảo tất cả các thao tác đều thành công hoặc bị rollback
                SqlTransaction transaction = cn.BeginTransaction();

                try
                {
                    // 1. Thêm phiếu mua hàng vào bảng PhieuBanHang
                    string sqlInsertPhieu = "INSERT INTO PhieuBanHang (MaPhieu, TenKH, SoDienThoaiKH, NgayMuaHang, MaNV) " +
                                            "VALUES (@maPhieu, @tenKH, @soDienThoaiKH, @ngayMuaHang, @maNV)";
                    SqlCommand cmdInsertPhieu = new SqlCommand(sqlInsertPhieu, cn, transaction);

                    // Thêm tham số để tránh SQL Injection
                    cmdInsertPhieu.Parameters.AddWithValue("@maPhieu", txtmaphieu.Text);
                    cmdInsertPhieu.Parameters.AddWithValue("@tenKH", txttenkhach.Text);
                    cmdInsertPhieu.Parameters.AddWithValue("@soDienThoaiKH", txtsdt.Text);

                    // Chuyển đổi chuỗi thành kiểu DateTime và kiểm tra tính hợp lệ
                    if (DateTime.TryParse(txtngaymua.Text, out DateTime ngayMua))
                    {
                        cmdInsertPhieu.Parameters.AddWithValue("@ngayMuaHang", ngayMua);
                    }
                    else
                    {
                        throw new Exception("Ngày mua hàng không hợp lệ.");
                    }

                    cmdInsertPhieu.Parameters.AddWithValue("@maNV", txtmanv.Text);

                    // Thực thi lệnh chèn phiếu mua hàng
                    cmdInsertPhieu.ExecuteNonQuery();

                    // 2. Tạo từ điển để đếm số lượng mã sản phẩm trong ListView
                    var sanPhamCount = new Dictionary<string, int>();

                    foreach (ListViewItem item in lvdanhsachmua.Items)
                    {
                        string maSanPham = item.SubItems[1].Text; // Cột 1: Mã sản phẩm

                        // Đếm số lần xuất hiện của mỗi mã sản phẩm
                        if (sanPhamCount.ContainsKey(maSanPham))
                        {
                            sanPhamCount[maSanPham]++;
                        }
                        else
                        {
                            sanPhamCount[maSanPham] = 1;
                        }
                    }

                    // 3. Thêm chi tiết phiếu bán hàng từ ListView và cập nhật tình trạng sản phẩm
                    foreach (ListViewItem item in lvdanhsachmua.Items)
                    {
                        string maSanPham = item.SubItems[1].Text;  // Cột 1: Mã sản phẩm
                        string tenSanPham = item.SubItems[2].Text; // Cột 2: Tên sản phẩm
                        string seriSP = item.SubItems[3].Text;     // Cột 3: Seri sản phẩm
                        decimal giaBan = decimal.Parse(item.SubItems[4].Text); // Cột 4: Giá bán

                        // Chèn vào bảng ChiTietPhieuBanHang
                        string sqlInsertChiTiet = "INSERT INTO ChiTietPhieuBanHang (MaPhieu, MaSanPham, TenSanPham, SeriSP, GiaBan) " +
                                                  "VALUES (@maPhieu, @maSanPham, @tenSanPham, @seriSP, @giaBan)";
                        SqlCommand cmdInsertChiTiet = new SqlCommand(sqlInsertChiTiet, cn, transaction);

                        cmdInsertChiTiet.Parameters.AddWithValue("@maPhieu", txtmaphieu.Text);
                        cmdInsertChiTiet.Parameters.AddWithValue("@maSanPham", maSanPham);
                        cmdInsertChiTiet.Parameters.AddWithValue("@tenSanPham", tenSanPham);
                        cmdInsertChiTiet.Parameters.AddWithValue("@seriSP", seriSP);
                        cmdInsertChiTiet.Parameters.AddWithValue("@giaBan", giaBan);

                        // Thực thi lệnh chèn chi tiết phiếu bán hàng
                        cmdInsertChiTiet.ExecuteNonQuery();

                        // Cập nhật tình trạng sản phẩm trong bảng Seri_sanpham thành 'Đã bán'
                        string sqlUpdateTinhTrang = "UPDATE Seri_sanpham SET TinhTrang = 'Da ban' WHERE SeriSP = @seriSP";
                        SqlCommand cmdUpdateTinhTrang = new SqlCommand(sqlUpdateTinhTrang, cn, transaction);
                        cmdUpdateTinhTrang.Parameters.AddWithValue("@seriSP", seriSP);
                        cmdUpdateTinhTrang.ExecuteNonQuery();
                    }

                    // 4. Cập nhật tồn kho dựa trên số lượng sản phẩm đã đếm
                    foreach (var kvp in sanPhamCount)
                    {
                        string maSanPham = kvp.Key;
                        int soLuong = kvp.Value;

                        string sqlUpdateKho = "UPDATE KhoHang SET SoLuongTonKho = SoLuongTonKho - @soLuong " +
                                              "WHERE MaSP = @maSanPham AND SoLuongTonKho >= @soLuong";
                        SqlCommand cmdUpdateKho = new SqlCommand(sqlUpdateKho, cn, transaction);

                        cmdUpdateKho.Parameters.AddWithValue("@maSanPham", maSanPham);
                        cmdUpdateKho.Parameters.AddWithValue("@soLuong", soLuong);

                        // Kiểm tra nếu tồn kho không đủ
                        int rowsAffected = cmdUpdateKho.ExecuteNonQuery();
                        if (rowsAffected == 0)
                        {
                            throw new Exception("Không đủ số lượng tồn kho cho sản phẩm: " + maSanPham);
                        }
                    }

                    // 5. Commit giao dịch nếu tất cả thao tác thành công
                    transaction.Commit();
                    MessageBox.Show("Đơn hàng đã được tạo và sản phẩm đã được cập nhật tình trạng và tồn kho thành công.");

                    // Thêm hóa đơn vào bảng HoaDon (nếu cần)
                    ThemHoaDon();
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
                // Đảm bảo đóng kết nối
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
            }


        }




        private void btluu_Click(object sender, EventArgs e)
        {
                addphieumuahangvadanhsachsanpham();
                
        }

        private void làmMớiTrangToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loaddata();
            lockphantu();
            xoatext();
            
        }
    }
 }

