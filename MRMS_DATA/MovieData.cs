
using MRMS_Model;
using System.Data.SqlClient;


namespace MRMS_Data
{
   public class MovieData
      {

       /* private string connectionString = "Data Source=LAPTOP-LGBEJ5GN\\SQLEXPRESS; Initial Catalog=MoiveRentalManagmentSystem; Integrated Security=True;";
        private SqlConnection sqlConnection;

        public MovieData()
        {
            sqlConnection = new SqlConnection(connectionString);
        }

        public List<Movie> GetMovies()
        {
            string selectStatement = "SELECT * FROM Movies";
            SqlCommand selectCommand = new SqlCommand(selectStatement, sqlConnection);
            sqlConnection.Open();
            List<Movie> movies = new List<Movie>();

            SqlDataReader reader = selectCommand.ExecuteReader();

            while (reader.Read())
            {
                Movie movie = new Movie
                {
                    Code = Convert.ToInt32(reader["Code"]),
                    Title = reader["Title"].ToString(),
                    Genre = reader["Genre"].ToString(),
                    Year = Convert.ToInt32(reader["Year"]),
                    Price = Convert.ToDouble(reader["Price"]),
                    IsRented = Convert.ToBoolean(reader["IsRented"])
                };

                movies.Add(movie);
            }

            sqlConnection.Close();

            return movies;
        } */







          private List<Movie> movies;

          public MovieData()
          {
              movies = new List<Movie>();
              MovieInfo();
          }

          private void MovieInfo()
          {
              movies.Add(new Movie
              {
                  Code = 101,
                  Title = "Parasyte : The Grey",
                  Genre = "Sci-fi Action Horror",
                  Price = 480.00
              });
              movies.Add(new Movie
              {
                  Code = 102,
                  Title = "Parasyte : The Maxim",
                  Genre = "Sci-fi Action Horror",
                  Price = 450.00
              });
              movies.Add(new Movie
              {
                  Code = 103,
                  Title = "Bar Boys",
                  Genre = "Drama",
                  Price = 400.00
              });
              movies.Add(new Movie
              {
                  Code = 104,
                  Title = "Barbie",
                  Genre = "Fantasy",
                  Price = 380.00
              });
              movies.Add(new Movie
              {
                  Code = 105,
                  Title = "One Piece: The Movie",
                  Genre = "Adventure",
                  Price = 350.00
              });
          }

          public List<Movie> GetMovies()
          {
              return movies;
          }  



    }
}
