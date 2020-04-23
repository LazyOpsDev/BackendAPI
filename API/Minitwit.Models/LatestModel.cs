using System;
using System.ComponentModel.DataAnnotations;

namespace Minitwit.Models
{
    public class LatestModel
    {
        [Key]
        public Guid Id { get; set; }
        public int latest { get; set; }
    }
}
