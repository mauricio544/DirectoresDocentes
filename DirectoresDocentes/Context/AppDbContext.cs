using System.Data.Entity;
using DirectoresDocentes.Models.BachilleratoModels;

namespace DirectoresDocentes.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
            : base("AcadConnection")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<ALUMNOS_FICHAS> Alumnos_Fichas { get; set; }
        public virtual DbSet<PERIODOS> Periodos { get; set; }
        public virtual DbSet<PERSONA_ROLES> Personas_Roles { get; set; }
        public virtual DbSet<PERSONAS> Personas { get; set; }
        public virtual DbSet<ROLES> Roles { get; set; }
        public virtual DbSet<FACULTADES_ESCUELAS> Facultades_Escuelas { get; set; }
        public virtual DbSet<PROGRAMAS_ESTUDIOS> Programas_Estudios { get; set; }
        public virtual DbSet<PLANES_ESTUDIOS> Planes_Estudios { get; set; }
        public virtual DbSet<PROGRAMAS_ESTUDIOS_PERSONAS> Programas_Estudios_Personas { get; set; }
        public virtual DbSet<PROFESORES_FICHAS> Profesores_Fichas { get; set; }
        public virtual DbSet<TIPOS_ALUMNO_ESTADO> TiposAlumnoEstados { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("ACADEMICO");
            modelBuilder.Entity<PERSONA_ROLES>()
                .HasRequired(p => p.PERSONAS)
                .WithMany()
                .HasForeignKey(p => new {p.PERSONAS_CODIGO});
        }
    }
}