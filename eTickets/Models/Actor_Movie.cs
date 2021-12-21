namespace eTickets.Models
{
    public class Actor_Movie
    {
       // We need this to join the actor and movies table
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public Actor Actor { get; set; }
        public int ActorId { get; set; }
    }
}
