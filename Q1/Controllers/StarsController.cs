using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using Q1.Models;

namespace Q1.Controllers
{
    public class StarsController : ODataController
    {
        private readonly PE_PRN_Fall22B1Context _context;

        public StarsController(PE_PRN_Fall22B1Context context)
        {
            _context = context;
        }


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

        // lấy detail
        [EnableQuery]
        public IActionResult Get(int key)
        {
            return Ok(_context.Stars.Include(star => star.Movies).Where(star => star.Id == key));
        }
    }
}
