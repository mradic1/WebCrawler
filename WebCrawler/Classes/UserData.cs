using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace WebCrawler
{
    public class UserData
    {
        private int id;
        private string data;


        string connetionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\mradi\Source\Repos\WebCrawler\WebCrawler\Database1.mdf;Integrated Security=True";
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }
        public string Data
        {
            get
            {
                return data;
            }
            set
            {
                data = value;
            }
        }
        public void pushValues(int id)
        {

            SqlConnection cnn;
            SqlCommand cmd;
            SqlDataAdapter adapter = new SqlDataAdapter();
            String sql = "";
            sql = "INSERT INTO Data VALUES ('" + id + "','" + data + "'); ";
            cnn = new SqlConnection(connetionString);
            cmd = new SqlCommand(sql, cnn);
            cnn.Open();
            adapter.InsertCommand = new SqlCommand(sql, cnn);
            adapter.InsertCommand.ExecuteNonQuery();
            cnn.Close();
        }

        public void deleteValue(int rowId)
        {

            SqlConnection cnn;
            SqlCommand cmd;
            SqlDataAdapter adapter = new SqlDataAdapter();
            String sql = "";
            sql = "DELETE FROM Data WHERE Id='" + rowId + "';";
            cnn = new SqlConnection(connetionString);
            cmd = new SqlCommand(sql, cnn);
            cnn.Open();
            adapter.InsertCommand = new SqlCommand(sql, cnn);
            adapter.InsertCommand.ExecuteNonQuery();
            cnn.Close();
        }

        public void getValues(int rowNum)
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\mradi\Source\Repos\WebCrawler\WebCrawler\Database1.mdf;Integrated Security=True"))
            using (SqlCommand command = new SqlCommand("select * from Data where Id = " + rowNum + "", connection))
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int.TryParse(reader["Id"].ToString(), out id);
                        data = reader["data"].ToString();

                    }
                }
                connection.Close();
            }
        }

        public void Reseed()
        {
            SqlConnection cnn;
            SqlCommand cmd;
            SqlDataAdapter adapter = new SqlDataAdapter();
            String sql = "";
            sql = "DBCC CHECKIDENT(Items, RESEED, 0)";
            cnn = new SqlConnection(connetionString);
            cmd = new SqlCommand(sql, cnn);
            cnn.Open();
            adapter.InsertCommand = new SqlCommand(sql, cnn);
            adapter.InsertCommand.ExecuteNonQuery();
            cnn.Close();
        }

    }
}
