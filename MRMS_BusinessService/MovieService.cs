using MRMS_Data;
using MRMS_Model;
using System.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace MRMS_BusinessService
{
    public class MovieService
    {
        private MovieData movieData;

        public MovieService()
        {
            movieData = new MovieData();
        }

        public string RentMovie(int movieCode, Customer currentCustomer)
        {
            List<Movie> movies = movieData.GetMovies();

            Movie movieToRent = movies.Find(movie => movie.Code == movieCode);

            if (movieToRent != null)
            {
                if (!movieToRent.IsRented)
                {
                    double rentalFee = movieToRent.Price;
                    movieToRent.IsRented = true;
                    // Update the movie's rented status in the database here
                    // movieData.UpdateMovie(movieToRent);

                    return $"Movie '{movieToRent.Title}' has been rented by {currentCustomer.Username}. Enjoy your movie! Rental Fee: Php{rentalFee}";
                }
                else
                {
                    return $"Movie '{movieToRent.Title}' is already rented.";
                }
            }
            else
            {
                return $"Movie with code {movieCode} does not exist.";
            }
        }

        public List<Movie> GetMovies()
        {
            return movieData.GetMovies();
        }

        public int AddMovie(int code, string title, string genre, string year, double price)
        {
            return movieData.AddMovie(code, title, genre, year, price);

        }

        public int RemoveMovie(string title)
        {
            return movieData.RemoveMovie(title);
        }

    }

}
