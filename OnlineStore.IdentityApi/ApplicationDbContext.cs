using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace OnlineStore.IdentityApi
{
    /// <summary>
    /// ApplicationDbContext.
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        /// <summary>
        /// ApplicationDbContext controller.
        /// </summary>
        /// <param name="options">options</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        /// <summary>
        /// On model creating method.
        /// </summary>
        /// <param name="builder">builder</param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("idt");
            base.OnModelCreating(builder);
            this.SeedRoles(builder);
        }

        /// <summary>
        /// SeedRoles method. Seeding default roles in database.
        /// </summary>
        /// <param name="builder"></param>
        private void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole() { Id = "fab4fac1-c546-41de-aebc-a14da6895711", Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin" },
                new IdentityRole() { Id = "c7b013f0-5201-4317-abd8-c211f91b7330", Name = "User", ConcurrencyStamp = "2", NormalizedName = "User" }
                );
        }
    }
}
