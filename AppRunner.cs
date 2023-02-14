using RealEstateRefactored.Interfaces;

namespace RealEstateRefactored
{
    /// <inheritdoc/>
    public class AppRunner : IAppRunner
    {
        private readonly IDbConnection _connection;
        private readonly ICommandContext _commandContext;

        public AppRunner(IDbConnection connection, ICommandContext commandContext)
        {
            _connection = connection;
            _commandContext = commandContext;
        }

        /// <inheritdoc/>
        public void StartApp()
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode; //for cyrillic
            Console.InputEncoding = System.Text.Encoding.Unicode; //for cyrillic

            _connection.Load();

            Console.WriteLine("Welcome to the Real Estate application!");
            Console.WriteLine("The database currently contains the following tables:");
            _commandContext.ProcessCommands("SHOW TABLES");
            Console.WriteLine("Enter the desired command/s (SQL syntax):");

            StartReceiving();

            _connection.Save();
        }

        private void StartReceiving()
        {
            string rawCommand;
            while ((rawCommand = Console.ReadLine()) is not null)
            {
                if(rawCommand.ToLowerInvariant() is "exit")
                {
                    break;
                }
                _commandContext.ProcessCommands(rawCommand);
            }
        }
    }
}
