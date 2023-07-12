using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NZWalksAPi.Controllers

{   //https://locahost:portnumber/api/students
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        //GET:https://locahost:portnumber/api/students
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            string[] StudentNames = new string[] {"John","jane","dev","david","Mike","Elon"};
            return Ok(StudentNames);
        }
    }
}
