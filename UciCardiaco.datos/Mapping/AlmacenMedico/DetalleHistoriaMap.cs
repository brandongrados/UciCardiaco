using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UciCardiaco.Entidad.Almacen;

namespace UciCardiaco.Datos.Mapping.Almacen
{
    public class DetalleHistoriaMap : IEntityTypeConfiguration<DetalleHistoria>
    {
        public void Configure(EntityTypeBuilder<DetalleHistoria> builder)
        {
            builder.ToTable("DETALLE_HISTORIA")
                .HasKey(d=>d.iddetalle_historia);
        }
    }
}
