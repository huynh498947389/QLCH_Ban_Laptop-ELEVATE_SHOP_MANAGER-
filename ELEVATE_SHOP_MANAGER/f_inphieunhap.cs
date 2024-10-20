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
   
    public partial class f_inphieunhap : Form
    { SqlConnection cn = ketnoidb.Ketnoidata();
        public f_inphieunhap(String manhap)
        {
            InitializeComponent();
            PrintHoaDon(manhap);
        }

        private void f_inphieunhap_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }
        private void PrintHoaDon(string maPhieu)
        {         
            String manv = " ";
            DateTime ngaynhap = DateTime.Now;

            try
            {
                if (cn.State == ConnectionState.Closed)
                {
                    cn.Open();
                }
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.CommandText = "Select * from DonNhap Where MaDonNhap = @maphieu";
                sqlcmd.Parameters.AddWithValue("@maphieu", maPhieu);
                sqlcmd.Connection = cn;
                SqlDataReader data = sqlcmd.ExecuteReader();
                if (data.Read())
                {

                    manv = data["MaNV"].ToString();
                    ngaynhap = Convert.ToDateTime(data["NgayNhap"]);

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
                string query = "SELECT * FROM ChiTietDonNhap WHERE MaDonNhap = @maPhieu";
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@maPhieu", maPhieu);

                // Thực thi truy vấn và lưu dữ liệu vào DataTable
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dtDonnhap = new DataTable();
                da.Fill(dtDonnhap);

                if (dtDonnhap.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy dữ liệu chi tiết hóa đơn.");
                    return;
                }

                // Đổ dữ liệu vào ReportViewer
                reportViewer1.LocalReport.DataSources.Clear();
                ReportDataSource rds = new ReportDataSource("DataSet1", dtDonnhap);
                reportViewer1.LocalReport.DataSources.Add(rds);

                // Nạp tham số cho báo cáo
                ReportParameter[] parameters = new ReportParameter[]
                {
           
             new ReportParameter("madonnhap",maPhieu), // Dữ liệu thực từ form hoặc DB
             new ReportParameter("ngaynhap",ngaynhap.ToString("dd/MM/yyyy")),         
             new ReportParameter("manv",manv)
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
