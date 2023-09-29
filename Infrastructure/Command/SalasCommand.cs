using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Command
{
    public class SalasCommand : ISalasCommand
    {
        private readonly AppDbContext _context;

        private readonly ISalasQuery _query;

        public SalasCommand(AppDbContext context, ISalasQuery query)
        {
            _context = context;
            _query = query;
        }
        public SalasCommand(AppDbContext dbContext)
        {
        }

        public Salas DeleteSala(int salaId)
        {
            Salas delete = _query.GetById(salaId);

            if (delete != null)
            {
                _context.Remove(delete);
                _context.SaveChanges();
            }
            return delete;
        }
    }
}
