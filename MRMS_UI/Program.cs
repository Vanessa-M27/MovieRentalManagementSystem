using MRMS_BusinessService;
using MRMS_Data;
using MRMS_Model;
using System;


namespace MRMS_UI
{
    internal class Program
    {
        static void Main(string[] args)
        {


            MovieData getServices = new MovieData();

            var movies = getServices.GetMovies();

            foreach (var item in movies)
            {
                Console.WriteLine($"Code: {item.Code}");
                Console.WriteLine($"Title: {item.Title}");
                Console.WriteLine($"Genre: {item.Genre}");
                Console.WriteLine($"Year: {item.Year}");
                Console.WriteLine($"Price: {item.Price}");

                Console.WriteLine();
            }


            SqlCustomerdbData getService = new SqlCustomerdbData();

            var customers = getService.GetCustomers();

            foreach (var item in customers)
            {
                Console.WriteLine($"Username: {item.Username}");
                Console.WriteLine($"Password: {item.Password}");
                Console.WriteLine($"Password: {item.Status}");

                Console.WriteLine();
            }

        }
    }






}
       
    
    
    
 

    


