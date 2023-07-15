using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using PE_BEST_PRACTICE.Dto;
using PE_BEST_PRACTICE.Models;

namespace PE_BEST_PRACTICE.Controllers
{
    public class DirectorsController : ODataController
    {
        private readonly PE_PRN_Fall22B1Context _context;
        private readonly IMapper _mapper;

        public DirectorsController(PE_PRN_Fall22B1Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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

        [EnableQuery]
        public IActionResult Post([FromBody] DirectorRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            Director director = _mapper.Map<Director>(request);

            try
            {
                _context.Directors.Add(director);
                _context.SaveChanges();
            } catch
            {
                return Conflict("Không thể tạo director");
            }

            return Created(director);
        }

        [EnableQuery]
        public IActionResult Put(int key, [FromBody] Director director)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Director? directorExist = null;
            
            try
            {
                directorExist = _context.Directors.Where(d => d.Id == key).FirstOrDefault();
                if (directorExist == null) return NotFound();
                directorExist.FullName = director.FullName;
                directorExist.Nationality = director.Nationality;
                directorExist.Description = director.Description;
                directorExist.Dob = director.Dob;
                directorExist.Male = director.Male;
                _context.Directors.Update(directorExist);

                _context.SaveChanges();
            } catch
            {
                return Conflict("Không thể cập nhật director");
            }

            return Created(directorExist);
        }
    }
}
