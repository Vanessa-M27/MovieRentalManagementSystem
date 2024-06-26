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
        private readonly CustomerGetService _customergetservice;
        private readonly MovieService _movieservice;

        public OwnerAPIController(CustomerGetService customergetservice, MovieService movieservice)
        {
            _customergetservice = customergetservice;
            _movieservice = movieservice;
        }

        [HttpGet("Customers")]
        public IEnumerable<Customer> GetAllCustomers()
        {
            return _customergetservice.GetAllCustomers();
        }

        [HttpGet("MOVIES")]
        public IEnumerable<Movie> GetMovies()
        {
            return _movieservice.GetMovies();
        }

        [HttpPost("login")]
        public ActionResult<CustomerProfile> Login([FromBody] CustomerProfile customerprofile)
        {
            var user = _customergetservice.GetCustomer(customerprofile.ProfileName, customerprofile.EmailAddress);
            if (user != null)
            {
                return Ok(user);
            }
            return Unauthorized();
        }

        [HttpPost("InsertMovies")]
        public ActionResult<int> AddMovie([FromBody] Movie movie)
        {
            var result = _movieservice.GetMovies();
            return Ok(result);
        }

        
    }





}

