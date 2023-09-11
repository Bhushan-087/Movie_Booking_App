using MovieBookingApp.Data.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MovieBookingApp.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Display(Name = "Movie Name")]
        public string Name { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        public double Price { get; set; }
        [Display(Name = "Movie Poster")]
        public byte[] ImageURL { get; set; }
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
        public MovieCategory MovieCategory { get; set; }

        //Relation
        public List<Actor_Movie> Actor_Movies { get; set; }

        ////Cinema
        //public int CinemaId { get; set; }
        //[ForeignKey("CinemaId")]
        public Cinema Cinema { get; set; }

        ////Producer
        //public int ProducerId { get; set; }
        //[ForeignKey("ProducerId")]
        public Producer Producer { get; set; }
    }
}
