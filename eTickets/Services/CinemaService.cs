using eTickets.Data;
using eTickets.Models;
using eTickets.Services.IServices;

namespace eTickets.Services
{
    public class CinemaService : EntityBaseRepository<Cinema>, ICinemaService
    {
        public CinemaService(ApplicationDbContext db) : base(db)
        {
        }
    }
}
