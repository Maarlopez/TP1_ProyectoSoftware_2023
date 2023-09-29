using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Command
{
    public class PeliculasCommand : IPeliculasCommand
    {
        private readonly AppDbContext _context;

        private readonly IPeliculasQuery _query;

        public PeliculasCommand(AppDbContext context)
        {
            _context = context;
        }
        public PeliculasCommand()
        {
        }
        public Peliculas DeletePelicula(int peliculaId)
        {
            Peliculas delete = _query.GetById(peliculaId);

            if (delete != null)
            {
                _context.Remove(delete);
                _context.SaveChanges();
            }
            return delete;
        }
    }
}