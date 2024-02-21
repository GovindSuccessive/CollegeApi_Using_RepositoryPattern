using ClassLibrary.Data.Entities;
using Microsoft.AspNetCore.Identity.Data;


namespace ClassLibrary.Service.AuthService
{
    public interface IAuthRepository
    {
        Task<User> AddUserAsync(User user);

        IResult Login(LoginRequest loginRequest);

        Task<IEnumerable<User>> GetAllUsersAsync(); 
    }
}