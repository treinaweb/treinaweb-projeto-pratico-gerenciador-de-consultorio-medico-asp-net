using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SisMed.Models.Entities;

namespace SisMed.Models.EntityConfigurations
{
    public class MedicoConfiguration : IEntityTypeConfiguration<Medico>
    {
        public void Configure(EntityTypeBuilder<Medico> builder)
        {
            builder.ToTable("Medicos");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.CRM)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.HasIndex(x => x.CRM)
                   .IsUnique();

            builder.Property(x => x.Nome)
                   .IsRequired()
                   .HasMaxLength(200);
        }
    }
}