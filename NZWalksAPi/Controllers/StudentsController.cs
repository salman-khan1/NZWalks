using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPi.Data;

namespace NZWalksAPi.Controllers

{   //https://locahost:portnumber/api/students
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        public StudentsController(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        //GET:https://locahost:portnumber/api/students
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            var names = dbContext.Students.ToList();
            return Ok(names);

        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetStudentByID(int id) { 
            var name=dbContext.Students.SingleOrDefault(x => x.Id == id);
            if(id == null)
            {
                return NotFound();
            }
            return Ok(name);
        }
    }
}
