
using MRMS_Model;
using System.Data.SqlClient;
using System.Diagnostics;


namespace MRMS_Data
{
    public class MovieData
    {

     //   private string connectionString = "Data Source=LAPTOP-LGBEJ5GN\\SQLEXPRESS02; Initial Catalog=MoiveRentalManagmentSystem; Integrated Security=True;";
        string connection = "Server = tcp:40.81.22.197,1433;Database=MoiveRentalManagmentSystem; User Id=sa; Password=VV1234v;";
         SqlConnection sqlConnection;
        
        public MovieData()
        {

            sqlConnection = new SqlConnection(connection);
        }

        public List<Movie> GetMovies()
        {
            string selectStatement = "SELECT Code, Title, Genre, Year, Price, IsRented FROM Movies";
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
                    Year = reader["Year"].ToString(),
                    Price = Convert.ToDouble(reader["Price"]),
                    IsRented = Convert.ToBoolean(reader["IsRented"])
                };

                movies.Add(movie);
            }

            sqlConnection.Close();

            return movies;
        }

        public int AddMovie(int Code, string Title, string Genre, string Year, double Price, bool IsRented)
        {
            int success;

            string insertStatement = "INSERT INTO Movies (Code, Title, Genre,Year, Price, IsRented) VALUES (@Code, @Title, @Genre, @Year, @Price, @IsRented)";
            SqlCommand insertCommand = new SqlCommand(insertStatement, sqlConnection);

            insertCommand.Parameters.AddWithValue("@Code", Code);
            insertCommand.Parameters.AddWithValue("@Title", Title);
            insertCommand.Parameters.AddWithValue("@Genre", Genre);
            insertCommand.Parameters.AddWithValue("@Year", Year);
            insertCommand.Parameters.AddWithValue("Price", Price);
            insertCommand.Parameters.AddWithValue("IsRented", IsRented);
            sqlConnection.Open();
            success = insertCommand.ExecuteNonQuery();
            sqlConnection.Close();

            return success;
        }

        public int UpdateMovie(Movie movie)
        {
            string updateStatement = "UPDATE Movies SET IsRented = @IsRented WHERE Code = @Code";
            using (SqlConnection sqlConnection = new SqlConnection(connection))
            {
                SqlCommand updateCommand = new SqlCommand(updateStatement, sqlConnection);

                updateCommand.Parameters.AddWithValue("@IsRented", movie.IsRented);
                updateCommand.Parameters.AddWithValue("@Code", movie.Code);

                sqlConnection.Open();
                int rowsAffected = updateCommand.ExecuteNonQuery();
                sqlConnection.Close();

                return rowsAffected;
            }
        }
        public int RemoveMovie(string title)
        {
            string remove = "DELETE FROM Movies WHERE Title = @title";
            SqlCommand removemovie = new SqlCommand(remove, sqlConnection);
            removemovie.Parameters.AddWithValue("@title", title);

            sqlConnection.Open();

            int movie = removemovie.ExecuteNonQuery();

            sqlConnection.Close();

            return movie;
        }


    }
}
