using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Q1.Models;

namespace PE_BEST_PRACTICE.Controllers
{
    public class MoviesController : ODataController
    {
        private readonly PE_PRN_Fall22B1Context _context;
        private readonly IMapper _mapper;

        public MoviesController(PE_PRN_Fall22B1Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }



        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(_context.Movies.ToList());
        }

    }
}
