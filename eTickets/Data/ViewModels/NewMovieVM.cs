using eTickets.Data;
using eTickets.Data.BaseRepository;
using eTickets.Data.Enums;
using eTickets.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Models
{
    public class NewMovieVM 
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Movie Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description is required")]
        [Display(Name = "Movie Description")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Price is required")]
        [Display(Name = "Ticket $AUD")]
        public double Price { get; set; }
        [Required(ErrorMessage = "Image is required")]
        [Display(Name = "Movie Poster ")]
        public string ImageURL { get; set; }
        [Required(ErrorMessage = "Start Date is required")]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "End Date is required")]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
        [Required(ErrorMessage = "Movie Category is required")]
        [Display(Name = "Movie Categories")]
        public MovieCategory MovieCategory { get; set; }

        [Required(ErrorMessage = "Watch Time is required")]
        [Display(Name = "Watch Time")]
        public int WatchTime { get; set; }
        [Display(Name = "Actors")]

        [Required(ErrorMessage = "Please Select an actor")]
        public List<int> ActorId { get; set; }
        [Display(Name = "Cinemas")]
        public int CinemaId { get; set; }
        [Display(Name = "Producers")]
        public int ProducerId { get; set; }
    }
}
