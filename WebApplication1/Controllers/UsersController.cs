using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using WebApplication1.Auth;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private ApiDbContext _db;
        public UsersController()
        {
            _db = new ApiDbContext();
        }

        // GET
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "Api works";
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            string token = this.Request.Headers["Token"];
            if (token == null)
                return BadRequest(new { error = "No authorization header" });

            bool isValid = JWTAuth.ValidateToken(token, id);
            if (!isValid)
            {
                this.Response.StatusCode = 403;
                return new ObjectResult(new { error = "Invalid access token" });
            }

            User user = _db.Users.FirstOrDefault(u => u.Id == id);
            return new ObjectResult(new
            {
                user.Id,
                user.Email,
                user.Firstname,
                user.Lastname,
            });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody]User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            User _user = _db.Users.FirstOrDefault(
                u => u.Email == user.Email &&
                u.Password == user.Password);

            if (_user == null)
                return new UnauthorizedResult();

            string accessToken = JWTAuth.GenerateToken(_user);
            return new ObjectResult(new
            {
                _user.Id,
                _user.Email,
                access_token = accessToken
            });
        }

        [HttpPost]
        public IActionResult Post([FromBody]User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var users = _db.Users;

            // Validate uniqueness of submitted email
            if (users.FirstOrDefault(u => u.Email == user.Email) != null) {
                return BadRequest(new { error = "Email already in use" });
            }

            // Auto increment Id
            if (users.Count == 0)
                user.Id = 1;
            else
                user.Id = users.Last().Id + 1;

            _db.Users.Add(user);
            _db.SaveChanges();

            string accessToken = JWTAuth.GenerateToken(user);
            return new ObjectResult(new
            {
                user.Id,
                user.Email,
                access_token = accessToken
            });
        }
    }
}