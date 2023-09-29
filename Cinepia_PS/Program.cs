using Application.Services;
using Cinepia_PS.Controllers;
using Infrastructure.Command;
using Infrastructure.Persistence;
using Infrastructure.Query;


namespace Cinepia_PS
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var dbContext = new AppDbContext())
            {
                int option;

                var funcionesQuery = new FuncionQuery(dbContext);
                var funcionesCommand = new FuncionesCommand(funcionesQuery, dbContext);
                // Crear una instancia de FuncionesServices
                var funcionesServices = new FuncionesService(funcionesCommand, funcionesQuery);

                var salasQuery = new SalaQuery(dbContext);
                var salasCommand = new SalasCommand(dbContext);
                // Crear una instancia de SalasService
                var salasService = new SalasService(salasCommand, salasQuery);

                var peliculasQuery = new PeliculaQuery(dbContext);
                var peliculasCommand = new PeliculasCommand(dbContext);
                // Crear una instancia de PeliculaService
                var peliculasService = new PeliculasService(peliculasCommand, peliculasQuery);


                // Crear una instancia de FuncionesConsoleController y pasar los servicios
                var funcionesController = new FuncionesConsoleController(funcionesServices, salasService, peliculasService);

                do
                {
                    Console.WriteLine("Bienvenido al menú principal");
                    Console.WriteLine("1. Listar funciones");
                    Console.WriteLine("2. Registrar Funcion");
                    Console.WriteLine("3. Eliminar Función");
                    Console.WriteLine("4. Salir");

                    if (!int.TryParse(Console.ReadLine(), out option))
                    {
                        Console.WriteLine("La opción ingresada no es válida. Por favor, ingrese un número válido.");
                        continue; // Vuelve al inicio del bucle para que el usuario ingrese una opción válida.
                    }

                    switch (option)
                    {
                        case 1:
                            Console.Clear();
                            funcionesController.RunListar();
                            break;

                        case 2:
                            Console.Clear();
                            funcionesController.RunRegistrar();
                            break;

                        case 3:
                            Console.Clear();
                            funcionesController.RunEliminar();
                            break;

                        case 4:
                            // Opción para salir del programa
                            Console.WriteLine("Saliendo del programa...");
                            break;

                        default:
                            Console.WriteLine("La opción ingresada no es válida. Por favor, seleccione una opción válida del menú.");
                            break;
                    }

                } while (option != 4);
            }
        }
    }
}