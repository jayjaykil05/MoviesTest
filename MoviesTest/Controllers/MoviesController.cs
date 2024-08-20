using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesTest.Models;

namespace MoviesTest.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : Controller
    {
        private readonly MovieTestContext _context;

        public MoviesController( MovieTestContext context)
        {
            _context = context;
        }

        [HttpGet("allMovie")]
        public async Task<ActionResult> AllAvailableMoviesList()
        {
            try
            {
                var movieList = _context.Movies.Where(x => x.IsDeleted == false).AsNoTracking().ToList();

                return Ok(new { isSuccess = true, movieList });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { isSuccess = false, message = "error fetching the list." });
            }
        }

        [HttpGet("allRented")]
        public async Task<ActionResult> RentedMoviesList()
        {
            try
            {
                var movieList = _context.Movies.Where(x => x.IsDeleted == false && x.IsRented == true).AsNoTracking().ToList();

                return Ok(new { isSuccess = true, movieList });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { isSuccess = false, message = "error fetching the list." });
            }
        }

        [HttpGet("allNotRented")]
        public async Task<ActionResult> NotRentedMoviesList()
        {
            try
            {
                var movieList = _context.Movies.Where(x => x.IsDeleted == false && x.IsRented == false).AsNoTracking().ToList();

                return Ok(new { isSuccess = true, movieList });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { isSuccess = false, message = "error fetching the list." });
            }
        }
    }
}
