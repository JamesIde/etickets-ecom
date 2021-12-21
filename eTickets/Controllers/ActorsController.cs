using eTickets.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace eTickets.Controllers
{
    public class ActorsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ActorsController(ApplicationDbContext db)
        {
            _db = db;
        }

    public IActionResult Index()
        {
            var data = _db.Actors.ToList();
            //Return the view, as in, the actual view in View folder. CSHTML file!
            return View(data);
        }
    }
}
