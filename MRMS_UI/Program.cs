using MRMS_BusinessService;
using MRMS_Data;
using MRMS_Model;


namespace MRMS_UI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //this block of code is for calling the data that is in the sqldatabase
            /* SqlCustomerdbData getServices = new SqlCustomerdbData();

             var customers = getServices.GetCustomers();

             foreach (var item in customers)
             {
                 Console.WriteLine($"Username: {item.Username}");
                 Console.WriteLine($"Password: {item.Password}");
                 Console.WriteLine($"Status: {item.Status}");
                 Console.WriteLine();
             }  */

            //when a customer input their data, the data that the customer provided will directly go to the sqldatabase
            MovieService movieService = new MovieService();
            CustomerGetService customerService = new CustomerGetService();
            MovieRentalUI ui = new MovieRentalUI(movieService, customerService);

            ui.Run();
        }

        public class MovieRentalUI
        {
            private MovieService movieService;
            private CustomerGetService customerService;

            public MovieRentalUI(MovieService movieService, CustomerGetService customerService)
            {
                this.movieService = movieService;
                this.customerService = customerService;
            }

            public void Run()
            {
                Customer currentCustomer = null;

                Console.WriteLine("Welcome to Movie Rental System!");
                Console.WriteLine("----------------------------------");

                while (true)
                {
                    if (currentCustomer == null)
                    {
                        Console.WriteLine("\n1. Register");
                        Console.WriteLine("2. Login");
                        Console.WriteLine("3. Exit");
                        Console.Write("Enter your choice: ");
                        string choice = Console.ReadLine();
                        Console.WriteLine("----------------------------------");
                        switch (choice)
                        {
                            case "1":
                                Console.Write("Enter your username: ");
                                string username = Console.ReadLine();
                                Console.Write("Enter your password: ");
                                string password = Console.ReadLine();
                                Console.Write("Enter your email address: ");
                                string email = Console.ReadLine();
                                Customer newCustomer = new Customer
                                {
                                    Username = username,
                                    Password = password,
                                    Profile = new CustomerProfile { EmailAddress = email, ProfileName = username },
                                    DateUpdated = DateTime.Now,
                                    DateVerified = DateTime.Now.AddDays(1),
                                    Status = 1
                                };
                                int result = customerService.AddCustomer(newCustomer);
                                if (result > 0)
                                {
                                    Console.WriteLine("----------------------------------");
                                    Console.WriteLine("Registration successful!");
                                }
                                else
                                {
                                    Console.WriteLine("----------------------------------");
                                    Console.WriteLine("Registration failed. Please try again.");
                                }
                                break;
                            case "2":
                                Console.Write("Enter your username: ");
                                username = Console.ReadLine();
                                Console.Write("Enter your password: ");
                                password = Console.ReadLine();
                                currentCustomer = customerService.GetCustomer(username, password);
                                if (currentCustomer != null)
                                {
                                    Console.WriteLine("----------------------------------");
                                    Console.WriteLine("Login successful!");
                                }
                                else
                                {
                                    Console.WriteLine("----------------------------------");
                                    Console.WriteLine("Invalid username or password.");
                                }
                                break;
                            case "3":
                                Console.WriteLine("----------------------------------");
                                Console.WriteLine("Thank you for using Movie Rental System!");
                                return;
                            default:
                                Console.WriteLine("----------------------------------");
                                Console.WriteLine("Invalid choice. Please try again.");
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"\nHello, {currentCustomer.Username}!");

                        Console.WriteLine("\nAvailable Movies:");
                        DisplayMovies(movieService.GetMovies());
                        Console.WriteLine("--------------------------------------------------------------------------------------");
                        Console.Write("Enter the Code of the movie you want to rent (0 to exit): ");
                        int movieCode = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("--------------------------------------------------------------------------------------");
                        if (movieCode == 0)
                        {
                            currentCustomer = null;
                            Console.WriteLine("----------------------------------");
                            Console.WriteLine("You have been logged out.");
                        }
                        else
                        {
                            string message = movieService.RentMovie(movieCode, currentCustomer);
                            Console.WriteLine(message);
                        }
                    }
                }
            }

            private void DisplayMovies(List<Movie> movies)
            {
                foreach (var movie in movies)
                {
                    Console.WriteLine($"Code: {movie.Code}, Title: {movie.Title}, Genre: {movie.Genre}, Price: {movie.Price}");
                }
            } 
        }




        /* MovieService movieService = new MovieService();
        CustomerGetService customerService = new CustomerGetService();
        MovieRentalUI ui = new MovieRentalUI(movieService, customerService);

        ui.Run();
        public class MovieRentalUI
{
    private MovieService movieService;
    private CustomerGetService customerService;

    public MovieRentalUI(MovieService movieService, CustomerGetService customerService)
    {
        this.movieService = movieService;
        this.customerService = customerService;
    }

    public void Run()
    {
        Customer currentCustomer = null;

        Console.WriteLine("Welcome to Movie Rental System!");
        Console.WriteLine("----------------------------------");

        while (true)
        {
            if (currentCustomer == null)
            {
                Console.WriteLine("\n1. Register");
                Console.WriteLine("2. Login");
                Console.WriteLine("3. Exit");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();
                Console.WriteLine("----------------------------------");
                switch (choice)
                {
                    case "1":
                        Console.Write("Enter your username: ");
                        string username = Console.ReadLine();
                        Console.Write("Enter your password: ");
                        string password = Console.ReadLine();
                        Console.Write("Enter your email address: ");
                        string email = Console.ReadLine();
                        Customer newCustomer = new Customer
                        {
                            Username = username,
                            Password = password,
                            Profile = new CustomerProfile { EmailAddress = email, ProfileName = username },
                            DateUpdated = DateTime.Now,
                            DateVerified = DateTime.Now.AddDays(1),
                            Status = 1
                        };
                        int result = customerService.AddCustomer(newCustomer);
                        if (result > 0)
                        {
                            Console.WriteLine("----------------------------------");
                            Console.WriteLine("Registration successful!");
                        }
                        else
                        {
                            Console.WriteLine("----------------------------------");
                            Console.WriteLine("Registration failed. Please try again.");
                        }
                        break;
                    case "2":
                        Console.Write("Enter your username: ");
                        username = Console.ReadLine();
                        Console.Write("Enter your password: ");
                        password = Console.ReadLine();
                        currentCustomer = customerService.GetCustomer(username, password);
                        if (currentCustomer != null)
                        {
                            Console.WriteLine("----------------------------------");
                            Console.WriteLine("Login successful!");
                        }
                        else
                        {
                            Console.WriteLine("----------------------------------");
                            Console.WriteLine("Invalid username or password.");
                        }
                        break;
                    case "3":
                        Console.WriteLine("----------------------------------");
                        Console.WriteLine("Thank you for using Movie Rental System!");
                        return;
                    default:
                        Console.WriteLine("----------------------------------");
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
            else
            {
                Console.WriteLine($"\nHello, {currentCustomer.Username}!");

                Console.WriteLine("\nAvailable Movies:");
                DisplayMovies(movieService.GetMovies());
                Console.WriteLine("--------------------------------------------------------------------------------------");
                Console.Write("Enter the Code of the movie you want to rent (0 to exit): ");
                int movieCode = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("--------------------------------------------------------------------------------------");
                if (movieCode == 0)
                {
                    currentCustomer = null;
                    Console.WriteLine("----------------------------------");
                    Console.WriteLine("You have been logged out.");
                }
                else
                {
                    string message = movieService.RentMovie(movieCode, currentCustomer);
                    Console.WriteLine(message);
                }
            }
        }
    }

    private void DisplayMovies(List<Movie> movies)
    {
        foreach (var movie in movies)
        {
            Console.WriteLine($"Code: {movie.Code}, Title: {movie.Title}, Genre: {movie.Genre}, Price: {movie.Price}");
        }
    } */
}
    }


