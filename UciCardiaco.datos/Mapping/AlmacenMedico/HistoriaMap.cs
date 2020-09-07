using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UciCardiaco.Entidad.Almacen;

namespace UciCardiaco.Datos.Mapping.Almacen
{
    public class HistoriaMap : IEntityTypeConfiguration<Historia>
    {
        public void Configure(EntityTypeBuilder<Historia> builder)
        {
            builder.ToTable("HISTORIA")
                .HasKey(h => h.idhistoria);
            builder.HasOne(h=>h.persona)
                .WithMany(p=>p.historias)
                .HasForeignKey(h => h.idenfermera);
        }
    }
}
