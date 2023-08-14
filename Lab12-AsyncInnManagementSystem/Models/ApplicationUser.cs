using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
namespace Lab12_AsyncInnManagementSystem.Models
{
   public class ApplicationUser : IdentityUser
    {
		[Key]
		public string Id { get; set; }
		public string Password { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; } 
		public string? Token { get; set; }
		public IList<string>? Roles { get; set; }
    }
}

