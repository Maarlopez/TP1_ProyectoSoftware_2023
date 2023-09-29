using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Query
{
    public class SalaQuery : ISalasQuery
    {
        private readonly AppDbContext _context;

        public SalaQuery(AppDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Salas> GetAll()
        {
            return _context.Salas;
        }

        public Salas GetById(int SalaId)
        {
            return _context.Salas.FirstOrDefault(sala => sala.SalaId == SalaId);
        }
    }
}
