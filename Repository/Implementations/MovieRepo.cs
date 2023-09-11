using CinemaBooking.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MovieBookingApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaBooking.Repository.Implementations
{
    public class MovieRepo : IMovieRepo
    {
        public MovieRepo(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void AddMovie(Movie_Temp movie)
        {
            var val = movie.Photo;
            byte[] bytes = null;
            if (val != null)
            {
                long fileSize = val.Length;
                BinaryReader reader = new BinaryReader(val.OpenReadStream());
                bytes = reader.ReadBytes((int)val.Length);
            }

            //byte[] bytes = new byte[10000];

                //if (fileSize > 0)
                //{
                //    using (var stream = new MemoryStream())
                //    {
                //        val.CopyTo(stream);
                //        bytes = stream.ToArray();
                //    }
                //}
            using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("AppDbContext")))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("spMasterinsertupdatedelete", connection);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = 1;
                cmd.Parameters.Add("@StatementType", SqlDbType.VarChar, 50).Value = "Insert";
                cmd.Parameters.Add("@Name", SqlDbType.VarChar, 50).Value = movie.Name;
                cmd.Parameters.Add("@Description", SqlDbType.VarChar, 50).Value = movie.Description;
                cmd.Parameters.Add("@StartDate", SqlDbType.Date, 50).Value = movie.StartDate;
                cmd.Parameters.Add("@EndDate", SqlDbType.Date, 50).Value = movie.EndDate;
                if (bytes !=null) {
                    cmd.Parameters.Add("@ImageURL", SqlDbType.VarBinary, int.MaxValue).Value = bytes;
                }
                else
                {
                    cmd.Parameters.Add("@ImageURL", SqlDbType.VarBinary, int.MaxValue).Value = DBNull.Value;
                }
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public IEnumerable<Movie> GetAllMovies()
        {

            List<Movie> movies = new List<Movie>();
            using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("AppDbContext")))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SelectAllMovies", connection);

                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    Movie movie = new Movie
                    {
                        Name = (string)sdr["Name"],
                        Id = Convert.ToInt32(sdr["ID"]),
                        //Price = Convert.ToInt32(sdr[""]),
                        Description = (string)sdr["Description"],
                        StartDate = sdr["StartDate"] == DBNull.Value ? Convert.ToDateTime("11/11/2023") : Convert.ToDateTime(sdr["StartDate"]),
                        EndDate = sdr["EndDate"] == DBNull.Value ? Convert.ToDateTime("11/11/2023") : Convert.ToDateTime(sdr["EndDate"]),
                        ImageURL = sdr["ImageURL"] != DBNull.Value ? (byte[])sdr["ImageURL"] : null
                    };
                    movies.Add(movie);
                }
            }
            //Cinema cinema = new Cinema
            //{
            //    Id = 1,
            //    Logo = "Abc"
            //};
            //List<Cinema> cinemaList = new List<Cinema>();
            //cinemaList.Add(cinema);
            return movies;
        }
    }
}
