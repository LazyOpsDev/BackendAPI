using System.ComponentModel.DataAnnotations;

namespace Infrastructure
{
    public class RegisterModel
    {
        [Required]
        public string username { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string pwd { get; set; }
        //[Required]
        //public string RepeatedPassword { get; set; }
    }
}
