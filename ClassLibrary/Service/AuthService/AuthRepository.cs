using ClassLibrary.Context;
using ClassLibrary.Data.Entities;
using CollegeWebApis.Model.RequestModel;
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

        public IResult Login(Microsoft.AspNetCore.Identity.Data.LoginRequest loginRequest)
        {
            if(loginRequest.Email != null && loginRequest.Password!=null) {

                var res = _collegeDbContext.Users.SingleOrDefault(X => X.Email.ToLower() == loginRequest.Email.ToLower() && X.Password.ToLower() == loginRequest.Password.ToLower());
                if(res != null)
                {
                    var issuer = _configuration["Jwt:Issuer"];
                    var audience = _configuration["Jwt:Audience"];
                    var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
                    var signingCredentials = new SigningCredentials(
                                            new SymmetricSecurityKey(key),
                                            SecurityAlgorithms.HmacSha512Signature
                                        );
                    var subject = new ClaimsIdentity(new[]
                        {
                        new Claim(JwtRegisteredClaimNames.Sub, loginRequest.Email),
                        new Claim(JwtRegisteredClaimNames.Email, loginRequest.Email),
                        });

                    var expires = DateTime.UtcNow.AddMinutes(10);

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = subject,
                        Expires = DateTime.UtcNow.AddMinutes(10),
                        Issuer = issuer,
                        Audience = audience,
                        SigningCredentials = signingCredentials
                    };
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    var jwtToken = tokenHandler.WriteToken(token);
                    return Results.Ok(jwtToken);
                }
                else
                {
                    return Results.Unauthorized();
                }
            }
            return Results.BadRequest(loginRequest);
        }




    }
}
