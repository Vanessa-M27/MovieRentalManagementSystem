using Microsoft.AspNetCore.Mvc;
using MRMS_Model;
using MRMS_Data;
using MRMS_BusinessService;
using MRMS_UI;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace MRMS_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OwnerAPIController : ControllerBase
    {
        private readonly CustomerGetService _customerGetService;
        private readonly MovieService _movieService;
        private readonly Email _email;

        public OwnerAPIController(CustomerGetService customerGetService, MovieService movieService, Email email)
        {
            _customerGetService = customerGetService;
            _movieService = movieService;
            _email = email;

        }

        [HttpGet("Customers")]
        public IEnumerable<CustomerAPI> GetAllCustomers()
        {
            var custo = _customerGetService.GetAllCustomers();
            List<CustomerAPI> cus = new List<CustomerAPI>();
            foreach (var cust in custo)
            {

                cus.Add(new CustomerAPI { Username = cust.Username, Password = cust.Password });
            }
            return cus;

        }

        [HttpGet("Movies")]
        public IEnumerable<MovieAPI> GetAllMovies()
        {
            var movies = _movieService.GetMovies();
            List<MovieAPI> mov = new List<MovieAPI>();

            foreach (var movi in movies)
            {
                mov.Add(new MovieAPI { Code = movi.Code, Genre = movi.Genre, Title = movi.Title, Year = movi.Year, Price = movi.Price, IsRented = movi.IsRented });
            }
            return mov;
        }

        [HttpPost("Login")]
        public ActionResult<Customer> Login(CustomerAPI customer)
        {
            var user = _customerGetService.GetCustomer(customer.Username, customer.Password);
            if (user != null)
            {
                return Ok("Successfuly Log in");
            }
            return Unauthorized();
        }

        [HttpPost("AddMovie")]
        public ActionResult<int> AddMovie(MovieAPI newMovie)
        {
            if (newMovie == null)
            {
                return BadRequest("Movie data is null");
            }

            var result = _movieService.AddMovie(newMovie.Code, newMovie.Title, newMovie.Genre, newMovie.Year, newMovie.Price, newMovie.IsRented);
            return Ok("Successfully Added");
        }

        [HttpPost("AddCustomer")]
        public ActionResult<string> AddCustomer(CustomerAPI newCustomer)
        {
            if (newCustomer == null)
            {
                return BadRequest("Customer data is null");
            }

            var result = _customerGetService.AddCustomer(newCustomer.Username, newCustomer.Password);

            if (result > 0) // Customer added successfully
            {
                // Call the email service to send the notification
                try
                {
                    _email.SendCustomerWelcomeEmail(newCustomer.Username); 
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to send email: {ex.Message}");
                    return Ok("Customer added, but email notification failed.");
                }

                return Ok("Customer successfully added, and email notification sent.");
            }

          
            return StatusCode(500, "Error adding customer.");
        }


        [HttpPatch("RentMovie")]
        public ActionResult<string> RentMovie(int MovieCode, string customerEmail)
        {
            if (MovieCode == 0)
            {
                return BadRequest("Invalid rental request.");
            }

            var result = _movieService.RentMovie(MovieCode);

            if (result.StartsWith("Movie")) // Movie rented successfully
            {
                // Send notification email to the customer
                try
                {
                    _email.SendRentalNotification(customerEmail, MovieCode); 
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to send email: {ex.Message}");
                    return Ok("Movie rented, but email notification failed.");
                }

                return Ok("Movie successfully rented, and email notification sent.");
            }
            else if (result.Contains("already rented"))
            {
                return Conflict(result); 
            }
            else if (result.Contains("does not exist"))
            {
                return NotFound(result); 
            }
            else
            {
                return BadRequest("Movie rental failed.");
            }
        }


        [HttpDelete("RemoveMovie")]
        public ActionResult<int> RemoveMovie(string title)
        {
            var result = _movieService.RemoveMovie(title);
            return Ok(result);
        }

        
  
    
    
    }


    
}
