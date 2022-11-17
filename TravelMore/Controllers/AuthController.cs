using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TravelMore.Models;
using TravelMore.ViewModels;

namespace TravelMore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly TravelMoreDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(
            UserManager<User> userManager,
            TravelMoreDbContext context,
            IConfiguration configuration
        )
        {
            _userManager = userManager;
            _context = context;
            _configuration = configuration;
        }

        private bool IsStringNumericOrEmpty(string str)
        {
            str = str.Trim();
            if(string.IsNullOrEmpty(str) || str.Any(char.IsDigit))
            {
                return true;
            }
            return false;
        }

        private bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false; // suggested by @TK-421
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }

        [HttpPost("register-user")]
        public async Task<IActionResult> Register(RegisterVM user)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("Please fill out all inputs");
            }

            var findUserByEmail = await _userManager.FindByEmailAsync(user.Email);
            var findUserByUsername = await _userManager.FindByNameAsync(user.UserName);

            if (findUserByEmail != null)
            {
                return BadRequest($"{findUserByEmail.Email} already exists");
            }
            if(findUserByUsername != null)
            {
                return BadRequest($"User with {findUserByUsername.UserName} already exists");
            }
            if (!IsValidEmail(user.Email!))
            {
                return BadRequest("Invalid email");
            }
            if(IsStringNumericOrEmpty(user.FirstName!))
            {
                return BadRequest("First name shouldn't be empty and can't contain numbers");
            }
            if (IsStringNumericOrEmpty(user.LastName!))
            {
                return BadRequest("Last name shouldn't be empty and can't contain numbers");
            }
            if(user.Password != user.RePassword)
            {
                return BadRequest("Passwords don't match");
            }
            if(string.IsNullOrEmpty(user.Password))
            {
                return BadRequest("Password is empty");
            }
            

            var newUser = new User()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email
            };

            var response = await _userManager.CreateAsync(newUser,user.Password);

            if(response.Succeeded)
            {
                return Created($"user/{newUser.Id}", newUser);
            }

            return BadRequest("User can't be created");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(AuthVM user)
        {
            
            if(!ModelState.IsValid)
            {
                return BadRequest("Please fill out all inputs");
            }

            var userExists = await _userManager.FindByNameAsync(user.UserName);

            if(userExists == null)
            {
                return BadRequest("User coudn't be found");
            }

            var isValidPassword = await _userManager.CheckPasswordAsync(userExists, user.Password);

            if(isValidPassword)
            {
                var token = await GenerateJWTToken(userExists);
                return Ok(token);
            }

            return BadRequest("Incorrect credentials");

        }

        [HttpGet("get-all-user")]
        public List<User> GetAllUsers()
        {
            return _userManager.Users.ToList();
        }

        private async Task<TokenVM> GenerateJWTToken(User user)
        {
            var authClaims = new List<Claim>()
            {
                new Claim("userName",user.UserName),
                new Claim("userID",user.Id),
            };

            var authSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]!));

            var token = new JwtSecurityToken(
                claims:authClaims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials:new SigningCredentials(authSigningKey,SecurityAlgorithms.HmacSha256)
            );

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            var response = new TokenVM { Token = jwtToken, ExpiresAt = token.ValidTo };

            return response;

        }
    }
}
