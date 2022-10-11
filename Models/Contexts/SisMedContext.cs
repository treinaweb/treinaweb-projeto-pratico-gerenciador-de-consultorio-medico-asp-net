using Microsoft.EntityFrameworkCore;
using SisMed.Models.Entities;
using SisMed.Models.EntityConfigurations;

namespace SisMed.Models.Contexts
{
    public class SisMedContext : DbContext
    {
        public DbSet<Medico> Medicos => Set<Medico>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=Localhost\\SQLExpress;Database=SisMed;Integrated Security=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MedicoConfiguration());
        }
    }
}