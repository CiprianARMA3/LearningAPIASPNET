using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Data
{
  

    public class ApplicationDBContext : IdentityDbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        public DbSet<Clienti> Clienti { get; set; }
        public DbSet<Operazioni> Operazioni { get; set; }
    }
}