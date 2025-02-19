using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendChallenge.Model
{
    public class User : IdentityUser
    {
        public string Fullname { get; set; } = string.Empty;
    }
}
