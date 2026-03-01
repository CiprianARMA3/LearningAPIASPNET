using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using WebAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace WebAPI.Data
{
  

    public class ApplicationDBContext : IdentityDbContext<AppUser>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        public DbSet<Clienti> Clienti { get; set; }
        public DbSet<Operazioni> Operazioni { get; set; }

        // PROTECTED OVERRIDE PER SEEDING DEI RUOLI
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole { Id = "65c1fb98-1e43-4fae-9d2a-e24dc5cdeee2", Name = "Admin", NormalizedName = "ADMIN", ConcurrencyStamp = "c4608c7e-b64d-4e92-9366-267320c99bba" },
                new IdentityRole { Id = "13facfcb-31d7-46dc-a095-2fe084d5dfce", Name = "User", NormalizedName = "USER", ConcurrencyStamp = "e512cefb-cb31-419b-b5ee-8f5b8ec5d1bf" }
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}