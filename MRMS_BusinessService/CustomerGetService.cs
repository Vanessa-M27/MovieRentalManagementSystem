using MRMS_Data;
using MRMS_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRMS_BusinessService
{
    public class CustomerGetService
    {
        private SqlCustomerdbData customerData;

        public CustomerGetService()
        {
            customerData = new SqlCustomerdbData();
        }

        public List<Customer> GetAllCustomers()
        {
            return customerData.GetCustomers();
        }

       

        public Customer GetCustomer(string username, string password)
        {
            foreach (var customer in GetAllCustomers())
            {
                if (customer.Username == username && customer.Password == password)
                {
                    return customer;
                }
            }

            return null;
        }

        public int AddCustomer(string Username, string Password)
        {
            return customerData.AddCustomer(Username, Password);
        }
     




    }

}
