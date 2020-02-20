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
        private readonly CustomDbContext _context;

        public UserRepository(CustomDbContext context)
        {
            _context = context;
        }
        public async Task<bool> FollowUser(string username, string follows)
        {
            var userWho = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (userWho == null)
                throw new ArgumentException();
            
            var followsUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == follows);
            if (followsUser == null)
                throw new ArgumentException();

            var follower = new Follower { Self = userWho, Following = followsUser };
            _context.Followers.Add(follower);

            return await _context.SaveChangesAsync () != 0;
        }

        public async Task<Guid> Login(LoginModel user)
        {
            var users = await _context.Users.FirstOrDefaultAsync(u => u.Username == user.Username);
            if (users == null)
                throw new ArgumentException();

            if (PasswordHandler.Validate(user.Password, users.PasswordHash))
                return users.UserId;
            
            return Guid.NewGuid();
        }

        public async Task<Guid> RegisterUser(RegisterModel user)
        {
            var users = _context.Users.Where(u => u.Username == user.username);
            if (users.Any())
                throw new ArgumentException();

            var passwordHash = PasswordHandler.CreatePasswordHash(user.pwd);

            var usr = new User()
            {
                Username = user.username,
                Email = user.email,
                PasswordHash = passwordHash
            };

            _context.Users.Add(usr);
            if(await _context.SaveChangesAsync() > 0)
            {
                return usr.UserId;
            }

            return Guid.NewGuid();
        }

        public async Task<bool> UnfollowUser(string username, string unfollows)
        {
            var usr = _context.Users.FirstOrDefault(u => u.Username == username);
            var unflws = _context.Users.FirstOrDefault(u => u.Username == unfollows);

            var followers = await _context.Followers
                .Include(f => f.Self)
                .Include(f => f.Following)
                .FirstOrDefaultAsync(f => f.Self.Username == username && f.Following.Username == unfollows);

            if (followers == null)
                return false;

            _context.Followers.Remove(followers);

            return await _context.SaveChangesAsync() != 0;
        }
    }
}
