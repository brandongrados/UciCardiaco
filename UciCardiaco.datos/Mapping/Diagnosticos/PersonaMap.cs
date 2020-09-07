using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UciCardiaco.Entidad.Diagnosticos;

namespace UciCardiaco.Datos.Mapping.Diagnosticos
{
    public class PersonaMap : IEntityTypeConfiguration<Persona>

    {
        public void Configure(EntityTypeBuilder<Persona> builder)
        {
            builder.ToTable("persona")
                .HasKey(p=> p.idpersona);
        }
    }
}
