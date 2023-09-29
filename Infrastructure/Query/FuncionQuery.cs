using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Query
{
    public class FuncionQuery : IFuncionesQuery
    {
        private readonly AppDbContext _context;

        public FuncionQuery(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Funciones> GetAll()
        {
            return _context.Funciones
                .Include(f => f.Pelicula)
                .Include(f => f.Sala)
                .Include(f => f.Tickets);
        }

        public Funciones? GetById(int funcionId)
        {
            var funcion = _context.Funciones
                .Include(f => f.Pelicula)
                .Include(f => f.Sala)
                .Include(f => f.Tickets)
                .FirstOrDefault(f => f.FuncionId == funcionId);

            return funcion;
        }

        public IEnumerable<Funciones> ListarFuncionesPorDia(DateTime fecha)
        {
            return _context.Funciones
                .Include(f => f.Pelicula)
                .Include(f => f.Sala)
                .Include(f => f.Tickets)
                .Where(f => f.Fecha.Date == fecha.Date)
                .ToList();
        }

        public IEnumerable<Funciones> ListarFuncionesPorDiaYTitulo(DateTime? fecha, string? titulo)
        {
            var query = _context.Funciones
                        .Include(f => f.Pelicula)
                        .Include(f => f.Sala)
                        .Include(f => f.Tickets)
                        .AsQueryable();

            if (fecha.HasValue)
            {
                query = query.Where(f => f.Fecha.Date == fecha.Value.Date);
            }

            if (!string.IsNullOrWhiteSpace(titulo))
            {
                query = query.Where(f => f.Pelicula.Titulo.Contains(titulo, StringComparison.OrdinalIgnoreCase));
            }

            return query.ToList();
        }

    }
}
