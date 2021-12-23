using eTickets.Data;
using eTickets.Models;
using eTickets.Services.IServices;

namespace eTickets.Services
{
    public class ProducerService : EntityBaseRepository<Producer>, IProducerService
    {
        public ProducerService(ApplicationDbContext db) : base(db) { }

    }
}
