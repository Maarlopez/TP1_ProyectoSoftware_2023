using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {

        public DbSet<Peliculas> Peliculas { get; set; }
        public DbSet<Funciones> Funciones { get; set; }
        public DbSet<Generos> Generos { get; set; }
        public DbSet<Tickets> Tickets { get; set; }
        public DbSet<Salas> Salas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=Cinepia;Trusted_Connection=True;TrustServerCertificate=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Peliculas>(entity =>
            {
                entity.HasKey(p => p.PeliculaId);

                entity.Property(p => p.Titulo)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(p => p.Sinopsis)
                    .HasMaxLength(255)
                    .IsRequired();

                entity.Property(p => p.Poster)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(p => p.Trailer)
                    .HasMaxLength(100)
                    .IsRequired();

                // Relación con Funciones
                entity.HasMany(p => p.Funciones)
                    .WithOne(f => f.Pelicula)
                    .HasForeignKey(f => f.PeliculaId)
                    .IsRequired();

                // Relación con Generos
                entity.HasOne(p => p.Genero)
                    .WithMany(g => g.Peliculas)
                    .HasForeignKey(p => p.GeneroId)
                    .IsRequired();
            });

            modelBuilder.ApplyConfiguration(new PeliculaData());

            modelBuilder.Entity<Funciones>(entity =>
            {
                entity.HasKey(f => f.FuncionId);

                entity.Property(f => f.Fecha)
                    .IsRequired();

                entity.Property(f => f.Horario)
                    .IsRequired();

                // Relación con Peliculas
                entity.HasOne(f => f.Pelicula)
                    .WithMany(p => p.Funciones)
                    .HasForeignKey(f => f.PeliculaId)
                    .IsRequired();

                // Relación con Salas
                entity.HasOne(f => f.Sala)
                    .WithMany(s => s.Funciones)
                    .HasForeignKey(f => f.SalaId)
                    .IsRequired();

                // Relación con Tickets
                entity.HasMany(f => f.Tickets)
                    .WithOne(t => t.Funcion)
                    .HasForeignKey(t => t.FuncionId)
                    .IsRequired();
            });

            modelBuilder.Entity<Generos>(entity =>
            {
                entity.HasKey(g => g.GeneroId);

                entity.Property(g => g.Nombre)
                    .HasMaxLength(50)
                    .IsRequired();

                // Relación con Peliculas
                entity.HasMany(g => g.Peliculas)
                    .WithOne(p => p.Genero)
                    .HasForeignKey(p => p.GeneroId)
                    .IsRequired();
            });

            modelBuilder.ApplyConfiguration(new GeneroData());


            modelBuilder.Entity<Tickets>(entity =>
            {
                entity.HasKey(t => t.TicketId);

                entity.Property(t => t.Usuario)
                    .HasMaxLength(50)
                    .IsRequired();

                // Relación con Funciones
                entity.HasOne(t => t.Funcion)
                    .WithMany(f => f.Tickets)
                    .HasForeignKey(t => t.FuncionId)
                    .IsRequired();
            });

            modelBuilder.Entity<Salas>(entity =>
            {
                entity.HasKey(s => s.SalaId);

                entity.Property(s => s.Nombre)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(s => s.Capacidad)
                    .IsRequired();

                // Relación con Funciones
                entity.HasMany(s => s.Funciones)
                    .WithOne(f => f.Sala)
                    .HasForeignKey(f => f.SalaId)
                    .IsRequired();
            });
            modelBuilder.ApplyConfiguration(new SalaData());
        }
    }
}