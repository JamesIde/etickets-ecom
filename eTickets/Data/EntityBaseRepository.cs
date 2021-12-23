using eTickets.Data.BaseRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eTickets.Data
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        private readonly ApplicationDbContext _db;
        public EntityBaseRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task Create(T entity)
        {
            await _db.Set<T>().AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var entity = await _db.Set<T>().FirstOrDefaultAsync(n => n.Id == id);
            EntityEntry entityEntry = _db.Entry<T>(entity);
            entityEntry.State = EntityState.Deleted;

            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> Get() => await _db.Set<T>().ToListAsync();
        public async Task<T> GetById(int id) => await _db.Set<T>().FirstOrDefaultAsync(n => n.Id == id);

        public async Task Update(int id, T entity)
        {
            EntityEntry entityEntry = _db.Entry<T>(entity);
            entityEntry.State = EntityState.Modified;

            await _db.SaveChangesAsync();
        }
    }
}
