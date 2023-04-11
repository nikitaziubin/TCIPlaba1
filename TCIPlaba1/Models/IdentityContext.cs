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
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
		=> optionsBuilder.UseSqlServer("Server= LAPTOP-J5R1H9E6; Database=DBIdentity; Trusted_Connection=True; Trust Server Certificate=True;");
	}
}
