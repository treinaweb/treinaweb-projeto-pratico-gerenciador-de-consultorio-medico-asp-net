using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SisMed.Models.Entities;

namespace SisMed.Models.EntityConfigurations
{
    public class ConsultaConfiguration : IEntityTypeConfiguration<Consulta>
    {
        public void Configure(EntityTypeBuilder<Consulta> builder)
        {
            builder.ToTable("Consultas");
            
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Data)
                   .IsRequired();
            
            builder.Property(x => x.Tipo)
                   .IsRequired();

            builder.HasOne(c => c.Paciente)
                   .WithMany(p => p.Consultas)
                   .HasForeignKey(c => c.IdPaciente);
            
            builder.HasOne(c => c.Medico)
                   .WithMany(m => m.Consultas)
                   .HasForeignKey(c => c.IdMedico);
        }
    }
}