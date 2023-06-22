using Microsoft.EntityFrameworkCore;
using User_Mgmt_Api.Model;

namespace User_Mgmt_Api.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options) 
        {
            
        }
        public DbSet<User> Users { get; set; }

        public DbSet<UserLogin> userLogins { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserLogin>().HasNoKey();    
		}
	}
}
