using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NZWalksAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllTeachers()
        {
            string[] Teachers= new string[] {"Kerry","Mike","Luke","Tom"};
            return Ok(Teachers);
        }
    }
}
