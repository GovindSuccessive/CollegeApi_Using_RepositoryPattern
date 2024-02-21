using ClassLibrary.Context;
using ClassLibrary.Data.Entities;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ClassLibrary.Service.AuthService
{
    public class AuthRepository : IAuthRepository
    {
        private readonly CollegeDbContext _collegeDbContext;
        private readonly IConfiguration _configuration;

        public AuthRepository(CollegeDbContext collegeDbContext,IConfiguration configuration)
        {
           _collegeDbContext = collegeDbContext;
            _configuration = configuration;
        }
        public async Task<User> AddUserAsync(User user)
        {
            var result = await _collegeDbContext.Users.AddAsync(user);
            await _collegeDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
           var users =  await _collegeDbContext.Users.ToListAsync();
            return users;
        }

        public IResult Login(LoginRequest user)
        {
            if(user.Email != null && user.Password!=null) {

                var res = _collegeDbContext.Users.SingleOrDefault(X => X.Email.ToLower() == user.Email.ToLower() && X.Password.ToLower() == user.Password.ToLower());
                if(res != null)
                {
                    var issuer = _configuration["Jwt:Issuer"];
                    var audience = _configuration["Jwt:Audience"];
                    var key = Encoding.ASCII.GetBytes
                    (_configuration["Jwt:Key"]);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new[]
                        {
                            new Claim("Id", Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                            new Claim(JwtRegisteredClaimNames.Email, user.Email),
                            new Claim(JwtRegisteredClaimNames.Jti,
                            Guid.NewGuid().ToString())
                        }),
                        Expires = DateTime.UtcNow.AddMinutes(5),
                        Issuer = issuer,
                        Audience = audience,
                        SigningCredentials = new SigningCredentials
                        (new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha512Signature)
                    };
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    var jwtToken = tokenHandler.WriteToken(token);
                    var stringToken = tokenHandler.WriteToken(token);
                    return Results.Ok(stringToken);
                }
                return Results.Unauthorized();
            }
            return Results.BadRequest(user);
            
        }

    }
}
