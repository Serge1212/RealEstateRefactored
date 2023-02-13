using RealEstateRefactored.Interfaces;

namespace RealEstateRefactored
{
    public class AppRunner : IAppRunner
    {
        private readonly IDbConnection _connection;
        private readonly IDbContext _context;
        //private readonly ICommandContext _commandContext;

        public AppRunner(IDbConnection connection, IDbContext context/*, ICommandContext commandContext*/)
        {
            _connection = connection;
            //_commandContext = commandContext;
            _context = context;
        }

        public void StartApp()
        {
            _connection.Load();
            _connection.Save();
            Console.WriteLine("Hello");
            Console.ReadLine();
        }
    }
}
