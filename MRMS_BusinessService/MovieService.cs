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

        public string RentMovie(int movieCode)
        {
            List<Movie> movies = movieData.GetMovies();
            Movie movieToRent = movies.Find(movie => movie.Code == movieCode);

            if (movieToRent != null)
            {
                if (!movieToRent.IsRented)
                {
                    double rentalFee = movieToRent.Price;
                    movieToRent.IsRented = true;

                    // Update the movie's rented status in the database
                    int rowsAffected = movieData.UpdateMovie(movieToRent);

                    if (rowsAffected > 0)
                    {
                        return $"Movie '{movieToRent.Title}' is successfully rented. Enjoy your movie! Rental Fee: Php{rentalFee}";
                    }
                    else
                    {
                        return "Movie rental failed due to a database update error.";
                    }
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
