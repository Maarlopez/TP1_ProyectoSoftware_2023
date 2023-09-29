using Domain.Entities;

namespace Application.Interfaces
{
    public interface IFuncionesQuery
    {
        IEnumerable<Funciones> GetAll();
        Funciones? GetById(int funcionId);
        IEnumerable<Funciones> ListarFuncionesPorDia(DateTime fecha);
        IEnumerable<Funciones> ListarFuncionesPorDiaYTitulo(DateTime? fecha, string? titulo);
    }
}
