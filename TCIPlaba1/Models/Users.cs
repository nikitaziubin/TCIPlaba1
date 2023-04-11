using Microsoft.AspNetCore.Identity;

namespace TCIPlaba1.Models
{
	public class Users: IdentityUser
	{
		public int Year { get; set; }
	}
}
