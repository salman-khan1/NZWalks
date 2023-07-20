using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPi.Models.DTO;
using NZWalksAPi.Repositories;

namespace NZWalksAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokkenRepository tokkenRepository;

        public AuthController(UserManager<IdentityUser> userManager,ITokkenRepository tokkenRepository)
        {
            this.userManager = userManager;
            this.tokkenRepository = tokkenRepository;
        }

        //POST:  /api/auth/register
        [HttpPost]
        [Route("Register")]
      public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.Username,
                Email = registerRequestDto.Username
            };
         var identityResult=await userManager.CreateAsync(identityUser, registerRequestDto.Password);

            if(identityResult.Succeeded)
            {
                //Add a roles to this user
                if (registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
                {
                  identityResult= await userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);

                    if(identityResult.Succeeded)
                    {
                        return Ok("User was Register! Please Login");
                    }

                }
            }
            return BadRequest("Something went wrong");

        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody]LoginRequestDto loginRequestDto)
        {
           var user= await userManager.FindByEmailAsync(loginRequestDto.Username);
            if (user != null) {
               var checkPasswordResult= await userManager.CheckPasswordAsync(user,loginRequestDto.Password);
                if (checkPasswordResult)
                {
                    //Get the roles for this users
                   var roles= await userManager.GetRolesAsync(user);
                    if (roles != null)
                    {

                        //Create Token
                       var jwtToken= tokkenRepository.CreateJWTToken(user, roles.ToList());
                        var response = new LoginResponseDto
                        {
                            JwtToken=jwtToken
                        };
                        return Ok(response);
                    } 
                }
            }
            return BadRequest("Usrname or password is incorrect");
        }
    }
}
