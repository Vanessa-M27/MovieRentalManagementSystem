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

        public List<Customer> GetCustomersByStatus(int customerStatus)
        {
            List<Customer> customersByStatus = new List<Customer>();

            foreach (var customer in GetAllCustomers())
            {
                if (customer.Status == customerStatus)
                {
                    customersByStatus.Add(customer);
                }
            }

            return customersByStatus;
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

        public Customer GetCustomer(string username)
        {
            foreach (var customer in GetAllCustomers())
            {
                if (customer.Username == username)
                {
                    return customer;
                }
            }

            return null;
        }

        public int AddCustomer(Customer customer)
        {
            return customerData.AddCustomer(customer.Username, customer.Password);
        }





    }

}
