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

namespace ELEVATE_SHOP_MANAGER
{
    public partial class f_inphieudathang : Form
    {
        SqlConnection cn = ketnoidb.Ketnoidata();
        public f_inphieudathang(String maphieu)
        {
            InitializeComponent();
            PrintHoaDon(maphieu);
        }

        private void f_inphieudathang_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }
        private void PrintHoaDon(string maPhieu)
        {
            String maphieudat = " ";
            String manv = " ";
            DateTime ngaydat= DateTime.Now;

            try
            {
                if (cn.State == ConnectionState.Closed)
                {
                    cn.Open();
                }
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.CommandText = "Select * from DatHang Where MaDonDat = @maphieu";
                sqlcmd.Parameters.AddWithValue("@maphieu", maPhieu);
                sqlcmd.Connection = cn;
                SqlDataReader data = sqlcmd.ExecuteReader();
                if (data.Read())
                {

                    manv = data["MaNV"].ToString();
                    ngaydat = Convert.ToDateTime(data["NgayDat"]);
                    maphieudat = data["MaDonDat"].ToString() ;

                }
                cn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi in đơn: " + ex.Message);
            }

            try
            {
                if (cn.State == ConnectionState.Closed)
                {
                    cn.Open();
                }

                // Truy vấn lấy dữ liệu hóa đơn dựa trên Mã Phiếu
                string query = "SELECT * FROM ChiTietDonDatHang WHERE MaDonDat = @maPhieu";
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@maPhieu", maPhieu);

                // Thực thi truy vấn và lưu dữ liệu vào DataTable
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dtHoaDon = new DataTable();
                da.Fill(dtHoaDon);

                if (dtHoaDon.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy dữ liệu chi tiết.");
                    return;
                }

                // Đổ dữ liệu vào ReportViewer
                reportViewer1.LocalReport.DataSources.Clear();
                ReportDataSource rds = new ReportDataSource("DataSetchitietdondat", dtHoaDon);
                reportViewer1.LocalReport.DataSources.Add(rds);

                // Nạp tham số cho báo cáo
                ReportParameter[] parameters = new ReportParameter[]
                {
            new ReportParameter("Maphieudat",maPhieu), // Dữ liệu thực từ form hoặc DB          
            new ReportParameter("Ngaydat",ngaydat.ToString("dd/MM/yyyy")),         
             new ReportParameter("Manhanvien",manv)
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
    }
}
