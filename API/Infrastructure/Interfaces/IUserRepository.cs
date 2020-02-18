using Minitwit.Models;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> UnfollowUser(Guid userId, string unfollows);
        Task<bool> FollowUser(Guid userId, string follows);
        Task<Guid> RegisterUser(RegisterModel user);
        Task<Guid> Login(LoginModel user);
    }
}
