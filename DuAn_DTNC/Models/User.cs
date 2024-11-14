using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DuAn_DTNC.Models
{
    public class User 
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; } = "user";


    }
}
