using Microsoft.EntityFrameworkCore;
using SisMed.Models.Entities;
using SisMed.Models.EntityConfigurations;

namespace SisMed.Models.Contexts
{
    public class SisMedContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public SisMedContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public DbSet<Medico> Medicos => Set<Medico>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("SisMed"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MedicoConfiguration());
        }
    }
}