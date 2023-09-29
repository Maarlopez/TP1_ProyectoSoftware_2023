using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Command
{
    public class GenerosCommand : IGenerosCommand
    {
        private readonly AppDbContext _context;

        private readonly IGenerosQuery _query;

        public GenerosCommand(AppDbContext context, IGenerosQuery query)
        {
            _context = context;
            _query = query;
        }

        public GenerosCommand()
        {
        }
        public Generos DeleteGenero(int generoId)
        {
            Generos delete = _query.GetById(generoId);

            if (delete != null)
            {
                _context.Remove(delete);
                _context.SaveChanges();
            }
            return delete;
        }
    }
}