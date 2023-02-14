using RealEstateRefactored.Interfaces;
using RealEstateRefactored.Interfaces.Strategies;
using RealEstateRefactored.Models;

namespace RealEstateRefactored.Services.Strategies
{
    public class ShowTablesStrategy : IShowTablesStrategy
    {
        private readonly IDbContext _context;

        public ShowTablesStrategy(IDbContext context)
        {
            _context = context;
        }

        public void Invoke(Command command)
        {
            PrintTables();
        }

        private void PrintTables()
        {
            Console.WriteLine(new string('=', 20));
            if (_context.Tables is null || !_context.Tables.Any())
            {
                Console.WriteLine("[No Tables]");
            }
            else
            {
                foreach (Table t in _context.Tables)
                {
                    Console.WriteLine(t.Name);
                }
            }
            Console.WriteLine(new string('=', 20));
        }
    }
}
