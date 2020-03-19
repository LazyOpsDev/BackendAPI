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

        public IEnumerable<Message> GetPublicTimeline()
        {
            //return new Message[] { };
            //using (var _context = new CustomDbContext())
                return  _context.Messages.Where(m => !m.Flagged).OrderByDescending(m => m.PublishedTime).Take(100);
        }

        public IEnumerable<Message> GetTimelineForLoggedInUser(Guid userId)
        {
            //using (var _context = new CustomDbContext())
                return _context.Messages.
            Join(_context.Followers,
            m => m.User.UserId,
            f => f.Self.UserId,
            (m, f) => new { Message = m, Follower = f }).
            Where(elem => elem.Message.UserId == userId && elem.Follower.Self.UserId == userId)
            .Select(elem => elem.Message).ToList();
        }

        public IEnumerable<Message> GetUserTimeline(string username)
        {
            //using (var _context = new CustomDbContext())
                return _context.Messages.Include(m => m.User).Where(m => !m.Flagged && m.User.Username == username).ToList();
        }

        public void PostMessage(string username, string message)
        {
            //using(var _context = new CustomDbContext())
            //{
                var user = _context.Users.FirstOrDefault(u => u.Username == username);
                if (user == null)
                    throw new ArgumentException();
                var msg = new Message();

                msg.Content = message;
                msg.Flagged = false;
                msg.User = user;
                msg.UserId = user.UserId;
                msg.PublishedTime = DateTime.Now;

                _context.Messages.Add(msg);
                _context.SaveChanges();
            //}
        }
    }
}
