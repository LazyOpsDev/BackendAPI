using System;
using System.ComponentModel.DataAnnotations;

namespace Minitwit.Models
{
    public class Follower
    {
        [Key]
        public Guid FollowerId { get; set; }
        public User Self { get; set; }
        public User Following { get; set; }
    }
}
