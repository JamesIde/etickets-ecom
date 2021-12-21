using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace eTickets.Models
{
    public class Producer
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "About")]
        public string Bio { get; set; }
        [Display(Name = "Profile Picture")]
        public string ProfilePictureURL { get; set; }

        //Relationship
        public List<Movie> Movies { get; set; }
    }
}
