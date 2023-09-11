using CinemaBooking.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieBookingApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MovieBookingApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMovieRepo _movieRepo;

        public HomeController(IMovieRepo movieRepo)
        {
            this._movieRepo = movieRepo;
        }

        [HttpGet]
        public IActionResult GetAllMovies()
        {
            return this.Ok(this._movieRepo.GetAllMovies());
        }
        public IActionResult Index()
        {
            return View(this._movieRepo.GetAllMovies());
        }

        [HttpGet]
        public IActionResult AddMovie()
        {
            return View("AddMovie");
        }
        [HttpPost]
        public IActionResult AddMovie(Movie_Temp movie)
        {
            //HttpPostedFileBase file = Request.Files["ImageData"];
            this._movieRepo.AddMovie(movie);
            return View("AddMovie");
        }
        [HttpPost]
        public IActionResult Add()
        {
            //var val = movie;
            return View("AddMovie");
        }
    }
}
