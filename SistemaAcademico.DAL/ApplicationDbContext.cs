using Microsoft.EntityFrameworkCore;
using SistemaAcademico.DAL.Models;

namespace SistemaAcademico.DAL
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

 
        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Periodo> Periodos { get; set; }
        public DbSet<Matricula> Matriculas { get; set; }
        public DbSet<Nota> Notas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
 
            modelBuilder.Entity<Estudiante>().HasKey(e => e.id_estudiante);
            modelBuilder.Entity<Curso>().HasKey(c => c.id_curso);
            modelBuilder.Entity<Periodo>().HasKey(p => p.id_periodo);
            modelBuilder.Entity<Matricula>().HasKey(m => m.id_matricula);
            modelBuilder.Entity<Nota>().HasKey(n => n.id_nota);

            base.OnModelCreating(modelBuilder);
        }
    }
}
