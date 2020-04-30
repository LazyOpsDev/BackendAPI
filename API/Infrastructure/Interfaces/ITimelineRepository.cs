using Minitwit.Models;
using System;
using System.Collections.Generic;

namespace Infrastructure.Interfaces
{
    public interface ITimelineRepository
    {
        IEnumerable<Message> GetPublicTimeline();
        IEnumerable<Message> GetTimelineForLoggedInUser(Guid uid);
        IEnumerable<Message> GetUserTimeline(string username);

        void PostMessage(string username, string message);

    }
}
