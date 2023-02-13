using RealEstateRefactored.Interfaces;

namespace RealEstateRefactored
{
    public class AppRunner : IAppRunner
    {
        private readonly IDbConnection _connection;

        public AppRunner(IDbConnection connection)
        {
            _connection = connection;
        }

        public void StartApp()
        {
            _connection.Load();
            Console.WriteLine("Hello");
        }
    }
}
