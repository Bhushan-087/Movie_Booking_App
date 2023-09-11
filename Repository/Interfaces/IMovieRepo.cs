using MovieBookingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaBooking.Repository.Interfaces
{
    public interface IMovieRepo
    {
        IEnumerable<Movie> GetAllMovies();
        void AddMovie(Movie_Temp movie);
    }
}
