using Isopoh.Cryptography.Argon2;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mvp_studio_api.Models;
using NuGet.Common;
using testApi;

namespace mvp_studio_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }


        // post request to see if user is authenticated
        [HttpPost("login")]
        public IActionResult Login(Admin admin)
        {

            var IsAuthenticated = ValidateUserCredentials(admin.Email, admin.Password);

            if(IsAuthenticated)
            {
                // gen a new valid token
                return Ok(new {Token = "good job"});
            } 

            return Unauthorized();  
            
        }

        private bool ValidateUserCredentials(string email, string password)
        {
            var admin = _context.Admin.FirstOrDefault(u  => u.Email == email); // checking if we get valid user

            if (admin != null)
            {
                if(Argon2.Verify(admin.Password, password))
                {
                    return true;
                }
               
            }
            return false;
        }
    }
}
