using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using eTickets.Models;
namespace eTickets.Services.IServices
{
    public interface IActorService
    {
        public Task<IEnumerable<Actor>> GetActors();

    }
}
