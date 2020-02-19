using Minitwit.Models;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> UnfollowUser(string username, string unfollows);
        Task<bool> FollowUser(string username, string follows);
        Task<Guid> RegisterUser(RegisterModel user);
        Task<Guid> Login(LoginModel user);
    }
}
