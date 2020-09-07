using UciCardiaco.Entidad.Almacen;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UciCardiaco.Datos.Mapping.Almacen
{
    public class SintomaMap : IEntityTypeConfiguration<Sintoma>
    {
        public void Configure(EntityTypeBuilder<Sintoma> builder)
        {
            builder.ToTable("SINTOMA")
                .HasKey(s => s.idsintoma);
                builder.Property(s => s.nombre)
               .HasMaxLength(50);
                builder.Property(s => s.descripcion)
                .HasMaxLength(256);

        }
    }
}
