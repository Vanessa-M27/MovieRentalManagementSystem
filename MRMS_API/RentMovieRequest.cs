using MRMS_Model;

namespace MRMS_API
{
    public class RentMovieRequest
    {
        public Customer Username { get; set; }
        public int MovieCode { get; set; }
    }
}
