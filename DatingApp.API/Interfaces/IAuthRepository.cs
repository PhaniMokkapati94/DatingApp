using System.Threading.Tasks;
using DatingApp.API.Models;
namespace DatingApp.API.Interfaces
{
    public interface IAuthRepository
    {
        Task<User> Login(string Username,string Password);

        Task<User> Register(User user,string Password);

        Task<bool> UserExists(string Username);
    }
}