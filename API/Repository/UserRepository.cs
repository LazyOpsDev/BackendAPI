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
        public bool FollowUser(string username, string follows)
        {
            //using (var _context = new CustomDbContext())
            //{
                var userWho = _context.Users.FirstOrDefault(u => u.Username == username);
                if (userWho == null)
                    throw new ArgumentException();

                var followsUser = _context.Users.FirstOrDefault(u => u.Username == follows);
                if (followsUser == null)
                    throw new ArgumentException();

                var follower = new Follower { Self = userWho, Following = followsUser };
                _context.Followers.Add(follower);

                return _context.SaveChanges() != 0;
            //}
        }

        public Guid Login(LoginModel user)
        {
            //using (var _context = new CustomDbContext())
            //{
                var users = _context.Users.FirstOrDefault(u => u.Username == user.Username);
                if (users == null)
                    throw new ArgumentException();

                if (PasswordHandler.Validate(user.Password, users.PasswordHash))
                    return users.UserId;

                return Guid.NewGuid();
            //}
        }

        public Guid RegisterUser(RegisterModel user)
        {
            //using (var _context = new CustomDbContext())
            //{

                var users = _context.Users.Where(u => u.Username == user.username);
                if (users.Any())
                    return Guid.Empty;

                var passwordHash = PasswordHandler.CreatePasswordHash(user.pwd);

                var usr = new User()
                {
                    Username = user.username,
                    Email = user.email,
                    PasswordHash = passwordHash
                };

                _context.Users.Add(usr);
                if (_context.SaveChanges() > 0)
                {
                    return usr.UserId;
                }

                return Guid.Empty;
            //}
        }

        public bool UnfollowUser(string username, string unfollows)
        {
            //using (var _context = new CustomDbContext())
            //{
                var usr = _context.Users.FirstOrDefault(u => u.Username == username);
                var unflws = _context.Users.FirstOrDefault(u => u.Username == unfollows);

                var followers = _context.Followers
                    .Include(f => f.Self)
                    .Include(f => f.Following)
                    .FirstOrDefault(f => f.Self.Username == username && f.Following.Username == unfollows);

                if (followers == null)
                    return false;

                _context.Followers.Remove(followers);

                return _context.SaveChanges() != 0;
            //}
        }
    }
}
