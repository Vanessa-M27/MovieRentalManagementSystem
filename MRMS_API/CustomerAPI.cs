using MRMS_Model;

namespace MRMS_API
{
    public class CustomerAPI
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime DateVerified { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime DateUpdated { get; set; }
        public CustomerProfile Profile { get; set; }
        public int Status { get; set; }

    }
}
