using Application.Interfaces;
using Domain.Entities;

namespace Application.Services
{
    public class FuncionesService : IFuncionesService
    {
        private readonly IFuncionesCommand _command;
        private readonly IFuncionesQuery _query;

        public FuncionesService(IFuncionesCommand command, IFuncionesQuery query)
        {
            _command = command;
            _query = query;
        }
        public IEnumerable<Funciones> GetAll()
        {
            return _query.GetAll();
        }

        public Funciones GetById(int funcionId)
        {
            return _query.GetById(funcionId);
        }
        public Funciones DeleteFuncion(int funcionId)
        {
            return _command.DeleteFuncion(funcionId);
        }

        public bool ExisteFuncionEnSalaHorario(int salaId, DateTime fecha, TimeSpan horaInicio, TimeSpan horaFin)
        {
            var funcionesDelDia = _query.ListarFuncionesPorDia(fecha);
            return funcionesDelDia.Any(f =>
                f.SalaId == salaId &&
                ((horaInicio >= f.Horario && horaInicio < f.Horario + TimeSpan.FromHours(2.5)) ||
                 (horaFin > f.Horario && horaFin <= f.Horario + TimeSpan.FromHours(2.5))));
        }
        public IEnumerable<Funciones> ListarFuncionesPorDia(DateTime fecha)
        {
            return _query.GetAll().Where(f => f.Fecha.Date == fecha.Date);

        }
        public IEnumerable<Funciones> ListarFuncionesPorDiaYTitulo(DateTime? fecha, string? titulo)
        {
            var funciones = _query.GetAll();

            if (fecha.HasValue)
            {
                funciones = funciones.Where(f => f.Fecha.Date == fecha.Value.Date);
            }

            if (!string.IsNullOrEmpty(titulo))
            {
                funciones = funciones.Where(f => f.Pelicula.Titulo.Contains(titulo, StringComparison.OrdinalIgnoreCase));
            }

            return funciones;
        }

        public Funciones RegistrarFuncion(int peliculaId, int salaId, DateTime fecha, TimeSpan horario)
        {
            return _command.RegistrarFuncion(peliculaId, salaId, fecha, horario);
        }
    }
}
