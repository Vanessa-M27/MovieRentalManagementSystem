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
                string username = reader["username"].ToString();
                string password = reader["password"].ToString();
                int status = Convert.ToInt32(reader["status"]);

                Customer readCustomer = new Customer
                {
                    Username = username,
                    Password = password,
                    Status = status
                };

                customers.Add(readCustomer);
            }

            sqlConnection.Close();

            return customers;
        }

        public int AddCustomer(string username, string password)
        {
            int success;

            string insertStatement = "INSERT INTO Customer (username, password, status) VALUES (@Username, @Password, @Status)";
            SqlCommand insertCommand = new SqlCommand(insertStatement, sqlConnection);

            insertCommand.Parameters.AddWithValue("@Username", username);
            insertCommand.Parameters.AddWithValue("@Password", password);
            insertCommand.Parameters.AddWithValue("@Status", 1);

            sqlConnection.Open();
            success = insertCommand.ExecuteNonQuery();
            sqlConnection.Close();

            return success;
        }

        public int UpdateCustomer(string username, string password)
        {
            int success;

            string updateStatement = "UPDATE customer SET password = @Password WHERE username = @Username";
            SqlCommand updateCommand = new SqlCommand(updateStatement, sqlConnection);

            updateCommand.Parameters.AddWithValue("@Password", password);
            updateCommand.Parameters.AddWithValue("@Username", username);

            sqlConnection.Open();
            success = updateCommand.ExecuteNonQuery();
            sqlConnection.Close();

            return success;
        }

        public int DeleteCustomer(string username)
        {
            int success;

            string deleteStatement = "DELETE FROM customer WHERE username = @Username";
            SqlCommand deleteCommand = new SqlCommand(deleteStatement, sqlConnection);

            deleteCommand.Parameters.AddWithValue("@Username", username);

            sqlConnection.Open();
            success = deleteCommand.ExecuteNonQuery();
            sqlConnection.Close();

            return success;
        }


    }
}


