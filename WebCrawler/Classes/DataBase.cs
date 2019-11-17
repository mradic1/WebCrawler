using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace WebCrawler
{
    public class DataBase : Form1
    {
        private int id;
        private string numId;
        private string name;
        private int price;
        private string link;
        private string date;
        private string imageLink;
        private int category;

        string connetionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\mradi\Source\Repos\WebCrawler\WebCrawler\Database1.mdf;Integrated Security=True";

        #region Data Types
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
        public string NumId
        {
            get
            {
                return numId;
            }
            set
            {
                numId = value;
            }
        }
        public string ItemName
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public int Price
        {
            get
            {
                return price;
            }
            set
            {
                price = value;
            }
        }

        public string Link
        {
            get
            {
                return link;
            }
            set
            {
                link = value;
            }
        }

        public string Date
        {
            get
            {
                return date;
            }
            set
            {
                date = value;
            }
        }

        public string ImageLink
        {
            get
            {
                return imageLink;
            }
            set
            {
                imageLink = value;
            }
        }

        public int Category
        {
            get
            {
                return category;
            }
            set
            {
                category = value;
            }
        }

        #endregion
        public void pushValues()
        {

            SqlConnection cnn;
            SqlCommand cmd;
            SqlDataAdapter adapter = new SqlDataAdapter();
            String sql = "";
            sql = "INSERT INTO Items VALUES ('" + numId + "', '" + name + "', '" + price + "', '" + link + "','" + date + "','" + imageLink + "','" + category + "'); ";
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
            sql = "DELETE FROM Items WHERE Id='" + rowId + "';";
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
            using (SqlCommand command = new SqlCommand("select * from Items where Id = " + rowNum + "", connection))
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int.TryParse(reader["Id"].ToString(), out id);
                        numId = reader["numId"].ToString();
                        name = reader["name"].ToString();
                        int.TryParse(reader["price"].ToString(), out price);
                        link = reader["link"].ToString();
                        date = reader["date"].ToString();
                        imageLink = reader["imageLink"].ToString();
                        int.TryParse(reader["category"].ToString(), out category);
                    }
                }
                connection.Close();
            }
        }

        public int MaxRowNumber()
        {
            SqlConnection con = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\mradi\Source\Repos\WebCrawler\WebCrawler\Database1.mdf; Integrated Security = True");
            con.Open();
            SqlCommand comm = new SqlCommand("SELECT COUNT(*) FROM Items", con);
            Int32 count = (Int32)comm.ExecuteScalar();
            con.Close();
            return count;
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
