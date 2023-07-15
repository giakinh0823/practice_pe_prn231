using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using PE_BEST_PRACTICE.Models;

namespace PE_BEST_PRACTICE.Controllers
{
    public class DirectorsController : ODataController
    {
        private readonly PE_PRN_Fall22B1Context _context;

        public DirectorsController(PE_PRN_Fall22B1Context context)
        {
            _context = context;
        }

        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(_context.Directors.ToList());
        }

        [EnableQuery]
        public IActionResult Get(int key)
        {
            return Ok(_context.Directors.Where(d => d.Id == key).FirstOrDefault());
        }
    }
}
