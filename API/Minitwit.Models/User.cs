using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Minitwit.Models
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        //[ForeignKey("TestKey")]
        public ICollection<Message> Messages { get; set; }

    }
}
