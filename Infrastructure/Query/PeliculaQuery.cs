using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;

public class PeliculaQuery : IPeliculasQuery
{
    private readonly AppDbContext _context;

    public PeliculaQuery(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Peliculas> GetAll()
    {
        return _context.Peliculas;
    }

    public Peliculas GetById(int PeliculaId)
    {
        return _context.Peliculas.FirstOrDefault(p => p.PeliculaId == PeliculaId);
    }

    public Peliculas GetByTitulo(string Titulo)
    {
        return _context.Peliculas.FirstOrDefault(p => p.Titulo == Titulo);
    }
}
