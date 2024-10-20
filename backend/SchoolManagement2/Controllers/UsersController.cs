using Microsoft.AspNetCore.Mvc;
using SchoolManagement.BusinessRules.Interface;
using SchoolManagement.Models;
using SchoolManagement.ServiceResult;
using SchoolManagement.Services.Interface;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUserRules _userRules;

        public UsersController(IUserRules userRules)
        {
            _userRules = userRules;
        }

        [HttpGet]
        public IActionResult GetUsers() 
        {
            var result = _userRules.GetAllUsers();
            if (result.IsNotOkResult())
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Data); 
        }

        [HttpPost]
        public IActionResult CreateUser(UserCreateVO user) 
        {
            var result = _userRules.CreateUser(user);
            if (result.IsNotOkResult())
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Data); 
        }

        [HttpGet("{id}")]
        public IActionResult GetUserByEmail(string email)
        {
            var result = _userRules.GetUserByEmail(email);
            if (result.IsNotOkResult())
            {
                return NotFound(result.Error); 
            }

            return Ok(result.Data);
        }

        [HttpPost("login")]
        public IActionResult Login(UserLogin user)
        {
            var result = _userRules.Login(user);
            if (result.IsNotOkResult())
            {
                return NotFound(result.Error);
            }

            return Ok(result.Data);
        }
    }
}
