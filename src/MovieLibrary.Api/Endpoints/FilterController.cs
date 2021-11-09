using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieLibrary.Data;
using MovieLibrary.Data.Entities;

namespace MovieLibrary.Api.Endpoints
{
    [Route("/v1/Movie/Filter/[controller]/[action]")]
    [ApiController]
    [Produces("application/json")]
    public class FilterController : ControllerBase
    {
        private readonly MovieLibraryContext _context;

        public FilterController(MovieLibraryContext context)
        {
            _context = context;
        }


        // GET: /v1/Movie/Filter
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            return await _context.Movies.ToListAsync();
        }

        /// <summary>
        /// Get list of movies, whose titles contains written text
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        // GET: /v1/Movie/Filter/text
        [HttpGet("{Text}")]
        [ActionName("GetTitle")]
        public async Task<ActionResult<IEnumerable<Movie>>> GetTitle(string Text)
        {
            var movies = await _context.Movies.Where(x => x.Title.ToLower().Contains(Text.ToLower())).ToListAsync();

            if (movies == null)
            {
                return NotFound();
            }
            List<Movie> sorted = movies.OrderByDescending(x => x.ImdbRating).ToList();

            var categories = await _context.Categories.ToListAsync();
            var movie_categories = await _context.MovieCategories.ToListAsync();

            // 1. Search movie id record in movie_categories
            // 2. Get category id from it
            // 3. Search record with category id in categories
            // 4. Add category name to movie

            // movies -> filtered movies
            foreach (var item in sorted)
            {
                //info - linked movie id with category id
                var info = movie_categories.Find(x => x.MovieId == item.Id);
                //info.categoryId
                var category = categories.Find(x => x.Id == info.CategoryId);
                //category.Name
                //item.MovieCategories.Add(category); ---> adding to infinity (if searched again, it will add same category again)
                item.MovieCategories.Clear();
                item.MovieCategories.Add(info);
            }

            return sorted;
        }

        //[HttpGet("{categories}")]
        //[ActionName("GetCategories")]
        //public async Task<ActionResult<IEnumerable<Movie>>> Categories(int[] categories)
        //{
        //    List<Movie> movies = new List<Movie>();
        //    foreach (var item in categories)
        //    {
        //        var movcat = await _context.MovieCategories.Where(x => x.CategoryId == item).ToListAsync();
        //        foreach (var id in movcat)
        //        {
        //            var movie = await _context.Movies.Where(x => x.Id == id.MovieId).ToListAsync();
        //            movies.Add(movie);
        //        }
        //    }

        //    if (movies == null)
        //    {
        //        return NotFound();
        //    }
        //    List<Movie> sorted = movies.OrderByDescending(x => x.ImdbRating).ToList();
        //    // fill Categories array searching MovieCategory entities with matching MovieId

        //    return sorted;
        //}

        [HttpGet("{rating}")]
        [ActionName("GetRating")]
        public async Task<ActionResult<IEnumerable<Movie>>> Rating(decimal minImdb, decimal maxImdb)
        {
            var movies = await _context.Movies.Where(x => maxImdb > x.ImdbRating && x.ImdbRating > minImdb).ToListAsync();

            if (movies == null)
            {
                return NotFound();
            }
            List<Movie> sorted = movies.OrderByDescending(x => x.ImdbRating).ToList();
            // fill Categories array searching MovieCategory entities with matching MovieId

            return sorted;
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }
    }
}
