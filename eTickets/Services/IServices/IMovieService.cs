using eTickets.Data.BaseRepository;
using eTickets.Data.ViewModels;
using eTickets.Models;
using System.Threading.Tasks;

namespace eTickets.Services.IServices
{
    public interface IMovieService : IEntityBaseRepository<Movie>
    {

        Task<Movie> GetMovieByAsync(int id);

        Task<MovieDropDownsVM> GetMovieDropDownVMValues();
        Task AddNewMovie(NewMovieVM data);

        Task UpdateMovieAsync(NewMovieVM data);

    }
}
