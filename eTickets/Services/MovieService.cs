using eTickets.Data;
using eTickets.Data.ViewModels;
using eTickets.Models;
using eTickets.Services.IServices;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Services
{
    public class MovieService : EntityBaseRepository<Movie>, IMovieService
    {
        private readonly ApplicationDbContext _db;
        public MovieService(ApplicationDbContext db) : base(db) { 
        _db= db;    
        }

        public async Task AddNewMovie(NewMovieVM data)
        {

            var newMovie = new Movie()
            {
                Name = data.Name,
                Description = data.Description,
                Price = data.Price,
                ImageURL = data.ImageURL,
                WatchTime = data.WatchTime,
                StartDate = data.StartDate,
                EndDate = data.EndDate,
                MovieCategory = data.MovieCategory,
                CinemaId = data.CinemaId,
                ProducerId = data.ProducerId
            };
            await  _db.Movies.AddAsync(newMovie);
            await _db.SaveChangesAsync();

            //Add movie actors and use join table

            foreach (var actorId in data.ActorId)
            {
                var newActorMovie = new Actor_Movie()
                {
                    ActorId = actorId,
                    MovieId = newMovie.Id
                };
                await _db.Actors_Movies.AddAsync(newActorMovie);

            }
            await _db.SaveChangesAsync();
        }

        public async Task<Movie> GetMovieByAsync(int id)
        {
            var movieDetails = await _db.Movies
                .Include(c => c.Cinema)
                .Include(c => c.Producer)
                .Include(c => c.Actors_Movies).ThenInclude(a=>a.Actor)
                .FirstOrDefaultAsync(n=>n.Id == id);

            return  movieDetails;
        }

        public async Task<MovieDropDownsVM> GetMovieDropDownVMValues()
        {
            var response  = new MovieDropDownsVM();
            response.Actors = await _db.Actors.OrderBy(n=>n.Name).ToListAsync();
            response.Cinemas = await _db.Cinemas.OrderBy(n => n.Name).ToListAsync();
            response.Producers = await _db.Producers.OrderBy(n => n.Name).ToListAsync();
            return response;
        }

        public async Task UpdateMovieAsync(NewMovieVM data)
        {
           var dbMovie = await _db.Movies.FirstOrDefaultAsync(n=>n.Id==data.Id);

            if(dbMovie != null) {

                dbMovie.Name = data.Name;
                dbMovie.Description = data.Description;
                dbMovie.Price = data.Price;
                dbMovie.ImageURL = data.ImageURL;
                dbMovie.WatchTime = data.WatchTime;
                dbMovie.StartDate = data.StartDate;
                dbMovie.EndDate = data.EndDate;
                dbMovie.MovieCategory = data.MovieCategory;
                dbMovie.CinemaId = data.CinemaId;
                dbMovie.ProducerId = data.ProducerId;
                await _db.SaveChangesAsync();

            };
            

            var existingActors = _db.Actors_Movies.Where(n=>n.MovieId == data.Id).ToList();
            _db.Actors_Movies.RemoveRange(existingActors);
            await _db.SaveChangesAsync();

            //Add movie actors and use join table

            foreach (var actorId in data.ActorId)
            {
                var newActorMovie = new Actor_Movie()
                {
                    ActorId = actorId,
                    MovieId = data.Id
                };
                await _db.Actors_Movies.AddAsync(newActorMovie);

            }
            await _db.SaveChangesAsync();
        }
    }
}
