using System;

namespace Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        bool UnfollowUser(string username, string unfollows);
        bool FollowUser(string username, string follows);
        Guid RegisterUser(RegisterModel user);
        Guid Login(LoginModel user);
    }
}
