using Microsoft.EntityFrameworkCore;
using UciCardiaco.Entidad.Usuarios;

namespace UciCardiaco.Datos.Mapping.Usuarios
{
    public class RolMap : IEntityTypeConfiguration<Rol>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Rol> builder)
        {
            builder.ToTable("ROL")
                .HasKey(r => r.idrol);
        }
    }
}
