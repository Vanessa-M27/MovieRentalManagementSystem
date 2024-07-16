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

        public OwnerAPIController(CustomerGetService customerGetService, MovieService movieService)
        {
            _customerGetService = customerGetService;
            _movieService = movieService;
        }

        [HttpGet("Customers")]
        public IEnumerable<Customer> GetAllCustomers()
        {
            return _customerGetService.GetAllCustomers();

           
        }

        [HttpGet("Movies")]
        public IEnumerable<Movie> GetAllMovies()
        {
            return _movieService.GetMovies();
            List <Movie> movies = new List <Movie>();
            foreach (var movi in movies)
            {
                movies.Add(movi);
            }
        }

        [HttpPost("Login")]
        public ActionResult<Customer> Login([FromBody] CustomerAPI customer)
        {
            var user = _customerGetService.GetCustomer(customer.Username, customer.Password);
            if (user != null)
            {
                return Ok("Successfuly Log in");
            }
            return Unauthorized();
        }

        [HttpPost("AddMovie")]
        public ActionResult<int> AddMovie([FromBody] MovieAPI newMovie)
        {
            if (newMovie == null)
            {
                return BadRequest("Movie data is null");
            }

            var result = _movieService.AddMovie(newMovie.Code, newMovie.Title, newMovie.Genre, newMovie.Year, newMovie.Price);
            return Ok(result);
        }

        [HttpPost("RentMovie")]
        public ActionResult<string> RentMovie([FromBody] RentMovieRequest request)
        {
            if (request == null || request.MovieCode == 0 || request.Customer == null)
            {
                return BadRequest("Invalid rental request.");
            }

            var result = _movieService.RentMovie(request.MovieCode, request.Customer);
            return Ok(result);
        }

        [HttpDelete("RemoveMovie")]
        public ActionResult<int> RemoveMovie([FromBody] string title)
        {
            var result = _movieService.RemoveMovie(title);
            return Ok(result);
        }
    }
}
