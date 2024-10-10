using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MRMS_Model;

namespace MRMS_Data
{
    public class SqlCustomerdbData
    {
        string connectionString = "Data Source=LAPTOP-LGBEJ5GN\\SQLEXPRESS02; Initial Catalog=MoiveRentalManagmentSystem; Integrated Security=True;";
      //  string connection = "Server = tcp:40.81.22.197,1433;Database=MoiveRentalManagmentSystem; User Id=sa; Password=VV1234v;";
        SqlConnection sqlConnection;


        public SqlCustomerdbData()
        {
            sqlConnection = new SqlConnection(connectionString);
        }

        public List<Customer> GetCustomers()
        {
            string selectStatement = "SELECT * FROM Customer";
            SqlCommand selectCommand = new SqlCommand(selectStatement, sqlConnection);
            sqlConnection.Open();
            List<Customer> customers = new List<Customer>();

            SqlDataReader reader = selectCommand.ExecuteReader();

            while (reader.Read())
            {
                string username = reader["Username"].ToString();
                string password = reader["Password"].ToString();
               

                Customer readCustomer = new Customer
                {
                    Username = username,
                    Password = password,
                    
                };

                customers.Add(readCustomer);
            }

            sqlConnection.Close();

            return customers;
        }

        public int AddCustomer(string username, string password)
        {
            int success;

            string insertStatement = "INSERT INTO Customer (Username, Password) VALUES (@Username, @Password)";
            SqlCommand insertCommand = new SqlCommand(insertStatement, sqlConnection);

            insertCommand.Parameters.AddWithValue("@Username", username);
            insertCommand.Parameters.AddWithValue("@Password", password);
           

            sqlConnection.Open();
            success = insertCommand.ExecuteNonQuery();
            sqlConnection.Close();

            return success;
        }

     



    }
}


