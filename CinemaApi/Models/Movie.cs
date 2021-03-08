using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaApi.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name cannot be null or empty")]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required(ErrorMessage = "Language cannot be null or empty")]
        public string Language { get; set; }
        [Required]
        public string Duration { get; set; }
        [Required]
        public DateTime PlayingDate { get; set; }
        [Required]
        public DateTime PlayingTime { get; set; }
        [Required]
        public double TicketPrice { get; set; }
        public double Rating { get; set; }
        [Required]
        public string Genre { get; set; }
        public string TrailerUrl { get; set; }
        public string ImageUrl { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }

        public ICollection<Reservation> Reservations { get; set; }
    }
}
