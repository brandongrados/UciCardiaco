using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UciCardiaco.Entidad.Almacen;
using System;
using System.Collections.Generic;
using System.Text;

namespace UciCardiaco.Datos.Mapping.Almacen
{
    public class EnfermedadMap : IEntityTypeConfiguration<Enfermedad>
    {
        //METODO CONFIGURE, mapeo de la entidad enfermedad
        public void Configure(EntityTypeBuilder<Enfermedad> builder)
        {
            builder.ToTable("ENFERMEDAD")
            .HasKey(e=>e.idenfermedad);
            builder.Property(e=> e.nombre)
            .HasMaxLength(50);
            builder.Property(e => e.descripcion)
            .HasMaxLength(256);
        }
    }
}
