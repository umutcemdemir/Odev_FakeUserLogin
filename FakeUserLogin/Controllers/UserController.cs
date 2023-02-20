using AutoMapper;
using FakeUserLogin.Applications.TokenOperations.Commands;
using FakeUserLogin.Applications.UserOperations.Commands;
using FakeUserLogin.DbOperations;
using FakeUserLogin.Models;
using FakeUserLogin.ViewModels.UserViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FakeUserLogin.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class UserController : Controller
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserController(BookStoreDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateUserViewModel newUser)
        {
            CreateUserCommand command = new(_context, _mapper);
            try
            {
                command.Model = newUser;
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


            return Ok();
        }

        [HttpPost("connect/token")]
        public ActionResult<Token> CreateToken([FromBody] CreateTokenViewModel login)
        {
            CreateTokenCommand command = new(_context, _mapper, _configuration);
            Token token = new();
            try
            {
                command.Model = login;
                token = command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return token;
        }
    }
}
