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
		public DbSet<Character> Characters { get; set; }
        public DbSet<Weapon> Weapons { get;set; }

		public DbSet<UserLogin> userLogins { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
            modelBuilder.Entity<Character>().HasOne(t => t.weapon)
                .WithOne(t => t.character)
                .HasForeignKey<Weapon>(t => t.characterId);

            modelBuilder.Entity<Weapon>().HasOne(t => t.character)
                .WithOne(t => t.weapon)
                .HasForeignKey<Character>(t => t.weaponId);

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserLogin>().HasNoKey();    
		}
	}
}
