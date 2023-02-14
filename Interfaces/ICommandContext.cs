namespace RealEstateRefactored.Interfaces
{
    /// <summary>
    /// The context class that identifies a command/s passed by a user and determines further actions.
    /// </summary>
    public interface ICommandContext
    {
        /// <summary>
        /// Processes the command/s sent by user input.
        /// </summary>
        /// <param name="rawCommand">The actual input that a user sent.</param>
        void ProcessCommands(string rawCommand);
    }
}
