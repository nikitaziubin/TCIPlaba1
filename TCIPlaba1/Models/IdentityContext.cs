using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TCIPlaba1.Models
{
	public class IdentityContext : IdentityDbContext<Users>
	{
		public IdentityContext(DbContextOptions<IdentityContext> options)
		: base(options)
		{
			Database.EnsureCreated();
		}
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		=> optionsBuilder.UseSqlServer("Server= LAPTOP-J5R1H9E6; Database=DBIdentity; Trusted_Connection=True; Trust Server Certificate=True;");
	}
}
