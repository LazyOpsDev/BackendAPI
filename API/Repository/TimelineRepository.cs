using Infrastructure.Interfaces;
using Minitwit.DataAccessLayer;
using Minitwit.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class TimelineRepository : ITimelineRepository
    {
        private readonly CustomDbContext _context;

        public TimelineRepository(CustomDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Message>> GetPublicTimeline()
        {
            //return new Message[] { };
            return await _context.Messages.Where(m => !m.Flagged).ToListAsync();
        }

        public async Task<IEnumerable<Message>> GetTimelineForLoggedInUser(Guid userId)
        {
            return await _context.Messages.
            Join(_context.Followers,
            m => m.User.UserId,
            f => f.Self.UserId,
            (m, f) => new { Message = m, Follower = f }).
            Where(elem => elem.Message.UserId == userId && elem.Follower.Self.UserId == userId)
            .Select(elem => elem.Message).ToListAsync();
        }

        public async Task<IEnumerable<Message>> GetUserTimeline(string username)
        {
            return await _context.Messages.Include(m => m.User).Where(m => !m.Flagged && m.User.Username == username).ToListAsync();
        }

        public async Task PostMessage(string username, string message)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null)
                throw new ArgumentException();
            var msg = new Message
            {
                Content = message,
                Flagged = false,
                User = user,
                UserId = user.UserId,
                PublishedTime = DateTime.Now,
            };
            _context.Messages.Add(msg);
            await _context.SaveChangesAsync();
        }
    }
}
