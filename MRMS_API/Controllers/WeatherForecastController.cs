using Microsoft.AspNetCore.Mvc;
using MRMS_Model;
using MRMS_Data;
using MRMS_BusinessService;
using MRMS_UI;
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
        public ActionResult<IEnumerable<Movie>> GetMovies()
        {
            var movies = _movieService.GetMovies();
            if (movies == null || !movies.Any())
            {
                return NotFound("No movies found.");
            }
            return Ok(movies);
        }

        [HttpPost("Login")]
        public ActionResult<Customer> Login([FromBody] Customer customer)
        {
            var user = _customerGetService.GetCustomer(customer.Username, customer.Password);
            if (user != null)
            {
                return Ok(user);
            }
            return Unauthorized();
        }

        [HttpPost("AddMovie")]
        public ActionResult<int> AddMovie([FromBody] Movie movie)
        {
            if (movie == null)
            {
                return BadRequest("Movie data is null.");
            }

            var result = _movieService.AddMovie(movie.Code, movie.Title, movie.Genre, movie.Year, movie.Price);
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


    }
 } 







