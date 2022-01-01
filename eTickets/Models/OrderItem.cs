using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eTickets.Models
{
    public class OrderItem
    {
        
        //This model represents the individial order item, so the movie is associated with a price and order id

        [Key]
        public int Id { get; set; }

        public int Amount { get; set; }
        public double Price { get; set; }

        //TO get the movie that they've ordered
        public int MovieId { get; set; }
        [ForeignKey("MovieId")]
        public virtual Movie Movie { get; set; }

       
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }
    }
}
