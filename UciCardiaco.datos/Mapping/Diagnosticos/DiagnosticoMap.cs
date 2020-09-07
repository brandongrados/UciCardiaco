using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UciCardiaco.Entidad.Diagnosticos;

namespace UciCardiaco.Datos.Mapping.Diagnosticos
{
    public class DiagnosticoMap : IEntityTypeConfiguration<Diagnostico>
    {
        public void Configure(EntityTypeBuilder<Diagnostico> builder)
        {
            builder.ToTable("DIAGNOSTICO")
                .HasKey(d =>  d.id_diagnostico );
            builder.HasOne(d => d.persona)
                .WithMany(p => p.diagnosticos)
                .HasForeignKey(d => d.idpaciente);
        }
    }
}
