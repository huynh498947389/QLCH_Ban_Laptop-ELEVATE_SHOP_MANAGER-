using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELEVATE_SHOP_MANAGER
{
    public class ketnoidb
    {
        public static SqlConnection Ketnoidata()
        {
            // Chuỗi kết nối đến SQL Server
            string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Elevate_store;Integrated Security=True";
            // Tạo đối tượng kết nối SQL
            SqlConnection connection = new SqlConnection(connectionString);
            // Trả về đối tượng kết nối
            return connection;
        }
    }
}
