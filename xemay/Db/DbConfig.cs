using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xemay.Db
{
    internal class DbConfig
    {

        //lấy chuỗi kết nối
        //k
        private string stringConnection = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"D:\\bài tập c#\\bài tập c# tiếp theo\\xemay\\xemay\\Properties\\DBQLXeMay.mdf\";Integrated Security=True";
        private SqlDataAdapter sqlDataAdapter;
        private SqlCommand sqlCommand;
        private SqlConnection sqlConnection()
        {
            return new SqlConnection(stringConnection);
        }
        public DataTable Table(string query)
        {
            DataTable dt= new DataTable();//khoi tao 1 bang du lieu moi
            using(SqlConnection cnn = sqlConnection())
            {
                cnn.Open();
                sqlDataAdapter = new SqlDataAdapter(query, cnn);
                sqlDataAdapter.Fill(dt);
                cnn.Close();

            }
            return dt;
        }
        public void Excute(string query) 
        {
            using(SqlConnection cnn = sqlConnection())
            {
                cnn.Open();
                sqlCommand= new SqlCommand(query, cnn);
                sqlCommand.ExecuteNonQuery();
                cnn.Close();
            }
        }

    }
}
 