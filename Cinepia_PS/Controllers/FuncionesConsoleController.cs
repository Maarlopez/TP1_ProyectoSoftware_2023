using Application.Interfaces;
using System.Globalization;

namespace Cinepia_PS.Controllers
{
    public class FuncionesConsoleController
    {
        private readonly IFuncionesService _funcionesService;
        private readonly ISalasService _salaService;
        private readonly IPeliculasService _peliculaService;

        public FuncionesConsoleController(IFuncionesService funcionesService, ISalasService salaService, IPeliculasService peliculasService)
        {
            _funcionesService = funcionesService;
            _salaService = salaService;
            _peliculaService = peliculasService;
        }
        public void ListarFuncionesPorDiaYTitulo()
        {
            DateTime? fecha = null;
            string? titulo = null;

            Console.WriteLine("Ingrese la fecha (dd/MM) o presione Enter para omitir:");
            string fechaInput = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(fechaInput))
            {
                if (!DateTime.TryParseExact(fechaInput, "dd/MM", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fechaParsed))
                {
                    Console.WriteLine("Formato de fecha no válido. Por favor, intente de nuevo.");
                    return;
                }
                else if (fechaParsed.Date < DateTime.Now.Date)
                {
                    Console.WriteLine("La fecha ingresada es una fecha pasada. Por favor, intente de nuevo.");
                    return;
                }
                else
                {
                    fecha = fechaParsed;
                }
            }

            Console.WriteLine("Ingrese el título de la película o presione Enter para omitir:");
            string tituloInput = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(fechaInput) && string.IsNullOrWhiteSpace(tituloInput))
            {
                Console.WriteLine("No ha ingresado ningún valor.");
                Console.ReadKey();
                Console.Clear();
                return;
            }

            if (!string.IsNullOrWhiteSpace(tituloInput))
            {
                titulo = tituloInput;
            }

            var funciones = _funcionesService.ListarFuncionesPorDiaYTitulo(fecha, titulo);

