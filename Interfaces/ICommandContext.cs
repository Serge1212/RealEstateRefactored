namespace RealEstateRefactored.Interfaces
{
    /// <summary>
    /// The context class that identifies a command/s passed by a user and determines further actions.
    /// </summary>
    public interface ICommandContext
    {
        /// <summary>
        /// Splits raw command into multiple commands.
        /// </summary>
        /// <param name="rawCommand">May have either one command or multiple ones.</param>
        void ProcessCommands(string rawCommand);
    }
}
