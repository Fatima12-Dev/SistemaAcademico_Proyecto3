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

            modelBuilder.Entity<Matricula>()
                .HasOne(m => m.Estudiante)
                .WithMany(e => e.Matriculas)
                .HasForeignKey(m => m.id_estudiante)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Matricula>()
                .HasOne(m => m.Curso)
                .WithMany(c => c.Matriculas)
                .HasForeignKey(m => m.id_curso)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Matricula>()
                .HasOne(m => m.Periodo)
                .WithMany(p => p.Matriculas)
                .HasForeignKey(m => m.id_periodo)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Nota>()
                .HasOne(n => n.Matricula)
                .WithMany(m => m.Notas)
                .HasForeignKey(n => n.id_matricula)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Nota>().Property(n => n.nota1).HasPrecision(18, 2);
            modelBuilder.Entity<Nota>().Property(n => n.nota2).HasPrecision(18, 2);
            modelBuilder.Entity<Nota>().Property(n => n.nota3).HasPrecision(18, 2);
            modelBuilder.Entity<Nota>().Property(n => n.promedio).HasPrecision(18, 2);

            base.OnModelCreating(modelBuilder);
        }
    }
}
