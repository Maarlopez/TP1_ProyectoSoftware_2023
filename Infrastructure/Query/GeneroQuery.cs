using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Query
{
    public class GeneroQuery : IGenerosQuery
    {
        private readonly AppDbContext _context;

        public GeneroQuery(AppDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Generos> GetAll()
        {
            return _context.Generos.ToList();
        }

        public Generos? GetById(int GeneroId)
        {
            return _context.Generos.FirstOrDefault(genero => genero.GeneroId == GeneroId);
        }
    }
}
