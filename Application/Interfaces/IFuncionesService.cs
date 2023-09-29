using Domain.Entities;

namespace Application.Interfaces
{
    public interface IFuncionesService
    {
        IEnumerable<Funciones> GetAll();
        Funciones? GetById(int funcionesId);
        IEnumerable<Funciones> ListarFuncionesPorDiaYTitulo(DateTime? fecha, string? titulo);
        IEnumerable<Funciones> ListarFuncionesPorDia(DateTime fecha);
        bool ExisteFuncionEnSalaHorario(int salaId, DateTime fecha, TimeSpan horaInicio, TimeSpan horaFin);
        Funciones RegistrarFuncion(int peliculaId, int salaId, DateTime fecha, TimeSpan horario);
        Funciones DeleteFuncion(int funcionId);
    }
}
