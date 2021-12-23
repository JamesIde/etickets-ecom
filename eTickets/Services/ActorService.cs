using eTickets.Data;
using eTickets.Data.BaseRepository;
using eTickets.Models;
using eTickets.Services.IServices;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Services
{
    public class ActorService : EntityBaseRepository<Actor>, IActorService
    {
        public ActorService(ApplicationDbContext db) : base(db) { }
    }


}