            if (funciones.Any())
            {
                Console.WriteLine("Funciones encontradas:");
                foreach (var funcion in funciones)
                {
                    Console.WriteLine($"ID: {funcion.FuncionId}");
                    Console.WriteLine($"Título: {funcion.Pelicula.Titulo}");
                    Console.WriteLine($"Fecha: {funcion.Fecha:dd/MM}");
                    Console.WriteLine($"Hora: {funcion.Horario:hh\\:mm}");
                }
            }
            else
            {
                Console.WriteLine("No se encontraron funciones con los criterios especificados.");
            }
            Console.Write("Presione cualquier tecla para volver al menú principal...");
            Console.ReadKey(true);
            Console.Clear();
        }

        public void RegistrarNuevaFuncion()
        {
            int peliculaId;
            while (true)
            {
                int minPeliculaId = _peliculaService.GetMinId();
                int maxPeliculaId = _peliculaService.GetMaxId();

                Console.WriteLine($"Ingrese el ID de la película (rango válido: {minPeliculaId} - {maxPeliculaId}):");
                string peliculaIdInput = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(peliculaIdInput))
                {
                    Console.WriteLine("No ha ingresado ningún ID de película. Por favor, intente de nuevo.");
                    continue;
                }

                if (!int.TryParse(peliculaIdInput, out peliculaId))
                {
                    Console.WriteLine("El ID de la película ingresado no es válido. Debe ingresar un número entero.");
                    continue;
                }

                var pelicula = _peliculaService.GetById(peliculaId);
                if (pelicula == null)
                {
                    Console.WriteLine($"La película con el ID especificado no existe. Por favor, intente con un ID en el rango de {minPeliculaId} a {maxPeliculaId}.");
                    continue;
                }
                break;
            }

            int salaId;
            while (true)
            {
                int minSalaId = _salaService.GetMinId();
                int maxSalaId = _salaService.GetMaxId();

                Console.WriteLine($"Ingrese el ID de la sala (rango válido: {minSalaId} - {maxSalaId}):");
                string salaIdInput = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(salaIdInput))
                {
                    Console.WriteLine("No ha ingresado ningún ID de sala. Por favor, intente de nuevo.");
                    continue;
                }

                if (!int.TryParse(salaIdInput, out salaId))
                {
                    Console.WriteLine("El ID de la sala ingresado no es válido. Debe ingresar un número entero.");
                    continue;
                }

                var sala = _salaService.GetById(salaId);
                if (sala == null)
                {
                    Console.WriteLine($"La sala con el ID especificado no existe. Por favor, intente con un ID en el rango de {minSalaId} a {maxSalaId}.");
                    continue;
                }
                break;
            }

            DateTime fecha;
            while (true)
            {
                Console.WriteLine("Ingrese la fecha (dd/MM):");
                string fechaInput = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(fechaInput))
                {
                    Console.WriteLine("No ha ingresado ninguna fecha. Por favor, intente de nuevo.");
                    continue;
                }

                if (!DateTime.TryParseExact(fechaInput, "dd/MM", CultureInfo.InvariantCulture, DateTimeStyles.None, out fecha))
                {
                    Console.WriteLine("Formato de fecha inválido. Por favor, ingrese la fecha en formato dd/MM.");
                    continue;
                }

                if (fecha < DateTime.Now.Date)
                {
                    Console.WriteLine("La fecha ingresada es una fecha pasada. Por favor, ingrese una fecha futura.");
                    continue;
                }
                break;
            }

            TimeSpan horario;
            while (true)
            {
                Console.WriteLine("Ingrese el horario (hh:mm):");
                string horarioInput = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(horarioInput))
                {
                    Console.WriteLine("No ha ingresado ningún horario. Por favor, intente de nuevo.");
                    continue;
                }

                if (!TimeSpan.TryParse(horarioInput, out horario))
                {
                    Console.WriteLine("Formato de horario inválido. Por favor, ingrese el horario en formato hh:mm.");
                    continue;
                }

                TimeSpan duracionFuncion = TimeSpan.FromHours(2.5);
                TimeSpan horaFin = horario + duracionFuncion;

                if (_funcionesService.ExisteFuncionEnSalaHorario(salaId, fecha, horario, horaFin))
                {
                    Console.WriteLine("No se puede crear la función ya que se superpone con otra función existente en la misma sala.");
                    continue;
                }
                break;
            }

            var nuevaFuncion = _funcionesService.RegistrarFuncion(peliculaId, salaId, fecha, horario);

            Console.Clear();

            Console.WriteLine("Nueva función registrada:");
            Console.WriteLine($"ID de la funcion: {nuevaFuncion.FuncionId}");
            Console.WriteLine($"Pelicula: {nuevaFuncion.Pelicula.Titulo}");
            Console.WriteLine($"Sala: {nuevaFuncion.Sala.Nombre}");
            Console.WriteLine($"Fecha: {nuevaFuncion.Fecha:dd/MM}");
            Console.WriteLine($"Horario: {nuevaFuncion.Horario:hh\\:mm}");

            Console.Write("Presione cualquier tecla para volver al menú principal...");
            Console.ReadKey(true);
            Console.Clear();
        }
        public void EliminarFuncion()
        {
            while (true)
            {
                Console.WriteLine("Ingrese el ID de la función que desea eliminar o escriba 's' para volver al menú principal:");
                string funcionIdInput = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(funcionIdInput))
                {
                    Console.WriteLine("No ha ingresado ningún ID. Por favor, intente de nuevo.");
                    continue;
                }

                if (funcionIdInput.Trim() == "s")
                {
                    Console.Clear();
                    return;
                }

                if (!int.TryParse(funcionIdInput, out int funcionId))
                {
                    Console.WriteLine("El ID de la función ingresado no es válido. Debe ingresar un número entero.");
                    Console.WriteLine("Por favor, intente de nuevo.");
                    continue;
                }

                var funcionEliminada = _funcionesService.DeleteFuncion(funcionId);

                if (funcionEliminada != null)
                {
                    Console.WriteLine("Función eliminada exitosamente.\n");
                    break; // Función eliminada exitosamente, saliendo del bucle
                }
                else
                {
                    Console.WriteLine("No se encontró una función con el ID proporcionado. Por favor, intente con un ID diferente.");
                }
            }

            Console.WriteLine("Presione cualquier tecla para volver al menú principal...");
            Console.ReadKey();
            Console.Clear();
        }

        public void RunListar()
        {
            ListarFuncionesPorDiaYTitulo();
        }
        public void RunRegistrar()
        {
            RegistrarNuevaFuncion();
        }
        public void RunEliminar()
        {
            EliminarFuncion();
        }
    }
}
