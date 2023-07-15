using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PE_BEST_PRACTICE.Dto;
using PE_BEST_PRACTICE.Models;
using System.Net;

namespace PE_BEST_PRACTICE.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StarController : Controller
    {
        private readonly PE_PRN_Fall22B1Context _context;

        public StarController(PE_PRN_Fall22B1Context context)
        {
            this._context = context;
        }

        [HttpGet("{nationality}/{gender}")]
        public IActionResult GetStars([FromRoute] string nationality, [FromRoute] string gender)
        {
            bool isMale = gender.Equals("Male") || gender.Equals("male");

            var stars = _context.Stars
                .Where(star => star.Nationality.ToLower() == nationality.ToLower() && star.Male == isMale)
                .Select(star => new
                {
                    id = star.Id,
                    fullName = star.FullName,
                    male = star.Male,
                    gender = star.Male !=null && (bool)star.Male ? "Male" : "Female",
                    dob = star.Dob,
                    dobString = star.Dob != null ? star.Dob.Value.ToString("MM/dd/yyyy") : "",
                    description = star.Description,
                    nationality = star.Nationality
                }).ToList();

            return Ok(stars);
        }


        [HttpGet("{starId}")]
        public IActionResult GetStar([FromRoute] int starId)
        {
            var stars = _context.Stars
                .Include(star => star.Movies)
                .Where(star => star.Id == starId)
                .Select(star => new
                {
                    id = star.Id,
                    fullName = star.FullName,
                    male = star.Male,
                    gender = star.Male != null && (bool)star.Male ? "Male" : "Female",
                    dob = star.Dob,
                    dobString = star.Dob != null ? star.Dob.Value.ToString("MM/dd/yyyy") : "",
                    description = star.Description,
                    nationality = star.Nationality,
                    movies = star.Movies.Select(movie => new
                    {
                        id = movie.Id,
                        title = movie.Title,
                        releaseDate = movie.ReleaseDate,
                        releaseYear = movie.ReleaseDate.Value.Year,
                        description = movie.Description,
                        language = movie.Language,
                        producerId = movie.ProducerId,
                        directorId = movie.DirectorId,
                        producerName = movie.Producer == null ? null : movie.Producer.Name,
                        directorName = movie.Director == null ? null : movie.Director.FullName,
                        genres = new List<Genre>(),
                        stars = new List<Star>()
                    })
                }).FirstOrDefault();

            return Ok(stars);
        }


        [HttpPost]
        public IActionResult Create([FromBody] StarRequest request)
        {
            try
            {
                Star star = new Star
                {
                    FullName = request.FullName,
                    Male = request.Male,
                    Dob = request.Dob,
                    Description = request.Description,
                    Nationality = request.Nationality,
                };
                _context.Stars.Add(star);
                _context.SaveChanges();
                return Ok(1);
            } catch
            {
                return StatusCode(409, "There is an error while adding");
            }
        }
    }
}
