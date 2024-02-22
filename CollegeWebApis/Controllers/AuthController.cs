using ClassLibrary.Data.Entities;
using ClassLibrary.Service.AuthService;
using CollegeWebApis.Model.Dto;
using CollegeWebApis.Model.RequestModel;
using Microsoft.AspNetCore.Mvc;


namespace CollegeWebApis.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;

        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpGet(Name ="Login")]
        public  IActionResult Login([FromQuery] Microsoft.AspNetCore.Identity.Data.LoginRequest loginRequest)
        {
            if(!ModelState.IsValid) { 
                BadRequest(ModelState);
            }
            var result  =  _authRepository.Login(loginRequest);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            var result = await _authRepository.GetAllUsersAsync();
            return result;
        }

        [HttpPost(Name ="AddNewUser")]
        public async Task<IActionResult> AddUser(AddUserDto addUserDto)
        {
            if(ModelState.IsValid)
            {
                var newUser = new User()
                {
                    Id = Guid.NewGuid(),
                    Name = addUserDto.Name,
                    Email = addUserDto.Email,
                    PhoneNo = addUserDto.PhoneNo,
                    address = addUserDto.address,
                    Password = addUserDto.Password,
                };
                await _authRepository.AddUserAsync(newUser);
                return Ok("User is added Successfully");
            }            
            return BadRequest(ModelState);  
        }
    }
}
