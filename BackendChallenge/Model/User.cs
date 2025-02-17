using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BackendChallenge.Model
{
    public class User : IdentityUser
    {
        public int UserId { get; set; }
        public string Fullname { get; set; } = string.Empty;
    }
}
