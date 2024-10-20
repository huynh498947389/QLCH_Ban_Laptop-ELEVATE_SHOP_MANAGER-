using Microsoft.Reporting.WinForms;
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
    public partial class uc_thongke : UserControl
    {
        SqlConnection cn = ketnoidb.Ketnoidata();
        public String gname, gquyen;
        public uc_thongke(String name, String quyen)
        {
            InitializeComponent(); 
            this.gname = name;
            this.gquyen = quyen;
            if (this.gquyen.Trim() == "Technician")
            {
                inPhiếuToolStripMenuItem.Enabled = false;
            }
            if (this.gquyen.Trim() == "Sale")
            {
                tạoPhiếuToolStripMenuItem.Enabled=false;
            }
        }

        private void tạoPhiếuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            f_baocaohangton baocao = new f_baocaohangton(); 
            baocao.ShowDialog();
        }

        private void uc_thongke_Load(object sender, EventArgs e)
        {
            reportViewer1.LocalReport.DataSources.Clear();  // Xóa tất cả các nguồn dữ liệu
            reportViewer1.RefreshReport();
            groupBox1.Visible = false;
            txtbatdau.Value = DateTime.Now;
            txtketthuc.Value = DateTime.Now;
        }

        private void làmMớiTrangToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void inPhiếuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBox1 .Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime ngayBatDau = txtbatdau.Value.Date;  // Chỉ lấy phần ngày, loại bỏ giờ
            DateTime ngayKetThuc = txtketthuc.Value.Date;  // Chỉ lấy phần ngày, loại bỏ giờ

            try
            {
                if (cn.State == ConnectionState.Closed)
                {
                    cn.Open();
                }

                // Truy vấn lấy dữ liệu hóa đơn dựa trên khoảng thời gian từ ngày - đến ngày
                string query = "SELECT * FROM HoaDon WHERE CAST(Ngaytt AS DATE) BETWEEN @NgayBatDau AND @NgayKetThuc";
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@NgayBatDau", ngayBatDau);
                cmd.Parameters.AddWithValue("@NgayKetThuc", ngayKetThuc);

                // Thực thi truy vấn và lưu dữ liệu vào DataTable
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dtHoaDon = new DataTable();
                da.Fill(dtHoaDon);

                if (dtHoaDon.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy dữ liệu trong khoảng thời gian đã chọn.");
                    return;
                }

                // Tính tổng cột Số Tiền Cần Thanh Toán
                decimal tongSoTien = dtHoaDon.AsEnumerable()
                                             .Sum(row => row.Field<decimal>("SoTienCanThanhToan"));

                // Đổ dữ liệu vào ReportViewer
                reportViewer1.LocalReport.DataSources.Clear();
                ReportDataSource rds = new ReportDataSource("DataSethoadon", dtHoaDon);
                reportViewer1.LocalReport.DataSources.Add(rds);

                // Nạp tham số cho báo cáo
                ReportParameter[] parameters = new ReportParameter[]
                {
        new ReportParameter("ngayxuatbaocao", DateTime.Now.ToString("dd/MM/yyyy")),
        new ReportParameter("ngaybatdau", ngayBatDau.ToString("dd/MM/yyyy")),
        new ReportParameter("ngayketthuc", ngayKetThuc.ToString("dd/MM/yyyy")),
        new ReportParameter("tongtien", tongSoTien.ToString("N0"))  // Định dạng số có dấu phân cách
                };
                reportViewer1.LocalReport.SetParameters(parameters);

                // Hiển thị báo cáo
                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi in báo cáo: " + ex.Message);
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
            }



        }

        private void làmMớiTrangToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            reportViewer1.LocalReport.DataSources.Clear();  // Xóa tất cả các nguồn dữ liệu
            reportViewer1.RefreshReport();
            groupBox1.Visible = false;
            txtbatdau.Value = DateTime.Now;
            txtketthuc.Value = DateTime.Now;
        }
    }
}
