using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using UciCardiaco.Datos.Mapping.Almacen;
using UciCardiaco.Datos.Mapping.Diagnosticos;
using UciCardiaco.Datos.Mapping.Usuarios;
using UciCardiaco.Entidad.Almacen;
using UciCardiaco.Entidad.Diagnosticos;
using UciCardiaco.Entidad.Usuarios;

namespace UciCardiaco.Datos
{
    public class DbContextUciCardiaco : DbContext
    {   

        //EXPONER LA COLECCION ENFERMEDAD EN UN OBJETO LLAMADO ENFERMEDAD
        public DbSet<Enfermedad> Enfermedades { get; set; }
        public DbSet<Sintoma> Sintomas { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Persona> Personas { get; set; }
        public DbSet<Historia> Historias { get; set; }
        public DbSet<DetalleHistoria> DetallesHistorias { get; set; }
        public DbSet<Diagnostico> Diagnosticos { get; set; }
        public DbSet<DetalleDiagnostico> DetallesDiagnosticos { get; set; }

        //METODO CONSTRUCTOR
        public DbContextUciCardiaco(DbContextOptions<DbContextUciCardiaco> options) : base(options)
        {

        }
        //METODO PARA MAPEAR TABLAS RESPECTO ENTIDADES CON LA BASE DE DATOS

        protected override void  OnModelCreating(ModelBuilder modelBuilder) 
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new EnfermedadMap());
            modelBuilder.ApplyConfiguration(new SintomaMap());
            modelBuilder.ApplyConfiguration(new RolMap());
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new PersonaMap());
            modelBuilder.ApplyConfiguration(new HistoriaMap());
            modelBuilder.ApplyConfiguration(new DetalleHistoriaMap());
            modelBuilder.ApplyConfiguration(new DiagnosticoMap());
            modelBuilder.ApplyConfiguration(new DetalleDiagnosticoMap());


        }


    }
}
