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
    public partial class f_baocaohangton : Form
    {
        SqlConnection cn = ketnoidb.Ketnoidata();
        public f_baocaohangton()
        {
            InitializeComponent();
        }

        private void f_baocaohangton_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
            DateTime ngaynhap = DateTime.Now;
            try
            {
                if (cn.State == ConnectionState.Closed)
                {
                    cn.Open();
                }

                // Truy vấn lấy dữ liệu hóa đơn dựa trên Mã Phiếu
                string query = "SELECT * FROM KhoHang";
                SqlCommand cmd = new SqlCommand(query, cn);
                // Thực thi truy vấn và lưu dữ liệu vào DataTable
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dtDonnhap = new DataTable();
                da.Fill(dtDonnhap);

                if (dtDonnhap.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy dữ liệu.");
                    return;
                }
                // Đổ dữ liệu vào ReportViewer
                reportViewer1.LocalReport.DataSources.Clear();
                ReportDataSource rds = new ReportDataSource("DataSetbaocaokho", dtDonnhap);
                reportViewer1.LocalReport.DataSources.Add(rds);

                // Nạp tham số cho báo cáo
                ReportParameter[] parameters = new ReportParameter[]
                {
                 new ReportParameter("ngayxuatbaocao",ngaynhap.ToString("dd/MM/yyyy")),
                };
                reportViewer1.LocalReport.SetParameters(parameters);

                // Hiển thị báo cáo
                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi in báo cáo " + ex.Message);
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
