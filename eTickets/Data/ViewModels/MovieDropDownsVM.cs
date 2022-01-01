using eTickets.Models;
using System.Collections.Generic;

namespace eTickets.Data.ViewModels
{
    public class MovieDropDownsVM
    {

        public MovieDropDownsVM()
        {
            Producers = new List<Producer>();
            Cinemas = new List<Cinema>();
            Actors = new List<Actor>();
        }
        // Pass data from controller to view
        public List<Producer> Producers{ get; set; }
        public List<Cinema> Cinemas { get; set; }

        public List<Actor> Actors { get; set; }

    }
}
