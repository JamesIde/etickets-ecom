using System.Collections.Generic;
using System.Threading.Tasks;

namespace eTickets.Data.BaseRepository
{
    public interface IEntityBaseRepository<T> where T : class, IEntityBase
    {
        //Generic solution for crud operations for cinema, producer, etc

        public Task<IEnumerable<T>> Get();
        public Task<T> GetById(int id);
        public Task Delete(int id);
        public Task Create(T entity);
        public Task Update(int actorId, T entity);



    }
}
