using eTickets.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace eTickets.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _db;

        public MoviesController(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            var movies = await _db.Movies.Include(n => n.Cinema).OrderBy(n => n.Name).ToListAsync();
            return View(movies);
        }
    }
}
