using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using POC_2_JWT_Auth.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace POC_2_JWT_Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IConfiguration _config;
        public AuthController(IConfiguration configuration)
        {
            _config = configuration;
        }

        private Students AuthenticateStd(Students student)
        {
            Students _student = null;
            if (student.Name == "Mangesh" && student.Password == "Mangesh@31")
            {
                _student = new Students
                {
                    Name = "Mangesh Patanker"
                };
            }
            return _student;
        }

        //Token Generate
        private string GenerateToken(Students students)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
            var credentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                    _config["JWT:Issuer"],
                    _config["JWT:Audience"], null,
                    expires: DateTime.Now.AddHours(5),
                    signingCredentials: credentials

                    );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        //By pass authentication- will accept user parameter
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(Students student)
        {
            IActionResult response = Unauthorized();
            var student_ = AuthenticateStd(student);
            if (student_ != null)
            {
                var token = GenerateToken(student_);
                response = Ok(new
                {
                    token = token,
                });
            }
            return response;
        }
    }
}
