using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CompanyServiceAPI.Controllers
{
    public class TokenController : Controller
    {
        private const string SECRET_KEY = "asfjaslkjfasljflaskjflkasjfalksjflaskjf";
        public static readonly SymmetricSecurityKey SIGNING_KEY = new
                      SymmetricSecurityKey(Encoding.UTF8.GetBytes(SECRET_KEY));

        [HttpGet]
        [Route("api/Token/{username}/{password}")]
        public IActionResult Get(string username, string password)
        {
            if (username == password)
                return new ObjectResult(GenerateToken(username));
            else
                return BadRequest();
        }
        private string GenerateToken(string username)
        {
            var token = new JwtSecurityToken(
                claims: new Claim[] { new Claim(ClaimTypes.Name, username) },
                notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                expires: new DateTimeOffset(DateTime.Now.AddMinutes(60)).DateTime,
                signingCredentials: new SigningCredentials(SIGNING_KEY,
                                                    SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

