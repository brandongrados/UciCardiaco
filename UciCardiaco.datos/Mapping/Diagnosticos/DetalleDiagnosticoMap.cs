using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UciCardiaco.Entidad.Diagnosticos;

namespace UciCardiaco.Datos.Mapping.Diagnosticos
{
    public class DetalleDiagnosticoMap : IEntityTypeConfiguration<DetalleDiagnostico>
    {
        public void Configure(EntityTypeBuilder<DetalleDiagnostico> builder)
        {
            builder.ToTable("DETALLE_DIAGNOSTICO")
                .HasKey(dd => dd.iddetalle_diagnostico);
        }
    }
}
