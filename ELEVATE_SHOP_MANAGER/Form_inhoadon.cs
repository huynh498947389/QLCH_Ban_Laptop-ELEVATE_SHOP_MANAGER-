using Microsoft.Reporting.WinForms;
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

namespace ELEVATE_SHOP_MANAGER
{
    public partial class Form_inhoadon : Form
    {
        SqlConnection cn = ketnoidb.Ketnoidata();
        public Form_inhoadon(String maphieubanhang)
        {
            InitializeComponent();
            PrintHoaDon(maphieubanhang);
        }

        private void Form_inhoadon_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
            
        }
        private void PrintHoaDon(string maPhieu)
        {
            String tenkh = " ";
            String sdtkh = " ";
            String manv = " ";
            DateTime ngaythanhtoan = DateTime.Now;
            String tongtien = "";

            try
            {
                if (cn.State == ConnectionState.Closed)
                {
                    cn.Open();
                }
               SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.CommandText = "Select * from PhieuBanHang Where MaPhieu = @maphieu";
                sqlcmd.Parameters.AddWithValue("@maphieu", maPhieu);
                sqlcmd.Connection = cn;
                SqlDataReader data = sqlcmd.ExecuteReader();
                if (data.Read())
                {

                    manv = data["MaNV"].ToString();
                    ngaythanhtoan = Convert.ToDateTime(data["NgayMuaHang"]);

                }
                cn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi in hóa đơn: " + ex.Message);
            }
            try
            {
                if (cn.State == ConnectionState.Closed)
                {
                    cn.Open();
                }
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.CommandText = "Select * from HoaDon Where MaPhieuBanHang = @maphieu";
                sqlcmd.Parameters.AddWithValue("@maphieu", maPhieu);
                sqlcmd.Connection = cn;
                SqlDataReader data = sqlcmd.ExecuteReader();
                if (data.Read())
                {

                    tenkh = data["TenKH"].ToString();
                    sdtkh = data["SDT_KH"].ToString();
                    tongtien = data["SoTienCanThanhToan"].ToString();
                    

                }
                cn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi in hóa đơn: " + ex.Message);
            }

            try
            {
                if (cn.State == ConnectionState.Closed)
                {
                    cn.Open();
                }

                // Truy vấn lấy dữ liệu hóa đơn dựa trên Mã Phiếu
                string query = "SELECT * FROM ChiTietPhieuBanHang WHERE MaPhieu = @maPhieu";
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@maPhieu", maPhieu);

                // Thực thi truy vấn và lưu dữ liệu vào DataTable
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dtHoaDon = new DataTable();
                da.Fill(dtHoaDon);

                if (dtHoaDon.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy dữ liệu chi tiết hóa đơn.");
                    return;
                }

                // Đổ dữ liệu vào ReportViewer
                reportViewer1.LocalReport.DataSources.Clear();
                ReportDataSource rds = new ReportDataSource("DataSethoadon", dtHoaDon);
                reportViewer1.LocalReport.DataSources.Add(rds);

                // Nạp tham số cho báo cáo
                ReportParameter[] parameters = new ReportParameter[]
                {
            new ReportParameter("tenkhachhang",tenkh), // Dữ liệu thực từ form hoặc DB
            new ReportParameter("sdt",sdtkh), // Dữ liệu thực từ form hoặc DB
            new ReportParameter("ngaythanhtoan",ngaythanhtoan.ToString("dd/MM/yyyy")),
            new ReportParameter("mahoadon", maPhieu),
             new ReportParameter("tongthanhtoan",tongtien),
             new ReportParameter("manhanvien",manv)
                };
                reportViewer1.LocalReport.SetParameters(parameters);

                // Hiển thị báo cáo
                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi in hóa đơn: " + ex.Message);
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
            }
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
