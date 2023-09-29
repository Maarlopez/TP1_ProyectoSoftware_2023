using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Command
{
    public class FuncionesCommand : IFuncionesCommand
    {
        private readonly AppDbContext _context;

        private readonly IFuncionesQuery _query;

        public FuncionesCommand(IFuncionesQuery query, AppDbContext context)
        {
            _context = context;
            _query = query;
        }

        public FuncionesCommand()
        {
        }

        public Funciones RegistrarFuncion(int peliculaId, int salaId, DateTime fecha, TimeSpan horario)
        {
            var nuevaFuncion = new Funciones
            {
                PeliculaId = peliculaId,
                SalaId = salaId,
                Fecha = fecha,
                Horario = horario,
                Tickets = new List<Tickets>()
            };

            _context.Funciones.Add(nuevaFuncion);
            _context.SaveChanges();

            return nuevaFuncion;
        }
        public Funciones DeleteFuncion(int funcionId)
        {
            if (_context == null)
            {
                Console.WriteLine("El contexto de la base de datos no ha sido inicializado.");
            }

            Funciones delete = _query.GetById(funcionId);

            if (delete != null)
            {
                _context.Remove(delete);
                _context.SaveChanges();
            }
            return delete;
        }
    }
}