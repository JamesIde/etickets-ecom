using eTickets.Data;
using eTickets.Data.Static;
using eTickets.Models;
using eTickets.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class MoviesController : Controller
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index(){
           var movies = await _movieService.GetAllAsync(n => n.Cinema);
            return View(movies);
        }

        //Get: Movies/Details/1
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var movie = await _movieService.GetMovieByAsync(id);
            return View(movie);
        }

        //Get: Movies/Create
        public async Task<IActionResult> Create()
        {
            var movieDrop = await _movieService.GetMovieDropDownVMValues();
            ViewBag.Cinemas = new SelectList(movieDrop.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(movieDrop.Producers, "Id", "Name");
            ViewBag.Actors = new SelectList(movieDrop.Actors, "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(NewMovieVM movie)
        {
            if(movie == null)
            {
                var movieDropdownsData = await _movieService.GetMovieDropDownVMValues();

                ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
                ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "Name");
                ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "Name");
                return View("Error");   
            }
            
            await _movieService.AddNewMovie(movie);
            return RedirectToAction(nameof(Index));
        }

        //Edit to get the movie data to edit
        public async Task<IActionResult> Edit(int id)
        {
            var movie = await _movieService.GetMovieByAsync(id);

            if (movie == null)
            {
                return View("Error");
            }

            var response = new NewMovieVM()
            {
                Name = movie.Name,
                Description = movie.Description,
                Price = movie.Price,
                ImageURL = movie.ImageURL,
                WatchTime = movie.WatchTime,
                StartDate = movie.StartDate,
                EndDate = movie.EndDate,
                MovieCategory = movie.MovieCategory,
                CinemaId = movie.CinemaId,
                ProducerId = movie.ProducerId,
                ActorId = movie.Actors_Movies.Select(n => n.ActorId).ToList()
            };


            var movieDropdownsData = await _movieService.GetMovieDropDownVMValues();

            ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "Name");
            ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "Name");

            return View(response);
        }
        //The other one to handle the post request
        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewMovieVM movie)
        {

            if (id != movie.Id)
            {
                return View("Not Found");
            }

            var movieDropdownsData = await _movieService.GetMovieDropDownVMValues();

            ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "Name");
            ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "Name");

            await _movieService.UpdateMovieAsync(movie);

            return RedirectToAction(nameof(Index));
        }
        [AllowAnonymous]
        public async Task<IActionResult> Filter(string searchString)
        {
            var allMovies = await _movieService.GetAllAsync(n => n.Cinema);

            if (!string.IsNullOrEmpty(searchString))
            {
                var filteredResult = allMovies.Where(n => n.Name.Contains(searchString) || n.Description.Contains(searchString)).ToList();
                return View("Index", filteredResult);
            }

            return View("Index", allMovies);
        }
    }
}
