using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using Q1.Dto;
using Q1.Models;

namespace Q1.Controllers
{
    public class StarsController : ODataController
    {
        private readonly PE_PRN_Fall22B1Context _context;
        private readonly IMapper _mapper;

        public StarsController(PE_PRN_Fall22B1Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        
        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(_context.Stars.ToList());
        }
        

        // lấy detail
        [EnableQuery]
        public IActionResult Get(int key)
        {
            Star? star = _context.Stars.Include(star => star.Movies).Where(star => star.Id == key).FirstOrDefault();
            return Ok(_mapper.Map<StarResponse>(star));
        }


        [EnableQuery]
        public IActionResult Post([FromBody] StarRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Star star = _mapper.Map<Star>(request);

            try
            {
                _context.Stars.Add(star);
                _context.SaveChanges();
            }
            catch
            {
                return Conflict("Không thể tạo director");
            }

            return Created(star);
        }

        [EnableQuery]
        public IActionResult Put(int key, [FromBody] Star star)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Star? starExist = null;

            try
            {
                starExist = _context.Stars.Where(s => s.Id == key).FirstOrDefault();
                if (starExist == null) return NotFound();
                starExist.FullName = star.FullName;
                starExist.Nationality = star.Nationality;
                starExist.Description = star.Description;
                starExist.Dob = star.Dob;
                starExist.Male = star.Male;

                _context.Stars.Update(starExist);

                _context.SaveChanges();
            }
            catch
            {
                return Conflict("Không thể cập nhật director");
            }

            return Created(star);
        }


        /*
        [EnableQuery]
        public IActionResult Get(ODataQueryOptions<Star> options)
        {
            IQueryable<Star> starsQuery = _context.Stars.Include(star => star.Movies);

            if (options.Filter != null)
            {
                starsQuery = options.Filter.ApplyTo(starsQuery, new ODataQuerySettings()) as IQueryable<Star>;
            }

            if (options.OrderBy != null)
            {
                starsQuery = options.OrderBy.ApplyTo(starsQuery, new ODataQuerySettings()) as IQueryable<Star>;
            }

            if (options.Skip != null)
            {
                starsQuery = options.Skip.ApplyTo(starsQuery, new ODataQuerySettings()) as IQueryable<Star>;
            }

            if (options.Top != null)
            {
                starsQuery = options.Top.ApplyTo(starsQuery, new ODataQuerySettings()) as IQueryable<Star>;
            }

            return Ok(starsQuery.ToList());
        }
        */
    }
}
