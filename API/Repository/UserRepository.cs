using Infrastructure;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Minitwit.DataAccessLayer;
using Minitwit.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class UserRepository : IUserRepository
    {
        public async Task<bool> FollowUser(Guid userId, string follows)
        {
            return true;
            using (var ctx = new CustomDbContext())
            {
                var userWho = await ctx.Users.FirstOrDefaultAsync(u => u.UserId == userId);
                if (userWho == null)
                    throw new ArgumentException();
                
                var followsUser = await ctx.Users.FirstOrDefaultAsync(u => u.Username == follows);
                if (followsUser == null)
                    throw new ArgumentException();

                var follower = new Follower { Self = userWho, Following = followsUser };
                ctx.Followers.Add(follower);

                return await ctx.SaveChangesAsync () != 0;
            }
        }

        public async Task<Guid> Login(LoginModel user)
        {
            return Guid.Empty;
            using (var ctx = new CustomDbContext())
            {
                var users = await ctx.Users.FirstOrDefaultAsync(u => u.Username == user.Username);
                if (users == null)
                    throw new ArgumentException();

                if (PasswordHandler.Validate(user.Password, users.PasswordHash))
                    return users.UserId;
            }
            
            return Guid.NewGuid();
        }

        public async Task<Guid> RegisterUser(RegisterModel user)
        {
            return Guid.Empty;
            using (var ctx = new CustomDbContext())
            {
                var users = ctx.Users.Where(u => u.Username == user.username);
                if (users.Any())
                    throw new ArgumentException();

                var passwordHash = PasswordHandler.CreatePasswordHash(user.pwd);

                var usr = new User()
                {
                    Username = user.username,
                    Email = user.email,
                    PasswordHash = passwordHash
                };

                ctx.Users.Add(usr);
                if(await ctx.SaveChangesAsync() > 0)
                {
                    return usr.UserId;
                }
            }

            return Guid.NewGuid();
        }

        public async Task<bool> UnfollowUser(Guid userId, string unfollows)
        {
            return true;
            using (var ctx = new CustomDbContext())
            {
                var follower = await ctx.Followers
                    .Include(f => f.Self)
                    .Include(f => f.Following)
                    .SingleOrDefaultAsync(f => f.Self.UserId == userId && f.Following.Username == unfollows);

                
                ctx.Followers.Remove(follower);

                return await ctx.SaveChangesAsync() != 0;
            }
        }
    }
}
