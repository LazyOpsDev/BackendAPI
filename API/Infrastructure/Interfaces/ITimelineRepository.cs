using Minitwit.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface ITimelineRepository
    {
        Task<IEnumerable<Message>> GetPublicTimeline();
        Task<IEnumerable<Message>> GetTimelineForLoggedInUser(Guid uid);
        Task<IEnumerable<Message>> GetUserTimeline(string username);

        Task PostMessage(string username, string message);

    }
}
